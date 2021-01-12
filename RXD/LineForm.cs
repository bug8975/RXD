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

namespace RXD
{
    public partial class LineForm : DevExpress.XtraEditors.XtraForm
    {
        public LineForm()
        {
            InitializeComponent();
        }

        private void LineForm_Load(object sender, EventArgs e)
        {
            comboBoxEdit1.Properties.Items.Clear();
            string sql = "select * from project";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboxData data = new ComboxData
                {
                    Text = dt.Rows[i].ItemArray[1].ToString(),
                    Value = dt.Rows[i].ItemArray[0].ToString()
                };
                comboBoxEdit1.Properties.Items.Add(data);
            }
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedItem = comboBoxEdit1.Properties.Items[0];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MySqlParameter project_id = new MySqlParameter("@project_id", MySqlDbType.Int32) { Value = (comboBoxEdit1.SelectedItem as ComboxData).Value };
            MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar) { Value = textEdit1.Text };
            string sql = "insert into monitorline (name,project_id) values (?,?)";
            int result = common.MySqlHelper.ExecuteNonQuery(sql, name, project_id);
            if (result == 1)
                alertControl1.Show(this, "提示", "新增成功");
            else
                alertControl1.Show(this, "提示", "新增失败");
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}