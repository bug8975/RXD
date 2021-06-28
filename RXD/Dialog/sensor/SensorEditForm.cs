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
using RXD.common;
using DevExpress.XtraEditors.Controls;

namespace RXD
{
    public partial class SensorEditForm : DevExpress.XtraEditors.XtraForm
    {
        private int monitorid;
        private string monitorName;
        private int sensorid;
        private string sensorName;
        private int states;
        private int isBasic;
        private int port;
        public SensorEditForm()
        {
            InitializeComponent();
        }

        public int Monitorid { get => monitorid; set => monitorid = value; }
        public string MonitorName { get => monitorName; set => monitorName = value; }
        public int Sensorid { get => sensorid; set => sensorid = value; }
        public string SensorName { get => sensorName; set => sensorName = value; }
        public int States { get => states; set => states = value; }
        public int IsBasic { get => isBasic; set => isBasic = value; }
        public int Port { get => port; set => port = value; }

        private void SensorEditForm_Load(object sender, EventArgs e)
        {
            if (MonitorName == null)
                return;
            textEdit1.Text = MonitorName;

            comboBoxEdit1.Properties.Items.Clear();
            List<string> states = new List<string>() { "关闭", "开启" };
            for (int i = 1; i >= 0; i--)
            {
                ComboxData comboxData = new ComboxData() { Text = states[i], Value = i.ToString() };
                comboBoxEdit1.Properties.Items.Add(comboxData);
            }
            ComboBoxItemCollection ic1 = comboBoxEdit1.Properties.Items;
            for (int i = 0; i < ic1.Count; i++)
            {
                ComboxData data = ic1[i] as ComboxData;
                if (States == Convert.ToInt32(data.Value))
                {
                    comboBoxEdit1.SelectedItem = ic1[i];
                }
            }

            comboBoxEdit2.Properties.Items.Clear();
            List<string> isBasics = new List<string>() { "移动站", "基站" };
            for (int i = 0; i < 2; i++)
            {
                ComboxData comboxData = new ComboxData() { Text = isBasics[i], Value = i.ToString() };
                comboBoxEdit2.Properties.Items.Add(comboxData);
            }
            ComboBoxItemCollection ic2 = comboBoxEdit2.Properties.Items;
            for (int i = 0; i < ic2.Count; i++)
            {
                ComboxData data = ic2[i] as ComboxData;
                if (IsBasic == Convert.ToInt32(data.Value))
                {
                    comboBoxEdit2.SelectedItem = ic2[i];
                }
            }

            textEdit2.Text = SensorName;
            textEdit3.Text = Port.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MySqlParameter param_port = new MySqlParameter(@"port", MySqlDbType.Int32) { Value = textEdit3.Text };
            string sql_isPortOK = "SELECT COUNT(1) FROM sensor WHERE port = ?";
            DataTable dt = common.MySqlHelper.GetDataSet(sql_isPortOK, param_port).Tables[0];
            int result = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
            if (result != 0)
            {
                if(Port != Convert.ToInt32(textEdit3.Text))
                {
                    XtraMessageBox.Show("端口已被使用");
                    this.Close();
                    return;
                }
            }
            string sql = "update sensor set name = ?,port = ?,states = ?,isbasic = ?,monitorline_id = ? where id = ?";
            ComboxData statesData = comboBoxEdit1.SelectedItem as ComboxData;
            ComboxData isBasicData = comboBoxEdit2.SelectedItem as ComboxData;
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_states = new MySqlParameter(@"states", MySqlDbType.Int32) { Value = Convert.ToInt32(statesData.Value) };
            MySqlParameter param_isbasic = new MySqlParameter(@"isbasic", MySqlDbType.Int32) { Value = Convert.ToInt32(isBasicData.Value) };
            MySqlParameter monitorid = new MySqlParameter(@"monitorline_id", MySqlDbType.Int32) { Value = Monitorid };
            MySqlParameter sensorid = new MySqlParameter(@"id", MySqlDbType.Int32) { Value = Sensorid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_port, param_states, param_isbasic, monitorid, sensorid);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "修改成功");
            else
                alertControl1.Show(this, "提示：", "修改失败");
            this.Close();
        }
    }
}