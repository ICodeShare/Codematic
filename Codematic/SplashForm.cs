using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	public class SplashForm : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.PictureBox pbLogo;

		private System.ComponentModel.Container components = null;

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SplashForm));
			this.Label1 = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// Label1
			// 
			this.Label1.AccessibleDescription = resources.GetString("Label1.AccessibleDescription");
			this.Label1.AccessibleName = resources.GetString("Label1.AccessibleName");
			this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("Label1.Anchor")));
			this.Label1.AutoSize = ((bool)(resources.GetObject("Label1.AutoSize")));
			this.Label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("Label1.Dock")));
			this.Label1.Enabled = ((bool)(resources.GetObject("Label1.Enabled")));
			this.Label1.Font = ((System.Drawing.Font)(resources.GetObject("Label1.Font")));
			this.Label1.Image = ((System.Drawing.Image)(resources.GetObject("Label1.Image")));
			this.Label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Label1.ImageAlign")));
			this.Label1.ImageIndex = ((int)(resources.GetObject("Label1.ImageIndex")));
			this.Label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("Label1.ImeMode")));
			this.Label1.Location = ((System.Drawing.Point)(resources.GetObject("Label1.Location")));
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("Label1.RightToLeft")));
			this.Label1.Size = ((System.Drawing.Size)(resources.GetObject("Label1.Size")));
			this.Label1.TabIndex = ((int)(resources.GetObject("Label1.TabIndex")));
			this.Label1.Text = resources.GetString("Label1.Text");
			this.Label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("Label1.TextAlign")));
			this.Label1.Visible = ((bool)(resources.GetObject("Label1.Visible")));
			// 
			// pbLogo
			// 
			this.pbLogo.AccessibleDescription = resources.GetString("pbLogo.AccessibleDescription");
			this.pbLogo.AccessibleName = resources.GetString("pbLogo.AccessibleName");
			this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pbLogo.Anchor")));
			this.pbLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbLogo.BackgroundImage")));
			this.pbLogo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pbLogo.Dock")));
			this.pbLogo.Enabled = ((bool)(resources.GetObject("pbLogo.Enabled")));
			this.pbLogo.Font = ((System.Drawing.Font)(resources.GetObject("pbLogo.Font")));
			this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
			this.pbLogo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pbLogo.ImeMode")));
			this.pbLogo.Location = ((System.Drawing.Point)(resources.GetObject("pbLogo.Location")));
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pbLogo.RightToLeft")));
			this.pbLogo.Size = ((System.Drawing.Size)(resources.GetObject("pbLogo.Size")));
			this.pbLogo.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("pbLogo.SizeMode")));
			this.pbLogo.TabIndex = ((int)(resources.GetObject("pbLogo.TabIndex")));
			this.pbLogo.TabStop = false;
			this.pbLogo.Text = resources.GetString("pbLogo.Text");
			this.pbLogo.Visible = ((bool)(resources.GetObject("pbLogo.Visible")));
			// 
			// SplashForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.ControlBox = false;
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.pbLogo);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "SplashForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		public SplashForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		
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
	}
}
