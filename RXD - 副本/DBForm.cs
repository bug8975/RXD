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
using System.Configuration;
using RXD.common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Xml;

namespace RXD
{
    public partial class DBForm : DevExpress.XtraEditors.XtraForm
    {
        public DBForm()
        {
            InitializeComponent();
        }

        private void DBForm_Load(object sender, EventArgs e)
        {
            String sql = "SELECT SCHEMA_NAME AS `Database` FROM INFORMATION_SCHEMA.SCHEMATA";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxEdit1.Properties.Items.AddRange(dt.Rows[i].ItemArray);
                if (dt.Rows[i].ItemArray.Equals("rxd"))
                {
                    comboBoxEdit1.SelectedItem = dt.Rows[i].ItemArray;
                }
            }
        }

        //保存
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text.Trim();
            string passWord = txtUser.Text.Trim();
            if (userName.Equals("") || passWord.Equals("")) { return; }
            StringBuilder sb = new StringBuilder(20);
            sb.Append("XpoProvider=MySql;server=localhost;port=3306;user id=");
            sb.Append(txtUser.Text);
            sb.Append("; password=");
            sb.Append(txtPassword.Text);
            sb.Append("; database=");
            sb.Append(comboBoxEdit1.SelectedText);
            sb.Append(";persist security info=true;CharSet=utf8;");
            UpdateConfig("RXD.Properties.Settings.rxdConnectionString", sb.ToString());

            StringBuilder sb_con = new StringBuilder(20);
            sb_con.Append("server=localhost;port=3306;user id=");
            sb_con.Append(txtUser.Text);
            sb_con.Append("; password=");
            sb_con.Append(txtPassword.Text);
            sb_con.Append("; database=");
            sb_con.Append(comboBoxEdit1.SelectedText);
            sb_con.Append("CharSet=utf8;");
            UpdateConfig("connstr", sb_con.ToString());

            this.Close();
        }

        //关闭
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //测试连接
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string ConnectionString = "server=localhost;database=" + comboBoxEdit1.SelectedText + ";uid=" + txtUser.Text + ";pwd=" + txtPassword.Text;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    this.alertControl1.Show(this, "提示", "连接成功!");
                    simpleButton1.Enabled = true;
                }
                else
                {
                    this.alertControl1.Show(this, "提示", "连接失败!");
                    simpleButton1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.alertControl1.Show(this, "提示", "连接失败!");
                simpleButton1.Enabled = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //更新数据库配置文件
        private void UpdateConfig(string AppKey, string KeyValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.ExecutablePath + ".config");//获取当前配置文件
            XmlNode node = doc.SelectSingleNode(@"//add[@name='" + AppKey + "']");
            XmlElement ele = (XmlElement)node;
            ele.SetAttribute("connectionString", KeyValue);
            doc.Save(Application.ExecutablePath + ".config");
            ConfigurationManager.RefreshSection("connectionStrings");
            //上面句很重要,强制程序重新获取配置文件中appSettings节点中所有的值,否则更改后要到程序关闭后才更新,因为程序启动后默认不再重新获取.
            this.alertControl1.Show(this, "提示", "保存成功!");
        }

    }
}