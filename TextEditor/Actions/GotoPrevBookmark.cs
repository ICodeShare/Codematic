using System;
namespace LTP.TextEditor.Actions
{
	public class GotoPrevBookmark : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			int prevMark = textArea.Document.BookmarkManager.GetPrevMark(textArea.Caret.Line);
			if (prevMark >= 0 && prevMark < textArea.Document.TotalNumberOfLines)
			{
				textArea.Caret.Line = prevMark;
			}
			textArea.SelectionManager.ClearSelection();
		}
	}
}
