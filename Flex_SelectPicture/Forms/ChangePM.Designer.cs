namespace Flex_SelectPicture
{
    partial class ChangePM
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lv_Product = new System.Windows.Forms.ListView();
            this.columnHeader_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Product = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tb_Product = new System.Windows.Forms.TextBox();
            this.btn_cancel = new MyControls.CrystalButton();
            this.btn_OK = new MyControls.CrystalButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_Product
            // 
            this.lv_Product.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lv_Product.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_No,
            this.columnHeader_Product});
            this.lv_Product.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lv_Product.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lv_Product.FullRowSelect = true;
            this.lv_Product.GridLines = true;
            this.lv_Product.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_Product.Location = new System.Drawing.Point(0, 33);
            this.lv_Product.MultiSelect = false;
            this.lv_Product.Name = "lv_Product";
            this.lv_Product.Size = new System.Drawing.Size(347, 257);
            this.lv_Product.TabIndex = 0;
            this.lv_Product.UseCompatibleStateImageBehavior = false;
            this.lv_Product.View = System.Windows.Forms.View.Details;
            this.lv_Product.SelectedIndexChanged += new System.EventHandler(this.lv_Product_SelectedIndexChanged);
            // 
            // columnHeader_No
            // 
            this.columnHeader_No.Text = "序号";
            this.columnHeader_No.Width = 51;
            // 
            // columnHeader_Product
            // 
            this.columnHeader_Product.Text = "品目";
            this.columnHeader_Product.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_Product.Width = 308;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tb_Product);
            this.splitContainer1.Panel1.Controls.Add(this.lv_Product);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer1.Panel2.Controls.Add(this.btn_OK);
            this.splitContainer1.Size = new System.Drawing.Size(347, 352);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 1;
            // 
            // tb_Product
            // 
            this.tb_Product.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_Product.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_Product.Location = new System.Drawing.Point(0, 0);
            this.tb_Product.Name = "tb_Product";
            this.tb_Product.ReadOnly = true;
            this.tb_Product.Size = new System.Drawing.Size(347, 33);
            this.tb_Product.TabIndex = 1;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Red;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.btn_cancel.Location = new System.Drawing.Point(205, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(130, 53);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "CANCEL";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.btn_OK.Location = new System.Drawing.Point(12, 3);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(130, 53);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ChangePM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 352);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChangePM";
            this.Load += new System.EventHandler(this.Change_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_Product;
        private System.Windows.Forms.ColumnHeader columnHeader_No;
        private System.Windows.Forms.ColumnHeader columnHeader_Product;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tb_Product;
        private MyControls.CrystalButton btn_OK;
        private MyControls.CrystalButton btn_cancel;
    }
}
