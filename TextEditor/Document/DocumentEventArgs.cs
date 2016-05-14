using System;
namespace LTP.TextEditor.Document
{
	public class DocumentEventArgs : EventArgs
	{
		private IDocument document;
		private int offset;
		private int length;
		private string text;
		public IDocument Document
		{
			get
			{
				return this.document;
			}
		}
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}
		public string Text
		{
			get
			{
				return this.text;
			}
		}
		public int Length
		{
			get
			{
				return this.length;
			}
		}
		public DocumentEventArgs(IDocument document) : this(document, -1, -1, null)
		{
		}
		public DocumentEventArgs(IDocument document, int offset) : this(document, offset, -1, null)
		{
		}
		public DocumentEventArgs(IDocument document, int offset, int length) : this(document, offset, length, null)
		{
		}
		public DocumentEventArgs(IDocument document, int offset, int length, string text)
		{
			this.document = document;
			this.offset = offset;
			this.length = length;
			this.text = text;
		}
		public override string ToString()
		{
			return string.Format("[DocumentEventArgs: Document = {0}, Offset = {1}, Text = {2}, Length = {3}]", new object[]
			{
				this.Document,
				this.Offset,
				this.Text,
				this.Length
			});
		}
	}
}
