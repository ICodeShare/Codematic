using System;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public class CodeCompletionWindow : AbstractCompletionWindow
	{
		private static ICompletionData[] completionData;
		private CodeCompletionListView codeCompletionListView;
		private VScrollBar vScrollBar = new VScrollBar();
		private int startOffset;
		private int endOffset;
		private DeclarationViewWindow declarationViewWindow;
		private Rectangle workingScreen;
		public static CodeCompletionWindow ShowCompletionWindow(Form parent, TextEditorControl control, string fileName, ICompletionDataProvider completionDataProvider, char firstChar)
		{
			CodeCompletionWindow.completionData = completionDataProvider.GenerateCompletionData(fileName, control.ActiveTextAreaControl.TextArea, firstChar);
			if (CodeCompletionWindow.completionData == null || CodeCompletionWindow.completionData.Length == 0)
			{
				return null;
			}
			CodeCompletionWindow codeCompletionWindow = new CodeCompletionWindow(completionDataProvider, parent, control, fileName);
			codeCompletionWindow.ShowCompletionWindow();
			return codeCompletionWindow;
		}
		private CodeCompletionWindow(ICompletionDataProvider completionDataProvider, Form parentForm, TextEditorControl control, string fileName) : base(parentForm, control, fileName)
		{
			this.workingScreen = Screen.GetWorkingArea(base.Location);
			this.startOffset = control.ActiveTextAreaControl.Caret.Offset + 1;
			this.endOffset = this.startOffset;
			if (completionDataProvider.PreSelection != null)
			{
				this.startOffset -= completionDataProvider.PreSelection.Length + 1;
				this.endOffset--;
			}
			this.codeCompletionListView = new CodeCompletionListView(CodeCompletionWindow.completionData);
			this.codeCompletionListView.ImageList = completionDataProvider.ImageList;
			this.codeCompletionListView.Dock = DockStyle.Fill;
			this.codeCompletionListView.SelectedItemChanged += new EventHandler(this.CodeCompletionListViewSelectedItemChanged);
			this.codeCompletionListView.DoubleClick += new EventHandler(this.CodeCompletionListViewDoubleClick);
			this.codeCompletionListView.Click += new EventHandler(this.CodeCompletionListViewClick);
			base.Controls.Add(this.codeCompletionListView);
			if (CodeCompletionWindow.completionData.Length > 10)
			{
				this.vScrollBar.Dock = DockStyle.Right;
				this.vScrollBar.Minimum = 0;
				this.vScrollBar.Maximum = CodeCompletionWindow.completionData.Length - 8;
				this.vScrollBar.SmallChange = 1;
				this.vScrollBar.LargeChange = 3;
				this.codeCompletionListView.FirstItemChanged += new EventHandler(this.CodeCompletionListViewFirstItemChanged);
				base.Controls.Add(this.vScrollBar);
			}
			this.drawingSize = new Size(this.codeCompletionListView.ItemHeight * 10, this.codeCompletionListView.ItemHeight * Math.Min(10, CodeCompletionWindow.completionData.Length));
			this.SetLocation();
			this.declarationViewWindow = new DeclarationViewWindow(parentForm);
			this.SetDeclarationViewLocation();
			this.declarationViewWindow.ShowDeclarationViewWindow();
			control.Focus();
			this.CodeCompletionListViewSelectedItemChanged(this, EventArgs.Empty);
			if (completionDataProvider.PreSelection != null)
			{
				this.CaretOffsetChanged(this, EventArgs.Empty);
			}
			this.vScrollBar.Scroll += new ScrollEventHandler(this.DoScroll);
		}
		public void HandleMouseWheel(MouseEventArgs e)
		{
			int num = 120;
			int num2 = Math.Abs(e.Delta) / num;
			int val;
			if (SystemInformation.MouseWheelScrollLines > 0)
			{
				val = this.vScrollBar.Value - (this.control.TextEditorProperties.MouseWheelScrollDown ? 1 : -1) * Math.Sign(e.Delta) * SystemInformation.MouseWheelScrollLines * this.vScrollBar.SmallChange * num2;
			}
			else
			{
				val = this.vScrollBar.Value - (this.control.TextEditorProperties.MouseWheelScrollDown ? 1 : -1) * Math.Sign(e.Delta) * this.vScrollBar.LargeChange;
			}
			this.vScrollBar.Value = Math.Max(this.vScrollBar.Minimum, Math.Min(this.vScrollBar.Maximum, val));
			this.DoScroll(this, null);
		}
		private void CodeCompletionListViewFirstItemChanged(object sender, EventArgs e)
		{
			this.vScrollBar.Value = Math.Min(this.vScrollBar.Maximum, this.codeCompletionListView.FirstItem);
		}
		private void SetDeclarationViewLocation()
		{
			Console.WriteLine("SET DECLARATION VIEW LOCATION.");
			int num = base.Bounds.Left - this.workingScreen.Left;
			int num2 = this.workingScreen.Right - base.Bounds.Right;
			Point point;
			if (num2 * 2 > num)
			{
				point = new Point(base.Bounds.Right, base.Bounds.Top);
			}
			else
			{
				point = new Point(base.Bounds.Left - this.declarationViewWindow.Width, base.Bounds.Top);
			}
			if (this.declarationViewWindow.Location != point)
			{
				this.declarationViewWindow.Location = point;
			}
		}
		protected override void SetLocation()
		{
			base.SetLocation();
			if (this.declarationViewWindow != null)
			{
				this.SetDeclarationViewLocation();
			}
		}
		private void CodeCompletionListViewSelectedItemChanged(object sender, EventArgs e)
		{
			ICompletionData selectedCompletionData = this.codeCompletionListView.SelectedCompletionData;
			if (selectedCompletionData != null && selectedCompletionData.Description != null)
			{
				this.declarationViewWindow.Description = selectedCompletionData.Description;
				this.SetDeclarationViewLocation();
				return;
			}
			this.declarationViewWindow.Size = new Size(0, 0);
		}
		public override bool ProcessKeyEvent(char ch)
		{
			if (!char.IsLetterOrDigit(ch) && ch != '_')
			{
				this.InsertSelectedItem();
				return false;
			}
			this.endOffset++;
			return base.ProcessKeyEvent(ch);
		}
		protected override void CaretOffsetChanged(object sender, EventArgs e)
		{
			int offset = this.control.ActiveTextAreaControl.Caret.Offset;
			if (offset < this.startOffset || offset > this.endOffset)
			{
				base.Close();
				return;
			}
			this.codeCompletionListView.SelectItemWithStart(this.control.Document.GetText(this.startOffset, offset - this.startOffset));
		}
		protected void DoScroll(object sender, ScrollEventArgs sea)
		{
			this.codeCompletionListView.FirstItem = this.vScrollBar.Value;
			this.codeCompletionListView.Refresh();
			this.control.ActiveTextAreaControl.TextArea.Focus();
		}
		protected override bool ProcessTextAreaKey(Keys keyData)
		{
			if (!base.Visible)
			{
				return false;
			}
			switch (keyData)
			{
			case Keys.Back:
				this.endOffset--;
				if (this.endOffset < this.startOffset)
				{
					base.Close();
				}
				return false;
			case Keys.Tab:
				break;
			default:
				if (keyData != Keys.Return)
				{
					switch (keyData)
					{
					case Keys.Prior:
						this.codeCompletionListView.PageUp();
						return true;
					case Keys.Next:
						this.codeCompletionListView.PageDown();
						return true;
					case Keys.End:
						this.codeCompletionListView.SelectIndex(CodeCompletionWindow.completionData.Length - 1);
						return true;
					case Keys.Home:
						this.codeCompletionListView.SelectIndex(0);
						return true;
					case Keys.Left:
					case Keys.Up:
						this.codeCompletionListView.SelectPrevItem();
						return true;
					case Keys.Right:
					case Keys.Down:
						this.codeCompletionListView.SelectNextItem();
						return true;
					case Keys.Delete:
						if (this.control.ActiveTextAreaControl.Caret.Offset <= this.endOffset)
						{
							this.endOffset--;
						}
						if (this.endOffset < this.startOffset)
						{
							base.Close();
						}
						return false;
					}
					return base.ProcessTextAreaKey(keyData);
				}
				break;
			}
			this.InsertSelectedItem();
			return true;
		}
		private void CodeCompletionListViewDoubleClick(object sender, EventArgs e)
		{
			this.InsertSelectedItem();
		}
		private void CodeCompletionListViewClick(object sender, EventArgs e)
		{
			this.control.ActiveTextAreaControl.TextArea.Focus();
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			base.Dispose();
			this.codeCompletionListView.Dispose();
			this.codeCompletionListView = null;
			this.declarationViewWindow.Dispose();
			this.declarationViewWindow = null;
		}
		private void InsertSelectedItem()
		{
			ICompletionData selectedCompletionData = this.codeCompletionListView.SelectedCompletionData;
			if (selectedCompletionData != null)
			{
				this.control.BeginUpdate();
				if (this.endOffset - this.startOffset > 0)
				{
					this.control.Document.Remove(this.startOffset, this.endOffset - this.startOffset);
					this.control.ActiveTextAreaControl.Caret.Position = this.control.Document.OffsetToPosition(this.startOffset);
				}
				selectedCompletionData.InsertAction(this.control);
				this.control.EndUpdate();
			}
			base.Close();
		}
	}
}
