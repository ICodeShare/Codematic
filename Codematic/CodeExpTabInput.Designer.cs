using System;
using System.Drawing;
using System.Windows.Forms;
namespace Codematic
{
    partial class CodeExpTabInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "CodeExpTabInput";
            this.txtTableList = new TextBox();
            this.btnOk = new Button();
            this.label1 = new Label();
            base.SuspendLayout();
            this.txtTableList.Location = new Point(12, 29);
            this.txtTableList.Multiline = true;
            this.txtTableList.Name = "txtTableList";
            this.txtTableList.Size = new Size(260, 246);
            this.txtTableList.TabIndex = 0;
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Location = new Point(197, 288);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(209, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "快速输入表名列表，请每行一个表名。";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(284, 323);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.txtTableList);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "CodeExpTabInput";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "快速输入表名";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        public TextBox txtTableList;
        private Button btnOk;
        private Label label1;
        #endregion
    }
}