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
using NLog;
using MySql.Data.MySqlClient;
using OneNet.common;

namespace OneNet
{
    public partial class SNEditForm : DevExpress.XtraEditors.XtraForm
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        private int projectid;
        private string sn;
        private int bindinfoid;

        public int Projectid { get => projectid; set => projectid = value; }
        public string Sn { get => sn; set => sn = value; }
        public int Bindinfoid { get => bindinfoid; set => bindinfoid = value; }

        public SNEditForm()
        {
            InitializeComponent();
        }

        private void SNEditForm_Load(object sender, EventArgs e)
        {
            if (Sn == null)
                return;
            textEdit1.Text = Sn;

            if (Projectid == 0)
                return;

            int selectIndex = 0;
            string sql = "select * from monitor where projectid = ?";
            MySqlParameter mp = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Projectid };
            try
            {
                DataTable dt = common.MySqlHelper.GetDataSet(sql, mp).Tables[0];
                comboBoxEdit1.Properties.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id = dt.Rows[i].ItemArray[0].ToString();
                    string name = dt.Rows[i].ItemArray[1].ToString();
                    ComboxData comboxData = new ComboxData() { Text = name, Value = id };
                    comboBoxEdit1.Properties.Items.Add(comboxData);
                }
                if (comboBoxEdit1.Properties.Items.Count > 0)
                    comboBoxEdit1.SelectedItem = comboBoxEdit1.Properties.Items[0];

                comboBoxEdit3.Properties.Items.Clear();
                List<string> texts = new List<string>() { "角度", "加速度", "位移" };
                for (int i = 0; i < 3; i++)
                {
                    ComboxData comboxData = new ComboxData() { Text = texts[i], Value = i.ToString() };
                    if (dt.Rows[i].ItemArray[0].ToString().Equals(Bindinfoid))
                        selectIndex = i;
                    comboBoxEdit3.Properties.Items.Add(comboxData);
                }
                if (comboBoxEdit3.Properties.Items.Count > 0)
                    comboBoxEdit3.SelectedItem = comboBoxEdit3.Properties.Items[selectIndex];
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----refresh_MonitorData方法");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int sn = Convert.ToInt32(textEdit1.Text);
            ComboxData comboxData_sensor = comboBoxEdit2.SelectedItem as ComboxData;
            ComboxData comboxData_type = comboBoxEdit3.SelectedItem as ComboxData;
            int sensorid = Convert.ToInt32(comboxData_sensor.Value);
            int type = Convert.ToInt32(comboxData_type.Value);
            string sql = "update bindinfo set sn = ?,sensorid = ?,type = ? where id = ?";
            MySqlParameter param_sn = new MySqlParameter(@"sn", MySqlDbType.Int32) { Value = sn };
            MySqlParameter param_sensorid = new MySqlParameter(@"sensorid", MySqlDbType.Int32) { Value = sensorid };
            MySqlParameter param_type = new MySqlParameter(@"type", MySqlDbType.Int32) { Value = type };
            MySqlParameter param_bindinfoid = new MySqlParameter(@"id", MySqlDbType.Int32) { Value = Bindinfoid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_sn, param_sensorid, param_type, param_bindinfoid);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "保存成功");
            else
                alertControl1.Show(this, "提示：", "保存失败");
            this.Close();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = 0;
            ComboxData comboxData = comboBoxEdit1.SelectedItem as ComboxData;
            int monitorid = Convert.ToInt32(comboxData.Value);
            MySqlParameter param_monitorid = new MySqlParameter(@"monitorid", MySqlDbType.Int32) { Value = monitorid };
            string sql = "select * from sensor where monitorid = ?";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, param_monitorid).Tables[0];
            if (dt.Rows.Count == 0)
                return;
            comboBoxEdit2.Properties.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboxData data = new ComboxData() { Text = dt.Rows[i].ItemArray[1].ToString(), Value = dt.Rows[i].ItemArray[0].ToString() };
                if (dt.Rows[i].ItemArray[0].ToString().Equals(Bindinfoid))
                    selectIndex = i;
                comboBoxEdit2.Properties.Items.Add(data);
            }
            comboBoxEdit2.SelectedItem = comboBoxEdit2.Properties.Items[selectIndex];
        }
    }
}