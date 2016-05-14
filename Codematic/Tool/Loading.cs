using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// Loading 的摘要说明。
	/// </summary>
	public class Loading : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Loading()
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
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// Loading
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 104);
			this.Controls.Add(this.label1);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Loading";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Loading";
			this.Load += new System.EventHandler(this.Loading_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Loading_Load(object sender, System.EventArgs e)
		{
			this.label1.Text="系统正在加载数据，请稍候....";
		}
	}
}
