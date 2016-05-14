using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace LTP.TextEditor
{
	public class IconBarMargin : AbstractMargin
	{
		public override Size Size
		{
			get
			{
				return new Size((int)((float)this.textArea.TextView.FontHeight * 1.2f), -1);
			}
		}
		public override bool IsVisible
		{
			get
			{
				return this.textArea.TextEditorProperties.IsIconBarVisible;
			}
		}
		public IconBarMargin(TextArea textArea) : base(textArea)
		{
		}
		public override void Paint(Graphics g, Rectangle rect)
		{
			if (rect.Width <= 0 || rect.Height <= 0)
			{
				return;
			}
			g.FillRectangle(SystemBrushes.Control, new Rectangle(this.drawingPosition.X, rect.Top, this.drawingPosition.Width - 1, rect.Height));
			g.DrawLine(SystemPens.ControlDark, this.drawingPosition.Right - 1, rect.Top, this.drawingPosition.Right - 1, rect.Bottom);
			foreach (int lineNumber in this.textArea.Document.BookmarkManager.Marks)
			{
				int visibleLine = this.textArea.Document.GetVisibleLine(lineNumber);
				int num = visibleLine * this.textArea.TextView.FontHeight - this.textArea.VirtualTop.Y;
				if (num >= rect.Y && num <= rect.Bottom)
				{
					this.DrawBookmark(g, num);
				}
			}
			base.Paint(g, rect);
		}
		public void DrawBreakpoint(Graphics g, int y, bool isEnabled)
		{
			int num = this.textArea.TextView.FontHeight / 8;
			Rectangle rect = new Rectangle(1 + num, y + num, this.textArea.TextView.FontHeight - 2 * num, this.textArea.TextView.FontHeight - 2 * num);
			g.FillEllipse(isEnabled ? Brushes.Firebrick : Brushes.Beige, rect);
			g.DrawEllipse(isEnabled ? Pens.Black : Pens.DarkGray, rect);
		}
		public void DrawBookmark(Graphics g, int y)
		{
			int num = this.textArea.TextView.FontHeight / 8;
			Rectangle r = new Rectangle(1, y + num, this.drawingPosition.Width - 4, this.textArea.TextView.FontHeight - num * 2);
			this.FillRoundRect(g, Brushes.Cyan, r);
			this.DrawRoundRect(g, Pens.Black, r);
		}
		public void DrawArrow(Graphics g, int y)
		{
			int num = this.textArea.TextView.FontHeight / 8;
			Rectangle r = new Rectangle(1, y + num, this.drawingPosition.Width - 4, this.textArea.TextView.FontHeight - num * 2);
			this.FillArrow(g, Brushes.Yellow, r);
			this.DrawArrow(g, Pens.Black, r);
		}
		private GraphicsPath CreateArrowGraphicsPath(Rectangle r)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = r.Width / 2;
			int num2 = r.Height / 2;
			graphicsPath.AddLine(r.X, r.Y + num2 / 2, r.X + num, r.Y + num2 / 2);
			graphicsPath.AddLine(r.X + num, r.Y + num2 / 2, r.X + num, r.Y);
			graphicsPath.AddLine(r.X + num, r.Y, r.Right, r.Y + num2);
			graphicsPath.AddLine(r.Right, r.Y + num2, r.X + num, r.Bottom);
			graphicsPath.AddLine(r.X + num, r.Bottom, r.X + num, r.Bottom - num2 / 2);
			graphicsPath.AddLine(r.X + num, r.Bottom - num2 / 2, r.X, r.Bottom - num2 / 2);
			graphicsPath.AddLine(r.X, r.Bottom - num2 / 2, r.X, r.Y + num2 / 2);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}
		private GraphicsPath CreateRoundRectGraphicsPath(Rectangle r)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			int num = r.Width / 2;
			graphicsPath.AddLine(r.X + num, r.Y, r.Right - num, r.Y);
			graphicsPath.AddArc(r.Right - num, r.Y, num, num, 270f, 90f);
			graphicsPath.AddLine(r.Right, r.Y + num, r.Right, r.Bottom - num);
			graphicsPath.AddArc(r.Right - num, r.Bottom - num, num, num, 0f, 90f);
			graphicsPath.AddLine(r.Right - num, r.Bottom, r.X + num, r.Bottom);
			graphicsPath.AddArc(r.X, r.Bottom - num, num, num, 90f, 90f);
			graphicsPath.AddLine(r.X, r.Bottom - num, r.X, r.Y + num);
			graphicsPath.AddArc(r.X, r.Y, num, num, 180f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}
		private void DrawRoundRect(Graphics g, Pen p, Rectangle r)
		{
			using (GraphicsPath graphicsPath = this.CreateRoundRectGraphicsPath(r))
			{
				g.DrawPath(p, graphicsPath);
			}
		}
		private void FillRoundRect(Graphics g, Brush b, Rectangle r)
		{
			using (GraphicsPath graphicsPath = this.CreateRoundRectGraphicsPath(r))
			{
				g.FillPath(b, graphicsPath);
			}
		}
		private void DrawArrow(Graphics g, Pen p, Rectangle r)
		{
			using (GraphicsPath graphicsPath = this.CreateArrowGraphicsPath(r))
			{
				g.DrawPath(p, graphicsPath);
			}
		}
		private void FillArrow(Graphics g, Brush b, Rectangle r)
		{
			using (GraphicsPath graphicsPath = this.CreateArrowGraphicsPath(r))
			{
				g.FillPath(b, graphicsPath);
			}
		}
	}
}
