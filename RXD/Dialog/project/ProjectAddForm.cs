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
    public partial class ProjectAddForm : DevExpress.XtraEditors.XtraForm
    {
        private int platformid;
        private string platformName;
        public ProjectAddForm()
        {
            InitializeComponent();
        }

        public int Platformid { get => platformid; set => platformid = value; }
        public string PlatformName { get => platformName; set => platformName = value; }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
            if (PlatformName == null)
                return;
            textEdit1.Text = PlatformName;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "insert into project (name,url,platform_id) values (?,?,?)";
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit2.Text };
            MySqlParameter param_url = new MySqlParameter(@"url", MySqlDbType.VarChar) { Value = textEdit3.Text };
            MySqlParameter param_platformid = new MySqlParameter(@"platform_id", MySqlDbType.Int32) { Value = Platformid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_url, param_platformid );
            if (cols == 1)
                alertControl1.Show(this, "提示：", "新增成功");
            else
                alertControl1.Show(this, "提示：", "新增失败");
            this.Close();
        }
    }
}