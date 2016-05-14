using System;
using System.Drawing;
namespace LTP.TextEditor.Util
{
	internal class TipSpacer : TipSection
	{
		private SizeF spacerSize;
		public TipSpacer(Graphics graphics, SizeF size) : base(graphics)
		{
			this.spacerSize = size;
		}
		public override void Draw(PointF location)
		{
		}
		protected override void OnMaximumSizeChanged()
		{
			base.OnMaximumSizeChanged();
			base.SetRequiredSize(new SizeF(Math.Min(base.MaximumSize.Width, this.spacerSize.Width), Math.Min(base.MaximumSize.Height, this.spacerSize.Height)));
		}
	}
}
