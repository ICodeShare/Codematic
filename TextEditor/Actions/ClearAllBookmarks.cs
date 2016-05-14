using System;
namespace LTP.TextEditor.Actions
{
	public class ClearAllBookmarks : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.Document.BookmarkManager.Clear();
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
			textArea.Document.CommitUpdate();
		}
	}
}
