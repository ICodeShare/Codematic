using System;
using System.Drawing;
namespace LTP.TextEditor
{
	public class Highlight
	{
		private Point openBrace;
		private Point closeBrace;
		public Point OpenBrace
		{
			get
			{
				return this.openBrace;
			}
			set
			{
				this.openBrace = value;
			}
		}
		public Point CloseBrace
		{
			get
			{
				return this.closeBrace;
			}
			set
			{
				this.closeBrace = value;
			}
		}
		public Highlight(Point openBrace, Point closeBrace)
		{
			this.openBrace = openBrace;
			this.closeBrace = closeBrace;
		}
	}
}
