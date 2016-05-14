using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// RenameFrm 的摘要说明。
	/// </summary>
	public class RenameFrm : System.Windows.Forms.Form
	{
		private WiB.Pinkie.Controls.ButtonXP btn_OK;
		private WiB.Pinkie.Controls.ButtonXP btn_Cancel;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox txtName;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RenameFrm()
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
			this.btn_OK = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_Cancel = new WiB.Pinkie.Controls.ButtonXP();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btn_OK
			// 
			this.btn_OK._Image = null;
			this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_OK.DefaultScheme = false;
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Image = null;
			this.btn_OK.Location = new System.Drawing.Point(70, 55);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_OK.Size = new System.Drawing.Size(65, 25);
			this.btn_OK.TabIndex = 50;
			this.btn_OK.Text = "确 定";
			// 
			// btn_Cancel
			// 
			this.btn_Cancel._Image = null;
			this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Cancel.DefaultScheme = false;
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Image = null;
			this.btn_Cancel.Location = new System.Drawing.Point(166, 55);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Cancel.Size = new System.Drawing.Size(65, 25);
			this.btn_Cancel.TabIndex = 49;
			this.btn_Cancel.Text = "取 消";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(110, 15);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(154, 21);
			this.txtName.TabIndex = 48;
			this.txtName.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 17);
			this.label1.TabIndex = 47;
			this.label1.Text = "输入新名称：";
			// 
			// RenameFrm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 95);
			this.Controls.Add(this.btn_OK);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RenameFrm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "新名称";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
