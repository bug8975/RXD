using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneNet.common
{
    class HeartBeat
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private DateTime lastTimes;
        private bool isSendOK;
        private int projectid;

        public DateTime LastTimes { get => lastTimes; set => lastTimes = value; }
        public bool IsSendOK { get => isSendOK; set => isSendOK = value; }
        public int Projectid { get => projectid; set => projectid = value; }

        public HeartBeat()
        {
        }

        public void CheckHeartBeat()
        {
            try
            {
                string sql = "select * from monitor where projectid = ?";
                MySqlParameter param_projectid = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Projectid };
                DataTable dt = common.MySqlHelper.GetDataSet(sql, param_projectid).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime collecttime = (DateTime)dt.Rows[i].ItemArray[2];
                    if (collecttime == null)
                        continue;
                    string monitorLineName = dt.Rows[i].ItemArray[1].ToString();
                    double deltaT = Math.Abs((collecttime - DateTime.Now).TotalMilliseconds);
                    double timeSpace = 24 * 60 * 60 * 1000;
                    if (deltaT >= timeSpace)
                        continue;

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("monitorLineName", monitorLineName);
                    //发送心跳请求
                    SendPost(dic);
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "--心跳功能异常");
            }
        }

        private void SendPost(Dictionary<string, string> dic)
        {
            try
            {
                //TODO:通过数据库获取推送URL
                string sql_url = "select ip from url where projectid = ?";
                MySqlParameter param_projectid = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = projectid };
                DataTable dt_url = common.MySqlHelper.GetDataSet(sql_url, param_projectid).Tables[0];
                if (dt_url.Rows.Count == 0)
                    return;
                string url = @dt_url.Rows[0].ItemArray[0].ToString() + "/app/heartbeat.htm";
                string strJson = JsonConvert.SerializeObject(dic);
                Console.WriteLine(strJson);
                Console.WriteLine(url);
                //_logger.Info(strJson + ":  " + url);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.ContentType = "text/plain";
                request.Method = "POST";
                request.Timeout = 1000;
                byte[] data = Encoding.UTF8.GetBytes(strJson);
                request.ContentLength = data.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "--SendPost");
            }
        }
    }
}
