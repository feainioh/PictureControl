using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture
{
    public partial class CountYeild : Form
    {
        AVI_Yeild avi1, avi2, avi3;
        public CountYeild()
        {
                InitializeComponent();
        }

        private void CountYeild_Load(object sender, EventArgs e)
        {
            LoadYeild load = new LoadYeild();
            if (load.ShowDialog() == DialogResult.OK)
                loadControls();
           else
            {
                this.Close();
                return;
            }
        }

        private void loadControls()
        {
            try
            {
                if (GlobalVar.gl_val_check_AVI1 == "1")
                {
                    if (GlobalVar.gl_val_check_stations == "1")
                    {
                        TabPage tp_1 = new TabPage();
                        tp_1.Name = "tp_1";
                        tp_1.Text = "AVI-1";
                        tabControl_yeild.Controls.Add(tp_1);
                        avi1 = new AVI_Yeild("AVI-1", int.Parse(GlobalVar.gl_val_check_stationscount));
                        avi1.Parent = tp_1;
                        avi1.Dock = DockStyle.Fill;
                        tp_1.Controls.Add(avi1);
                        //tp_1.Controls.Add(new Button());
                    }
                }
                if (GlobalVar.gl_val_check_AVI2 == "1")
                {
                    if (GlobalVar.gl_val_check_stations == "1")
                    {
                        TabPage tp_2 = new TabPage();
                        tp_2.Name = "tp_2";
                        tp_2.Text = "AVI-2";
                        tabControl_yeild.Controls.Add(tp_2);
                        avi2 = new AVI_Yeild("AVI-2", int.Parse(GlobalVar.gl_val_check_stationscount));
                        avi2.Parent = tp_2;
                        avi2.Dock = DockStyle.Fill;
                        tp_2.Controls.Add(avi2);
                    }
                }
                if (GlobalVar.gl_val_check_AVI3 == "1")
                {
                    if (GlobalVar.gl_val_check_stations == "1")
                    {
                        TabPage tp_3 = new TabPage();
                        tp_3.Name = "tp_3";
                        tp_3.Text = "AVI-3";
                        tabControl_yeild.Controls.Add(tp_3);
                        avi3 = new AVI_Yeild("AVI-3", int.Parse(GlobalVar.gl_val_check_stationscount));
                        avi3.Parent = tp_3;
                        avi3.Dock = DockStyle.Fill;
                        tp_3.Controls.Add(avi3);
                    }
                }
                dtp_analyDate.Value = GlobalVar.gl_check_checkDate;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (GlobalVar.gl_check_checkDate != dtp_analyDate.Value)
            {
                GlobalVar.gl_check_checkDate = dtp_analyDate.Value;
                if (GlobalVar.gl_val_check_AVI1 == "1") avi1.changeDate("AVI-1");
                if (GlobalVar.gl_val_check_AVI2 == "1") avi2.changeDate("AVI-2");
                if (GlobalVar.gl_val_check_AVI3 == "1") avi3.changeDate("AVI-3");

            }
        }
    }
}
