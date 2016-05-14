namespace Codematic.UserControls
{
    partial class UcCSSet
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
            this.lblNamepace = new System.Windows.Forms.Label();
            this.lblDbHelperName = new System.Windows.Forms.Label();
            this.txtNamepace = new System.Windows.Forms.TextBox();
            this.txtDbHelperName = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radbtn_Frame_One = new System.Windows.Forms.RadioButton();
            this.radbtn_Frame_F3 = new System.Windows.Forms.RadioButton();
            this.radbtn_Frame_S3 = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtProcPrefix = new System.Windows.Forms.TextBox();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNamepace
            // 
            this.lblNamepace.AutoSize = true;
            this.lblNamepace.Location = new System.Drawing.Point(27, 17);
            this.lblNamepace.Name = "lblNamepace";
            this.lblNamepace.Size = new System.Drawing.Size(89, 12);
            this.lblNamepace.TabIndex = 0;
            this.lblNamepace.Text = "顶级命名空间：";
            // 
            // lblDbHelperName
            // 
            this.lblDbHelperName.AutoSize = true;
            this.lblDbHelperName.Location = new System.Drawing.Point(27, 41);
            this.lblDbHelperName.Name = "lblDbHelperName";
            this.lblDbHelperName.Size = new System.Drawing.Size(89, 12);
            this.lblDbHelperName.TabIndex = 0;
            this.lblDbHelperName.Text = "数据访问基类：";
            // 
            // txtNamepace
            // 
            this.txtNamepace.Location = new System.Drawing.Point(126, 13);
            this.txtNamepace.Name = "txtNamepace";
            this.txtNamepace.Size = new System.Drawing.Size(143, 21);
            this.txtNamepace.TabIndex = 2;
            // 
            // txtDbHelperName
            // 
            this.txtDbHelperName.Location = new System.Drawing.Point(126, 37);
            this.txtDbHelperName.Name = "txtDbHelperName";
            this.txtDbHelperName.Size = new System.Drawing.Size(143, 21);
            this.txtDbHelperName.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radbtn_Frame_One);
            this.groupBox6.Controls.Add(this.radbtn_Frame_F3);
            this.groupBox6.Controls.Add(this.radbtn_Frame_S3);
            this.groupBox6.Location = new System.Drawing.Point(13, 113);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(360, 44);
            this.groupBox6.TabIndex = 52;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "默认生成项目架构类型";
            // 
            // radbtn_Frame_One
            // 
            this.radbtn_Frame_One.AutoSize = true;
            this.radbtn_Frame_One.Location = new System.Drawing.Point(21, 18);
            this.radbtn_Frame_One.Name = "radbtn_Frame_One";
            this.radbtn_Frame_One.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Frame_One.TabIndex = 0;
            this.radbtn_Frame_One.Text = "单类结构";
            this.radbtn_Frame_One.UseVisualStyleBackColor = true;
            // 
            // radbtn_Frame_F3
            // 
            this.radbtn_Frame_F3.AutoSize = true;
            this.radbtn_Frame_F3.Checked = true;
            this.radbtn_Frame_F3.Location = new System.Drawing.Point(203, 18);
            this.radbtn_Frame_F3.Name = "radbtn_Frame_F3";
            this.radbtn_Frame_F3.Size = new System.Drawing.Size(95, 16);
            this.radbtn_Frame_F3.TabIndex = 0;
            this.radbtn_Frame_F3.TabStop = true;
            this.radbtn_Frame_F3.Text = "工厂模式三层";
            this.radbtn_Frame_F3.UseVisualStyleBackColor = true;
            // 
            // radbtn_Frame_S3
            // 
            this.radbtn_Frame_S3.AutoSize = true;
            this.radbtn_Frame_S3.Location = new System.Drawing.Point(112, 18);
            this.radbtn_Frame_S3.Name = "radbtn_Frame_S3";
            this.radbtn_Frame_S3.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Frame_S3.TabIndex = 0;
            this.radbtn_Frame_S3.Text = "简单三层";
            this.radbtn_Frame_S3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(13, 161);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(360, 96);
            this.groupBox5.TabIndex = 51;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "选择默认代码模板";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "存储过程前缀：";
            // 
            // txtProjectName
            // 
            this.txtProjectName.AcceptsReturn = true;
            this.txtProjectName.Location = new System.Drawing.Point(126, 61);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(143, 21);
            this.txtProjectName.TabIndex = 2;
            // 
            // txtProcPrefix
            // 
            this.txtProcPrefix.Location = new System.Drawing.Point(126, 85);
            this.txtProcPrefix.Name = "txtProcPrefix";
            this.txtProcPrefix.Size = new System.Drawing.Size(143, 21);
            this.txtProcPrefix.TabIndex = 2;
            // 
            // UcCSSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.txtProcPrefix);
            this.Controls.Add(this.txtDbHelperName);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.txtNamepace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDbHelperName);
            this.Controls.Add(this.lblNamepace);
            this.Name = "UcCSSet";
            this.Size = new System.Drawing.Size(381, 273);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblNamepace;
        public System.Windows.Forms.Label lblDbHelperName;
        public System.Windows.Forms.TextBox txtNamepace;
        public System.Windows.Forms.TextBox txtDbHelperName;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.RadioButton radbtn_Frame_One;
        public System.Windows.Forms.RadioButton radbtn_Frame_F3;
        public System.Windows.Forms.RadioButton radbtn_Frame_S3;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtProjectName;
        public System.Windows.Forms.TextBox txtProcPrefix;
    }
}
