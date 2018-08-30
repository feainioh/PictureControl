namespace Flex_SelectPicture
{
    partial class Load
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_Machine3 = new System.Windows.Forms.CheckBox();
            this.checkBox_Machine2 = new System.Windows.Forms.CheckBox();
            this.checkBox_Machine1 = new System.Windows.Forms.CheckBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(56, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 62);
            this.label1.TabIndex = 0;
            this.label1.Text = "FLEX数据查询";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_Machine3);
            this.groupBox1.Controls.Add(this.checkBox_Machine2);
            this.groupBox1.Controls.Add(this.checkBox_Machine1);
            this.groupBox1.Location = new System.Drawing.Point(40, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // checkBox_Machine3
            // 
            this.checkBox_Machine3.AutoSize = true;
            this.checkBox_Machine3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_Machine3.Location = new System.Drawing.Point(266, 25);
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
            this.checkBox_Machine2.Location = new System.Drawing.Point(146, 25);
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
            this.checkBox_Machine1.Location = new System.Drawing.Point(31, 25);
            this.checkBox_Machine1.Name = "checkBox_Machine1";
            this.checkBox_Machine1.Size = new System.Drawing.Size(65, 24);
            this.checkBox_Machine1.TabIndex = 3;
            this.checkBox_Machine1.Text = "AVI-1";
            this.checkBox_Machine1.UseVisualStyleBackColor = true;
            this.checkBox_Machine1.CheckedChanged += new System.EventHandler(this.checkBox_Machine1_CheckedChanged);
            // 
            // btn_OK
            // 
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OK.Location = new System.Drawing.Point(71, 187);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(86, 35);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancel.Location = new System.Drawing.Point(285, 187);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(86, 35);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(436, 242);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Load";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load";
            this.Load += new System.EventHandler(this.Load_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.CheckBox checkBox_Machine3;
        private System.Windows.Forms.CheckBox checkBox_Machine2;
        private System.Windows.Forms.CheckBox checkBox_Machine1;
    }
}