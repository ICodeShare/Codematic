using LTP.TextEditor.Actions;
using LTP.TextEditor.Util;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class TextAreaClipboardHandler
	{
		private TextArea textArea;
        public event CopyTextEventHandler CopyText;
		public bool EnableCut
		{
			get
			{
				return this.textArea != null && this.textArea.SelectionManager != null && this.textArea.SelectionManager.HasSomethingSelected && this.textArea.EnableCutOrPaste;
			}
		}
		public bool EnableCopy
		{
			get
			{
				return this.textArea != null && this.textArea.SelectionManager != null && this.textArea.SelectionManager.HasSomethingSelected;
			}
		}
		public bool EnablePaste
		{
			get
			{
				bool result;
				try
				{
					if (!this.textArea.EnableCutOrPaste)
					{
						result = false;
					}
					else
					{
						IDataObject dataObject = Clipboard.GetDataObject();
						result = (dataObject != null && dataObject.GetDataPresent(DataFormats.Text));
					}
				}
				catch (Exception arg)
				{
					Console.WriteLine("Got exception while enablepaste : " + arg);
					result = false;
				}
				return result;
			}
		}
		public bool EnableDelete
		{
			get
			{
				return this.textArea != null && this.textArea.SelectionManager != null && this.textArea.SelectionManager.HasSomethingSelected && this.textArea.EnableCutOrPaste;
			}
		}
		public bool EnableSelectAll
		{
			get
			{
				return true;
			}
		}
		public TextAreaClipboardHandler(TextArea textArea)
		{
			this.textArea = textArea;
			textArea.SelectionManager.SelectionChanged += new EventHandler(this.DocumentSelectionChanged);
		}
		private void DocumentSelectionChanged(object sender, EventArgs e)
		{
		}
		private bool CopyTextToClipboard()
		{
			string selectedText = this.textArea.SelectionManager.SelectedText;
			if (selectedText.Length > 0)
			{
				for (int i = 0; i < 5; i++)
				{
					try
					{
						DataObject dataObject = new DataObject();
						dataObject.SetData(DataFormats.UnicodeText, true, selectedText);
						if (this.textArea.Document.HighlightingStrategy.Name != "Default")
						{
							dataObject.SetData(DataFormats.Rtf, RtfWriter.GenerateRtf(this.textArea));
						}
						this.OnCopyText(new CopyTextEventArgs(selectedText));
						Clipboard.SetDataObject(dataObject, true);
						return true;
					}
					catch (Exception arg)
					{
						Console.WriteLine("Got exception while Copy text to clipboard : " + arg);
					}
					Thread.Sleep(100);
				}
			}
			return false;
		}
		public void Cut(object sender, EventArgs e)
		{
			if (this.CopyTextToClipboard())
			{
				this.textArea.BeginUpdate();
				this.textArea.Caret.Position = this.textArea.SelectionManager.SelectionCollection[0].StartPosition;
				this.textArea.SelectionManager.RemoveSelectedText();
				this.textArea.EndUpdate();
			}
		}
		public void Copy(object sender, EventArgs e)
		{
			this.CopyTextToClipboard();
		}
		public void Paste(object sender, EventArgs e)
		{
			try
			{
				IDataObject dataObject = Clipboard.GetDataObject();
				if (dataObject.GetDataPresent(DataFormats.UnicodeText))
				{
					string text = (string)dataObject.GetData(DataFormats.UnicodeText);
					if (text.Length > 0)
					{
						int num = 0;
						if (this.textArea.SelectionManager.HasSomethingSelected)
						{
							this.Delete(sender, e);
							num++;
						}
						this.textArea.InsertString(text);
						if (num > 0)
						{
							this.textArea.Document.UndoStack.UndoLast(num + 1);
						}
					}
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Got exception while Paste : " + arg);
			}
		}
		public void Delete(object sender, EventArgs e)
		{
			new Delete().Execute(this.textArea);
		}
		public void SelectAll(object sender, EventArgs e)
		{
			new SelectWholeDocument().Execute(this.textArea);
		}
		protected virtual void OnCopyText(CopyTextEventArgs e)
		{
			if (this.CopyText != null)
			{
				this.CopyText(this, e);
			}
		}
	}
}
