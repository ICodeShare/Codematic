using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
namespace LTP.SplashScrForm
{
    /// <summary>
    /// Summary description for SplashScreen.
    /// </summary>
    public class SplashScreen : System.Windows.Forms.Form
    {
        #region
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlStatus;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPcAdminName;


        // Threading
        static SplashScreen ms_frmSplash = null;//引导窗体
        static Thread ms_oThread = null;

        // Fade in and out.淡入淡出效果
        private double m_dblOpacityIncrement = .08;
        private double m_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 20;

        // Status and progress bar进度条
        static string ms_sStatus;
        private double m_dblCompletionFraction = 0;
        private Rectangle m_rProgress;

        // Progress smoothing
        private double m_dblLastCompletionFraction = 0.0;
        private double m_dblPBIncrementPerTimerInterval = .015;

        // Self-calibration support
        private bool m_bFirstLaunch = false;
        private DateTime m_dtStart;//开始时间
        private bool m_bDTSet = false;//
        private int m_iIndex = 1;
        private int m_iActualTicks = 0;
        private ArrayList m_alPreviousCompletionFraction;
        private ArrayList m_alActualTimes = new ArrayList();
        private const string REG_KEY_INITIALIZATION = "Initialization";
        private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
        private const string REGVALUE_PB_PERCENTS = "Percents";

        #endregion

        delegate void SetPcAdminNameCallback(string text);
        delegate void SetTimeRemainingCallback(string text);


        public SplashScreen()
        {
            InitializeComponent();
            lblPcAdminName.Text = SystemInformation.UserName;   
            //this.Opacity = .00;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            this.ClientSize = this.BackgroundImage.Size;
        }


        #region Windows Form Designer generated code

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPcAdminName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(11, 202);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(228, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Honeydew;
            this.pnlStatus.Location = new System.Drawing.Point(121, 231);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(266, 15);
            this.pnlStatus.TabIndex = 1;
            this.pnlStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.pnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatus_Paint);
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Location = new System.Drawing.Point(393, 232);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(53, 12);
            this.lblTimeRemaining.TabIndex = 2;
            this.lblTimeRemaining.Text = "时间剩余";
            this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(135, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "本产品使用权属于：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(135, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "该程序受版权法保护，";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(135, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "请参见“帮助”中的“关于”。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(135, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Maticsoft(C)2004-2010 李天平 保留所有权利。";
            // 
            // lblPcAdminName
            // 
            this.lblPcAdminName.AutoSize = true;
            this.lblPcAdminName.BackColor = System.Drawing.Color.Transparent;
            this.lblPcAdminName.Location = new System.Drawing.Point(254, 94);
            this.lblPcAdminName.Name = "lblPcAdminName";
            this.lblPcAdminName.Size = new System.Drawing.Size(23, 12);
            this.lblPcAdminName.TabIndex = 0;
            this.lblPcAdminName.Text = "LTP";
            // 
            // SplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(501, 260);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPcAdminName);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        //信息提示
        public void SetPcAdminName(string text)
        {
            if (this.lblPcAdminName.InvokeRequired)
            {
                SetPcAdminNameCallback d = new SetPcAdminNameCallback(SetPcAdminName);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblPcAdminName.Text = text;
            }
        }
        public void SetTimeRemaining(string text)
        {
            if (this.lblTimeRemaining.InvokeRequired)
            {
                SetTimeRemainingCallback d = new SetTimeRemainingCallback(SetTimeRemaining);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblTimeRemaining.Text = text;
            }
        }

        #region Static Methods

        //创建线程，开始显示窗体
        static public void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;

            ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            ms_oThread.IsBackground = true;//后台线程
            ms_oThread.ApartmentState = ApartmentState.STA;
            ms_oThread.Start();
        }

        //返回引导窗体实例
        static public SplashScreen SplashForm
        {
            get
            {
                return ms_frmSplash;
            }
        }


        // A private entry point for the thread.
        static private void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }

        //关闭窗体
        static public void CloseForm()
        {
            if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            }
            ms_oThread = null;	// we don't need these any more.
            ms_frmSplash = null;
        }


        //设置状态，更新显示.
        static public void SetStatus(string newStatus)
        {
            SetStatus(newStatus, true);
        }
        public static void SetUserName(string UserName)
        {
            if (ms_frmSplash != null)
            {
                ms_frmSplash.lblPcAdminName.Text = UserName;
                //SetStatusTip(ms_frmSplash.lblPcAdminName, UserName);
            }
        }

        // A static method to set the status and optionally update the reference.
        // This is useful if you are in a section of code that has a variable
        // set of status string updates.  In that case, don't set the reference.
        static public void SetStatus(string newStatus, bool setReference)
        {
            ms_sStatus = newStatus;
            if (ms_frmSplash == null)
                return;
            if (setReference)
            {
                ms_frmSplash.SetReferenceInternal();
            }
        }

        // Static method called from the initializing application to 
        // give the splash screen reference points.  Not needed if
        // you are using a lot of status strings.
        static public void SetReferencePoint()
        {
            if (ms_frmSplash == null)
                return;
            ms_frmSplash.SetReferenceInternal();

        }
        #endregion

        #region Private methods

        private void SetlblPcAdminName(string str)
        {
            SetPcAdminName(str);
            //this.lblPcAdminName.Text=str;
        }

        //设置控制点.
        private void SetReferenceInternal()
        {
            if (m_bDTSet == false)
            {
                m_bDTSet = true;
                m_dtStart = DateTime.Now;
                ReadIncrements();
            }
            double dblMilliseconds = ElapsedMilliSeconds();//从开始到现在已经运行的时间
            m_alActualTimes.Add(dblMilliseconds);
            m_dblLastCompletionFraction = m_dblCompletionFraction;
            if (m_alPreviousCompletionFraction != null && m_iIndex < m_alPreviousCompletionFraction.Count)
            {
                m_dblCompletionFraction = (double)m_alPreviousCompletionFraction[m_iIndex++];
            }
            else
            {
                m_dblCompletionFraction = (m_iIndex > 0) ? 1 : 0;
            }
        }

        //从开始到现在已经运行的时间.
        private double ElapsedMilliSeconds()
        {
            TimeSpan ts = DateTime.Now - m_dtStart;
            return ts.TotalMilliseconds;
        }


        //从注册表读取，先前设置的值.
        private void ReadIncrements()
        {
            string sPBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
            double dblResult;

            if (Double.TryParse(sPBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblResult) == true)
                m_dblPBIncrementPerTimerInterval = dblResult;
            else
                m_dblPBIncrementPerTimerInterval = .0015;

            string sPBPreviousPctComplete = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_PERCENTS, "");

            if (sPBPreviousPctComplete != "")
            {
                string[] aTimes = sPBPreviousPctComplete.Split(null);
                m_alPreviousCompletionFraction = new ArrayList();

                for (int i = 0; i < aTimes.Length; i++)
                {
                    double dblVal;
                    if (Double.TryParse(aTimes[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblVal))
                        m_alPreviousCompletionFraction.Add(dblVal);
                    else
                        m_alPreviousCompletionFraction.Add(1.0);
                }
            }
            else
            {
                m_bFirstLaunch = true;
                //lblTimeRemaining.Text = "";
                SetTimeRemaining("");
            }
        }


        // Method to store the intervals (in percent complete) from the current invocation of the splash screen to the registry.
        private void StoreIncrements()
        {
            string sPercent = "";
            double dblElapsedMilliseconds = ElapsedMilliSeconds();
            for (int i = 0; i < m_alActualTimes.Count; i++)
            {
                sPercent += ((double)m_alActualTimes[i] / dblElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";
            }

            RegistryAccess.SetStringRegistryValue(REGVALUE_PB_PERCENTS, sPercent);

            m_dblPBIncrementPerTimerInterval = 1.0 / (double)m_iActualTicks;
            RegistryAccess.SetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, m_dblPBIncrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo));
        }
        #endregion

        #region Event Handlers

        // 窗体淡入淡出，和进度条控制
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            try
            {

                lblStatus.Text = ms_sStatus;
                //窗体淡入淡出
                if (m_dblOpacityIncrement > 0)
                {
                    m_iActualTicks++;
                    if (this.Opacity < 1)
                    {
                        this.Opacity += m_dblOpacityIncrement;
                    }
                }
                else
                {
                    if (this.Opacity > 0)
                    {
                        this.Opacity += m_dblOpacityIncrement;
                    }
                    else
                    {
                        StoreIncrements();//存储时间
                        this.Close();
                        Debug.WriteLine("Called this.Close()");
                    }
                }

                //进度条控制
                if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction)
                {
                    m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
                    int width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
                    int height = pnlStatus.ClientRectangle.Height;
                    int x = pnlStatus.ClientRectangle.X;
                    int y = pnlStatus.ClientRectangle.Y;
                    if (width > 0 && height > 0)
                    {
                        m_rProgress = new Rectangle(x, y, width, height);
                        pnlStatus.Invalidate(m_rProgress);
                        int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - m_dblLastCompletionFraction) / m_dblPBIncrementPerTimerInterval)) / 1000;
                        if (iSecondsLeft == 1)
                        {
                            //lblTimeRemaining.Text = string.Format( "时间剩余 1 秒");
                            SetTimeRemaining(string.Format("时间剩余 1 秒"));
                        }
                        else
                        {
                            //lblTimeRemaining.Text = string.Format( "时间剩余 {0} 秒 ", iSecondsLeft);
                            SetTimeRemaining(string.Format("时间剩余 {0} 秒 ", iSecondsLeft));
                        }

                    }
                }
            }
            catch
            {
            }
        }

        // 刷新进度条颜色.
        private void pnlStatus_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (m_bFirstLaunch == false && e.ClipRectangle.Width > 0 && m_iActualTicks > 1)
                {
                    LinearGradientBrush brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
                    e.Graphics.FillRectangle(brBackground, m_rProgress);
                }
            }
            catch
            { }
        }


        // 双击关闭窗体
        private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                CloseForm();
            }
            catch
            { }
        }
        #endregion
    }



    /// <summary>
    /// 注册表类.
    /// </summary>
    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "Software";
        private const string COMPANY_NAME = "Maticsoft";
        private const string APPLICATION_NAME = "Codematic";

        //得到返回值.
        static public string GetStringRegistryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
                if (rkApplication != null)
                {
                    foreach (string sKey in rkApplication.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            return (string)rkApplication.GetValue(sKey);
                        }
                    }
                }
            }
            return defaultValue;
        }


        // 设置存储值.
        static public void SetStringRegistryValue(string key, string stringValue)
        {
            RegistryKey rkSoftware;
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
            rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
                if (rkApplication != null)
                {
                    rkApplication.SetValue(key, stringValue);
                }
            }
        }
    }
}
