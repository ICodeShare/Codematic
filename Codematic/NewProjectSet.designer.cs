namespace Codematic
{
    partial class NewProjectSet
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkGetList = new System.Windows.Forms.CheckBox();
            this.chkGetModel = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkUpdate = new System.Windows.Forms.CheckBox();
            this.chkAdd = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.chkExists = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radbtn_DAL_SQL = new System.Windows.Forms.RadioButton();
            this.radbtn_DAL_Param = new System.Windows.Forms.RadioButton();
            this.radbtn_DAL_Proc = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_Build = new System.Windows.Forms.Button();
            this.btn_Pri = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNamespace);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFolder);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "命名空间";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(87, 26);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(100, 21);
            this.txtNamespace.TabIndex = 4;
            this.txtNamespace.Text = "CodematicDemo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "命名空间：";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(341, 26);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(100, 21);
            this.txtFolder.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "子文件夹名：(可选)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkGetList);
            this.groupBox2.Controls.Add(this.chkGetModel);
            this.groupBox2.Controls.Add(this.chkDelete);
            this.groupBox2.Controls.Add(this.chkUpdate);
            this.groupBox2.Controls.Add(this.chkAdd);
            this.groupBox2.Controls.Add(this.checkBox8);
            this.groupBox2.Controls.Add(this.checkBox7);
            this.groupBox2.Controls.Add(this.chkExists);
            this.groupBox2.Location = new System.Drawing.Point(8, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 162);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "类方法";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(106, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "数据查询";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(106, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "得到实体对象模型";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "删除数据";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "更新数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "添加数据";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "检查存在性";
            // 
            // chkGetList
            // 
            this.chkGetList.AutoSize = true;
            this.chkGetList.Checked = true;
            this.chkGetList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGetList.Location = new System.Drawing.Point(25, 130);
            this.chkGetList.Name = "chkGetList";
            this.chkGetList.Size = new System.Drawing.Size(66, 16);
            this.chkGetList.TabIndex = 0;
            this.chkGetList.Text = "GetList";
            this.chkGetList.UseVisualStyleBackColor = true;
            // 
            // chkGetModel
            // 
            this.chkGetModel.AutoSize = true;
            this.chkGetModel.Checked = true;
            this.chkGetModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGetModel.Location = new System.Drawing.Point(25, 108);
            this.chkGetModel.Name = "chkGetModel";
            this.chkGetModel.Size = new System.Drawing.Size(72, 16);
            this.chkGetModel.TabIndex = 0;
            this.chkGetModel.Text = "GetModel";
            this.chkGetModel.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Checked = true;
            this.chkDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDelete.Location = new System.Drawing.Point(25, 86);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(60, 16);
            this.chkDelete.TabIndex = 0;
            this.chkDelete.Text = "Delete";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // chkUpdate
            // 
            this.chkUpdate.AutoSize = true;
            this.chkUpdate.Checked = true;
            this.chkUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdate.Location = new System.Drawing.Point(25, 64);
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Size = new System.Drawing.Size(60, 16);
            this.chkUpdate.TabIndex = 0;
            this.chkUpdate.Text = "Update";
            this.chkUpdate.UseVisualStyleBackColor = true;
            // 
            // chkAdd
            // 
            this.chkAdd.AutoSize = true;
            this.chkAdd.Checked = true;
            this.chkAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdd.Location = new System.Drawing.Point(25, 42);
            this.chkAdd.Name = "chkAdd";
            this.chkAdd.Size = new System.Drawing.Size(42, 16);
            this.chkAdd.TabIndex = 0;
            this.chkAdd.Text = "Add";
            this.chkAdd.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(278, 42);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(180, 16);
            this.checkBox8.TabIndex = 0;
            this.checkBox8.Text = "GetMaxID()手工得到最大编号";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(177, 42);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(96, 16);
            this.checkBox7.TabIndex = 0;
            this.checkBox7.Text = "是否有返回值";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // chkExists
            // 
            this.chkExists.AutoSize = true;
            this.chkExists.Checked = true;
            this.chkExists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExists.Location = new System.Drawing.Point(25, 20);
            this.chkExists.Name = "chkExists";
            this.chkExists.Size = new System.Drawing.Size(60, 16);
            this.chkExists.TabIndex = 0;
            this.chkExists.Text = "Exists";
            this.chkExists.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox9);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.radbtn_DAL_SQL);
            this.groupBox3.Controls.Add(this.radbtn_DAL_Param);
            this.groupBox3.Controls.Add(this.radbtn_DAL_Proc);
            this.groupBox3.Location = new System.Drawing.Point(8, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 78);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据访问层类型";
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Enabled = false;
            this.checkBox9.Location = new System.Drawing.Point(259, 50);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(108, 16);
            this.checkBox9.TabIndex = 6;
            this.checkBox9.Text = "使用企业库方式";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(139, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "DbHelperSQL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "数据访问基础类名：";
            // 
            // radbtn_DAL_SQL
            // 
            this.radbtn_DAL_SQL.Location = new System.Drawing.Point(29, 18);
            this.radbtn_DAL_SQL.Name = "radbtn_DAL_SQL";
            this.radbtn_DAL_SQL.Size = new System.Drawing.Size(96, 24);
            this.radbtn_DAL_SQL.TabIndex = 3;
            this.radbtn_DAL_SQL.Text = "基于SQL语句";
            // 
            // radbtn_DAL_Param
            // 
            this.radbtn_DAL_Param.Checked = true;
            this.radbtn_DAL_Param.Location = new System.Drawing.Point(130, 18);
            this.radbtn_DAL_Param.Name = "radbtn_DAL_Param";
            this.radbtn_DAL_Param.Size = new System.Drawing.Size(128, 24);
            this.radbtn_DAL_Param.TabIndex = 2;
            this.radbtn_DAL_Param.TabStop = true;
            this.radbtn_DAL_Param.Text = "基于Parameter参数";
            // 
            // radbtn_DAL_Proc
            // 
            this.radbtn_DAL_Proc.Location = new System.Drawing.Point(263, 18);
            this.radbtn_DAL_Proc.Name = "radbtn_DAL_Proc";
            this.radbtn_DAL_Proc.Size = new System.Drawing.Size(104, 24);
            this.radbtn_DAL_Proc.TabIndex = 1;
            this.radbtn_DAL_Proc.Text = "基于存储过程";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(13, 342);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(450, 4);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // btn_Build
            // 
            this.btn_Build.Location = new System.Drawing.Point(297, 356);
            this.btn_Build.Name = "btn_Build";
            this.btn_Build.Size = new System.Drawing.Size(75, 23);
            this.btn_Build.TabIndex = 11;
            this.btn_Build.Text = "开始生成";
            this.btn_Build.UseVisualStyleBackColor = true;
            this.btn_Build.Click += new System.EventHandler(this.btn_Build_Click);
            // 
            // btn_Pri
            // 
            this.btn_Pri.Location = new System.Drawing.Point(216, 356);
            this.btn_Pri.Name = "btn_Pri";
            this.btn_Pri.Size = new System.Drawing.Size(75, 23);
            this.btn_Pri.TabIndex = 11;
            this.btn_Pri.Text = "上一步";
            this.btn_Pri.UseVisualStyleBackColor = true;
            this.btn_Pri.Click += new System.EventHandler(this.btn_Pri_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(378, 356);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 11;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // NewProjectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 389);
            this.Controls.Add(this.btn_Pri);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Build);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectSet";
            this.ShowIcon = false;
            this.Text = "代码生成设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radbtn_DAL_SQL;
        private System.Windows.Forms.RadioButton radbtn_DAL_Param;
        private System.Windows.Forms.RadioButton radbtn_DAL_Proc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkGetList;
        private System.Windows.Forms.CheckBox chkGetModel;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckBox chkUpdate;
        private System.Windows.Forms.CheckBox chkAdd;
        private System.Windows.Forms.CheckBox chkExists;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_Build;
        private System.Windows.Forms.Button btn_Pri;
        private System.Windows.Forms.Button btn_Cancel;
    }
}