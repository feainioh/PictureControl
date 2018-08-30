using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture.Forms
{
    public partial class MsgBox : Form
    {
        public MsgBox(DateTime searchdate, string avi)
        {
            search_date = searchdate;
            AVI = avi;
            InitializeComponent();
        }

        public static DateTime search_date;
        string AVI = string.Empty;


        private void MsgBox_Load(object sender, EventArgs e)
        {
              MySearch mys = new MySearch( search_date, AVI);
            mys.SearchStrUseTh();
            while (true)
            {
                if (mys.complete)
                {
                    this.BeginInvoke(new Action(() => {
                        this.Close();
                    }));
                    break;
                }
                Thread.Sleep(10);
            }
        }

    }
}
