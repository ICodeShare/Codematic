
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using QueryCommander.Config;

namespace Codematic.UserControls
{
	/// <summary>
	/// Summary description for UcOptionEditor.
	/// </summary>
	public class UcOptionEditor : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSelectFont;
		private System.Windows.Forms.GroupBox groupBox1;
		public Font font;
		public System.Windows.Forms.CheckBox chbShowLineNumbers;
		public System.Windows.Forms.CheckBox chbShowEOLMarkers;
		public System.Windows.Forms.CheckBox chbShowMatchingBrackets;
		public System.Windows.Forms.CheckBox chbShowSpaces;
		public System.Windows.Forms.CheckBox chbShowTabs;
        private System.Windows.Forms.Label lblFont;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UcOptionEditor()
		{
			
			InitializeComponent();
			
            //QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
            //if(settings.fontFamily==null)
            //    settings.fontFamily="Courier New";

            //if(settings.Exists())
            //{
            //    font = settings.GetFont();
            //    this.chbShowEOLMarkers.Checked=settings.ShowEOLMarkers;
            //    this.chbShowSpaces.Checked=settings.ShowSpaces;
            //    this.chbShowTabs.Checked=settings.ShowTabs;
            //    this.chbShowLineNumbers.Checked=settings.ShowLineNumbers;
            //    this.chbShowMatchingBrackets.Checked=settings.ShowMatchingBracket;
            //    this.txtTabIndent.Text=settings.TabIndent.ToString();
            //}
            //else
            //{
            //    font=new Font("Courier New",11);
            //    this.chbShowEOLMarkers.Checked=false;
            //    this.chbShowSpaces.Checked=false;
            //    this.chbShowTabs.Checked=false;
            //    this.chbShowLineNumbers.Checked=true;
            //    this.chbShowMatchingBrackets.Checked=true;
            //    this.txtTabIndent.Text="4";
            //}
            //lblFont.Text=font.FontFamily.Name;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.chbShowLineNumbers = new System.Windows.Forms.CheckBox();
            this.chbShowEOLMarkers = new System.Windows.Forms.CheckBox();
            this.chbShowMatchingBrackets = new System.Windows.Forms.CheckBox();
            this.chbShowSpaces = new System.Windows.Forms.CheckBox();
            this.chbShowTabs = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbShowLineNumbers
            // 
            this.chbShowLineNumbers.Checked = true;
            this.chbShowLineNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbShowLineNumbers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chbShowLineNumbers.Location = new System.Drawing.Point(8, 8);
            this.chbShowLineNumbers.Name = "chbShowLineNumbers";
            this.chbShowLineNumbers.Size = new System.Drawing.Size(248, 16);
            this.chbShowLineNumbers.TabIndex = 0;
            this.chbShowLineNumbers.Text = "Show line numbers";
            // 
            // chbShowEOLMarkers
            // 
            this.chbShowEOLMarkers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chbShowEOLMarkers.Location = new System.Drawing.Point(8, 32);
            this.chbShowEOLMarkers.Name = "chbShowEOLMarkers";
            this.chbShowEOLMarkers.Size = new System.Drawing.Size(248, 16);
            this.chbShowEOLMarkers.TabIndex = 1;
            this.chbShowEOLMarkers.Text = "Show EOL markers";
            // 
            // chbShowMatchingBrackets
            // 
            this.chbShowMatchingBrackets.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chbShowMatchingBrackets.Location = new System.Drawing.Point(8, 56);
            this.chbShowMatchingBrackets.Name = "chbShowMatchingBrackets";
            this.chbShowMatchingBrackets.Size = new System.Drawing.Size(248, 16);
            this.chbShowMatchingBrackets.TabIndex = 2;
            this.chbShowMatchingBrackets.Text = "Show matching brackets";
            // 
            // chbShowSpaces
            // 
            this.chbShowSpaces.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chbShowSpaces.Location = new System.Drawing.Point(8, 80);
            this.chbShowSpaces.Name = "chbShowSpaces";
            this.chbShowSpaces.Size = new System.Drawing.Size(248, 16);
            this.chbShowSpaces.TabIndex = 3;
            this.chbShowSpaces.Text = "Show spaces";
            // 
            // chbShowTabs
            // 
            this.chbShowTabs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chbShowTabs.Location = new System.Drawing.Point(8, 104);
            this.chbShowTabs.Name = "chbShowTabs";
            this.chbShowTabs.Size = new System.Drawing.Size(248, 16);
            this.chbShowTabs.TabIndex = 4;
            this.chbShowTabs.Text = "Show tabs";
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Font:";
            // 
            // lblFont
            // 
            this.lblFont.BackColor = System.Drawing.Color.White;
            this.lblFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFont.Location = new System.Drawing.Point(56, 24);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(168, 16);
            this.lblFont.TabIndex = 6;
            this.lblFont.Text = "Curier";
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelectFont.Location = new System.Drawing.Point(232, 24);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(32, 16);
            this.btnSelectFont.TabIndex = 7;
            this.btnSelectFont.Text = "...";
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFont);
            this.groupBox1.Controls.Add(this.btnSelectFont);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 64);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font settings";
            // 
            // UcOptionEditor
            // 
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chbShowTabs);
            this.Controls.Add(this.chbShowSpaces);
            this.Controls.Add(this.chbShowMatchingBrackets);
            this.Controls.Add(this.chbShowEOLMarkers);
            this.Controls.Add(this.chbShowLineNumbers);
            this.Name = "UcOptionEditor";
            this.Size = new System.Drawing.Size(312, 248);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnSelectFont_Click(object sender, System.EventArgs e)
		{
            //QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
            //FontDialog fontDialog = new FontDialog();
            //fontDialog.FontMustExist=false;
            //fontDialog.MaxSize = 20;
            //fontDialog.MinSize = 6;
            //fontDialog.ShowColor = false;
            //fontDialog.ShowApply=false;
            //fontDialog.Font=settings.GetFont();

            //if(fontDialog.ShowDialog() != DialogResult.Cancel )
            //{
            //    font = fontDialog.Font ;
            //}
            //lblFont.Text=font.FontFamily.Name;

		}

		private void txtTabIndent_TextChanged(object sender, System.EventArgs e)
		{
            //int cursorPos = txtTabIndent.SelectionStart;

            //string notValid = "[^0-9]";
            //Regex regex = new Regex(notValid);
			
            //string oldText = txtTabIndent.Text;
            //string newText = regex.Replace(oldText, "");

            //if (newText != oldText) 
            //{
            //    txtTabIndent.Text = newText;
            //    int i;
            //    for (i = 0; i < newText.Length; i++) if (newText[i] != oldText[i]) break;
            //    txtTabIndent.SelectionStart = i;
            //}
		}
	}
}
