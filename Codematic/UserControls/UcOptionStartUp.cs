
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Codematic.UserControls
{
	/// <summary>
	/// .
	/// </summary>
	public class UcOptionStartUp : System.Windows.Forms.UserControl
    {
        public System.Windows.Forms.CheckBox chb_isTimespan;
        private System.Windows.Forms.Label label4;
        public ComboBox cmb_StartUpItem;
        private Label label1;
        public TextBox txtStartUpPage;
        public NumericUpDown num_Time;
        private Label label2;
        Maticsoft.CmConfig.AppSettings settings;

        #region
        /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public UcOptionStartUp(Maticsoft.CmConfig.AppSettings setting)
		{			
			InitializeComponent();
            settings = setting;
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
        #endregion

        #region Component Designer generated code
        /// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.chb_isTimespan = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_StartUpItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartUpPage = new System.Windows.Forms.TextBox();
            this.num_Time = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_Time)).BeginInit();
            this.SuspendLayout();
            // 
            // chb_isTimespan
            // 
            this.chb_isTimespan.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chb_isTimespan.Location = new System.Drawing.Point(10, 94);
            this.chb_isTimespan.Name = "chb_isTimespan";
            this.chb_isTimespan.Size = new System.Drawing.Size(264, 24);
            this.chb_isTimespan.TabIndex = 10;
            this.chb_isTimespan.Text = "下载内容的时间间隔(&D)：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "启动时(&P):";
            // 
            // cmb_StartUpItem
            // 
            this.cmb_StartUpItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_StartUpItem.FormattingEnabled = true;
            this.cmb_StartUpItem.Items.AddRange(new object[] {
            "显示起始页",
            "显示空环境",
            "打开主页"});
            this.cmb_StartUpItem.Location = new System.Drawing.Point(10, 23);
            this.cmb_StartUpItem.Name = "cmb_StartUpItem";
            this.cmb_StartUpItem.Size = new System.Drawing.Size(343, 20);
            this.cmb_StartUpItem.TabIndex = 13;
            this.cmb_StartUpItem.SelectedIndexChanged += new System.EventHandler(this.cmb_StartUpItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "启动页新闻频道(RSS)(&S):";
            // 
            // txtStartUpPage
            // 
            this.txtStartUpPage.Location = new System.Drawing.Point(10, 67);
            this.txtStartUpPage.Name = "txtStartUpPage";
            this.txtStartUpPage.Size = new System.Drawing.Size(343, 21);
            this.txtStartUpPage.TabIndex = 14;
            this.txtStartUpPage.Text = "http://ltp.cnblogs.com/Rss.aspx";
            // 
            // num_Time
            // 
            this.num_Time.Location = new System.Drawing.Point(25, 117);
            this.num_Time.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.num_Time.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_Time.Name = "num_Time";
            this.num_Time.Size = new System.Drawing.Size(40, 21);
            this.num_Time.TabIndex = 15;
            this.num_Time.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "分钟(&M)";
            // 
            // UcOptionStartUp
            // 
            this.Controls.Add(this.num_Time);
            this.Controls.Add(this.txtStartUpPage);
            this.Controls.Add(this.cmb_StartUpItem);
            this.Controls.Add(this.chb_isTimespan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Name = "UcOptionStartUp";
            this.Size = new System.Drawing.Size(372, 181);
            this.Load += new System.EventHandler(this.UcOptionStartUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_Time)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        
		private void UcOptionStartUp_Load(object sender, System.EventArgs e)
		{            
            switch (settings.AppStart)
            {
                case "startuppage"://显示起始页
                    cmb_StartUpItem.SelectedIndex = 0;
                    txtStartUpPage.Enabled = true;
                    break;
                case "blank"://显示空环境
                    cmb_StartUpItem.SelectedIndex = 1;
                    txtStartUpPage.Enabled = false;
                    break;
                case "homepage": //打开主页
                    cmb_StartUpItem.SelectedIndex = 2;
                    txtStartUpPage.Enabled = true;
                    break;
            }
            txtStartUpPage.Text = settings.StartUpPage;
                      
		}

        private void cmb_StartUpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_StartUpItem.SelectedIndex)
            {
                case 0:
                    label1.Text = "启动页新闻频道(RSS)(&S):";
                    txtStartUpPage.Text = settings.StartUpPage;
                    txtStartUpPage.Enabled = true;
                    break;
                case 1:
                    txtStartUpPage.Enabled = false;
                    break;
                case 2:
                    label1.Text = "主页地址(&H):";
                    txtStartUpPage.Text = settings.HomePage;
                    txtStartUpPage.Enabled = true;
                    break;
            }
        }
	}
}
