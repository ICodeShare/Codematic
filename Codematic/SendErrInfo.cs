using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Codematic
{
    public partial class SendErrInfo : Form
    {
        public SendErrInfo(string errorMessage)
        {
            InitializeComponent();
            txtMessage.Text = errorMessage;
            LogInfo.WriteLog(errorMessage);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Codematic.UpServer.UpServer upser = new Codematic.UpServer.UpServer();
            //upser.ErrInfo(Application.ProductName+Application.ProductVersion,"",txtMessage.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "http://www.maticsoft.com/softdown.aspx";
            System.Diagnostics.Process.Start(target);
        }

       

        
    }
}