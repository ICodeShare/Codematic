using System;
namespace LTP.TextEditor.Actions
{
	public class GotoNextBookmark : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			int nextMark = textArea.Document.BookmarkManager.GetNextMark(textArea.Caret.Line);
			if (nextMark >= 0 && nextMark < textArea.Document.TotalNumberOfLines)
			{
				textArea.Caret.Line = nextMark;
			}
			textArea.SelectionManager.ClearSelection();
		}
	}
}
