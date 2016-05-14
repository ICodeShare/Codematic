
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Codematic
{
	/// <summary>
	/// Summary description for FrmSearch.
	/// </summary>
	public class FrmSearch : System.Windows.Forms.Form
	{
        private DbQuery _frmQuery;
		private int _lastPos = 0;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Button btn_FindNext;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rb_Up;
		private System.Windows.Forms.RadioButton rb_Down;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnReplaceAll;
		private System.Windows.Forms.Button btnReplace;
		private System.Windows.Forms.Panel panel1;
		MainForm mainForm;
		private System.Windows.Forms.CheckBox chbMatchCase;
		private System.Windows.Forms.CheckBox chbMatchWholeWord;
		private System.Windows.Forms.TextBox txtReplacewith;
		private System.Windows.Forms.ComboBox cboWord;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public FrmSearch(DbQuery frmQuery)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			string word = frmQuery.txtContent.GetCurrentWord();
			cboWord.Text = word;
			_frmQuery = frmQuery;
			this.Height=152;
			btnReplace.Visible=false;
			btnReplaceAll.Visible=false;
			panel1.Location=new Point(8,48);

			mainForm = (MainForm)frmQuery.MdiParentForm;
            mainForm.查找下一个ToolStripMenuItem.Visible = true;

			this.Focus();
		}

        public FrmSearch(DbQuery frmQuery, bool replace)
		{	
			InitializeComponent();
			this.Text = "Replace";
			_frmQuery = frmQuery;
			if(!replace)
			{
				this.Height=152;
			}
			this.Focus();
		}

		public Object[] SearchItems
		{
			get
			{	
				ArrayList al = new ArrayList(cboWord.Items);
				object [] obj = al.ToArray();
				return obj;
			}
			set
			{
				if (value != null)
				{
					cboWord.Items.AddRange(value); 
				}
			}
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
            this.label1 = new System.Windows.Forms.Label();
            this.chbMatchCase = new System.Windows.Forms.CheckBox();
            this.btn_FindNext = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.chbMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_Down = new System.Windows.Forms.RadioButton();
            this.rb_Up = new System.Windows.Forms.RadioButton();
            this.txtReplacewith = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboWord = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找内容：";
            // 
            // chbMatchCase
            // 
            this.chbMatchCase.AutoSize = true;
            this.chbMatchCase.Location = new System.Drawing.Point(10, 35);
            this.chbMatchCase.Name = "chbMatchCase";
            this.chbMatchCase.Size = new System.Drawing.Size(84, 16);
            this.chbMatchCase.TabIndex = 8;
            this.chbMatchCase.Text = "区分大小写";
            // 
            // btn_FindNext
            // 
            this.btn_FindNext.Enabled = false;
            this.btn_FindNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_FindNext.Location = new System.Drawing.Point(355, 17);
            this.btn_FindNext.Name = "btn_FindNext";
            this.btn_FindNext.Size = new System.Drawing.Size(96, 26);
            this.btn_FindNext.TabIndex = 2;
            this.btn_FindNext.Text = "查找下一个(&F)";
            this.btn_FindNext.Click += new System.EventHandler(this.btn_FindNext_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Cancel.Location = new System.Drawing.Point(355, 52);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(96, 26);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "取消(&C)";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // chbMatchWholeWord
            // 
            this.chbMatchWholeWord.AutoSize = true;
            this.chbMatchWholeWord.Location = new System.Drawing.Point(10, 9);
            this.chbMatchWholeWord.Name = "chbMatchWholeWord";
            this.chbMatchWholeWord.Size = new System.Drawing.Size(84, 16);
            this.chbMatchWholeWord.TabIndex = 7;
            this.chbMatchWholeWord.Text = "匹配整个词";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_Down);
            this.groupBox1.Controls.Add(this.rb_Up);
            this.groupBox1.Location = new System.Drawing.Point(177, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 60);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方向";
            this.groupBox1.Visible = false;
            // 
            // rb_Down
            // 
            this.rb_Down.Checked = true;
            this.rb_Down.Location = new System.Drawing.Point(77, 26);
            this.rb_Down.Name = "rb_Down";
            this.rb_Down.Size = new System.Drawing.Size(67, 17);
            this.rb_Down.TabIndex = 10;
            this.rb_Down.TabStop = true;
            this.rb_Down.Text = "向下";
            // 
            // rb_Up
            // 
            this.rb_Up.Location = new System.Drawing.Point(19, 26);
            this.rb_Up.Name = "rb_Up";
            this.rb_Up.Size = new System.Drawing.Size(48, 17);
            this.rb_Up.TabIndex = 9;
            this.rb_Up.Text = "向上";
            // 
            // txtReplacewith
            // 
            this.txtReplacewith.Location = new System.Drawing.Point(106, 44);
            this.txtReplacewith.Name = "txtReplacewith";
            this.txtReplacewith.Size = new System.Drawing.Size(240, 21);
            this.txtReplacewith.TabIndex = 4;
            this.txtReplacewith.TextChanged += new System.EventHandler(this.txt_replacewith_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "替换为：";
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Enabled = false;
            this.btnReplaceAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReplaceAll.Location = new System.Drawing.Point(355, 121);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(96, 25);
            this.btnReplaceAll.TabIndex = 7;
            this.btnReplaceAll.Text = "替换所有";
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Enabled = false;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReplace.Location = new System.Drawing.Point(355, 86);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(96, 26);
            this.btnReplace.TabIndex = 6;
            this.btnReplace.Text = "替换(&R)";
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.chbMatchWholeWord);
            this.panel1.Controls.Add(this.chbMatchCase);
            this.panel1.Location = new System.Drawing.Point(10, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 68);
            this.panel1.TabIndex = 11;
            // 
            // cboWord
            // 
            this.cboWord.Location = new System.Drawing.Point(106, 17);
            this.cboWord.Name = "cboWord";
            this.cboWord.Size = new System.Drawing.Size(240, 20);
            this.cboWord.TabIndex = 1;
            this.cboWord.TextChanged += new System.EventHandler(this.txt_word_TextChanged);
            // 
            // FrmSearch
            // 
            this.AcceptButton = this.btn_FindNext;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(460, 154);
            this.Controls.Add(this.cboWord);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReplacewith);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_FindNext);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btn_FindNext_Click(object sender, System.EventArgs e)
		{
			string pattern = cboWord.Text; //txtWord.Text;
			try
			{
				Regex regex;
				
				if(chbMatchWholeWord.Checked)
					pattern = @"\b"+pattern +@"\b";
				
				pattern=pattern.Replace("[",@"\[");
				pattern=pattern.Replace("(",@"\(");
		
				if(chbMatchCase.Checked)
					regex = new Regex(pattern);
				else
					regex = new Regex(pattern, RegexOptions.IgnoreCase );

				_lastPos = _frmQuery.Find(regex, _lastPos);

				if (!cboWord.Items.Contains(cboWord.Text))
					cboWord.Items.Add(cboWord.Text);
			}
			catch//(Exception ex)
			{
				MessageBox.Show("Unable to search for ["+pattern+"].)"); 	
			}
		}

		private void btn_Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void FrmSearch_Load(object sender, System.EventArgs e)
		{
			this.TopLevel=true;
		}

		private void btnReplace_Click(object sender, System.EventArgs e)
		{
			Regex regex;
			string pattern = cboWord.Text; // txtWord.Text;
			if(chbMatchWholeWord.Checked)
				pattern=@"\b"+pattern +@"\b";

			if(chbMatchCase.Checked)
				regex = new Regex(pattern);
			else
				regex = new Regex(pattern, RegexOptions.IgnoreCase );

			_lastPos = _frmQuery.Replace(regex,_lastPos,txtReplacewith.Text);

			if (!cboWord.Items.Contains(cboWord.Text))
			{
				cboWord.Items.Add(cboWord.Text);
			}
			//_frmQuery.Focus();
		}

		private void txt_word_TextChanged(object sender, System.EventArgs e)
		{
			btn_FindNext.Enabled = cboWord.Text.Length > 0; // txtWord.Text.Length > 0;
			//btnReplace.Enabled=txt_word.Text.Length>0 & txt_replacewith.Text.Length>0;
		}

		private void txt_replacewith_TextChanged(object sender, System.EventArgs e)
		{
			btnReplace.Enabled = cboWord.Text.Length > 0  & txtReplacewith.Text.Length > 0;//txtWord.Text.Length>0 & txtReplacewith.Text.Length>0;
			btnReplaceAll.Enabled = btnReplace.Enabled; //txtWord.Text.Length>0 & txtReplacewith.Text.Length>0;
		}

		private void btnReplaceAll_Click(object sender, System.EventArgs e)
		{
			try
			{
			Regex regex;
			string pattern = cboWord.Text; // txtWord.Text;
			if(chbMatchWholeWord.Checked)
				pattern = @"\b"+pattern +@"\b";

			if(chbMatchCase.Checked)
				regex = new Regex(pattern);
			else
				regex = new Regex(pattern, RegexOptions.IgnoreCase );

			_frmQuery.ReplaceAll(regex,txtReplacewith.Text);

			if (!cboWord.Items.Contains(cboWord.Text))
			{
				cboWord.Items.Add(cboWord.Text);
			}
			//_frmQuery.Focus();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
//        private string StringToHex(string str)
//        {
//            return "";
//            //string hexString = String.Empty;

////			for(int i=0;i<str.Length;i++)
////			{
////				hexString+=Asc("w");
////			}
		
//        }
//		Function stringtohex(ByVal str As String) As String 
//			ReDim data(Len(str)) 
//			For i = 1 To Len(str) 
//					  stringtohex = stringtohex & Hex(Asc(Mid(str, i, 1))) 
//			Next i 
//		End Function 

		public void FindNext()
		{
		}
	}
}
