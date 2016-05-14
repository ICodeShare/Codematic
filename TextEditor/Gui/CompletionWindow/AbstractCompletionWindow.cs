using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public abstract class AbstractCompletionWindow : Form
	{
		protected TextEditorControl control;
		protected string fileName;
		protected Size drawingSize;
		private Form parentForm;
		private Rectangle workingScreen;
		public static readonly int SW_SHOWNA = 8;
		protected AbstractCompletionWindow(Form parentForm, TextEditorControl control, string fileName)
		{
			this.workingScreen = Screen.GetWorkingArea(parentForm);
			this.parentForm = parentForm;
			this.control = control;
			this.fileName = fileName;
			this.SetLocation();
			base.StartPosition = FormStartPosition.Manual;
			base.FormBorderStyle = FormBorderStyle.None;
			base.ShowInTaskbar = false;
			base.Size = new Size(1, 1);
		}
		protected virtual void SetLocation()
		{
			TextArea textArea = this.control.ActiveTextAreaControl.TextArea;
			Point position = textArea.Caret.Position;
			int drawingXPos = textArea.TextView.GetDrawingXPos(position.Y, position.X);
			int num = textArea.TextEditorProperties.ShowHorizontalRuler ? textArea.TextView.FontHeight : 0;
			Point p = new Point(textArea.TextView.DrawingPosition.X + drawingXPos, textArea.TextView.DrawingPosition.Y + textArea.Document.GetVisibleLine(position.Y) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y + textArea.TextView.FontHeight + num);
			Point location = this.control.ActiveTextAreaControl.PointToScreen(p);
			Rectangle rectangle = new Rectangle(location, this.drawingSize);
			if (!this.workingScreen.Contains(rectangle))
			{
				if (rectangle.Right > this.workingScreen.Right)
				{
					rectangle.X = this.workingScreen.Right - rectangle.Width;
				}
				if (rectangle.Left < this.workingScreen.Left)
				{
					rectangle.X = this.workingScreen.Left;
				}
				if (rectangle.Top < this.workingScreen.Top)
				{
					rectangle.Y = this.workingScreen.Top;
				}
				if (rectangle.Bottom > this.workingScreen.Bottom)
				{
					rectangle.Y = rectangle.Y - rectangle.Height - this.control.ActiveTextAreaControl.TextArea.TextView.FontHeight;
					if (rectangle.Bottom > this.workingScreen.Bottom)
					{
						rectangle.Y = this.workingScreen.Bottom - rectangle.Height;
					}
				}
			}
			base.Bounds = rectangle;
		}
		[DllImport("user32")]
		public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
		protected void ShowCompletionWindow()
		{
			base.Owner = this.parentForm;
			base.Enabled = true;
			AbstractCompletionWindow.ShowWindow(base.Handle, AbstractCompletionWindow.SW_SHOWNA);
			this.control.Focus();
			if (this.parentForm != null)
			{
				this.parentForm.LocationChanged += new EventHandler(this.ParentFormLocationChanged);
			}
			this.control.ActiveTextAreaControl.VScrollBar.ValueChanged += new EventHandler(this.ParentFormLocationChanged);
			this.control.ActiveTextAreaControl.HScrollBar.ValueChanged += new EventHandler(this.ParentFormLocationChanged);
			this.control.ActiveTextAreaControl.TextArea.DoProcessDialogKey += new DialogKeyProcessor(this.ProcessTextAreaKey);
			this.control.ActiveTextAreaControl.Caret.PositionChanged += new EventHandler(this.CaretOffsetChanged);
			this.control.ActiveTextAreaControl.TextArea.LostFocus += new EventHandler(this.TextEditorLostFocus);
			this.control.Resize += new EventHandler(this.ParentFormLocationChanged);
		}
		private void ParentFormLocationChanged(object sender, EventArgs e)
		{
			this.SetLocation();
		}
		public virtual bool ProcessKeyEvent(char ch)
		{
			return false;
		}
		protected virtual bool ProcessTextAreaKey(Keys keyData)
		{
			if (!base.Visible)
			{
				return false;
			}
			if (keyData == Keys.Escape)
			{
				base.Close();
				return true;
			}
			return false;
		}
		protected virtual void CaretOffsetChanged(object sender, EventArgs e)
		{
		}
		protected void TextEditorLostFocus(object sender, EventArgs e)
		{
			if (!this.control.ActiveTextAreaControl.TextArea.Focused && !base.ContainsFocus)
			{
				base.Close();
			}
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.parentForm.LocationChanged -= new EventHandler(this.ParentFormLocationChanged);
			this.control.ActiveTextAreaControl.VScrollBar.ValueChanged -= new EventHandler(this.ParentFormLocationChanged);
			this.control.ActiveTextAreaControl.HScrollBar.ValueChanged -= new EventHandler(this.ParentFormLocationChanged);
			this.control.ActiveTextAreaControl.TextArea.LostFocus -= new EventHandler(this.TextEditorLostFocus);
			this.control.ActiveTextAreaControl.Caret.PositionChanged -= new EventHandler(this.CaretOffsetChanged);
			this.control.ActiveTextAreaControl.TextArea.DoProcessDialogKey -= new DialogKeyProcessor(this.ProcessTextAreaKey);
			this.control.Resize -= new EventHandler(this.ParentFormLocationChanged);
			base.Dispose();
		}
	}
}
