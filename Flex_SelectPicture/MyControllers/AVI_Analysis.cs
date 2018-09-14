using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Threading;

namespace Flex_SelectPicture
{
    public partial class AVI_Analysis : UserControl
    {
        private string AVI;
        private List<string> testItems = new List<string>();
        DataTable analysisData = new DataTable();
        DataTable temp = new DataTable();
        DateTime analysisdate = new DateTime();
        string path_TestResult;
        private bool wetherExportPic = false;//是否导出图片，默认为false不导出
        MyFunctions myf = new MyFunctions();
        private string testItem_Select = "";


        List<double> result_list = new List<double>();
        Series series;

        public AVI_Analysis()
        {
            InitializeComponent();
            if (wetherExportPic) lb_ExportPic.Text = "是";
            else lb_ExportPic.Text = "否";
        }
        public AVI_Analysis(string AVI, DateTime analysisDate)
        {
            this.AVI = AVI;
            this.analysisdate = analysisDate;
            InitializeComponent();
            switch (AVI)
            {
                case "1":
                    lb_S1TestItems.Text = GlobalVar.AVI1_S1_TestItems.Count.ToString() + "项";
                    lb_S2TestItems.Text = GlobalVar.AVI1_S2_TestItems.Count.ToString() + "项";
                    break;
                case "2":
                    lb_S1TestItems.Text = GlobalVar.AVI2_S1_TestItems.Count.ToString() + "项";
                    lb_S2TestItems.Text = GlobalVar.AVI2_S2_TestItems.Count.ToString() + "项";
                    break;
                case "3":
                    lb_S1TestItems.Text = GlobalVar.AVI3_S1_TestItems.Count.ToString() + "项";
                    lb_S2TestItems.Text = GlobalVar.AVI3_S2_TestItems.Count.ToString() + "项";
                    break;
                default:
                    break;
            }
            progress_Analysis.Value = 0;
            textBox_testItem.Text = "";
            progress_Analysis.Value += 10;
            LoadResultByDate(analysisDate);//读取测试结果
            series = chart_Analysis_Poisson.Series[0];
        }

        private void LoadResultByDate(DateTime analysisDate)
        {
            switch (AVI)
            {
                case "1":
                    progress_Analysis.Value = 20;
                    LoadAVIResultByDate(GlobalVar.gl_val_Machine1_S1_TestResult, analysisDate);
                    this.path_TestResult = GlobalVar.gl_val_Machine1_S1_TestResult;
                    break;
                case "2":
                    progress_Analysis.Value = 20;
                    LoadAVIResultByDate(GlobalVar.gl_val_Machine2_S1_TestResult, analysisDate);
                    this.path_TestResult = GlobalVar.gl_val_Machine2_S1_TestResult;
                    break;
                case "3":
                    progress_Analysis.Value = 20;
                    LoadAVIResultByDate(GlobalVar.gl_val_Machine3_S1_TestResult, analysisDate);
                    this.path_TestResult = GlobalVar.gl_val_Machine3_S1_TestResult;
                    break;
                default:
                    break;
            }
        }
        public DataTable GetAllDataTable(DataSet ds)
        {
            DataTable newDataTable = ds.Tables[0].Clone();                //创建新表 克隆以有表的架构。
            try
            {
                object[] objArray = new object[newDataTable.Columns.Count];   //定义与表列数相同的对象数组 存放表的一行的值。
                List<object> objList = new List<object>();
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    //newDataTable = ds.Tables[i].Clone();
                    //objArray = new object[newDataTable.Columns.Count];
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        //ds.Tables[i].Rows[j].ItemArray.CopyTo(objArray, 0);    //将表的一行的值存放数组中。
                        objList.Add(ds.Tables[i].Rows[j].ItemArray);
                        objArray = ds.Tables[i].Rows[j].ItemArray;
                        newDataTable.Rows.Add(objArray);                       //将数组的值添加到新表中。
                    }
                }
                return newDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取异常:" + ex.ToString());
                return newDataTable;
            }                                     //返回新表。
        }
        /// <summary>
        /// 读取指定机台指定日期的测试结果
        /// </summary>
        /// <param name="machine_TestResult">指定机台的测试结果路径</param>
        /// <param name="analysisDate">指定分析日期</param>
        private void LoadAVIResultByDate(string machine_TestResult, DateTime analysisDate)
        {
            string date = analysisDate.ToString("yyyy-MM-dd");
            string folderPath = machine_TestResult + "\\" + date;
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("无测试结果数据！");
                Form f = this.FindForm();
                //f.Close();
                return;
            }
            string[] files = Directory.GetFiles(folderPath, "*.csv");
            AllTestItems(files[0]);//获取所有测试项
            analysisData.Clear();
            DataSet ds = new DataSet();
            foreach (string fullPath in files)
            {
                ds.Tables.Add(OpenCSV(fullPath));
                //string filePath = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
                //string fileName = fullPath.Substring(fullPath.LastIndexOf('\\') + 1, fullPath.Length - fullPath.LastIndexOf('\\') - 1);
                //string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                //   + filePath + ";Extended Properties='Text;HDR=YES;FMT=Delimited;IMEX=1;'";

                //    string cmdStr = "select * from [" + fileName + "] where '结果'='OKPass' OR  '结果'='NGFail' ";
                //    var conn = new System.Data.OleDb.OleDbConnection(connStr);
                //    try
                //    {
                //        conn.Open();
                //        var adapter = new System.Data.OleDb.OleDbDataAdapter(cmdStr, conn);
                //        adapter.Fill(analysisData);
                //    }
                //    catch (Exception ex)
                //    {
                //        myf.writeErrorLog("查询异常" + ex.ToString());
                //        MessageBox.Show("查询异常！"+ex.Message);
                //    }
                //    finally
                //    {
                //        if (conn.State == System.Data.ConnectionState.Open)
                //            conn.Close();
                //    }

            }
            progress_Analysis.Value += 20;
            analysisData = GetAllDataTable(ds);
            if (analysisData.Columns.Count > 10)
            {
                int temp = 1;
                for (int i = 10; i < analysisData.Columns.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = temp.ToString();
                    lvi.SubItems.Add(analysisData.Columns[i].ColumnName);
                    this.listView_TestItems.Items.Add(lvi);
                    temp++;
                }
            }
            progress_Analysis.Value = 100;
        }

        private void AllTestItems(string path)
        {
            FileStream fs = new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string temp = sr.ReadLine();
            if (temp != null) testItems = temp.Split(',').ToList();
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// 分析日期更换
        /// </summary>
        /// <param name="date">新的分析日期</param>
        public void Analysis_DateChange(DateTime date)
        {
            this.analysisdate = date;
            LoadResultByDate(date);
        }




        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (textBox_testItem.Text != "")
            {
                if (testItems.Contains(textBox_testItem.Text))
                {
                    testItem_Select = textBox_testItem.Text;
                    LoadResultByItem(textBox_testItem.Text);
                    this.itemName = textBox_testItem.Text;
                }
                else MessageBox.Show("无当前测试项！");
            }
        }

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public DataTable OpenCSV(string fileName)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            try
            {
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;

                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(',');
                    if (IsFirst == true)
                    {
                        IsFirst = false;
                        columnCount = aryLine.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(aryLine[i]);
                            if (!dt.Columns.Contains((aryLine[i])))
                                dt.Columns.Add(dc);
                            else
                            {
                                string str = aryLine[i] + "[" + i.ToString() + "]";
                                dc = new DataColumn(str);
                                dt.Columns.Add(dc);
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < columnCount; j++)
                        {
                            dr[j] = aryLine[j];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch
            {
                return dt;
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }

        private void LoadResultByItem(string text)
        {
            try
            {
                temp.Clear();
                temp = analysisData.DefaultView.ToTable(false, text);
                dataGridView_Result.DataSource = temp;
                result_list.Clear();//清空测试结果
                textBox_USL.Text = "";
                textBox_LSL.Text = "";
                chart_Analysis_Normal.ChartAreas[0].AxisX.MajorGrid.Enabled = false;//关闭网格线
                chart_Analysis_Poisson.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//关闭网格线
                chart_Analysis_Normal.Series[0].Points.Clear();
                chart_Analysis_Normal.Series[1].Points.Clear();
                chart_Analysis_Poisson.Series[0].Points.Clear();
                chart_Analysis_Poisson.ChartAreas[0].AxisY.StripLines.Clear();
                chart_Analysis_Normal.ChartAreas[0].AxisX.StripLines.Clear();
                foreach (DataRow row in analysisData.Rows)
                {
                    if (row[text].ToString() != "\\" && row[text].ToString() != "" && row[text].ToString() != "\"\"")
                    {
                        if (IsNum(row[text].ToString()))
                            result_list.Add(double.Parse(row[text].ToString()));
                    }

                }
                for (int i = temp.Rows.Count - 1; i > 0; i--)
                {
                    if (temp.Rows[i][text].ToString() == "\\" || temp.Rows[i][text].ToString() == "" || temp.Rows[i][text].ToString() == "\"\"")
                    {
                        temp.Rows.Remove(temp.Rows[i]);
                    }
                }
                //去掉最大最小值
                if (result_list.Count > 1000)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        result_list.Remove(result_list.Max());
                        result_list.Remove(result_list.Min());
                    }
                }
                textBox_Max.Text = result_list.Max().ToString("00.000");
                textBox_Min.Text = result_list.Min().ToString("00.000");
                textBox_TestCount.Text = result_list.Count.ToString();
                textBox_AVG.Text = result_list.Average().ToString("00.000");
                textBox_stdevResult.Text = (Stdev(result_list)).ToString("00.0000");//计算标准差
                btn_Cp.Text = "计算";
                btn_CPK.Text = "计算";
            }
            catch (Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
                MessageBox.Show(ex.Message);
                this.FindForm().Close();
            }
        }

        private bool IsNum(string v)
        {
            try
            {
                double.Parse(v);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="result_list">数据源</param>
        /// <returns></returns>
        private double Stdev(List<double> result_list)
        {
            double xSum = 0F;
            double xAvg = 0F;
            double sSum = 0F;
            double tmpStDev = 0F;
            int arrNum = result_list.Count;
            xSum = result_list.Sum();
            xAvg = result_list.Average();
            for (int j = 0; j < arrNum; j++)
            {
                sSum += ((result_list[j] - xAvg) * (result_list[j] - xAvg));
            }
            tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (arrNum - 1))).ToString());
            return tmpStDev;
        }

        private void listView_TestItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView_TestItems.SelectedIndices;
            if (indexes.Count > 0)
            {
                int index = indexes[0];
                string item = listView_TestItems.Items[index].SubItems[1].Text;
                this.itemName = item;
                LoadResultByItem(item);
            }
        }

        private void btn_CPK_Click(object sender, EventArgs e)
        {
            try
            {
                // if (btn_CPK.Text == "计算")

                {
                    double xSum = result_list.Sum();
                    double xAvg = result_list.Average();
                    double sSum = 0F;
                    double tmpStDev = 0F;
                    for (int j = 0; j < result_list.Count; j++)
                    {
                        sSum += ((result_list[j] - xAvg) * (result_list[j] - xAvg));
                    }
                    tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (result_list.Count - 1))).ToString());
                    if (textBox_USL.Text != "" && textBox_LSL.Text != "")
                    {
                        double usl = double.Parse(textBox_USL.Text);//均值加3sigma
                        double lsl = double.Parse(textBox_LSL.Text);//均值减3sigma
                        double USL = double.Parse(textBox_USL.Text) * (double)0.1;
                        double Cpk = Math.Min((usl - xAvg) / (3 * tmpStDev), (xAvg - lsl) / (3 * tmpStDev));
                        btn_CPK.Text = Cpk.ToString("0.0000");
                        LoadDataSource();
                    }
                    else
                    {
                        MessageBox.Show("请设置上下限！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                {
                    double temp;
                    temp = 6 * Stdev(result_list);
                    if (textBox_USL.Text != "" && textBox_LSL.Text != "")
                    {
                        double StanderAver = double.Parse(textBox_USL.Text) - double.Parse(textBox_LSL.Text);
                        double Cp = (StanderAver) / temp;
                        btn_Cp.Text = Cp.ToString("0.0000");
                    }
                    else
                    {
                        MessageBox.Show("请设置上下限！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                ExportTestResult();
            }
            catch (Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
                MessageBox.Show(ex.Message);
                this.FindForm().Close();
            }
        }

        private void ExportTestResult()
        {
            string filename = Application.StartupPath + @"\Export\" + this.itemName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + @".csv";
            if (!Directory.Exists(Application.StartupPath + @"\Export\")) Directory.CreateDirectory(Application.StartupPath + @"\Export\");
            string[] files = Directory.GetFiles(Application.StartupPath + @"\Export\", this.itemName + "*.csv");
            if (files.Length > 0)
            {
                foreach (string path in files) File.Delete(path);
            }
            // if (!File.Exists(filename)) File.Create(filename);
            string[] param = new string[] { "BarCode", this.itemName };
            DataTable dt = analysisData.DefaultView.ToTable(false, param);
            SaveCSV(dt, filename);
            //ExportImage(dt);
        }

        private void ExportImage(DataTable dt)
        {
            //导出图片
            this.BeginInvoke(new Action(() =>
            {
                progress_Analysis.Value = 0;
                {
                    string export_Path = Application.StartupPath + @"\Export\Pic\" + this.itemName + "\\" + DateTime.Now.ToString("yyyyMMddHHmm");
                    if (!Directory.Exists(export_Path)) Directory.CreateDirectory(export_Path);
                    List<string> result = new List<string>();
                    List<string> barcode_list = new List<string>();
                    switch (AVI)
                    {
                        case "1":
                            string[] filepath = Directory.GetFiles(GlobalVar.gl_val_Machine1_S1_TestResult + @"\" + analysisdate.ToString("yyyy-MM-dd"), analysisdate.ToString("yyyyMMdd") + "*.csv");
                            foreach (string path in filepath)
                            {
                                result.AddRange(myf.SearchResult(path, this.itemName));
                            }
                            break;
                        case "2":
                            filepath = Directory.GetFiles(GlobalVar.gl_val_Machine2_S1_TestResult + @"\" + analysisdate.ToString("yyyy-MM-dd"), analysisdate.ToString("yyyyMMdd") + "*.csv");
                            foreach (string path in filepath)
                            {
                                result.AddRange(myf.SearchResult(path, this.itemName));
                            }
                            break;
                        case "3":
                            filepath = Directory.GetFiles(GlobalVar.gl_val_Machine3_S1_TestResult + @"\" + analysisdate.ToString("yyyy-MM-dd"), analysisdate.ToString("yyyyMMdd") + "*.csv");
                            foreach (string path in filepath)
                            {
                                result.AddRange(myf.SearchResult(path, this.itemName));
                            }
                            break;
                        default:
                            break;
                    }
                    progress_Analysis.Value = 30;
                    try
                    {
                        string temp1 = this.itemName.Substring(this.itemName.IndexOf('_') + 1);
                        string item = temp1.Substring(temp1.IndexOf('_') + 1);
                        //获取所有条码
                        foreach (string temp in result)
                        {
                            string[] arr_result = temp.Split('|');
                            if (wetherExportPic)
                            {
                                if (arr_result[1].Contains(item) && ResultBetweenValue(arr_result[8], dt)) barcode_list.Add(arr_result[8]);
                            }
                            else
                            {
                                if (ResultBetweenValue(arr_result[8], dt)) barcode_list.Add(arr_result[8]);
                            }
                        }
                        progress_Analysis.Value = 60;
                        foreach (string barcode in barcode_list)
                        {
                            if (barcode.Length < 16) continue;
                            string[] paths = myf.FindAllImageByBarcode(barcode);
                            foreach (string path in paths)
                            {
                                FileInfo file = new FileInfo(path);
                                string filefullName = file.Name;
                                if (cb_export.Checked)
                                {
                                    if (!Directory.Exists(export_Path + "\\" + barcode)) Directory.CreateDirectory(export_Path + "\\" + barcode);
                                    string fileName_Path = export_Path + "\\" + barcode + "\\" + filefullName;
                                    if (!File.Exists(fileName_Path))
                                        file.CopyTo(fileName_Path);
                                }
                                else
                                {
                                    if (path.Contains("主站主"))
                                    {
                                        if (!Directory.Exists(export_Path + "\\主站主")) Directory.CreateDirectory(export_Path + "\\主站主");
                                        string fileName_Path = export_Path + "\\主站主\\" + filefullName;
                                        if (!File.Exists(fileName_Path))
                                            file.CopyTo(fileName_Path);
                                    }
                                    else if (path.Contains("从站主"))
                                    {
                                        if (!Directory.Exists(export_Path + "\\从站主")) Directory.CreateDirectory(export_Path + "\\从站主");
                                        string fileName_Path = export_Path + "\\从站主\\" + filefullName;
                                        if (!File.Exists(fileName_Path)) file.CopyTo(fileName_Path);
                                    }
                                    else if (path.Contains("从站副"))
                                    {
                                        if (!Directory.Exists(export_Path + "\\从站副\\")) Directory.CreateDirectory(export_Path + "\\从站副\\");
                                        string fileName_Path = export_Path + "\\从站副\\" + filefullName;
                                        if (!File.Exists(fileName_Path))
                                            file.CopyTo(fileName_Path);
                                    }
                                    else if (path.Contains("主站副"))
                                    {
                                        if (!Directory.Exists(export_Path + "\\主站副\\")) Directory.CreateDirectory(export_Path + "\\主站副\\");
                                        string fileName_Path = export_Path + "\\主站副\\" + filefullName;
                                        if (!File.Exists(fileName_Path))
                                            file.CopyTo(fileName_Path);
                                    }
                                    else if (path.Contains("主站"))
                                    {
                                        if (!Directory.Exists(export_Path + "\\主站\\")) Directory.CreateDirectory(export_Path + "\\主站\\");
                                        string fileName_Path = export_Path + "\\主站\\" + filefullName;
                                        if (!File.Exists(fileName_Path))
                                            file.CopyTo(fileName_Path);
                                    }
                                    else
                                    {
                                        if (!Directory.Exists(export_Path + "\\TEMP")) Directory.CreateDirectory(export_Path + "\\TEMP");
                                        string fileName_Path = export_Path + "\\TEMP\\" + filefullName;
                                        if (!File.Exists(fileName_Path))
                                            file.CopyTo(fileName_Path);
                                    }
                                }
                            }
                        }
                        progress_Analysis.Value = 100;
                        MessageBox.Show("导出完成!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出异常!" + ex.ToString());
                    }
                }
            }));
        }

        /// <summary>
        /// 结果是否在范围之内
        /// </summary>
        /// <param name="result">条码</param>
        /// <returns></returns>
        private bool ResultBetweenValue(string result, DataTable dt)
        {
            try
            {
                int index = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(result))
                    {
                        index = i;
                        break;
                    }
                }
                double result_double = Convert.ToDouble(dt.Rows[index][1].ToString());
                double usl = Convert.ToDouble(textBox_USL.Text);
                double lsl = Convert.ToDouble(textBox_LSL.Text);
                if (result_double <= usl && result_double >= lsl) return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// 将DataTable中数据写入到CSV文件中
        ///
        /// 提供保存数据的DataTable
        /// CSV的文件路径
        public void SaveCSV(DataTable dt, string fileName)
        {
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";

            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            MessageBox.Show("CSV文件保存成功！");
        }

        private void btn_Cp_Click(object sender, EventArgs e)
        {
            try
            {
                // if (btn_Cp.Text == "计算")
                {
                    double temp;
                    temp = 6 * Stdev(result_list);
                    if (textBox_USL.Text != "" && textBox_LSL.Text != "")
                    {
                        double StanderAver = double.Parse(textBox_USL.Text) - double.Parse(textBox_LSL.Text);
                        double Cp = (StanderAver) / temp;
                        btn_Cp.Text = Cp.ToString("0.0000");
                    }
                    else
                    {
                        MessageBox.Show("请设置上下限！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                myf.writeErrorLog(ex.ToString());
                MessageBox.Show(ex.Message);
                this.FindForm().Close();
            }
        }

        #region 分布图
        int mostPrecision;
        private string itemName;

        /// <summary>
        /// 获取数据序列的最大精度 (即小数点后面的位数长度)
        /// </summary>
        /// <param name="sampleData">样本数据</param>
        /// <returns></returns>
        public static int GetMostPrecision(List<double> sampleData)
        {
            if (sampleData == null || sampleData.Count == 0)
            {
                return 0;
            }

            int mostPrecision = 0;
            int tempValue = 0;

            foreach (double value in sampleData)
            {
                string data = Math.Abs(value).ToString();
                int dateLength = data.Length;
                int dotIndex = data.IndexOf(".");

                if (dotIndex > 0)
                {
                    tempValue = dateLength - (dotIndex + 1);
                }

                if (tempValue > mostPrecision) //取更大的精度
                {
                    mostPrecision = tempValue;
                }
            }

            return mostPrecision;
        }

        /// <summary>
        /// 缩放倍数
        /// </summary>
        /// <param name="mostPrecision"></param>
        /// <returns></returns>
        private static double ZoomMultiple(ref int mostPrecision)
        {
            double zoomMultiple = Math.Pow(10, mostPrecision - 1);

            if (mostPrecision <= 2) //保证精度大于二的数据序列图形的平滑
            {
                mostPrecision = 4;
                zoomMultiple = 100;
            }

            return zoomMultiple;
        }


        /// <summary>
        /// 计算Sigma
        /// </summary>
        /// <param name="sampleData">样本数据</param>
        /// <param name="xbar">平均值</param>
        /// <returns></returns>
        public static double CalculateSigma(List<double> sampleData, double xbar)
        {
            double sigma = 0;
            int sampleCount = sampleData.Count;
            double powSum = 0;

            if (sampleData == null || sampleCount <= 2
) //样本个数大于2计算才有意思
            {
                return sigma;
            }

            foreach (double value in sampleData)
            {
                powSum += Math.Pow(value - xbar, 2); //样本值减去均值2的次幂相加。
            }

            sigma = Math.Sqrt(powSum / (sampleCount - 1));

            return sigma;
        }

        private void LoadDataSource()
        {
            chart_Analysis_Normal.Series[0].Points.Clear();
            chart_Analysis_Normal.Series[1].Points.Clear();
            chart_Analysis_Poisson.Series[0].Points.Clear();
            chart_Analysis_Poisson.ChartAreas[0].AxisY.StripLines.Clear();
            chart_Analysis_Normal.ChartAreas[0].AxisX.StripLines.Clear();
            ResetAxisBySampleData(result_list, chart_Analysis_Normal);
            mostPrecision = GetMostPrecision(result_list);
            double xbar = Math.Round(result_list.Average(), mostPrecision);
            double sigma = CalculateSigma(result_list, xbar);
            double zoomMultiple = ZoomMultiple(ref mostPrecision);
            double usl = double.Parse(textBox_USL.Text);
            double lsl = double.Parse(textBox_LSL.Text);
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();
            int positiveLimit = (int)((xbar + 6 * sigma) * zoomMultiple); //X轴的正界限
            int minusLimit = (int)((xbar - 6 * sigma) * zoomMultiple); //X轴的负界限
            double coefficient = (1 / Math.Sqrt(2 * Math.PI) / sigma);//Math.Round(1 / Math.Sqrt(2 * Math.PI) / sigma, mostPrecision); //系数；如果计算需要精确，就不要四舍五入；建议：为了提高运算效率要四舍五入。


            for (int x = minusLimit; x <= positiveLimit; x++)
            {
                //x轴缩小zoomMultiple倍x每隔1/zoomMultiple变化曲线变平滑
                double xValue = x / zoomMultiple;
                double yValue = coefficient * Math.Exp(Math.Pow((xValue - xbar), 2) / (-2 * Math.Pow(sigma, 2)));
                xValue = Math.Round(xValue, mostPrecision);
                yValue = Math.Round(yValue, mostPrecision);
                if (yValue > 0.0001)//可设为yValue > 0
                {
                    xValues.Add(xValue);
                    yValues.Add(yValue);
                }
            }

            //为MSChart绑定数据值
            AddStripLineX(chart_Analysis_Normal.ChartAreas[0], "期望:" + result_list.Average().ToString("0.000"), xbar, 0.001, Color.Green);
            chart_Analysis_Normal.Series[0].Points.DataBindXY(xValues, yValues);
            #region 数量分布
            List<int> y = new List<int>();
            List<double> xV = new List<double>();
            double temp = result_list.Max() - result_list.Min();
            temp = temp / 10;
            for (int i = 1; i <= 10; i++)
            {
                double t1 = result_list.Min() + i * temp;
                double t2 = result_list.Min() + (i - 1) * temp;
                int j = 0;
                foreach (double tmp in result_list)
                {
                    if (tmp < t1 && tmp >= t2) j++;
                    //else break;
                }
                y.Add(j);
                xV.Add(t1);
            }
            List<int> xx = new List<int>();
            int num = 0;
            foreach (double tmp in result_list)
            {
                num++;
                xx.Add(num);
            }
            chart_Analysis_Poisson.ChartAreas[0].AxisY.Minimum = result_list.Min() - result_list.Min() / 10;
            chart_Analysis_Poisson.ChartAreas[0].AxisY.Maximum = result_list.Max() + result_list.Max() / 10;
            chart_Analysis_Poisson.Series[0].Points.DataBindXY(xx, result_list);
            AddStripLineY(chart_Analysis_Poisson.ChartAreas[0], "Average", xbar, 0.003, Color.Green);
            AddStripLineY(chart_Analysis_Poisson.ChartAreas[0], "USL", usl, 0.003, Color.Red);
            AddStripLineY(chart_Analysis_Poisson.ChartAreas[0], "LSL", lsl, 0.003, Color.Red);
            chart_Analysis_Normal.Series[1].Points.DataBindXY(xV, y);
            #endregion
            double yAxisMax;
            if (yValues.Count > 0)
            {
                yAxisMax = 0;
                return;

            }
            //将Y轴最大值放大倍作为
            double yMax = Math.Round(yValues.Max() * 1.1, mostPrecision);
            double xMin = xValues.Min();
            double xMax = xValues.Max();
            double yMin = yValues.Min();
            yAxisMax = yValues.Max();
            if (xMin > lsl)
            {
                xMin = lsl;
            }
            if (xMax < usl)
            {
                xMax = usl;
            }
            //设置轴值x轴加减极大极小值的1/zoomMultiple倍是为了图形能全部绘制出来
            chart_Analysis_Normal.ChartAreas[0].AxisX.Minimum = (double)Math.Round(xMin - xMin * 1 / zoomMultiple, mostPrecision);
            chart_Analysis_Normal.ChartAreas[0].AxisX.Maximum = (double)Math.Round(xMax + xMax * 1 / zoomMultiple, mostPrecision);
            chart_Analysis_Normal.ChartAreas[0].AxisY.Minimum = (double)Math.Round(yMin, mostPrecision);
            chart_Analysis_Normal.ChartAreas[0].AxisY.Maximum = (double)Math.Round(yMax, mostPrecision);



        }

        /// <summary>
        /// 添加阈值线
        /// </summary>
        /// <param name="chartArea">图形Area</param>
        /// <param name="lineName">在线显示的名子</param>
        /// <param name="lineOffset">线在图上的位置</param>
        /// <param name="lineWidth">线宽</param>
        /// <param name="lineColor">线的颜色</param>
        public void AddStripLineY(ChartArea chartArea, string lineName, double lineOffset, double lineWidth, Color lineColor)
        {
            StripLine stripLine = new StripLine
            {
                BackColor = lineColor,
                StripWidth = lineWidth,
                // BackHatchStyle = ChartHatchStyle.DarkVertical,
                Text = lineName,
                //TextAlignment = StringAlignment.Far,
                //TextLineAlignment = StringAlignment.Center,
                IntervalOffset = lineOffset
            };

            chartArea.AxisY.StripLines.Add(stripLine);
        }
        /// <summary>
        /// 添加阈值线
        /// </summary>
        /// <param name="chartArea">图形Area</param>
        /// <param name="lineName">在线显示的名子</param>
        /// <param name="lineOffset">线在图上的位置</param>
        /// <param name="lineWidth">线宽</param>
        /// <param name="lineColor">线的颜色</param>
        public void AddStripLineX(ChartArea chartArea, string lineName, double lineOffset, double lineWidth, Color lineColor)
        {
            StripLine stripLine = new StripLine
            {
                BackColor = lineColor,
                StripWidth = lineWidth,
                // BackHatchStyle = ChartHatchStyle.DarkVertical,
                Text = lineName,
                TextAlignment = StringAlignment.Far,
                TextLineAlignment = StringAlignment.Center,
                IntervalOffset = lineOffset
            };

            chartArea.AxisX.StripLines.Add(stripLine);
        }



        /// <summary>
        /// 根据sampleData的最大和最小值重设X轴的最大和最小刻度
        /// </summary>
        /// <param name="queueValue"></param>
        public static void ResetAxisBySampleData(List<double> sampleData, Chart chart)
        {
            if (sampleData == null || sampleData.Count <= 0)
            {
                return;
            }

            double max = (double)sampleData.Max();
            double min = (double)sampleData.Min();
            double xMax = chart.ChartAreas[0].AxisX.Maximum;
            double xMin = chart.ChartAreas[0].AxisX.Minimum;

            if (xMin > xMax)
            {
                chart.ChartAreas[0].AxisX.Minimum = min;
                chart.ChartAreas[0].AxisX.Maximum = max;
            }
        }

        #endregion

        private void lb_ExportPic_Click(object sender, EventArgs e)
        {
            if (wetherExportPic)
            {
                lb_ExportPic.Text = "否";
                wetherExportPic = false;
            }
            else
            {
                lb_ExportPic.Text = "是";
                wetherExportPic = true;
            }
        }

        private void textBox_USL_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void listView_TestItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //========================================================设置选择条目背景颜色

            if (listView_TestItems.SelectedIndices.Count > 0)         //若有选中项 
            {
                listView_TestItems.Items[listView_TestItems.SelectedIndices[0]].BackColor = Color.SkyBlue; //设置选中项的背景颜色 

            }

        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            string[] param = new string[] { "BarCode", this.itemName };
            DataTable dt = analysisData.DefaultView.ToTable(false, param);
            Thread thread = new Thread(new ThreadStart(delegate
            {
                ExportImage(dt);
            }));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
