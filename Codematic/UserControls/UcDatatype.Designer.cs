namespace Codematic.UserControls
{
    partial class UcDatatype
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcDatatype));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIniDatatype = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmb_Section = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIniDatatype);
            this.groupBox1.Location = new System.Drawing.Point(10, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段类型映射";
            // 
            // txtIniDatatype
            // 
            this.txtIniDatatype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIniDatatype.Location = new System.Drawing.Point(3, 17);
            this.txtIniDatatype.Name = "txtIniDatatype";
            this.txtIniDatatype.Size = new System.Drawing.Size(345, 185);
            this.txtIniDatatype.TabIndex = 0;
            this.txtIniDatatype.Text = "";
            this.toolTip1.SetToolTip(this.txtIniDatatype, resources.GetString("txtIniDatatype.ToolTip"));
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "[DbToCS] 段，是数据库类型和C#类型的对应关系。";
            // 
            // cmb_Section
            // 
            this.cmb_Section.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Section.FormattingEnabled = true;
            this.cmb_Section.Location = new System.Drawing.Point(13, 8);
            this.cmb_Section.Name = "cmb_Section";
            this.cmb_Section.Size = new System.Drawing.Size(125, 20);
            this.cmb_Section.TabIndex = 1;
            this.cmb_Section.SelectedIndexChanged += new System.EventHandler(this.cmb_Section_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // UcDatatype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Section);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcDatatype";
            this.Size = new System.Drawing.Size(368, 267);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtIniDatatype;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmb_Section;
        private System.Windows.Forms.Label label1;
    }
}
