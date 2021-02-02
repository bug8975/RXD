using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RXD.pojo
{
    public class DataView
    {
        private int id;
        private double x;
        private double y;
        private double z;
        private DateTime time;
        private int sensorid;

        public DataView()
        {
        }

        public DataView(double x, double y, double z, DateTime time, int sensorid)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.time = time;
            this.sensorid = sensorid;
        }

        public int Id { get => id; set => id = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public DateTime Time { get => time; set => time = value; }
        public int Sensorid { get => sensorid; set => sensorid = value; }
    }
}
