using System;
namespace LTP.TextEditor.Document
{
	public class ColumnRange
	{
		public static readonly ColumnRange NoColumn = new ColumnRange(-2, -2);
		public static readonly ColumnRange WholeColumn = new ColumnRange(-1, -1);
		private int startColumn;
		private int endColumn;
		public int StartColumn
		{
			get
			{
				return this.startColumn;
			}
			set
			{
				this.startColumn = value;
			}
		}
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
			set
			{
				this.endColumn = value;
			}
		}
		public ColumnRange(int startColumn, int endColumn)
		{
			this.startColumn = startColumn;
			this.endColumn = endColumn;
		}
		public override int GetHashCode()
		{
			return this.startColumn + (this.endColumn << 16);
		}
		public override bool Equals(object obj)
		{
			return obj is ColumnRange && ((ColumnRange)obj).startColumn == this.startColumn && ((ColumnRange)obj).endColumn == this.endColumn;
		}
		public override string ToString()
		{
			return string.Format("[ColumnRange: StartColumn={0}, EndColumn={1}]", this.startColumn, this.endColumn);
		}
	}
}
