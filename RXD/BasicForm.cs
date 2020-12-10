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
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using MySql.Data.MySqlClient;
using RXD.common;
using RXD.pojo;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;

namespace RXD
{
    public partial class BasicForm : DevExpress.XtraEditors.XtraForm
    {
        [DllImport("convbin.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_convin(int a, int b, int c, int d);

        [DllImport("rnx2rtkp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_rnx2rtkp(int a, int b, int c, int d, int e, int f);
        static AutoResetEvent auto = new AutoResetEvent(false);

        public BasicForm()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            URLForm url = new URLForm();
            if (url.ShowDialog() == DialogResult.OK)
            {
                url.MdiParent = this;
                url.ShowDialog();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DBForm db = new DBForm();
            if (db.ShowDialog() == DialogResult.OK)
            {
                db.MdiParent = this;
                db.ShowDialog();
            }
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“rxdDataSet.dataview”中。您可以根据需要移动或删除它。
            this.dataviewTableAdapter.Fill(this.rxdDataSet.dataview);
            // TODO: 这行代码将数据加载到表“rxdDataSet.dataview”中。您可以根据需要移动或删除它。
            this.dataviewTableAdapter.Fill(this.rxdDataSet.dataview);
            SkinHelper.InitSkinPopupMenu(MenuSkin);
            load_InitData();
        }
        /// <summary>
        /// 项目下拉列表的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemFaviate_ItemClick(object sender, ItemClickEventArgs e)
        {
            //加载侧边栏数据
            string sql = "select * from monitorline where project_id = ?";
            MySqlParameter project_id = new MySqlParameter("@project_id", MySqlDbType.Int32)
            {
                Value = e.Item.Id
            };
            DataTable dt = common.MySqlHelper.GetDataSet(sql, project_id).Tables[0];
            navBarControl1.Groups.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                NavBarGroup group = new NavBarGroup();
                group.Caption = dt.Rows[i].ItemArray[1].ToString();
                group.Name = "navBarGroup" + id;
                string sql_sensortype = "select * from sensor where monitorline_id = ?";
                MySqlParameter monitorline_id = new MySqlParameter("@monitorline_id", MySqlDbType.Int32)
                {
                    Value = id
                };
                DataTable sensorTypes = common.MySqlHelper.GetDataSet(sql_sensortype, monitorline_id).Tables[0];
                for (int j = 0; j < sensorTypes.Rows.Count; j++)
                {
                    int sensorTypeId = Convert.ToInt32(sensorTypes.Rows[j].ItemArray[0]);
                    NavBarItem item = new NavBarItem();
                    item.Caption = sensorTypes.Rows[j].ItemArray[2].ToString();
                    item.Name = sensorTypes.Rows[j].ItemArray[1].ToString();
                    item.Tag = sensorTypeId;
                    group.ItemLinks.Add(item);
                    item.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(item_LinkClicked);
                }
                navBarControl1.Groups.Add(group);
            }
        }

        /// <summary>
        /// 切换传感器重新加载数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            this.alertControl1.Show(this, "提示", e.Link.Caption + ".." + e.Link.Item.Tag + ".." + e.Link.ItemName);
            string sql = "select * from dataview where sensor_id = ?";
            MySqlParameter mp = new MySqlParameter(@"sensor_id", MySqlDbType.Int32) { Value = e.Link.Item.Tag };
            DataSet ds = common.MySqlHelper.GetDataSet(sql, mp);
            this.chartControl1.DataSource = null;
            this.gridControl1.DataSource = null;
            this.chartControl1.DataSource = ds.Tables[0];
            this.gridControl1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// 表格自动行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 传感器--创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            SensorTypeForm sensorTypeForm = new SensorTypeForm();
            if (sensorTypeForm.ShowDialog() == DialogResult.OK)
            {
                sensorTypeForm.MdiParent = this;
                sensorTypeForm.Show();
            }
        }

        #region 自定义方法

        private void load_InitData()
        {

            //TODO:1开启多线程获取数据流
            Download.GetInstance().DownloadData();

            //TODO:2注册定时任务（从文件向数据库写入数据）
            timer1.Start();
            timer2.Start();

            //加载菜单栏项目下拉列表和点击事件
            string sql = "select * from project";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            //清空原有子菜单
            barSubItem1.ItemLinks.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BarButtonItem item = new BarButtonItem();
                item.Caption = dt.Rows[i].ItemArray[1].ToString();
                item.Id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                item.Name = "itemFaviate" + item.Id;
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemFaviate_ItemClick);
                if (i == 0)
                {
                    BarItemLink itemLink = barSubItem1.AddItem(item);
                    itemLink.BeginGroup = true;
                }
                else
                {
                    barSubItem1.AddItem(item);
                }
            }
        }

        public void dat2navFile(object obj)
        {
            if (obj == null)
                return;
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            int sensor_id = Convert.ToInt32(dic[0]);
            DateTime dt = (DateTime)dic[1];
            int result = rtklib_convin(sensor_id, dt.Year, dt.Month, dt.Day);
            //auto.Set();
            Console.WriteLine(result);
        }

        public void nav2posFile(object obj)
        {
            if (obj == null)
                return;
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            DateTime dt = (DateTime)dic[0]; //createtime
            List<int> data = (List<int>)dic[1]; //idList
            MySqlParameter moniterline_id = new MySqlParameter(@"moniterline_id", MySqlDbType.Int32) { Value = Convert.ToInt32(dic[2]) };
            string sql = "select cycles from frequency where monitorline_id = ?";
            DataTable dataTable = common.MySqlHelper.GetDataSet(sql, moniterline_id).Tables[0];
            int result = rtklib_rnx2rtkp(dt.Year, dt.Month, dt.Day, data[1], data[0], Convert.ToInt32(dataTable.Rows[0].ItemArray[0]));
            //auto.Set();
            Console.WriteLine(result);
        }

        private void ReadPosFile(object obj)
        {
            if (obj == null)
                return;
            Dictionary<int, object> dic = obj as Dictionary<int, object>;
            string fileName = dic[0].ToString();
            int sensor_id = Convert.ToInt32(dic[1]);
            List<pojo.DataView> SensorList = new List<pojo.DataView>();
            using (FileStream fs = File.OpenRead(fileName))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
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
                        pojo.DataView dataView = new pojo.DataView(double.Parse(result[1]), double.Parse(result[2]), double.Parse(result[3]), dt, sensor_id);
                        SensorList.Add(dataView);
                    }
                }
            }
            //插入数据库
            common.MySqlHelper.InsertTable(SensorList);
            Console.WriteLine("插入数据成功");

            //TODO: 6.删除POS文件
            ThreadPool.QueueUserWorkItem(o =>
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.WriteLine("删除{0}文件成功", fileName);
                }
            });

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            int hour = time.Hour;
            int sleephour = Math.Abs(15 - hour);
            if (sleephour != 0)
            {
                timer1.Interval = 1000 * 60 * 60 * sleephour;
                return;
            }
            int minute = time.Minute;
            int sleepminute = Math.Abs(4 - minute);
            if (sleepminute != 0)
            {
                timer1.Interval = 1000 * 60 * sleepminute;
                return;
            }
            int second = time.Second;
            int sleepsecond = Math.Abs((30 - 1) - second);
            if (sleepsecond != 0)
            {
                timer1.Interval = 1000 * sleepsecond;
                return;
            }
            Thread.Sleep(1000);
            timer1.Interval = 500;
            ThreadPool.QueueUserWorkItem(p => { timerToDo(); });
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            int hour = time.Hour;
            int sleephour = Math.Abs(23 - hour);
            if (sleephour != 0)
            {
                timer2.Interval = 1000 * 60 * 60 * sleephour;
                return;
            }
            int minute = time.Minute;
            int sleepminute = Math.Abs(59 - minute);
            if (sleepminute != 0)
            {
                timer2.Interval = 1000 * 60 * sleepminute;
                return;
            }
            int second = time.Second;
            int sleepsecond = Math.Abs(59 - second);
            if (sleepsecond != 0)
            {
                timer2.Interval = 1000 * sleepsecond;
                return;
            }
            Thread.Sleep(1000);
            timer2.Interval = 500;
            //TODO: 7. 24点以后重新开启获取数据流的线程
            Download.GetInstance().DownloadData();
        }

        #region 定时核心任务
        private void timerToDo()
        {
            //TODO: 1.终止线程池中的正在获取数据源的线程
            Download.GetInstance().ctsToken.Cancel();
            Console.WriteLine("终止数据源线程成功");

            string sql = "select s.id, f.createtime, f.name, s.monitorline_id from file f left join sensor s on f.sensor_id = s.id where f.states = 0 and s.type = 0";
            DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
            Dictionary<int, object> nav_dic = new Dictionary<int, object>();
            List<int> list = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<int, object> dat_dic = new Dictionary<int, object>();
                dat_dic.Add(0, Convert.ToInt32(dt.Rows[i].ItemArray[0])); //sensor_id
                dat_dic.Add(1, dt.Rows[i].ItemArray[1]); //createtime
                //TODO: 2.调用算法将DAT转成NAV文件
                //ThreadPool.QueueUserWorkItem(new WaitCallback(dat2navFile), dat_dic);
                //auto.WaitOne();
                dat2navFile(dat_dic);
                list.Add(Convert.ToInt32(dt.Rows[i].ItemArray[0])); //sensor_id
            }
            nav_dic.Add(0, dt.Rows[0].ItemArray[1]); //createtime
            nav_dic.Add(1, list); //idList
            nav_dic.Add(2, dt.Rows[0].ItemArray[3]); //monitorline_id

            //TODO: 3.调用算法将NAV转成POS文件
            //ThreadPool.QueueUserWorkItem(new WaitCallback(nav2posFile), nav_dic);
            //auto.WaitOne();
            nav2posFile(nav_dic);
            Console.WriteLine("生成POS文件成功");

            //TODO: 4.删除NAV和OBS等中间文件 && 更新数据库中文件状态
            ThreadPool.QueueUserWorkItem(o =>
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string fileName = dt.Rows[i].ItemArray[2].ToString();
                    string fileName1 = fileName + ".nav";
                    string fileName2 = fileName + ".obs";
                    if (File.Exists(fileName1))
                    {
                        File.Delete(fileName1);
                        Console.WriteLine("删除{0}文件成功", fileName1);
                    }
                    if (File.Exists(fileName2))
                    {
                        File.Delete(fileName2);
                        Console.WriteLine("删除{0}文件成功", fileName2);
                    }
                    string sql_updatefile = "UPDATE file SET states = 1 WHERE name = ? ";
                    MySqlParameter mp = new MySqlParameter(@"name", MySqlDbType.VarChar)
                    {
                        Value = fileName
                    };
                    int result = common.MySqlHelper.ExecuteNonQuery(sql_updatefile, mp);
                    if (result == 1)
                    {
                        Console.WriteLine("更新文件：{0}状态成功", fileName);
                    }
                    else
                    {
                        Console.WriteLine("更新文件：{0}状态失败", fileName);
                    }
                }
            });

            string filename = dt.Rows[1].ItemArray[2].ToString() + ".pos";
            Dictionary<int, object> pos_dic = new Dictionary<int, object>();
            pos_dic.Add(0, filename);
            pos_dic.Add(1, Convert.ToInt32(dt.Rows[1].ItemArray[0])); //sensor_id
            //TODO: 5.读取POS文件，批量插入dataview表
            ThreadPool.QueueUserWorkItem(new WaitCallback(ReadPosFile), pos_dic);
        }

        #endregion

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataviewTableAdapter.FillBy(this.rxdDataSet.dataview);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}