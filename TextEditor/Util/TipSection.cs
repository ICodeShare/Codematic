using System;
using System.Drawing;
namespace LTP.TextEditor.Util
{
	internal abstract class TipSection
	{
		private SizeF tipAllocatedSize;
		private Graphics tipGraphics;
		private SizeF tipMaxSize;
		private SizeF tipRequiredSize;
		protected Graphics Graphics
		{
			get
			{
				return this.tipGraphics;
			}
		}
		protected SizeF AllocatedSize
		{
			get
			{
				return this.tipAllocatedSize;
			}
		}
		protected SizeF MaximumSize
		{
			get
			{
				return this.tipMaxSize;
			}
		}
		protected TipSection(Graphics graphics)
		{
			this.tipGraphics = graphics;
		}
		public abstract void Draw(PointF location);
		public SizeF GetRequiredSize()
		{
			return this.tipRequiredSize;
		}
		public void SetAllocatedSize(SizeF allocatedSize)
		{
			this.tipAllocatedSize = allocatedSize;
			this.OnAllocatedSizeChanged();
		}
		public void SetMaximumSize(SizeF maximumSize)
		{
			this.tipMaxSize = maximumSize;
			this.OnMaximumSizeChanged();
		}
		protected virtual void OnAllocatedSizeChanged()
		{
		}
		protected virtual void OnMaximumSizeChanged()
		{
		}
		protected void SetRequiredSize(SizeF requiredSize)
		{
			requiredSize.Width = Math.Max(0f, requiredSize.Width);
			requiredSize.Height = Math.Max(0f, requiredSize.Height);
			requiredSize.Width = Math.Min(this.tipMaxSize.Width, requiredSize.Width);
			requiredSize.Height = Math.Min(this.tipMaxSize.Height, requiredSize.Height);
			this.tipRequiredSize = requiredSize;
		}
	}
}
