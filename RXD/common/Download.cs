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
                string sql = "select * from sensor";
                DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((bool)dt.Rows[i].ItemArray[3])
                        continue;
                    if ((bool)dt.Rows[i].ItemArray[6])
                        continue;
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(dt.Rows[i].ItemArray[4].ToString()), Convert.ToInt32(dt.Rows[i].ItemArray[5]));
                    string id = dt.Rows[i].ItemArray[0].ToString();
                    Dictionary<int, object> downloadInfoDic = new Dictionary<int, object> { { 0, id }, { 1, socket } };
                    socket.Bind(endPoint);
                    socket.Listen(20);
                    string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
                    string fileName = @"ReceivedTofile" + id + "-TCPCLIENT-" + time;
                    Dictionary<int, string> insertParamDic = new Dictionary<int, string> { { 0, fileName }, { 1, "" }, { 2, dt.Rows[i].ItemArray[0].ToString() } };
                    ThreadPool.QueueUserWorkItem(o => RecMsg(downloadInfoDic, ctsToken.Token));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(insertFile), insertParamDic);
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----DownloadData方法");
            }
        }

        private void RecMsg(object obj, CancellationToken token)
        {
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
            string fileName = @"ReceivedTofile" + dic[0] + "-TCPCLIENT-" + time + ".DAT";
            Socket socket = (Socket)dic[1];
            Socket s = socket.Accept();
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default, true))
                    {
                        while (!token.IsCancellationRequested) //持续监听服务端发来的消息
                        {
                            try
                            {
                                byte[] arrRecMsg = new byte[1024 * 2];
                                int length = s.Receive(arrRecMsg);
                                byte[] tem = new byte[length];
                                Array.Copy(arrRecMsg, 0, tem, 0, length);
                                bw.Write(tem);
                                bw.Flush();
                            }
                            catch
                            {
                                bw.Flush();
                                if (s.Connected)
                                    s.Shutdown(SocketShutdown.Both);
                                s.Close();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----RecMsg方法");
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
