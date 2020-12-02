using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RXD.pojo
{
    public class Sensor
    {
        private int id;
        private double x;
        private double y;
        private double z;
        private double ax;
        private double ay;
        private double az;
        private DateTime time;

        public Sensor(double x, double y, double z, double ax, double ay, double az, DateTime time)
        {
            X = x;
            Y = y;
            Z = z;
            Ax = ax;
            Ay = ay;
            Az = az;
            Time = time;
        }

        public int Id { get => id; set => id = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public double Ax { get => ax; set => ax = value; }
        public double Ay { get => ay; set => ay = value; }
        public double Az { get => az; set => az = value; }
        public DateTime Time { get => time; set => time = value; }
    }
}
