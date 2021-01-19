using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNet.common
{
    class HeartBeat
    {
        private long currentTicks;
        private DateTime lastTimes;
        private bool isSendOK;

        public long CurrentTicks { get => currentTicks; set => currentTicks = value; }
        public DateTime LastTimes { get => lastTimes; set => lastTimes = value; }
        public bool IsSendOK { get => isSendOK; set => isSendOK = value; }


        public HeartBeat()
        {
            CurrentTicks = DateTime.Now.Ticks;
        }

        private long[] getLastTimes2Ticks()
        {
            string sql = "select * from monitor where projectid = 1";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            long[] ticks = new long[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                long temp = 0;
                long.TryParse(dt.Rows[i].ItemArray[2].ToString(), out temp);
                ticks[i] = Math.Abs(CurrentTicks - temp);
            }
            return ticks;
        }

        public void CheckHeartBeat()
        {
            long[] ticks = getLastTimes2Ticks();

            
        }
    }
}
