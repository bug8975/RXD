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
using DevExpress.XtraEditors.Controls;

namespace RXD
{
    public partial class MonitorEditForm : DevExpress.XtraEditors.XtraForm
    {
        private int projectid;
        private int sensortypeid;
        private string projectName;
        private int monitorid;
        private string monitorName;
        private int frequency;
        public MonitorEditForm()
        {
            InitializeComponent();
        }

        public int Projectid { get => projectid; set => projectid = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string MonitorName { get => monitorName; set => monitorName = value; }
        public int Monitorid { get => monitorid; set => monitorid = value; }
        public int Frequency { get => frequency; set => frequency = value; }
        public int Sensortypeid { get => sensortypeid; set => sensortypeid = value; }

        private void MonitorEditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM sensortype";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            comboBoxEdit1.Properties.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i].ItemArray[0].ToString();
                string name = dt.Rows[i].ItemArray[2].ToString();
                ComboxData sensorTypeData = new ComboxData() { Text = name, Value = id };
                comboBoxEdit1.Properties.Items.Add(sensorTypeData);
            }
            ComboBoxItemCollection ic1 = comboBoxEdit1.Properties.Items;
            for (int i = 0; i < ic1.Count; i++)
            {
                ComboxData data = ic1[i] as ComboxData;
                if (Sensortypeid == Convert.ToInt32(data.Value))
                {
                    comboBoxEdit1.SelectedItem = ic1[i];
                }
            }

            if (ProjectName == null)
                return;
            textEdit1.Text = ProjectName;

            textEdit2.Text = MonitorName;
            textEdit3.Text = Frequency.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "update monitorline set name = ?,frequency = ?,sensortype_id = ? where id = ?";
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_frequency = new MySqlParameter(@"frequency", MySqlDbType.Int32) { Value = Convert.ToInt32(textEdit3.Text) };
            ComboxData data = comboBoxEdit1.SelectedItem as ComboxData;
            MySqlParameter param_sensortypeid = new MySqlParameter(@"sensortype_id", MySqlDbType.Int32) { Value = Convert.ToInt32(data.Value) };
            MySqlParameter param_id = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Monitorid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_frequency, param_sensortypeid, param_id);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "修改成功");
            else
                alertControl1.Show(this, "提示：", "修改失败");
            this.Close();
        }

    }
}