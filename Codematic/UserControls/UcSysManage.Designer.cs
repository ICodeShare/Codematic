namespace Codematic.UserControls
{
    partial class UcSysManage
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
            this.groupBox_DBO = new System.Windows.Forms.GroupBox();
            this.radbtnDBO_SP = new System.Windows.Forms.RadioButton();
            this.radbtnDBO_SQL = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkLoginfo = new System.Windows.Forms.CheckBox();
            this.groupBox_DBO.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_DBO
            // 
            this.groupBox_DBO.Controls.Add(this.radbtnDBO_SP);
            this.groupBox_DBO.Controls.Add(this.radbtnDBO_SQL);
            this.groupBox_DBO.Location = new System.Drawing.Point(12, 5);
            this.groupBox_DBO.Name = "groupBox_DBO";
            this.groupBox_DBO.Size = new System.Drawing.Size(342, 55);
            this.groupBox_DBO.TabIndex = 0;
            this.groupBox_DBO.TabStop = false;
            this.groupBox_DBO.Text = "DBO方式";
            // 
            // radbtnDBO_SP
            // 
            this.radbtnDBO_SP.AutoSize = true;
            this.radbtnDBO_SP.Location = new System.Drawing.Point(164, 20);
            this.radbtnDBO_SP.Name = "radbtnDBO_SP";
            this.radbtnDBO_SP.Size = new System.Drawing.Size(59, 16);
            this.radbtnDBO_SP.TabIndex = 0;
            this.radbtnDBO_SP.Text = "SP方式";
            this.radbtnDBO_SP.UseVisualStyleBackColor = true;
            // 
            // radbtnDBO_SQL
            // 
            this.radbtnDBO_SQL.AutoSize = true;
            this.radbtnDBO_SQL.Checked = true;
            this.radbtnDBO_SQL.Location = new System.Drawing.Point(35, 20);
            this.radbtnDBO_SQL.Name = "radbtnDBO_SQL";
            this.radbtnDBO_SQL.Size = new System.Drawing.Size(71, 16);
            this.radbtnDBO_SQL.TabIndex = 0;
            this.radbtnDBO_SQL.TabStop = true;
            this.radbtnDBO_SQL.Text = "默认方式";
            this.radbtnDBO_SQL.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLoginfo);
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 65);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "软件设置";
            // 
            // chkLoginfo
            // 
            this.chkLoginfo.AutoSize = true;
            this.chkLoginfo.Checked = true;
            this.chkLoginfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoginfo.Location = new System.Drawing.Point(23, 21);
            this.chkLoginfo.Name = "chkLoginfo";
            this.chkLoginfo.Size = new System.Drawing.Size(96, 16);
            this.chkLoginfo.TabIndex = 0;
            this.chkLoginfo.Text = "记录错误日志";
            this.chkLoginfo.UseVisualStyleBackColor = true;
            // 
            // UcSysManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_DBO);
            this.Name = "UcSysManage";
            this.Size = new System.Drawing.Size(368, 267);
            this.groupBox_DBO.ResumeLayout(false);
            this.groupBox_DBO.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_DBO;
        private System.Windows.Forms.RadioButton radbtnDBO_SP;
        private System.Windows.Forms.RadioButton radbtnDBO_SQL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkLoginfo;
    }
}
