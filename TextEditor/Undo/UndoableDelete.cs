using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Undo
{
	public class UndoableDelete : IUndoableOperation
	{
		private IDocument document;
		private int offset;
		private string text;
		public UndoableDelete(IDocument document, int offset, string text)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (offset < 0 || offset > document.TextLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this.document = document;
			this.offset = offset;
			this.text = text;
		}
		public void Undo()
		{
			this.document.UndoStack.AcceptChanges = false;
			this.document.Insert(this.offset, this.text);
			this.document.UndoStack.AcceptChanges = true;
		}
		public void Redo()
		{
			this.document.UndoStack.AcceptChanges = false;
			this.document.Remove(this.offset, this.text.Length);
			this.document.UndoStack.AcceptChanges = true;
		}
	}
}
