using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NLog;
using OneNet.pojo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OneNet.common
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

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 11108);
                socket.Bind(endPoint);
                socket.Listen(20);
                ThreadPool.QueueUserWorkItem(o => RecMsg(socket, ctsToken.Token));
                //ThreadPool.QueueUserWorkItem(new WaitCallback(insertFile), insertParamDic);
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----DownloadData方法");
            }
        }

        private void RecMsg(object obj, CancellationToken token)
        {
            Socket socket = (Socket)obj;
            Socket s = socket.Accept();
            byte[] arrRecMsg = new byte[1024];
            while (!token.IsCancellationRequested) //持续监听服务端发来的消息
            {
                try
                {
                    
                    int length = s.Receive(arrRecMsg);
                    if (length == 0)
                        break;
                    byte[] tem = new byte[length];
                    Array.Copy(arrRecMsg, 0, tem, 0, length);
                    string recString = Encoding.ASCII.GetString(tem);
                    Console.WriteLine(recString);
                    _logger.Info(recString);
                    string[] recStrList = Regex.Split(recString, ",", RegexOptions.None);
                    //发送Post请求
                    SendPost(recStrList);
                }
                catch (Exception ex)
                {
                    _logger.Trace(ex.Message + "----RecMsg方法");
                    break;
                }
            }
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
