using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Maticsoft.CmConfig;
namespace Codematic
{
    public partial class LoginMySQL : Form
    {
        Maticsoft.CmConfig.DbSettings dbobj = new Maticsoft.CmConfig.DbSettings();
        public string constr;
        public string dbname = "mysql";

        public LoginMySQL()
        {
            InitializeComponent();
        }

        private void btn_ConTest_Click(object sender, EventArgs e)
        {
            try
            {
                string server = this.comboBoxServer.Text.Trim();
                string user = this.txtUser.Text.Trim();
                string pass = this.txtPass.Text.Trim();
                string port = this.textBox1.Text.Trim();
                if ((user == "") || (server == ""))
                {
                    MessageBox.Show(this, "服务器或用户名不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    constr = String.Format("server={0};uid={1}; Port={2};pwd={3}; pooling=false", server, user, port, pass);
                    try
                    {
                        this.Text = "正在连接服务器，请稍候...";
                        Maticsoft.IDBO.IDbObject dbobj = Maticsoft.DBFactory.DBOMaker.CreateDbObj("MySQL");
                        dbobj.DbConnectStr = constr;
                        List<string> dblist = dbobj.GetDBList();
                        this.cmbDBlist.Enabled = true;
                        this.cmbDBlist.Items.Clear();
                        this.cmbDBlist.Items.Add("全部库");//5_1_a_s_p_x
                        if (dblist != null && dblist.Count > 0)
                        {
                            foreach (string dbname in dblist)
                            {
                                this.cmbDBlist.Items.Add(dbname);
                            }
                        }
                        this.cmbDBlist.SelectedIndex = 0;
                        this.Text = "连接服务器成功！";

                    }
                    catch (System.Exception ex)
                    {
                        LogInfo.WriteLog(ex);
                        this.Text = "连接服务器或获取数据信息失败！";
                        DialogResult drs = MessageBox.Show(this, "连接服务器或获取数据信息失败！\r\n请检查服务器地址或用户名密码是否正确！查看帮助文件以帮助您解决问题？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                        //if (drs == DialogResult.OK)
                        //{
                        //    try
                        //    {
                        //        //Process proc = new Process();
                        //        //Process.Start("IExplore.exe", "http://help.maticsoft.com");
                        //    }
                        //    catch
                        //    {
                        //        MessageBox.Show("请访问：http://www.maticsoft.com", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //    }
                        //}
                        //return;
                    }

                }
            }
            catch (Exception ex2)
            {
                LogInfo.WriteLog(ex2);
                MessageBox.Show(this, ex2.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                string server = this.comboBoxServer.Text.Trim();
                string user = this.txtUser.Text.Trim();
                string pass = this.txtPass.Text.Trim();
                string port = this.textBox1.Text.Trim();
                if (user == "" || server == "")
                {
                    MessageBox.Show(this, "服务器或用户名不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (this.cmbDBlist.SelectedIndex > 0)
                    {
                        dbname = cmbDBlist.Text;
                    }
                    else
                    {
                        dbname = "mysql";
                    }
                    constr = String.Format("server={0};user id={1}; Port={2};password={3}; database={4}; pooling=false", server, user, port, pass, dbname);
                    //测试连接
                    MySqlConnection myCn = new MySqlConnection(constr);
                    try
                    {
                        this.Text = "正在连接服务器，请稍候...";
                        myCn.Open();
                    }
                    catch (System.Exception ex)
                    {
                        LogInfo.WriteLog(ex);
                        this.Text = "连接服务器失败！";
                        MessageBox.Show(this, "连接服务器失败！请检查服务器地址或用户名密码是否正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    finally
                    {
                        myCn.Close();
                    }
                    this.Text = "连接服务器成功！";
                    if (dbobj == null)
                    {
                        dbobj = new Maticsoft.CmConfig.DbSettings();
                    }
                    string strtype = "MySQL";
                    //将当前配置写入配置文件
                    dbobj.DbType = strtype;
                    dbobj.Server = server;
                    dbobj.ConnectStr = constr;
                    dbobj.DbName = dbname;
                    this.dbobj.DbHelperName = "DbHelperMySQL";
                    dbobj.ConnectSimple = chk_Simple.Checked;
                    switch (DbConfig.AddSettings(this.dbobj))
                    {
                        case 0:
                            MessageBox.Show(this, "添加服务器配置失败，请检查安装目录是否有写入权限或文件是否存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LogInfo.WriteLog(ex);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginMySQL_Load(object sender, EventArgs e)
        {
            //comboBoxServerVer.SelectedIndex = 0;
            //comboBox_Verified.SelectedIndex = 0;
        }
    }
}