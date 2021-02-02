using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RXD
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Register skins
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Lilian");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (test_DBConnect())
            {
                Application.Run(new BasicForm());
            }
            else
            {
                LoginForm login = new LoginForm();
                login.ShowDialog();
                if(login.DialogResult == DialogResult.OK)
                {
                    login.Dispose();
                    //Application.Run(new BasicForm());
                    return;
                }
                else
                {
                    login.Dispose();
                    return;
                }
            }
            //Application.Run(new BasicForm());
            //Application.Run(new HomeForm());
            //Application.Run(new Form1());
        }

        static bool test_DBConnect()
        {
            //数据库连接字符串
            string ConnectionString = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    //MessageBox.Show("连接成功!");
                    return true;
                }
                else
                {
                    //MessageBox.Show("数据库连接失败，请填写正确的账号密码!");
                    return false;
                }
            }
            catch
            {
                //MessageBox.Show("数据库连接失败，请填写正确的账号密码!");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
