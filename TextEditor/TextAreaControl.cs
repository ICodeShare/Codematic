using LTP.TextEditor.Document;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	[ToolboxItem(false)]
	public class TextAreaControl : Panel
	{
		private TextEditorControl motherTextEditorControl;
		private HRuler hRuler;
		private VScrollBar vScrollBar = new VScrollBar();
		private HScrollBar hScrollBar = new HScrollBar();
		private TextArea textArea;
		private bool doHandleMousewheel = true;
		private int scrollMarginHeight = 3;
		public TextArea TextArea
		{
			get
			{
				return this.textArea;
			}
		}
		public SelectionManager SelectionManager
		{
			get
			{
				return this.textArea.SelectionManager;
			}
		}
		public Caret Caret
		{
			get
			{
				return this.textArea.Caret;
			}
		}
		[Browsable(false)]
		public IDocument Document
		{
			get
			{
				if (this.motherTextEditorControl != null)
				{
					return this.motherTextEditorControl.Document;
				}
				return null;
			}
		}
		public ITextEditorProperties TextEditorProperties
		{
			get
			{
				if (this.motherTextEditorControl != null)
				{
					return this.motherTextEditorControl.TextEditorProperties;
				}
				return null;
			}
		}
		public VScrollBar VScrollBar
		{
			get
			{
				return this.vScrollBar;
			}
		}
		public HScrollBar HScrollBar
		{
			get
			{
				return this.hScrollBar;
			}
		}
		public bool DoHandleMousewheel
		{
			get
			{
				return this.doHandleMousewheel;
			}
			set
			{
				this.doHandleMousewheel = value;
			}
		}
		public TextAreaControl(TextEditorControl motherTextEditorControl)
		{
			this.motherTextEditorControl = motherTextEditorControl;
			this.textArea = new TextArea(motherTextEditorControl, this);
			base.Controls.Add(this.textArea);
			this.vScrollBar.ValueChanged += new EventHandler(this.VScrollBarValueChanged);
			base.Controls.Add(this.vScrollBar);
			this.hScrollBar.ValueChanged += new EventHandler(this.HScrollBarValueChanged);
			base.Controls.Add(this.hScrollBar);
			base.ResizeRedraw = true;
			this.Document.DocumentChanged += new DocumentEventHandler(this.AdjustScrollBars);
			base.SetStyle(ControlStyles.Selectable, true);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Document.DocumentChanged -= new DocumentEventHandler(this.AdjustScrollBars);
				this.motherTextEditorControl = null;
				if (this.vScrollBar != null)
				{
					this.vScrollBar.Dispose();
					this.vScrollBar = null;
				}
				if (this.hScrollBar != null)
				{
					this.hScrollBar.Dispose();
					this.hScrollBar = null;
				}
				if (this.hRuler != null)
				{
					this.hRuler.Dispose();
					this.hRuler = null;
				}
			}
			base.Dispose(disposing);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.ResizeTextArea();
		}
		public void ResizeTextArea()
		{
			int y = 0;
			int num = 0;
			if (this.hRuler != null)
			{
				this.hRuler.Bounds = new Rectangle(0, 0, base.Width - SystemInformation.HorizontalScrollBarArrowWidth, this.textArea.TextView.FontHeight);
				y = this.hRuler.Bounds.Bottom;
				num = this.hRuler.Bounds.Height;
			}
			this.textArea.Bounds = new Rectangle(0, y, base.Width - SystemInformation.HorizontalScrollBarArrowWidth, base.Height - SystemInformation.VerticalScrollBarArrowHeight - num);
			this.SetScrollBarBounds();
		}
		public void SetScrollBarBounds()
		{
			this.vScrollBar.Bounds = new Rectangle(this.textArea.Bounds.Right, 0, SystemInformation.HorizontalScrollBarArrowWidth, base.Height - SystemInformation.VerticalScrollBarArrowHeight);
			this.hScrollBar.Bounds = new Rectangle(0, this.textArea.Bounds.Bottom, base.Width - SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.VerticalScrollBarArrowHeight);
		}
		public void AdjustScrollBars(object sender, DocumentEventArgs e)
		{
			this.vScrollBar.Minimum = 0;
			this.vScrollBar.Maximum = this.textArea.MaxVScrollValue;
			int num = 0;
			foreach (ISegment segment in this.Document.LineSegmentCollection)
			{
				if (this.Document.FoldingManager.IsLineVisible(this.Document.GetLineNumberForOffset(segment.Offset)))
				{
					num = Math.Max(segment.Length, num);
				}
			}
			this.hScrollBar.Minimum = 0;
			this.hScrollBar.Maximum = Math.Max(0, num + this.textArea.TextView.VisibleColumnCount - 1);
			this.vScrollBar.LargeChange = Math.Max(0, this.textArea.TextView.DrawingPosition.Height);
			this.vScrollBar.SmallChange = Math.Max(0, this.textArea.TextView.FontHeight);
			this.hScrollBar.LargeChange = Math.Max(0, this.textArea.TextView.VisibleColumnCount - 1);
			this.hScrollBar.SmallChange = Math.Max(0, (int)this.textArea.TextView.GetWidth(' '));
		}
		public void OptionsChanged()
		{
			this.textArea.OptionsChanged();
			if (this.textArea.TextEditorProperties.ShowHorizontalRuler)
			{
				if (this.hRuler == null)
				{
					this.hRuler = new HRuler(this.textArea);
					base.Controls.Add(this.hRuler);
					this.ResizeTextArea();
				}
			}
			else
			{
				if (this.hRuler != null)
				{
					base.Controls.Remove(this.hRuler);
					this.hRuler.Dispose();
					this.hRuler = null;
					this.ResizeTextArea();
				}
			}
			this.AdjustScrollBars(null, null);
		}
		private void VScrollBarValueChanged(object sender, EventArgs e)
		{
			this.textArea.VirtualTop = new Point(this.textArea.VirtualTop.X, this.vScrollBar.Value);
			this.textArea.Invalidate();
		}
		private void HScrollBarValueChanged(object sender, EventArgs e)
		{
			this.textArea.VirtualTop = new Point(this.hScrollBar.Value, this.textArea.VirtualTop.Y);
			this.textArea.Invalidate();
		}
		public void HandleMouseWheel(MouseEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.None || !this.TextEditorProperties.MouseWheelTextZoom)
			{
				int num = 120;
				int num2 = Math.Abs(e.Delta) / num;
				int val;
				if (SystemInformation.MouseWheelScrollLines > 0)
				{
					val = this.vScrollBar.Value - (this.TextEditorProperties.MouseWheelScrollDown ? 1 : -1) * Math.Sign(e.Delta) * SystemInformation.MouseWheelScrollLines * this.vScrollBar.SmallChange * num2;
				}
				else
				{
					val = this.vScrollBar.Value - (this.TextEditorProperties.MouseWheelScrollDown ? 1 : -1) * Math.Sign(e.Delta) * this.vScrollBar.LargeChange;
				}
				this.vScrollBar.Value = Math.Max(this.vScrollBar.Minimum, Math.Min(this.vScrollBar.Maximum, val));
				return;
			}
			if (e.Delta > 0)
			{
				this.motherTextEditorControl.Font = new Font(this.motherTextEditorControl.Font.Name, this.motherTextEditorControl.Font.Size + 1f);
				return;
			}
			this.motherTextEditorControl.Font = new Font(this.motherTextEditorControl.Font.Name, Math.Max(6f, this.motherTextEditorControl.Font.Size - 1f));
		}
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (this.DoHandleMousewheel)
			{
				this.HandleMouseWheel(e);
			}
		}
		public void ScrollToCaret()
		{
			int num = this.hScrollBar.Value - this.hScrollBar.Minimum;
			int num2 = num + this.textArea.TextView.VisibleColumnCount;
			int visualColumn = this.textArea.TextView.GetVisualColumn(this.textArea.Caret.Line, this.textArea.Caret.Column);
			if (this.textArea.TextView.VisibleColumnCount < 0)
			{
				this.hScrollBar.Value = 0;
			}
			else
			{
				if (visualColumn < num)
				{
					this.hScrollBar.Value = Math.Max(0, visualColumn - this.scrollMarginHeight);
				}
				else
				{
					if (visualColumn > num2)
					{
						this.hScrollBar.Value = Math.Max(0, Math.Min(this.hScrollBar.Maximum, visualColumn - this.textArea.TextView.VisibleColumnCount + this.scrollMarginHeight));
					}
				}
			}
			this.ScrollTo(this.textArea.Caret.Line);
		}
		public void ScrollTo(int line)
		{
			line = Math.Max(0, Math.Min(this.Document.TotalNumberOfLines - 1, line));
			line = this.Document.GetVisibleLine(line);
			int num = this.textArea.TextView.FirstPhysicalLine;
			if (this.textArea.TextView.LineHeightRemainder > 0)
			{
				num++;
			}
			if (line - this.scrollMarginHeight + 3 < num)
			{
				this.vScrollBar.Value = Math.Max(0, Math.Min(this.vScrollBar.Maximum, (line - this.scrollMarginHeight + 3) * this.textArea.TextView.FontHeight));
				this.VScrollBarValueChanged(this, EventArgs.Empty);
				return;
			}
			int num2 = num + this.textArea.TextView.VisibleLineCount;
			if (line + this.scrollMarginHeight - 1 > num2)
			{
				if (this.textArea.TextView.VisibleLineCount == 1)
				{
					this.vScrollBar.Value = Math.Max(0, Math.Min(this.vScrollBar.Maximum, (line - this.scrollMarginHeight - 1) * this.textArea.TextView.FontHeight));
				}
				else
				{
					this.vScrollBar.Value = Math.Min(this.vScrollBar.Maximum, (line - this.textArea.TextView.VisibleLineCount + this.scrollMarginHeight - 1) * this.textArea.TextView.FontHeight);
				}
				this.VScrollBarValueChanged(this, EventArgs.Empty);
			}
		}
		public void JumpTo(int line, int column)
		{
			this.textArea.Focus();
			this.textArea.SelectionManager.ClearSelection();
			this.textArea.Caret.Position = new Point(column, line);
			this.textArea.SetDesiredColumn();
			this.ScrollToCaret();
		}
	}
}
