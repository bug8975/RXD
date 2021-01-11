using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;

namespace OneNet
{
    public partial class SensorEditForm : DevExpress.XtraEditors.XtraForm
    {
        private int monitorid;
        private string monitorName;
        private int sensorid;
        private string sensorName;
        public SensorEditForm()
        {
            InitializeComponent();
        }

        public int Monitorid { get => monitorid; set => monitorid = value; }
        public string MonitorName { get => monitorName; set => monitorName = value; }
        public int Sensorid { get => sensorid; set => sensorid = value; }
        public string SensorName { get => sensorName; set => sensorName = value; }

        private void SensorEditForm_Load(object sender, EventArgs e)
        {
            if (MonitorName == null)
                return;
            textEdit1.Text = MonitorName;
            if (SensorName == null)
                return;
            textEdit2.Text = SensorName;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "update sensor set name = ? where id = ?";
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_id = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Sensorid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_id);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "修改成功");
            else
                alertControl1.Show(this, "提示：", "修改失败");
            this.Close();
        }
    }
}