using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture
{
    public partial class Config : Form
    {
        MyFunctions myf = new MyFunctions();
        public Config()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 配置文件路径
        private void label1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI1_S1_PIC.Text = folderBrowser.SelectedPath;//AVI-1主站图片、测试地址
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI1_S2_PIC.Text = folderBrowser.SelectedPath;//AVI-1从站图片地址
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI1_S3_PIC.Text = folderBrowser.SelectedPath;//AVI-1从站2图片地址
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI1_S1_TestItem.Text = folderBrowser.SelectedPath;//AVI-1主站测试项地址
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI1_S2_TestItem.Text = folderBrowser.SelectedPath;//AVI-1从站测试项地址
            }
        }


        private void label14_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI1_S3_TestItem.Text = folderBrowser.SelectedPath;//AVI-1从站2测试项地址
            }
        }
        private void label8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI2_S1_PIC.Text = folderBrowser.SelectedPath;//AVI-2主站图片、测试地址
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI2_S2_PIC.Text = folderBrowser.SelectedPath;//AVI-2从站图片地址
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI2_S3_PIC.Text = folderBrowser.SelectedPath;//AVI-2从站2图片地址
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI2_S1_TestItem.Text = folderBrowser.SelectedPath;//AVI-2主站测试项地址
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI2_S2_TestItem.Text = folderBrowser.SelectedPath;//AVI-2从站测试项地址
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI2_S3_TestItem.Text = folderBrowser.SelectedPath;//AVI-2从站2测试项地址
            }
        }
        private void label12_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI3_S1_PIC.Text = folderBrowser.SelectedPath;//AVI-3主站图片、测试地址
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI3_S2_PIC.Text = folderBrowser.SelectedPath;//AVI-3从站图片地址
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI3_S3_PIC.Text = folderBrowser.SelectedPath;//AVI-3从站2图片地址
            }
        }
        private void label10_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI3_S1_TestItem.Text = folderBrowser.SelectedPath;//AVI-3主站测试项地址
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI3_S2_TestItem.Text = folderBrowser.SelectedPath;//AVI-3从站测试项地址
            }
        }
        private void label18_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "选择文件存储路径";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                tb_AVI3_S3_TestItem.Text = folderBrowser.SelectedPath;//AVI-3从站2测试项地址
            }
        }

        #endregion

        private void Config_Load(object sender, EventArgs e)
        {
            string filepath = Application.StartupPath + @"\CONFIG.ini";
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
                return;
            }
            #region 读取config配置
            //AVI-1主站测试结果
            StringBuilder str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestResult,"", str_tmp, 100, filepath);
            tb_AVI1_S1_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine1_S1_TestResult = str_tmp.ToString();
            //AVI-1从站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestResult, "", str_tmp, 100, filepath);
            tb_AVI1_S2_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine1_S2_TestResult = str_tmp.ToString();
            //AVI-1从站2测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S3_TestResult, "", str_tmp, 100, filepath);
            tb_AVI1_S3_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine1_S3_TestResult = str_tmp.ToString();
            //AVI-1主站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestVal, "", str_tmp, 100, filepath);
            tb_AVI1_S1_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine1_S1_TestVal = str_tmp.ToString();
            //AVI-1从站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestVal, "", str_tmp, 100, filepath);
            tb_AVI1_S2_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine1_S2_TestVal = str_tmp.ToString();
            //AVI-1从站2测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S3_TestVal, "", str_tmp, 100, filepath);
            tb_AVI1_S3_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine1_S3_TestVal = str_tmp.ToString();
            //AVI-2主站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestResult, "", str_tmp, 100, filepath);
            tb_AVI2_S1_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine2_S1_TestResult = str_tmp.ToString();
            //AVI-2从站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestResult, "", str_tmp, 100, filepath);
            tb_AVI2_S2_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine2_S2_TestResult = str_tmp.ToString();
            //AVI-2从站2测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S3_TestResult, "", str_tmp, 100, filepath);
            tb_AVI2_S3_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine2_S3_TestResult = str_tmp.ToString();
            //AVI-2主站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestVal, "", str_tmp, 100, filepath);
            tb_AVI2_S1_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine2_S1_TestVal = str_tmp.ToString();
            //AVI-2从站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestVal, "", str_tmp, 100, filepath);
            tb_AVI2_S2_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine2_S2_TestVal = str_tmp.ToString();
            //AVI-2从站2测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S3_TestVal, "", str_tmp, 100, filepath);
            tb_AVI2_S3_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine2_S3_TestVal = str_tmp.ToString();
            //AVI-3主站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestResult, "", str_tmp, 100, filepath);
            tb_AVI3_S1_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine3_S1_TestResult = str_tmp.ToString();
            //AVI-3从站测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestResult, "", str_tmp, 100, filepath);
            tb_AVI3_S2_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine3_S2_TestResult = str_tmp.ToString();
            //AVI-3从站2测试结果
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestResult, "", str_tmp, 100, filepath);
            tb_AVI3_S3_PIC.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine3_S3_TestResult = str_tmp.ToString();
            //AVI-3主站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestVal, "", str_tmp, 100, filepath);
            tb_AVI3_S1_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine3_S1_TestVal = str_tmp.ToString();
            //AVI-3从站测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestVal, "", str_tmp, 100, filepath);
            tb_AVI3_S2_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine3_S2_TestVal = str_tmp.ToString();
            //AVI-3从站2测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestVal, "", str_tmp, 100, filepath);
            tb_AVI3_S3_TestItem.Text = str_tmp.ToString();
            GlobalVar.gl_val_Machine3_S3_TestVal = str_tmp.ToString();
            #endregion
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string filepath = Application.StartupPath + @"\CONFIG.ini";
            if (tb_AVI1_S1_PIC.Text != "" && tb_AVI1_S2_PIC.Text != "" && tb_AVI1_S3_PIC.Text != "" && tb_AVI1_S1_TestItem.Text != "" && tb_AVI1_S2_TestItem.Text != "" && tb_AVI1_S3_TestItem.Text != "")
            {
                
                #region 存储配置信息
                //AVI-1 主站测试结果存储
                GlobalVar.gl_val_Machine1_S1_TestResult = tb_AVI1_S1_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestResult, tb_AVI1_S1_PIC.Text, filepath);

                //AVI-1 从站测试结果存储
                GlobalVar.gl_val_Machine1_S2_TestResult = tb_AVI1_S2_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestResult, tb_AVI1_S2_PIC.Text, filepath);
                //AVI-1 从站2测试结果存储
                GlobalVar.gl_val_Machine1_S3_TestResult = tb_AVI1_S3_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S3_TestResult, tb_AVI1_S3_PIC.Text, filepath);

                //AVI-1 主站测试项存储
                GlobalVar.gl_val_Machine1_S1_TestVal = tb_AVI1_S1_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestVal, tb_AVI1_S1_TestItem.Text, filepath);

                //AVI-1 从站测试项存储
                GlobalVar.gl_val_Machine1_S2_TestVal = tb_AVI1_S2_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestVal, tb_AVI1_S2_TestItem.Text, filepath);
                //AVI-1 从站2测试项存储
                GlobalVar.gl_val_Machine1_S3_TestVal = tb_AVI1_S3_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S3_TestVal, tb_AVI1_S3_TestItem.Text, filepath);

                #endregion
            }
            if(tb_AVI2_S1_PIC.Text != "" && tb_AVI2_S2_PIC.Text != "" && tb_AVI2_S1_TestItem.Text != "" && tb_AVI2_S2_TestItem.Text != "")
            {
                //AVI-2 主站测试结果存储
                GlobalVar.gl_val_Machine2_S1_TestResult = tb_AVI2_S1_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestResult, tb_AVI2_S1_PIC.Text, filepath);
                //AVI-2 从站测试结果存储
                GlobalVar.gl_val_Machine2_S2_TestResult = tb_AVI2_S2_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestResult, tb_AVI2_S2_PIC.Text, filepath);
                //AVI-2 从站2测试结果存储
                GlobalVar.gl_val_Machine2_S3_TestResult = tb_AVI2_S3_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S3_TestResult, tb_AVI2_S3_PIC.Text, filepath);
                //AVI-2 主站测试项存储
                GlobalVar.gl_val_Machine2_S1_TestVal = tb_AVI2_S1_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestVal, tb_AVI2_S1_TestItem.Text, filepath);
                //AVI-2 从站测试项存储
                GlobalVar.gl_val_Machine2_S2_TestVal = tb_AVI2_S2_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestVal, tb_AVI2_S2_TestItem.Text, filepath);
                //AVI-2 从站2测试项存储
                GlobalVar.gl_val_Machine2_S3_TestVal = tb_AVI2_S3_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S3_TestVal, tb_AVI2_S3_TestItem.Text, filepath);

            }
            if(tb_AVI3_S1_PIC.Text != "" && tb_AVI3_S2_PIC.Text != "" && tb_AVI3_S1_TestItem.Text != "" && tb_AVI3_S2_TestItem.Text != "")
            {
                //AVI-3 主站测试结果存储
                GlobalVar.gl_val_Machine3_S1_TestResult = tb_AVI3_S1_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestResult, tb_AVI3_S1_PIC.Text, filepath);
                //AVI-3 从站测试结果存储
                GlobalVar.gl_val_Machine2_S2_TestResult = tb_AVI2_S2_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestResult, tb_AVI3_S2_PIC.Text, filepath);
                //AVI-3 从站2测试结果存储
                GlobalVar.gl_val_Machine2_S3_TestResult = tb_AVI2_S3_PIC.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestResult, tb_AVI3_S3_PIC.Text, filepath);
                //AVI-3 主站测试项存储
                GlobalVar.gl_val_Machine3_S1_TestVal = tb_AVI3_S1_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestVal, tb_AVI3_S1_TestItem.Text, filepath);
                //AVI-3 从站测试项存储
                GlobalVar.gl_val_Machine3_S2_TestVal = tb_AVI3_S2_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestVal, tb_AVI3_S2_TestItem.Text, filepath);
                //AVI-3 从站2测试项存储
                GlobalVar.gl_val_Machine3_S3_TestVal = tb_AVI3_S3_TestItem.Text;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestVal, tb_AVI3_S3_TestItem.Text, filepath);
            }
            
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
            //AVI-1从站2测试结果
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
            //AVI-1从站2测试项
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
            //AVI-2从站2测试结果
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
            //AVI-2从站2测试项
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
            //AVI-3从站2测试结果
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
            //AVI-3从站2测试项
            str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S3_TestVal, "", str_tmp, 100, filepath);
            GlobalVar.gl_val_Machine3_S3_TestVal = str_tmp.ToString();
            //AVI-1是否启用
            str_tmp = new StringBuilder(50);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_Check, "", str_tmp, 50, filepath);
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
            this.Close();
        }


    }
}
