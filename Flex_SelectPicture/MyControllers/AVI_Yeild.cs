using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using Flex_SelectPicture.Forms;

namespace Flex_SelectPicture
{
    public partial class AVI_Yeild : UserControl
    {
        string AVI_No = string.Empty;
        int stationsCount = 0;
        Dictionary<string, string[]> S1_TestItem = new Dictionary<string, string[]>();//测试项信息
        Dictionary<string, string[]> S2_TestItem = new Dictionary<string, string[]>();
        Dictionary<string, string[]> S3_TestItem = new Dictionary<string, string[]>();
        Dictionary<string, string[]> S4_TestItem = new Dictionary<string, string[]>();//主站副
        Dictionary<string, int> S1_NGFail = new Dictionary<string, int>();//测试线NG情况
        Dictionary<string, int> S2_NGFail = new Dictionary<string, int>();
        Dictionary<string, int> S3_NGFail = new Dictionary<string, int>();
        Dictionary<string, int> S4_NGFail = new Dictionary<string, int>();//主站副
        List<Chart> chart_List_s1 = new List<Chart>();//工站图表
        List<Chart> chart_List_s2 = new List<Chart>();//工站图表
        List<Chart> chart_List_s3 = new List<Chart>();//从站副工站图表
        List<Chart> chart_List_s4 = new List<Chart>();//主站副工站图表
        Station[] stations_List = new Station[10] ;
        List<string> testResult = new List<string>();
        double yeild = 100;//良率
        public AVI_Yeild()
        {
            InitializeComponent();
        }
        public AVI_Yeild(string avi,int stationCount)
        {
            InitializeComponent();
            AVI_No = avi;
            stationsCount = stationCount;
            lb_AVI_NO.Text = AVI_No;
            lb_count.Text = stationCount.ToString();
            if (avi.Equals("AVI-1"))
            {
                S1_TestItem = GlobalVar.AVI1_S1_TestItems;
                S2_TestItem = GlobalVar.AVI1_S2_TestItems;
                S3_TestItem = GlobalVar.AVI1_S3_TestItems;
            }
            else if (avi.Equals("AVI-2"))
            {
                S1_TestItem = GlobalVar.AVI2_S1_TestItems;
                S2_TestItem = GlobalVar.AVI2_S2_TestItems;
                S3_TestItem = GlobalVar.AVI2_S3_TestItems;
            }
            else if (avi.Equals("AVI-3"))
            {
                S1_TestItem = GlobalVar.AVI3_S1_TestItems;
                S2_TestItem = GlobalVar.AVI3_S2_TestItems;
                S3_TestItem = GlobalVar.AVI3_S3_TestItems;
            }
            for (int i = 0; i < stationCount; i++)
            {
                Station _station = new Station();
                stations_List[i] = _station;
                foreach (string item in S1_TestItem.Keys)
                {
                    if (!_station.S1_NgFail.ContainsKey(item)) _station.S1_NgFail.Add(item, 0);
                }
                foreach (string item in S2_TestItem.Keys)
                {
                    if (!_station.S2_NgFail.ContainsKey(item)) _station.S2_NgFail.Add(item, 0);
                }
                foreach (string item in S3_TestItem.Keys)
                {
                    if (!_station.S3_NgFail.ContainsKey(item)) _station.S3_NgFail.Add(item, 0);
                }
            }
            MsgBox msg = new MsgBox(GlobalVar.gl_check_checkDate, avi);
            msg.ShowDialog();
            this.flowLayoutPanel_s1.MouseWheel += FlowLayoutPanel_s1_MouseWheel;
            this.flowLayoutPanel_s2.MouseWheel += FlowLayoutPanel_s2_MouseWheel;
            this.flowLayoutPanel_s3.MouseWheel += FlowLayoutPanel_s3_MouseWheel;
        }

        public void changeDate(string avi)
        {
            try
            {
                MsgBox msg = new MsgBox(GlobalVar.gl_check_checkDate, avi);
                msg.ShowDialog();
                GetAllTestResult();
            }
            catch { }
        }


        private void AVI_Yeild_Load(object sender, EventArgs e)
        {
            analy_Progress.Value = 0;
            InitialChart();//初始化图表
            analy_Progress.Value += 10;
            AddTestItemtoDictionary();
            analy_Progress.Value += 10;
            Thread TH_getTestResult = new Thread(GetAllTestResult);
            TH_getTestResult.IsBackground = true;
            TH_getTestResult.Start();
            
        }
        /// <summary>
        ///     获取所有测试数据
        /// </summary>
        public void GetAllTestResult()
        {
            try
            {
                if (AVI_No.Equals("AVI-1"))
                {
                    testResult = GlobalVar.yeild_check_AVI1_result;
                }
                else if (AVI_No.Equals("AVI-2"))
                {
                    testResult = GlobalVar.yeild_check_AVI2_result;
                }
                else if (AVI_No.Equals("AVI-3"))
                {
                    testResult = GlobalVar.yeild_check_AVI3_result;
                }
                FindResultByClass(testResult);//获得良率，并把测试NG项添加到dictionary中
                this.Invoke(new Action(()=> { if(analy_Progress.Value<100) analy_Progress.Value += 10; }));
                #region 排序
                var dicSort = from objDic in S1_NGFail orderby objDic.Value descending select objDic;
                var dicSorting = from objDic in S2_NGFail orderby objDic.Value descending select objDic;
                var dicSorting_s3 = from objDic in S3_NGFail orderby objDic.Value descending select objDic;

                List<string> x = new List<string>();
                List<int> y = new List<int>();
                List<double> rate = new List<double>();
                List<string> x1 = new List<string>();
                List<int> y1 = new List<int>();
                List<double> rate1 = new List<double>();
                List<string> x2 = new List<string>();
                List<int> y2 = new List<int>();
                List<double> rate2 = new List<double>();
                int tmp = 0;
                foreach (KeyValuePair<string, int> kv in dicSort)
                {
                    if (tmp < 20)
                    {
                        x.Add(kv.Key);
                        y.Add(kv.Value); double r = Math.Round((double)kv.Value / (double)(total), 4);
                        rate.Add(r * 100);
                        tmp++;
                    }
                    else { break; }
                }
                tmp = 0;
                foreach (KeyValuePair<string, int> kv in dicSorting)
                {
                    if (tmp < 20)
                    {
                        x1.Add(kv.Key);
                        y1.Add(kv.Value); double r = Math.Round((double)kv.Value / (double)(total), 4);
                        rate1.Add(r * 100);
                        tmp++;
                    }
                    else break;
                }
                tmp = 0;
                foreach (KeyValuePair<string, int> kv in dicSorting_s3)
                {
                    if (tmp < 20)
                    {
                        x2.Add(kv.Key);
                        y2.Add(kv.Value);
                        double r = Math.Round((double)kv.Value/(double)(total),4);
                        rate2.Add(r*100);
                        tmp++;
                    }
                    else break;
                }
                #endregion
                this.Invoke(new Action(() => { if (analy_Progress.Value < 100) analy_Progress.Value += 10; }));
                DrawChart(x.ToArray(), y.ToArray(),rate.ToArray(),x1.ToArray(), y1.ToArray(), rate1.ToArray(),x2.ToArray(), y2.ToArray(),rate2.ToArray());
                this.Invoke(new Action(() => { if (analy_Progress.Value < 100) analy_Progress.Value += 10; }));
                DrawStationChart(stations_List);
                this.Invoke(new Action(() => { analy_Progress.Value = 100; }));
            }
            catch (Exception ex) { }
        }

        private void DrawStationChart(Station[] stations_List)
        {
            for(int i = 0; i < stationsCount; i++)
            {
                var dicSort = from objDic in stations_List[i].S1_NgFail orderby objDic.Value descending select objDic;
                var dicSorting = from objDic in stations_List[i].S2_NgFail orderby objDic.Value descending select objDic;
                var dicSorting_s3 = from objDic in stations_List[i].S3_NgFail orderby objDic.Value descending select objDic;

                List<string> x = new List<string>();
                List<int> y = new List<int>();
                List<string> x1 = new List<string>();
                List<int> y1 = new List<int>();
                List<string> x2 = new List<string>();
                List<int> y2 = new List<int>();
                int tmp = 0;
                foreach (KeyValuePair<string, int> kv in dicSort)
                {
                    if (tmp < 10)
                    {
                        x.Add(kv.Key);
                        y.Add(kv.Value);
                        tmp++;
                    }
                    else { break; }
                }
                tmp = 0;
                foreach (KeyValuePair<string, int> kv in dicSorting)
                {
                    if (tmp < 10)
                    {
                        x1.Add(kv.Key);
                        y1.Add(kv.Value);
                        tmp++;
                    }
                    else break;
                }
                tmp = 0;
                foreach (KeyValuePair<string, int> kv in dicSorting_s3)
                {
                    if (tmp < 10)
                    {
                        x2.Add(kv.Key);
                        y2.Add(kv.Value);
                        tmp++;
                    }
                    else break;
                }
                #region 绘制主站频率图
                //绑定数据
                chart_List_s1[i].Series[0].Points.DataBindXY(x, y);
                chart_List_s1[i].Series[0].Points[0].Color = Color.Black;
                chart_List_s1[i].Series[0].Palette = ChartColorPalette.Bright;
                #endregion
                #region 绘制从站频率图
                //绑定数据
                chart_List_s2[i].Series[0].Points.DataBindXY(x1, y1);
                chart_List_s2[i].Series[0].Points[0].Color = Color.Black;
                chart_List_s2[i].Series[0].Palette = ChartColorPalette.Bright;
                #endregion
                #region 绘制从站副频率图
                //绑定数据
                chart_List_s3[i].Series[0].Points.DataBindXY(x2, y2);
                chart_List_s3[i].Series[0].Points[0].Color = Color.Black;
                chart_List_s3[i].Series[0].Palette = ChartColorPalette.Bright;
                #endregion

            }
        }


        /// <summary>
        ///     绘制图表
        /// </summary>
        private void DrawChart(string[] x, int[] y,double[] rate, string[] x1, int[] y1, double[] rate1,string[] x2, int[] y2,double[] rate2)
        {
            BeginInvoke(new Action(() =>
            {               
                #region 绘制主站频率图
                //绑定数据
                chart_S1.Series[0].Points.DataBindXY(x, y);
                chart_S1.Series[1].Points.DataBindY(rate);
                chart_S1.Series[0].Points[0].Color = Color.Black;
                chart_S1.Series[0].Palette = ChartColorPalette.Bright;
                #endregion
                #region 绘制从站频率图
                //绑定数据
                chart_S2.Series[1].Points.DataBindY(rate1);
                chart_S2.Series[0].Points.DataBindXY(x1, y1);
                chart_S2.Series[0].Points[0].Color = Color.Black;
                chart_S2.Series[0].Palette = ChartColorPalette.Bright;
                #endregion
                #region 绘制从站频率图
                //绑定数据
                chart_S3.Series[1].Points.DataBindY(rate2);
                chart_S3.Series[0].Points.DataBindXY(x2, y2);
                chart_S3.Series[0].Points[0].Color = Color.Black;
                chart_S3.Series[0].Palette = ChartColorPalette.Bright;
                #endregion
                if (yeild < 90 && yeild > 80)
                {
                    lb_Yeild.ForeColor = Color.Orange;
                }
                else if (yeild < 80 &&yeild > 60)
                {
                    lb_Yeild.ForeColor = Color.OrangeRed;
                }
                else if (yeild < 60)
                {

                    lb_Yeild.ForeColor = Color.Red;
                }
            }));
        }


                #region 变量
                int total = 0;
                int pass = 0;
                int fail = 0;//定义测试数量变量
                #endregion
        private void FindResultByClass(List<string> result)
        {
            try
            {
                 total = 0;
                 pass = 0;
                 fail = 0;
                if (testResult.Count > 0)
                {
                    foreach (string item in result)
                    {
                        string[] arrayLine = item.Split('|');
                        //string test = arrayLine[2] + "|" + arrayLine[3];//测试时间以及结果简报
                        if (arrayLine[0] != "\\" && arrayLine[0] != "NoBarC" && arrayLine[0].Length > 10)
                        {//只记录有barcode的测试结果
                            for (int i = 0; i < stationsCount; i++)
                            {
                                if ((i + 1).ToString().Equals(arrayLine[3].Substring(0, 1)))
                                {
                                    stations_List[i].total++;
                                    break;
                                }
                            }
                            total += 1;//总数加1s
                            if (arrayLine[2].Contains("OK"))
                            {
                                pass += 1;
                                for (int i = 0; i < stationsCount; i++)
                                {
                                    if ((i + 1).ToString().Equals(arrayLine[3].Substring(0, 1)))
                                    {
                                        stations_List[i].pass++;
                                        break;
                                    }
                                }
                            }
                            else if (arrayLine[2].Contains("NG"))
                            {
                                fail += 1;
                                
                                AddResultToDic(arrayLine);
                                AddResultToStationDics(arrayLine);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("当前无测试数据!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                yeild = (float)(Convert.ToDouble(pass) / Convert.ToDouble(total)) * 100;
                foreach(Station s in stations_List)
                {
                    if(s!=null)
                    s.station_yeild = (float)(Convert.ToDouble(s.pass) / Convert.ToDouble(s.total)) * 100;
                }
                BeginInvoke(new Action(() =>
                {
                    lb_Yeild.Text = yeild.ToString("00.00") + "%";
                    lb_Num.Text = total.ToString();
                    lb_NG.Text = (total - pass).ToString();
                    for (int i = 0; i < stationsCount; i++)
                    {
                        if (chart_List_s1[i].Titles.Count>2) chart_List_s1[i].Titles[2].Text = "工站良率：" + stations_List[i].station_yeild + " %";
                        else chart_List_s1[i].Titles.Add("工站良率：" + stations_List[i].station_yeild.ToString("00.000") + " %");
                        chart_List_s1[i].Titles[2].ForeColor = Color.Black;
                        chart_List_s1[i].Titles[2].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
                        chart_List_s1[i].Titles[2].Alignment = ContentAlignment.TopRight;
                        if (chart_List_s2[i].Titles.Count >2) chart_List_s2[i].Titles[2].Text = "工站良率：" + stations_List[i].station_yeild + " %";
                        else chart_List_s2[i].Titles.Add("工站良率：" + stations_List[i].station_yeild.ToString("00.000") + " %");
                        chart_List_s2[i].Titles[2].ForeColor = Color.Black;
                        chart_List_s2[i].Titles[2].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
                        chart_List_s2[i].Titles[2].Alignment = ContentAlignment.TopRight;
                    }
                }));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void AddResultToStationDics(string[] result)
        {
            string station = result[3].Substring(0, 1);//工位号
            //结果
            string[] s11_result = result[4].Split('，');
            string[] s12_result = result[5].Split('，');
            string[] s21_result = result[6].Split('，');
            string[] s22_result = result[7].Split('，');
            if (s11_result.Length > 0)
            {
                foreach (string item in s11_result)
                {
                    for (int i = 0; i < stationsCount; i++)
                    {
                        if ((i + 1).ToString().Equals(station))
                        {
                            if (stations_List[i].S1_NgFail.ContainsKey(item))
                                stations_List[i].S1_NgFail[item] += 1;
                        }
                    }
                }
            }
            if (s12_result.Length > 0)
            {
                foreach (string item in s12_result)
                {
                    for (int i = 0; i < stationsCount; i++)
                    {
                        if ((i + 1).ToString().Equals(station))
                        {
                            if (stations_List[i].S4_NgFail.ContainsKey(item))
                                stations_List[i].S4_NgFail[item] += 1;
                        }
                    }
                }
            }
            if (s21_result.Length > 0)
            {
                foreach (string item in s21_result)
                {
                    for (int i = 0; i < stationsCount; i++)
                    {
                        if ((i + 1).ToString().Equals(station))
                        {
                            if (stations_List[i].S2_NgFail.ContainsKey(item))
                                stations_List[i].S2_NgFail[item] += 1;
                        }
                    }
                }
            }
            if (s22_result.Length > 0)
            {
                foreach (string item in s22_result)
                {
                    for (int i = 0; i < stationsCount; i++)
                    {
                        if ((i + 1).ToString().Equals(station))
                        {
                            if (stations_List[i].S3_NgFail.ContainsKey(item))
                                stations_List[i].S3_NgFail[item] += 1;
                        }
                    }
                }
            }
            #region old disabled
            //if (arrayResult[0].Contains("主"))//处理主站测试NG项
            //{
            //    if (arrayResult.Length > 2)
            //    {
            //        foreach (string item in arrayResult[1].Split('，'))//主站
            //        {
            //            for (int i = 0; i < stationsCount; i++)
            //            {
            //                if ((i + 1).ToString().Equals(station))
            //                {
            //                    if (stations_List[i].S1_NgFail.ContainsKey(item))
            //                        stations_List[i].S1_NgFail[item] += 1;
            //                }
            //            }

            //        }

            //        foreach (string item in arrayResult[2].Split('，'))
            //        {
            //            for (int i = 0; i < stationsCount; i++)
            //            {
            //                if ((i + 1).ToString().Equals(station))
            //                {
            //                    if (stations_List[i].S2_NgFail.ContainsKey(item))
            //                        stations_List[i].S2_NgFail[item] += 1;
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (arrayResult.Length > 0)
            //    {
            //        foreach (string item in arrayResult[2].Split('，'))
            //        {
            //            for (int i = 0; i < stationsCount; i++)
            //            {
            //                if ((i + 1).ToString().Equals(station))
            //                {
            //                    if (stations_List[i].S2_NgFail.ContainsKey(item))
            //                        stations_List[i].S2_NgFail[item] += 1;
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion


        }




        /// <summary>
        ///     统计NG项的频率
        /// </summary>
        /// <param name="result" type="string">
        ///     <para>
        ///         测试结果
        ///     </para>
        /// </param>
        private void AddResultToDic(string[] result)
        {
            if (result.Length > 7)
            {
                string[] s11_result = result[4].Split('，');
                string[] s12_result = result[5].Split('，');
                string[] s21_result = result[6].Split('，');
                string[] s22_result = result[7].Split('，');
                if (s11_result.Length > 0)
                {
                    foreach (string item in s11_result)
                    {
                        if (S1_NGFail.ContainsKey(item))
                        {
                            S1_NGFail[item] += 1;
                        }
                    }
                }
                if (s12_result.Length > 0)
                {
                    foreach (string item in s12_result)
                    {
                        if (S4_NGFail.ContainsKey(item))
                        {
                            S4_NGFail[item] += 1;
                        }
                    }
                }
                if (s21_result.Length > 0)
                {
                    foreach (string item in s21_result)
                    {
                        if (S2_NGFail.ContainsKey(item))
                        {
                            S2_NGFail[item] += 1;
                        }
                    }
                }
                if (s22_result.Length > 0)
                {
                    foreach (string item in s22_result)
                    {
                        if (S3_NGFail.ContainsKey(item))
                        {
                            S3_NGFail[item] += 1;
                        }
                    }
                }
            }
            #region 旧的不用
            //if (arrayResult[0].Contains("主"))//处理主站测试NG项
            //{
            //    if (arrayResult.Length > 3)
            //    {
            //        foreach (string item in arrayResult[1].Split('，'))//主站
            //        {

            //            if (S1_NGFail.ContainsKey(item))
            //            {
            //                S1_NGFail[item] += 1;
            //            }
            //        }

            //        foreach (string item in arrayResult[2].Split('，'))
            //        {
            //            if (S2_NGFail.ContainsKey(item))
            //            {
            //                S2_NGFail[item] += 1;
            //            }
            //        }
            //        foreach (string item in arrayResult[2].Split('，'))
            //        {
            //            if (S2_NGFail.ContainsKey(item))
            //            {
            //                S2_NGFail[item] += 1;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (arrayResult.Length > 0)
            //    {
            //        foreach (string item in arrayResult[2].Split('，'))
            //        {
            //            if (S2_NGFail.ContainsKey(item))
            //            {
            //                S2_NGFail[item] += 1;
            //            }
            //        }
            //    }
            //}
            #endregion
        }


        /// <summary>
        ///     将测试项加载到dictionary中
        /// </summary>
        private void AddTestItemtoDictionary()//?是否有重复的测试项名称
        {
            try
            {
                S1_NGFail.Clear();
                S2_NGFail.Clear();
                S3_NGFail.Clear();
                foreach (string item in S1_TestItem.Keys)
                {
                    if (!S1_NGFail.ContainsKey(item))
                    {
                        S1_NGFail.Add(item, 0);
                    }
                }
                foreach (string item in S2_TestItem.Keys)
                {
                    if (!S2_NGFail.ContainsKey(item))
                    {
                        S2_NGFail.Add(item, 0);
                    }
                }
                foreach (string item in S3_TestItem.Keys)
                {
                    if (!S3_NGFail.ContainsKey(item))
                    {
                        S3_NGFail.Add(item, 0);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void InitialChart()
        {
            #region 主站chart
            //标题
            chart_S1.Titles.Add("主站测试项NG数据分析");
            chart_S1.Titles[0].ForeColor = Color.Black;
            chart_S1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart_S1.Titles[0].Alignment = ContentAlignment.TopCenter;
            chart_S1.Titles.Add("合计：" + S1_TestItem.Count + " 项,统计前十项");
            chart_S1.Titles[1].ForeColor = Color.Black;
            chart_S1.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            chart_S1.Titles[1].Alignment = ContentAlignment.TopRight;

            //控件背景
            chart_S1.BackColor = Color.Transparent;
            //图表区背景
            chart_S1.ChartAreas[0].BackColor = Color.Transparent;
            chart_S1.ChartAreas[0].BorderColor = Color.Transparent;
            //X轴标签间距
            chart_S1.ChartAreas[0].AxisX.Interval = 1;
            chart_S1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chart_S1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart_S1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            chart_S1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

            //X坐标轴颜色
            chart_S1.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
            chart_S1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart_S1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //X轴网络线条
            chart_S1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart_S1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart_S1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart_S1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            chart_S1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart_S1.ChartAreas[0].AxisY.Title = "数量(项)";
            chart_S1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart_S1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chart_S1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart_S1.ChartAreas[0].AxisY.ToolTip = "数量(项)";
            //Y轴网格线条
            chart_S1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart_S1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart_S1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart_S1.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            Legend legend = new Legend("legend");
            legend.Title = "legendTitle";

            chart_S1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart_S1.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart_S1.Series[0].LabelForeColor = Color.Black;
            chart_S1.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


            chart_S1.Series[0].Color = Color.Lime;
            chart_S1.Series[0].LegendText = legend.Name;
            chart_S1.Series[0].IsValueShownAsLabel = true;
            chart_S1.Series[0].LabelForeColor = Color.Black;
            chart_S1.Series[0].CustomProperties = "DrawingStyle = Cylinder";

            chart_S1.Series[1].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart_S1.Series[1].Label = "#VAL";                //设置显示X Y的值    
            chart_S1.Series[1].LabelForeColor = Color.Black;
            chart_S1.Series[1].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


            chart_S1.Series[1].Color = Color.Lime;
            chart_S1.Series[1].LegendText = legend.Name;
            chart_S1.Series[1].IsValueShownAsLabel = true;
            chart_S1.Series[1].LabelForeColor = Color.Black;
            chart_S1.Series[1].CustomProperties = "DrawingStyle = Cylinder";
            chart_S1.Legends.Add(legend);
            chart_S1.Legends[0].Position.Auto = false;
            #endregion
            #region 从站chart 
            //标题
            chart_S2.Titles.Add("从站主测试项NG数据分析");
            chart_S2.Titles[0].ForeColor = Color.Black;
            chart_S2.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart_S2.Titles[0].Alignment = ContentAlignment.TopCenter;
            chart_S2.Titles.Add("合计：" + S2_TestItem.Count + " 项,统计前十项");
            chart_S2.Titles[1].ForeColor = Color.Black;
            chart_S2.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            chart_S2.Titles[1].Alignment = ContentAlignment.TopRight;

            //控件背景
            chart_S2.BackColor = Color.Transparent;
            //图表区背景
            chart_S2.ChartAreas[0].BackColor = Color.Transparent;
            chart_S2.ChartAreas[0].BorderColor = Color.Transparent;
            //X轴标签间距
            chart_S2.ChartAreas[0].AxisX.Interval = 1;
            chart_S2.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chart_S2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart_S2.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            chart_S2.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

            //X坐标轴颜色
            chart_S2.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
            chart_S2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart_S2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //X轴网络线条
            chart_S2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart_S2.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart_S2.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart_S2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            chart_S2.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart_S2.ChartAreas[0].AxisY.Title = "数量(项)";
            chart_S2.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart_S2.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chart_S2.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart_S2.ChartAreas[0].AxisY.ToolTip = "数量(项)";
            //Y轴网格线条
            chart_S2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart_S2.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart_S2.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart_S2.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            //Legend legend = new Legend("legend");
            //legend.Title = "legendTitle";

            chart_S2.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart_S2.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart_S2.Series[0].LabelForeColor = Color.Black;
            chart_S2.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


            chart_S2.Series[0].Color = Color.Lime;
            chart_S2.Series[0].LegendText = legend.Name;
            chart_S2.Series[0].IsValueShownAsLabel = true;
            chart_S2.Series[0].LabelForeColor = Color.Black;
            chart_S2.Series[0].CustomProperties = "DrawingStyle = Cylinder";

            chart_S2.Series[1].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart_S2.Series[1].Label = "#VAL";                //设置显示X Y的值    
            chart_S2.Series[1].LabelForeColor = Color.Black;
            chart_S2.Series[1].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


            chart_S2.Series[1].Color = Color.Lime;
            chart_S2.Series[1].LegendText = legend.Name;
            chart_S2.Series[1].IsValueShownAsLabel = true;
            chart_S2.Series[1].LabelForeColor = Color.Black;
            chart_S2.Series[1].CustomProperties = "DrawingStyle = Cylinder";
            chart_S2.Legends.Add(legend);
            chart_S2.Legends[0].Position.Auto = false;
            #endregion
            #region 从站2chart 
            //标题
            chart_S3.Titles.Add("从站副测试项NG数据分析");
            chart_S3.Titles[0].ForeColor = Color.Black;
            chart_S3.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart_S3.Titles[0].Alignment = ContentAlignment.TopCenter;
            chart_S3.Titles.Add("合计：" + S3_TestItem.Count + " 项,统计前十项");
            chart_S3.Titles[1].ForeColor = Color.Black;
            chart_S3.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            chart_S3.Titles[1].Alignment = ContentAlignment.TopRight;

            //控件背景
            chart_S3.BackColor = Color.Transparent;
            //图表区背景
            chart_S3.ChartAreas[0].BackColor = Color.Transparent;
            chart_S3.ChartAreas[0].BorderColor = Color.Transparent;
            //X轴标签间距
            chart_S3.ChartAreas[0].AxisX.Interval = 1;
            chart_S3.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chart_S3.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart_S3.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            chart_S3.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

            //X坐标轴颜色
            chart_S3.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
            chart_S3.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart_S3.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //X轴网络线条
            chart_S3.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart_S3.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart_S3.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart_S3.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            chart_S3.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart_S3.ChartAreas[0].AxisY.Title = "数量(项)";
            chart_S3.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart_S3.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chart_S3.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart_S3.ChartAreas[0].AxisY.ToolTip = "数量(项)";
            //Y轴网格线条
            chart_S3.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart_S3.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart_S3.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart_S3.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            //Legend legend = new Legend("legend");
            //legend.Title = "legendTitle";

            chart_S3.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart_S3.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart_S3.Series[0].LabelForeColor = Color.Black;
            chart_S3.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


            chart_S3.Series[0].Color = Color.Lime;
            chart_S3.Series[0].LegendText = legend.Name;
            chart_S3.Series[0].IsValueShownAsLabel = true;
            chart_S3.Series[0].LabelForeColor = Color.Black;
            chart_S3.Series[0].CustomProperties = "DrawingStyle = Cylinder";

            chart_S3.Series[1].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart_S3.Series[1].Label = "#VAL";                //设置显示X Y的值    
            chart_S3.Series[1].LabelForeColor = Color.Black;
            chart_S3.Series[1].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


            chart_S3.Series[1].Color = Color.Lime;
            chart_S3.Series[1].LegendText = legend.Name;
            chart_S3.Series[1].IsValueShownAsLabel = true;
            chart_S3.Series[1].LabelForeColor = Color.Black;
            chart_S3.Series[1].CustomProperties = "DrawingStyle = Cylinder";
            chart_S3.Legends.Add(legend);
            chart_S3.Legends[0].Position.Auto = false;
            #endregion
            for (int i = 1; i <= stationsCount; i++)
            {
                string chart = "station_" + i.ToString() + "_s1";
                Chart chart_ = new Chart();
                chart_.Width = 860;
                chart_.Height = 230;
                chart_.Name = chart;
                #region init chart
                //标题
                chart_.Titles.Add("工站" + chart + "测试项NG数据分析");
                chart_.Titles[0].ForeColor = Color.Black;
                chart_.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
                chart_.Titles[0].Alignment = ContentAlignment.TopCenter;
                chart_.Titles.Add("合计：" + S1_TestItem.Count + " 项,统计前十项");
                chart_.Titles[1].ForeColor = Color.Black;
                chart_.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
                chart_.Titles[1].Alignment = ContentAlignment.TopRight;

                //控件背景
                chart_.BackColor = Color.Transparent;
                ChartArea area = new ChartArea();
                chart_.ChartAreas.Add(area);
                //图表区背景
                chart_.ChartAreas[0].BackColor = Color.Transparent;
                chart_.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                chart_.ChartAreas[0].AxisX.Interval = 1;
                chart_.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                chart_.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart_.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
                chart_.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

                //X坐标轴颜色
                chart_.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
                chart_.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart_.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
                //X轴网络线条
                chart_.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart_.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                //Y坐标轴颜色
                chart_.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
                chart_.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart_.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
                //Y坐标轴标题
                chart_.ChartAreas[0].AxisY.Title = "数量(项)";
                chart_.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
                chart_.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chart_.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                chart_.ChartAreas[0].AxisY.ToolTip = "数量(项)";
                //Y轴网格线条
                chart_.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chart_.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                chart_.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
                chart_.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                legend = new Legend("legend");
                legend.Title = "legendTitle";

                Series s = new Series();
                chart_.Series.Add(s);
                chart_.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
                chart_.Series[0].Label = "#VAL";                //设置显示X Y的值    
                chart_.Series[0].LabelForeColor = Color.Black;
                chart_.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


                chart_.Series[0].Color = Color.Lime;
                chart_.Series[0].LegendText = legend.Name;
                chart_.Series[0].IsValueShownAsLabel = true;
                chart_.Series[0].LabelForeColor = Color.Black;
                chart_.Series[0].CustomProperties = "DrawingStyle = Cylinder";
                chart_.Legends.Add(legend);
                chart_.Legends[0].Position.Auto = false;
                #endregion
                flowLayoutPanel_s1.Controls.Add(chart_);
                chart_List_s1.Add(chart_);


                 chart = "station_" + i.ToString() + "_s2";
                 chart_ = new Chart();

                 s = new Series();
                chart_.Series.Add(s);
                 area = new ChartArea();
                chart_.ChartAreas.Add(area);
                chart_.Width = 860;
                chart_.Height = 230;
                chart_.Name = chart;
                #region init chart
                //标题
                chart_.Titles.Add("工站" + chart + "测试项NG数据分析");
                chart_.Titles[0].ForeColor = Color.Black;
                chart_.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
                chart_.Titles[0].Alignment = ContentAlignment.TopCenter;
                chart_.Titles.Add("合计：" + S2_TestItem.Count + " 项,统计前十项");
                chart_.Titles[1].ForeColor = Color.Black;
                chart_.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
                chart_.Titles[1].Alignment = ContentAlignment.TopRight;

                //控件背景
                chart_.BackColor = Color.Transparent;
                //图表区背景
                chart_.ChartAreas[0].BackColor = Color.Transparent;
                chart_.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                chart_.ChartAreas[0].AxisX.Interval = 1;
                chart_.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                chart_.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart_.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
                chart_.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

                //X坐标轴颜色
                chart_.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
                chart_.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart_.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
                //X轴网络线条
                chart_.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart_.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                //Y坐标轴颜色
                chart_.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
                chart_.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart_.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
                //Y坐标轴标题
                chart_.ChartAreas[0].AxisY.Title = "数量(项)";
                chart_.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
                chart_.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chart_.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                chart_.ChartAreas[0].AxisY.ToolTip = "数量(项)";
                //Y轴网格线条
                chart_.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chart_.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                chart_.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
                chart_.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                legend = new Legend("legend");
                legend.Title = "legendTitle";

                chart_.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
                chart_.Series[0].Label = "#VAL";                //设置显示X Y的值    
                chart_.Series[0].LabelForeColor = Color.Black;
                chart_.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


                chart_.Series[0].Color = Color.Lime;
                chart_.Series[0].LegendText = legend.Name;
                chart_.Series[0].IsValueShownAsLabel = true;
                chart_.Series[0].LabelForeColor = Color.Black;
                chart_.Series[0].CustomProperties = "DrawingStyle = Cylinder";
                chart_.Legends.Add(legend);
                chart_.Legends[0].Position.Auto = false;
                #endregion
                flowLayoutPanel_s2.Controls.Add(chart_);
                chart_List_s2.Add(chart_);

                chart = "station_" + i.ToString() + "_s3";
                chart_ = new Chart();

                s = new Series();
                chart_.Series.Add(s);
                area = new ChartArea();
                chart_.ChartAreas.Add(area);
                chart_.Width = 860;
                chart_.Height = 230;
                chart_.Name = chart;
                #region init chart
                //标题
                chart_.Titles.Add("工站" + chart + "测试项NG数据分析");
                chart_.Titles[0].ForeColor = Color.Black;
                chart_.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
                chart_.Titles[0].Alignment = ContentAlignment.TopCenter;
                chart_.Titles.Add("合计：" + S2_TestItem.Count + " 项,统计前十项");
                chart_.Titles[1].ForeColor = Color.Black;
                chart_.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
                chart_.Titles[1].Alignment = ContentAlignment.TopRight;

                //控件背景
                chart_.BackColor = Color.Transparent;
                //图表区背景
                chart_.ChartAreas[0].BackColor = Color.Transparent;
                chart_.ChartAreas[0].BorderColor = Color.Transparent;
                //X轴标签间距
                chart_.ChartAreas[0].AxisX.Interval = 1;
                chart_.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
                chart_.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart_.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
                chart_.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

                //X坐标轴颜色
                chart_.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
                chart_.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart_.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
                //X轴网络线条
                chart_.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart_.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                //Y坐标轴颜色
                chart_.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
                chart_.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart_.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
                //Y坐标轴标题
                chart_.ChartAreas[0].AxisY.Title = "数量(项)";
                chart_.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
                chart_.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                chart_.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
                chart_.ChartAreas[0].AxisY.ToolTip = "数量(项)";
                //Y轴网格线条
                chart_.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chart_.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

                chart_.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
                chart_.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
                legend = new Legend("legend");
                legend.Title = "legendTitle";

                chart_.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
                chart_.Series[0].Label = "#VAL";                //设置显示X Y的值    
                chart_.Series[0].LabelForeColor = Color.Black;
                chart_.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值


                chart_.Series[0].Color = Color.Lime;
                chart_.Series[0].LegendText = legend.Name;
                chart_.Series[0].IsValueShownAsLabel = true;
                chart_.Series[0].LabelForeColor = Color.Black;
                chart_.Series[0].CustomProperties = "DrawingStyle = Cylinder";
                chart_.Legends.Add(legend);
                chart_.Legends[0].Position.Auto = false;
                #endregion
                flowLayoutPanel_s3.Controls.Add(chart_);
                chart_List_s3.Add(chart_);
            }
        }


        #region 鼠标滚动事件
        private void FlowLayoutPanel_s2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (this.flowLayoutPanel_s2.VerticalScroll.Maximum > this.flowLayoutPanel_s2.VerticalScroll.Value + 10)
                    this.flowLayoutPanel_s2.VerticalScroll.Value += 10;
                else
                    this.flowLayoutPanel_s2.VerticalScroll.Value = this.flowLayoutPanel_s2.VerticalScroll.Maximum;
            }
            else
            {
                if (this.flowLayoutPanel_s2.VerticalScroll.Value >10)
                    this.flowLayoutPanel_s2.VerticalScroll.Value -= 10;
                else
                {
                    this.flowLayoutPanel_s2.VerticalScroll.Value = 0;
                }
            }
        }

        private void FlowLayoutPanel_s1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (this.flowLayoutPanel_s1.VerticalScroll.Maximum > this.flowLayoutPanel_s1.VerticalScroll.Value + 10)
                    this.flowLayoutPanel_s1.VerticalScroll.Value += 10;
                else
                    this.flowLayoutPanel_s1.VerticalScroll.Value = this.flowLayoutPanel_s1.VerticalScroll.Maximum;
            }
            else
            {
                if (this.flowLayoutPanel_s1.VerticalScroll.Value > 10)
                    this.flowLayoutPanel_s1.VerticalScroll.Value -= 10;
                else
                {
                    this.flowLayoutPanel_s1.VerticalScroll.Value = 0;
                }
            }
        }
        private void FlowLayoutPanel_s3_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (this.flowLayoutPanel_s3.VerticalScroll.Maximum > this.flowLayoutPanel_s3.VerticalScroll.Value + 10)
                    this.flowLayoutPanel_s3.VerticalScroll.Value += 10;
                else
                    this.flowLayoutPanel_s3.VerticalScroll.Value = this.flowLayoutPanel_s3.VerticalScroll.Maximum;
            }
            else
            {
                if (this.flowLayoutPanel_s3.VerticalScroll.Value > 10)
                    this.flowLayoutPanel_s3.VerticalScroll.Value -= 10;
                else
                {
                    this.flowLayoutPanel_s3.VerticalScroll.Value = 0;
                }
            }
        }

        private void flowLayoutPanel_s2_MouseClick(object sender, MouseEventArgs e)
        {
            this.flowLayoutPanel_s2.Focus();
        }

        private void flowLayoutPanel_s1_MouseClick(object sender, MouseEventArgs e)
        {
            this.flowLayoutPanel_s1.Focus();
        }

        private void flowLayoutPanel_s1_MouseEnter(object sender, EventArgs e)
        {
            this.flowLayoutPanel_s1.Focus();
        }

        private void flowLayoutPanel_s2_MouseEnter(object sender, EventArgs e)
        {
            this.flowLayoutPanel_s2.Focus();
        }

        private void flowLayoutPanel_s3_MouseClick(object sender, MouseEventArgs e)
        {
            this.flowLayoutPanel_s3.Focus();
        }
        private void flowLayoutPanel_s3_MouseEnter(object sender, EventArgs e)
        {
            this.flowLayoutPanel_s3.Focus();
        }
        #endregion

    }
    class Station
    {
       public Dictionary<string, int> S1_NgFail = new Dictionary<string, int>();
        public Dictionary<string, int> S2_NgFail = new Dictionary<string, int>();
        public Dictionary<string, int> S3_NgFail = new Dictionary<string, int>();
        public Dictionary<string, int> S4_NgFail = new Dictionary<string, int>();//主站副
        public double station_yeild=100;
        public int pass;
        public int total;
    }
}
