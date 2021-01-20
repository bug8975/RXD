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
using OneNet.pojo;
using System.IO;
using System.Collections.Generic;

namespace OneNet.common
{
    class Server
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private TcpListener tcpListener;
        private Thread listenThread;

        public Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 11108);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
            Console.WriteLine("Server started at {0} :{1} @ {2}", IPAddress.Any, 1031, DateTime.Now.ToString());
        }


        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }


        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            Console.WriteLine("Client @[{0}] connected @{1}", tcpClient.Client.LocalEndPoint, DateTime.Now.ToString());

            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead = 0;
            //bool isRight=false;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    Console.WriteLine("Error:receive msg error");
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    Console.WriteLine("Client @[{0}] disconnect @{1}", tcpClient.Client.LocalEndPoint, DateTime.Now.ToString());
                    break;
                }

                string recString = Encoding.ASCII.GetString(message, 0, bytesRead);
                _logger.Info(recString);
                string[] recStrList = Regex.Split(recString, ",", RegexOptions.None);
                //发送Post请求
                SendPost(recStrList);
            }

            tcpClient.Close();
        }

        private void SendPost(string[] recList)
        {
            if (recList == null || recList.Length == 0)
                return;
            string sn = recList[2];         //SN号
            string time = recList[3];       //时间
            string dev_state = recList[4];  //震动状态：0-无振动，1-振动，2-崩塌
            string dev_bat = recList[11];   //电压
            double.TryParse(dev_bat, out double dev_batDouble);
            dev_batDouble /= 3650;
            string sql = "select m.name as monitorname,s.name sensorname,bi.type,bi.L,st.code,m.projectid from bindinfo bi LEFT JOIN sensor s on bi.sensorid = s.id LEFT JOIN monitor m on s.monitorid = m.id LEFT JOIN sensor_type st ON s.sensortypeid = st.id where sn = ?";
            MySqlParameter mp = new MySqlParameter(@"sn", MySqlDbType.VarChar) { Value = sn };
            DataTable dt = common.MySqlHelper.GetDataSet(sql, mp).Tables[0];
            int projectid = 1;
            List<SendMsg> list = new List<SendMsg>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //TODO:通过数据库获取推送URL
            string sql_url = "select ip from url where projectid = ?";
            MySqlParameter param_projectid = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = projectid };
            DataTable dt_url = common.MySqlHelper.GetDataSet(sql_url, param_projectid).Tables[0];
            if (dt_url.Rows.Count == 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string monitorline = dt.Rows[i].ItemArray[0].ToString();
                string sensorname = dt.Rows[i].ItemArray[1].ToString();
                int type = Convert.ToInt32(dt.Rows[i].ItemArray[2]);
                int L = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
                string code = dt.Rows[i].ItemArray[4].ToString();
                projectid = Convert.ToInt32(dt.Rows[i].ItemArray[5]);
                DateTime DateStart = new DateTime(1970, 1, 1, 8, 0, 0);
                DateTime Now = Convert.ToDateTime(time);
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

                //TODO:更新监测线推送数据的时间
                ThreadPool.QueueUserWorkItem(p =>
                {
                    string sql_updataMonitor = "updata monitor set collecttime = ? where name = ?";
                    MySqlParameter param_collecttime = new MySqlParameter(@"collecttime", MySqlDbType.DateTime) { Value = Now };
                    MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = monitorline };
                    int cols = common.MySqlHelper.ExecuteNonQuery(sql_updataMonitor, param_collecttime, param_name);
                    if (cols != 1)
                        _logger.Trace("更新monitor表中collecttime字段失败 " + Now);
                });

                //TODO:推送电池电量，震动状态信息
                ThreadPool.QueueUserWorkItem(p =>
                {
                    dic.Add("monitorLineName", monitorline);
                    dic.Add("ICCID", "");
                    dic.Add("GPS_LNG", dev_state);
                    dic.Add("GPS_LAT", 0);
                    dic.Add("DEV_BAT", dev_batDouble);
                    dic.Add("CSQ", 0);
                    string url_info = @dt_url.Rows[0].ItemArray[0].ToString() + "/app/monitorinforeport.htm";
                    string strJson_info = JsonConvert.SerializeObject(dic);
                    PostTools(strJson_info, url_info);
                });
            }

            //TODO:推送监测数据
            ThreadPool.QueueUserWorkItem(p =>
            {
                string url_data = @dt_url.Rows[0].ItemArray[0].ToString() + "/app/monitorData.htm";
                string strJson_data = JsonConvert.SerializeObject(list);
                PostTools(strJson_data, url_data);
            });
        }

        private void PostTools(string strJson,string url)
        {
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

    }


}