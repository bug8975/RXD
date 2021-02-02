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
    public partial class URLForm : DevExpress.XtraEditors.XtraForm
    {
        public URLForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新增工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "insert into project (name) values (?)";
            MySqlParameter name = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = textEdit1.Text.Trim() };
            int result = common.MySqlHelper.ExecuteNonQuery(sql, name);
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