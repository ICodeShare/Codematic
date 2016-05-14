namespace Codematic
{
    partial class DataList
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_Tip = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_dbname = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_Count = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel_rowcol = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(458, 338);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_Tip,
            this.StatusLabel_dbname,
            this.StatusLabel_time,
            this.StatusLabel_Count,
            this.StatusLabel_rowcol});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(458, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            // 
            // StatusLabel_Tip
            // 
            this.StatusLabel_Tip.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.StatusLabel_Tip.Name = "StatusLabel_Tip";
            this.StatusLabel_Tip.Size = new System.Drawing.Size(250, 17);
            this.StatusLabel_Tip.Spring = true;
            this.StatusLabel_Tip.Text = "就绪";
            this.StatusLabel_Tip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel_dbname
            // 
            this.StatusLabel_dbname.Name = "StatusLabel_dbname";
            this.StatusLabel_dbname.Size = new System.Drawing.Size(53, 17);
            this.StatusLabel_dbname.Text = "数据库：";
            this.StatusLabel_dbname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel_time
            // 
            this.StatusLabel_time.Name = "StatusLabel_time";
            this.StatusLabel_time.Size = new System.Drawing.Size(47, 17);
            this.StatusLabel_time.Text = "0:00:00";
            this.StatusLabel_time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel_Count
            // 
            this.StatusLabel_Count.Name = "StatusLabel_Count";
            this.StatusLabel_Count.Size = new System.Drawing.Size(23, 17);
            this.StatusLabel_Count.Text = "0行";
            this.StatusLabel_Count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel_rowcol
            // 
            this.StatusLabel_rowcol.AutoSize = false;
            this.StatusLabel_rowcol.Name = "StatusLabel_rowcol";
            this.StatusLabel_rowcol.Size = new System.Drawing.Size(70, 17);
            this.StatusLabel_rowcol.Text = "行0，列0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 360);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "DataList";
            this.Text = "DataList";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_Tip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_dbname;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_time;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_Count;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_rowcol;

    }
}