namespace Flex_SelectPicture
{
    partial class LoadYeild
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_checkDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown_stationcount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_station = new System.Windows.Forms.CheckBox();
            this.checkBox_Machine3 = new System.Windows.Forms.CheckBox();
            this.checkBox_Machine2 = new System.Windows.Forms.CheckBox();
            this.checkBox_Machine1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_stationcount)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancel.Location = new System.Drawing.Point(227, 225);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(86, 35);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OK.Location = new System.Drawing.Point(10, 225);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(86, 35);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_checkDate);
            this.groupBox1.Controls.Add(this.numericUpDown_stationcount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox_station);
            this.groupBox1.Controls.Add(this.checkBox_Machine3);
            this.groupBox1.Controls.Add(this.checkBox_Machine2);
            this.groupBox1.Controls.Add(this.checkBox_Machine1);
            this.groupBox1.Location = new System.Drawing.Point(10, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 155);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // dtp_checkDate
            // 
            this.dtp_checkDate.Location = new System.Drawing.Point(151, 122);
            this.dtp_checkDate.Name = "dtp_checkDate";
            this.dtp_checkDate.Size = new System.Drawing.Size(122, 21);
            this.dtp_checkDate.TabIndex = 10;
            // 
            // numericUpDown_stationcount
            // 
            this.numericUpDown_stationcount.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown_stationcount.Location = new System.Drawing.Point(217, 66);
            this.numericUpDown_stationcount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_stationcount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_stationcount.Name = "numericUpDown_stationcount";
            this.numericUpDown_stationcount.Size = new System.Drawing.Size(56, 26);
            this.numericUpDown_stationcount.TabIndex = 9;
            this.numericUpDown_stationcount.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_stationcount.Visible = false;
            this.numericUpDown_stationcount.ValueChanged += new System.EventHandler(this.numericUpDown_stationcount_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(147, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "工站数量:";
            this.label2.Visible = false;
            // 
            // checkBox_station
            // 
            this.checkBox_station.AutoSize = true;
            this.checkBox_station.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_station.Location = new System.Drawing.Point(151, 20);
            this.checkBox_station.Name = "checkBox_station";
            this.checkBox_station.Size = new System.Drawing.Size(84, 24);
            this.checkBox_station.TabIndex = 6;
            this.checkBox_station.Text = "分析工站";
            this.checkBox_station.UseVisualStyleBackColor = true;
            this.checkBox_station.CheckedChanged += new System.EventHandler(this.checkBox_station_CheckedChanged);
            // 
            // checkBox_Machine3
            // 
            this.checkBox_Machine3.AutoSize = true;
            this.checkBox_Machine3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_Machine3.Location = new System.Drawing.Point(21, 122);
            this.checkBox_Machine3.Name = "checkBox_Machine3";
            this.checkBox_Machine3.Size = new System.Drawing.Size(65, 24);
            this.checkBox_Machine3.TabIndex = 5;
            this.checkBox_Machine3.Text = "AVI-3";
            this.checkBox_Machine3.UseVisualStyleBackColor = true;
            this.checkBox_Machine3.CheckedChanged += new System.EventHandler(this.checkBox_Machine3_CheckedChanged);
            // 
            // checkBox_Machine2
            // 
            this.checkBox_Machine2.AutoSize = true;
            this.checkBox_Machine2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_Machine2.Location = new System.Drawing.Point(21, 68);
            this.checkBox_Machine2.Name = "checkBox_Machine2";
            this.checkBox_Machine2.Size = new System.Drawing.Size(65, 24);
            this.checkBox_Machine2.TabIndex = 4;
            this.checkBox_Machine2.Text = "AVI-2";
            this.checkBox_Machine2.UseVisualStyleBackColor = true;
            this.checkBox_Machine2.CheckedChanged += new System.EventHandler(this.checkBox_Machine2_CheckedChanged);
            // 
            // checkBox_Machine1
            // 
            this.checkBox_Machine1.AutoSize = true;
            this.checkBox_Machine1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_Machine1.Location = new System.Drawing.Point(21, 20);
            this.checkBox_Machine1.Name = "checkBox_Machine1";
            this.checkBox_Machine1.Size = new System.Drawing.Size(65, 24);
            this.checkBox_Machine1.TabIndex = 3;
            this.checkBox_Machine1.Text = "AVI-1";
            this.checkBox_Machine1.UseVisualStyleBackColor = true;
            this.checkBox_Machine1.CheckedChanged += new System.EventHandler(this.checkBox_Machine1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-1, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 62);
            this.label1.TabIndex = 4;
            this.label1.Text = "FLEX良率分析";
            // 
            // LoadYeild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(325, 266);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadYeild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoadYeild";
            this.Load += new System.EventHandler(this.LoadYeild_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_stationcount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_Machine3;
        private System.Windows.Forms.CheckBox checkBox_Machine2;
        private System.Windows.Forms.CheckBox checkBox_Machine1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_station;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_stationcount;
        private System.Windows.Forms.DateTimePicker dtp_checkDate;
    }
}