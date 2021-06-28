using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using NLog;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using RXD.pojo;

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
                string id = dt.Rows[i].ItemArray[0].ToString();
                int port = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                bool states = (bool)dt.Rows[i].ItemArray[3];

                string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
                string fileName = @"ReceivedTofile" + id + "-TCPCLIENT-" + time;

                //传感器状态： 0：关闭；  1：开启
                if (!states)
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

        public void SendPost(List<SendMsg> list,string urlstr)
        {
            if (list == null || list.Count == 0)
            {
                _logger.Trace("url:{"+urlstr+"},推送数据为空");
                return;
            }
            urlstr = urlstr.Trim();
            if (urlstr == "" || urlstr.Length == 0)
            {
                _logger.Trace("推送地址为空");
                return;
            }
            //string pattern = @"^http://((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?):(8080)?(8888)?$";
            //if(!Regex.IsMatch(urlstr, pattern))
            //{
            //    _logger.Trace("IP地址格式不正确：" + urlstr);
            //    return;
            //}
            //TODO:通过数据库获取推送URL
            string url = urlstr + "/app/monitorData.htm";
            string strJson = JsonConvert.SerializeObject(list);
            Console.WriteLine(strJson);
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