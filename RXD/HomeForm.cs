using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Configuration;
using System.Data;
using DevExpress.XtraCharts;
using System.Collections.Generic;
using RXD.common;
using RXD.pojo;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Columns;

namespace RXD
{
    public partial class HomeForm : DevExpress.XtraEditors.XtraForm
    {
        Thread threadWatch1 = null; //负责监听客户端的线程
        Socket socketWatch1 = null; //
        Thread threadWatch2 = null; //负责监听客户端的线程
        Socket socketWatch2 = null; //负责监听客户端的套接字
        List<Socket> socConnections = new List<Socket>();
        List<Thread> dictThread = new List<Thread>();

        public HomeForm()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void HomeForm_Load(object sender, EventArgs e)
        {
            BindChartControl();
            //批量插入数据
            /*threadInsert = new Thread(ReadFile);
            threadInsert.IsBackground = true;
            threadInsert.Start();*/
        }

        //打开socket连接
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketWatch1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息 需要1个IP地址和端口号
            IPAddress ipaddress = IPAddress.Parse(txtIP.Text.Trim());
            //将IP地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(txtPORT.Text.Trim()));
            //监听绑定的网络节点
            socketWatch1.Bind(endpoint);
            //将套接字的监听队列长度限制为20
            socketWatch1.Listen(20);
            //创建一个监听线程
            threadWatch1 = new Thread(RecMsg1);
            //将窗体线程设置为与后台同步
            threadWatch1.IsBackground = true;
            //启动线程
            threadWatch1.Start();
            //启动线程后 txtMsg文本框显示相应提示
            txtMsg.AppendText("开始监听客户端传来的信息!" + "\r\n");
            simpleButton3.Enabled = false;
            simpleButton4.Enabled = true;
        }

        private void RecMsg1()
        {
            string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
            string filePath = @"Test-" + time + ".DAT";
            try
            {
                using (FileStream s = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(s, Encoding.Default, true))
                    {
                        while (true) //持续监听服务端发来的消息
                        {
                            Socket socConnection = socketWatch1.Accept();
                            byte[] arrRecMsg = new byte[1024*2];
                            //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                            int length = socConnection.Receive(arrRecMsg);
                            byte[] tem = new byte[length];
                            Array.Copy(arrRecMsg, 0, tem, 0, length);
                            //StringBuilder builder = new StringBuilder();
                            //for (int i = 0; i < arrRecMsg.Length; i++)
                            //{
                            //    builder.Append(string.Format("{0:X2} ", arrRecMsg[i]));
                            //}
                            //string str = builder.ToString().Trim();
                            //bw.Write(str);
                            bw.Write(Encoding.Default.GetString(tem));
                            bw.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Asterisk.Play();
                Console.WriteLine(ex.Message);
            }
        }



        //关闭socket连接
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            threadWatch1.Abort();
            if (socketWatch1 != null)
            {
                socketWatch1.Shutdown(SocketShutdown.Both);
                socketWatch1.Close();
            }
            simpleButton3.Enabled = true;
            simpleButton4.Enabled = false;
        }

        //修改数据库连接密码
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text.Trim();
            string passWord = txtUser.Text.Trim();
            if (userName.Equals("") || passWord.Equals("")) { return; }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["connectionString"].Value = "server=localhost;uid=" + userName + "; pwd=" + passWord + ";Database=rxd";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        [Obsolete]
        private void BindChartControl()
        {
            //string sql = "select * from sensor";
            //DataTable dataTable = MySqlHelper.GetDataSet(sql, null).Tables[0];

            //gridControl表格数据源
            //绑定列
            //gridView1.Columns.Add(new GridColumn() { Name = "id", FieldName = "id", Caption = "No", Visible = false });
            //gridView1.Columns.Add(new GridColumn() { Name = "x", FieldName = "x", Caption = "X(m)", VisibleIndex = 0 });
            //gridView1.Columns.Add(new GridColumn() { Name = "y", FieldName = "y", Caption = "Y(m)", VisibleIndex = 1 });
            //gridView1.Columns.Add(new GridColumn() { Name = "z", FieldName = "z", Caption = "Z(m)", VisibleIndex = 2 });
            //gridView1.Columns.Add(new GridColumn() { Name = "ax", FieldName = "ax", Caption = "AX(m)", VisibleIndex = 3 });
            //gridView1.Columns.Add(new GridColumn() { Name = "ay", FieldName = "ay", Caption = "AY(m)", VisibleIndex = 4 });
            //gridView1.Columns.Add(new GridColumn() { Name = "az", FieldName = "az", Caption = "AZ(m)", VisibleIndex = 5 });
            //gridView1.Columns.Add(new GridColumn() { Name = "time", FieldName = "time", Caption = "时间", VisibleIndex = 6 });
            //自动列宽,会出现横向滚动条
            //gridView1.OptionsView.ColumnAutoWidth = false;
            //自动列宽
            //gridView1.BestFitColumns();
            //gridControl1.DataSource = dataTable;

            //chartControl数据源
            //DataView dv = new DataView(dataTable);
            //chartControl1.DataSource = dv;
            //chartControl1.Series.Clear();
            //for (int i = 0; i < dataTable.Columns.Count; i++)
            //{
            //    if (dataTable.Columns[i].ToString().Equals("id"))
            //        continue;
            //    if (dataTable.Columns[i].ToString().Equals("time"))
            //        continue;
            //    if (dataTable.Columns[i].ToString().Equals("ax"))
            //        continue;
            //    if (dataTable.Columns[i].ToString().Equals("ay"))
            //        continue;
            //    if (dataTable.Columns[i].ToString().Equals("az"))
            //        continue;
            //    Series series1 = new Series(dataTable.Columns[i].ToString(), ViewType.Spline);
            //    for (int m = 0; m < dataTable.Rows.Count; m++)
            //    {
            //        series1.Points.Add(new SeriesPoint(dataTable.Rows[m].ItemArray[7].ToString(), new double[] { Convert.ToDouble(dataTable.Rows[m][dataTable.Columns[i].ToString()]) }));
            //    }
            //    chartControl1.Series.Add(series1);
            //}
            //chartControl1.Legend.Visible = true;

        }

        private void ReadFile()
        {
            String fileName = @"static.pos";
            List<pojo.DataView> SensorList = new List<pojo.DataView>();
            StreamReader sr = new StreamReader(fileName, Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("%"))
                {
                    continue;
                }
                string[] data = Regex.Split(line, @"\s {2,}");
                string[] result = new string[4];
                Array.Copy(data, result, 4);
                DateTime dt;
                DateTime.TryParse(result[0], out dt);
                dt.ToString("yyyy/MM/dd HH:mm:ss");
                pojo.DataView sensor = new pojo.DataView(double.Parse(result[1]), double.Parse(result[2]), double.Parse(result[3]), dt, 0);
                SensorList.Add(sensor);
            }
            //插入数据库
            MySqlHelper.InsertTable_DataView(SensorList);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            socketWatch2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息 需要1个IP地址和端口号
            IPAddress ipaddress = IPAddress.Parse(textEdit2.Text.Trim());
            //将IP地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(textEdit1.Text.Trim()));
            //监听绑定的网络节点
            socketWatch2.Connect(endpoint);
            //将套接字的监听队列长度限制为20
            /*socketWatch.Listen(20);*/
            //创建一个监听线程
            threadWatch2 = new Thread(RecMsg2);
            //将窗体线程设置为与后台同步
            threadWatch2.IsBackground = true;
            //启动线程
            threadWatch2.Start();
            //启动线程后 txtMsg文本框显示相应提示
            txtMsg.AppendText("开始监听客户端传来的信息!" + "\r\n");
            simpleButton2.Enabled = false;
            simpleButton1.Enabled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            threadWatch2.Abort();
            if (socketWatch2 != null)
            {
                socketWatch2.Shutdown(SocketShutdown.Both);
                socketWatch2.Close();
            }
            simpleButton2.Enabled = true;
            simpleButton1.Enabled = false;
        }

        private void RecMsg2()
        {
            string time = DateTime.Now.ToString("yyyy_M_d_H-0-0");
            string filePath = @"ReceivedTofile2-TCPCLIENT-" + time + ".DAT";
            try
            {
                using (FileStream s = new FileStream(filePath, FileMode.Append))
                {
                    using (BinaryWriter bw = new BinaryWriter(s, Encoding.Default, true))
                    {
                        while (true) //持续监听服务端发来的消息
                        {
                            //定义一个1024*200的内存缓冲区 用于临时性存储接收到的信息
                            byte[] arrRecMsg = new byte[1024 * 200];
                            //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                            int length = socketWatch2.Receive(arrRecMsg);
                            byte[] tem = new byte[length];
                            Array.Copy(arrRecMsg, 0, tem, 0, length);
                            bw.Write(arrRecMsg);
                            Console.WriteLine(Encoding.UTF8.GetString(arrRecMsg));
                            bw.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Asterisk.Play();
                Console.WriteLine(ex.Message);
            }

        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }
    }
}