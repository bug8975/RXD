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
    public partial class PlatformAddForm : DevExpress.XtraEditors.XtraForm
    {
        public PlatformAddForm()
        {
            InitializeComponent();
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string platformName = textEdit1.Text.Trim();
            if (platformName == null || platformName.Length == 0)
            {
                alertControl1.Show(this, "提示：", "工程名称不能为空");
                return;
            }
            string sql = "insert into platform (name) values (?)";
            MySqlParameter param_platformName = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = platformName };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_platformName);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "新增成功");
            else
                alertControl1.Show(this, "提示：", "新增失败");
            this.Close();
        }
    }
}