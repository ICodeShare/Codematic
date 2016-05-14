using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic.UserControls
{
    partial class UcAddInManage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlAddIn = new System.Windows.Forms.TabControl();
            this.tabPage_Add = new System.Windows.Forms.TabPage();
            this.btnBrow = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtClassname = new System.Windows.Forms.TextBox();
            this.txtAssembly = new System.Windows.Forms.TextBox();
            this.txtDecr = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage_List = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menu_ShowMain = new System.Windows.Forms.MenuItem();
            this.tabPage_Code = new System.Windows.Forms.TabPage();
            this.tabControlAddIn.SuspendLayout();
            this.tabPage_Add.SuspendLayout();
            this.tabPage_List.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAddIn
            // 
            this.tabControlAddIn.Controls.Add(this.tabPage_Add);
            this.tabControlAddIn.Controls.Add(this.tabPage_List);
            this.tabControlAddIn.Controls.Add(this.tabPage_Code);
            this.tabControlAddIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAddIn.Location = new System.Drawing.Point(0, 0);
            this.tabControlAddIn.Name = "tabControlAddIn";
            this.tabControlAddIn.SelectedIndex = 0;
            this.tabControlAddIn.Size = new System.Drawing.Size(368, 267);
            this.tabControlAddIn.TabIndex = 0;
            // 
            // tabPage_Add
            // 
            this.tabPage_Add.Controls.Add(this.btnBrow);
            this.tabPage_Add.Controls.Add(this.btn_Add);
            this.tabPage_Add.Controls.Add(this.txtFile);
            this.tabPage_Add.Controls.Add(this.txtVersion);
            this.tabPage_Add.Controls.Add(this.txtClassname);
            this.tabPage_Add.Controls.Add(this.txtAssembly);
            this.tabPage_Add.Controls.Add(this.txtDecr);
            this.tabPage_Add.Controls.Add(this.txtName);
            this.tabPage_Add.Controls.Add(this.label6);
            this.tabPage_Add.Controls.Add(this.label5);
            this.tabPage_Add.Controls.Add(this.label4);
            this.tabPage_Add.Controls.Add(this.label3);
            this.tabPage_Add.Controls.Add(this.label2);
            this.tabPage_Add.Controls.Add(this.label1);
            this.tabPage_Add.Location = new System.Drawing.Point(4, 21);
            this.tabPage_Add.Name = "tabPage_Add";
            this.tabPage_Add.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Add.Size = new System.Drawing.Size(360, 242);
            this.tabPage_Add.TabIndex = 1;
            this.tabPage_Add.Text = "增加组件";
            this.tabPage_Add.UseVisualStyleBackColor = true;
            // 
            // btnBrow
            // 
            this.btnBrow.Location = new System.Drawing.Point(268, 16);
            this.btnBrow.Name = "btnBrow";
            this.btnBrow.Size = new System.Drawing.Size(33, 23);
            this.btnBrow.TabIndex = 3;
            this.btnBrow.Text = "...";
            this.btnBrow.UseVisualStyleBackColor = true;
            this.btnBrow.Click += new System.EventHandler(this.btnBrow_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(83, 175);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "添加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(83, 18);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(182, 21);
            this.txtFile.TabIndex = 1;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(83, 143);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(182, 21);
            this.txtVersion.TabIndex = 1;
            // 
            // txtClassname
            // 
            this.txtClassname.Location = new System.Drawing.Point(83, 118);
            this.txtClassname.Name = "txtClassname";
            this.txtClassname.Size = new System.Drawing.Size(182, 21);
            this.txtClassname.TabIndex = 1;
            // 
            // txtAssembly
            // 
            this.txtAssembly.Location = new System.Drawing.Point(83, 93);
            this.txtAssembly.Name = "txtAssembly";
            this.txtAssembly.Size = new System.Drawing.Size(182, 21);
            this.txtAssembly.TabIndex = 1;
            // 
            // txtDecr
            // 
            this.txtDecr.Location = new System.Drawing.Point(83, 68);
            this.txtDecr.Name = "txtDecr";
            this.txtDecr.Size = new System.Drawing.Size(182, 21);
            this.txtDecr.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(182, 21);
            this.txtName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "选择文件：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "版本：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "类名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "程序集：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "说明：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // tabPage_List
            // 
            this.tabPage_List.Controls.Add(this.listView1);
            this.tabPage_List.Location = new System.Drawing.Point(4, 21);
            this.tabPage_List.Name = "tabPage_List";
            this.tabPage_List.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_List.Size = new System.Drawing.Size(360, 242);
            this.tabPage_List.TabIndex = 0;
            this.tabPage_List.Text = "组件列表";
            this.tabPage_List.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.ContextMenu = this.contextMenu1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(354, 236);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menu_ShowMain});
            // 
            // menu_ShowMain
            // 
            this.menu_ShowMain.Index = 0;
            this.menu_ShowMain.Text = "删除";
            this.menu_ShowMain.Click += new System.EventHandler(this.menu_ShowMain_Click);
            // 
            // tabPage_Code
            // 
            this.tabPage_Code.Location = new System.Drawing.Point(4, 21);
            this.tabPage_Code.Name = "tabPage_Code";
            this.tabPage_Code.Size = new System.Drawing.Size(360, 242);
            this.tabPage_Code.TabIndex = 2;
            this.tabPage_Code.Text = "编辑文件";
            this.tabPage_Code.UseVisualStyleBackColor = true;
            // 
            // UcAddInManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlAddIn);
            this.Name = "UcAddInManage";
            this.Size = new System.Drawing.Size(368, 267);
            this.tabControlAddIn.ResumeLayout(false);
            this.tabPage_Add.ResumeLayout(false);
            this.tabPage_Add.PerformLayout();
            this.tabPage_List.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlAddIn;
        private System.Windows.Forms.TabPage tabPage_List;
        private System.Windows.Forms.TabPage tabPage_Add;
        private System.Windows.Forms.TabPage tabPage_Code;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtClassname;
        private System.Windows.Forms.TextBox txtAssembly;
        private System.Windows.Forms.TextBox txtDecr;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btnBrow;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menu_ShowMain;
        //private LTP.TextEditor.TextEditorControl txtXml;
    }
}
