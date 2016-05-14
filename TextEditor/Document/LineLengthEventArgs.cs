using System;
namespace LTP.TextEditor.Document
{
	public class LineLengthEventArgs : EventArgs
	{
		private IDocument document;
		private int lineNumber;
		private int lineOffset;
		private int moved;
		public IDocument Document
		{
			get
			{
				return this.document;
			}
		}
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}
		public int LineOffset
		{
			get
			{
				return this.lineOffset;
			}
		}
		public int Moved
		{
			get
			{
				return this.moved;
			}
		}
		public LineLengthEventArgs(IDocument document, int lineNumber, int lineOffset, int moved)
		{
			this.document = document;
			this.lineNumber = lineNumber;
			this.lineOffset = lineOffset;
			this.moved = moved;
		}
		public override string ToString()
		{
			return string.Format("[LineLengthEventArgs: Document = {0}, LineNumber = {1}, LineOffset = {2}, Moved = {3}]", new object[]
			{
				this.Document,
				this.LineNumber,
				this.LineOffset,
				this.Moved
			});
		}
	}
}
