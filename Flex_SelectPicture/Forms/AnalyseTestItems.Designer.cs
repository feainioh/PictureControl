namespace Flex_SelectPicture
{
    partial class AnalyseTestItems
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
            this.dateTimePicker_ANA = new System.Windows.Forms.DateTimePicker();
            this.panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // dateTimePicker_ANA
            // 
            this.dateTimePicker_ANA.Dock = System.Windows.Forms.DockStyle.Top;
            this.dateTimePicker_ANA.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker_ANA.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker_ANA.Name = "dateTimePicker_ANA";
            this.dateTimePicker_ANA.Size = new System.Drawing.Size(950, 26);
            this.dateTimePicker_ANA.TabIndex = 0;
            this.dateTimePicker_ANA.ValueChanged += new System.EventHandler(this.dateTimePicker_ANA_ValueChanged);
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 26);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(950, 518);
            this.panel.TabIndex = 1;
            // 
            // AnalyseTestItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 544);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.dateTimePicker_ANA);
            this.Name = "AnalyseTestItems";
            this.Text = "AnalyseTestItems";
            this.Load += new System.EventHandler(this.AnalyseTestItems_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_ANA;
        private System.Windows.Forms.Panel panel;
    }
}