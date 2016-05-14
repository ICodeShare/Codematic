using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Xml;
using Maticsoft.CmConfig;

namespace Codematic
{
    /// <summary>
    /// FormLogin 的摘要说明。
    /// </summary>
    public class LoginOra : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private WiB.Pinkie.Controls.ButtonXP BtnOk;
        private WiB.Pinkie.Controls.ButtonXP BtnCancle;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.TextBox txtPass;
        public System.Windows.Forms.TextBox txtServer;

        Maticsoft.CmConfig.DbSettings dbobj = new Maticsoft.CmConfig.DbSettings();
        public string constr = "";
        public string dbname = "";
        public CheckBox chk_Simple;
        public CheckBox chkboxConnectStr;
        public TextBox txtConnectStr;

        #region system
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public LoginOra()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginOra));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.BtnOk = new WiB.Pinkie.Controls.ButtonXP();
            this.BtnCancle = new WiB.Pinkie.Controls.ButtonXP();
            this.chk_Simple = new System.Windows.Forms.CheckBox();
            this.chkboxConnectStr = new System.Windows.Forms.CheckBox();
            this.txtConnectStr = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 276);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(173, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 1);
            this.label1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(168, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = " 登录到数据库";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(176, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "用户名：";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(176, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "口令(P&)：";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(176, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "服务：";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(184, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "版权所有(C) 2004 Maticsoft 。保留所有权利。";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Location = new System.Drawing.Point(288, 80);
            this.txtUser.MaxLength = 30;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(168, 21);
            this.txtUser.TabIndex = 7;
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Location = new System.Drawing.Point(288, 112);
            this.txtPass.MaxLength = 25;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(168, 21);
            this.txtPass.TabIndex = 7;
            // 
            // txtServer
            // 
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Location = new System.Drawing.Point(288, 144);
            this.txtServer.MaxLength = 30;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(168, 21);
            this.txtServer.TabIndex = 7;
            // 
            // BtnOk
            // 
            this.BtnOk._Image = null;
            this.BtnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnOk.DefaultScheme = true;
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Image = null;
            this.BtnOk.Location = new System.Drawing.Point(263, 221);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.BtnOk.Size = new System.Drawing.Size(70, 24);
            this.BtnOk.TabIndex = 8;
            this.BtnOk.Text = "确 定";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancle
            // 
            this.BtnCancle._Image = null;
            this.BtnCancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnCancle.DefaultScheme = true;
            this.BtnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancle.Image = null;
            this.BtnCancle.Location = new System.Drawing.Point(414, 221);
            this.BtnCancle.Name = "BtnCancle";
            this.BtnCancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.BtnCancle.Size = new System.Drawing.Size(70, 24);
            this.BtnCancle.TabIndex = 9;
            this.BtnCancle.Text = "取 消";
            this.BtnCancle.Click += new System.EventHandler(this.BtnCancle_Click);
            // 
            // chk_Simple
            // 
            this.chk_Simple.AutoSize = true;
            this.chk_Simple.Checked = true;
            this.chk_Simple.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Simple.Location = new System.Drawing.Point(288, 173);
            this.chk_Simple.Name = "chk_Simple";
            this.chk_Simple.Size = new System.Drawing.Size(96, 16);
            this.chk_Simple.TabIndex = 37;
            this.chk_Simple.Text = "高效连接模式";
            this.chk_Simple.UseVisualStyleBackColor = true;
            // 
            // chkboxConnectStr
            // 
            this.chkboxConnectStr.AutoSize = true;
            this.chkboxConnectStr.Location = new System.Drawing.Point(414, 173);
            this.chkboxConnectStr.Name = "chkboxConnectStr";
            this.chkboxConnectStr.Size = new System.Drawing.Size(120, 16);
            this.chkboxConnectStr.TabIndex = 38;
            this.chkboxConnectStr.Text = "自定义连接字符串";
            this.chkboxConnectStr.UseVisualStyleBackColor = true;
            this.chkboxConnectStr.CheckedChanged += new System.EventHandler(this.chkboxConnectStr_CheckedChanged);
            // 
            // txtConnectStr
            // 
            this.txtConnectStr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConnectStr.Location = new System.Drawing.Point(288, 195);
            this.txtConnectStr.MaxLength = 30;
            this.txtConnectStr.Name = "txtConnectStr";
            this.txtConnectStr.Size = new System.Drawing.Size(250, 21);
            this.txtConnectStr.TabIndex = 39;
            this.txtConnectStr.Visible = false;
            // 
            // LoginOra
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(550, 273);
            this.Controls.Add(this.txtConnectStr);
            this.Controls.Add(this.chkboxConnectStr);
            this.Controls.Add(this.chk_Simple);
            this.Controls.Add(this.BtnCancle);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginOra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion




        #endregion


        private void FormLogin_Load(object sender, System.EventArgs e)
        {
            //try
            //{
            //    dbobj=Maticsoft.CmConfig.DbConfig.GetSetting("Oracle");
            //    if(dbobj!=null)
            //    {
            //        txtServer.Text=dbobj.Server;
            //        txtUser.Text=dbobj.Uid;
            //        txtPass.Text=dbobj.Password;					
            //    }

            //}
            //catch
            //{
            //    MessageBox.Show("读取配置文件失败!");
            //}

        }

        private void BtnCancle_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            //try
            //{
            //    string user = this.txtUser.Text.Trim();
            //    string pass = this.txtPass.Text.Trim();
            //    string server = this.txtServer.Text.Trim();

            //    if ((user.Trim() == "") || (server.Trim() == ""))
            //    {
            //        MessageBox.Show(this, "用户名或密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    constr = "Data Source=" + server + "; user id=" + user + ";password=" + pass;

            //    //测试连接
            //    OracleConnection myCn = new OracleConnection(constr);
            //    try
            //    {
            //        this.Text = "正在连接服务器，请稍候...";
            //        myCn.Open();
            //    }
            //    catch
            //    {
            //        this.Text = "连接服务器失败！";
            //        myCn.Close();
            //        MessageBox.Show(this, "连接服务器失败！请检查服务器地址或用户名密码是否正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    myCn.Close();
            //    this.Text = "连接服务器成功！";


            //    if (dbobj == null)
            //        dbobj = new Maticsoft.CmConfig.DbSettings();

            //    //将当前配置写入配置文件
            //    dbobj.DbType = "Oracle";
            //    dbobj.Server = server;
            //    dbobj.ConnectStr = constr;
            //    dbobj.DbName = "";
            //    dbobj.ConnectSimple = chk_Simple.Checked;
            //    bool succ = Maticsoft.CmConfig.DbConfig.AddSettings(dbobj);
            //    if (!succ)
            //    {
            //        MessageBox.Show(this, "该服务器已经存在！请更换服务器地址或检查输入是否正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
                
            //    //将当前数据库系统类型写入配置
            //    //MainForm.setting.DbType="Oracle";
            //    //Maticsoft.CmConfig.ModuleConfig.SaveSettings(MainForm.setting);
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();

            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    LogInfo.WriteLog(ex);
            //}
            try
            {
                if (this.chkboxConnectStr.Checked)
                {
                    if (this.txtConnectStr.Text.Length == 0)
                    {
                        MessageBox.Show(this, "连接字符串不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    this.constr = this.txtConnectStr.Text;
                }
                else
                {
                    string text = this.txtUser.Text.Trim();
                    string text2 = this.txtPass.Text.Trim();
                    string text3 = this.txtServer.Text.Trim();
                    if (text.Trim() == "" || text3.Trim() == "")
                    {
                        MessageBox.Show(this, "用户名或密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    this.constr = string.Concat(new string[]
					{
						"Data Source=",
						text3,
						"; user id=",
						text,
						";password=",
						text2
					});
                }
                if (this.constr.Length >= 5)
                {
                    OracleConnection oracleConnection = new OracleConnection(this.constr);
                    try
                    {
                        this.Text = "正在连接服务器，请稍候...";
                        oracleConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        this.Text = "连接服务器失败！";
                        oracleConnection.Close();
                        LogInfo.WriteLog(ex);
                        MessageBox.Show(this, "连接服务器失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    oracleConnection.Close();
                    this.Text = "连接服务器成功！";
                    if (this.dbobj == null)
                    {
                        this.dbobj = new DbSettings();
                    }
                    this.dbobj.DbType="Oracle";
                    this.dbobj.Server=this.txtServer.Text.Trim();
                    this.dbobj.ConnectStr=this.constr;
                    this.dbobj.DbName="";
                    this.dbobj.DbHelperName="DbHelperOra";
                    this.dbobj.ConnectSimple=this.chk_Simple.Checked;
                    switch (DbConfig.AddSettings(this.dbobj))
                    {
                        case 0:
                            MessageBox.Show(this, "添加服务器配置失败，请检查是否有写入权限或文件是否存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        case 2:
                            {
                                DialogResult dialogResult = MessageBox.Show(this, "该服务器信息已经存在！你确认是否覆盖当前数据库配置？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                                if (dialogResult != DialogResult.Yes)
                                {
                                    return;
                                }
                                DbConfig.DelSetting(this.dbobj.DbType, this.dbobj.Server, this.dbobj.DbName);
                                int num = DbConfig.AddSettings(this.dbobj);
                                if (num != 1)
                                {
                                    MessageBox.Show(this, "建议卸载当前版本，并删除安装目录后重新安装最新版本！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    return;
                                }
                                break;
                            }
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(this, ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LogInfo.WriteLog(ex2);
            }
        }

        private void chkboxConnectStr_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkboxConnectStr.Checked)
            {
                this.txtConnectStr.Visible = true;
                this.txtPass.Enabled = false;
                this.txtServer.Enabled = false;
                this.txtUser.Enabled = false;
                return;
            }
            this.txtConnectStr.Visible = false;
            this.txtPass.Enabled = true;
            this.txtServer.Enabled = true;
            this.txtUser.Enabled = true;
        }
    }
}
