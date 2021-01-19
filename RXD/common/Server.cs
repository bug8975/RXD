using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using NLog;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using OneNet.pojo;

namespace RXD.common
{
    class Server
    {
        private Server() { }
        private static Server instance = null;
        private static readonly object locker = new object();
        Logger _logger = LogManager.GetCurrentClassLogger();
        public CancellationTokenSource ctsToken = new CancellationTokenSource();

        public static Server GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Server();
                    }
                }
            }
            return instance;
        }

        public void Start()
        {
            string sql = "select * from sensor";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool type = (bool)dt.Rows[i].ItemArray[3];
                bool states = (bool)dt.Rows[i].ItemArray[6];
                int port = Convert.ToInt32(dt.Rows[i].ItemArray[5]);
                string id = dt.Rows[i].ItemArray[0].ToString();

                string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
                string fileName = @"ReceivedTofile" + id + "-TCPCLIENT-" + time;

                if (type)
                    continue;
                if (states)
                    continue;

                TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                Dictionary<int, object> tcpDic = new Dictionary<int, object> { { 0, fileName }, { 1, tcpListener } };
                ThreadPool.QueueUserWorkItem(o => { ListenForClients(tcpDic); });

                Dictionary<int, string> insertParamDic = new Dictionary<int, string> { { 0, fileName }, { 1, "" }, { 2, id } };
                ThreadPool.QueueUserWorkItem(new WaitCallback(insertFile), insertParamDic);
                Console.WriteLine("Server started at {0} :{1} @ {2}", IPAddress.Any, port, DateTime.Now.ToString());
            }
        }

        private void ListenForClients(object obj)
        {
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            object fileName = dic[0];
            TcpListener listener = dic[1] as TcpListener;
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            Dictionary<int, object> infoDic = new Dictionary<int, object> { { 0, fileName }, { 1, client }, { 2, listener } };
            ThreadPool.QueueUserWorkItem(o => HandleClientComm(infoDic, ctsToken.Token));
        }


        private void HandleClientComm(object obj, CancellationToken token)
        {
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            string fileName = dic[0].ToString() + ".DAT";
            TcpClient tcpClient = (TcpClient)dic[1];
            TcpListener listener = (TcpListener)dic[2];
            Console.WriteLine("Client @[{0}] connected @{1}", tcpClient.Client.LocalEndPoint, DateTime.Now.ToString());

            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead = 0;

            while (!token.IsCancellationRequested)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    _logger.Trace("Error:receive msg error --HandleClientComm");
                    break;
                }

                if (bytesRead == 0)
                {
                    Console.WriteLine("Client @[{0}] disconnect @{1}", tcpClient.Client.LocalEndPoint, DateTime.Now.ToString());
                    break;
                }

                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default, true))
                    {
                        byte[] tem = new byte[bytesRead];
                        Array.Copy(message, 0, tem, 0, bytesRead);
                        bw.Write(tem);
                        bw.Flush();
                    }
                }

            }

            tcpClient.Close();
            listener.Stop();
        }

        private void SendPost(string[] recList)
        {
            if (recList == null || recList.Length == 0)
                return;
            string sn = recList[2];
            string sql = "select m.name as monitorname,s.name sensorname,bi.type,bi.L,st.code,m.projectid from bindinfo bi LEFT JOIN sensor s on bi.sensorid = s.id LEFT JOIN monitor m on s.monitorid = m.id LEFT JOIN sensor_type st ON s.sensortypeid = st.id where sn = ?";
            MySqlParameter mp = new MySqlParameter(@"sn", MySqlDbType.VarChar) { Value = sn };
            DataTable dt = common.MySqlHelper.GetDataSet(sql, mp).Tables[0];
            int projectid = 1;
            List<SendMsg> list = new List<SendMsg>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string monitorline = dt.Rows[i].ItemArray[0].ToString();
                string sensorname = dt.Rows[i].ItemArray[1].ToString();
                int type = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                int L = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
                string code = dt.Rows[i].ItemArray[4].ToString();
                projectid = Convert.ToInt32(dt.Rows[i].ItemArray[5]);
                DateTime DateStart = new DateTime(1970, 1, 1, 8, 0, 0);
                DateTime Now = Convert.ToDateTime(recList[3]);
                string timeStr = (Now - DateStart).TotalMilliseconds.ToString();
                double x = 0d;
                double y = 0d;
                double z = 0d;
                switch (type)
                {
                    case 0:
                        double.TryParse(recList[5], out x);
                        double.TryParse(recList[6], out y);
                        double.TryParse(recList[7], out z);
                        break;
                    case 1:
                        double.TryParse(recList[8], out x);
                        double.TryParse(recList[9], out y);
                        double.TryParse(recList[10], out z);
                        break;
                    case 2:
                        double.TryParse(recList[8], out x);
                        double.TryParse(recList[9], out y);
                        double.TryParse(recList[10], out z);
                        x = L * Math.Sin(Math.PI / 180 * x);
                        y = L * Math.Sin(Math.PI / 180 * y);
                        z = L * Math.Sin(Math.PI / 180 * z);
                        break;
                    default:
                        break;
                }
                SendMsg msg = new SendMsg(monitorline, sensorname, x, y, z, code, timeStr);
                list.Add(msg);
            }
            //TODO:通过数据库获取推送URL
            string url = "";
            string sql_url = "select ip from url where projectid = ?";
            MySqlParameter param_projectid = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = projectid };
            DataTable dt_url = common.MySqlHelper.GetDataSet(sql_url, param_projectid).Tables[0];
            if (dt_url.Rows.Count > 0)
                url = @dt_url.Rows[0].ItemArray[0].ToString() + "/app/monitorData.htm";
            string strJson = JsonConvert.SerializeObject(list);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            //request.Timeout = 1000;
            byte[] data = Encoding.UTF8.GetBytes(strJson);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
            }
        }


        private void insertFile(object obj)
        {
            try
            {
                Dictionary<int, string> dic = obj as Dictionary<int, string>;
                string sqlCheckName = "select * from file where name = ?";
                MySqlParameter name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = dic[0] };
                MySqlParameter path = new MySqlParameter(@"path", MySqlDbType.VarChar) { Value = dic[1] };
                MySqlParameter createtime = new MySqlParameter(@"createtime", MySqlDbType.DateTime) { Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                MySqlParameter sensor_id = new MySqlParameter(@"sensor_id", MySqlDbType.VarChar) { Value = dic[2] };
                DataTable dt = common.MySqlHelper.GetDataSet(sqlCheckName, name).Tables[0];
                if (dt.Rows.Count != 0)
                    return;
                string sql = "INSERT INTO file (name,path,createtime,sensor_id) VALUES (?,?,?,?)";
                int cnt = common.MySqlHelper.ExecuteNonQuery(sql, name, path, createtime, sensor_id);
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----insertFile方法");
            }
        }
    }


}