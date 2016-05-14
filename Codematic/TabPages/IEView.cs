using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Codematic
{
    public partial class IEView : Form
    {
        MainForm mainfrm=null;        
        public IEView(Form mdiParentForm, string url)
        {
            InitializeComponent();
            webBrowser1.CanGoBackChanged += new EventHandler(webBrowser1_CanGoBackChanged);
            webBrowser1.CanGoForwardChanged += new EventHandler(webBrowser1_CanGoForwardChanged);
            webBrowser1.StatusTextChanged +=  new EventHandler(webBrowser1_StatusTextChanged);
            
            mainfrm = (MainForm)mdiParentForm;
            this.txtUrl.Text = url;           
            mainfrm.StatusLabel1.Text = url;
            this.webBrowser1.Navigate(url);            
        }

        private void txtUrl_Click(object sender, EventArgs e)
        {
            txtUrl.SelectAll();
        }
        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(txtUrl.Text);
            }
        } 
        private void btn_Go_Click(object sender, EventArgs e)
        {            
            Navigate(this.txtUrl.Text);            
        }

        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }        


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            mainfrm.StatusLabel1.Text = "Íê³É";  
            Crownwood.Magic.Controls.TabPage page = (Crownwood.Magic.Controls.TabPage)this.Parent.Parent;
            string title = this.webBrowser1.DocumentTitle;
            page.Title = title;                      
        }

        protected string FormatString(string str)
        {
            if (str.Length > 30)
            {
                str = str.Substring(0, 29) + "...";
            }
            return str;
        }

        private void backFileToolStripButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
        private void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            backFileToolStripButton.Enabled = webBrowser1.CanGoBack;
        }
        private void forwardFileToolStripButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }
        private void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            forwardFileToolStripButton.Enabled = webBrowser1.CanGoForward;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txtUrl.Text = webBrowser1.Url.ToString();
        }
        private void webBrowser1_StatusTextChanged(object sender, EventArgs e)
        {
            mainfrm.StatusLabel1.Text= webBrowser1.StatusText;            
        }



        
    }
}