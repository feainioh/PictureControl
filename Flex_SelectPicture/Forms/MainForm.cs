using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture
{
    public partial class MainForm : Form
    {


        MyFunctions myf = new MyFunctions();

        public MainForm()
        {
            Load load = new Load();
            if(load.ShowDialog()==DialogResult.OK)
                InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            selectStandImage();
            startThread();
        }

        //确认启动机器
        private void selectStandImage()
        {
            if (GlobalVar.gl_val_Machine1_Check == "0")
            {
                tp_result_AVI1.Parent = null;
            }
            if (GlobalVar.gl_val_Machine2_Check == "0")
            {
                tp_result_AVI2.Parent = null;
            }
            if (GlobalVar.gl_val_Machine3_Check == "0")
            {
                tp_result_AVI3.Parent = null;
            }
            if(GlobalVar.gl_val_Machine1_Check == "1")
            {
                pictureBox_AVI1_S1_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine1_S1_TestVal + @"\Stand.bmp");//加载机台一 主站标准图片
                pictureBox_AVI1_S2_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine1_S2_TestVal + @"\Stand.bmp");//加载机台一 从站标准图片
                pictureBox_AVI1_S3_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine1_S3_TestVal + @"\Stand.bmp");//加载机台一 从站2标准图片
                int width = this.flowLayoutPanel1.Width - 25;
                int height = (int)Math.Round((this.flowLayoutPanel1.Width - 25) * 0.357);
                pictureBox_AVI1_S1_Stand.Width = width;
                pictureBox_AVI1_S1_Stand.Height = height;
                pictureBox_AVI1_S2_Stand.Width = width;
                pictureBox_AVI1_S2_Stand.Height = height;
                pictureBox_AVI1_S3_Stand.Width = width;
                pictureBox_AVI1_S3_Stand.Height = height;

            }
            if (GlobalVar.gl_val_Machine2_Check == "1")
            {
                pictureBox_AVI2_S1_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine2_S1_TestVal+@"\Stand.bmp");//加载机台一 主站标准图片
                pictureBox_AVI2_S2_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine2_S2_TestVal + @"\Stand.bmp");//加载机台一 从站标准图片
                pictureBox_AVI2_S3_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine2_S3_TestVal + @"\Stand.bmp");//加载机台一 从站2标准图片
                int width = this.flowLayoutPanel2.Width - 25;
                int height = (int)Math.Round((this.flowLayoutPanel2.Width - 25) * 0.357);
                pictureBox_AVI2_S1_Stand.Width = width;
                pictureBox_AVI2_S1_Stand.Height = height;
                pictureBox_AVI2_S2_Stand.Width = width;
                pictureBox_AVI2_S2_Stand.Height = height;
                pictureBox_AVI2_S3_Stand.Width = width;
                pictureBox_AVI2_S3_Stand.Height = height;
            }
            if (GlobalVar.gl_val_Machine3_Check == "1")
            {
                pictureBox_AVI3_S1_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine3_S1_TestVal + @"\Stand.bmp");//加载机台一 主站标准图片
                pictureBox_AVI3_S2_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine3_S2_TestVal + @"\Stand.bmp");//加载机台一 从站标准图片
                pictureBox_AVI3_S3_Stand.Image = Image.FromFile(GlobalVar.gl_val_Machine3_S3_TestVal + @"\Stand.bmp");//加载机台一 从站2标准图片
                int width = this.flowLayoutPanel3.Width - 25;
                int height = (int)Math.Round((this.flowLayoutPanel3.Width - 25) * 0.357);
                pictureBox_AVI3_S1_Stand.Width = width;
                pictureBox_AVI3_S1_Stand.Height = height;
                pictureBox_AVI3_S2_Stand.Width = width;
                pictureBox_AVI3_S2_Stand.Height = height;
                pictureBox_AVI3_S3_Stand.Width = width;
                pictureBox_AVI3_S3_Stand.Height = height;
            }
        }



        private void startThread()
        {
            Thread update_Indec_TH = new Thread(update_Index);
            update_Indec_TH.IsBackground = true;
            update_Indec_TH.Name = "更新everything文件夹索引 线程";
            update_Indec_TH.Start();

            Thread getNg_TH = new Thread(getNgLocation);
            getNg_TH.Name = "获取测试数据 线程";
            getNg_TH.IsBackground = true;
            getNg_TH.Start();
        }

        //获取测试结果
        private void getNgLocation()
        {
            try
            {
                while (GlobalVar.gl_appIsRunning)
                {
                    query.WaitOne();
                    this.Invoke(new Action(() => { select_Progress.Value = 0; }));
                    //获取测试机台
                    if (GlobalVar.gl_val_Machine1_Check == "1" && GlobalVar.Pic_S1_fileName.Contains(GlobalVar.gl_val_Machine1_S1_TestResult) && GlobalVar.Pic_S2_fileName.Contains(GlobalVar.gl_val_Machine1_S2_TestResult))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.tabControl1.SelectedTab = tp_result_AVI1;
                            select_Progress.Value += 20;
                        }));
                        LoadPic("1");//重新加载图片
                        GlobalVar.gl_ResultFrom = "1";
                    }
                    if (GlobalVar.gl_val_Machine2_Check == "1" && GlobalVar.Pic_S1_fileName.Contains(GlobalVar.gl_val_Machine2_S1_TestResult) && GlobalVar.Pic_S2_fileName.Contains(GlobalVar.gl_val_Machine2_S2_TestResult))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.tabControl1.SelectedTab = tp_result_AVI2;
                            select_Progress.Value += 20;
                        }));
                        LoadPic("2");
                        GlobalVar.gl_ResultFrom = "2";
                    }
                    if (GlobalVar.gl_val_Machine3_Check == "1" && GlobalVar.Pic_S1_fileName.Contains(GlobalVar.gl_val_Machine3_S1_TestResult) && GlobalVar.Pic_S2_fileName.Contains(GlobalVar.gl_val_Machine3_S2_TestResult))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.tabControl1.SelectedTab = tp_result_AVI3;
                            select_Progress.Value += 20;
                        }));
                        LoadPic("3");
                        GlobalVar.gl_ResultFrom = "3";
                    }
                    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                    Thread.Sleep(50);
                    findResult();//查询结果 
                    if (GlobalVar.gl_TestResult.Count == 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lb_testResult.Text = "无数据";
                            select_Progress.Value = 100;
                        }));
                        MessageBox.Show("未找到测试结果！");
                    }
                    else
                    {
                        for (int i = 0; i < GlobalVar.gl_TestResult.Count - 1; i++)//对测试结果进行排序
                        {
                            for (int j = 0; j < GlobalVar.gl_TestResult.Count - 1 - i; j++)
                            {
                                string temp;
                                string[] array_0 = GlobalVar.gl_TestResult[j].Split('|');
                                string[] array_1 = GlobalVar.gl_TestResult[j + 1].Split('|');
                                if ((DateTime.Parse(array_0[0]) - DateTime.Parse(array_1[0])).TotalDays > 0)
                                {
                                    temp = GlobalVar.gl_TestResult[j];
                                    GlobalVar.gl_TestResult[j] = GlobalVar.gl_TestResult[j + 1];
                                    GlobalVar.gl_TestResult[j + 1] = temp;
                                }
                            }
                        }
                        string lastResult = GlobalVar.gl_TestResult[GlobalVar.gl_TestResult.Count - 1];//取最新的测试结果
                        string[] array = lastResult.Split('|');
                        this.BeginInvoke(new Action(() =>
                        {
                            lb_testTime.Text = array[0].Trim();
                        }));
                        GlobalVar.gl_testDate = DateTime.Parse(array[0]);
                        this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                        #region 处理NG项
                        List<Point[]> S1_point = new List<Point[]>();
                        List<Point[]> S2_point = new List<Point[]>();
                        List<Point[]> S3_point = new List<Point[]>();
                       // string[] arrRe = array[1].Split('：');
                        //array[3]主站主 array[4]主站副 array[5]从站主 array[6]从站副
                                string[] arr_s1_1 = array[3].Split('，');//朱站住测试项
                                string[] arr_s1_2 = array[4].Split('，');//主站副测试项 
                                string[] arr_s2_1 = array[5].Split('，');//从站主测试项 
                                string[] arr_s2_2 = array[6].Split('，');//从站副测试项 
                        switch (GlobalVar.gl_ResultFrom)
                        {
                            case "1":
                                #region 处理AVI-1--不用
                                //if (arrRe[0].Contains("主"))//处理主站测试NG项
                                //{
                                //    foreach (string item in arrRe[1].Split('，'))//主站
                                //    {
                                //        if (item != "")
                                //        { 
                                //        if (GlobalVar.AVI1_S1_TestItems.ContainsKey(item))
                                //        {
                                //            Point p1 = new Point();
                                //            p1.X = int.Parse(GlobalVar.AVI1_S1_TestItems[item][0]);
                                //            p1.Y = int.Parse(GlobalVar.AVI1_S1_TestItems[item][1]);
                                //            Point p2 = new Point();
                                //            p2.X = int.Parse(GlobalVar.AVI1_S1_TestItems[item][2]);
                                //            p2.Y = int.Parse(GlobalVar.AVI1_S1_TestItems[item][3]);
                                //            Point[] p = { p1, p2 };
                                //            S1_point.Add(p);
                                //        }
                                //       }
                                //    }
                                //    if (arrRe[1].Contains("从"))
                                //    {
                                //        foreach (string item in arrRe[2].Split('，'))//从站
                                //        {
                                //            if (item != "")
                                //            {
                                //                if (GlobalVar.AVI1_S2_TestItems.ContainsKey(item))
                                //                {
                                //                    Point p1 = new Point();
                                //                    p1.X = int.Parse(GlobalVar.AVI1_S2_TestItems[item][0]);
                                //                    p1.Y = int.Parse(GlobalVar.AVI1_S2_TestItems[item][1]);
                                //                    Point p2 = new Point();
                                //                    p2.X = int.Parse(GlobalVar.AVI1_S2_TestItems[item][2]);
                                //                    p2.Y = int.Parse(GlobalVar.AVI1_S2_TestItems[item][3]);
                                //                    Point[] p = { p1, p2 };
                                //                    S2_point.Add(p);
                                //                }
                                //            }
                                //        }
                                //    }
                                //    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                //    DrawNGRectangle(pictureBox_AVI1_S1_Stand, S1_point);
                                //    DrawNGRectangle(pictureBox_AVI1_S2_Stand, S2_point);
                                //}
                                //else
                                //{
                                //    S2_point = new List<Point[]>();
                                //    foreach (string item in arrRe[1].Split('，'))//仅有从站
                                //    {
                                //        if (item != "")
                                //        {
                                //        if (GlobalVar.AVI1_S2_TestItems.ContainsKey(item))
                                //        {
                                //            Point p1 = new Point();
                                //            p1.X = int.Parse(GlobalVar.AVI1_S2_TestItems[item][0]);
                                //            p1.Y = int.Parse(GlobalVar.AVI1_S2_TestItems[item][1]);
                                //            Point p2 = new Point();
                                //            p2.X = int.Parse(GlobalVar.AVI1_S2_TestItems[item][2]);
                                //            p2.Y = int.Parse(GlobalVar.AVI1_S2_TestItems[item][3]);
                                //            Point[] p = { p1, p2 };
                                //            S2_point.Add(p);
                                //        }
                                //        }

                                //    }
                                //    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                //    DrawNGRectangle(pictureBox_AVI1_S2_Stand, S2_point);
                                //}
                                #endregion

                                #region 处理AVI-1
                                //主站主处理
                                 foreach(string  item in arr_s1_1)
                                {
                                    if (GlobalVar.AVI1_S1_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI1_S1_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI1_S1_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI1_S1_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI1_S1_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S1_point.Add(p);
                                    }
                                }
                                 //从站主
                                foreach (string item in arr_s2_1)
                                {
                                    if (GlobalVar.AVI1_S2_TestItems.ContainsKey(item))
                                    {
                        //MessageBox.Show(array[5].ToString()+ GlobalVar.AVI1_S2_TestItems.ContainsKey(item).ToString());
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI1_S2_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI1_S2_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI1_S2_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI1_S2_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S2_point.Add(p);
                                    }
                                }
                                //从站副
                                foreach (string item in arr_s2_2)
                                {
                                    if (GlobalVar.AVI1_S3_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI1_S3_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI1_S3_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI1_S3_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI1_S3_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S3_point.Add(p);
                                    }
                                }
                                this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                DrawNGRectangle(pictureBox_AVI1_S1_Stand, S1_point);
                                DrawNGRectangle(pictureBox_AVI1_S2_Stand, S2_point);
                                DrawNGRectangle(pictureBox_AVI1_S3_Stand, S3_point);
                                #endregion
                                break;
                            case "2":
                                #region 处理AVI-2 --不用
                                //S1_point = new List<Point[]>();
                                //S2_point = new List<Point[]>();
                                //if (arrRe[0].Contains("主"))//处理主站测试NG项
                                //{
                                //    foreach (string item in arrRe[1].Split('，'))//主站
                                //    {
                                //        if (GlobalVar.AVI2_S1_TestItems.ContainsKey(item))
                                //        {
                                //            Point p1 = new Point();
                                //            p1.X = int.Parse(GlobalVar.AVI2_S1_TestItems[item][0]);
                                //            p1.Y = int.Parse(GlobalVar.AVI2_S1_TestItems[item][1]);
                                //            Point p2 = new Point();
                                //            p2.X = int.Parse(GlobalVar.AVI2_S1_TestItems[item][2]);
                                //            p2.Y = int.Parse(GlobalVar.AVI2_S1_TestItems[item][3]);
                                //            Point[] p = { p1, p2 };
                                //            S1_point.Add(p);
                                //        }
                                //    }
                                //    if (arrRe[1].Contains("从"))
                                //    {
                                //        foreach (string item in arrRe[2].Split('，'))//从站
                                //        {
                                //            if (GlobalVar.AVI2_S2_TestItems.ContainsKey(item))
                                //            {
                                //                Point p1 = new Point();
                                //                p1.X = int.Parse(GlobalVar.AVI2_S2_TestItems[item][0]);
                                //                p1.Y = int.Parse(GlobalVar.AVI2_S2_TestItems[item][1]);
                                //                Point p2 = new Point();
                                //                p2.X = int.Parse(GlobalVar.AVI2_S2_TestItems[item][2]);
                                //                p2.Y = int.Parse(GlobalVar.AVI2_S2_TestItems[item][3]);
                                //                Point[] p = { p1, p2 };
                                //                S2_point.Add(p);
                                //            }
                                //        }
                                //    }
                                //    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                //    DrawNGRectangle(pictureBox_AVI2_S1_Stand, S1_point);
                                //    DrawNGRectangle(pictureBox_AVI2_S2_Stand, S2_point);
                                //}
                                //else
                                //{
                                //    S2_point = new List<Point[]>();
                                //    foreach (string item in arrRe[1].Split('，'))//仅有从站
                                //    {
                                //        if (GlobalVar.AVI2_S2_TestItems.ContainsKey(item))
                                //        {
                                //            Point p1 = new Point();
                                //            p1.X = int.Parse(GlobalVar.AVI2_S2_TestItems[item][0]);
                                //            p1.Y = int.Parse(GlobalVar.AVI2_S2_TestItems[item][1]);
                                //            Point p2 = new Point();
                                //            p2.X = int.Parse(GlobalVar.AVI2_S2_TestItems[item][2]);
                                //            p2.Y = int.Parse(GlobalVar.AVI2_S2_TestItems[item][3]);
                                //            Point[] p = { p1, p2 };
                                //            S2_point.Add(p);
                                //        }
                                //    }
                                //    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                //    DrawNGRectangle(pictureBox_AVI2_S2_Stand, S2_point);
                                //}
                                #endregion

                                #region 处理AVI-2
                                S1_point = new List<Point[]>();
                                S2_point = new List<Point[]>();
                                S3_point = new List<Point[]>();
                                //主站主处理
                                foreach (string item in arr_s1_1)
                                {
                                    if (GlobalVar.AVI2_S1_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI2_S1_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI2_S1_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI2_S1_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI2_S1_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S1_point.Add(p);
                                    }
                                }
                                //从站主
                                foreach (string item in arr_s2_1)
                                {
                                    if (GlobalVar.AVI2_S2_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI2_S2_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI2_S2_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI2_S2_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI2_S2_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S2_point.Add(p);
                                    }
                                }
                                //从站副
                                foreach (string item in arr_s2_2)
                                {
                                    if (GlobalVar.AVI2_S3_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI2_S3_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI2_S3_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI2_S3_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI2_S3_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S3_point.Add(p);
                                    }
                                }
                                this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                DrawNGRectangle(pictureBox_AVI2_S1_Stand, S1_point);
                                DrawNGRectangle(pictureBox_AVI2_S2_Stand, S2_point);
                                DrawNGRectangle(pictureBox_AVI2_S3_Stand, S3_point);
                                #endregion
                                break;
                            case "3":
                                #region 处理AVI-3--不用
                                //S1_point = new List<Point[]>();
                                //S2_point = new List<Point[]>();
                                //if (arrRe[0].Contains("主"))//处理主站测试NG项
                                //{
                                //    foreach (string item in arrRe[1].Split('，'))//主站
                                //    {
                                //        if (GlobalVar.AVI3_S1_TestItems.ContainsKey(item))
                                //        {
                                //            Point p1 = new Point();
                                //            p1.X = int.Parse(GlobalVar.AVI3_S1_TestItems[item][0]);
                                //            p1.Y = int.Parse(GlobalVar.AVI3_S1_TestItems[item][1]);
                                //            Point p2 = new Point();
                                //            p2.X = int.Parse(GlobalVar.AVI3_S1_TestItems[item][2]);
                                //            p2.Y = int.Parse(GlobalVar.AVI3_S1_TestItems[item][3]);
                                //            Point[] p = { p1, p2 };
                                //            S1_point.Add(p);
                                //        }
                                //    }
                                //    if (arrRe[1].Contains("从"))
                                //    {
                                //        foreach (string item in arrRe[2].Split('，'))//从站
                                //        {
                                //            if (GlobalVar.AVI3_S2_TestItems.ContainsKey(item))
                                //            {
                                //                Point p1 = new Point();
                                //                p1.X = int.Parse(GlobalVar.AVI3_S2_TestItems[item][0]);
                                //                p1.Y = int.Parse(GlobalVar.AVI3_S2_TestItems[item][1]);
                                //                Point p2 = new Point();
                                //                p2.X = int.Parse(GlobalVar.AVI3_S2_TestItems[item][2]);
                                //                p2.Y = int.Parse(GlobalVar.AVI3_S2_TestItems[item][3]);
                                //                Point[] p = { p1, p2 };
                                //                S2_point.Add(p);
                                //            }
                                //        }
                                //    }
                                //    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                //    DrawNGRectangle(pictureBox_AVI3_S1_Stand, S1_point);
                                //    DrawNGRectangle(pictureBox_AVI3_S2_Stand, S2_point);
                                //}
                                //else
                                //{
                                //    S2_point = new List<Point[]>();
                                //    foreach (string item in arrRe[1].Split('，'))//仅有从站
                                //    {
                                //        if (GlobalVar.AVI3_S2_TestItems.ContainsKey(item))
                                //        {
                                //            Point p1 = new Point();
                                //            p1.X = int.Parse(GlobalVar.AVI3_S2_TestItems[item][0]);
                                //            p1.Y = int.Parse(GlobalVar.AVI3_S2_TestItems[item][1]);
                                //            Point p2 = new Point();
                                //            p2.X = int.Parse(GlobalVar.AVI3_S2_TestItems[item][2]);
                                //            p2.Y = int.Parse(GlobalVar.AVI3_S2_TestItems[item][3]);
                                //            Point[] p = { p1, p2 };
                                //            S2_point.Add(p);
                                //        }
                                //    }
                                //    this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                //    DrawNGRectangle(pictureBox_AVI3_S2_Stand, S2_point);
                                //}
                                #endregion
                                #region 处理AVI-2
                                S1_point = new List<Point[]>();
                                S2_point = new List<Point[]>();
                                S3_point = new List<Point[]>();
                                //主站主处理
                                foreach (string item in arr_s1_1)
                                {
                                    if (GlobalVar.AVI3_S1_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI3_S1_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI3_S1_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI3_S1_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI3_S1_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S1_point.Add(p);
                                    }
                                }
                                //从站主
                                foreach (string item in arr_s2_1)
                                {
                                    if (GlobalVar.AVI3_S2_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI3_S2_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI3_S2_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI3_S2_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI3_S2_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S2_point.Add(p);
                                    }
                                }
                                //从站副
                                foreach (string item in arr_s2_2)
                                {
                                    if (GlobalVar.AVI2_S3_TestItems.ContainsKey(item))
                                    {
                                        Point p1 = new Point();
                                        p1.X = int.Parse(GlobalVar.AVI3_S3_TestItems[item][0]);
                                        p1.Y = int.Parse(GlobalVar.AVI3_S3_TestItems[item][1]);
                                        Point p2 = new Point();
                                        p2.X = int.Parse(GlobalVar.AVI3_S3_TestItems[item][2]);
                                        p2.Y = int.Parse(GlobalVar.AVI3_S3_TestItems[item][3]);
                                        Point[] p = { p1, p2 };
                                        S3_point.Add(p);
                                    }
                                }
                                this.Invoke(new Action(() => { select_Progress.Value += 20; }));
                                DrawNGRectangle(pictureBox_AVI3_S1_Stand, S1_point);
                                DrawNGRectangle(pictureBox_AVI3_S2_Stand, S2_point);
                                DrawNGRectangle(pictureBox_AVI3_S3_Stand, S3_point);
                                #endregion
                                break;
                            default:
                                break;
                        }
                        #endregion
                        this.Invoke(new Action(() =>
                        {

                            if (array[2].Contains("无产品"))
                            {
                                lb_testResult.Text = array[2];
                                lb_testResult.ForeColor = Color.YellowGreen;
                            }
                            else if (array[2].Contains("\\"))
                            {
                                lb_testResult.Text = "数据异常";
                                lb_testResult.ForeColor = Color.Gray;
                            }
                            else if (array[2].Contains("OK"))
                            {
                                lb_testResult.Text = array[2].Trim();
                                lb_testResult.ForeColor = Color.Green;
                            }
                            else if (array[2].Contains("NG"))
                            {
                                lb_testResult.Text = array[2].Trim();
                                lb_testResult.ForeColor = Color.Red;
                            }
                            select_Progress.Value = 100;
                        }));

                    }
                }
            }catch(Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
            }
        }
        private static object locker = new object();
        private void DrawNGRectangle(PictureBox pictureBox_avi, List<Point[]> arraypoint)
        {
            locker = pictureBox_avi;
            lock (locker)
            {
                Bitmap image = (Bitmap)pictureBox_avi.Image.Clone();
                Graphics g = Graphics.FromImage(image);
                foreach (Point[] point in arraypoint)
                {
                    g.DrawRectangle(new Pen(Color.Red, 2), point[0].X, point[0].Y, (point[1].X - point[0].X), (point[1].Y - point[0].Y));
                }
                pictureBox_avi.Image = image;
                g.Dispose();
            }
        }

        private void findResult()
        {
            try
            {
                GlobalVar.gl_TestResult.Clear();
                string[] filepath;
                if (GlobalVar.gl_val_Machine1_Check == "1")
                {
                    filepath = Directory.GetFiles(GlobalVar.gl_val_Machine1_S1_TestResult + @"\" + DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyyMMdd") + "*.csv");
                    foreach (string path in filepath)
                    {
                        GlobalVar.gl_TestResult.AddRange(myf.SearchReult(path));
                    }
                }
                if (GlobalVar.gl_val_Machine2_Check == "1")
                {
                    filepath = Directory.GetFiles(GlobalVar.gl_val_Machine2_S1_TestResult + @"\" + DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyyMMdd") + "*.csv");
                    foreach (string path in filepath)
                    {
                        GlobalVar.gl_TestResult.AddRange(myf.SearchReult(path));
                    }
                }
                if (GlobalVar.gl_val_Machine3_Check == "1")
                {
                    filepath = Directory.GetFiles(GlobalVar.gl_val_Machine3_S1_TestResult + @"\" + DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyyMMdd") + "*.csv");
                    foreach (string path in filepath)
                    {
                        GlobalVar.gl_TestResult.AddRange(myf.SearchReult(path));
                    }
                }
            }catch(Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
            }
        }

        private void LoadPic(string v)
        {
            try
            {
                Image S1_Image;
                Image S2_Image;
                Image S3_Image;
                switch (v)
                {
                    case "1":
                        #region AVI1 PIC
                        S1_Image = Image.FromFile(GlobalVar.gl_val_Machine1_S1_TestVal + @"\Stand.bmp");
                        S2_Image = Image.FromFile(GlobalVar.gl_val_Machine1_S2_TestVal + @"\Stand.bmp");
                        S3_Image = Image.FromFile(GlobalVar.gl_val_Machine1_S3_TestVal+@"\Stand.bmp");
                        Bitmap bmp = (Bitmap)new Bitmap(S1_Image.Width, S1_Image.Height);
                        Bitmap bmp1 = (Bitmap)new Bitmap(S2_Image.Width, S2_Image.Height);
                        Bitmap bmp2 = (Bitmap)new Bitmap(S3_Image.Width, S3_Image.Height);

                        BufferedGraphicsContext GraphicsContext = BufferedGraphicsManager.Current;//双缓冲
                        BufferedGraphics myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI1_S1_Stand.CreateGraphics(), this.pictureBox_AVI1_S1_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            graphics.DrawImage(bmp, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI1_S2_Stand.CreateGraphics(), this.pictureBox_AVI1_S2_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp1.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
                            graphics.DrawImage(bmp1, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI1_S3_Stand.CreateGraphics(), this.pictureBox_AVI1_S3_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp1.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                            graphics.DrawImage(bmp2, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        Bitmap image = new Bitmap(S1_Image);
                        Bitmap image1 = new Bitmap(S2_Image);
                        Bitmap image2 = new Bitmap(S3_Image);
                        Graphics g = Graphics.FromImage(image);
                        Graphics g1 = Graphics.FromImage(image1);
                        Graphics g2 = Graphics.FromImage(image2);
                        //加水印  
                        String str = GlobalVar.gl_Barcode;
                        Font font = new Font("微软雅黑", image.Width / 85);

                        SolidBrush sbrush = new SolidBrush(Color.Red);
                        lock (pictureBox_AVI1_S1_Stand)
                        {
                            g.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI1_S1_Stand.Image = image;
                            g.Dispose();
                            if (bmp != null)
                                bmp.Dispose();
                            myBuffer.Dispose();
                        }
                        lock (pictureBox_AVI1_S2_Stand)
                        {
                            g1.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI1_S2_Stand.Image = image1;
                            g1.Dispose();
                            if (bmp1 != null)
                                bmp1.Dispose();
                        }
                        lock (pictureBox_AVI1_S3_Stand)
                        {
                            g2.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI1_S3_Stand.Image = image2;
                            g2.Dispose();
                            if (bmp2 != null)
                                bmp2.Dispose();
                        }
                        #endregion
                        break;
                    case "2":
                        #region AVI2 PIC
                        S1_Image = Image.FromFile(GlobalVar.gl_val_Machine2_S1_TestVal + @"\Stand.bmp");
                        S2_Image = Image.FromFile(GlobalVar.gl_val_Machine2_S2_TestVal + @"\Stand.bmp");
                        S3_Image = Image.FromFile(GlobalVar.gl_val_Machine2_S3_TestVal + @"\Stand.bmp");
                        bmp = (Bitmap)new Bitmap(S1_Image.Width, S1_Image.Height);
                         bmp1 = (Bitmap)new Bitmap(S2_Image.Width, S2_Image.Height);
                         bmp2 = (Bitmap)new Bitmap(S3_Image.Width, S3_Image.Height);

                        GraphicsContext = BufferedGraphicsManager.Current;//双缓冲
                         myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI2_S1_Stand.CreateGraphics(), this.pictureBox_AVI2_S1_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            graphics.DrawImage(bmp, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI2_S2_Stand.CreateGraphics(), this.pictureBox_AVI2_S2_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp1.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
                            graphics.DrawImage(bmp1, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI2_S3_Stand.CreateGraphics(), this.pictureBox_AVI2_S3_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp1.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                            graphics.DrawImage(bmp2, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        image = new Bitmap(S1_Image);
                         image1 = new Bitmap(S2_Image);
                         image2 = new Bitmap(S3_Image);
                        g = Graphics.FromImage(image);
                         g1 = Graphics.FromImage(image1);
                         g2 = Graphics.FromImage(image2);
                        //加水印  
                        str = GlobalVar.gl_Barcode;
                         font = new Font("微软雅黑", image.Width / 85);

                         sbrush = new SolidBrush(Color.Red);
                        lock (pictureBox_AVI2_S1_Stand)
                        {
                            g.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI2_S1_Stand.Image = image;
                            g.Dispose();
                            if (bmp != null)
                                bmp.Dispose();
                            myBuffer.Dispose();
                        }
                        lock (pictureBox_AVI2_S2_Stand)
                        {
                            g1.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI2_S2_Stand.Image = image1;
                            g1.Dispose();
                            if (bmp1 != null)
                                bmp1.Dispose();
                        }
                        lock (pictureBox_AVI2_S3_Stand)
                        {
                            g2.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI2_S3_Stand.Image = image2;
                            g2.Dispose();
                            if (bmp2 != null)
                                bmp2.Dispose();
                        }
                        #endregion
                        break;
                    case "3":
                        #region AVI3 PIC
                        S1_Image = Image.FromFile(GlobalVar.gl_val_Machine3_S1_TestVal + @"\Stand.bmp");
                        S2_Image = Image.FromFile(GlobalVar.gl_val_Machine3_S2_TestVal + @"\Stand.bmp");
                        S3_Image = Image.FromFile(GlobalVar.gl_val_Machine3_S3_TestVal + @"\Stand.bmp");
                        bmp = (Bitmap)new Bitmap(S1_Image.Width, S1_Image.Height);
                        bmp1 = (Bitmap)new Bitmap(S2_Image.Width, S2_Image.Height);
                        bmp2 = (Bitmap)new Bitmap(S3_Image.Width, S3_Image.Height);

                        GraphicsContext = BufferedGraphicsManager.Current;//双缓冲
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI3_S1_Stand.CreateGraphics(), this.pictureBox_AVI3_S1_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            graphics.DrawImage(bmp, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI3_S2_Stand.CreateGraphics(), this.pictureBox_AVI3_S2_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp1.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);
                            graphics.DrawImage(bmp1, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        myBuffer = GraphicsContext.Allocate(this.pictureBox_AVI3_S3_Stand.CreateGraphics(), this.pictureBox_AVI3_S3_Stand.DisplayRectangle);
                        using (Graphics graphics = myBuffer.Graphics /*Graphics.FromImage((Bitmap)bmp1.Clone())*/)
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            Rectangle destRect = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                            Rectangle srcRect = new Rectangle(0, 0, bmp2.Width, bmp2.Height);
                            graphics.DrawImage(bmp2, destRect, srcRect, GraphicsUnit.Pixel);
                            graphics.Dispose();
                        }
                        image = new Bitmap(S1_Image);
                        image1 = new Bitmap(S2_Image);
                        image2 = new Bitmap(S3_Image);
                        g = Graphics.FromImage(image);
                        g1 = Graphics.FromImage(image1);
                        g2 = Graphics.FromImage(image2);
                        //加水印  
                        str = GlobalVar.gl_Barcode;
                        font = new Font("微软雅黑", image.Width / 85);

                        sbrush = new SolidBrush(Color.Red);
                        lock (pictureBox_AVI3_S1_Stand)
                        {
                            g.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI3_S1_Stand.Image = image;
                            g.Dispose();
                            if (bmp != null)
                                bmp.Dispose();
                            myBuffer.Dispose();
                        }
                        lock (pictureBox_AVI3_S2_Stand)
                        {
                            g1.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI3_S2_Stand.Image = image1;
                            g1.Dispose();
                            if (bmp1 != null)
                                bmp1.Dispose();
                        }
                        lock (pictureBox_AVI3_S3_Stand)
                        {
                            g2.DrawString(str, font, sbrush, new PointF(10, 10));
                            pictureBox_AVI3_S3_Stand.Image = image2;
                            g2.Dispose();
                            if (bmp2 != null)
                                bmp2.Dispose();
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            } catch(System.Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
            }
        }

        private void update_Index()
        {
            while (GlobalVar.gl_appIsRunning)
            {
                SearchFiles.Everything_UpdateAllFolderIndexes();
                Thread.Sleep(1000);
            }
        }
        AutoResetEvent query = new AutoResetEvent(false);
        private void tb_Barcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tb_Barcode.Text != "" && tb_Barcode.Text.Length >= GlobalVar.gl_BarcodeLength)
                {
                    GlobalVar.Pic_S1_fileName = "";//清空选中图片缓存    
                    GlobalVar.Pic_S2_fileName = "";
                    GlobalVar.Pic_S3_fileName = "";
                    GlobalVar.gl_Barcode = tb_Barcode.Text.Trim();
                    myf.searchImage();
                    if (GlobalVar.Pic_S1_fileName != "" && GlobalVar.Pic_S2_fileName != ""&&GlobalVar.Pic_S3_fileName!="")
                    {
                        pictureZoom_s1.PictureLocation = GlobalVar.Pic_S1_fileName;
                        pictureZoom_s2.PictureLocation = GlobalVar.Pic_S2_fileName;
                        pictureZoom_S3.PictureLocation = GlobalVar.Pic_S3_fileName;
                        //pictureBox_S1.Image = Image.FromFile(GlobalVar.Pic_S1_fileName);
                        //pictureBox_S2.Image = Image.FromFile(GlobalVar.Pic_S2_fileName);
                    }
                    else
                    {
                          MessageBox.Show("未查找到数据!");
                        tb_Barcode.Text = "";
                        return;
                    }
                    query.Set();
                    tb_Barcode.Text = "";
                }
            }catch(System.Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
            }
        }

        private void 设置路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config config = new Config();
            config.ShowDialog();
        }

        private void aVI1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePM avi1_change = new ChangePM("1");
            avi1_change.ShowDialog();
        }

        private void aVI2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePM avi1_change = new ChangePM("2");
            avi1_change.ShowDialog();
        }

        private void aVI3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePM avi1_change = new ChangePM("3");
            avi1_change.ShowDialog();
        }

        private void 分析良率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CountYeild yeild = new CountYeild();
            yeild.Show();
        }

        private void 更换品目ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (GlobalVar.gl_val_Machine1_Check == "0") aVI1ToolStripMenuItem.Visible = false;
            if (GlobalVar.gl_val_Machine2_Check == "0") aVI2ToolStripMenuItem.Visible = false;
            if (GlobalVar.gl_val_Machine3_Check == "0") aVI3ToolStripMenuItem.Visible = false;
        }

        private void 分析测试项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyseTestItems analysis = new AnalyseTestItems();
            analysis.Show();
        }

        private void aVI1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalyseTestItems analysis = new AnalyseTestItems("1");
            analysis.Show();
        }

        private void aVI2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalyseTestItems analysis = new AnalyseTestItems("2");
            analysis.Show();
        }

        private void aVI3ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AnalyseTestItems analysis = new AnalyseTestItems("3");
            analysis.Show();
        }

        private void 更新测试项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_Progress.Value = 0;
            if (GlobalVar.gl_val_Machine1_Check == "1")
            {
                select_Progress.Value += 40;
                myf.getTestItem(GlobalVar.gl_val_Machine1_S1_TestVal, GlobalVar.AVI1_S1_TestItems);//读取测试项
                myf.getTestItem(GlobalVar.gl_val_Machine1_S2_TestVal, GlobalVar.AVI1_S2_TestItems);
                myf.getTestItem(GlobalVar.gl_val_Machine1_S3_TestVal, GlobalVar.AVI1_S3_TestItems);

            }
            if (GlobalVar.gl_val_Machine2_Check == "1")
            {
                select_Progress.Value += 40;
                myf.getTestItem(GlobalVar.gl_val_Machine2_S1_TestVal, GlobalVar.AVI2_S1_TestItems);//读取测试项
                myf.getTestItem(GlobalVar.gl_val_Machine2_S2_TestVal, GlobalVar.AVI2_S2_TestItems);
                myf.getTestItem(GlobalVar.gl_val_Machine2_S3_TestVal, GlobalVar.AVI2_S3_TestItems);

            }
            if (GlobalVar.gl_val_Machine3_Check == "1")
            {
                select_Progress.Value += 40;
                myf.getTestItem(GlobalVar.gl_val_Machine3_S1_TestVal, GlobalVar.AVI3_S1_TestItems);//读取测试项
                myf.getTestItem(GlobalVar.gl_val_Machine3_S2_TestVal, GlobalVar.AVI3_S2_TestItems);
                myf.getTestItem(GlobalVar.gl_val_Machine3_S3_TestVal, GlobalVar.AVI3_S3_TestItems);

            }
                select_Progress.Value = 100;
        }
    }
}
