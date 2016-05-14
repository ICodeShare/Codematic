using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// DbOption 的摘要说明。
	/// </summary>
	public class DbOption : System.Windows.Forms.Form
	{
		private WiB.Pinkie.Controls.ButtonXP btnOk;
		private WiB.Pinkie.Controls.ButtonXP btn_Exit;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton radbtn_dbtype_Oracle;
		private System.Windows.Forms.RadioButton radbtn_dbtype_SQL;
		private System.Windows.Forms.RadioButton radbtn_dbtype_Access;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        Maticsoft.CmConfig.ModuleSettings setting = new Maticsoft.CmConfig.ModuleSettings();

		public DbOption()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOk = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_Exit = new WiB.Pinkie.Controls.ButtonXP();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.radbtn_dbtype_Oracle = new System.Windows.Forms.RadioButton();
			this.radbtn_dbtype_SQL = new System.Windows.Forms.RadioButton();
			this.radbtn_dbtype_Access = new System.Windows.Forms.RadioButton();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk._Image = null;
			this.btnOk.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btnOk.DefaultScheme = false;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnOk.Image = null;
			this.btnOk.Location = new System.Drawing.Point(112, 176);
			this.btnOk.Name = "btnOk";
			this.btnOk.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btnOk.Size = new System.Drawing.Size(75, 26);
			this.btnOk.TabIndex = 44;
			this.btnOk.Text = "确  定";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btn_Exit
			// 
			this.btn_Exit._Image = null;
			this.btn_Exit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btn_Exit.DefaultScheme = false;
			this.btn_Exit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Exit.Image = null;
			this.btn_Exit.Location = new System.Drawing.Point(200, 176);
			this.btn_Exit.Name = "btn_Exit";
			this.btn_Exit.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Exit.Size = new System.Drawing.Size(75, 26);
			this.btn_Exit.TabIndex = 43;
			this.btn_Exit.Text = "取  消";
			this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.radbtn_dbtype_Oracle);
			this.groupBox4.Controls.Add(this.radbtn_dbtype_SQL);
			this.groupBox4.Controls.Add(this.radbtn_dbtype_Access);
			this.groupBox4.Location = new System.Drawing.Point(8, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(280, 144);
			this.groupBox4.TabIndex = 47;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "选择数据源类型";
			// 
			// radbtn_dbtype_Oracle
			// 
			this.radbtn_dbtype_Oracle.Location = new System.Drawing.Point(24, 64);
			this.radbtn_dbtype_Oracle.Name = "radbtn_dbtype_Oracle";
			this.radbtn_dbtype_Oracle.TabIndex = 1;
			this.radbtn_dbtype_Oracle.Text = " Oracle";
			// 
			// radbtn_dbtype_SQL
			// 
			this.radbtn_dbtype_SQL.Checked = true;
			this.radbtn_dbtype_SQL.Location = new System.Drawing.Point(24, 32);
			this.radbtn_dbtype_SQL.Name = "radbtn_dbtype_SQL";
			this.radbtn_dbtype_SQL.Size = new System.Drawing.Size(160, 24);
			this.radbtn_dbtype_SQL.TabIndex = 0;
			this.radbtn_dbtype_SQL.TabStop = true;
			this.radbtn_dbtype_SQL.Text = " SQL Server ";
			// 
			// radbtn_dbtype_Access
			// 
			this.radbtn_dbtype_Access.Location = new System.Drawing.Point(24, 96);
			this.radbtn_dbtype_Access.Name = "radbtn_dbtype_Access";
			this.radbtn_dbtype_Access.TabIndex = 1;
			this.radbtn_dbtype_Access.Text = " OleDb";
			// 
			// DbOption
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 215);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btn_Exit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DbOption";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "数据源设置";
			this.Load += new System.EventHandler(this.DbOption_Load);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btn_Exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}		
		private void DbOption_Load(object sender, System.EventArgs e)
		{
            //setting=Maticsoft.CmConfig.ModuleConfig.GetSettings();
            //switch(setting.DbType)
            //{
            //    case "SQL2000":
            //    case "SQL2005":
            //        this.radbtn_dbtype_SQL.Checked=true;
            //        break;
            //    case "Oracle":
            //        this.radbtn_dbtype_Oracle.Checked=true;
            //        break;
            //    case  "OleDb":
            //        this.radbtn_dbtype_Access.Checked=true;
            //        break;
            //    default:
            //        this.radbtn_dbtype_SQL.Checked=true;
            //        break;
            //}
		
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
            //string dbtype="SQL2000";
            //if(this.radbtn_dbtype_SQL.Checked)
            //{
            //    dbtype="SQL2000";
            //}
            //if(this.radbtn_dbtype_Oracle.Checked)
            //{
            //    dbtype="Oracle";
            //}
            //if(this.radbtn_dbtype_Access.Checked)
            //{
            //    dbtype="OleDb";
            //}
            //setting.DbType=dbtype;
            //Maticsoft.CmConfig.ModuleConfig.SaveSettings(setting);
            //MessageBox.Show(this,"数据源设置成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //this.Close();

		}


	}
}
