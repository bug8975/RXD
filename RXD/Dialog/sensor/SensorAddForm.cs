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
using RXD.common;
using MySql.Data.MySqlClient;

namespace RXD
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
            textEdit2.Text = MonitorName;

            comboBoxEdit1.Properties.Items.Clear();
            List<string> states = new List<string>() { "关闭", "开启" };
            for (int i = 1; i >= 0; i--)
            {
                ComboxData comboxData = new ComboxData() { Text = states[i], Value = i.ToString() };
                comboBoxEdit1.Properties.Items.Add(comboxData);
            }
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedItem = comboBoxEdit1.Properties.Items[0];

            comboBoxEdit2.Properties.Items.Clear();
            List<string> isBasics = new List<string>() { "移动站", "基站" };
            for (int i = 0; i < 2; i++)
            {
                ComboxData comboxData = new ComboxData() { Text = isBasics[i], Value = i.ToString() };
                comboBoxEdit2.Properties.Items.Add(comboxData);
            }
            if (comboBoxEdit2.Properties.Items.Count > 0)
                comboBoxEdit2.SelectedItem = comboBoxEdit2.Properties.Items[0];
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
                XtraMessageBox.Show("端口已被使用");
                return;
            }
            string sql = "insert into sensor (name,port,states,isbasic,monitorline_id) values (?,?,?,?,?)";
            ComboxData statesData = comboBoxEdit1.SelectedItem as ComboxData;
            ComboxData isBasicData = comboBoxEdit2.SelectedItem as ComboxData;
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_states = new MySqlParameter(@"states", MySqlDbType.Int32) { Value = Convert.ToInt32(statesData.Value) };
            MySqlParameter param_isbasic = new MySqlParameter(@"isbasic", MySqlDbType.Int32) { Value = Convert.ToInt32(isBasicData.Value) };
            MySqlParameter monitorid = new MySqlParameter(@"monitorline_id", MySqlDbType.Int32) { Value = Monitorid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_port, param_states, param_isbasic, monitorid);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "新增成功");
            else
                alertControl1.Show(this, "提示：", "新增失败");
            this.Close();
        }
    }
}