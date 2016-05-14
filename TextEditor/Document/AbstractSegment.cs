using System;
namespace LTP.TextEditor.Document
{
	public class AbstractSegment : ISegment
	{
		protected int offset = -1;
		protected int length = -1;
		public virtual int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}
		public virtual int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}
		public override string ToString()
		{
			return string.Format("[AbstractSegment: Offset = {0}, Length = {1}]", this.Offset, this.Length);
		}
	}
}
