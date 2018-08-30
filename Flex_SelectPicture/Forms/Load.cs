using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture
{
    public partial class Load : Form
    {
        MyFunctions myf = new MyFunctions();
        public Load()
        {
            InitializeComponent();
            loadConfig();
        }

        //启动everything查询api
        public static void StartEverything()
        {
            //Regex regex = new Regex(@"Everything([-.0-9])");
            bool found = false;
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("Everything"))
                {
                //    found = true;
                    process.Kill();
                    break;
                }

            }
            if (!found)
            {
                string path = Application.StartupPath + @"\Everything\Everything.exe";
                ProcessStartInfo processStartInfo = new ProcessStartInfo(path);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(processStartInfo);
            }
        }
        //读取配置信息
        private void loadConfig()
        {
            string filepath = Application.StartupPath + @"\CONFIG.ini";
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
                Config config = new Config();
                config.ShowDialog();
                return;
            }
            #region 读取config配置
            //AVI-1主站测试结果
            StringBuilder str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine1_S1_TestResult = str_tmp.ToString();
            //AVI-1从站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine1_S2_TestResult = str_tmp.ToString();
            //AVI-1从站副测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S3_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine1_S3_TestResult = str_tmp.ToString();
            //AVI-1主站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine1_S1_TestVal = str_tmp.ToString();
            //AVI-1从站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine1_S2_TestVal = str_tmp.ToString();
            //AVI-1从站副测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S3_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine1_S3_TestVal = str_tmp.ToString();
            //AVI-2主站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine2_S1_TestResult = str_tmp.ToString();
            //AVI-2从站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine2_S2_TestResult = str_tmp.ToString();
            //AVI-2从站副测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S3_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine2_S3_TestResult = str_tmp.ToString();
            //AVI-2主站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine2_S1_TestVal = str_tmp.ToString();
            //AVI-2从站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine2_S2_TestVal = str_tmp.ToString();
            //AVI-2从站副测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S3_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine2_S3_TestVal = str_tmp.ToString();
            //AVI-3主站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S1_TestResult = str_tmp.ToString();
            //AVI-3从站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S2_TestResult = str_tmp.ToString();
            //AVI-3从站副测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestResult, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S3_TestResult = str_tmp.ToString();
            //AVI-3主站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S1_TestVal = str_tmp.ToString();
            //AVI-3从站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S2_TestVal = str_tmp.ToString();
            //AVI-3从站副测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S3_TestVal = str_tmp.ToString();
            //AVI-1是否启用
            str_tmp = new StringBuilder(50);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global,GlobalVar.gl_key_Machine1_Check,"",str_tmp,50,filepath);
            GlobalVar.gl_val_Machine1_Check = str_tmp.ToString();
            //AVI-2是否启用
            str_tmp = new StringBuilder(50);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_Check, "", str_tmp, 50, filepath);
            GlobalVar.gl_val_Machine2_Check = str_tmp.ToString();
            //AVI-3是否启用
            str_tmp = new StringBuilder(50);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_Check, "", str_tmp, 50, filepath);
            GlobalVar.gl_val_Machine3_Check = str_tmp.ToString();

            #endregion
            if (GlobalVar.gl_val_Machine1_Check == "1") checkBox_Machine1.Checked = true;
            if (GlobalVar.gl_val_Machine2_Check == "1") checkBox_Machine2.Checked = true;
            if (GlobalVar.gl_val_Machine3_Check == "1") checkBox_Machine3.Checked = true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            GlobalVar.gl_appIsRunning = false;
            System.Environment.Exit(0);
        }

        private void Load_Load(object sender, EventArgs e)
        {
            GlobalVar.gl_appIsRunning = true;
            if (checkBox_Machine1.Checked)
            {
                GlobalVar.gl_val_Machine1_Check = "1";
            }
            if (checkBox_Machine2.Checked)
            {
                GlobalVar.gl_val_Machine2_Check = "1";
            }
            if (checkBox_Machine3.Checked)
            {
                GlobalVar.gl_val_Machine3_Check = "1";
            }
        }

        private void checkBox_Machine1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox_Machine1.Checked)
            {
                GlobalVar.gl_val_Machine1_Check = "1";
            }
            else
            {
                GlobalVar.gl_val_Machine1_Check = "0";
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if(checkBox_Machine1.Checked || checkBox_Machine2.Checked || checkBox_Machine3.Checked)
            {
                string filepath = Application.StartupPath + @"\CONFIG.ini";
                if (checkBox_Machine1.Checked)
                {
                    myf.getTestItem(GlobalVar.gl_val_Machine1_S1_TestVal, GlobalVar.AVI1_S1_TestItems);//读取测试项
                    myf.getTestItem(GlobalVar.gl_val_Machine1_S2_TestVal,GlobalVar.AVI1_S2_TestItems);
                    myf.getTestItem(GlobalVar.gl_val_Machine1_S3_TestVal, GlobalVar.AVI1_S3_TestItems);
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_Check, GlobalVar.gl_val_Machine1_Check, filepath);
                }
                else
                {
                    GlobalVar.gl_val_Machine1_Check = "0";
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_Check, GlobalVar.gl_val_Machine1_Check, filepath);
                }
                if (checkBox_Machine2.Checked)
                {
                    myf.getTestItem(GlobalVar.gl_val_Machine2_S1_TestVal, GlobalVar.AVI2_S1_TestItems);//读取测试项
                    myf.getTestItem(GlobalVar.gl_val_Machine2_S2_TestVal, GlobalVar.AVI2_S2_TestItems);
                    myf.getTestItem(GlobalVar.gl_val_Machine2_S3_TestVal, GlobalVar.AVI2_S3_TestItems);
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_Check, GlobalVar.gl_val_Machine2_Check, filepath);
                }
                else
                {
                    GlobalVar.gl_val_Machine2_Check = "0";
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_Check, GlobalVar.gl_val_Machine2_Check, filepath);
                }
                if (checkBox_Machine3.Checked)
                {
                    myf.getTestItem(GlobalVar.gl_val_Machine3_S1_TestVal, GlobalVar.AVI3_S1_TestItems);//读取测试项
                    myf.getTestItem(GlobalVar.gl_val_Machine3_S2_TestVal, GlobalVar.AVI3_S2_TestItems);
                    myf.getTestItem(GlobalVar.gl_val_Machine3_S3_TestVal, GlobalVar.AVI3_S3_TestItems);
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_Check, GlobalVar.gl_val_Machine3_Check, filepath);
                }
                else
                {
                    GlobalVar.gl_val_Machine3_Check = "0";
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_Check, GlobalVar.gl_val_Machine3_Check, filepath);
                }
                //string path=string.Empty;
                //if (checkBox_Machine1.Checked) path += "\"" + GlobalVar.gl_val_Machine1_S1_TestResult + "\",\"" + GlobalVar.gl_val_Machine1_S2_TestResult + "\",";
                //if (checkBox_Machine2.Checked) path += "\"" + GlobalVar.gl_val_Machine2_S1_TestResult + "\",\"" + GlobalVar.gl_val_Machine2_S2_TestResult + "\",";
                //if (checkBox_Machine3.Checked) path += "\"" + GlobalVar.gl_val_Machine3_S1_TestResult + "\",\"" + GlobalVar.gl_val_Machine3_S2_TestResult + "\"";
                //string everything_path = Application.StartupPath + @"\Everything\Everything.ini";
                //MyFunctions.WritePrivateProfileString("Everything","folders",path,everything_path);
                //string folder_monitor_changes = string.Empty;
                //string folder_update_types = string.Empty;
                //string folder_update_days = string.Empty;
                //string folder_update_intervals = string.Empty;
                //string folder_update_interval_types = string.Empty;
                //if (checkBox_Machine1.Checked)
                //{
                //    folder_monitor_changes += "1,1,";
                //    folder_update_types += "1,1,";
                //    folder_update_days += "0,0,";
                //    folder_update_intervals += "1,1,";
                //    folder_update_interval_types += "0,0,";
                //}
                //if (checkBox_Machine2.Checked)
                //{
                //    folder_monitor_changes += "1,1,";
                //    folder_update_types += "1,1,";
                //    folder_update_days += "0,0,";
                //    folder_update_intervals += "1,1,";
                //    folder_update_interval_types += "0,0,";
                //}

                //if (checkBox_Machine3.Checked)
                //{
                //    folder_monitor_changes += "1,1,";
                //    folder_update_types += "1,1,";
                //    folder_update_days += "0,0,";
                //    folder_update_intervals += "1,1,";
                //    folder_update_interval_types += "0,0,";
                //}
                //MyFunctions.WritePrivateProfileString("Everything", "folder_monitor_changes", folder_monitor_changes, everything_path);
                //MyFunctions.WritePrivateProfileString("Everything", "folder_update_types", folder_update_types, everything_path);
                //MyFunctions.WritePrivateProfileString("Everything", "folder_update_days", folder_update_days, everything_path);
                //MyFunctions.WritePrivateProfileString("Everything", "folder_update_intervals", folder_update_intervals, everything_path);
                //MyFunctions.WritePrivateProfileString("Everything", "folder_update_interval_types", folder_update_interval_types, everything_path);
                StartEverything();//启动everything后台程序

                this.Close();
            }
            else
            {
                MessageBox.Show("当前未选择查询机台，请选择至少一台机台来查询！");
            }
        }

        

        private void checkBox_Machine2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Machine2.Checked)
            {
                GlobalVar.gl_val_Machine2_Check = "1";
            }
            else
            {
                GlobalVar.gl_val_Machine2_Check = "0";
            }
        }

        private void checkBox_Machine3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Machine3.Checked)
            {
                GlobalVar.gl_val_Machine3_Check = "1";
            }
            else
            {
                GlobalVar.gl_val_Machine3_Check = "0";
            }
        }
    }
}
