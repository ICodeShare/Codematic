using LTP.TextEditor.Actions;
using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LTP.TextEditor
{
    public enum Languages
    {
        SQL,
        VBNET,
        CSHARP,
        HTML,
        CPP,
        Java,
        JavaScript,
        XML
    }

    public abstract class TextEditorControlBase : UserControl
	{

        private string currentFileName;
        private int updateLevel;
        private IDocument document;
        protected Hashtable editactions = new Hashtable();
        private Languages language =Languages.CSHARP;
        public event EventHandler FileNameChanged;
        public event EventHandler Changed;
        public ITextEditorProperties TextEditorProperties
        {
            get
            {
                return this.document.TextEditorProperties;
            }
            set
            {
                this.document.TextEditorProperties = value;
                this.OptionsChanged();
            }
        }
        public Encoding Encoding
        {
            get
            {
                return this.TextEditorProperties.Encoding;
            }
            set
            {
                this.TextEditorProperties.Encoding = value;
            }
        }
        [Browsable(false), ReadOnly(true)]
        public string FileName
        {
            get
            {
                return this.currentFileName;
            }
            set
            {
                if (this.currentFileName != value)
                {
                    this.currentFileName = value;
                    this.OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        [Browsable(false)]
        public bool IsUpdating
        {
            get
            {
                return this.updateLevel > 0;
            }
        }
        [Browsable(false)]
        public IDocument Document
        {
            get
            {
                return this.document;
            }
            set
            {
                this.document = value;
                this.document.UndoStack.TextEditorControl = this;
            }
        }
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return this.Document.TextContent;
            }
            set
            {
                this.Document.TextContent = value;
            }
        }
        [Browsable(false), ReadOnly(true)]
        public bool IsReadOnly
        {
            get
            {
                return this.Document.ReadOnly;
            }
            set
            {
                this.Document.ReadOnly = value;
            }
        }
        [Browsable(false)]
        public bool IsInUpdate
        {
            get
            {
                return this.updateLevel > 0;
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 100);
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true spaces are shown in the textarea")]
        public bool ShowSpaces
        {
            get
            {
                return this.document.TextEditorProperties.ShowSpaces;
            }
            set
            {
                this.document.TextEditorProperties.ShowSpaces = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true antialiased fonts are used inside the textarea")]
        public bool UseAntiAliasFont
        {
            get
            {
                return this.document.TextEditorProperties.UseAntiAliasedFont;
            }
            set
            {
                this.document.TextEditorProperties.UseAntiAliasedFont = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true tabs are shown in the textarea")]
        public bool ShowTabs
        {
            get
            {
                return this.document.TextEditorProperties.ShowTabs;
            }
            set
            {
                this.document.TextEditorProperties.ShowTabs = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true EOL markers are shown in the textarea")]
        public bool ShowEOLMarkers
        {
            get
            {
                return this.document.TextEditorProperties.ShowEOLMarker;
            }
            set
            {
                this.document.TextEditorProperties.ShowEOLMarker = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true the horizontal ruler is shown in the textarea")]
        public bool ShowHRuler
        {
            get
            {
                return this.document.TextEditorProperties.ShowHorizontalRuler;
            }
            set
            {
                this.document.TextEditorProperties.ShowHorizontalRuler = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true the vertical ruler is shown in the textarea")]
        public bool ShowVRuler
        {
            get
            {
                return this.document.TextEditorProperties.ShowVerticalRuler;
            }
            set
            {
                this.document.TextEditorProperties.ShowVerticalRuler = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(80), Description("The row in which the vertical ruler is displayed")]
        public int VRulerRow
        {
            get
            {
                return this.document.TextEditorProperties.VerticalRulerRow;
            }
            set
            {
                this.document.TextEditorProperties.VerticalRulerRow = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(true), Description("If true line numbers are shown in the textarea")]
        public bool ShowLineNumbers
        {
            get
            {
                return this.document.TextEditorProperties.ShowLineNumbers;
            }
            set
            {
                this.document.TextEditorProperties.ShowLineNumbers = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(false), Description("If true invalid lines are marked in the textarea")]
        public bool ShowInvalidLines
        {
            get
            {
                return this.document.TextEditorProperties.ShowInvalidLines;
            }
            set
            {
                this.document.TextEditorProperties.ShowInvalidLines = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(true), Description("If true folding is enabled in the textarea")]
        public bool EnableFolding
        {
            get
            {
                return this.document.TextEditorProperties.EnableFolding;
            }
            set
            {
                this.document.TextEditorProperties.EnableFolding = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(true), Description("If true matching brackets are highlighted")]
        public bool ShowMatchingBracket
        {
            get
            {
                return this.document.TextEditorProperties.ShowMatchingBracket;
            }
            set
            {
                this.document.TextEditorProperties.ShowMatchingBracket = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(true), Description("If true the icon bar is displayed")]
        public bool IsIconBarVisible
        {
            get
            {
                return this.document.TextEditorProperties.IsIconBarVisible;
            }
            set
            {
                this.document.TextEditorProperties.IsIconBarVisible = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(4), Description("The width in spaces of a tab character")]
        public int TabIndent
        {
            get
            {
                return this.document.TextEditorProperties.TabIndent;
            }
            set
            {
                this.document.TextEditorProperties.TabIndent = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), DefaultValue(LineViewerStyle.None), Description("The line viewer style")]
        public LineViewerStyle LineViewerStyle
        {
            get
            {
                return this.document.TextEditorProperties.LineViewerStyle;
            }
            set
            {
                this.document.TextEditorProperties.LineViewerStyle = value;
                this.OptionsChanged();
            }
        }
        [Category("Behavior"), DefaultValue(IndentStyle.Smart), Description("The indent style")]
        public IndentStyle IndentStyle
        {
            get
            {
                return this.document.TextEditorProperties.IndentStyle;
            }
            set
            {
                this.document.TextEditorProperties.IndentStyle = value;
                this.OptionsChanged();
            }
        }
        [Category("Behavior"), DefaultValue(false), Description("Converts tabs to spaces while typing")]
        public bool ConvertTabsToSpaces
        {
            get
            {
                return this.document.TextEditorProperties.ConvertTabsToSpaces;
            }
            set
            {
                this.document.TextEditorProperties.ConvertTabsToSpaces = value;
                this.OptionsChanged();
            }
        }
        [Category("Behavior"), DefaultValue(false), Description("Creates a backup copy for overwritten files")]
        public bool CreateBackupCopy
        {
            get
            {
                return this.document.TextEditorProperties.CreateBackupCopy;
            }
            set
            {
                this.document.TextEditorProperties.CreateBackupCopy = value;
                this.OptionsChanged();
            }
        }
        [Category("Behavior"), DefaultValue(false), Description("Hide the mouse cursor while typing")]
        public bool HideMouseCursor
        {
            get
            {
                return this.document.TextEditorProperties.HideMouseCursor;
            }
            set
            {
                this.document.TextEditorProperties.HideMouseCursor = value;
                this.OptionsChanged();
            }
        }
        [Category("Behavior"), DefaultValue(false), Description("Allows the caret to be places beyonde the end of line")]
        public bool AllowCaretBeyondEOL
        {
            get
            {
                return this.document.TextEditorProperties.AllowCaretBeyondEOL;
            }
            set
            {
                this.document.TextEditorProperties.AllowCaretBeyondEOL = value;
                this.OptionsChanged();
            }
        }
        [Category("Behavior"), DefaultValue(BracketMatchingStyle.After), Description("Specifies if the bracket matching should match the bracket before or after the caret.")]
        public BracketMatchingStyle BracketMatchingStyle
        {
            get
            {
                return this.document.TextEditorProperties.BracketMatchingStyle;
            }
            set
            {
                this.document.TextEditorProperties.BracketMatchingStyle = value;
                this.OptionsChanged();
            }
        }
        [Browsable(true), Description("The base font of the text area. No bold or italic fonts can be used because bold/italic is reserved for highlighting purposes.")]
        public override Font Font
        {
            get
            {
                return this.document.TextEditorProperties.Font;
            }
            set
            {
                this.document.TextEditorProperties.Font = value;
                this.OptionsChanged();
            }
        }
        [Category("Appearance"), Description("设定高亮显示的语言.")]
        public Languages Language
        {
            get
            {
                return this.language;
            }
            set
            {
                switch (value)
                {
                    case Languages.SQL:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
                        return;
                    case Languages.VBNET:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("VBNET");
                        return;
                    case Languages.CSHARP:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                        return;
                    case Languages.HTML:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
                        return;
                    case Languages.CPP:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C++.NET");
                        return;
                    case Languages.Java:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("Java");
                        return;
                    case Languages.JavaScript:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("JavaScript");
                        return;
                    case Languages.XML:
                        this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
                        return;
                    default:
                        return;
                }
            }
        }
        public abstract TextAreaControl ActiveTextAreaControl
        {
            get;
        }
        private static Font ParseFont(string font)
        {
            string[] array = font.Split(new char[]
			{
				',',
				'='
			});
            return new Font(array[1], float.Parse(array[3]));
        }
        protected TextEditorControlBase()
        {
            this.GenerateDefaultActions();
            HighlightingManager.Manager.ReloadSyntaxHighlighting += new EventHandler(this.ReloadHighlighting);
        }
        private void ReloadHighlighting(object sender, EventArgs e)
        {
            if (this.Document.HighlightingStrategy != null)
            {
                this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(this.Document.HighlightingStrategy.Name);
                this.OptionsChanged();
            }
        }
        internal IEditAction GetEditAction(Keys keyData)
        {
            return (IEditAction)this.editactions[keyData];
        }
        private void GenerateDefaultActions()
        {
            this.editactions[Keys.Left] = new CaretLeft();
            this.editactions[Keys.LButton | Keys.MButton | Keys.Space | Keys.Shift] = new ShiftCaretLeft();
            this.editactions[Keys.LButton | Keys.MButton | Keys.Space | Keys.Control] = new WordLeft();
            this.editactions[Keys.LButton | Keys.MButton | Keys.Space | Keys.Shift | Keys.Control] = new ShiftWordLeft();
            this.editactions[Keys.Right] = new CaretRight();
            this.editactions[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift] = new ShiftCaretRight();
            this.editactions[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Control] = new WordRight();
            this.editactions[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift | Keys.Control] = new ShiftWordRight();
            this.editactions[Keys.Up] = new CaretUp();
            this.editactions[Keys.RButton | Keys.MButton | Keys.Space | Keys.Shift] = new ShiftCaretUp();
            this.editactions[Keys.RButton | Keys.MButton | Keys.Space | Keys.Control] = new ScrollLineUp();
            this.editactions[Keys.Down] = new CaretDown();
            this.editactions[Keys.Back | Keys.Space | Keys.Shift] = new ShiftCaretDown();
            this.editactions[Keys.Back | Keys.Space | Keys.Control] = new ScrollLineDown();
            this.editactions[Keys.Insert] = new ToggleEditMode();
            this.editactions[Keys.LButton | Keys.MButton | Keys.Back | Keys.Space | Keys.Control] = new Copy();
            this.editactions[Keys.LButton | Keys.MButton | Keys.Back | Keys.Space | Keys.Shift] = new Paste();
            this.editactions[Keys.Delete] = new Delete();
            this.editactions[Keys.RButton | Keys.MButton | Keys.Back | Keys.Space | Keys.Shift] = new Cut();
            this.editactions[Keys.Home] = new Home();
            this.editactions[Keys.MButton | Keys.Space | Keys.Shift] = new ShiftHome();
            this.editactions[Keys.MButton | Keys.Space | Keys.Control] = new MoveToStart();
            this.editactions[Keys.MButton | Keys.Space | Keys.Shift | Keys.Control] = new ShiftMoveToStart();
            this.editactions[Keys.End] = new End();
            this.editactions[Keys.LButton | Keys.RButton | Keys.Space | Keys.Shift] = new ShiftEnd();
            this.editactions[Keys.LButton | Keys.RButton | Keys.Space | Keys.Control] = new MoveToEnd();
            this.editactions[Keys.LButton | Keys.RButton | Keys.Space | Keys.Shift | Keys.Control] = new ShiftMoveToEnd();
            this.editactions[Keys.Prior] = new MovePageUp();
            this.editactions[Keys.LButton | Keys.Space | Keys.Shift] = new ShiftMovePageUp();
            this.editactions[Keys.Next] = new MovePageDown();
            this.editactions[Keys.RButton | Keys.Space | Keys.Shift] = new ShiftMovePageDown();
            this.editactions[Keys.Return] = new Return();
            this.editactions[Keys.Tab] = new Tab();
            this.editactions[Keys.LButton | Keys.Back | Keys.Shift] = new ShiftTab();
            this.editactions[Keys.Back] = new Backspace();
            this.editactions[Keys.Back | Keys.Shift] = new Backspace();
            this.editactions[(Keys)131160] = new Cut();
            this.editactions[(Keys)131139] = new Copy();
            this.editactions[(Keys)131158] = new Paste();
            this.editactions[(Keys)131137] = new SelectWholeDocument();
            this.editactions[Keys.Escape] = new ClearAllSelections();
            this.editactions[(Keys)131183] = new ToggleComment();
            this.editactions[Keys.LButton | Keys.RButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Control] = new ToggleComment();
            this.editactions[Keys.Back | Keys.Alt] = new LTP.TextEditor.Actions.Undo();
            this.editactions[(Keys)131162] = new LTP.TextEditor.Actions.Undo();
            this.editactions[(Keys)131161] = new Redo();
            this.editactions[Keys.RButton | Keys.MButton | Keys.Back | Keys.Space | Keys.Control] = new DeleteWord();
            this.editactions[Keys.Back | Keys.Control] = new WordBackspace();
            this.editactions[(Keys)131140] = new DeleteLine();
            this.editactions[(Keys)196676] = new DeleteToLineEnd();
            this.editactions[(Keys)131138] = new GotoMatchingBrace();
        }
        public virtual void BeginUpdate()
        {
            this.updateLevel++;
        }
        public virtual void EndUpdate()
        {
            this.updateLevel = Math.Max(0, this.updateLevel - 1);
        }
        public void LoadFile(string fileName)
        {
            this.LoadFile(fileName, true);
        }
        public void LoadFile(string fileName, bool autoLoadHighlighting)
        {
            this.BeginUpdate();
            this.document.TextContent = string.Empty;
            this.document.UndoStack.ClearAll();
            this.document.BookmarkManager.Clear();
            if (autoLoadHighlighting)
            {
                this.document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(fileName);
            }
            StreamReader streamReader;
            if (this.Encoding != null)
            {
                streamReader = new StreamReader(fileName, this.Encoding);
            }
            else
            {
                streamReader = new StreamReader(fileName);
            }
            this.Document.TextContent = streamReader.ReadToEnd();
            streamReader.Close();
            this.FileName = fileName;
            this.OptionsChanged();
            this.Document.UpdateQueue.Clear();
            this.EndUpdate();
            this.Refresh();
        }
        public void SaveFile(string fileName)
        {
            if (this.document.TextEditorProperties.CreateBackupCopy)
            {
                this.MakeBackupCopy(fileName);
            }
            StreamWriter streamWriter;
            if (this.Encoding != null && this.Encoding.CodePage != 65001)
            {
                streamWriter = new StreamWriter(fileName, false, this.Encoding);
            }
            else
            {
                streamWriter = new StreamWriter(fileName, false);
            }
            foreach (LineSegment lineSegment in this.Document.LineSegmentCollection)
            {
                streamWriter.Write(this.Document.GetText(lineSegment.Offset, lineSegment.Length));
                streamWriter.Write(this.document.TextEditorProperties.LineTerminator);
            }
            streamWriter.Close();
            this.FileName = fileName;
        }
        private void MakeBackupCopy(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string text = fileName + ".bak";
                    if (File.Exists(text))
                    {
                        File.Delete(text);
                    }
                    File.Copy(fileName, text);
                }
            }
            catch (Exception)
            {
            }
        }
        public abstract void OptionsChanged();
        public virtual string GetRangeDescription(int selectedItem, int itemCount)
        {
            StringBuilder stringBuilder = new StringBuilder(selectedItem.ToString());
            stringBuilder.Append(" from ");
            stringBuilder.Append(itemCount.ToString());
            return stringBuilder.ToString();
        }
        public override void Refresh()
        {
            if (this.IsUpdating)
            {
                return;
            }
            base.Refresh();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                HighlightingManager.Manager.ReloadSyntaxHighlighting -= new EventHandler(this.ReloadHighlighting);
                this.document.HighlightingStrategy = null;
                this.document.UndoStack.TextEditorControl = null;
            }
            base.Dispose(disposing);
        }
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (this.FileNameChanged != null)
            {
                this.FileNameChanged(this, e);
            }
        }
        protected virtual void OnChanged(EventArgs e)
        {
            if (this.Changed != null)
            {
                this.Changed(this, e);
            }
        }
    }
}
