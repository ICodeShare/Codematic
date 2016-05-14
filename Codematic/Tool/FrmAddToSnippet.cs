using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
namespace Codematic
{
	/// <summary>
	/// FrmAddToSnippet 的摘要说明。
	/// </summary>
	public class FrmAddToSnippet : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Label label1;
		private WiB.Pinkie.Controls.ButtonXP btnOk;
		private WiB.Pinkie.Controls.ButtonXP btnExit;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.RichTextBox qcEditor;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmAddToSnippet(string text)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			qcEditor.Text=text;

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
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.qcEditor = new System.Windows.Forms.RichTextBox();
            this.btnOk = new WiB.Pinkie.Controls.ButtonXP();
            this.btnExit = new WiB.Pinkie.Controls.ButtonXP();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(16, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "标 题：";
            // 
            // txtCaption
            // 
            this.txtCaption.Location = new System.Drawing.Point(72, 87);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(384, 21);
            this.txtCaption.TabIndex = 2;
            // 
            // qcEditor
            // 
            this.qcEditor.Location = new System.Drawing.Point(8, 112);
            this.qcEditor.Name = "qcEditor";
            this.qcEditor.Size = new System.Drawing.Size(480, 184);
            this.qcEditor.TabIndex = 3;
            this.qcEditor.Text = "";
            // 
            // btnOk
            // 
            this.btnOk._Image = null;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnOk.DefaultScheme = false;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Image = null;
            this.btnOk.Location = new System.Drawing.Point(296, 306);
            this.btnOk.Name = "btnOk";
            this.btnOk.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btnOk.Size = new System.Drawing.Size(75, 26);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "保  存";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnExit
            // 
            this.btnExit._Image = null;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnExit.DefaultScheme = false;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExit.Image = null;
            this.btnExit.Location = new System.Drawing.Point(392, 306);
            this.btnExit.Name = "btnExit";
            this.btnExit.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btnExit.Size = new System.Drawing.Size(75, 26);
            this.btnExit.TabIndex = 42;
            this.btnExit.Text = "取  消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmAddToSnippet
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackgroundImage = global::Codematic.Properties.Resources.about3;
            this.ClientSize = new System.Drawing.Size(498, 344);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.qcEditor);
            this.Controls.Add(this.txtCaption);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddToSnippet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "脚本片段管理";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if(txtCaption.Text.Length==0)
				return;

			XmlDocument xmlSnippets = new XmlDocument();
			xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
			XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

			foreach(XmlNode node in xmlNodeList[0].ChildNodes)
			{
				if(node.Attributes["name"].Value.ToUpper()==txtCaption.Text.ToUpper())
				{
					MessageBox.Show("对不起，该名称已经存在！");
					return;
				}
			}
			
			XmlNode root = xmlSnippets.DocumentElement;

			//Create a new node.
			XmlElement elem = xmlSnippets.CreateElement("snippet");
			elem.InnerText=qcEditor.Text;
			
			XmlAttribute nameAttr = xmlSnippets.CreateAttribute("name");
			nameAttr.Value = txtCaption.Text;

			elem.Attributes.Append(nameAttr);


			//Add the node to the document.
			root.AppendChild(elem);

			xmlSnippets.Save(Application.StartupPath+@"\Snippets.xml");

			this.Close();
		
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
