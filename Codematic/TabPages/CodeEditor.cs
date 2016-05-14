using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LTP.TextEditor;
namespace Codematic
{
    public partial class CodeEditor : Form
    {
        public string fileName = "";
        private string fileType = "cs";
        public CodeEditor()
        {
            InitializeComponent();
        }

        public CodeEditor(string tempFile, string FileType)
        {
            InitializeComponent();
            switch (FileType)
            {
                case "cs":
                    txtContent.Language = Languages.CSHARP;
                    break;
                case "vb":
                    txtContent.Language = Languages.VBNET;
                    break;
                case "html":
                    txtContent.Language = Languages.HTML;
                    break;
                case "sql":
                    txtContent.Language = Languages.SQL;
                    break;
                case "cpp":
                    txtContent.Language = Languages.CPP;
                    break;
                case "js":
                    txtContent.Language = Languages.JavaScript;
                    break;
                case "java":
                    txtContent.Language = Languages.Java;
                    break;
                case "xml":
                    txtContent.Language = Languages.XML;
                    break;
                case "txt":
                    txtContent.Language = Languages.XML;
                    break;
            }
            StreamReader srFile = new StreamReader(tempFile, Encoding.Default);
            string Contents = srFile.ReadToEnd();
            srFile.Close();
            this.txtContent.Text = Contents;
        }

        public CodeEditor(string tempFile, string FileType, bool NewCreate)
        {
            this.InitializeComponent();
            this.fileType = FileType;
            string key;
            switch (key = FileType.ToLower())
            {
                case "cs":
                    this.txtContent.Language = Languages.CSHARP;
                    break;
                case "vb":
                    this.txtContent.Language = Languages.VBNET;
                    break;
                case "cmt":
                case "tt":
                case "html":
                    this.txtContent.Language = Languages.HTML;
                    break;
                case "sql":
                    this.txtContent.Language = Languages.SQL;
                    break;
                case "cpp":
                    this.txtContent.Language = Languages.CPP;
                    break;
                case "js":
                    this.txtContent.Language = Languages.JavaScript;
                    break;
                case "java":
                    this.txtContent.Language = Languages.Java;
                    break;
                case "xml":
                    this.txtContent.Language = Languages.XML;
                    break;
                case "txt":
                    this.txtContent.Language = Languages.XML;
                    break;
                default: this.txtContent.Language = Languages.CSHARP;
                    break;
            }

            if (!NewCreate)
            {
                this.fileName = tempFile;
            }
            StreamReader streamReader = new StreamReader(tempFile, Encoding.Default);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            this.txtContent.Text = text;
        }

        public CodeEditor(string strCode, string FileType, string temp)
        {
            InitializeComponent();
            switch (FileType)
            {
                case "cs":
                    txtContent.Language = Languages.CSHARP;
                    break;
                case "vb":
                    txtContent.Language = Languages.VBNET;
                    break;
                case "html":
                    txtContent.Language = Languages.HTML;
                    break;
                case "sql":
                    txtContent.Language = Languages.SQL;
                    break;
                case "cpp":
                    txtContent.Language = Languages.CPP;
                    break;
                case "js":
                    txtContent.Language = Languages.JavaScript;
                    break;
                case "java":
                    txtContent.Language = Languages.Java;
                    break;
                case "xml":
                    txtContent.Language = Languages.XML;
                    break;
                case "txt":
                    txtContent.Language = Languages.XML;
                    break;
            }
            this.txtContent.Text = strCode;
        }
        public void Save()
        {
            if (this.fileName.Length > 1 && this.txtContent.Text.Trim().Length > 0)
            {
                StreamWriter streamWriter = new StreamWriter(this.fileName, false, Encoding.UTF8);
                streamWriter.Write(this.txtContent.Text.Trim());
                streamWriter.Flush();
                streamWriter.Close();
                MessageBox.Show(this, "保存成功！", "提示", MessageBoxButtons.OK);
                return;
            }
            this.menu_SaveAs_Click(null, null);
        }
        private void menu_Save_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        private void menu_SaveAs_Click(object sender, EventArgs e)
        {
            if (this.txtContent.Text.Trim().Length > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "保存文件";
                string key;
                switch (key = this.fileType)
                {
                    case "cs":
                        saveFileDialog.Filter = "C# files (*.cs)|*.cs|All files (*.*)|*.*";
                        break;
                    case "vb":
                        saveFileDialog.Filter = "VB files (*.vb)|*.vb|All files (*.*)|*.*";
                        break;
                    case "cmt":
                        saveFileDialog.Filter = "Template files (*.cmt)|*.cmt|All files (*.*)|*.*";
                        break;
                    case "tt":
                        saveFileDialog.Filter = "Template files (*.tt)|*.tt|All files (*.*)|*.*";
                        break;
                    case "html":
                        saveFileDialog.Filter = "Html files (*.htm)|*.htm|All files (*.*)|*.*";
                        break;
                    case "sql":
                        saveFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
                        break;
                    case "cpp":
                        saveFileDialog.Filter = "CPP files (*.cpp)|*.cpp|All files (*.*)|*.*";
                        break;
                    case "js":
                        saveFileDialog.Filter = "JS files (*.js)|*.js|All files (*.*)|*.*";
                        break;
                    case "java":
                        saveFileDialog.Filter = "Java files (*.java)|*.java|All files (*.*)|*.*";
                        break;
                    case "xml":
                        saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                        break;
                    case "txt":
                        saveFileDialog.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
                        break;
                    default:
                        saveFileDialog.Filter = "C# files (*.cs)|*.cs|All files (*.*)|*.*";
                        break;
                }
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8);
                    streamWriter.Write(this.txtContent.Text.Trim());
                    streamWriter.Flush();
                    streamWriter.Close();
                    MessageBox.Show(this, "保存成功！", "提示", MessageBoxButtons.OK);
                }
            }
        }
    }
}