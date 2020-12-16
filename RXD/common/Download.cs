using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RXD.common
{
    class Download
    {
        private Download() { }
        private static Download instance = null;
        private static readonly object locker = new object();
        public CancellationTokenSource ctsToken = new CancellationTokenSource();
        Logger _logger = LogManager.GetCurrentClassLogger();
        public static Download GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Download();
                    }
                }
            }
            return instance;
        }

        public void DownloadData()
        {
            try
            {
                ThreadPool.SetMinThreads(1, 1);
                ThreadPool.SetMaxThreads(5, 5);
                string sql = "select * from sensor";
                DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[3].Equals("1"))
                        continue;
                    if (dt.Rows[i].ItemArray[6].Equals("1"))
                        continue;
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(dt.Rows[i].ItemArray[4].ToString()), Convert.ToInt32(dt.Rows[i].ItemArray[5]));
                    socket.Connect(endPoint);
                    string id = dt.Rows[i].ItemArray[0].ToString();
                    Dictionary<int, object> downloadInfoDic = new Dictionary<int, object>
                    {
                        { 0, id },
                        { 1, socket }
                    };
                    string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
                    string fileName = @"ReceivedTofile" + id + "-TCPCLIENT-" + time;
                    Dictionary<int, string> insertParamDic = new Dictionary<int, string>
                    {
                        { 0, fileName },
                        { 1, "" },
                        { 2, dt.Rows[i].ItemArray[0].ToString() }
                    };
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(RecMsg), downloadInfoDic);
                    //Thread threadRecMsg = new Thread(new ParameterizedThreadStart(RecMsg));
                    //threadRecMsg.Start(downloadInfoDic);
                    ThreadPool.QueueUserWorkItem(o => RecMsg(downloadInfoDic, ctsToken.Token));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(insertFile), insertParamDic);
                    //ThreadPool.QueueUserWorkItem(p => { try { RecMsg(id, socket); } catch (Exception ex) { Console.WriteLine(ex.Message); } } );
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message);
            }
        }

        private void RecMsg(object obj,CancellationToken token)
        {
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
            string fileName = @"ReceivedTofile" + dic[0] + "-TCPCLIENT-" + time + ".DAT";
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Append))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default, true))
                    {
                        while (true) //持续监听服务端发来的消息
                        {
                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                            byte[] arrRecMsg = new byte[1024 * 200];
                            Socket s = (Socket)dic[1];
                            int length = s.Receive(arrRecMsg);
                            byte[] tem = new byte[length];
                            Array.Copy(arrRecMsg, 0, tem, 0, length);
                            bw.Write(arrRecMsg);
                            bw.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message);
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
                MySqlParameter createtime = new MySqlParameter(@"createtime", MySqlDbType.DateTime) { Value = DateTime.Now.ToString("yyyy-M-d") };
                MySqlParameter sensor_id = new MySqlParameter(@"sensor_id", MySqlDbType.VarChar) { Value = dic[2] };
                DataTable dt = common.MySqlHelper.GetDataSet(sqlCheckName, name).Tables[0];
                if (dt.Rows.Count != 0)
                    return;
                string sql = "INSERT INTO file (name,path,createtime,sensor_id) VALUES (?,?,?,?)";
                int cnt = common.MySqlHelper.ExecuteNonQuery(sql, name, path, createtime, sensor_id);
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message);
            }
        }

    }
}
