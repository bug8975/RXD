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
    public partial class ProjectEditForm : DevExpress.XtraEditors.XtraForm
    {
        private int platformid;
        private string platformName;
        private int projectid;
        private string projectName;
        private string url;
        public ProjectEditForm()
        {
            InitializeComponent();
        }

        public int Platformid { get => platformid; set => platformid = value; }
        public string PlatformName { get => platformName; set => platformName = value; }
        public int Projectid { get => projectid; set => projectid = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string Url { get => url; set => url = value; }

        private void MonitorEditForm_Load(object sender, EventArgs e)
        {
            if (PlatformName == null)
                return;
            textEdit1.Text = PlatformName;
            if (ProjectName == null)
                return;
            textEdit2.Text = ProjectName;
            textEdit3.Text = Url;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "update project set name = ?,url = ? where id = ?";
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_url = new MySqlParameter(@"url", MySqlDbType.VarChar) { Value = textEdit3.Text };
            MySqlParameter param_id = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Projectid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_url, param_id);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "修改成功");
            else
                alertControl1.Show(this, "提示：", "修改失败");
            this.Close();
        }

    }
}