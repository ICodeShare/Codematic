using System;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public class DefaultSelection : ISelection
	{
		private IDocument document;
		private bool isRectangularSelection;
		private Point startPosition = new Point(-1, -1);
		private Point endPosition = new Point(-1, -1);
		public Point StartPosition
		{
			get
			{
				return this.startPosition;
			}
			set
			{
				this.startPosition = value;
			}
		}
		public Point EndPosition
		{
			get
			{
				return this.endPosition;
			}
			set
			{
				this.endPosition = value;
			}
		}
		public int Offset
		{
			get
			{
				return this.document.PositionToOffset(this.startPosition);
			}
		}
		public int EndOffset
		{
			get
			{
				return this.document.PositionToOffset(this.endPosition);
			}
		}
		public int Length
		{
			get
			{
				return this.EndOffset - this.Offset;
			}
		}
		public bool IsEmpty
		{
			get
			{
				return this.startPosition == this.endPosition;
			}
		}
		public bool IsRectangularSelection
		{
			get
			{
				return this.isRectangularSelection;
			}
			set
			{
				this.isRectangularSelection = value;
			}
		}
		public string SelectedText
		{
			get
			{
				if (this.document == null)
				{
					return null;
				}
				if (this.Length < 0)
				{
					return null;
				}
				return this.document.GetText(this.Offset, this.Length);
			}
		}
		public DefaultSelection(IDocument document, Point startPosition, Point endPosition)
		{
			this.document = document;
			this.startPosition = startPosition;
			this.endPosition = endPosition;
		}
		public override string ToString()
		{
			return string.Format("[DefaultSelection : StartPosition={0}, EndPosition={1}]", this.startPosition, this.endPosition);
		}
		public bool ContainsPosition(Point position)
		{
			return (this.startPosition.Y < position.Y && position.Y < this.endPosition.Y) || (this.startPosition.Y == position.Y && this.startPosition.X <= position.X && (this.startPosition.Y != this.endPosition.Y || position.X <= this.endPosition.X)) || (this.endPosition.Y == position.Y && this.startPosition.Y != this.endPosition.Y && position.X <= this.endPosition.X);
		}
		public bool ContainsOffset(int offset)
		{
			return this.Offset <= offset && offset <= this.EndOffset;
		}
	}
}
