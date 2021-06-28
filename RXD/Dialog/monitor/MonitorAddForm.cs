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
    public partial class MonitorAddForm : DevExpress.XtraEditors.XtraForm
    {
        private int projectid;
        private string projectName;
        public MonitorAddForm()
        {
            InitializeComponent();
        }

        public int Projectid { get => projectid; set => projectid = value; }
        public string ProjectName { get => projectName; set => projectName = value; }

        private void MonitorForm_Load(object sender, EventArgs e)
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
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedItem = comboBoxEdit1.Properties.Items[0];
            if (ProjectName == null)
                return;
            textEdit1.Text = ProjectName;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "insert into monitorline (name,frequency,project_id,sensortype_id) values (?,?,?,?)";
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_frequency = new MySqlParameter(@"frequency", MySqlDbType.Int32) { Value = Convert.ToInt32(textEdit3.Text) }; 
            MySqlParameter param_projectid = new MySqlParameter(@"project_id", MySqlDbType.Int32) { Value = Projectid };
            ComboxData data = comboBoxEdit1.SelectedItem as ComboxData;
            MySqlParameter param_sensortypeid = new MySqlParameter(@"sensortype_id", MySqlDbType.Int32) { Value = Convert.ToInt32(data.Value) };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_frequency, param_projectid, param_sensortypeid);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "新增成功");
            else
                alertControl1.Show(this, "提示：", "新增失败");
            this.Close();
        }
    }
}