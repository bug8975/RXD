using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RXD.common
{
    class DataProcessing
    {
        [DllImport("convbin.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_convin(int a, int b, int c, int d);

        [DllImport("rnx2rtkp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_rnx2rtkp(int a, int b, int c, int d, int e, int f);

        public void dat2navFile(object obj)
        {
            if (obj == null)
                return;
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            DateTime dt = (DateTime)dic[0];
            int sensor_id = Convert.ToInt32(dic[1]);
            int result = rtklib_convin(dt.Year, dt.Month, dt.Day, sensor_id);
            Console.WriteLine(result);
        }

        public void nav2posFile(object obj)
        {
            if (obj == null)
                return;
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            DateTime dt = (DateTime)dic[0];
            List<int> data = (List<int>)dic[1];
            int result = rtklib_rnx2rtkp(dt.Year, dt.Month, dt.Day, data[0], data[1], 3600);
        }
    }
}
