using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;
using Maticsoft.Utility;
namespace Codematic
{
    public partial class DbQuery : Form
    {
        public Form MdiParentForm = null;
        TreeNode _dragObject;
        MainForm mainfrm;

        public DbQuery(Form mdiParentForm, string strSQL)
        {
            InitializeComponent();
            this.MdiParentForm = mdiParentForm;
            this.tabControl1.Visible = false;


            txtContent.ActiveTextAreaControl.TextArea.MouseUp += new MouseEventHandler(qcTextEditor_MouseUp);
            txtContent.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(TextArea_DragDrop);
            txtContent.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(TextArea_DragEnter);
            txtContent.ActiveTextAreaControl.TextArea.Click += new EventHandler(TextArea_Click);
            txtContent.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler(TextArea_KeyPress);
            txtContent.ActiveTextAreaControl.TextArea.KeyUp += new System.Windows.Forms.KeyEventHandler(TextArea_KeyUp);

            mainfrm = (MainForm)mdiParentForm;
            mainfrm.toolBtn_SQLExe.Visible = true;
            mainfrm.查询QToolStripMenuItem.Visible = true;

            this.txtContent.Text = strSQL;
        }

        //public DbQuery(string tempFile)
        //{
        //    InitializeComponent();
        //    StreamReader srFile = new StreamReader(tempFile, Encoding.Default);
        //    string Contents = srFile.ReadToEnd();
        //    srFile.Close();
        //    this.txtContent.Text = Contents;
        //}

        #region 私有成员

        private int keyPressCount = 0;
        //private string[] _lines;
        //private int _linesPrinted;
        private Hashtable Aliases = new Hashtable();
        private ArrayList AliasList = new ArrayList();
        private int lastPos;
        private int firstPos;
        private XmlDocument sqlReservedWords = new XmlDocument();
        private static ArrayList _sqlInfoMessages = new ArrayList();
        private ArrayList ReservedWords = new ArrayList();
        //private bool DoInsert;
        private string _OrginalName;
        private string m_fileName = string.Empty;
        private bool m_resetText = true;
        private bool _canceled = false;
        private Regex _findNextRegex;
        private int _findNextStartPos;
        private TimeSpan _currentExecutionTime;
        private IAsyncResult _asyncResult;
        private Exception _currentException = null;
        private int _dragPos;

        //private IDatabaseManager _currentManager = null;
        //private DataSet _currentDataSet = null;

        //private QCTreeNode _dragObject;

        #endregion

        #region  公有成员

        public bool IsActive;
        /// <summary>
        /// All queries will be executed with this connection
        /// </summary>
        //public IDbConnection dbConnection = null;
        /// <summary>
        /// Current database name
        /// </summary>
        public string DatabaseName = "";


        /// <summary>
        /// The Syntax reader handles all font and color settings.
        /// </summary>
        public LTPTextEditor.SyntaxReader syntaxReader;
        /// <summary>
        /// Used when opening/saving.
        /// </summary>
        public string FileName
        {
            get { return m_fileName; }
            set
            {
                if (value != string.Empty)
                {
                    string fileName = value; //.Substring(value.LastIndexOf(@"\")+1);
                    this.Text = fileName;
                }

                m_fileName = value;
            }
        }

        /// <summary>
        /// The content of the txtContent
        /// </summary>
        public string Content
        {
            get { return txtContent.Text; }
            set
            {
                txtContent.Text = value;

                txtContent.Refresh();
                //MainForm frm = (MainForm)MdiParentForm;
                //frm.SetPandelInfo();
            }
        }

        /// <summary>
        /// Font settings 
        /// </summary>
        public Font EditorFont
        {
            get { return txtContent.Font; }
            set { txtContent.Font = value; }
        }
        #endregion

        private void DbQuery_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        #region 键盘事件

        private void qcTextEditor_KeyPressEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string keyString = e.ToString();
            string line = txtContent.ActiveTextAreaControl.Caret.Line.ToString();
            string col = txtContent.ActiveTextAreaControl.Caret.Column.ToString();
            //((MainForm)MdiParentForm).SetPandelPositionInfo(line, col);

            if (e.Alt == true && e.KeyValue == 88)
            {
                e.Handled = false;
                RunQuery();
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                //lstv_Commands.Focus();
            }

            if (e.KeyCode == Keys.F5)
            {
                RunQuery();
            }

            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 32
                || e.KeyValue == 190)
            {
                if (e.KeyValue == 190)
                {
                    //ApplyProperty();
                }
                else
                {
                    e.Handled = true;
                    //ComplementWord();
                }
            }
            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 67 || e.KeyValue == 99)
            {
                //ToggleComment();
            }
        }
        //托拽
        private void TextArea_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("System.Windows.Forms.TreeNode", false) != null)
            {
                TreeNode node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", false);
                if ((node.Tag.ToString() == "table") || (node.Tag.ToString() == "view") || (node.Tag.ToString() == "column"))
                {
                    System.Drawing.Rectangle r = txtContent.RectangleToClient(txtContent.ClientRectangle);
                    Point p = new Point(e.X + r.X, e.Y + r.Y);

                    _dragPos = txtContent.GetCharIndexFromPosition(p);
                    _dragObject = node;

                    string objectName = node.Text;
                    //if (node.Tag.ToString() == "column")
                    //{
                    //    int spacePos = objectName.IndexOf("[");
                    //    if (spacePos > 0)
                    //        objectName = objectName.Substring(0, spacePos);

                    //    objectName = ((TreeNode)node.Parent.Parent).Text + "." + objectName;

                    //}

                    //_dragObject.Text = objectName;
                    SetDragAndDropContextMenu(node);
                    //foreach (MainForm.DBConnection c in ((MainForm)this.MdiParentForm).DBConnections)
                    //{
                    //    if (c.ConnectionName == node.server)
                    //    {
                    //        SetDatabaseConnection(node.database, c.Connection);
                    //        break;
                    //    }
                    //}

                    cmDragAndDrp.Show(txtContent, p);

                }
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in 
                // case the user has selected multiple files.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    string fullName = files[0];
                    if (files.Length > 1)
                        return;
                    MainForm mainform = (MainForm)MdiParentForm;
                    string fileName = Path.GetFileName(fullName);
                    //string line;
                    string fileContent = "";

                    //FrmQuery frmquery = new FrmQuery(MdiParentForm);

                    StreamReader sr = new StreamReader(fullName);
                    fileContent = sr.ReadToEnd();
                    //					while ((line = sr.ReadLine()) != null) 
                    //					{
                    //						fileContent += "\n" + line;
                    //					}
                    sr.Close();
                    sr = null;

                    this.Content = fileContent;
                }
                catch (Exception ex)
                {
                    LogInfo.WriteLog(ex);
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

        }
        private void TextArea_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetData("System.Windows.Forms.TreeNode", false) != null)
            {
                TreeNode node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", false);
                if ((node.Tag.ToString() == "table") || (node.Tag.ToString() == "view"))
                    ((DragEventArgs)e).Effect = DragDropEffects.Copy;
                else
                    if (node.Tag.ToString() == "column")
                        ((DragEventArgs)e).Effect = DragDropEffects.Copy;

                return;
            }

            if (((DragEventArgs)e).Data.GetDataPresent(DataFormats.FileDrop))
                ((DragEventArgs)e).Effect = DragDropEffects.Copy;
            else
                ((DragEventArgs)e).Effect = DragDropEffects.None;

        }

        private void TextArea_Click(object sender, EventArgs e)
        {
            string line = txtContent.ActiveTextAreaControl.Caret.Line.ToString();
            string col = txtContent.ActiveTextAreaControl.Caret.Column.ToString();
            //((MainForm)MdiParentForm).SetPandelPositionInfo(line, col);
        }
        private void ExecutionTimer_Tick(object sender, System.EventArgs e)
        {
            //if (_asyncResult.IsCompleted)
            //{
            //    ExecutionTimer.Enabled = false;
            //    HandleExecutionResult(_currentDataSet, _currentExecutionTime, _currentManager);
            //}
        }

        private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                // Counts the backspaces.
                case '\b':
                    break;
                // Counts the ENTER keys.
                case '\r':
                    break;
                // Counts the ESC keys.  
                case (char)27:
                    break;
                // Counts all other keys.
                default:
                    keyPressCount = keyPressCount + 1;
                    if (this.Text.IndexOf("*") <= 0)
                    {
                        this.Text = this.Text + "*";
                    }
                    break;
            }
        }

        private void TextArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.KeyCode == Keys.Enter)
            {
                if (this.Text.IndexOf("*") <= 0)
                {
                    this.Text = this.Text + "*";
                }
            }
        }

        #endregion

        #region 鼠标右键事件
        private void qcTextEditor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string word = txtContent.GetCurrentWord();
                IDataObject iData = Clipboard.GetDataObject();
                MenuItem miRunCurrentQuery = null;
                MenuItem miValidateCurrentQuery = null;
                MenuItem miMakeCurrentQueryCS = null;
                MenuItem miMakeCurrentQuerySQL = null;


                MenuItem miCopy = new MenuItem("复制 (&C)");
                MenuItem miCut = new MenuItem("剪切 (&T)");
                MenuItem miPaste = new MenuItem("粘贴 (&P)");
                MenuItem miAllSel = new MenuItem("全选 (&A)");
                MenuItem miSeparator = new MenuItem("-");
                MenuItem miGoToDefinision = new MenuItem("转到定义 (&G)");
                MenuItem miGoToRererence = new MenuItem("转到对象引用 (&R)");
                MenuItem miGoToAnyRererence = new MenuItem("转到所有对象引用");
                MenuItem miSeparator2 = new MenuItem("-");
                if (txtContent.SelectedText.Length > 0)
                {
                    miRunCurrentQuery = new MenuItem("运行当前选择");
                    miValidateCurrentQuery = new MenuItem("验证当前选择");
                    miMakeCurrentQueryCS = new MenuItem("生成当前选择SQL语句的拼接代码");
                    miMakeCurrentQuerySQL = new MenuItem("生成当前选择查询结果的数据脚本");
                }
                else
                {
                    miRunCurrentQuery = new MenuItem("运行当前查询");
                    miValidateCurrentQuery = new MenuItem("验证当前查询");
                    miMakeCurrentQueryCS = new MenuItem("生成当前查询SQL语句的拼接代码");
                    miMakeCurrentQuerySQL = new MenuItem("生成当前查询结果的数据脚本");
                }
                MenuItem miSeparator3 = new MenuItem("-");
                MenuItem miOptions = new MenuItem("选项 (&O)");
                MenuItem miSeparator4 = new MenuItem("-");
                MenuItem miSnippets = new MenuItem("脚本片断");
                MenuItem miAddToSnippets = new MenuItem("增加到脚本片断");

                // Events				
                miCopy.Click += new System.EventHandler(this.miCopy_Click);
                miCut.Click += new System.EventHandler(this.miCut_Click);
                miPaste.Click += new System.EventHandler(this.miPaste_Click);
                miAllSel.Click += new System.EventHandler(this.miAllSel_Click);
                miGoToDefinision.Click += new System.EventHandler(this.miGoToDefinision_Click);
                miGoToRererence.Click += new System.EventHandler(this.miGoToRererence_Click);
                miGoToAnyRererence.Click += new System.EventHandler(this.miGoToAnyRererence_Click);
                miRunCurrentQuery.Click += new System.EventHandler(this.miRunCurrentQuery_Click);
                miValidateCurrentQuery.Click += new EventHandler(miValidateCurrentQuery_Click);
                miMakeCurrentQueryCS.Click += new EventHandler(miMakeCurrentQueryCS_Click);
                miMakeCurrentQuerySQL.Click += new EventHandler(miMakeCurrentQuerySQL_Click);

                miOptions.Click += new System.EventHandler(this.miOptions_Click);
                miAddToSnippets.Click += new System.EventHandler(this.miAddToSnippet_Click);

                if (!iData.GetDataPresent(DataFormats.Text))
                    miPaste.Enabled = false;

                // Clear all previously added MenuItems.
                cmShortcutMeny.MenuItems.Clear();

                cmShortcutMeny.MenuItems.Add(miCopy);
                cmShortcutMeny.MenuItems.Add(miCut);
                cmShortcutMeny.MenuItems.Add(miPaste);
                cmShortcutMeny.MenuItems.Add(miAllSel);
                //cmShortcutMeny.MenuItems.Add(miSeparator);
                //cmShortcutMeny.MenuItems.Add(miGoToDefinision);
                //cmShortcutMeny.MenuItems.Add(miGoToRererence);
                //cmShortcutMeny.MenuItems.Add(miGoToAnyRererence);
                cmShortcutMeny.MenuItems.Add(miSeparator2);
                cmShortcutMeny.MenuItems.Add(miRunCurrentQuery);
                //cmShortcutMeny.MenuItems.Add(miValidateCurrentQuery);

                cmShortcutMeny.MenuItems.Add(miSeparator3);
                cmShortcutMeny.MenuItems.Add(miMakeCurrentQueryCS);
                cmShortcutMeny.MenuItems.Add(miMakeCurrentQuerySQL);
                //cmShortcutMeny.MenuItems.Add(miOptions);
                cmShortcutMeny.MenuItems.Add(miSeparator4);
                cmShortcutMeny.MenuItems.Add(miSnippets);


                // Snippets//插入脚本语句片断
                XmlDocument xmlSnippets = new XmlDocument();
                xmlSnippets.Load(Application.StartupPath + @"\Snippets.xml");
                XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

                if (txtContent.SelectedText.Length > 1)
                    miSnippets.MenuItems.Add(miAddToSnippets);

                foreach (XmlNode node in xmlNodeList[0].ChildNodes)
                {
                    SnippetMenuItem snippet = new SnippetMenuItem();
                    snippet.Text = node.Attributes["name"].Value;
                    snippet.statement = node.InnerText;
                    snippet.Click += new System.EventHandler(this.miSnippet_Click);

                    miSnippets.MenuItems.Add(snippet);
                }

                cmShortcutMeny.Show(txtContent, new Point(e.X, e.Y));

            }

        }
        #endregion

        #region 鼠标右键菜单事件
        private void miCopy_Click(object sender, System.EventArgs e)
        {
            this.Copy();
        }
        private void miCut_Click(object sender, System.EventArgs e)
        {
            this.txtContent.Cut();
        }
        private void miPaste_Click(object sender, System.EventArgs e)
        {
            this.Paste();
        }
        private void miAllSel_Click(object sender, System.EventArgs e)
        {
            this.txtContent.Select(0, this.txtContent.Text.Length);
        }
        private void miGoToDefinision_Click(object sender, System.EventArgs e)
        {
            this.GoToDefenition();
        }
        private void miGoToRererence_Click(object sender, System.EventArgs e)
        {
            this.GoToReferenceObject();
        }
        private void miGoToAnyRererence_Click(object sender, System.EventArgs e)
        {
            this.GoToReferenceAny();
        }
        private void miOptions_Click(object sender, System.EventArgs e)
        {
            //MainForm frm = (MainForm)MdiParentForm;

            //FrmOption frmOption = new FrmOption(frm);
            //frmOption.ShowDialog();

        }
        //增加到脚本片断
        private void miAddToSnippet_Click(object sender, System.EventArgs e)
        {
            FrmAddToSnippet frm = new FrmAddToSnippet(txtContent.Text);
            frm.ShowDialog(this);

        }
        private void miSnippet_Click(object sender, System.EventArgs e)
        {
            string statement = ((SnippetMenuItem)sender).statement;

            statement = statement.Replace(@"\n", "\n");
            statement = statement.Replace(@"\t", "\t");

            int cursorPos = txtContent.SelectionStart;

            txtContent.Document.Replace(cursorPos, 0, statement);

            if (statement.IndexOf("{}") > -1)
                cursorPos = cursorPos + statement.IndexOf("{}") + 1;

            txtContent.SetPosition(cursorPos);
            txtContent.Refresh();
        }

        private void miRunCurrentQuery_Click(object sender, System.EventArgs e)
        {
            RunCurrentQuery();
        }
        public void miValidateCurrentQuery_Click(object sender, EventArgs e)
        {
            txtContent.ResumeLayout();
            string contentHolder = this.Content;
            if (txtContent.SelectedText.Length > 0)
            {
                string validate = "SET NOEXEC ON;" + txtContent.SelectedText + ";SET NOEXEC OFF;";
                int len = validate.Length;
                int pos = this.Content.IndexOf(txtContent.SelectedText);
                if (this.Content.IndexOf("SET NOEXEC ON", 0) < 0 && pos >= 0 && len > 0)
                {
                    this.Content = this.Content.Replace(txtContent.SelectedText, validate);
                    txtContent.Select(pos, len);
                }
            }
            else
            {
                this.Content = "SET NOEXEC ON;" + this.Content + ";SET NOEXEC OFF;\n\n";
            }
            this.RunQuery();
            this.Content = contentHolder;
            txtContent.ResumeLayout();
        }
        //生成sql语句的拼接代码
        private void miMakeCurrentQueryCS_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.toolStripStatusLabel1.Text = "正在生成......";
                string SQLstring = this.txtContent.Text;
                if (txtContent.SelectedText.Length > 1)
                {
                    SQLstring = txtContent.SelectedText;
                }
                else
                {
                    SQLstring = txtContent.Text;

                }
                if (SQLstring.Trim() == "")
                {
                    this.toolStripStatusLabel1.Text = "查询语句为空！";
                    return;
                }
                StringPlus strCode = new StringPlus();
                strCode.AppendLine("StringBuilder strSql=new StringBuilder();");
                //GO的处理
                if (SQLstring.IndexOf("\r\n") > 0)//多行语句
                {
                    string[] split = SQLstring.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string sql in split)
                    {
                        if (sql.Trim() != "")
                        {
                            strCode.AppendLine("strSql.Append(\"" + sql + " \");");
                        }
                    }
                }
                else//单行语句
                {
                    strCode.AppendLine("strSql.Append(\"" + SQLstring + " \");");
                }
                mainfrm.AddTabPage("Class1.cs", new CodeEditor(strCode.Value, "cs",""));
                this.toolStripStatusLabel1.Text = "成功完成。";
            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                this.toolStripStatusLabel1.Text = "执行失败！";
            }
        }

        //根据sql查询数据结果生成数据脚本
        private void miMakeCurrentQuerySQL_Click(object sender, System.EventArgs e)
        {
            try
            {
                MainForm frm = (MainForm)MdiParentForm;

                DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
                string longServername = dbviewfrm.GetLongServername();
                if (longServername.Length < 1)
                {
                    this.toolStripStatusLabel1.Text = "没有选择任何服务器！";
                    return;
                }
                string dbname = frm.toolComboBox_DB.Text;
                if (dbname.Length < 1)
                {
                    this.toolStripStatusLabel1.Text = "没有选择可执行的数据库！";
                    return;
                }

                this.toolStripStatusLabel1.Text = "正在生成......";
                string SQLstring = this.txtContent.Text;
                if (txtContent.SelectedText.Length > 1)
                {
                    SQLstring = txtContent.SelectedText;
                }
                else
                {
                    SQLstring = txtContent.Text;

                }
                if (SQLstring.Trim() == "")
                {
                    this.toolStripStatusLabel1.Text = "查询语句为空！";
                    return;
                }
                
                Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(longServername);
                string strCode = dsb.CreateTabScriptBySQL(dbname, SQLstring.Trim());
                mainfrm.AddTabPage("SQL1.sql", new CodeEditor(strCode, "sql", ""));
                this.toolStripStatusLabel1.Text = "成功完成。";
            }
            catch(System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                this.toolStripStatusLabel1.Text = "执行失败！";
            }
        }



        #endregion

        #region Drag & Drop Context menu

        private void SetDragAndDropContextMenu(TreeNode node)
        {
            foreach (MenuItem mi in cmDragAndDrp.MenuItems)
                mi.Visible = false;

            menuItemObjectName.Visible = true;
            menuItemObjectName.Text = node.Text;
            menuItemSplitter.Visible = true;

            //QueryCommander.SQL.SQLStatement sqlStatement = new QueryCommander.SQL.SQLStatement(qcTextEditor.Text, qcTextEditor.SelectionStart, QueryCommander.SQL.SQLStatement.SearchOrder.asc, DBConnectionType.MicrosoftSqlClient);
            //Chris.Beckett.MenuImageLib.MenuImage menuExtender = new Chris.Beckett.MenuImageLib.MenuImage();
            //menuExtender.ImageList = imageList1;

            //if (node.objecttype == QCTreeNode.ObjectType.Table ||
            //    node.objecttype == QCTreeNode.ObjectType.View)
            if ((node.Tag.ToString() == "table") || (node.Tag.ToString() == "view"))
            {
                menuItemSelect1.Visible = true;
                menuItemSelect2.Visible = true;
                menuItemJoin.Visible = true;
                menuItemLeftOuterJoin.Visible = true;
                menuItemRightOuterJoin.Visible = true;
                //menuExtender.SetMenuImage(menuItemSelect1, "2");
                //menuExtender.SetMenuImage(menuItemSelect2, "2");
                //menuExtender.SetMenuImage(menuItemJoin, "2");
                //menuExtender.SetMenuImage(menuItemLeftOuterJoin, "2");
                //menuExtender.SetMenuImage(menuItemRightOuterJoin, "2");

                //menuExtender.SetMenuImage(menuItemObjectName, "4");
            }
            else if (node.Tag.ToString() == "column")
            {
                //if (sqlStatement.Statement.ToUpper().IndexOf("WHERE") >= 0)
                //    menuItemWhere.Text = "AND " + node.Text;
                //else
                //    menuItemWhere.Text = "WHERE " + node.Text;

                //menuItemWhere.Visible = true;
                //menuItemOrderBy.Visible = true;
                //menuItemGroupBy.Visible = true;
                //menuExtender.SetMenuImage(menuItemWhere, "2");
                //menuExtender.SetMenuImage(menuItemOrderBy, "2");
                //menuExtender.SetMenuImage(menuItemGroupBy, "2");

                //menuExtender.SetMenuImage(menuItemObjectName, "3");

            }

        }
        private void SetDragAndDropMenuIcons()
        {
            //Chris.Beckett.MenuImageLib.MenuImage menuExtender = new Chris.Beckett.MenuImageLib.MenuImage();
            //menuExtender.ImageList = imageList1;

            //menuExtender.SetMenuImage(menuItemObjectName, "6");
        }
        private void menuItemObjectName_Click(object sender, System.EventArgs e)
        {
            string text = _dragObject.Text;
            txtContent.ActiveTextAreaControl.TextArea.InsertString(text);
        }

        private void menuItemSelect1_Click(object sender, System.EventArgs e)
        {
            string text = "SELECT *\nFROM\t" + _dragObject.Text + "\n";
            txtContent.ActiveTextAreaControl.TextArea.InsertString(text);
        }

        private void menuItemSelect2_Click(object sender, System.EventArgs e)
        {
            string longservername = _dragObject.Parent.Parent.Parent.Text;
            string dbname = _dragObject.Parent.Parent.Text;
            string tabname = _dragObject.Text;
            Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(longservername);
            string strSQL = dsb.GetSQLSelect(dbname, tabname);
            txtContent.ActiveTextAreaControl.TextArea.InsertString(strSQL);

        }
        private void menuItemJoin_Click(object sender, System.EventArgs e)
        {
            string longservername = _dragObject.Parent.Parent.Parent.Text;
            string dbname = _dragObject.Parent.Parent.Text;
            string tabname = _dragObject.Text;
            Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(longservername);
            string strSQL = dsb.GetSQLUpdate(dbname, tabname);
            txtContent.ActiveTextAreaControl.TextArea.InsertString(strSQL);

        }

        private void menuItemLeftOuterJoin_Click(object sender, System.EventArgs e)
        {
            string longservername = _dragObject.Parent.Parent.Parent.Text;
            string dbname = _dragObject.Parent.Parent.Text;
            string tabname = _dragObject.Text;
            Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(longservername);
            string strSQL = dsb.GetSQLDelete(dbname, tabname);
            txtContent.ActiveTextAreaControl.TextArea.InsertString(strSQL);

        }

        private void menuItemRightOuterJoin_Click(object sender, System.EventArgs e)
        {
            string longservername = _dragObject.Parent.Parent.Parent.Text;
            string dbname = _dragObject.Parent.Parent.Text;
            string tabname = _dragObject.Text;
            Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(longservername);
            string strSQL = dsb.GetSQLInsert(dbname, tabname);
            txtContent.ActiveTextAreaControl.TextArea.InsertString(strSQL);

        }
        private void menuItemWhere_Click(object sender, System.EventArgs e)
        {
        }
        private void menuItemOrderBy_Click(object sender, System.EventArgs e)
        {
        }
        private void menuItemGroupBy_Click(object sender, System.EventArgs e)
        {
        }

        #endregion

        #region 公共方法
        public bool ClosingCanceled
        {
            get { return _canceled; }
        }

        public void RefreshLineRangeColor(int firstLine, int toLine)
        {
            //txtContent.SetLineRangeColor(firstLine,toLine);
        }

        public void SendToOutPutWindow()
        {
            //if (_outPutContainer == null)
            //    return;

            //MainForm frm = (MainForm)MdiParentForm;

            //if (_outPutContainer.dataset.Tables.Count > 0)
            //{
            //    if (_outPutContainer.dataset.Tables.Count > 1)
            //        frm.OutputWindow.BrowseTable(_outPutContainer.dataset, _outPutContainer.database.DataAdapter);
            //    else
            //        frm.OutputWindow.BrowseTable(_outPutContainer.dataset.Tables[0], _outPutContainer.database.DataAdapter);
            //}
            //else
            //    frm.OutputWindow.tabControl1.TabPages.Clear();

            //frm.OutputWindow.AddMessage(_outPutContainer.message);
            //frm.statusBar.Panels[3].Text = _outPutContainer.statusText;
            //frm.OutputWindow.Activate();
            //frm.TaskList.ApplyTask("Query executed successfully");
        }

        /// <summary>
        /// Openeds a new query window displaing the requested constructor (alter statement) 
        /// </summary>
        public void GoToDefenition()
        {
            //this.Cursor = Cursors.WaitCursor;
            //string objectName = txtContent.GetCurrentWord();
            //if (DatabaseFactory.GetConnectionType(dbConnection) != DBConnectionType.DB2)
            //    objectName = objectName.Substring(objectName.IndexOf(".") + 1);
            //if (objectName.Length == 0)
            //{
            //    MessageBox.Show("The referenced object was not found", "Go to reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Cursor = Cursors.Default;
            //    return;
            //}
            //MainForm frm = (MainForm)MdiParentForm;
            //frm.m_FrmDBObjects.CreateConstructorString(objectName);
            //this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Displays all database objects referencing selected object 
        /// Matching only objectname. Asuming, that objectname is enclosed by spaces
        /// </summary>
        public void GoToReferenceObject()
        {
            this.Cursor = Cursors.WaitCursor;
            string objectName = txtContent.GetCurrentWord();
            if (objectName.IndexOf(".") > -1)
                objectName = objectName.Substring(objectName.IndexOf(".") + 1);

            if (objectName.Length == 0)
            {
                MessageBox.Show("The referenced object was not found", "Go to reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            //IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);

            //ArrayList DatabaseObjects = db.GetDatabasesReferencedObjectsClear(DatabaseName, objectName, dbConnection);


            //this.Cursor = Cursors.Default;

            //MainForm frm = (MainForm)MdiParentForm;
            //frm.ResultWindow.ShowResults(DatabaseObjects, objectName);
            //frm.ResultWindow.Show(dockManager);
        }

        /// <summary>
        /// Displays all database objects referencing selected keyword
        /// Matching any occurence within a text (Example sp_test -> sp_test, sp_test1,...)
        /// </summary>
        public void GoToReferenceAny()
        {
            //this.Cursor = Cursors.WaitCursor;
            //string objectName = txtContent.GetCurrentWord();
            //if (objectName.Length == 0)
            //{
            //    MessageBox.Show("The referenced object was not found", "Go to reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Cursor = Cursors.Default;
            //    return;
            //}

            //IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
            ////Database db = new Database();
            //ArrayList DatabaseObjects = db.GetDatabasesReferencedObjects(DatabaseName, objectName, dbConnection);

            ////FrmReferenceObjects frmReferenceObjects = new FrmReferenceObjects(DatabaseObjects,objectName);
            //MainForm frm = (MainForm)MdiParentForm;
            //frm.ResultWindow.ShowResults(DatabaseObjects, objectName);
            //frm.ResultWindow.Show(dockManager);

            //this.Cursor = Cursors.Default;
            ///*if(frmReferenceObjects.ShowDialogWindow(this) ==DialogResult.OK)
            //{
            //    MainForm frm = (MainForm)MdiParentForm;
            //    frm.m_FrmDBObjects.CreateConstructorString(frmReferenceObjects.ReferencedObject);
            //}
            //this.Cursor=Cursors.Default;*/
        }

        /// <summary>
        /// Searches for specified pattern. Called from FrmSearch
        /// </summary>
        /// <param name="pathern"></param>
        /// <param name="startPos"></param>
        /// <param name="richTextBoxFinds"></param>
        /// <returns></returns>
        public int Find(Regex regex, int startPos)
        {
            string context = this.txtContent.Text.Substring(startPos);

            Match m = regex.Match(context);
            if (!m.Success)
            {
                MessageBox.Show("没有找到指定文本.", "Codematic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            int line = txtContent.Document.GetLineNumberForOffset(m.Index + startPos);
            txtContent.ActiveTextAreaControl.TextArea.ScrollTo(line);

            txtContent.Select(m.Index + startPos, m.Length);
            _findNextRegex = regex;
            _findNextStartPos = m.Index + startPos;

            txtContent.SetPosition(m.Index + m.Length + startPos);
            return m.Index + m.Length + startPos;
        }
        /// <summary>
        /// 
        /// </summary>
        public void FindNext()
        {
            if (_findNextRegex != null)
                Find(_findNextRegex, _findNextStartPos + 1);
        }
        /// <summary>
        /// Searches for specified pattern and replaces it. Called from FrmSearch
        /// </summary>
        /// <param name="pathern"></param>
        /// <param name="startPos"></param>
        /// <param name="richTextBoxFinds"></param>
        /// <returns></returns>
        public int Replace(Regex regex, int startPos, string replaceWith)
        {
            if (txtContent.SelectedText.Length > 0)
            {
                int start = txtContent.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
                //int start = txtContent.SelectionStart;
                int length = txtContent.SelectedText.Length;
                txtContent.Document.Replace(start, length, replaceWith);

                return Find(regex, length + start);

            }

            string context = this.txtContent.Text.Substring(startPos);

            Match m = regex.Match(context);

            if (!m.Success)
            {
                MessageBox.Show("没有找到指定文本.", "Codematic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            txtContent.Document.Replace(m.Index + startPos, m.Length, replaceWith);

            return m.Index + m.Length + startPos;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="replaceWith"></param>
        public void ReplaceAll(Regex regex, string replaceWith)
        {
            string context = this.txtContent.Text;

            this.txtContent.Text = regex.Replace(this.txtContent.Text, replaceWith);

        }
        /// <summary>
        /// Calls FrmGotoLine
        /// </summary>
        public void GoToLine()
        {
            int lineNumber = txtContent.GetLineFromCharIndex(txtContent.SelectionStart);
            // Fix Goto Bug
            lineNumber++;
            FrmGotoLine frmGotoLine = new FrmGotoLine(this, lineNumber, txtContent.Document.LineSegmentCollection.Count);
            frmGotoLine.Show();
        }
        /// <summary>
        /// Sets cursor to requested line
        /// </summary>
        /// <param name="line"></param>
        public void GoToLine(int line)
        {
            // Fix Goto Bug
            int offset = txtContent.Document.GetLineSegment(line - 1).Offset;
            int length = txtContent.Document.GetLineSegment(line - 1).Length;
            txtContent.ActiveTextAreaControl.TextArea.ScrollTo(line - 1);
            if (length == 0)
                length++;
            txtContent.Select(offset, length);
            txtContent.SetLine(line - 1);
        }

        /// <summary>
        /// Returns the name of requested alia
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public string GetAliasTableName(string alias)
        {
            //SQL.SQLStatement statement = new QueryCommander.SQL.SQLStatement(txtContent.Text, txtContent.SelectionStart, SQL.SQLStatement.SearchOrder.asc, DBConnectionType.MicrosoftSqlClient);
            //return statement.GetAliasTableName(alias);
            return null;
        }
        /// <summary>
        /// Sets [dbConnection] and [DatabaseName]
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="conn"></param>
        public void SetDatabaseConnection(string dbName, IDbConnection conn)
        {
            //if (DatabaseFactory.GetConnectionType(conn) != QueryCommander.Database.DBConnectionType.DB2)
            //{
            //    this.Text = _OrginalName + " [" + dbName + "]";
            //    DatabaseName = dbName;
            //}
            //else
            //{
            //    this.Text = _OrginalName + " [" + conn.Database + "]";
            //    DatabaseName = conn.Database;

            //}
            //dbConnection = conn;

            //string highLightingStragegy = DatabaseFactory.ChangeDatabase(conn, dbName);
            //DatabaseName = dbName;
            //txtContent.SetHighLightingStragegy(highLightingStragegy);


            //MainForm frm = (MainForm)MdiParentForm;
            //frm.SetPandelInfo();
        }

        /// <summary>
        /// This is where it happens...
        /// </summary>
        public void RunQuery()
        {
            ExecuteSql();
            //MessageBox.Show("查询");
            //MainForm frm = (MainForm)MdiParentForm;

            //string msg = "";
            //this.Cursor = Cursors.WaitCursor;
            //string SQLstring = "";
            //DataSet ds = null;

            //// VSS
            //if (IsCheckedOutBuOtherUser())
            //{
            //    MessageBox.Show("Object is checked out by other user", "Source Control");
            //    return;
            //}
            //// Plugin
            //ExecutePlugin(Common.TriggerTypes.OnBeforeQueryExecution, new QueryCommander.PlugIn.Core.CallContext(dbConnection, txtContent.Text, null));

            //frm.statusBar.Panels[3].Text = "Executing query...";

            //try
            //{
            //    // Handling InfoMessages
            //    txtContent.ClearInfoMessages();

            //    /// Handling Comment header
            //    ComplementHeader();

            //    frm.OutputWindow.Text = "Output";

            //    // Resets exception underlining
            //    if (txtContent.SelectedText.Length > 1)
            //    {
            //        SQLstring = txtContent.SelectedText;
            //    }
            //    else
            //    {
            //        SQLstring = txtContent.Text;

            //    }

            //    IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);

            //    /********************************************
            //        * The Query is expected to return a dataset 
            //    ********************************************/
            //db.ExecuteCommand("SET SHOWPLAN_ALL OFF\nGO", dbConnection, this.DatabaseName);
            //db.ExecuteCommand("SET NOEXEC OFF\nGO", dbConnection, this.DatabaseName);
            //    //SQLstring = "SET SHOWPLAN_ALL OFF\nGO\nSET NOEXEC OFF\nGO\n" + SQLstring;

            //    // RunWithIOStatistics hits the user about none efficient queries
            //    if (_settings.Exists() && SQLstring.IndexOf("SHOWPLAN_ALL ON") <= 0)
            //    {
            //        if (_settings.RunWithIOStatistics && GetObjectName().Length == 0)
            //            SQLstring = "SET STATISTICS IO ON\n" + SQLstring + "\nSET STATISTICS IO OFF";
            //    }

            //    DateTime dt = DateTime.Now;


            //    _currentManager = db;
            //    txtContent.Enabled = false;
            //    RunAsyncCallDelegate msc = new RunAsyncCallDelegate(RunAsyncCall);
            //    AsyncCallback cb = new AsyncCallback(RunAsyncCallback);
            //    _asyncResult = msc.BeginInvoke(SQLstring, db, dbConnection, DatabaseName, cb, null);
            //    ExecutionTimer.Enabled = true;

            //    return;

            //}
            //catch (Exception ex)
            //{
            //    string error = ex.Message;

            //    if (error == "Database cannot be null, the empty string, or string of only whitespace.")
            //        error += "\nRight click on a database node in the Microsoft SQL Servers window to the left. Click [Use database].";

            //    frm.OutputWindow.Text = "Output";
            //    this.Cursor = Cursors.Default;
            //    frm.ShowTaskWindow();
            //    if (ex is System.Xml.XmlException)
            //    {
            //        frm.TaskList.ApplyTask(msg + "\n\n" + "XML Exception message\n" + error);
            //    }
            //    else
            //        frm.TaskList.ApplyTask(msg + "\n\n" + "Server message\n" + error);

            //    frm.TaskList.Activate();
            //    frm.statusBar.Panels[3].Text = "";

            //    if (ex.Message.IndexOf("Line") > -1)
            //    {
            //        try
            //        {
            //            int start = ex.Message.IndexOf("Line") + 4;
            //            int length = ex.Message.IndexOf(":", start) - start;
            //            string line = ex.Message.Substring(start, length);
            //            int l = Convert.ToInt32(line);

            //            GoToLine(l);
            //        }
            //        catch
            //        {
            //            return;
            //        }
            //    }

            //}
            //this.Cursor = Cursors.Default;

            ////Plugin
            //ExecutePlugin(Common.TriggerTypes.OnAfterQueryExecution, new QueryCommander.PlugIn.Core.CallContext(dbConnection, txtContent.Text, ds));

        }

        public void ExecuteSql()
        {
            MainForm frm = (MainForm)MdiParentForm;
            DataSet ds = new DataSet();
            try
            {
                DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
                string longServername = dbviewfrm.GetLongServername();
                if (longServername.Length < 1)
                {
                    this.toolStripStatusLabel1.Text = "没有选择任何服务器！";
                    return;
                }
                string dbname = frm.toolComboBox_DB.Text;
                if (dbname.Length < 1)
                {
                    this.toolStripStatusLabel1.Text = "没有选择可执行的数据库！";
                    return;
                }

                this.toolStripStatusLabel1.Text = "正在进行批查询......";
                string SQLstring = this.txtContent.Text;
                if (txtContent.SelectedText.Length > 1)
                {
                    SQLstring = txtContent.SelectedText;
                }
                else
                {
                    SQLstring = txtContent.Text;

                }
                if (SQLstring.Trim() == "")
                {
                    this.toolStripStatusLabel1.Text = "查询语句为空！";
                    return;
                }
                Maticsoft.IDBO.IDbObject dbobj = ObjHelper.CreatDbObj(longServername);
                this.txtInfo.Text = "";
                StringBuilder rowinfo = new StringBuilder();
                if ((!SQLstring.Trim().StartsWith("select")) && (!SQLstring.Trim().StartsWith("SELECT")))
                {
                    //GO的处理
                    if (SQLstring.IndexOf("GO\r\n") > 0)
                    {
                        string[] split = SQLstring.Split(new string[] { "GO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string sql in split)
                        {
                            if (sql.Trim() != "")
                            {
                                int rows = dbobj.ExecuteSql(dbname, sql.Trim());
                                rowinfo.Append("命令成功完成!" + "\r\n");
                            }
                        }
                    }
                    else
                    {
                        int rows = dbobj.ExecuteSql(dbname, SQLstring);
                        rowinfo.Append("命令成功完成" + "（所影响的行数为 " + rows.ToString() + " 行）");
                    }
                    this.tabControl1.SelectedIndex = 1;
                }
                else
                {
                    ds = dbobj.Query(dbname, SQLstring);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            rowinfo.Append("（所影响的行数为 " + dt.Rows.Count.ToString() + " 行）" + "\r\n");
                        }
                    }
                    this.tabControl1.SelectedIndex = 0;
                }
                this.txtInfo.Text = rowinfo.ToString();
                this.toolStripStatusLabel1.Text = "命令已成功完成。";

            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                this.toolStripStatusLabel1.Text = "命令执行失败！";
                txtInfo.Text = ex.Message;
                this.tabControl1.SelectedIndex = 1;
            }
            try
            {
                this.tabControl1.Visible = true;
                //this.menuhidewin.Text="隐藏结果窗口";
                //this.dataGrid1.BackgroundColor=Color.FromArgb(150,185,226);//控件的背景色
                //this.dataGrid1.BackColor=Color.FromArgb(150,185,226);//交叉颜色
                this.dataGridView1.BackColor = Color.FromArgb(148, 182, 237);//交叉颜色
                //this.dataGridView1.SelectionBackColor = Color.FromArgb(0, 85, 231);//整行选中时的背景颜色
                //this.dataGridView1.SelectionForeColor = Color.FromArgb(100, 0, 0);//前景字体色
                if (ds.Tables.Count > 1)
                {
                    this.dataGridView1.DataSource = ds;
                }
                else
                    if (ds.Tables.Count == 1)
                    {
                        this.dataGridView1.DataSource = ds.Tables[0];
                    }
            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                //this.toolStripStatusLabel1.Text = "命令执行失败！";
                //txtInfo.Text = ex.Message;
                //this.tabControl1.SelectedIndex = 1;
            }

            // connect.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopCurrentExecution()
        {
            txtContent.Enabled = true;

            //if (_currentManager == null)
            //    return;

            //if (!_currentManager.StopExecuting())
            //    MessageBox.Show("Unable to stop execution");
            //else
            //    MessageBox.Show("Execution terminated.");

        }
        /// <summary>
        /// Selects current line before calling RunQuery
        /// </summary>
        public void RunQueryLine()
        {
            Point pt = txtContent.GetPositionFromCharIndex(txtContent.SelectionStart);
            pt.X = 0;
            int lineStartPosition = txtContent.GetCharIndexFromPosition(pt);
            int lineEndPosition = txtContent.Text.IndexOf("\n", lineStartPosition);
            if (lineEndPosition == -1)
                lineEndPosition = txtContent.Text.Length;

            txtContent.Select(lineStartPosition, lineEndPosition - lineStartPosition);
            RunQuery();
        }
        /// <summary>
        /// Selects current query before calling RunQuery
        /// </summary>
        public void RunCurrentQuery()
        {
            try
            {
                //SetCurrentStatement();
                RunQuery();
            }
            catch(System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                return; 
            }


        }
        /// <summary>
        /// Generates insert statements based on current query
        /// </summary>
        public void CreateInsertStatement()
        {
            //MainForm frm = (MainForm)MdiParentForm;
            //string SQLstring;

            //try
            //{
            //    txtContent.SuspendLayout();
            //    if (txtContent.SelectedText.Length > 1)
            //        SQLstring = txtContent.SelectedText;
            //    else
            //        SQLstring = txtContent.Text;

            //    frm.NewQueryform();

            //    txtContent.ResumeLayout();

            //    CreateInsertStatement(SQLstring);
            //}
            //catch (Exception ex)
            //{
            //    frm.ShowTaskWindow();
            //    frm.TaskList.ApplyTask("Invalid SQL\n" + ex.Message);
            //    frm.TaskList.Activate();
            //}
        }

        public void CreateInsertStatement(string SQLstring)
        {

            //string Result = "";
            //MainForm frm = (MainForm)MdiParentForm;
            //try
            //{
            //    txtContent.SuspendLayout();

            //    IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
            //    Result = db.GetInsertStatements(SQLstring, dbConnection, DatabaseName);

            //    if (Result.Length > 0)
            //    {
            //        frm.ActiveQueryForm.SetDatabaseConnection(this.DatabaseName, this.dbConnection);
            //        frm.ActiveQueryForm.Content = Result;
            //    }
            //    txtContent.ResumeLayout();
            //}
            //catch (Exception ex)
            //{
            //    frm.ShowTaskWindow();
            //    frm.TaskList.ApplyTask("Invalid SQL\n" + ex.Message);
            //    frm.TaskList.Activate();
            //}
        }

        /// <summary>
        /// Generates update statements based on current query
        /// </summary>
        public void CreateUpdateStatement()
        {
            //MainForm frm = (MainForm)MdiParentForm;
            //string SQLstring;

            //try
            //{
            //    txtContent.SuspendLayout();
            //    if (txtContent.SelectedText.Length > 1)
            //        SQLstring = txtContent.SelectedText;
            //    else
            //        SQLstring = txtContent.Text;
            //    txtContent.ResumeLayout();

            //    frm.NewQueryform();

            //    CreateUpdateStatement(SQLstring);
            //}
            //catch (Exception ex)
            //{
            //    frm.ShowTaskWindow();
            //    frm.TaskList.ApplyTask("Invalid SQL\n" + ex.Message);
            //    frm.TaskList.Activate();
            //}
        }

        /// <summary>
        /// Generates update statements based on current query
        /// </summary>
        public void CreateUpdateStatement(string SQLstring)
        {
            //string Result = "";
            //MainForm frm = (MainForm)MdiParentForm;
            //try
            //{
            //    txtContent.SuspendLayout();

            //    IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
            //    Result = db.GetUpdateStatements(SQLstring, dbConnection, DatabaseName);

            //    if (Result.Length > 0)
            //    {
            //        frm.ActiveQueryForm.SetDatabaseConnection(this.DatabaseName, this.dbConnection);
            //        frm.ActiveQueryForm.Content = Result;
            //    }
            //    txtContent.ResumeLayout();
            //}
            //catch (Exception ex)
            //{
            //    frm.ShowTaskWindow();
            //    frm.TaskList.ApplyTask("Invalid SQL\n" + ex.Message);
            //    frm.TaskList.Activate();
            //}
        }

        /// <summary>
        /// Undo next action in the undo buffer
        /// </summary>
        public void Undo()
        {
            txtContent.UndoAction();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Paste()
        {

            txtContent.Paste();

        }
        /// <summary>
        /// 
        /// </summary>
        public void Copy()
        {
            txtContent.Copy();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Cut()
        {
            txtContent.Cut();
        }
        /// <summary>
        /// Adds a Comment header
        /// </summary>
        public void InsertHeader()
        {
            //txtContent.SuspendLayout();
            //string header = ParseHeaderComment();
            //txtContent.Text = header + txtContent.Text;
            //txtContent.ResumeLayout();
        }
        /// <summary>
        /// Alters the comment header with a revision tag
        /// </summary>
        public void AddRevisionCommentSection()
        {
            int startpos = txtContent.Text.IndexOf("</member>", 0);
            if (startpos < 1)
                return;
            startpos = txtContent.Text.LastIndexOf("</revision>") + 11;
            txtContent.Text = txtContent.Text.Substring(0, startpos) + "\n\t<revision author='" + SystemInformation.UserName.ToString() + "' date='" + DateTime.Now.ToString() + "'>Altered</revision>" + txtContent.Text.Substring(startpos);
            txtContent.Refresh();

        }

        /// <summary>
        /// Consolidates all xml comment headers 
        /// </summary>
        public void GetXmlDocFile()
        {
            //string whereConcitions = "";
            //FrmAlterDocumentationOutput frm = new FrmAlterDocumentationOutput();
            //frm.ShowDialog(this);
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    if (frm.chbView.Checked)
            //        whereConcitions = "'V'";
            //    if (frm.chbSP.Checked)
            //        if (whereConcitions.Length > 0)
            //            whereConcitions += ",'P'";
            //        else
            //            whereConcitions = "'P'";
            //    if (frm.chbFn.Checked)
            //        if (whereConcitions.Length > 0)
            //            whereConcitions += ",'FN','TF'";
            //        else
            //            whereConcitions = "'FN','TF'";
            //    if (frm.txtLike.Text.Length > 0)
            //    {
            //        string like = frm.txtLike.Text.Replace("*", "%");
            //        whereConcitions += ") AND (o.name like '" + like + "'";
            //    }
            //}

            //this.Cursor = Cursors.WaitCursor;
            //string doc = "<?xml version='1.0' encoding='UTF-8'?>\n<!-- Generated by QueryCommander-->\n<?xml-stylesheet type='text/xsl' href='doc.xsl'?>\n<members>\n";
            //XmlDocument xmlDoc = new XmlDocument();
            //try
            //{
            //    IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
            //    doc += db.GetXmlDoc(DatabaseName, dbConnection, whereConcitions) + "\n</members>";

            //    xmlDoc.LoadXml(doc);
            //}
            //catch (Exception ex)
            //{
            //    int startpos = ex.Message.IndexOf("Line ") + 5;
            //    int endpos = ex.Message.IndexOf(",", startpos);
            //    int line = Convert.ToInt16(ex.Message.Substring(startpos, endpos - startpos));
            //    FrmXMLError frmXMLError = new FrmXMLError(ex.Message, doc, line);
            //    frmXMLError.ShowDialog(this);
            //    return;
            //}


            //// TODO:  create common file same method or class
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.AddExtension = true;
            //saveFileDialog.DefaultExt = "xml";
            //saveFileDialog.FileName = DatabaseName + " Procedures";
            //saveFileDialog.Title = "Save Documentation file";
            //saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            //DialogResult result = saveFileDialog.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    string saveFileName = saveFileDialog.FileName;
            //    try
            //    {
            //        xmlDoc.Save(saveFileName);

            //    }
            //    catch (Exception exp)
            //    {
            //        MessageBox.Show("An error occurred while attempting to save the file. The error is:"
            //            + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
            //        return;
            //    }
            //    string xslPath = saveFileName.Substring(0, saveFileName.LastIndexOf("\\"));
            //    CopyEmbeddedResource("QueryCommander.Embedded.doc.xsl", xslPath + "\\doc.xsl");

            //    System.Diagnostics.Process.Start("IExplore.exe", saveFileName);


            //    this.Cursor = Cursors.Default;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentWord()
        {
            return txtContent.GetCurrentWord();
        }
        /// <summary>
        /// Save query statement to file
        /// </summary>
        /// <param name="path"></param>
        public void SaveAs(string path)
        {
            try
            {
                // remove readonly attribute
                FileInfo fi = new FileInfo(path);
                if (fi.Exists && ((fi.Attributes & System.IO.FileAttributes.ReadOnly) != 0))
                {
                    DialogResult dr = MessageBox.Show("Overwrite read-only file?", path, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        fi.Attributes -= System.IO.FileAttributes.ReadOnly;
                        fi.Delete();
                    }
                    else
                    {
                        return;
                    }
                }
                txtContent.SaveFile(path);
                FileName = path;
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(string.Format("Errors Ocurred\n{0}", ex.Message), "Save Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Gives the user the option to alter the header
        /// </summary>
        public void ComplementHeader()
        {
            //string header = "";
            //if (txtContent.SelectedText.Length > 1)
            //    return;
            //try
            //{
            //    QueryCommander.Config.Settings settings = QueryCommander.Config.Settings.Load();
            //    if (settings.Exists())
            //    {
            //        if (!settings.ShowFrmDocumentHeader)
            //            return;
            //    }
            //    int start = txtContent.Text.IndexOf("<member");
            //    int end = txtContent.Text.IndexOf("</member>");
            //    if (start > -1 && end > -1)
            //    {
            //        end += 9; //Add length of </member>
            //        header = txtContent.Text.Substring(start, end - start);
            //        FrmDocumentHeader frm = new FrmDocumentHeader(header);
            //        if (frm.ShowDialogWindow(this) == DialogResult.OK)
            //        {
            //            txtContent.Text = txtContent.Text.Replace(header, frm.Header);
            //            txtContent.Refresh();

            //        }
            //        XmlDocument _doc = new XmlDocument();
            //        _doc.LoadXml(header);
            //        XmlNodeList nList = _doc.GetElementsByTagName("member");
            //        XmlNode n = nList[0].Attributes.GetNamedItem("name");
            //    }
            //}
            //catch (System.Xml.XmlException ex)
            //{
            //    int pos = 0;
            //    for (int i = 0; i < ex.LineNumber - 1; i++)
            //    {
            //        pos = header.IndexOf("\n", pos + 1);
            //    }

            //    string lineText = header.Substring(pos, ex.LinePosition) + "<-\n\n\tMake sure the text in well formated\n\nref: http://www.w3c.org\n";

            //    XmlException xmlEx = new XmlException(ex.Message + "\n" + lineText, ex.InnerException, ex.LineNumber, ex.LinePosition);

            //    throw xmlEx;
            //}

        }

        /// <summary>
        /// Calls MainForm.PopulateRecentItems
        /// </summary>
        /// <param name="objectName"></param>
        public void AddToRecentObjects(string objectName)
        {
            //if (objectName.Trim().Length == 0)
            //    return;

            //bool nodeExists = false;

            //try
            //{
            //    XmlDocument doc = GetRecentObjects();

            //    XmlNodeList rootNodeList = doc.GetElementsByTagName("recentobjects");

            //    XmlNodeList nl = doc.GetElementsByTagName("objectName");

            //    foreach (XmlNode n in nl)
            //    {
            //        if (n.InnerText == objectName)
            //        {
            //            n.Attributes["changedate"].Value = DateTime.Now.ToString();
            //            nodeExists = true;
            //            break;
            //        }
            //    }

            //    if (!nodeExists)
            //    {
            //        XmlElement xmlelem = doc.CreateElement("", "objectName", "");
            //        XmlText xmltext = doc.CreateTextNode(objectName);
            //        xmlelem.AppendChild(xmltext);
            //        xmlelem.SetAttribute("changedate", DateTime.Now.ToString());
            //        doc.ChildNodes.Item(1).AppendChild(xmlelem);
            //        XmlElement elem = doc.CreateElement("objectName");
            //        elem.SetAttribute("changedate", DateTime.Now.ToLongTimeString());
            //    }

            //    doc.Save(Application.StartupPath + "\\RecentObjects.xml");
            //    MainForm frm = (MainForm)MdiParentForm;
            //    frm.PopulateRecentItems();
            //}
            //catch
            //{
            //    throw;
            //}

        }

        /// <summary>
        /// xml2Data - CreateTablesScript
        /// </summary>
        public void GetCreateTablesScriptFromXMLFile()
        {
            try
            {
                //    FrmChooseXMLFile frm = new FrmChooseXMLFile();
                //    frm.rbData.Checked = false;
                //    frm.rbStructure.Checked = true;

                //    if (frm.ShowDialogWindow(this) == DialogResult.OK)
                //    {
                //        string file = frm.FileName;
                //        bool createKeys = frm.CreateKeys;
                //        XmlTextReader reader = new XmlTextReader(file);
                //        XMLDatabase xmlDatabase = new XMLDatabase(reader);
                //        string script = "";

                //        if (frm.rbStructure.Checked)
                //        {
                //            script = xmlDatabase.GetDatabaseSQLScript(createKeys);
                //        }
                //        else
                //        {
                //            script = xmlDatabase.GetInsertScript(createKeys);
                //        }

                //        this.Content = script;
                //    }
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                if (ex.Message == "XmlContainsAttributes")
                {
                    MessageBox.Show("QueryCommander XML-import only support XmlElement in this version\n\nSample:\n<PERSON>\n\t<FIRSTNAME>John</FIRSTNAME>\n\t<LASTNAME>Smith</LASTNAME>\n</PERSON>", "XMLAttributes not supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                throw;
            }
        }
        /// <summary>
        /// xml2Data - Import data
        /// </summary>
        public void GetInsertScriptFromXMLFile()
        {
            try
            {
                //FrmChooseXMLFile frm = new FrmChooseXMLFile();
                //frm.rbData.Checked = true;
                //frm.rbStructure.Checked = false;

                //if (frm.ShowDialogWindow(this) == DialogResult.OK)
                //{
                //    string file = frm.FileName;
                //    bool createKeys = frm.CreateKeys;
                //    XmlTextReader reader = new XmlTextReader(file);
                //    XMLDatabase xmlDatabase = new XMLDatabase(reader);
                //    string script = "";

                //    if (frm.rbStructure.Checked)
                //    {
                //        script = xmlDatabase.GetDatabaseSQLScript(createKeys);
                //    }
                //    else
                //    {
                //        script = xmlDatabase.GetInsertScript(createKeys);
                //    }

                //    this.Content = script;
                //}
            }
            catch
            {
                throw;
            }
        }

        ///// <summary>
        ///// Calls Plug-in method
        ///// </summary>
        ///// <param name="triggerType"></param>
        ///// <param name="callContext"></param>
        //public void ExecutePlugin(Common.TriggerTypes triggerType, QueryCommander.PlugIn.Core.CallContext callContext)
        //{
        //    try
        //    {
        //        string ret = null;
        //        MainForm frm = (MainForm)MdiParentForm;
        //        foreach (string fileName in frm.Plugins)
        //        {
        //            if (fileName.IndexOf("Interop.") > -1)
        //                continue;

        //            if (IsTriggerType(fileName, triggerType))
        //            {
        //                object obj = GetTriggerTypeObject(fileName, triggerType);
        //                PropertyInfo property = obj.GetType().GetProperty("ExecutionType");
        //                Common.ExecutionTypes executionType = (Common.ExecutionTypes)property.GetValue(obj, null);

        //                switch (triggerType)
        //                {
        //                    case Common.TriggerTypes.OnBeforeQueryExecution:
        //                        ret = (string)((QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnBeforeQueryExecution1)obj).Execute(callContext, this.Handle, ((MainForm)MdiParentForm).plugInVariables);
        //                        break;
        //                    case Common.TriggerTypes.OnAfterQueryExecution:
        //                        ret = (string)((QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnAfterQueryExecution1)obj).Execute(callContext, this.Handle, ((MainForm)MdiParentForm).plugInVariables);
        //                        break;
        //                    default:
        //                        return;
        //                }

        //                switch (executionType)
        //                {
        //                    case Common.ExecutionTypes.InsertAtPoint:
        //                        int pos = txtContent.SelectionStart;
        //                        txtContent.SelectedText = (string)ret;
        //                        break;
        //                    case Common.ExecutionTypes.InsertToNewQueryWindow:
        //                        frm.NewQueryform();
        //                        frm.ActiveQueryForm.Content = (string)ret;
        //                        break;
        //                    case Common.ExecutionTypes.ReplaceToQueryWindow:
        //                        this.Content = (string)ret;
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    catch //(Exception ex)
        //    {
        //        return;
        //    }
        //}
        ///// <summary>
        ///// Calls Plug-in method
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="menuItem"></param>

        //public void ExecutePlugin(QueryCommander.PlugIn.Core.Interfaces.IPlugin_OnMenuClick1 obj, MenuItem menuItem)
        //{
        //    QueryCommander.PlugIn.Core.CallContext callContext = new QueryCommander.PlugIn.Core.CallContext(this.dbConnection, txtContent.Text, null);

        //    object ret = obj.Execute(callContext, this.Handle, ((MainForm)MdiParentForm).plugInVariables, menuItem);
        //    switch (obj.ExecutionType)
        //    {
        //        case Common.ExecutionTypes.InsertAtPoint:
        //            int pos = txtContent.SelectionStart;
        //            txtContent.SelectedText = (string)ret;
        //            break;
        //        case Common.ExecutionTypes.InsertToNewQueryWindow:
        //            MainForm frm = (MainForm)MdiParentForm;
        //            frm.NewQueryform();
        //            frm.ActiveQueryForm.Content = (string)ret;
        //            break;
        //        case Common.ExecutionTypes.ReplaceToQueryWindow:
        //            this.Content = (string)ret;
        //            break;
        //    }
        //}


        public void PrintStatement(bool preview)
        {
            //_printType = PrintType.Statement;
            //if (preview)
            //{
            //    printPreviewDialog.ShowDialog();
            //    return;
            //}
            //if (printDialog.ShowDialog() == DialogResult.OK)
            //{
            //    printDocument.Print();
            //}
        }
        public void PrintOutPut(bool preview)
        {
            try
            {
                //_printType = PrintType.output;
                //TabPage tc = ((MainForm)this.MdiParentForm).OutputWindow.tabControl1.TabPages[0];

                //for (int controlIndex = 0; controlIndex < tc.Controls.Count; controlIndex++)
                //{
                //    if (tc.Controls[controlIndex] is DataGrid)
                //    {
                //        DataGrid dg = (DataGrid)tc.Controls[controlIndex];

                //        _gridPrinterClass = new GridPrinterClass(printDocument, dg);

                //        if (preview)
                //        {
                //            printPreviewDialog.ShowDialog();
                //            return;
                //        }
                //        else
                //            printDocument.Print();
                //    }
                //}

                ////			_printType = PrintType.output;
                ////			foreach(TabPage tc in ((MainForm) this.MdiParentForm).OutputWindow.tabControl1.TabPages)
                ////			{
                ////				//foreach(DataGrid dg in tc.Controls)
                ////				for(int controlIndex=0;controlIndex<tc.Controls.Count;controlIndex++)
                ////				{
                ////					if(tc.Controls[controlIndex] is DataGrid)
                ////					{
                ////						DataGrid dg = (DataGrid)tc.Controls[controlIndex];
                ////					
                ////						_gridPrinterClass = new GridPrinterClass(printDocument,dg);
                ////					
                ////						if(preview)
                ////						{
                ////							printPreviewDialog.ShowDialog();
                ////							return;
                ////						}
                ////						else
                ////							printDocument.Print();
                ////					}
                ////				}
                ////			}

                //_gridPrinterClass = null;
            }
            catch
            { return; }

        }
        public void PrintPageSetUp()
        {
            //pageSetupDialog.ShowDialog();
        }


        #endregion



        #region Classes
        /// <summary>
        /// Custom MenuItem used for snippets
        /// </summary>
        public class SnippetMenuItem : MenuItem
        {
            public string statement = "";
        }
        //public class DateTimeReverserClass : IComparer
        //{
        //    // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        //    int IComparer.Compare(Object x, Object y)
        //    {
        //        DateTime dx = (DateTime)x;
        //        DateTime dy = (DateTime)y;
        //        if (dx > dy)
        //            return -1;
        //        else
        //            return 1;
        //    }
        //}

        private class Alias
        {
            public Alias(string alias, string table)
            {
                AliasName = alias;
                TableName = table;

            }

            public string AliasName;
            public string TableName;
        }

        //private class OutPutContainer
        //{
        //    public OutPutContainer(DataSet dataset, IDatabaseManager database, string message, TimeSpan executionTime, bool query, string statusText)
        //    {
        //        this.dataset = dataset;
        //        this.database = database;
        //        this.message = message;
        //        this.executionTime = executionTime;
        //        this.query = query;
        //        this.statusText = statusText;
        //    }
        //    public DataSet dataset;
        //    public IDatabaseManager database;
        //    public string message;
        //    public TimeSpan executionTime;
        //    public string statusText;
        //    bool query;
        //}
        #endregion
    }
}