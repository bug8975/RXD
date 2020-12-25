using RXD.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RXD
{
    public partial class Form1 : Form
    {
        [DllImport("rnx2rtkp_sky.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int rtklib_sky(int station, int year, int mon, int day, int hour);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO:1开启多线程获取数据流
            Download.GetInstance().DownloadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO: 1.终止线程池中的正在获取数据源的线程
            Download.GetInstance().ctsToken.Cancel();
            Download.GetInstance().ctsToken = new CancellationTokenSource();
            //TODO:1开启多线程获取数据流
            Download.GetInstance().DownloadData();
        }
    }
}
