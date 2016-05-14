using System;
using System.Drawing;
namespace LTP.TextEditor.Util
{
	internal class CountTipText : TipText
	{
		private float triHeight = 10f;
		private float triWidth = 10f;
		public Rectangle DrawingRectangle1;
		public Rectangle DrawingRectangle2;
		public CountTipText(Graphics graphics, Font font, string text) : base(graphics, font, text)
		{
		}
		private void DrawTriangle(float x, float y, bool flipped)
		{
			Brush brush = BrushRegistry.GetBrush(Color.FromArgb(192, 192, 192));
			base.Graphics.FillRectangle(brush, new RectangleF(x, y, this.triHeight, this.triHeight));
			float num = this.triHeight / 2f;
			float num2 = this.triHeight / 4f;
			brush = Brushes.Black;
			if (flipped)
			{
				base.Graphics.FillPolygon(brush, new PointF[]
				{
					new PointF(x, y + num - num2),
					new PointF(x + this.triWidth / 2f, y + num + num2),
					new PointF(x + this.triWidth, y + num - num2)
				});
				return;
			}
			base.Graphics.FillPolygon(brush, new PointF[]
			{
				new PointF(x, y + num + num2),
				new PointF(x + this.triWidth / 2f, y + num - num2),
				new PointF(x + this.triWidth, y + num + num2)
			});
		}
		public override void Draw(PointF location)
		{
			if (this.tipText != null && this.tipText.Length > 0)
			{
				base.Draw(new PointF(location.X + this.triWidth + 4f, location.Y));
				this.DrawingRectangle1 = new Rectangle((int)location.X + 2, (int)location.Y + 2, (int)this.triWidth, (int)this.triHeight);
				this.DrawingRectangle2 = new Rectangle((int)(location.X + base.AllocatedSize.Width - this.triWidth - 2f), (int)location.Y + 2, (int)this.triWidth, (int)this.triHeight);
				this.DrawTriangle(location.X + 2f, location.Y + 2f, false);
				this.DrawTriangle(location.X + base.AllocatedSize.Width - this.triWidth - 2f, location.Y + 2f, true);
			}
		}
		protected override void OnMaximumSizeChanged()
		{
			if (base.IsTextVisible())
			{
				SizeF requiredSize = base.Graphics.MeasureString(this.tipText, this.tipFont, base.MaximumSize, base.GetInternalStringFormat());
				requiredSize.Width += this.triWidth * 2f + 8f;
				base.SetRequiredSize(requiredSize);
				return;
			}
			base.SetRequiredSize(SizeF.Empty);
		}
	}
}
