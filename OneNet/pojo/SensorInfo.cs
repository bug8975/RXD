using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNet.pojo
{
    public class SensorInfo
    {
        private int id;
        private string name;
        private string type;
        private double longitude;
        private double latitude;
        private int noise;
        private DateTime times;
        private int sensorid;

        public SensorInfo()
        {
        }

        public SensorInfo(string name,string type, double longitude, double latitude, int noise, DateTime times, int sensorid)
        {
            this.name = name;
            this.type = type;
            this.longitude = longitude;
            this.latitude = latitude;
            this.Noise = noise;
            this.times = times;
            this.sensorid = sensorid;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public DateTime Times { get => times; set => times = value; }
        public int Sensorid { get => sensorid; set => sensorid = value; }
        public string Type { get => type; set => type = value; }
        public int Noise { get => noise; set => noise = value; }
    }
}
