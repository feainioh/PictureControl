namespace Flex_SelectPicture
{
    partial class CountYeild
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtp_analyDate = new System.Windows.Forms.DateTimePicker();
            this.tabControl_yeild = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // dtp_analyDate
            // 
            this.dtp_analyDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtp_analyDate.Location = new System.Drawing.Point(0, 0);
            this.dtp_analyDate.Name = "dtp_analyDate";
            this.dtp_analyDate.Size = new System.Drawing.Size(916, 21);
            this.dtp_analyDate.TabIndex = 0;
            this.dtp_analyDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // tabControl_yeild
            // 
            this.tabControl_yeild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_yeild.Location = new System.Drawing.Point(0, 21);
            this.tabControl_yeild.Name = "tabControl_yeild";
            this.tabControl_yeild.SelectedIndex = 0;
            this.tabControl_yeild.Size = new System.Drawing.Size(916, 622);
            this.tabControl_yeild.TabIndex = 1;
            // 
            // CountYeild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 643);
            this.Controls.Add(this.tabControl_yeild);
            this.Controls.Add(this.dtp_analyDate);
            this.Name = "CountYeild";
            this.Text = "CountYeild";
            this.Load += new System.EventHandler(this.CountYeild_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_analyDate;
        private System.Windows.Forms.TabControl tabControl_yeild;
    }
}