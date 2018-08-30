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
    public partial class LoadYeild : Form
    {
        public LoadYeild()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + @"\CONFIG.ini";
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_AVI1, GlobalVar.gl_val_check_AVI1,filePath);
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_AVI2, GlobalVar.gl_val_check_AVI2, filePath);
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_AVI3, GlobalVar.gl_val_check_AVI3, filePath);
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_stations, GlobalVar.gl_val_check_stations, filePath);
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Yeild,GlobalVar.gl_key_check_stationscount,GlobalVar.gl_val_check_stationscount,filePath);
            GlobalVar.gl_check_checkDate = dtp_checkDate.Value;
            this.Close();
         }

        private void LoadYeild_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + @"\CONFIG.ini";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                Config config = new Config();
                config.Show();
                return;
            }
            StringBuilder str_tmp = new StringBuilder();
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_AVI1, "0", str_tmp, 50, filePath);
            GlobalVar.gl_val_check_AVI1 = str_tmp.ToString();
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_AVI2, "0", str_tmp, 50, filePath);
            GlobalVar.gl_val_check_AVI2 = str_tmp.ToString();
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Yeild, GlobalVar.gl_key_check_AVI3, "0", str_tmp, 50, filePath);
            GlobalVar.gl_val_check_AVI3 = str_tmp.ToString();
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Yeild,GlobalVar.gl_key_check_stations,"0",str_tmp,50,filePath);
            GlobalVar.gl_val_check_stations = str_tmp.ToString();
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_section_Yeild,GlobalVar.gl_key_check_stationscount,"1",str_tmp,50,filePath);
            GlobalVar.gl_val_check_stationscount = str_tmp.ToString();
            LoadControls();

        }

        private void LoadControls()
        {
            if (GlobalVar.gl_val_check_AVI1 == "1") checkBox_Machine1.Checked = true;
            if (GlobalVar.gl_val_check_AVI2 == "1") checkBox_Machine2.Checked = true;
            if (GlobalVar.gl_val_check_AVI3 == "1") checkBox_Machine3.Checked = true;
            if (GlobalVar.gl_val_check_stations == "1")
            {
                checkBox_station.Checked = true;
                numericUpDown_stationcount.Visible = true;
                label2.Visible = true;
                numericUpDown_stationcount.Value =int.Parse(GlobalVar.gl_val_check_stationscount);
            }

        }

        private void checkBox_Machine1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Machine1.Checked) GlobalVar.gl_val_check_AVI1 = "1";
            else GlobalVar.gl_val_check_AVI1 = "0";
        }

        private void checkBox_Machine2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Machine2.Checked) GlobalVar.gl_val_check_AVI2 = "1";
            else GlobalVar.gl_val_check_AVI2 = "0";
        }

        private void checkBox_Machine3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Machine3.Checked) GlobalVar.gl_val_check_AVI3 = "1";
            else GlobalVar.gl_val_check_AVI3 = "0";
        }

        private void checkBox_station_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_station.Checked)
            {
                GlobalVar.gl_val_check_stations = "1";
                numericUpDown_stationcount.Visible = true;
                label2.Visible = true;
            }
            else
            {
                GlobalVar.gl_val_check_stations = "0";
                numericUpDown_stationcount.Visible = false;
                label2.Visible = false;
            }
        }

        private void numericUpDown_stationcount_ValueChanged(object sender, EventArgs e)
        {
            GlobalVar.gl_val_check_stationscount = numericUpDown_stationcount.Value.ToString("0");
        }
    }
}
