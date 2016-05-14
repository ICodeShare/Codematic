using System;
using System.Drawing;
namespace LTP.TextEditor
{
	public class TextAreaUpdate
	{
		private Point position;
		private TextAreaUpdateType type;
		public TextAreaUpdateType TextAreaUpdateType
		{
			get
			{
				return this.type;
			}
		}
		public Point Position
		{
			get
			{
				return this.position;
			}
		}
		public TextAreaUpdate(TextAreaUpdateType type)
		{
			this.type = type;
		}
		public TextAreaUpdate(TextAreaUpdateType type, Point position)
		{
			this.type = type;
			this.position = position;
		}
		public TextAreaUpdate(TextAreaUpdateType type, int startLine, int endLine)
		{
			this.type = type;
			this.position = new Point(startLine, endLine);
		}
		public TextAreaUpdate(TextAreaUpdateType type, int singleLine)
		{
			this.type = type;
			this.position = new Point(0, singleLine);
		}
		public override string ToString()
		{
			return string.Format("[TextAreaUpdate: Type={0}, Position={1}]", this.type, this.position);
		}
	}
}
