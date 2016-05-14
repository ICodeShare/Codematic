using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.IO;
using System.Threading;

namespace Codematic
{
    public partial class Form1 : Form
    {
        private Thread thread;
        private string tempOpmlfile = Application.StartupPath + "~tempopml.xml"; //临时RSS
        private string tempRssfile = Application.StartupPath + "~temprss.xml"; //临时RSS
        string temphtml = Application.StartupPath + "~temp.html";//临时文章

        private string RssPath = "http://blog.csdn.net/litp/rss.aspx";	//rss网址

        delegate void SetTextCallback(string text);
        delegate void SetListCallback();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CreatListview();
                this.thread = new Thread(new ThreadStart(this.LoadRss));
                this.thread.Start();                
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.ToString());
            } 

          
        }

        #region CreatListview()
        private void CreatListview()
        {
            //创建列表
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            //this.listView1.LargeImageList=tablarimglist;
            //this.listView1.SmallImageList=img_tabsmall;
            this.listView1.View = View.LargeIcon;


            this.listView1.View = View.Details;
            //this.listView1.GridLines = true;
            this.listView1.FullRowSelect = true;
            //this.listView1.CheckBoxes=true;

            listView1.Columns.Add("", 300, HorizontalAlignment.Left);
            //listView1.Columns.Add("日期", 200, HorizontalAlignment.Left);
            //listView1.Columns.Add("link", 100, HorizontalAlignment.Left);
            
           
        }
        #endregion

        #region LoadRss

        private void LoadRss()
        {
            //this.btn_go.Enabled = false;
           // this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
           // RssPath = "http://blog.csdn.net/litp/rss.aspx";
            LoadRss(RssPath);
           // this.Cursor = System.Windows.Forms.Cursors.Default;
            //this.btn_go.Enabled = true;
        }
        private void SetText(string text)
        {

            if (this.lblTip.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblTip.Text = text;
            }
        }
        private void SetListText()
        {

            if (this.listView1.InvokeRequired)
            {
                SetListCallback d = new SetListCallback(SetListText);
                this.Invoke(d, null);
            }
            else
            {
                LoadItem();
            }
        }

        private void LoadRss(string RssPath)
        {
            SetText("正在读取" + RssPath + "并校验…");          
            this.LoadXml2Coach(RssPath, tempRssfile);
           
            SetText("正在读取RSS内容信息…");            
            SetListText();

            SetText("完成");
        }

        #endregion


        #region LoadXml2Coach

        private void LoadXml2Coach(string url, string tempfile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(url);
                doc.Save(tempfile);
            }
            catch(System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show("连接失败，请重试！");
                return;
            }
        }
        #endregion

       
        #region LoadItem

        /// <summary>
        /// 引导条目
        /// </summary>
        private void LoadItem()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.tempRssfile);
            XmlNodeList nodeList;
            nodeList = doc.SelectNodes("/rss/channel/item");
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");

            this.listView1.Items.Clear();
            try
            {
                foreach (XmlNode objNode in nodeList)//循环每个item项
                {
                    if (objNode.HasChildNodes == true)
                    {
                        string title = "";
                        string link = "";
                        string creator = "";
                        string author = "";
                        string pubDate = DateTime.Now.ToString();
                       

                        XmlNodeList objItemsChild = objNode.ChildNodes;
                        foreach (XmlNode objNodeChild in objItemsChild)//每个item项下的节点值
                        {
                            switch (objNodeChild.Name)
                            {
                                case "title":
                                    title = objNodeChild.InnerText;
                                    break;
                                case "link":
                                    link = objNodeChild.InnerText;
                                    break;
                                case "dc:creator":
                                    creator = objNodeChild.InnerText;
                                    break;
                                case "author":
                                    author = objNodeChild.InnerText;
                                    break;
                                case "pubDate":
                                    pubDate = objNodeChild.InnerText;
                                    pubDate = DateTime.Parse(pubDate).ToString();
                                    break;

                            }
                        }

                        ListViewItem item1 = new ListViewItem(title, 0);
                        item1.SubItems.Add(pubDate);
                        item1.SubItems.Add(link);                        
                        listView1.Items.AddRange(new ListViewItem[] { item1 });
                    }

                }
                listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);



            }
            catch(Exception ex) 
            {
                LogInfo.WriteLog(ex);
                //MessageBox.Show(ex.ToString()); 
            }
        }

        #endregion

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                string selstr = this.listView1.SelectedItems[0].Text;
                string link = this.listView1.SelectedItems[0].SubItems[2].Text;
                IEView ieview = new IEView(this,link);
                ieview.Show();
            }
        }

 
    }
}