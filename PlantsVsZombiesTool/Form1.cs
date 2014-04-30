using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlantsVsZombiesTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //启动无线阳光
        private void btnGet_Click(object sender, EventArgs e)
        {
            if (Helper.GetPidByProcessName(processName) == 0)
            {
                MessageBox.Show("哥们启用之前游戏总该运行吧！");
                return;
            }
            if (btnGet.Text == "启用-阳光无限")
            {
                timer1.Enabled = true;
                btnGet.Text = "关闭-阳光无限";
            }
            else
            {
                timer1.Enabled = false;
                btnGet.Text = "启用-阳光无限";
            }
        }

        private void btnMoney_Click(object sender, EventArgs e)
        {
            if (Helper.GetPidByProcessName(processName) == 0)
            {
                MessageBox.Show("哥们启用之前游戏总该运行吧！");
                return;
            }
            if (btnMoney.Text == "启用-金钱无限")
            {
                timer2.Enabled = true;
                btnMoney.Text = "关闭-金钱无限";
            }
            else
            {
                timer2.Enabled = false;
                btnMoney.Text = "启用-金钱无限";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Helper.GetPidByProcessName(processName) == 0)
            {
                timer1.Enabled = false;
                btnGet.Text = "启用-阳光无限";
            }
            int address = ReadMemoryValue(baseAddress);             //读取基址(该地址不会改变)
            address = address + 0x768;                              //获取2级地址
            address = ReadMemoryValue(address);
            address = address + 0x5560;                             //获取存放阳光数值的地址
            WriteMemory(address, 0x1869F);                          //写入数据到地址（0x1869F表示99999）
            timer1.Interval = 1000;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Helper.GetPidByProcessName(processName) == 0)
            {
                timer2.Enabled = false;
                btnMoney.Text = "启用-金钱无限";
            }
            int address = ReadMemoryValue(baseAddress);             //读取基址(该地址不会改变)
            address = address + 0x82C;                              //获取2级地址
            address = ReadMemoryValue(address);
            address = address + 0x28;                               //得到金钱地址
            WriteMemory(address, 0x1869F);                          //写入数据到地址（0x1869F表示99999）
            timer2.Interval = 1000;

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (Helper.GetPidByProcessName(processName) == 0)
            {
                MessageBox.Show("哥们启用之前游戏总该运行吧！");
                return;
            }
            int address = ReadMemoryValue(baseAddress);             //读取基址(该地址不会改变)
            address = address + 0x82C;                              //获取2级地址
            address = ReadMemoryValue(address);
            address = address + 0x24;
            int lev = 1;
            try
            {
                lev = int.Parse(txtLev.Text.Trim());
            }
            catch
            {
                MessageBox.Show("输入的关卡格式不真确！默认设置为1");
            }

            WriteMemory(address, lev);

        }

        //读取制定内存中的值
        public int ReadMemoryValue(int baseAdd)
        {
            return Helper.ReadMemoryValue(baseAdd, processName);
        }

        //将值写入指定内存中
        public void WriteMemory(int baseAdd, int value)
        {
            Helper.WriteMemoryValue(baseAdd, processName, value);
        }

        private int baseAddress = 0x006A9EC0;           //游戏内存基址
        private string processName = "PlantsVsZombies"; //游戏进程名字

    }
}
