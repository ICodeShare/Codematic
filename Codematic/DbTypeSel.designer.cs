namespace Codematic
{
    partial class DbTypeSel
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radbtn_SQLite = new System.Windows.Forms.RadioButton();
            this.radbtn_dbtype_MySQL = new System.Windows.Forms.RadioButton();
            this.radbtn_dbtype_Oracle = new System.Windows.Forms.RadioButton();
            this.radbtn_dbtype_SQL2000 = new System.Windows.Forms.RadioButton();
            this.radbtn_dbtype_Access = new System.Windows.Forms.RadioButton();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radbtn_SQLite);
            this.groupBox1.Controls.Add(this.radbtn_dbtype_MySQL);
            this.groupBox1.Controls.Add(this.radbtn_dbtype_Oracle);
            this.groupBox1.Controls.Add(this.radbtn_dbtype_SQL2000);
            this.groupBox1.Controls.Add(this.radbtn_dbtype_Access);
            this.groupBox1.Location = new System.Drawing.Point(10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据源类型";
            // 
            // radbtn_SQLite
            // 
            this.radbtn_SQLite.AutoSize = true;
            this.radbtn_SQLite.Enabled = false;
            this.radbtn_SQLite.Location = new System.Drawing.Point(167, 24);
            this.radbtn_SQLite.Name = "radbtn_SQLite";
            this.radbtn_SQLite.Size = new System.Drawing.Size(59, 16);
            this.radbtn_SQLite.TabIndex = 6;
            this.radbtn_SQLite.TabStop = true;
            this.radbtn_SQLite.Text = "SQLite";
            this.radbtn_SQLite.UseVisualStyleBackColor = true;
            // 
            // radbtn_dbtype_MySQL
            // 
            this.radbtn_dbtype_MySQL.AutoSize = true;
            this.radbtn_dbtype_MySQL.Location = new System.Drawing.Point(24, 84);
            this.radbtn_dbtype_MySQL.Name = "radbtn_dbtype_MySQL";
            this.radbtn_dbtype_MySQL.Size = new System.Drawing.Size(59, 16);
            this.radbtn_dbtype_MySQL.TabIndex = 5;
            this.radbtn_dbtype_MySQL.TabStop = true;
            this.radbtn_dbtype_MySQL.Text = " MySQL";
            this.radbtn_dbtype_MySQL.UseVisualStyleBackColor = true;
            // 
            // radbtn_dbtype_Oracle
            // 
            this.radbtn_dbtype_Oracle.Location = new System.Drawing.Point(24, 52);
            this.radbtn_dbtype_Oracle.Name = "radbtn_dbtype_Oracle";
            this.radbtn_dbtype_Oracle.Size = new System.Drawing.Size(104, 24);
            this.radbtn_dbtype_Oracle.TabIndex = 4;
            this.radbtn_dbtype_Oracle.Text = " Oracle";
            // 
            // radbtn_dbtype_SQL2000
            // 
            this.radbtn_dbtype_SQL2000.Checked = true;
            this.radbtn_dbtype_SQL2000.Location = new System.Drawing.Point(24, 20);
            this.radbtn_dbtype_SQL2000.Name = "radbtn_dbtype_SQL2000";
            this.radbtn_dbtype_SQL2000.Size = new System.Drawing.Size(160, 24);
            this.radbtn_dbtype_SQL2000.TabIndex = 2;
            this.radbtn_dbtype_SQL2000.TabStop = true;
            this.radbtn_dbtype_SQL2000.Text = " SQL Server";
            // 
            // radbtn_dbtype_Access
            // 
            this.radbtn_dbtype_Access.Location = new System.Drawing.Point(24, 108);
            this.radbtn_dbtype_Access.Name = "radbtn_dbtype_Access";
            this.radbtn_dbtype_Access.Size = new System.Drawing.Size(104, 24);
            this.radbtn_dbtype_Access.TabIndex = 3;
            this.radbtn_dbtype_Access.Text = " OleDb";
            // 
            // btn_Next
            // 
            this.btn_Next.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Next.Location = new System.Drawing.Point(96, 176);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(75, 25);
            this.btn_Next.TabIndex = 1;
            this.btn_Next.Text = "下一步";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(188, 176);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 25);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取  消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // DbTypeSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 213);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbTypeSel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据库类型";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radbtn_dbtype_Oracle;
        private System.Windows.Forms.RadioButton radbtn_dbtype_SQL2000;
        private System.Windows.Forms.RadioButton radbtn_dbtype_Access;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.RadioButton radbtn_dbtype_MySQL;
        private System.Windows.Forms.RadioButton radbtn_SQLite;
    }
}