using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
namespace LTP.TextEditor.Util
{
	internal sealed class TipPainter
	{
		private const float HorizontalBorder = 2f;
		private const float VerticalBorder = 1f;
		private static RectangleF workingArea = RectangleF.Empty;
		private TipPainter()
		{
		}
		public static Size GetTipSize(Control control, Graphics graphics, Font font, string description)
		{
			return TipPainter.GetTipSize(control, graphics, new TipText(graphics, font, description));
		}
		public static Size GetTipSize(Control control, Graphics graphics, TipSection tipData)
		{
			Size size = Size.Empty;
			SizeF sizeF = SizeF.Empty;
			if (TipPainter.workingArea == RectangleF.Empty)
			{
				Form form = control.FindForm();
				if (form.Owner != null)
				{
					form = form.Owner;
				}
				TipPainter.workingArea = Screen.GetWorkingArea(form);
			}
			PointF pointF = control.PointToScreen(Point.Empty);
			SizeF maximumSize = new SizeF(TipPainter.workingArea.Right - pointF.X - 4f, TipPainter.workingArea.Bottom - pointF.Y - 2f);
			if (maximumSize.Width > 0f && maximumSize.Height > 0f)
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				tipData.SetMaximumSize(maximumSize);
				sizeF = tipData.GetRequiredSize();
				tipData.SetAllocatedSize(sizeF);
				sizeF += new SizeF(4f, 2f);
				size = Size.Ceiling(sizeF);
			}
			if (control.ClientSize != size)
			{
				control.ClientSize = size;
			}
			return size;
		}
		public static Size DrawTip(Control control, Graphics graphics, Font font, string description)
		{
			return TipPainter.DrawTip(control, graphics, new TipText(graphics, font, description));
		}
		public static Size DrawTip(Control control, Graphics graphics, TipSection tipData)
		{
			Size size = Size.Empty;
			SizeF sizeF = SizeF.Empty;
			PointF pointF = control.PointToScreen(Point.Empty);
			if (TipPainter.workingArea == RectangleF.Empty)
			{
				Form form = control.FindForm();
				if (form.Owner != null)
				{
					form = form.Owner;
				}
				TipPainter.workingArea = Screen.GetWorkingArea(form);
			}
			SizeF maximumSize = new SizeF(TipPainter.workingArea.Right - pointF.X - 4f, TipPainter.workingArea.Bottom - pointF.Y - 2f);
			if (maximumSize.Width > 0f && maximumSize.Height > 0f)
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				tipData.SetMaximumSize(maximumSize);
				sizeF = tipData.GetRequiredSize();
				tipData.SetAllocatedSize(sizeF);
				sizeF += new SizeF(4f, 2f);
				size = Size.Ceiling(sizeF);
			}
			if (control.ClientSize != size)
			{
				control.ClientSize = size;
			}
			if (size != Size.Empty)
			{
				Rectangle rect = new Rectangle(Point.Empty, size - new Size(1, 1));
				new RectangleF(2f, 1f, sizeF.Width - 4f, sizeF.Height - 2f);
				graphics.DrawRectangle(SystemPens.WindowFrame, rect);
				tipData.Draw(new PointF(2f, 1f));
			}
			return size;
		}
	}
}
