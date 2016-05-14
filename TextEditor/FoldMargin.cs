using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class FoldMargin : AbstractMargin
	{
		private int selectedFoldLine = -1;
		public override Size Size
		{
			get
			{
				return new Size(this.textArea.TextView.FontHeight, -1);
			}
		}
		public override bool IsVisible
		{
			get
			{
				return this.textArea.TextEditorProperties.EnableFolding;
			}
		}
		public FoldMargin(TextArea textArea) : base(textArea)
		{
		}
		public override void Paint(Graphics g, Rectangle rect)
		{
			if (rect.Width <= 0 || rect.Height <= 0)
			{
				return;
			}
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("LineNumbers");
			this.textArea.Document.HighlightingStrategy.GetColorFor("FoldLine");
			for (int i = 0; i < (base.DrawingPosition.Height + this.textArea.TextView.VisibleLineDrawingRemainder) / this.textArea.TextView.FontHeight + 1; i++)
			{
				Rectangle rectangle = new Rectangle(base.DrawingPosition.X, base.DrawingPosition.Top + i * this.textArea.TextView.FontHeight - this.textArea.TextView.VisibleLineDrawingRemainder, base.DrawingPosition.Width, this.textArea.TextView.FontHeight);
				if (rect.IntersectsWith(rectangle))
				{
					if (this.textArea.Document.TextEditorProperties.ShowLineNumbers)
					{
						g.FillRectangle(BrushRegistry.GetBrush(this.textArea.Enabled ? colorFor.BackgroundColor : SystemColors.InactiveBorder), new Rectangle(rectangle.X + 1, rectangle.Y, rectangle.Width - 1, rectangle.Height));
						g.DrawLine(BrushRegistry.GetDotPen(colorFor.Color, colorFor.BackgroundColor), this.drawingPosition.X, rectangle.Y, this.drawingPosition.X, rectangle.Bottom);
					}
					else
					{
						g.FillRectangle(BrushRegistry.GetBrush(this.textArea.Enabled ? colorFor.BackgroundColor : SystemColors.InactiveBorder), rectangle);
					}
					int firstLogicalLine = this.textArea.Document.GetFirstLogicalLine(this.textArea.Document.GetVisibleLine(this.textArea.TextView.FirstVisibleLine) + i);
					this.PaintFoldMarker(g, firstLogicalLine, rectangle);
				}
			}
		}
		private bool SelectedFoldingFrom(ArrayList list)
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (this.selectedFoldLine == ((FoldMarker)list[i]).StartLine)
					{
						return true;
					}
				}
			}
			return false;
		}
		private void PaintFoldMarker(Graphics g, int lineNumber, Rectangle drawingRectangle)
		{
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("FoldLine");
			HighlightColor colorFor2 = this.textArea.Document.HighlightingStrategy.GetColorFor("SelectedFoldLine");
			ArrayList foldingsWithStart = this.textArea.Document.FoldingManager.GetFoldingsWithStart(lineNumber);
			ArrayList foldingsContainsLineNumber = this.textArea.Document.FoldingManager.GetFoldingsContainsLineNumber(lineNumber);
			ArrayList foldingsWithEnd = this.textArea.Document.FoldingManager.GetFoldingsWithEnd(lineNumber);
			bool flag = foldingsWithStart.Count > 0;
			bool flag2 = foldingsContainsLineNumber.Count > 0;
			bool flag3 = foldingsWithEnd.Count > 0;
			bool flag4 = this.SelectedFoldingFrom(foldingsWithStart);
			bool flag5 = this.SelectedFoldingFrom(foldingsContainsLineNumber);
			bool flag6 = this.SelectedFoldingFrom(foldingsWithEnd);
			int num = (int)Math.Round((double)((float)this.textArea.TextView.FontHeight * 0.57f));
			num -= num % 2;
			int num2 = drawingRectangle.Y + (drawingRectangle.Height - num) / 2;
			int num3 = drawingRectangle.X + (drawingRectangle.Width - num) / 2 + num / 2;
			if (flag)
			{
				bool flag7 = true;
				bool flag8 = false;
				foreach (FoldMarker foldMarker in foldingsWithStart)
				{
					if (foldMarker.IsFolded)
					{
						flag7 = false;
					}
					else
					{
						flag8 = (foldMarker.EndLine > foldMarker.StartLine);
					}
				}
				bool flag9 = false;
				foreach (FoldMarker foldMarker2 in foldingsWithEnd)
				{
					if (foldMarker2.EndLine > foldMarker2.StartLine && !foldMarker2.IsFolded)
					{
						flag9 = true;
					}
				}
				this.DrawFoldMarker(g, new RectangleF((float)(drawingRectangle.X + (drawingRectangle.Width - num) / 2), (float)num2, (float)num, (float)num), flag7, flag4);
				if (flag2 || flag9)
				{
					g.DrawLine(BrushRegistry.GetPen(flag5 ? colorFor2.Color : colorFor.Color), num3, drawingRectangle.Top, num3, num2 - 1);
				}
				if (flag2 || flag8)
				{
					g.DrawLine(BrushRegistry.GetPen((flag6 || (flag4 && flag7) || flag5) ? colorFor2.Color : colorFor.Color), num3, num2 + num + 1, num3, drawingRectangle.Bottom);
					return;
				}
			}
			else
			{
				if (flag3)
				{
					int num4 = drawingRectangle.Top + drawingRectangle.Height / 2;
					g.DrawLine(BrushRegistry.GetPen((flag5 || flag6) ? colorFor2.Color : colorFor.Color), num3, drawingRectangle.Top, num3, num4);
					g.DrawLine(BrushRegistry.GetPen((flag5 || flag6) ? colorFor2.Color : colorFor.Color), num3, num4, num3 + num / 2, num4);
					if (flag2)
					{
						g.DrawLine(BrushRegistry.GetPen(flag5 ? colorFor2.Color : colorFor.Color), num3, num4 + 1, num3, drawingRectangle.Bottom);
						return;
					}
				}
				else
				{
					if (flag2)
					{
						g.DrawLine(BrushRegistry.GetPen(flag5 ? colorFor2.Color : colorFor.Color), num3, drawingRectangle.Top, num3, drawingRectangle.Bottom);
					}
				}
			}
		}
		public override void HandleMouseMove(Point mousepos, MouseButtons mouseButtons)
		{
			bool enableFolding = this.textArea.Document.TextEditorProperties.EnableFolding;
			int lineNumber = (mousepos.Y + this.textArea.VirtualTop.Y) / this.textArea.TextView.FontHeight;
			int firstLogicalLine = this.textArea.Document.GetFirstLogicalLine(lineNumber);
			if (!enableFolding || firstLogicalLine < 0 || firstLogicalLine + 1 >= this.textArea.Document.TotalNumberOfLines)
			{
				return;
			}
			ArrayList foldingsWithStart = this.textArea.Document.FoldingManager.GetFoldingsWithStart(firstLogicalLine);
			int num = this.selectedFoldLine;
			if (foldingsWithStart.Count > 0)
			{
				this.selectedFoldLine = firstLogicalLine;
			}
			else
			{
				this.selectedFoldLine = -1;
			}
			if (num != this.selectedFoldLine)
			{
				this.textArea.Refresh(this);
			}
		}
		public override void HandleMouseDown(Point mousepos, MouseButtons mouseButtons)
		{
			bool enableFolding = this.textArea.Document.TextEditorProperties.EnableFolding;
			int lineNumber = (mousepos.Y + this.textArea.VirtualTop.Y) / this.textArea.TextView.FontHeight;
			int firstLogicalLine = this.textArea.Document.GetFirstLogicalLine(lineNumber);
			this.textArea.Focus();
			if (!enableFolding || firstLogicalLine < 0 || firstLogicalLine + 1 >= this.textArea.Document.TotalNumberOfLines)
			{
				return;
			}
			ArrayList foldingsWithStart = this.textArea.Document.FoldingManager.GetFoldingsWithStart(firstLogicalLine);
			foreach (FoldMarker foldMarker in foldingsWithStart)
			{
				foldMarker.IsFolded = !foldMarker.IsFolded;
			}
			this.textArea.MotherTextAreaControl.AdjustScrollBars(null, null);
			this.textArea.Refresh();
		}
		public override void HandleMouseLeave(EventArgs e)
		{
			if (this.selectedFoldLine != -1)
			{
				this.selectedFoldLine = -1;
				this.textArea.Refresh(this);
			}
		}
		private void DrawFoldMarker(Graphics g, RectangleF rectangle, bool isOpened, bool isSelected)
		{
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("FoldMarker");
			HighlightColor colorFor2 = this.textArea.Document.HighlightingStrategy.GetColorFor("FoldLine");
			HighlightColor colorFor3 = this.textArea.Document.HighlightingStrategy.GetColorFor("SelectedFoldLine");
			Rectangle rect = new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
			g.FillRectangle(BrushRegistry.GetBrush(colorFor.BackgroundColor), rect);
			g.DrawRectangle(BrushRegistry.GetPen(isSelected ? colorFor3.Color : colorFor.Color), rect);
			int num = (int)Math.Round((double)rectangle.Height / 8.0) + 1;
			int num2 = rect.Height / 2 + rect.Height % 2;
			g.DrawLine(BrushRegistry.GetPen(colorFor2.BackgroundColor), rectangle.X + (float)num, rectangle.Y + (float)num2, rectangle.X + rectangle.Width - (float)num, rectangle.Y + (float)num2);
			if (!isOpened)
			{
				g.DrawLine(BrushRegistry.GetPen(colorFor2.BackgroundColor), rectangle.X + (float)num2, rectangle.Y + (float)num, rectangle.X + (float)num2, rectangle.Y + rectangle.Height - (float)num);
			}
		}
	}
}
