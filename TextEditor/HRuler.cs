using System;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class HRuler : UserControl
	{
		private TextArea textArea;
		public HRuler(TextArea textArea)
		{
			this.textArea = textArea;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			int num = 0;
			for (float num2 = (float)this.textArea.TextView.DrawingPosition.Left; num2 < (float)this.textArea.TextView.DrawingPosition.Right; num2 += this.textArea.TextView.GetWidth(' '))
			{
				int num3 = base.Height * 2 / 3;
				if (num % 5 == 0)
				{
					num3 = base.Height * 4 / 5;
				}
				if (num % 10 == 0)
				{
					num3 = 1;
				}
				num++;
				graphics.DrawLine(Pens.Black, (int)num2, num3, (int)num2, base.Height - num3);
			}
		}
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, base.Width, base.Height));
		}
	}
}
