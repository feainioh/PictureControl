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
    public partial class AnalyseTestItems : Form
    {
        private string AVI;

        public AnalyseTestItems()
        {
            InitializeComponent();
        }
        AVI_Analysis analysist;
        public AnalyseTestItems(string v)
        {
            InitializeComponent();
            this.AVI = v;
            this.Text += "AVI-"+AVI;
        }

        private void AnalyseTestItems_Load(object sender, EventArgs e)
        {
            AVI_Analysis aVI_Analysis = new AVI_Analysis(AVI, dateTimePicker_ANA.Value);
            analysist = aVI_Analysis;
            analysist.Dock = DockStyle.Fill;
            panel.Controls.Add(analysist);
        }

        private void dateTimePicker_ANA_ValueChanged(object sender, EventArgs e)
        {
            if(analysist!=null)
            analysist.Analysis_DateChange(dateTimePicker_ANA.Value);
            else
            {
                AVI_Analysis aVI_Analysis = new AVI_Analysis(AVI, dateTimePicker_ANA.Value);
                analysist = aVI_Analysis;
                analysist.Dock = DockStyle.Fill;
                panel.Controls.Add(analysist);
            }
        }
    }
}
