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
    public partial class PlatformEditForm : DevExpress.XtraEditors.XtraForm
    {
        private int platformid;
        private string platformName;
        public PlatformEditForm()
        {
            InitializeComponent();
        }

        public int Platformid { get => platformid; set => platformid = value; }
        public string PlatformName { get => platformName; set => platformName = value; }

        private void MonitorEditForm_Load(object sender, EventArgs e)
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
            string sql = "update platform set name = ? where id = ?";
            MySqlParameter param_name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit1.Text };
            MySqlParameter param_id = new MySqlParameter(@"projectid", MySqlDbType.Int32) { Value = Platformid };
            int cols = common.MySqlHelper.ExecuteNonQuery(sql, param_name, param_id);
            if (cols == 1)
                alertControl1.Show(this, "提示：", "修改成功");
            else
                alertControl1.Show(this, "提示：", "修改失败");
            this.Close();
        }

    }
}