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

namespace Flex_SelectPicture
{
    public partial class ChangePM : Form
    {
        MyFunctions myf = new MyFunctions();
        private readonly string AVI;
        private string s1_testitem_Path = "";
        public ChangePM()
        {
            InitializeComponent();
        }
        public ChangePM(string avi)
        {
            this.AVI = avi;
            switch (avi)
            {
                case "1":
                    s1_testitem_Path = GlobalVar.gl_val_Machine1_S1_TestVal;
                    Text = "AVI-1 品目切换";
                    break;
                case "2":
                    s1_testitem_Path = GlobalVar.gl_val_Machine2_S1_TestVal;
                    Text = "AVI-2 品目切换";
                    break;
                case "3":
                    s1_testitem_Path = GlobalVar.gl_val_Machine3_S1_TestVal;
                    Text = "AVI-3 品目切换";
                    break;
                default:
                    break;
            }
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string product = tb_Product.Text;
            switch (AVI)
            {
                case "1":
                    DirectoryInfo dir_1 = new DirectoryInfo(GlobalVar.gl_val_Machine1_S1_TestVal);
                    DirectoryInfo parent_1 = dir_1.Parent;
                    DirectoryInfo dir_2 = new DirectoryInfo(GlobalVar.gl_val_Machine1_S2_TestVal);
                    DirectoryInfo parent_2 = dir_2.Parent;
                    GlobalVar.gl_val_Machine1_S1_TestVal = parent_1.FullName + @"\" + product;
                    GlobalVar.gl_val_Machine1_S2_TestVal = parent_2.FullName + @"\" + product;
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S1_TestVal, GlobalVar.gl_val_Machine1_S1_TestVal, Application.StartupPath + @"\CONFIG.ini");
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine1_S2_TestVal, GlobalVar.gl_val_Machine1_S2_TestVal, Application.StartupPath + @"\CONFIG.ini");
                    myf.getTestItem(GlobalVar.gl_val_Machine1_S1_TestVal, GlobalVar.AVI1_S1_TestItems);
                    myf.getTestItem(GlobalVar.gl_val_Machine1_S2_TestVal, GlobalVar.AVI1_S2_TestItems);
                    break;
                case "2":
                     dir_1 = new DirectoryInfo(GlobalVar.gl_val_Machine2_S1_TestVal);
                     parent_1 = dir_1.Parent;
                     dir_2 = new DirectoryInfo(GlobalVar.gl_val_Machine2_S2_TestVal);
                     parent_2 = dir_2.Parent;
                    GlobalVar.gl_val_Machine2_S1_TestVal = parent_1.FullName + @"\" + product;
                    GlobalVar.gl_val_Machine2_S2_TestVal = parent_2.FullName + @"\" + product;
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S1_TestVal, GlobalVar.gl_val_Machine2_S1_TestVal, Application.StartupPath + @"\CONFIG.ini");
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine2_S2_TestVal, GlobalVar.gl_val_Machine2_S2_TestVal, Application.StartupPath + @"\CONFIG.ini");
                    myf.getTestItem(GlobalVar.gl_val_Machine2_S1_TestVal, GlobalVar.AVI2_S1_TestItems);
                    myf.getTestItem(GlobalVar.gl_val_Machine2_S2_TestVal, GlobalVar.AVI2_S2_TestItems);
                    break;
                case "3":
                    dir_1 = new DirectoryInfo(GlobalVar.gl_val_Machine3_S1_TestVal);
                    parent_1 = dir_1.Parent;
                    dir_2 = new DirectoryInfo(GlobalVar.gl_val_Machine3_S2_TestVal);
                    parent_2 = dir_2.Parent;
                    GlobalVar.gl_val_Machine3_S1_TestVal = parent_1.FullName + @"\" + product;
                    GlobalVar.gl_val_Machine3_S2_TestVal = parent_2.FullName + @"\" + product;
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S1_TestVal, GlobalVar.gl_val_Machine3_S1_TestVal, Application.StartupPath + @"\CONFIG.ini");
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_section_Global, GlobalVar.gl_key_Machine3_S2_TestVal, GlobalVar.gl_val_Machine3_S2_TestVal, Application.StartupPath + @"\CONFIG.ini");
                    myf.getTestItem(GlobalVar.gl_val_Machine3_S1_TestVal, GlobalVar.AVI3_S1_TestItems);
                    myf.getTestItem(GlobalVar.gl_val_Machine3_S2_TestVal, GlobalVar.AVI3_S2_TestItems);
                    break;
                default:
                    break;
            }
            this.Close();
        }

        private void Change_Load(object sender, EventArgs e)
        {
            
            try
            {
                DirectoryInfo dir = new DirectoryInfo(s1_testitem_Path);
                DirectoryInfo parent = dir.Parent;
                DirectoryInfo[] Products = parent.GetDirectories();
                int i = 0;
                foreach (DirectoryInfo product in Products)
                {
                    i++;
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(product.Name);
                    this.lv_Product.Items.Add(lvi);
                }
            }
            catch (Exception ex) { }
        }

        private void lv_Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedIndexCollection indexes = this.lv_Product.SelectedIndices;
                if (indexes.Count > 0)
                {
                    int index = indexes[0];
                    string sPartNo = this.lv_Product.Items[index].SubItems[0].Text;//获取第一列的值  
                    string product = this.lv_Product.Items[index].SubItems[1].Text;//获取第二列的值  

                    tb_Product.Text = product;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！\n" + ex.Message, "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
