namespace Codematic.UserControls
{
    partial class UcCSMyCode
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
            this.radbtn_Model = new System.Windows.Forms.RadioButton();
            this.radbtn_DAL = new System.Windows.Forms.RadioButton();
            this.radbtn_BLL = new System.Windows.Forms.RadioButton();
            this.txt_Content = new System.Windows.Forms.RichTextBox();
            this.radbtn_using = new System.Windows.Forms.RadioButton();
            this.radbtn_Note = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radbtn_Model
            // 
            this.radbtn_Model.AutoSize = true;
            this.radbtn_Model.Location = new System.Drawing.Point(61, 20);
            this.radbtn_Model.Name = "radbtn_Model";
            this.radbtn_Model.Size = new System.Drawing.Size(53, 16);
            this.radbtn_Model.TabIndex = 0;
            this.radbtn_Model.TabStop = true;
            this.radbtn_Model.Text = "Model";
            this.radbtn_Model.UseVisualStyleBackColor = true;
            // 
            // radbtn_DAL
            // 
            this.radbtn_DAL.AutoSize = true;
            this.radbtn_DAL.Location = new System.Drawing.Point(144, 20);
            this.radbtn_DAL.Name = "radbtn_DAL";
            this.radbtn_DAL.Size = new System.Drawing.Size(41, 16);
            this.radbtn_DAL.TabIndex = 0;
            this.radbtn_DAL.TabStop = true;
            this.radbtn_DAL.Text = "DAL";
            this.radbtn_DAL.UseVisualStyleBackColor = true;
            // 
            // radbtn_BLL
            // 
            this.radbtn_BLL.AutoSize = true;
            this.radbtn_BLL.Location = new System.Drawing.Point(215, 20);
            this.radbtn_BLL.Name = "radbtn_BLL";
            this.radbtn_BLL.Size = new System.Drawing.Size(41, 16);
            this.radbtn_BLL.TabIndex = 0;
            this.radbtn_BLL.TabStop = true;
            this.radbtn_BLL.Text = "BLL";
            this.radbtn_BLL.UseVisualStyleBackColor = true;
            // 
            // txt_Content
            // 
            this.txt_Content.Location = new System.Drawing.Point(32, 127);
            this.txt_Content.Name = "txt_Content";
            this.txt_Content.Size = new System.Drawing.Size(311, 98);
            this.txt_Content.TabIndex = 2;
            this.txt_Content.Text = "";
            // 
            // radbtn_using
            // 
            this.radbtn_using.AutoSize = true;
            this.radbtn_using.Location = new System.Drawing.Point(44, 20);
            this.radbtn_using.Name = "radbtn_using";
            this.radbtn_using.Size = new System.Drawing.Size(83, 16);
            this.radbtn_using.TabIndex = 0;
            this.radbtn_using.TabStop = true;
            this.radbtn_using.Text = "自定义引用";
            this.radbtn_using.UseVisualStyleBackColor = true;
            // 
            // radbtn_Note
            // 
            this.radbtn_Note.AutoSize = true;
            this.radbtn_Note.Location = new System.Drawing.Point(137, 20);
            this.radbtn_Note.Name = "radbtn_Note";
            this.radbtn_Note.Size = new System.Drawing.Size(167, 16);
            this.radbtn_Note.TabIndex = 0;
            this.radbtn_Note.TabStop = true;
            this.radbtn_Note.Text = "自定义注释(无需注释符号)";
            this.radbtn_Note.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radbtn_DAL);
            this.groupBox1.Controls.Add(this.radbtn_Model);
            this.groupBox1.Controls.Add(this.radbtn_BLL);
            this.groupBox1.Location = new System.Drawing.Point(32, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "代码类型：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radbtn_using);
            this.groupBox2.Controls.Add(this.radbtn_Note);
            this.groupBox2.Location = new System.Drawing.Point(32, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 46);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "添加类型：";
            // 
            // UcCSMyCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Content);
            this.Name = "UcCSMyCode";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(381, 273);
            this.Load += new System.EventHandler(this.UcCSMyCode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radbtn_Model;
        private System.Windows.Forms.RadioButton radbtn_DAL;
        private System.Windows.Forms.RadioButton radbtn_BLL;
        private System.Windows.Forms.RichTextBox txt_Content;
        private System.Windows.Forms.RadioButton radbtn_using;
        private System.Windows.Forms.RadioButton radbtn_Note;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
