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
using OneNet.common;
using MySql.Data.MySqlClient;

namespace OneNet
{
    public partial class SensorAddForm : DevExpress.XtraEditors.XtraForm
    {
        private int monitorid;
        private string monitorName;
        public SensorAddForm()
        {
            InitializeComponent();
        }
        public int Monitorid { get => monitorid; set => monitorid = value; }
        public string MonitorName { get => monitorName; set => monitorName = value; }

        private void SensorForm_Load(object sender, EventArgs e)
        {
            if (MonitorName == null)
                return;
            textEdit1.Text = MonitorName;

            comboBoxEdit1.Properties.Items.Clear();
            string sql = "select * from sensor_type";
            DataTable dt = common.MySqlHelper.GetDataSet(sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i].ItemArray[0].ToString();
                string sensor_name = dt.Rows[i].ItemArray[1].ToString();
                ComboxData data = new ComboxData() { Text = sensor_name, Value = id };
                comboBoxEdit1.Properties.Items.Add(data);
            }
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedItem = comboBoxEdit1.Properties.Items[0];
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "insert into sensor (name,monitorid,sensortypeid) values (?,?,?)";
            ComboxData data = comboBoxEdit1.SelectedItem as ComboxData;
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_projectid = new MySqlParameter(@"monitorid", MySqlDbType.Int32) { Value = Monitorid };
            MySqlParameter param_sensortypeid = new MySqlParameter(@"sensortypeid", MySqlDbType.Int32) { Value = data.Value };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_projectid, param_sensortypeid);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "新增成功");
            else
                alertControl1.Show(this, "提示：", "新增失败");
            this.Close();
        }
    }
}