using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ToggleAllFoldings : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			bool isFolded = true;
			foreach (FoldMarker foldMarker in textArea.Document.FoldingManager.FoldMarker)
			{
				if (foldMarker.IsFolded)
				{
					isFolded = false;
					break;
				}
			}
			foreach (FoldMarker foldMarker2 in textArea.Document.FoldingManager.FoldMarker)
			{
				foldMarker2.IsFolded = isFolded;
			}
			textArea.Refresh();
			textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
		}
	}
}
