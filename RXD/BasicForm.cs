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
using NLog;
using DevExpress.XtraCharts;
using DevExpress.XtraBars.Docking2010;

namespace RXD
{
    public partial class BasicForm : DevExpress.XtraEditors.XtraForm
    {
        [DllImport("convbin.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_convin(int id, int year, int month, int day, int hour);

        [DllImport("rnx2rtkp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_rnx2rtkp(int year, int month, int day, int hour, int id1, int id, int cycle);

        [DllImport("rnx2rtkp_sky.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_sky(int station, int year, int mon, int day, int hour);

        static AutoResetEvent auto = new AutoResetEvent(false);
        Logger _logger = LogManager.GetCurrentClassLogger();

        public BasicForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 工程--创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            URLForm url = new URLForm();
            if (url.ShowDialog() == DialogResult.OK)
            {
                url.MdiParent = this;
                url.ShowDialog();
            }
        }

        /// <summary>
        /// 数据库--修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DBForm db = new DBForm();
            if (db.ShowDialog() == DialogResult.OK)
            {
                db.MdiParent = this;
                db.ShowDialog();
            }
        }

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasicForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“rxdDataSet.sensorinfo”中。您可以根据需要移动或删除它。
            this.sensorinfoTableAdapter.Fill(this.rxdDataSet.sensorinfo);
            // TODO: 这行代码将数据加载到表“rxdDataSet.dataview”中。您可以根据需要移动或删除它。
            this.dataviewTableAdapter.Fill(this.rxdDataSet.dataview);
            SkinHelper.InitSkinPopupMenu(MenuSkin);
            //TODO:2注册定时任务
            DateTime now = DateTime.Now.AddHours(1.0);
            DateTime oclock = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            TimeSpan ts = oclock - DateTime.Now;
            timer1.Interval = (int)ts.TotalMilliseconds;
            timer1.Start();

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
            try
            {
                string sql = "select * from monitorline where project_id = ?";
                MySqlParameter project_id = new MySqlParameter("@project_id", MySqlDbType.Int32) { Value = e.Item.Id };
                DataTable dt = common.MySqlHelper.GetDataSet(sql, project_id).Tables[0];
                navBarControl1.Groups.Clear();
                //主窗口标题
                this.Text = e.Item.Caption;
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
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----itemFaviate_ItemClick方法");
            }
        }

        /// <summary>
        /// 切换传感器重新加载数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string sql = "select * from dataview where sensor_id = ?";
            MySqlParameter mp = new MySqlParameter(@"sensor_id", MySqlDbType.Int32) { Value = e.Link.Item.Tag };
            string sql_satallite = "SELECT s1.* FROM sensorinfo s1 LEFT JOIN sensorinfo s2 ON (s1.name = s2.name AND s1.id < s2.id) WHERE s2.id is NULL AND s1.sensor_id = ?";
            //string sql_noise = "SELECT s1.name,s1.noise,s1.type FROM sensorinfo s1 LEFT JOIN sensorinfo s2 ON (s1.name = s2.name AND s1.id < s2.id) WHERE s2.id is NULL AND s1.sensor_id = ?";
            try
            {
                DataSet ds = common.MySqlHelper.GetDataSet(sql, mp);
                this.chartControl1.DataSource = null;
                this.chartControl1.DataSource = ds.Tables[0];

                this.gridControl1.DataSource = null;
                this.gridControl1.DataSource = ds.Tables[0];

                DataSet ds_satallite = common.MySqlHelper.GetDataSet(sql_satallite, mp);
                this.chartControl2.DataSource = null;
                this.chartControl2.DataSource = ds_satallite.Tables[0];

                //DataSet ds_noise = common.MySqlHelper.GetDataSet(sql_noise, mp);
                this.chartControl3.DataSource = null;
                this.chartControl3.DataSource = ds_satallite.Tables[0];
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----item_LinkClicked方法");
            }
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

        /// <summary>
        /// 项目--创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            LineForm lineForm = new LineForm();
            if (lineForm.ShowDialog() == DialogResult.OK)
            {
                lineForm.MdiParent = this;
                lineForm.Show();
            }
        }

        /// <summary>
        /// 卫星图--传感器点上色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartControl2_BoundDataChanged(object sender, EventArgs e)
        {
            SeriesPointCollection points = chartControl2.Series[0].Points;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].ToolTipHint.Equals("G"))
                {
                    points[i].Color = Color.Red;
                    continue;
                }
                if (points[i].ToolTipHint.Equals("C"))
                {
                    points[i].Color = Color.Green;
                    continue;
                }
                if (points[i].ToolTipHint.Equals("R"))
                {
                    points[i].Color = Color.Yellow;
                    continue;
                }
            }
        }

        /// <summary>
        /// 信噪比图--传感器点上色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartControl3_BoundDataChanged(object sender, EventArgs e)
        {
            SeriesPointCollection points = chartControl3.Series[0].Points;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].ToolTipHint.Equals("G"))
                {
                    points[i].Color = Color.Red;
                    continue;
                }
                if (points[i].ToolTipHint.Equals("C"))
                {
                    points[i].Color = Color.Green;
                    continue;
                }
                if (points[i].ToolTipHint.Equals("R"))
                {
                    points[i].Color = Color.Yellow;
                    continue;
                }
            }
        }

        /// <summary>
        /// 卫星图和信噪比切换显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowsUIButtonPanel1_Click(object sender, ButtonEventArgs e)
        {
            switch (e.Button.Properties.Caption)
            {
                case "卫星图":
                    panelControl1.Visible = true;
                    panelControl2.Visible = false;
                    break;
                case "信噪比":
                    panelControl1.Visible = false;
                    panelControl2.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 3600 * 1000;
            ThreadPool.QueueUserWorkItem(p => { timerToDo(); });
            ThreadPool.QueueUserWorkItem(p =>
            {
                //TODO: 整点重新开启获取数据流的线程
                Download.GetInstance().ctsToken.Cancel();
                Download.GetInstance().ctsToken = new CancellationTokenSource();
                load_InitData();
            });
        }

        #region 自定义方法
        private void load_InitData()
        {
            //TODO:1开启多线程获取数据流
            ThreadPool.QueueUserWorkItem(o=> { Download.GetInstance().DownloadData(); });
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

        public void dat2navFile(int sensor_id,DateTime dt)
        {
            int result = rtklib_convin(sensor_id, dt.Year, dt.Month, dt.Day, dt.Hour);
            Console.WriteLine(result);
        }

        public void nav2posFile(DateTime dt,List<int> idList,int monitorlineid)
        {
            MySqlParameter moniterline_id = new MySqlParameter(@"moniterline_id", MySqlDbType.Int32) { Value = monitorlineid };
            string sql = "select cycles from frequency where monitorline_id = ?";
            DataTable dataTable = common.MySqlHelper.GetDataSet(sql, moniterline_id).Tables[0];
            int result = rtklib_rnx2rtkp(dt.Year, dt.Month, dt.Day, dt.Hour, idList[1], idList[0], Convert.ToInt32(dataTable.Rows[0].ItemArray[0]));
            Console.WriteLine(result);
        }

        private void ReadPosFile(string fileName,int sensor_id)
        {
            try
            {
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
                common.MySqlHelper.InsertTable_DataView(SensorList);
                Console.WriteLine("插入数据成功");

                //TODO: 6.删除POS文件
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.WriteLine("删除{0}文件成功", fileName);
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----ReadPosFile方法");
            }
        }

        private void ReadSkyFile(string fileName,int sensor_id)
        {
            try
            {
                List<pojo.SensorInfo> SensorList = new List<pojo.SensorInfo>();
                using (FileStream fs = File.OpenRead(fileName))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] data = Regex.Split(line, @",");
                            string[] result = new string[6];
                            Array.Copy(data, result, 6);
                            DateTime dt;
                            DateTime.TryParse(result[5], out dt);
                            dt.ToString("yyyy/MM/dd HH:mm:ss");
                            pojo.SensorInfo sensorInfo = new pojo.SensorInfo(result[0], result[1], double.Parse(result[2]), double.Parse(result[3]), int.Parse(result[4]), dt, sensor_id);
                            SensorList.Add(sensorInfo);
                        }
                    }
                }
                //插入数据库
                common.MySqlHelper.InsertTable_SensorInfo(SensorList);
                Console.WriteLine("插入数据成功");

                //TODO: 6.删除SKY文件
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.WriteLine("删除{0}文件成功", fileName);
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----ReadSkyFile方法");
            }
        }
        #endregion



        #region 定时核心任务
        private void timerToDo()
        {
            try
            {
                //Thread.Sleep(3000);
                string sql = "select s.id, f.createtime, f.name, s.monitorline_id, s.isbasic from file f left join sensor s on f.sensor_id = s.id where f.states = 0 and s.type = 0";
                DataTable dt = common.MySqlHelper.GetDataSet(sql, null).Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                var monitorlineid_list = dt.AsEnumerable().Select(c => c.Field<UInt32>("monitorline_id")).Distinct().ToList();
                for (int i = 0; i < monitorlineid_list.Count; i++)
                {
                    int id_basic = 0;
                    int _monitorlineid = (int)monitorlineid_list[i];
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        int _mid = Convert.ToInt32(dt.Rows[j].ItemArray[3]);
                        if (_mid != _monitorlineid)
                            continue;

                        //TODO: 将DAT转成NAV
                        int sensor_id = Convert.ToInt32(dt.Rows[j].ItemArray[0]);
                        DateTime createTime = (DateTime)dt.Rows[j].ItemArray[1];
                        int result_nav = rtklib_convin(sensor_id, createTime.Year, createTime.Month, createTime.Day, createTime.Hour);
                        if (result_nav != 0)
                            _logger.Trace("DAT转NAV文件出错,时间：{0}, 文件名：{1}", DateTime.Now, dt.Rows[j].ItemArray[2].ToString());

                        //TODO: 找到基点的id
                        int _sid = Convert.ToInt32(dt.Rows[j].ItemArray[0]);
                        int _isbasic = Convert.ToInt32(dt.Rows[j].ItemArray[4]);
                        if (_isbasic == 1)
                            id_basic = _sid;
                    }
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        int _mid = Convert.ToInt32(dt.Rows[k].ItemArray[3]);
                        if (_mid != _monitorlineid)
                            continue;
                        int _isbasic = Convert.ToInt32(dt.Rows[k].ItemArray[4]);
                        if (_isbasic == 1)
                            continue;

                        //TODO: 将NAV转成POS
                        int _sid = Convert.ToInt32(dt.Rows[k].ItemArray[0]);
                        string filename_pos = dt.Rows[k].ItemArray[2].ToString() + ".pos";
                        DateTime fileTime = (DateTime)dt.Rows[k].ItemArray[1];
                        MySqlParameter moniterline_id = new MySqlParameter(@"moniterline_id", MySqlDbType.Int32) { Value = _monitorlineid };
                        string sql_frequency = "select cycles from frequency where monitorline_id = ?";
                        DataTable dataTable = common.MySqlHelper.GetDataSet(sql_frequency, moniterline_id).Tables[0];
                        int _frequency = 60;
                        if(dataTable.Rows.Count == 0)
                        {
                            _logger.Trace("周期表没有采集周期的相关记录,monitorlineid:{0}",_monitorlineid);
                        }
                        else
                        {
                            _frequency = Convert.ToInt32(dataTable.Rows[0].ItemArray[0]);
                        }
                        int result_pos = rtklib_rnx2rtkp(fileTime.Year, fileTime.Month, fileTime.Day, fileTime.Hour, _sid, id_basic, _frequency);
                        if (result_pos != 0)
                            _logger.Trace("生成pos文件出错,时间：{0}, 文件名：{1}", DateTime.Now, dt.Rows[0].ItemArray[2].ToString());

                        //TODO: 读取POS文件，批量插入dataview表
                        ReadPosFile(filename_pos, _sid);
                    }
                }

                //TODO: 删除NAV和OBS等中间文件 && 更新数据库中文件状态
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                    int sensor_id = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                    string fileName = dt.Rows[i].ItemArray[2].ToString();
                    string fileName_nav = fileName + ".nav";
                    string fileName_obs = fileName + ".obs";
                    string filename_sky = fileName + ".sky";
                    DateTime time = (DateTime)dt.Rows[i].ItemArray[1];

                    //TODO: 生成卫星位置文件
                    int result_sky = rtklib_sky(id, time.Year, time.Month, time.Day, time.Hour);
                    if (result_sky != 0)
                        _logger.Trace("生成sky文件出错,时间：{0}, 文件名：{1}", DateTime.Now, fileName);

                    //TODO: 读取卫星信息文件，写入数据库，并删除文件
                    ReadSkyFile(filename_sky, sensor_id);

                    if (File.Exists(fileName_nav))
                    {
                        File.Delete(fileName_nav);
                        Console.WriteLine("删除{0}文件成功", fileName_nav);
                    }
                    if (File.Exists(fileName_obs))
                    {
                        File.Delete(fileName_obs);
                        Console.WriteLine("删除{0}文件成功", fileName_obs);
                    }

                    string sql_updatefile = "UPDATE file SET states = 1 WHERE name = ? ";
                    MySqlParameter mp = new MySqlParameter(@"name", MySqlDbType.VarChar) { Value = fileName };
                    int result_nav = common.MySqlHelper.ExecuteNonQuery(sql_updatefile, mp);
                    if (result_nav == 1)
                    {
                        Console.WriteLine("更新文件：{0}状态成功", fileName);
                    }
                    else
                    {
                        Console.WriteLine("更新文件：{0}状态失败", fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Trace(ex.Message + "----timerToDo方法");
            }
        }
        #endregion
    }
}