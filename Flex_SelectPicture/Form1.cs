using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flex_SelectPicture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int begin_x;        //图片开始位置
        private int begin_y;


        private Image image_ori;    //最原始的图片
        //private Image image_dest;   //经缩放后的图片

        private float zoom;           //缩小放大百份比，每10%为一个阶梯。每次缩放都基于最原始的图片

        private Point m_StarPoint = Point.Empty;        //for 拖动
        private Point m_ViewPoint = Point.Empty;
        private bool m_StarMove = false;

        int w;                      //缩放后的图片大小
        int h;


        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "*.jpg|*.jpg;*.jpeg";
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
               // pictureBox1.Image= image_ori = Image.FromFile(openFileDlg.FileName);
                pictureZoom1.PictureLocation = openFileDlg.FileName;
                begin_x = 0;
                begin_y = 0;
                zoom = 30;
               // zoom_image(false);
            }
            openFileDlg.Dispose();
        }

        ///// <summary>
        ///// 缩放最原始的图片到image_dest
        ///// </summary>
        //private void zoom_image(bool chec)
        //{
        //    try
        //    {
        //        w = Convert.ToInt32(image_ori.Width * zoom / 100);
        //        h = Convert.ToInt32(image_ori.Height * zoom / 100);
        //        if (w < 1 || h < 1) return;
        //        if (chec)
        //        {
        //            if (begin_x + pictureBox1.Width > w) begin_x = w - pictureBox1.Width;
        //            if (begin_y + pictureBox1.Height > h) begin_y = h - pictureBox1.Height;
        //            if (begin_x < 0) begin_x = 0;
        //            if (begin_y < 0) begin_y = 0;
        //        } 
        //        Bitmap resizedBmp = new Bitmap(w, h);
        //        Graphics g = Graphics.FromImage(resizedBmp);
        //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        g.DrawImage(image_ori, new Rectangle(0,0, w, h), new Rectangle(0,0, image_ori.Width, image_ori.Height), GraphicsUnit.Pixel);

        //        int ww, hh;
        //        ww = w;
        //        hh = h;
        //        if (pictureBox1.Width < ww) ww = pictureBox1.Width;
        //        if (pictureBox1.Height < hh) hh = pictureBox1.Height;

        //        pictureBox1.Image = resizedBmp.Clone(new RectangleF(begin_x, begin_y, ww, hh), PixelFormat.Format24bppRgb);   //在图片框上显示区域图片
        //         g.Dispose();                                                                                                                            // pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        //    }
        //    catch (Exception ex)
        //    {
        //        return;
        //    }
        //}

        //#region  拖动
        //private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Cursor = Cursors.Hand;
        //    m_StarMove = true;
        //    m_StarPoint = e.Location;
        //}

        //private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    m_StarMove = true;
        //}

        //private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (m_StarMove)
        //    {
        //        int x, y;
        //        x = m_StarPoint.X - e.X;
        //        y = m_StarPoint.Y - e.Y;

        //        if (x > 0)
        //        {
        //            if (begin_x + x <= w - pictureBox1.Width) begin_x += x;
        //            else begin_x = w - pictureBox1.Width;
        //        }
        //        else
        //        {
        //            if (begin_x + x <= 0) begin_x = 0;
        //            else begin_x += x;
        //        }
        //        if (y > 0)
        //        {
        //            if (begin_y + y <= h - pictureBox1.Height) begin_y += y;
        //            else begin_y = h - pictureBox1.Height;
        //        }
        //        else
        //        {
        //            if (begin_y + y <= 0) begin_y = 0;
        //            else begin_y += y;
        //        }
        //        //this.pictureBox1.Location = new Point(begin_x,begin_y);
        //        zoom_image(false);
        //    }
        //    m_StarMove = false;
        //}
        //#endregion

        //#region 缩放 
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    float min = pictureBox1.Width / image_ori.Width;
        //    float min_y = pictureBox1.Height / image_ori.Height;
        //    if (min < min_y) min = min_y;
        //    else min = min_y;

        //    zoom -= 10;
        //    if (zoom < min) zoom = min;
        //    zoom_image(true);
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    float min = pictureBox1.Width / image_ori.Width;
        //    float min_y = pictureBox1.Height / image_ori.Height;
        //    if (min < min_y) min = min_y;
        //    else min = min_y;

        //    zoom += 10;
        //    if (zoom > 200) zoom = 200;
        //    zoom_image(true);
        //}
        //#endregion
        //[DllImport("user32.dll")]
        //public static extern int WindowFromPoint(int xPoint, int yPoint);
        private void Form1_Load(object sender, EventArgs e)
        {
           // this.MouseWheel += Form1_MouseWheel;
        }

        //private void Form1_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        System.Drawing.Point p = PointToScreen(e.Location);
        //        if (WindowFromPoint(p.X, p.Y) == pictureBox1.Handle.ToInt32())
        //        {
        //            //向前
        //            if (e.Delta > 0)
        //            {
        //                float min = pictureBox1.Width / image_ori.Width;
        //                float min_y = pictureBox1.Height / image_ori.Height;
        //                if (min < min_y) min = min_y;
        //                else min = min_y;
        //                if (pictureBox1.Height >= Screen.PrimaryScreen.Bounds.Height * 10)
        //                    return;

        //                zoom += 10;
        //                if (zoom > 200) zoom = 200;
        //                zoom_image(true);
        //            }
        //            //向后
        //            else if (e.Delta < 0)
        //            {
        //                float min = pictureBox1.Width / image_ori.Width;
        //                float min_y = pictureBox1.Height / image_ori.Height;
        //                if (min < min_y) min = min_y;
        //                else min = min_y;

        //                zoom -= 10;
        //                if (zoom < min) zoom = min;
        //                zoom_image(true);
        //            }
        //        }
        //    }
        //    catch { }
        //}
    }
}
