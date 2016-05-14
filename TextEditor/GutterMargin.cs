using LTP.TextEditor.Document;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class GutterMargin : AbstractMargin
	{
		private StringFormat numberStringFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
		public static Cursor RightLeftCursor;
		private Point selectionStartPos;
		private bool selectionComeFromGutter;
		private bool selectionGutterDirectionDown;
		public override Cursor Cursor
		{
			get
			{
				return GutterMargin.RightLeftCursor;
			}
		}
		public override Size Size
		{
			get
			{
				return new Size((int)(this.textArea.TextView.GetWidth('8') * (float)Math.Max(3, (int)Math.Log10((double)this.textArea.Document.TotalNumberOfLines) + 1)), -1);
			}
		}
		public override bool IsVisible
		{
			get
			{
				return this.textArea.TextEditorProperties.ShowLineNumbers;
			}
		}
		static GutterMargin()
		{
			Stream baseStream = new StreamReader(Application.StartupPath + "\\TextStyle\\RightArrow.cur", Encoding.Default).BaseStream;
			GutterMargin.RightLeftCursor = new Cursor(baseStream);
			baseStream.Close();
		}
		public void Dispose()
		{
			this.numberStringFormat.Dispose();
		}
		public GutterMargin(TextArea textArea) : base(textArea)
		{
			this.numberStringFormat.LineAlignment = StringAlignment.Far;
			this.numberStringFormat.FormatFlags = (StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
		}
		public override void Paint(Graphics g, Rectangle rect)
		{
			if (rect.Width <= 0 || rect.Height <= 0)
			{
				return;
			}
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("LineNumbers");
			int fontHeight = this.textArea.TextView.FontHeight;
			Brush brush = this.textArea.Enabled ? BrushRegistry.GetBrush(colorFor.BackgroundColor) : SystemBrushes.InactiveBorder;
			Brush brush2 = BrushRegistry.GetBrush(colorFor.Color);
			for (int i = 0; i < (base.DrawingPosition.Height + this.textArea.TextView.VisibleLineDrawingRemainder) / fontHeight + 1; i++)
			{
				int y = this.drawingPosition.Y + fontHeight * i - this.textArea.TextView.VisibleLineDrawingRemainder;
				Rectangle rectangle = new Rectangle(this.drawingPosition.X, y, this.drawingPosition.Width, fontHeight);
				if (rect.IntersectsWith(rectangle))
				{
					g.FillRectangle(brush, rectangle);
					int firstLogicalLine = this.textArea.Document.GetFirstLogicalLine(this.textArea.Document.GetVisibleLine(this.textArea.TextView.FirstVisibleLine) + i);
					if (firstLogicalLine < this.textArea.Document.TotalNumberOfLines)
					{
						g.DrawString((firstLogicalLine + 1).ToString(), colorFor.Font, brush2, rectangle, this.numberStringFormat);
					}
				}
			}
		}
		public override void HandleMouseDown(Point mousepos, MouseButtons mouseButtons)
		{
			this.selectionComeFromGutter = true;
			int logicalLine = this.textArea.TextView.GetLogicalLine(mousepos);
			if (logicalLine >= 0 && logicalLine < this.textArea.Document.TotalNumberOfLines)
			{
				if ((Control.ModifierKeys & Keys.Shift) != Keys.None && this.textArea.SelectionManager.HasSomethingSelected)
				{
					this.HandleMouseMove(mousepos, mouseButtons);
					return;
				}
				this.selectionGutterDirectionDown = false;
				this.selectionStartPos = new Point(0, logicalLine);
				this.textArea.SelectionManager.ClearSelection();
				this.textArea.SelectionManager.SetSelection(new DefaultSelection(this.textArea.Document, this.selectionStartPos, new Point(this.textArea.Document.GetLineSegment(logicalLine).Length + 1, logicalLine)));
				this.textArea.Caret.Position = this.selectionStartPos;
			}
		}
		public override void HandleMouseLeave(EventArgs e)
		{
			this.selectionComeFromGutter = false;
		}
		public override void HandleMouseMove(Point mousepos, MouseButtons mouseButtons)
		{
			if (mouseButtons == MouseButtons.Left)
			{
				if (this.selectionComeFromGutter)
				{
					int logicalLine = this.textArea.TextView.GetLogicalLine(mousepos);
					Point point = new Point(0, logicalLine);
					if (point.Y < this.textArea.Document.TotalNumberOfLines)
					{
						if (this.selectionStartPos.Y == point.Y)
						{
							this.textArea.SelectionManager.SetSelection(new DefaultSelection(this.textArea.Document, point, new Point(this.textArea.Document.GetLineSegment(point.Y).Length + 1, point.Y)));
							this.selectionGutterDirectionDown = false;
						}
						else
						{
							if (this.selectionStartPos.Y < point.Y && this.textArea.SelectionManager.HasSomethingSelected)
							{
								if (!this.selectionGutterDirectionDown)
								{
									this.selectionGutterDirectionDown = true;
									this.textArea.SelectionManager.SetSelection(new DefaultSelection(this.textArea.Document, this.selectionStartPos, new Point(0, this.selectionStartPos.Y)));
									this.textArea.SelectionManager.ExtendSelection(this.textArea.SelectionManager.SelectionCollection[0].EndPosition, new Point(this.textArea.Document.GetLineSegment(point.Y).Length + 1, point.Y));
								}
								else
								{
									this.textArea.SelectionManager.ExtendSelection(this.textArea.SelectionManager.SelectionCollection[0].EndPosition, new Point(this.textArea.Document.GetLineSegment(point.Y).Length + 1, point.Y));
								}
							}
							else
							{
								if (this.textArea.SelectionManager.HasSomethingSelected)
								{
									if (this.selectionGutterDirectionDown)
									{
										this.selectionGutterDirectionDown = false;
										this.textArea.SelectionManager.SetSelection(new DefaultSelection(this.textArea.Document, this.selectionStartPos, new Point(this.textArea.Document.GetLineSegment(this.selectionStartPos.Y).Length + 1, this.selectionStartPos.Y)));
										this.textArea.SelectionManager.ExtendSelection(this.selectionStartPos, point);
									}
									else
									{
										this.textArea.SelectionManager.ExtendSelection(this.textArea.Caret.Position, point);
									}
								}
							}
						}
						this.textArea.Caret.Position = point;
						return;
					}
				}
				else
				{
					if (this.textArea.SelectionManager.HasSomethingSelected)
					{
						this.selectionStartPos = this.textArea.Document.OffsetToPosition(this.textArea.SelectionManager.SelectionCollection[0].Offset);
						int logicalLine2 = this.textArea.TextView.GetLogicalLine(mousepos);
						Point point2 = new Point(0, logicalLine2);
						if (point2.Y < this.textArea.Document.TotalNumberOfLines)
						{
							this.textArea.SelectionManager.ExtendSelection(this.textArea.Caret.Position, point2);
						}
						this.textArea.Caret.Position = point2;
					}
				}
			}
		}
	}
}
