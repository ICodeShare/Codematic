using LTP.TextEditor.Document;
using System;
using System.Collections;
namespace LTP.TextEditor.Actions
{
	public class ToggleFolding : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			ArrayList foldingsWithStart = textArea.Document.FoldingManager.GetFoldingsWithStart(textArea.Caret.Line);
			foreach (FoldMarker foldMarker in foldingsWithStart)
			{
				foldMarker.IsFolded = !foldMarker.IsFolded;
			}
			textArea.Refresh();
			textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
		}
	}
}
