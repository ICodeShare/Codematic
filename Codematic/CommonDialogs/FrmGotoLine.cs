
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// Summary description for FrmGotoLine.
	/// </summary>
	public class FrmGotoLine : System.Windows.Forms.Form
	{
        private DbQuery _frmQuery;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblText;
		private System.Windows.Forms.TextBox txtLine;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public FrmGotoLine(DbQuery frmQuery, int currentPosition, int lastPosition)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_frmQuery = frmQuery;
			lblText.Text="Line number(1-" + lastPosition.ToString() + ")";
			txtLine.Text=currentPosition.ToString();
			txtLine.SelectAll();
		}

		/// <summary>
		/// Clean up any resources being used.
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblText = new System.Windows.Forms.Label();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(10, 11);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(65, 12);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "转到行号：";
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(10, 30);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(182, 21);
            this.txtLine.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOk.Location = new System.Drawing.Point(35, 61);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(116, 61);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmGotoLine
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(203, 96);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.lblText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGotoLine";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "跳到指定行";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			int line = Convert.ToInt32(txtLine.Text);
			_frmQuery.GoToLine(line);
			this.Close();
		}
	}
}
