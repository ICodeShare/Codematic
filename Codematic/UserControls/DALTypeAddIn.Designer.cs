namespace Codematic.UserControls
{
    partial class DALTypeAddIn
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbox_DALType = new System.Windows.Forms.ComboBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbox_DALType
            // 
            this.cmbox_DALType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_DALType.FormattingEnabled = true;
            this.cmbox_DALType.Location = new System.Drawing.Point(24, 0);
            this.cmbox_DALType.Name = "cmbox_DALType";
            this.cmbox_DALType.Size = new System.Drawing.Size(186, 20);
            this.cmbox_DALType.TabIndex = 0;
            this.cmbox_DALType.SelectedIndexChanged += new System.EventHandler(this.cmbox_DALType_SelectedIndexChanged);
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(214, 4);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(101, 12);
            this.lblTip.TabIndex = 1;
            this.lblTip.Text = "请先选择代码类型";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(23, 12);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "DAL";
            // 
            // DALTypeAddIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cmbox_DALType);
            this.Controls.Add(this.lblTip);
            this.Name = "DALTypeAddIn";
            this.Size = new System.Drawing.Size(345, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbox_DALType;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblTitle;
    }
}
