namespace Codematic.UserControls
{
    partial class UcCSNameSet
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTabModel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtModel_Suffix = new System.Windows.Forms.TextBox();
            this.txtModel_Prefix = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBLL_Prefix = new System.Windows.Forms.TextBox();
            this.lblTabBLL = new System.Windows.Forms.Label();
            this.txtBLL_Suffix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDAL_Prefix = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTabDAL = new System.Windows.Forms.Label();
            this.txtDAL_Suffix = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radbtn_firstUpper = new System.Windows.Forms.RadioButton();
            this.radbtn_Lower = new System.Windows.Forms.RadioButton();
            this.radbtn_Upper = new System.Windows.Forms.RadioButton();
            this.radbtn_Same = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblTabModel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtModel_Suffix);
            this.groupBox1.Controls.Add(this.txtModel_Prefix);
            this.groupBox1.Location = new System.Drawing.Point(12, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model类命名规则";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "+";
            // 
            // lblTabModel
            // 
            this.lblTabModel.AutoSize = true;
            this.lblTabModel.Location = new System.Drawing.Point(153, 25);
            this.lblTabModel.Name = "lblTabModel";
            this.lblTabModel.Size = new System.Drawing.Size(29, 12);
            this.lblTabModel.TabIndex = 1;
            this.lblTabModel.Text = "表名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "+";
            // 
            // txtModel_Suffix
            // 
            this.txtModel_Suffix.Location = new System.Drawing.Point(221, 21);
            this.txtModel_Suffix.Name = "txtModel_Suffix";
            this.txtModel_Suffix.Size = new System.Drawing.Size(65, 21);
            this.txtModel_Suffix.TabIndex = 0;
            // 
            // txtModel_Prefix
            // 
            this.txtModel_Prefix.Location = new System.Drawing.Point(49, 21);
            this.txtModel_Prefix.Name = "txtModel_Prefix";
            this.txtModel_Prefix.Size = new System.Drawing.Size(65, 21);
            this.txtModel_Prefix.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtBLL_Prefix);
            this.groupBox2.Controls.Add(this.lblTabBLL);
            this.groupBox2.Controls.Add(this.txtBLL_Suffix);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "BLL层类命名规则";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "+";
            // 
            // txtBLL_Prefix
            // 
            this.txtBLL_Prefix.Location = new System.Drawing.Point(49, 20);
            this.txtBLL_Prefix.Name = "txtBLL_Prefix";
            this.txtBLL_Prefix.Size = new System.Drawing.Size(65, 21);
            this.txtBLL_Prefix.TabIndex = 0;
            // 
            // lblTabBLL
            // 
            this.lblTabBLL.AutoSize = true;
            this.lblTabBLL.Location = new System.Drawing.Point(153, 24);
            this.lblTabBLL.Name = "lblTabBLL";
            this.lblTabBLL.Size = new System.Drawing.Size(29, 12);
            this.lblTabBLL.TabIndex = 1;
            this.lblTabBLL.Text = "表名";
            // 
            // txtBLL_Suffix
            // 
            this.txtBLL_Suffix.Location = new System.Drawing.Point(221, 20);
            this.txtBLL_Suffix.Name = "txtBLL_Suffix";
            this.txtBLL_Suffix.Size = new System.Drawing.Size(65, 21);
            this.txtBLL_Suffix.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "+";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtDAL_Prefix);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblTabDAL);
            this.groupBox3.Controls.Add(this.txtDAL_Suffix);
            this.groupBox3.Location = new System.Drawing.Point(12, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(354, 50);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DAL层类命名规则";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(196, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "+";
            // 
            // txtDAL_Prefix
            // 
            this.txtDAL_Prefix.Location = new System.Drawing.Point(49, 20);
            this.txtDAL_Prefix.Name = "txtDAL_Prefix";
            this.txtDAL_Prefix.Size = new System.Drawing.Size(65, 21);
            this.txtDAL_Prefix.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(128, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "+";
            // 
            // lblTabDAL
            // 
            this.lblTabDAL.AutoSize = true;
            this.lblTabDAL.Location = new System.Drawing.Point(153, 24);
            this.lblTabDAL.Name = "lblTabDAL";
            this.lblTabDAL.Size = new System.Drawing.Size(29, 12);
            this.lblTabDAL.TabIndex = 1;
            this.lblTabDAL.Text = "表名";
            // 
            // txtDAL_Suffix
            // 
            this.txtDAL_Suffix.Location = new System.Drawing.Point(221, 20);
            this.txtDAL_Suffix.Name = "txtDAL_Suffix";
            this.txtDAL_Suffix.Size = new System.Drawing.Size(65, 21);
            this.txtDAL_Suffix.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radbtn_firstUpper);
            this.groupBox4.Controls.Add(this.radbtn_Lower);
            this.groupBox4.Controls.Add(this.radbtn_Upper);
            this.groupBox4.Controls.Add(this.radbtn_Same);
            this.groupBox4.Location = new System.Drawing.Point(12, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(354, 58);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "表名规则";
            // 
            // radbtn_firstUpper
            // 
            this.radbtn_firstUpper.AutoSize = true;
            this.radbtn_firstUpper.Location = new System.Drawing.Point(264, 25);
            this.radbtn_firstUpper.Name = "radbtn_firstUpper";
            this.radbtn_firstUpper.Size = new System.Drawing.Size(83, 16);
            this.radbtn_firstUpper.TabIndex = 0;
            this.radbtn_firstUpper.Text = "首字母大写";
            this.radbtn_firstUpper.UseVisualStyleBackColor = true;
            // 
            // radbtn_Lower
            // 
            this.radbtn_Lower.AutoSize = true;
            this.radbtn_Lower.Location = new System.Drawing.Point(177, 25);
            this.radbtn_Lower.Name = "radbtn_Lower";
            this.radbtn_Lower.Size = new System.Drawing.Size(83, 16);
            this.radbtn_Lower.TabIndex = 0;
            this.radbtn_Lower.Text = "表名全小写";
            this.radbtn_Lower.UseVisualStyleBackColor = true;
            // 
            // radbtn_Upper
            // 
            this.radbtn_Upper.AutoSize = true;
            this.radbtn_Upper.Location = new System.Drawing.Point(90, 25);
            this.radbtn_Upper.Name = "radbtn_Upper";
            this.radbtn_Upper.Size = new System.Drawing.Size(83, 16);
            this.radbtn_Upper.TabIndex = 0;
            this.radbtn_Upper.Text = "表名全大写";
            this.radbtn_Upper.UseVisualStyleBackColor = true;
            // 
            // radbtn_Same
            // 
            this.radbtn_Same.AutoSize = true;
            this.radbtn_Same.Checked = true;
            this.radbtn_Same.Location = new System.Drawing.Point(15, 25);
            this.radbtn_Same.Name = "radbtn_Same";
            this.radbtn_Same.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Same.TabIndex = 0;
            this.radbtn_Same.TabStop = true;
            this.radbtn_Same.Text = "表名不变";
            this.radbtn_Same.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "注：默认情况下，将以表名作为类名生成，可以通过下面设置更改。";
            // 
            // UcCSNameSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcCSNameSet";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(381, 273);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTabModel;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtModel_Suffix;
        public System.Windows.Forms.TextBox txtModel_Prefix;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtBLL_Prefix;
        private System.Windows.Forms.Label lblTabBLL;
        public System.Windows.Forms.TextBox txtBLL_Suffix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtDAL_Prefix;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTabDAL;
        public System.Windows.Forms.TextBox txtDAL_Suffix;
        public System.Windows.Forms.RadioButton radbtn_Lower;
        public System.Windows.Forms.RadioButton radbtn_Upper;
        public System.Windows.Forms.RadioButton radbtn_Same;
        public System.Windows.Forms.RadioButton radbtn_firstUpper;
        private System.Windows.Forms.Label label2;
    }
}
