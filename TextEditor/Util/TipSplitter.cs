using System;
using System.Drawing;
namespace LTP.TextEditor.Util
{
	internal class TipSplitter : TipSection
	{
		private bool isHorizontal;
		private float[] offsets;
		private TipSection[] tipSections;
		public TipSplitter(Graphics graphics, bool horizontal, params TipSection[] sections) : base(graphics)
		{
			this.isHorizontal = horizontal;
			this.offsets = new float[sections.Length];
			this.tipSections = (TipSection[])sections.Clone();
		}
		public override void Draw(PointF location)
		{
			if (this.isHorizontal)
			{
				for (int i = 0; i < this.tipSections.Length; i++)
				{
					this.tipSections[i].Draw(new PointF(location.X + this.offsets[i], location.Y));
				}
				return;
			}
			for (int j = 0; j < this.tipSections.Length; j++)
			{
				this.tipSections[j].Draw(new PointF(location.X, location.Y + this.offsets[j]));
			}
		}
		protected override void OnMaximumSizeChanged()
		{
			base.OnMaximumSizeChanged();
			float num = 0f;
			float num2 = 0f;
			SizeF maximumSize = base.MaximumSize;
			for (int i = 0; i < this.tipSections.Length; i++)
			{
				TipSection tipSection = this.tipSections[i];
				tipSection.SetMaximumSize(maximumSize);
				SizeF requiredSize = tipSection.GetRequiredSize();
				this.offsets[i] = num;
				if (this.isHorizontal)
				{
					float num3 = (float)Math.Ceiling((double)requiredSize.Width);
					num += num3;
					maximumSize.Width = Math.Max(0f, maximumSize.Width - num3);
					num2 = Math.Max(num2, requiredSize.Height);
				}
				else
				{
					float num3 = (float)Math.Ceiling((double)requiredSize.Height);
					num += num3;
					maximumSize.Height = Math.Max(0f, maximumSize.Height - num3);
					num2 = Math.Max(num2, requiredSize.Width);
				}
			}
			TipSection[] array = this.tipSections;
			for (int j = 0; j < array.Length; j++)
			{
				TipSection tipSection2 = array[j];
				if (this.isHorizontal)
				{
					tipSection2.SetAllocatedSize(new SizeF(tipSection2.GetRequiredSize().Width, num2));
				}
				else
				{
					tipSection2.SetAllocatedSize(new SizeF(num2, tipSection2.GetRequiredSize().Height));
				}
			}
			if (this.isHorizontal)
			{
				base.SetRequiredSize(new SizeF(num, num2));
				return;
			}
			base.SetRequiredSize(new SizeF(num2, num));
		}
	}
}
