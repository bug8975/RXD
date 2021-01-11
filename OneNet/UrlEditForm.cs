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
    public partial class UrlEditForm : DevExpress.XtraEditors.XtraForm
    {
        private int projectid;
        private string projectName;
        private int urlid;
        private string ip;
        private int port;

        public int Projectid { get => projectid; set => projectid = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public int Urlid { get => urlid; set => urlid = value; }
        public string Ip { get => ip; set => ip = value; }
        public int Port { get => port; set => port = value; }

        public UrlEditForm()
        {
            InitializeComponent();
        }

        private void UrlEditForm_Load(object sender, EventArgs e)
        {
            if (ProjectName == null)
                return;
            textEdit1.Text = ProjectName;
            if (Ip == null)
                return;
            textEdit2.Text = Ip;
            if (Port == 0)
                return;
            textEdit3.Text = Port.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "update url set ip = ?,port = ? where id = ?";
            MySqlParameter param_ip = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_port = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit3.Text };
            MySqlParameter param_id = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Urlid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_ip, param_port, param_id);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "修改成功");
            else
                alertControl1.Show(this, "提示：", "修改失败");
            this.Close();
        }
    }
}