using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RXD
{
    public partial class Form1 : Form
    {
        [DllImport("rnx2rtkp_sky.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_sky(int station, int year, int mon, int day, int hour);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "select s.id, f.createtime, f.name, s.monitorline_id from file f left join sensor s on f.sensor_id = s.id where f.states = 0 and s.type = 0";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                DateTime time = (DateTime)dt.Rows[i].ItemArray[1];
                //TODO: 生成卫星位置文件
                int result_sky = rtklib_sky(id, time.Year, time.Month, time.Day, time.Hour);
                Console.WriteLine("ddd" + result_sky);
            }
        }
    }
}
