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
    public partial class UrlAddForm : DevExpress.XtraEditors.XtraForm
    {
        private int projectid;
        private string projectName;
        public UrlAddForm()
        {
            InitializeComponent();
        }
        public int Projectid { get => projectid; set => projectid = value; }
        public string ProjectName { get => projectName; set => projectName = value; }

        private void UrlAddForm_Load(object sender, EventArgs e)
        {
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
            string sql = "insert into url (ip,projectid) values (?,?)";
            MySqlParameter param_ip = new MySqlParameter(@"ip", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_projectid = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Projectid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_ip, param_projectid);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "新增成功");
            else
                alertControl1.Show(this, "提示：", "新增失败");
            this.Close();
        }
    }
}