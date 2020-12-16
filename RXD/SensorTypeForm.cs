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
using System.Net.Sockets;
using NLog;

namespace RXD
{
    public partial class SensorTypeForm : DevExpress.XtraEditors.XtraForm
    {
        Logger _logger = LogManager.GetCurrentClassLogger();
        public SensorTypeForm()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“rxdDataSet.project”中。您可以根据需要移动或删除它。
            this.projectTableAdapter.Fill(this.rxdDataSet.project);
            comboBoxEdit1.Properties.Items.Clear();
            comboBoxEdit5.Properties.Items.Clear();
            comboBoxEdit8.Properties.Items.Clear();
            ComboxData data51 = new ComboxData
            {
                Text = "串口通信",
                Value = "1"
            };
            ComboxData data52 = new ComboxData
            {
                Text = "TCP通信",
                Value = "0"
            };
            comboBoxEdit5.Properties.Items.Add(data51);
            comboBoxEdit5.Properties.Items.Add(data52);
            ComboxData data81 = new ComboxData
            {
                Text = "开启",
                Value = "0"
            };
            ComboxData data82 = new ComboxData
            {
                Text = "关闭",
                Value = "1"
            };
            comboBoxEdit8.Properties.Items.Add(data81);
            comboBoxEdit8.Properties.Items.Add(data82);
            string sql = "select * from project";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboxData data = new ComboxData
                {
                    Text = dt.Rows[i].ItemArray[1].ToString(),
                    Value = dt.Rows[i].ItemArray[0].ToString()
                };
                comboBoxEdit1.Properties.Items.Add(data);
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit2.Properties.Items.Clear();
            MySqlParameter project_id = new MySqlParameter("@project_id", MySqlDbType.Int32) { Value = (comboBoxEdit1.SelectedItem as ComboxData).Value };
            string sql = "select * from monitorline where project_id = ?";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, project_id).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboxData data = new ComboxData
                {
                    Text = dt.Rows[i].ItemArray[1].ToString(),
                    Value = dt.Rows[i].ItemArray[0].ToString()
                };
                comboBoxEdit2.Properties.Items.Add(data);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MySqlParameter monitorline_id = new MySqlParameter("@monitorline_id", MySqlDbType.Int32) { Value = (comboBoxEdit2.SelectedItem as ComboxData).Value };
            MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar) { Value = comboBoxEdit3.Text };
            MySqlParameter capation = new MySqlParameter("@capation", MySqlDbType.VarChar) { Value = comboBoxEdit4.Text };
            MySqlParameter type = new MySqlParameter("@type", MySqlDbType.Int32) { Value = (comboBoxEdit5.SelectedItem as ComboxData).Value };
            MySqlParameter url = new MySqlParameter("@url", MySqlDbType.VarChar) { Value = comboBoxEdit6.Text };
            MySqlParameter port = new MySqlParameter("@port", MySqlDbType.VarChar) { Value = comboBoxEdit7.Text };
            MySqlParameter states = new MySqlParameter("@states", MySqlDbType.Int32) { Value = (comboBoxEdit8.SelectedItem as ComboxData).Value };
            string sql = "INSERT INTO sensor (name,capation,type,url,port,states,monitorline_id) VALUES (?,?,?,?,?,?,?)";
            int result = common.MySqlHelper.ExecuteNonQuery(sql, name, capation, type, url, port, states, monitorline_id);
            if (result == 1)
                alertControl1.Show(this, "提示", "新增成功");
            else
                alertControl1.Show(this, "提示", "新增失败");
            this.Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// TCP/串口选择功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit5.SelectedItem.ToString().Equals("串口通信"))
            {
                comboBoxEdit6.Enabled = false;
                comboBoxEdit7.Enabled = false;
            }
            else
            {
                comboBoxEdit6.Enabled = true;
                comboBoxEdit7.Enabled = true;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                string ip = comboBoxEdit6.Text.Trim();
                string port = comboBoxEdit7.Text.Trim();
                //参数1指定ip地址，参数2指定端口
                var result = client.BeginConnect(ip, Convert.ToInt32(port), null, null);
                //TimeSpan.FromSeconds(1) 表示测试连接1秒，即超时时间
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                if (success)
                {
                    alertControl1.Show(this, "提示", "测试连接成功");
                    simpleButton1.Enabled = true;
                }
                else
                {
                    alertControl1.Show(this, "提示", "测试连接失败");
                    simpleButton1.Enabled = false;
                }
                client.Close();
                Console.WriteLine(success);
            }
            catch (Exception ex)
            {
                _logger.Info(ex.Message);
            }
        }
    }

    class ComboxData
    {
        public string Text { set; get; }
        public string Value { set; get; }
        public override string ToString()
        {
            return Text;
        }
    }

}