using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNet.pojo
{
    class SendMsg
    {
        public string monitorLineName;
        public string sensorName;
        public double deviceData;
        public double sinkingData;
        public double sinkingAccumulation;
        public string sensorType;
        public string collectingTime;

        public SendMsg(string monitorLineName, string sensorName, double deviceData, double sinkingData, double sinkingAccumulation, string sensorType, string collectingTime)
        {
            this.monitorLineName = monitorLineName;
            this.sensorName = sensorName;
            this.deviceData = deviceData;
            this.sinkingData = sinkingData;
            this.sinkingAccumulation = sinkingAccumulation;
            this.sensorType = sensorType;
            this.collectingTime = collectingTime;
        }
    }
}
