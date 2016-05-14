namespace Codematic
{
    partial class LoginMySQL
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.cmbDBlist = new System.Windows.Forms.ComboBox();
            this.btn_Cancel = new WiB.Pinkie.Controls.ButtonXP();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Ok = new WiB.Pinkie.Controls.ButtonXP();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btn_ConTest = new WiB.Pinkie.Controls.ButtonXP();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chk_Simple = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Codematic.Properties.Resources.loginmysql;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(417, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(121, 96);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(163, 20);
            this.comboBoxServer.TabIndex = 33;
            this.comboBoxServer.Text = "127.0.0.1";
            // 
            // cmbDBlist
            // 
            this.cmbDBlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDBlist.Enabled = false;
            this.cmbDBlist.Location = new System.Drawing.Point(121, 171);
            this.cmbDBlist.Name = "cmbDBlist";
            this.cmbDBlist.Size = new System.Drawing.Size(246, 20);
            this.cmbDBlist.TabIndex = 29;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel._Image = null;
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btn_Cancel.DefaultScheme = false;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Image = null;
            this.btn_Cancel.Location = new System.Drawing.Point(256, 240);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Cancel.Size = new System.Drawing.Size(80, 28);
            this.btn_Cancel.TabIndex = 32;
            this.btn_Cancel.Text = "取消(&C):";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "数据库(&D)：";
            // 
            // btn_Ok
            // 
            this.btn_Ok._Image = null;
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btn_Ok.DefaultScheme = false;
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Ok.Image = null;
            this.btn_Ok.Location = new System.Drawing.Point(149, 240);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Ok.Size = new System.Drawing.Size(80, 28);
            this.btn_Ok.TabIndex = 30;
            this.btn_Ok.Text = "确定(&O):";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(121, 121);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(246, 21);
            this.txtUser.TabIndex = 27;
            this.txtUser.Text = "root";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Location = new System.Drawing.Point(18, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 5);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "登录名(&L)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "服务器名称(&S)：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(121, 146);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(246, 21);
            this.txtPass.TabIndex = 26;
            // 
            // btn_ConTest
            // 
            this.btn_ConTest._Image = null;
            this.btn_ConTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ConTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btn_ConTest.DefaultScheme = false;
            this.btn_ConTest.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_ConTest.Image = null;
            this.btn_ConTest.Location = new System.Drawing.Point(42, 240);
            this.btn_ConTest.Name = "btn_ConTest";
            this.btn_ConTest.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_ConTest.Size = new System.Drawing.Size(80, 28);
            this.btn_ConTest.TabIndex = 31;
            this.btn_ConTest.Text = "连接/测试";
            this.btn_ConTest.Click += new System.EventHandler(this.btn_ConTest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "密码(&P)：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "端口：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(327, 96);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 21);
            this.textBox1.TabIndex = 35;
            this.textBox1.Text = "3306";
            // 
            // chk_Simple
            // 
            this.chk_Simple.AutoSize = true;
            this.chk_Simple.Checked = true;
            this.chk_Simple.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Simple.Location = new System.Drawing.Point(121, 198);
            this.chk_Simple.Name = "chk_Simple";
            this.chk_Simple.Size = new System.Drawing.Size(96, 16);
            this.chk_Simple.TabIndex = 36;
            this.chk_Simple.Text = "高效连接模式";
            this.chk_Simple.UseVisualStyleBackColor = true;
            // 
            // LoginMySQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 286);
            this.Controls.Add(this.chk_Simple);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBoxServer);
            this.Controls.Add(this.cmbDBlist);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btn_ConTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginMySQL";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接到MySQL服务器";
            this.Load += new System.EventHandler(this.LoginMySQL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ComboBox comboBoxServer;
        public System.Windows.Forms.ComboBox cmbDBlist;
        private WiB.Pinkie.Controls.ButtonXP btn_Cancel;
        private System.Windows.Forms.Label label4;
        private WiB.Pinkie.Controls.ButtonXP btn_Ok;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPass;
        private WiB.Pinkie.Controls.ButtonXP btn_ConTest;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.CheckBox chk_Simple;
    }
}