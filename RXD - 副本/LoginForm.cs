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
using System.Xml;
using System.Configuration;

namespace RXD
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
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
            sb.Append("; database=rxd");
            sb.Append(";persist security info=true;CharSet=utf8;");
            UpdateConfig("RXD.Properties.Settings.rxdConnectionString", sb.ToString());

            StringBuilder sb_con = new StringBuilder(20);
            sb_con.Append("server=localhost;port=3306;user id=");
            sb_con.Append(txtUser.Text);
            sb_con.Append("; password=");
            sb_con.Append(txtPassword.Text);
            sb_con.Append("; database=rxd");
            sb_con.Append(";CharSet=utf8;");
            UpdateConfig("connstr", sb_con.ToString());

            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }

        //关闭
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
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