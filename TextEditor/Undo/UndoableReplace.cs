using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Undo
{
	public class UndoableReplace : IUndoableOperation
	{
		private IDocument document;
		private int offset;
		private string text;
		private string origText;
		public UndoableReplace(IDocument document, int offset, string origText, string text)
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
			this.origText = origText;
		}
		public void Undo()
		{
			this.document.UndoStack.AcceptChanges = false;
			this.document.Replace(this.offset, this.text.Length, this.origText);
			this.document.UndoStack.AcceptChanges = true;
		}
		public void Redo()
		{
			this.document.UndoStack.AcceptChanges = false;
			this.document.Replace(this.offset, this.origText.Length, this.text);
			this.document.UndoStack.AcceptChanges = true;
		}
	}
}
