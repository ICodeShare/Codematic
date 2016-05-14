using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic
{
    partial class CodeMakerM
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeMakerM));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_Next = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtClassName2 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label14 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.cmbox_PTab = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbox_PField = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbox_STab = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbox_SField = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_SelAll = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_SelAll2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_SelI = new System.Windows.Forms.Button();
            this.btn_SelI2 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.groupBox_Web = new System.Windows.Forms.GroupBox();
            this.chk_Web_Show = new System.Windows.Forms.CheckBox();
            this.chk_Web_Update = new System.Windows.Forms.CheckBox();
            this.chk_Web_HasKey = new System.Windows.Forms.CheckBox();
            this.chk_Web_Add = new System.Windows.Forms.CheckBox();
            this.radbtn_Web_AspxCS = new System.Windows.Forms.RadioButton();
            this.radbtn_Web_Aspx = new System.Windows.Forms.RadioButton();
            this.groupBox_AppType = new System.Windows.Forms.GroupBox();
            this.radbtn_AppType_Winform = new System.Windows.Forms.RadioButton();
            this.radbtn_AppType_Web = new System.Windows.Forms.RadioButton();
            this.groupBox_Method = new System.Windows.Forms.GroupBox();
            this.chk_CS_GetList = new System.Windows.Forms.CheckBox();
            this.chk_CS_GetModelByCache = new System.Windows.Forms.CheckBox();
            this.chk_CS_GetModel = new System.Windows.Forms.CheckBox();
            this.chk_CS_Delete = new System.Windows.Forms.CheckBox();
            this.chk_CS_Update = new System.Windows.Forms.CheckBox();
            this.chk_CS_Add = new System.Windows.Forms.CheckBox();
            this.chk_CS_Exists = new System.Windows.Forms.CheckBox();
            this.groupBox_DALType = new System.Windows.Forms.GroupBox();
            this.groupBox_F3 = new System.Windows.Forms.GroupBox();
            this.radbtn_F3_BLL = new System.Windows.Forms.RadioButton();
            this.radbtn_F3_DALFactory = new System.Windows.Forms.RadioButton();
            this.radbtn_F3_IDAL = new System.Windows.Forms.RadioButton();
            this.radbtn_F3_DAL = new System.Windows.Forms.RadioButton();
            this.radbtn_F3_Model = new System.Windows.Forms.RadioButton();
            this.groupBox_FrameSel = new System.Windows.Forms.GroupBox();
            this.radbtn_Frame_F3 = new System.Windows.Forms.RadioButton();
            this.radbtn_Frame_S3 = new System.Windows.Forms.RadioButton();
            this.radbtn_Frame_One = new System.Windows.Forms.RadioButton();
            this.groupBox_DB = new System.Windows.Forms.GroupBox();
            this.chk_DB_GetList = new System.Windows.Forms.CheckBox();
            this.chk_DB_GetModel = new System.Windows.Forms.CheckBox();
            this.chk_DB_Delete = new System.Windows.Forms.CheckBox();
            this.chk_DB_Update = new System.Windows.Forms.CheckBox();
            this.chk_DB_Add = new System.Windows.Forms.CheckBox();
            this.chk_DB_Exists = new System.Windows.Forms.CheckBox();
            this.chk_DB_GetMaxID = new System.Windows.Forms.CheckBox();
            this.txtTabname = new System.Windows.Forms.TextBox();
            this.txtProcPrefix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radbtn_DB_DDL = new System.Windows.Forms.RadioButton();
            this.radbtn_DB_Proc = new System.Windows.Forms.RadioButton();
            this.groupBox_Type = new System.Windows.Forms.GroupBox();
            this.radbtn_Type_Web = new System.Windows.Forms.RadioButton();
            this.radbtn_Type_CS = new System.Windows.Forms.RadioButton();
            this.radbtn_Type_DB = new System.Windows.Forms.RadioButton();
            this.groupBox_Parameter = new System.Windows.Forms.GroupBox();
            this.txtNameSpace2 = new System.Windows.Forms.TextBox();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imgListTabpage = new System.Windows.Forms.ImageList(this.components);
            this.imglistDB = new System.Windows.Forms.ImageList(this.components);
            this.imglistView = new System.Windows.Forms.ImageList(this.components);
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.详细信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_Web.SuspendLayout();
            this.groupBox_AppType.SuspendLayout();
            this.groupBox_Method.SuspendLayout();
            this.groupBox_F3.SuspendLayout();
            this.groupBox_FrameSel.SuspendLayout();
            this.groupBox_DB.SuspendLayout();
            this.groupBox_Type.SuspendLayout();
            this.groupBox_Parameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imgListTabpage;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 641);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_Next);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(627, 614);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "数据源";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(473, 283);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(82, 23);
            this.btn_Next.TabIndex = 3;
            this.btn_Next.Text = "继续设置>>";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 277);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtClassName2);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.listView2);
            this.groupBox1.Controls.Add(this.txtClassName);
            this.groupBox1.Controls.Add(this.cmbox_PTab);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbox_PField);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbox_STab);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cmbox_SField);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btn_SelAll);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btn_SelAll2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btn_SelI);
            this.groupBox1.Controls.Add(this.btn_SelI2);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 264);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表与字段";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "父表：";
            // 
            // txtClassName2
            // 
            this.txtClassName2.Location = new System.Drawing.Point(400, 230);
            this.txtClassName2.Name = "txtClassName2";
            this.txtClassName2.Size = new System.Drawing.Size(200, 21);
            this.txtClassName2.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(86, 51);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(200, 147);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(323, 237);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "类名：";
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(400, 51);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(200, 147);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(86, 230);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(200, 21);
            this.txtClassName.TabIndex = 4;
            // 
            // cmbox_PTab
            // 
            this.cmbox_PTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_PTab.FormattingEnabled = true;
            this.cmbox_PTab.Location = new System.Drawing.Point(86, 25);
            this.cmbox_PTab.Name = "cmbox_PTab";
            this.cmbox_PTab.Size = new System.Drawing.Size(200, 20);
            this.cmbox_PTab.TabIndex = 1;
            this.cmbox_PTab.SelectedIndexChanged += new System.EventHandler(this.cmbox_PTab_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Model类名：";
            // 
            // cmbox_PField
            // 
            this.cmbox_PField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_PField.FormattingEnabled = true;
            this.cmbox_PField.Location = new System.Drawing.Point(86, 204);
            this.cmbox_PField.Name = "cmbox_PField";
            this.cmbox_PField.Size = new System.Drawing.Size(200, 20);
            this.cmbox_PField.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(353, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "子表：";
            // 
            // cmbox_STab
            // 
            this.cmbox_STab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_STab.FormattingEnabled = true;
            this.cmbox_STab.Location = new System.Drawing.Point(400, 22);
            this.cmbox_STab.Name = "cmbox_STab";
            this.cmbox_STab.Size = new System.Drawing.Size(200, 20);
            this.cmbox_STab.TabIndex = 1;
            this.cmbox_STab.SelectedIndexChanged += new System.EventHandler(this.cmbox_STab_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(288, 208);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "<=====>外键字段：";
            // 
            // cmbox_SField
            // 
            this.cmbox_SField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_SField.FormattingEnabled = true;
            this.cmbox_SField.Location = new System.Drawing.Point(400, 204);
            this.cmbox_SField.Name = "cmbox_SField";
            this.cmbox_SField.Size = new System.Drawing.Size(200, 20);
            this.cmbox_SField.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 207);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "主键字段：";
            // 
            // btn_SelAll
            // 
            this.btn_SelAll.Location = new System.Drawing.Point(10, 86);
            this.btn_SelAll.Name = "btn_SelAll";
            this.btn_SelAll.Size = new System.Drawing.Size(70, 23);
            this.btn_SelAll.TabIndex = 0;
            this.btn_SelAll.Text = "全选(&A)";
            this.btn_SelAll.UseVisualStyleBackColor = true;
            this.btn_SelAll.Click += new System.EventHandler(this.btn_SelAll_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(353, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "字段：";
            // 
            // btn_SelAll2
            // 
            this.btn_SelAll2.Location = new System.Drawing.Point(324, 86);
            this.btn_SelAll2.Name = "btn_SelAll2";
            this.btn_SelAll2.Size = new System.Drawing.Size(70, 23);
            this.btn_SelAll2.TabIndex = 0;
            this.btn_SelAll2.Text = "全选(&A)";
            this.btn_SelAll2.UseVisualStyleBackColor = true;
            this.btn_SelAll2.Click += new System.EventHandler(this.btn_SelAll2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "字段：";
            // 
            // btn_SelI
            // 
            this.btn_SelI.Location = new System.Drawing.Point(10, 115);
            this.btn_SelI.Name = "btn_SelI";
            this.btn_SelI.Size = new System.Drawing.Size(70, 23);
            this.btn_SelI.TabIndex = 0;
            this.btn_SelI.Text = "反选(&I)";
            this.btn_SelI.UseVisualStyleBackColor = true;
            this.btn_SelI.Click += new System.EventHandler(this.btn_SelI_Click);
            // 
            // btn_SelI2
            // 
            this.btn_SelI2.Location = new System.Drawing.Point(324, 115);
            this.btn_SelI2.Name = "btn_SelI2";
            this.btn_SelI2.Size = new System.Drawing.Size(70, 23);
            this.btn_SelI2.TabIndex = 0;
            this.btn_SelI2.Text = "反选(&I)";
            this.btn_SelI2.UseVisualStyleBackColor = true;
            this.btn_SelI2.Click += new System.EventHandler(this.btn_SelI2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 614);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "代码类型";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Ok);
            this.panel2.Controls.Add(this.groupBox_Web);
            this.panel2.Controls.Add(this.groupBox_AppType);
            this.panel2.Controls.Add(this.groupBox_Method);
            this.panel2.Controls.Add(this.groupBox_DALType);
            this.panel2.Controls.Add(this.groupBox_F3);
            this.panel2.Controls.Add(this.groupBox_FrameSel);
            this.panel2.Controls.Add(this.groupBox_DB);
            this.panel2.Controls.Add(this.groupBox_Type);
            this.panel2.Controls.Add(this.groupBox_Parameter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(621, 608);
            this.panel2.TabIndex = 2;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ok.Location = new System.Drawing.Point(489, 429);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(85, 23);
            this.btn_Ok.TabIndex = 7;
            this.btn_Ok.Text = "生成类代码>>";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // groupBox_Web
            // 
            this.groupBox_Web.Controls.Add(this.chk_Web_Show);
            this.groupBox_Web.Controls.Add(this.chk_Web_Update);
            this.groupBox_Web.Controls.Add(this.chk_Web_HasKey);
            this.groupBox_Web.Controls.Add(this.chk_Web_Add);
            this.groupBox_Web.Controls.Add(this.radbtn_Web_AspxCS);
            this.groupBox_Web.Controls.Add(this.radbtn_Web_Aspx);
            this.groupBox_Web.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Web.Location = new System.Drawing.Point(0, 424);
            this.groupBox_Web.Name = "groupBox_Web";
            this.groupBox_Web.Size = new System.Drawing.Size(621, 67);
            this.groupBox_Web.TabIndex = 6;
            this.groupBox_Web.TabStop = false;
            this.groupBox_Web.Text = "Web页面";
            // 
            // chk_Web_Show
            // 
            this.chk_Web_Show.AutoSize = true;
            this.chk_Web_Show.Checked = true;
            this.chk_Web_Show.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Web_Show.Location = new System.Drawing.Point(292, 45);
            this.chk_Web_Show.Name = "chk_Web_Show";
            this.chk_Web_Show.Size = new System.Drawing.Size(60, 16);
            this.chk_Web_Show.TabIndex = 3;
            this.chk_Web_Show.Text = "显示页";
            this.chk_Web_Show.UseVisualStyleBackColor = true;
            // 
            // chk_Web_Update
            // 
            this.chk_Web_Update.AutoSize = true;
            this.chk_Web_Update.Checked = true;
            this.chk_Web_Update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Web_Update.Location = new System.Drawing.Point(217, 45);
            this.chk_Web_Update.Name = "chk_Web_Update";
            this.chk_Web_Update.Size = new System.Drawing.Size(60, 16);
            this.chk_Web_Update.TabIndex = 3;
            this.chk_Web_Update.Text = "修改页";
            this.chk_Web_Update.UseVisualStyleBackColor = true;
            // 
            // chk_Web_HasKey
            // 
            this.chk_Web_HasKey.AutoSize = true;
            this.chk_Web_HasKey.Checked = true;
            this.chk_Web_HasKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Web_HasKey.Location = new System.Drawing.Point(55, 45);
            this.chk_Web_HasKey.Name = "chk_Web_HasKey";
            this.chk_Web_HasKey.Size = new System.Drawing.Size(72, 16);
            this.chk_Web_HasKey.TabIndex = 3;
            this.chk_Web_HasKey.Text = "包括主键";
            this.chk_Web_HasKey.UseVisualStyleBackColor = true;
            // 
            // chk_Web_Add
            // 
            this.chk_Web_Add.AutoSize = true;
            this.chk_Web_Add.Checked = true;
            this.chk_Web_Add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Web_Add.Location = new System.Drawing.Point(142, 45);
            this.chk_Web_Add.Name = "chk_Web_Add";
            this.chk_Web_Add.Size = new System.Drawing.Size(60, 16);
            this.chk_Web_Add.TabIndex = 3;
            this.chk_Web_Add.Text = "增加页";
            this.chk_Web_Add.UseVisualStyleBackColor = true;
            // 
            // radbtn_Web_AspxCS
            // 
            this.radbtn_Web_AspxCS.AutoSize = true;
            this.radbtn_Web_AspxCS.Location = new System.Drawing.Point(158, 20);
            this.radbtn_Web_AspxCS.Name = "radbtn_Web_AspxCS";
            this.radbtn_Web_AspxCS.Size = new System.Drawing.Size(89, 16);
            this.radbtn_Web_AspxCS.TabIndex = 2;
            this.radbtn_Web_AspxCS.Text = "Aspx.CS代码";
            this.radbtn_Web_AspxCS.UseVisualStyleBackColor = true;
            // 
            // radbtn_Web_Aspx
            // 
            this.radbtn_Web_Aspx.AutoSize = true;
            this.radbtn_Web_Aspx.Checked = true;
            this.radbtn_Web_Aspx.Location = new System.Drawing.Point(56, 20);
            this.radbtn_Web_Aspx.Name = "radbtn_Web_Aspx";
            this.radbtn_Web_Aspx.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Web_Aspx.TabIndex = 1;
            this.radbtn_Web_Aspx.TabStop = true;
            this.radbtn_Web_Aspx.Text = "Aspx页面";
            this.radbtn_Web_Aspx.UseVisualStyleBackColor = true;
            // 
            // groupBox_AppType
            // 
            this.groupBox_AppType.Controls.Add(this.radbtn_AppType_Winform);
            this.groupBox_AppType.Controls.Add(this.radbtn_AppType_Web);
            this.groupBox_AppType.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_AppType.Location = new System.Drawing.Point(0, 384);
            this.groupBox_AppType.Name = "groupBox_AppType";
            this.groupBox_AppType.Size = new System.Drawing.Size(621, 40);
            this.groupBox_AppType.TabIndex = 6;
            this.groupBox_AppType.TabStop = false;
            this.groupBox_AppType.Text = "应用系统类型";
            // 
            // radbtn_AppType_Winform
            // 
            this.radbtn_AppType_Winform.AutoSize = true;
            this.radbtn_AppType_Winform.Location = new System.Drawing.Point(186, 17);
            this.radbtn_AppType_Winform.Name = "radbtn_AppType_Winform";
            this.radbtn_AppType_Winform.Size = new System.Drawing.Size(89, 16);
            this.radbtn_AppType_Winform.TabIndex = 2;
            this.radbtn_AppType_Winform.Text = "WinForm系统";
            this.radbtn_AppType_Winform.UseVisualStyleBackColor = true;
            // 
            // radbtn_AppType_Web
            // 
            this.radbtn_AppType_Web.AutoSize = true;
            this.radbtn_AppType_Web.Checked = true;
            this.radbtn_AppType_Web.Location = new System.Drawing.Point(84, 17);
            this.radbtn_AppType_Web.Name = "radbtn_AppType_Web";
            this.radbtn_AppType_Web.Size = new System.Drawing.Size(65, 16);
            this.radbtn_AppType_Web.TabIndex = 1;
            this.radbtn_AppType_Web.TabStop = true;
            this.radbtn_AppType_Web.Text = "Web系统";
            this.radbtn_AppType_Web.UseVisualStyleBackColor = true;
            // 
            // groupBox_Method
            // 
            this.groupBox_Method.Controls.Add(this.chk_CS_GetList);
            this.groupBox_Method.Controls.Add(this.chk_CS_GetModelByCache);
            this.groupBox_Method.Controls.Add(this.chk_CS_GetModel);
            this.groupBox_Method.Controls.Add(this.chk_CS_Delete);
            this.groupBox_Method.Controls.Add(this.chk_CS_Update);
            this.groupBox_Method.Controls.Add(this.chk_CS_Add);
            this.groupBox_Method.Controls.Add(this.chk_CS_Exists);
            this.groupBox_Method.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Method.Location = new System.Drawing.Point(0, 342);
            this.groupBox_Method.Name = "groupBox_Method";
            this.groupBox_Method.Size = new System.Drawing.Size(621, 42);
            this.groupBox_Method.TabIndex = 5;
            this.groupBox_Method.TabStop = false;
            this.groupBox_Method.Text = "方法选择";
            // 
            // chk_CS_GetList
            // 
            this.chk_CS_GetList.AutoSize = true;
            this.chk_CS_GetList.Checked = true;
            this.chk_CS_GetList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_GetList.Location = new System.Drawing.Point(491, 18);
            this.chk_CS_GetList.Name = "chk_CS_GetList";
            this.chk_CS_GetList.Size = new System.Drawing.Size(66, 16);
            this.chk_CS_GetList.TabIndex = 8;
            this.chk_CS_GetList.Text = "GetList";
            this.chk_CS_GetList.UseVisualStyleBackColor = true;
            // 
            // chk_CS_GetModelByCache
            // 
            this.chk_CS_GetModelByCache.AutoSize = true;
            this.chk_CS_GetModelByCache.Checked = true;
            this.chk_CS_GetModelByCache.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_GetModelByCache.Location = new System.Drawing.Point(377, 18);
            this.chk_CS_GetModelByCache.Name = "chk_CS_GetModelByCache";
            this.chk_CS_GetModelByCache.Size = new System.Drawing.Size(114, 16);
            this.chk_CS_GetModelByCache.TabIndex = 9;
            this.chk_CS_GetModelByCache.Text = "GetModelByCache";
            this.chk_CS_GetModelByCache.UseVisualStyleBackColor = true;
            // 
            // chk_CS_GetModel
            // 
            this.chk_CS_GetModel.AutoSize = true;
            this.chk_CS_GetModel.Checked = true;
            this.chk_CS_GetModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_GetModel.Location = new System.Drawing.Point(305, 18);
            this.chk_CS_GetModel.Name = "chk_CS_GetModel";
            this.chk_CS_GetModel.Size = new System.Drawing.Size(72, 16);
            this.chk_CS_GetModel.TabIndex = 9;
            this.chk_CS_GetModel.Text = "GetModel";
            this.chk_CS_GetModel.UseVisualStyleBackColor = true;
            // 
            // chk_CS_Delete
            // 
            this.chk_CS_Delete.AutoSize = true;
            this.chk_CS_Delete.Checked = true;
            this.chk_CS_Delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_Delete.Location = new System.Drawing.Point(245, 18);
            this.chk_CS_Delete.Name = "chk_CS_Delete";
            this.chk_CS_Delete.Size = new System.Drawing.Size(60, 16);
            this.chk_CS_Delete.TabIndex = 10;
            this.chk_CS_Delete.Text = "Delete";
            this.chk_CS_Delete.UseVisualStyleBackColor = true;
            // 
            // chk_CS_Update
            // 
            this.chk_CS_Update.AutoSize = true;
            this.chk_CS_Update.Checked = true;
            this.chk_CS_Update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_Update.Location = new System.Drawing.Point(185, 18);
            this.chk_CS_Update.Name = "chk_CS_Update";
            this.chk_CS_Update.Size = new System.Drawing.Size(60, 16);
            this.chk_CS_Update.TabIndex = 7;
            this.chk_CS_Update.Text = "Update";
            this.chk_CS_Update.UseVisualStyleBackColor = true;
            // 
            // chk_CS_Add
            // 
            this.chk_CS_Add.AutoSize = true;
            this.chk_CS_Add.Checked = true;
            this.chk_CS_Add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_Add.Location = new System.Drawing.Point(143, 18);
            this.chk_CS_Add.Name = "chk_CS_Add";
            this.chk_CS_Add.Size = new System.Drawing.Size(42, 16);
            this.chk_CS_Add.TabIndex = 4;
            this.chk_CS_Add.Text = "Add";
            this.chk_CS_Add.UseVisualStyleBackColor = true;
            // 
            // chk_CS_Exists
            // 
            this.chk_CS_Exists.AutoSize = true;
            this.chk_CS_Exists.Checked = true;
            this.chk_CS_Exists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_CS_Exists.Location = new System.Drawing.Point(82, 18);
            this.chk_CS_Exists.Name = "chk_CS_Exists";
            this.chk_CS_Exists.Size = new System.Drawing.Size(60, 16);
            this.chk_CS_Exists.TabIndex = 5;
            this.chk_CS_Exists.Text = "Exists";
            this.chk_CS_Exists.UseVisualStyleBackColor = true;
            // 
            // groupBox_DALType
            // 
            this.groupBox_DALType.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_DALType.Location = new System.Drawing.Point(0, 300);
            this.groupBox_DALType.Name = "groupBox_DALType";
            this.groupBox_DALType.Size = new System.Drawing.Size(621, 42);
            this.groupBox_DALType.TabIndex = 4;
            this.groupBox_DALType.TabStop = false;
            this.groupBox_DALType.Text = "代码模板组件类型";
            // 
            // groupBox_F3
            // 
            this.groupBox_F3.Controls.Add(this.radbtn_F3_BLL);
            this.groupBox_F3.Controls.Add(this.radbtn_F3_DALFactory);
            this.groupBox_F3.Controls.Add(this.radbtn_F3_IDAL);
            this.groupBox_F3.Controls.Add(this.radbtn_F3_DAL);
            this.groupBox_F3.Controls.Add(this.radbtn_F3_Model);
            this.groupBox_F3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_F3.Location = new System.Drawing.Point(0, 260);
            this.groupBox_F3.Name = "groupBox_F3";
            this.groupBox_F3.Size = new System.Drawing.Size(621, 40);
            this.groupBox_F3.TabIndex = 4;
            this.groupBox_F3.TabStop = false;
            this.groupBox_F3.Text = "代码类型";
            // 
            // radbtn_F3_BLL
            // 
            this.radbtn_F3_BLL.AutoSize = true;
            this.radbtn_F3_BLL.Location = new System.Drawing.Point(342, 18);
            this.radbtn_F3_BLL.Name = "radbtn_F3_BLL";
            this.radbtn_F3_BLL.Size = new System.Drawing.Size(41, 16);
            this.radbtn_F3_BLL.TabIndex = 0;
            this.radbtn_F3_BLL.Text = "BLL";
            this.radbtn_F3_BLL.UseVisualStyleBackColor = true;
            this.radbtn_F3_BLL.Click += new System.EventHandler(this.radbtn_F3_Click);
            // 
            // radbtn_F3_DALFactory
            // 
            this.radbtn_F3_DALFactory.AutoSize = true;
            this.radbtn_F3_DALFactory.Location = new System.Drawing.Point(244, 18);
            this.radbtn_F3_DALFactory.Name = "radbtn_F3_DALFactory";
            this.radbtn_F3_DALFactory.Size = new System.Drawing.Size(83, 16);
            this.radbtn_F3_DALFactory.TabIndex = 0;
            this.radbtn_F3_DALFactory.Text = "DALFactory";
            this.radbtn_F3_DALFactory.UseVisualStyleBackColor = true;
            this.radbtn_F3_DALFactory.Click += new System.EventHandler(this.radbtn_F3_Click);
            // 
            // radbtn_F3_IDAL
            // 
            this.radbtn_F3_IDAL.AutoSize = true;
            this.radbtn_F3_IDAL.Location = new System.Drawing.Point(182, 18);
            this.radbtn_F3_IDAL.Name = "radbtn_F3_IDAL";
            this.radbtn_F3_IDAL.Size = new System.Drawing.Size(47, 16);
            this.radbtn_F3_IDAL.TabIndex = 0;
            this.radbtn_F3_IDAL.Text = "IDAL";
            this.radbtn_F3_IDAL.UseVisualStyleBackColor = true;
            this.radbtn_F3_IDAL.Click += new System.EventHandler(this.radbtn_F3_Click);
            // 
            // radbtn_F3_DAL
            // 
            this.radbtn_F3_DAL.AutoSize = true;
            this.radbtn_F3_DAL.Location = new System.Drawing.Point(126, 18);
            this.radbtn_F3_DAL.Name = "radbtn_F3_DAL";
            this.radbtn_F3_DAL.Size = new System.Drawing.Size(41, 16);
            this.radbtn_F3_DAL.TabIndex = 0;
            this.radbtn_F3_DAL.Text = "DAL";
            this.radbtn_F3_DAL.UseVisualStyleBackColor = true;
            this.radbtn_F3_DAL.Click += new System.EventHandler(this.radbtn_F3_Click);
            // 
            // radbtn_F3_Model
            // 
            this.radbtn_F3_Model.AutoSize = true;
            this.radbtn_F3_Model.Checked = true;
            this.radbtn_F3_Model.Location = new System.Drawing.Point(58, 18);
            this.radbtn_F3_Model.Name = "radbtn_F3_Model";
            this.radbtn_F3_Model.Size = new System.Drawing.Size(53, 16);
            this.radbtn_F3_Model.TabIndex = 0;
            this.radbtn_F3_Model.TabStop = true;
            this.radbtn_F3_Model.Text = "Model";
            this.radbtn_F3_Model.UseVisualStyleBackColor = true;
            this.radbtn_F3_Model.Click += new System.EventHandler(this.radbtn_F3_Click);
            // 
            // groupBox_FrameSel
            // 
            this.groupBox_FrameSel.Controls.Add(this.radbtn_Frame_F3);
            this.groupBox_FrameSel.Controls.Add(this.radbtn_Frame_S3);
            this.groupBox_FrameSel.Controls.Add(this.radbtn_Frame_One);
            this.groupBox_FrameSel.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_FrameSel.Location = new System.Drawing.Point(0, 220);
            this.groupBox_FrameSel.Name = "groupBox_FrameSel";
            this.groupBox_FrameSel.Size = new System.Drawing.Size(621, 40);
            this.groupBox_FrameSel.TabIndex = 4;
            this.groupBox_FrameSel.TabStop = false;
            this.groupBox_FrameSel.Text = "架构选择";
            // 
            // radbtn_Frame_F3
            // 
            this.radbtn_Frame_F3.AutoSize = true;
            this.radbtn_Frame_F3.Checked = true;
            this.radbtn_Frame_F3.Location = new System.Drawing.Point(245, 16);
            this.radbtn_Frame_F3.Name = "radbtn_Frame_F3";
            this.radbtn_Frame_F3.Size = new System.Drawing.Size(95, 16);
            this.radbtn_Frame_F3.TabIndex = 0;
            this.radbtn_Frame_F3.TabStop = true;
            this.radbtn_Frame_F3.Text = "工厂模式三层";
            this.radbtn_Frame_F3.UseVisualStyleBackColor = true;
            this.radbtn_Frame_F3.Click += new System.EventHandler(this.radbtn_Frame_Click);
            // 
            // radbtn_Frame_S3
            // 
            this.radbtn_Frame_S3.AutoSize = true;
            this.radbtn_Frame_S3.Location = new System.Drawing.Point(156, 16);
            this.radbtn_Frame_S3.Name = "radbtn_Frame_S3";
            this.radbtn_Frame_S3.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Frame_S3.TabIndex = 0;
            this.radbtn_Frame_S3.Text = "简单三层";
            this.radbtn_Frame_S3.UseVisualStyleBackColor = true;
            this.radbtn_Frame_S3.Click += new System.EventHandler(this.radbtn_Frame_Click);
            // 
            // radbtn_Frame_One
            // 
            this.radbtn_Frame_One.AutoSize = true;
            this.radbtn_Frame_One.Location = new System.Drawing.Point(58, 16);
            this.radbtn_Frame_One.Name = "radbtn_Frame_One";
            this.radbtn_Frame_One.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Frame_One.TabIndex = 0;
            this.radbtn_Frame_One.Text = "单类结构";
            this.radbtn_Frame_One.UseVisualStyleBackColor = true;
            this.radbtn_Frame_One.Visible = false;
            this.radbtn_Frame_One.Click += new System.EventHandler(this.radbtn_Frame_Click);
            // 
            // groupBox_DB
            // 
            this.groupBox_DB.Controls.Add(this.chk_DB_GetList);
            this.groupBox_DB.Controls.Add(this.chk_DB_GetModel);
            this.groupBox_DB.Controls.Add(this.chk_DB_Delete);
            this.groupBox_DB.Controls.Add(this.chk_DB_Update);
            this.groupBox_DB.Controls.Add(this.chk_DB_Add);
            this.groupBox_DB.Controls.Add(this.chk_DB_Exists);
            this.groupBox_DB.Controls.Add(this.chk_DB_GetMaxID);
            this.groupBox_DB.Controls.Add(this.txtTabname);
            this.groupBox_DB.Controls.Add(this.txtProcPrefix);
            this.groupBox_DB.Controls.Add(this.label6);
            this.groupBox_DB.Controls.Add(this.label7);
            this.groupBox_DB.Controls.Add(this.label5);
            this.groupBox_DB.Controls.Add(this.radbtn_DB_DDL);
            this.groupBox_DB.Controls.Add(this.radbtn_DB_Proc);
            this.groupBox_DB.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_DB.Location = new System.Drawing.Point(0, 118);
            this.groupBox_DB.Name = "groupBox_DB";
            this.groupBox_DB.Size = new System.Drawing.Size(621, 102);
            this.groupBox_DB.TabIndex = 3;
            this.groupBox_DB.TabStop = false;
            this.groupBox_DB.Text = "数据库脚本";
            // 
            // chk_DB_GetList
            // 
            this.chk_DB_GetList.AutoSize = true;
            this.chk_DB_GetList.Checked = true;
            this.chk_DB_GetList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_GetList.Location = new System.Drawing.Point(486, 77);
            this.chk_DB_GetList.Name = "chk_DB_GetList";
            this.chk_DB_GetList.Size = new System.Drawing.Size(66, 16);
            this.chk_DB_GetList.TabIndex = 3;
            this.chk_DB_GetList.Text = "GetList";
            this.chk_DB_GetList.UseVisualStyleBackColor = true;
            // 
            // chk_DB_GetModel
            // 
            this.chk_DB_GetModel.AutoSize = true;
            this.chk_DB_GetModel.Checked = true;
            this.chk_DB_GetModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_GetModel.Location = new System.Drawing.Point(412, 77);
            this.chk_DB_GetModel.Name = "chk_DB_GetModel";
            this.chk_DB_GetModel.Size = new System.Drawing.Size(72, 16);
            this.chk_DB_GetModel.TabIndex = 3;
            this.chk_DB_GetModel.Text = "GetModel";
            this.chk_DB_GetModel.UseVisualStyleBackColor = true;
            // 
            // chk_DB_Delete
            // 
            this.chk_DB_Delete.AutoSize = true;
            this.chk_DB_Delete.Checked = true;
            this.chk_DB_Delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_Delete.Location = new System.Drawing.Point(350, 77);
            this.chk_DB_Delete.Name = "chk_DB_Delete";
            this.chk_DB_Delete.Size = new System.Drawing.Size(60, 16);
            this.chk_DB_Delete.TabIndex = 3;
            this.chk_DB_Delete.Text = "Delete";
            this.chk_DB_Delete.UseVisualStyleBackColor = true;
            // 
            // chk_DB_Update
            // 
            this.chk_DB_Update.AutoSize = true;
            this.chk_DB_Update.Checked = true;
            this.chk_DB_Update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_Update.Location = new System.Drawing.Point(288, 77);
            this.chk_DB_Update.Name = "chk_DB_Update";
            this.chk_DB_Update.Size = new System.Drawing.Size(60, 16);
            this.chk_DB_Update.TabIndex = 3;
            this.chk_DB_Update.Text = "Update";
            this.chk_DB_Update.UseVisualStyleBackColor = true;
            // 
            // chk_DB_Add
            // 
            this.chk_DB_Add.AutoSize = true;
            this.chk_DB_Add.Checked = true;
            this.chk_DB_Add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_Add.Location = new System.Drawing.Point(244, 77);
            this.chk_DB_Add.Name = "chk_DB_Add";
            this.chk_DB_Add.Size = new System.Drawing.Size(42, 16);
            this.chk_DB_Add.TabIndex = 3;
            this.chk_DB_Add.Text = "Add";
            this.chk_DB_Add.UseVisualStyleBackColor = true;
            // 
            // chk_DB_Exists
            // 
            this.chk_DB_Exists.AutoSize = true;
            this.chk_DB_Exists.Checked = true;
            this.chk_DB_Exists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_Exists.Location = new System.Drawing.Point(182, 77);
            this.chk_DB_Exists.Name = "chk_DB_Exists";
            this.chk_DB_Exists.Size = new System.Drawing.Size(60, 16);
            this.chk_DB_Exists.TabIndex = 3;
            this.chk_DB_Exists.Text = "Exists";
            this.chk_DB_Exists.UseVisualStyleBackColor = true;
            // 
            // chk_DB_GetMaxID
            // 
            this.chk_DB_GetMaxID.AutoSize = true;
            this.chk_DB_GetMaxID.Checked = true;
            this.chk_DB_GetMaxID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_DB_GetMaxID.Location = new System.Drawing.Point(108, 77);
            this.chk_DB_GetMaxID.Name = "chk_DB_GetMaxID";
            this.chk_DB_GetMaxID.Size = new System.Drawing.Size(72, 16);
            this.chk_DB_GetMaxID.TabIndex = 3;
            this.chk_DB_GetMaxID.Text = "GetMaxID";
            this.chk_DB_GetMaxID.UseVisualStyleBackColor = true;
            // 
            // txtTabname
            // 
            this.txtTabname.Location = new System.Drawing.Point(201, 49);
            this.txtTabname.Name = "txtTabname";
            this.txtTabname.Size = new System.Drawing.Size(75, 21);
            this.txtTabname.TabIndex = 2;
            // 
            // txtProcPrefix
            // 
            this.txtProcPrefix.Location = new System.Drawing.Point(108, 48);
            this.txtProcPrefix.Name = "txtProcPrefix";
            this.txtProcPrefix.Size = new System.Drawing.Size(75, 21);
            this.txtProcPrefix.TabIndex = 2;
            this.txtProcPrefix.Text = "UP_";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "存储过程方法：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "+";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "存储过程前缀：";
            // 
            // radbtn_DB_DDL
            // 
            this.radbtn_DB_DDL.AutoSize = true;
            this.radbtn_DB_DDL.Location = new System.Drawing.Point(169, 20);
            this.radbtn_DB_DDL.Name = "radbtn_DB_DDL";
            this.radbtn_DB_DDL.Size = new System.Drawing.Size(89, 16);
            this.radbtn_DB_DDL.TabIndex = 0;
            this.radbtn_DB_DDL.Text = "DDL数据脚本";
            this.radbtn_DB_DDL.UseVisualStyleBackColor = true;
            this.radbtn_DB_DDL.Click += new System.EventHandler(this.radbtn_DBSel_Click);
            // 
            // radbtn_DB_Proc
            // 
            this.radbtn_DB_Proc.AutoSize = true;
            this.radbtn_DB_Proc.Checked = true;
            this.radbtn_DB_Proc.Location = new System.Drawing.Point(58, 20);
            this.radbtn_DB_Proc.Name = "radbtn_DB_Proc";
            this.radbtn_DB_Proc.Size = new System.Drawing.Size(71, 16);
            this.radbtn_DB_Proc.TabIndex = 0;
            this.radbtn_DB_Proc.TabStop = true;
            this.radbtn_DB_Proc.Text = "存储过程";
            this.radbtn_DB_Proc.UseVisualStyleBackColor = true;
            this.radbtn_DB_Proc.Click += new System.EventHandler(this.radbtn_DBSel_Click);
            // 
            // groupBox_Type
            // 
            this.groupBox_Type.Controls.Add(this.radbtn_Type_Web);
            this.groupBox_Type.Controls.Add(this.radbtn_Type_CS);
            this.groupBox_Type.Controls.Add(this.radbtn_Type_DB);
            this.groupBox_Type.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Type.Location = new System.Drawing.Point(0, 74);
            this.groupBox_Type.Name = "groupBox_Type";
            this.groupBox_Type.Size = new System.Drawing.Size(621, 44);
            this.groupBox_Type.TabIndex = 2;
            this.groupBox_Type.TabStop = false;
            this.groupBox_Type.Text = "类型";
            // 
            // radbtn_Type_Web
            // 
            this.radbtn_Type_Web.AutoSize = true;
            this.radbtn_Type_Web.Location = new System.Drawing.Point(242, 20);
            this.radbtn_Type_Web.Name = "radbtn_Type_Web";
            this.radbtn_Type_Web.Size = new System.Drawing.Size(65, 16);
            this.radbtn_Type_Web.TabIndex = 0;
            this.radbtn_Type_Web.Text = "Web页面";
            this.radbtn_Type_Web.UseVisualStyleBackColor = true;
            this.radbtn_Type_Web.Click += new System.EventHandler(this.radbtn_Type_Click);
            // 
            // radbtn_Type_CS
            // 
            this.radbtn_Type_CS.AutoSize = true;
            this.radbtn_Type_CS.Checked = true;
            this.radbtn_Type_CS.Location = new System.Drawing.Point(152, 20);
            this.radbtn_Type_CS.Name = "radbtn_Type_CS";
            this.radbtn_Type_CS.Size = new System.Drawing.Size(59, 16);
            this.radbtn_Type_CS.TabIndex = 0;
            this.radbtn_Type_CS.TabStop = true;
            this.radbtn_Type_CS.Text = "C#代码";
            this.radbtn_Type_CS.UseVisualStyleBackColor = true;
            this.radbtn_Type_CS.Click += new System.EventHandler(this.radbtn_Type_Click);
            // 
            // radbtn_Type_DB
            // 
            this.radbtn_Type_DB.AutoSize = true;
            this.radbtn_Type_DB.Location = new System.Drawing.Point(58, 20);
            this.radbtn_Type_DB.Name = "radbtn_Type_DB";
            this.radbtn_Type_DB.Size = new System.Drawing.Size(59, 16);
            this.radbtn_Type_DB.TabIndex = 0;
            this.radbtn_Type_DB.Text = "DB脚本";
            this.radbtn_Type_DB.UseVisualStyleBackColor = true;
            this.radbtn_Type_DB.Visible = false;
            this.radbtn_Type_DB.Click += new System.EventHandler(this.radbtn_Type_Click);
            // 
            // groupBox_Parameter
            // 
            this.groupBox_Parameter.Controls.Add(this.txtNameSpace2);
            this.groupBox_Parameter.Controls.Add(this.txtNameSpace);
            this.groupBox_Parameter.Controls.Add(this.txtProjectName);
            this.groupBox_Parameter.Controls.Add(this.label4);
            this.groupBox_Parameter.Controls.Add(this.label2);
            this.groupBox_Parameter.Controls.Add(this.label1);
            this.groupBox_Parameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Parameter.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Parameter.Name = "groupBox_Parameter";
            this.groupBox_Parameter.Size = new System.Drawing.Size(621, 74);
            this.groupBox_Parameter.TabIndex = 1;
            this.groupBox_Parameter.TabStop = false;
            this.groupBox_Parameter.Text = "参数";
            // 
            // txtNameSpace2
            // 
            this.txtNameSpace2.Location = new System.Drawing.Point(382, 20);
            this.txtNameSpace2.Name = "txtNameSpace2";
            this.txtNameSpace2.Size = new System.Drawing.Size(100, 21);
            this.txtNameSpace2.TabIndex = 1;
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(111, 43);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(100, 21);
            this.txtNameSpace.TabIndex = 1;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(111, 20);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(100, 21);
            this.txtProjectName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "二级命名空间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "顶级命名空间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名称：";
            // 
            // tabPage2
            // 
            this.tabPage2.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(627, 614);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "代码查看";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imgListTabpage
            // 
            this.imgListTabpage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListTabpage.ImageStream")));
            this.imgListTabpage.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListTabpage.Images.SetKeyName(0, "dotNET.ico");
            this.imgListTabpage.Images.SetKeyName(1, "cs.gif");
            this.imgListTabpage.Images.SetKeyName(2, "tab2.gif");
            // 
            // imglistDB
            // 
            this.imglistDB.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistDB.ImageStream")));
            this.imglistDB.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistDB.Images.SetKeyName(0, "db.gif");
            // 
            // imglistView
            // 
            this.imglistView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistView.ImageStream")));
            this.imglistView.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistView.Images.SetKeyName(0, "server.ico");
            this.imglistView.Images.SetKeyName(1, "db.gif");
            this.imglistView.Images.SetKeyName(2, "tab2.gif");
            this.imglistView.Images.SetKeyName(3, "view.gif");
            this.imglistView.Images.SetKeyName(4, "fild2.gif");
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.列表ToolStripMenuItem,
            this.详细信息ToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(64, 23);
            this.toolStripSplitButton1.Text = "列表";
            // 
            // 列表ToolStripMenuItem
            // 
            this.列表ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("列表ToolStripMenuItem.Image")));
            this.列表ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.列表ToolStripMenuItem.Name = "列表ToolStripMenuItem";
            this.列表ToolStripMenuItem.Size = new System.Drawing.Size(121, 26);
            this.列表ToolStripMenuItem.Text = "列表";
            // 
            // 详细信息ToolStripMenuItem
            // 
            this.详细信息ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("详细信息ToolStripMenuItem.Image")));
            this.详细信息ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.详细信息ToolStripMenuItem.Name = "详细信息ToolStripMenuItem";
            this.详细信息ToolStripMenuItem.Size = new System.Drawing.Size(121, 26);
            this.详细信息ToolStripMenuItem.Text = "详细信息";
            // 
            // CodeMakerM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 641);
            this.Controls.Add(this.tabControl1);
            this.Name = "CodeMakerM";
            this.Text = "CodeMaker";
            this.Load += new System.EventHandler(this.CodeMaker_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox_Web.ResumeLayout(false);
            this.groupBox_Web.PerformLayout();
            this.groupBox_AppType.ResumeLayout(false);
            this.groupBox_AppType.PerformLayout();
            this.groupBox_Method.ResumeLayout(false);
            this.groupBox_Method.PerformLayout();
            this.groupBox_F3.ResumeLayout(false);
            this.groupBox_F3.PerformLayout();
            this.groupBox_FrameSel.ResumeLayout(false);
            this.groupBox_FrameSel.PerformLayout();
            this.groupBox_DB.ResumeLayout(false);
            this.groupBox_DB.PerformLayout();
            this.groupBox_Type.ResumeLayout(false);
            this.groupBox_Type.PerformLayout();
            this.groupBox_Parameter.ResumeLayout(false);
            this.groupBox_Parameter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;       
        private System.Windows.Forms.ImageList imglistDB;
        private System.Windows.Forms.ImageList imglistView;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem 列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 详细信息ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox_Parameter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtNameSpace2;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox_Type;
        private System.Windows.Forms.RadioButton radbtn_Type_Web;
        private System.Windows.Forms.RadioButton radbtn_Type_CS;
        private System.Windows.Forms.RadioButton radbtn_Type_DB;
        private System.Windows.Forms.GroupBox groupBox_DB;
        private System.Windows.Forms.RadioButton radbtn_DB_DDL;
        private System.Windows.Forms.RadioButton radbtn_DB_Proc;
        private System.Windows.Forms.TextBox txtProcPrefix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chk_DB_GetList;
        private System.Windows.Forms.CheckBox chk_DB_GetModel;
        private System.Windows.Forms.CheckBox chk_DB_Delete;
        private System.Windows.Forms.CheckBox chk_DB_Update;
        private System.Windows.Forms.CheckBox chk_DB_Add;
        private System.Windows.Forms.CheckBox chk_DB_Exists;
        private System.Windows.Forms.CheckBox chk_DB_GetMaxID;
        private System.Windows.Forms.TextBox txtTabname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox_FrameSel;
        private System.Windows.Forms.GroupBox groupBox_DALType;
        private System.Windows.Forms.GroupBox groupBox_F3;
        private System.Windows.Forms.RadioButton radbtn_Frame_F3;
        private System.Windows.Forms.RadioButton radbtn_Frame_S3;
        private System.Windows.Forms.RadioButton radbtn_Frame_One;
        private System.Windows.Forms.RadioButton radbtn_F3_BLL;
        private System.Windows.Forms.RadioButton radbtn_F3_DALFactory;
        private System.Windows.Forms.RadioButton radbtn_F3_IDAL;
        private System.Windows.Forms.RadioButton radbtn_F3_DAL;
        private System.Windows.Forms.RadioButton radbtn_F3_Model;
        private System.Windows.Forms.GroupBox groupBox_Method;
        private System.Windows.Forms.CheckBox chk_CS_GetList;
        private System.Windows.Forms.CheckBox chk_CS_GetModel;
        private System.Windows.Forms.CheckBox chk_CS_Delete;
        private System.Windows.Forms.CheckBox chk_CS_Update;
        private System.Windows.Forms.CheckBox chk_CS_Add;
        private System.Windows.Forms.GroupBox groupBox_AppType;
        private System.Windows.Forms.RadioButton radbtn_AppType_Winform;
        private System.Windows.Forms.RadioButton radbtn_AppType_Web;
        private System.Windows.Forms.GroupBox groupBox_Web;
        private System.Windows.Forms.RadioButton radbtn_Web_AspxCS;
        private System.Windows.Forms.RadioButton radbtn_Web_Aspx;
        private System.Windows.Forms.Button btn_Ok;        
        private System.Windows.Forms.CheckBox chk_Web_Show;
        private System.Windows.Forms.CheckBox chk_Web_Update;
        private System.Windows.Forms.CheckBox chk_Web_Add;
        private System.Windows.Forms.CheckBox chk_Web_HasKey;
        
        private System.Windows.Forms.ImageList imgListTabpage;
        private System.Windows.Forms.CheckBox chk_CS_GetModelByCache;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chk_CS_Exists;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_SelI2;
        private System.Windows.Forms.Button btn_SelI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_SelAll2;
        private System.Windows.Forms.Button btn_SelAll;
        private System.Windows.Forms.ComboBox cmbox_SField;
        private System.Windows.Forms.ComboBox cmbox_STab;
        private System.Windows.Forms.ComboBox cmbox_PField;
        private System.Windows.Forms.ComboBox cmbox_PTab;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.TextBox txtClassName2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        //private LTP.TextEditor.TextEditorControl txtContent_SQL;
        //private LTP.TextEditor.TextEditorControl txtContent_Web;
        //private LTP.TextEditor.TextEditorControl txtContent_CS;


    }
}