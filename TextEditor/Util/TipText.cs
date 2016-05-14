using System;
using System.Drawing;
namespace LTP.TextEditor.Util
{
	internal class TipText : TipSection
	{
		protected StringAlignment horzAlign;
		protected StringAlignment vertAlign;
		protected Color tipColor;
		protected Font tipFont;
		protected StringFormat tipFormat;
		protected string tipText;
		public Color Color
		{
			get
			{
				return this.tipColor;
			}
			set
			{
				this.tipColor = value;
			}
		}
		public StringAlignment HorizontalAlignment
		{
			get
			{
				return this.horzAlign;
			}
			set
			{
				this.horzAlign = value;
				this.tipFormat = null;
			}
		}
		public StringAlignment VerticalAlignment
		{
			get
			{
				return this.vertAlign;
			}
			set
			{
				this.vertAlign = value;
				this.tipFormat = null;
			}
		}
		public TipText(Graphics graphics, Font font, string text) : base(graphics)
		{
			this.tipFont = font;
			this.tipText = text;
			this.Color = SystemColors.InfoText;
			this.HorizontalAlignment = StringAlignment.Near;
			this.VerticalAlignment = StringAlignment.Near;
		}
		public override void Draw(PointF location)
		{
			if (this.IsTextVisible())
			{
				RectangleF layoutRectangle = new RectangleF(location, base.AllocatedSize);
				base.Graphics.DrawString(this.tipText, this.tipFont, BrushRegistry.GetBrush(this.Color), layoutRectangle, this.GetInternalStringFormat());
			}
		}
		protected StringFormat GetInternalStringFormat()
		{
			if (this.tipFormat == null)
			{
				this.tipFormat = TipText.CreateTipStringFormat(this.horzAlign, this.vertAlign);
			}
			return this.tipFormat;
		}
		protected override void OnMaximumSizeChanged()
		{
			base.OnMaximumSizeChanged();
			if (this.IsTextVisible())
			{
				SizeF requiredSize = base.Graphics.MeasureString(this.tipText, this.tipFont, base.MaximumSize, this.GetInternalStringFormat());
				base.SetRequiredSize(requiredSize);
				return;
			}
			base.SetRequiredSize(SizeF.Empty);
		}
		private static StringFormat CreateTipStringFormat(StringAlignment horizontalAlignment, StringAlignment verticalAlignment)
		{
			StringFormat stringFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
			stringFormat.FormatFlags = (StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces);
			stringFormat.Alignment = horizontalAlignment;
			stringFormat.LineAlignment = verticalAlignment;
			return stringFormat;
		}
		protected bool IsTextVisible()
		{
			return this.tipText != null && this.tipText.Length > 0;
		}
	}
}
