using System;
namespace LTP.TextEditor.Document
{
	public class LineManagerEventArgs : EventArgs
	{
		private IDocument document;
		private int start;
		private int moved;
		public IDocument Document
		{
			get
			{
				return this.document;
			}
		}
		public int LineStart
		{
			get
			{
				return this.start;
			}
		}
		public int LinesMoved
		{
			get
			{
				return this.moved;
			}
		}
		public LineManagerEventArgs(IDocument document, int lineStart, int linesMoved)
		{
			this.document = document;
			this.start = lineStart;
			this.moved = linesMoved;
		}
	}
}
