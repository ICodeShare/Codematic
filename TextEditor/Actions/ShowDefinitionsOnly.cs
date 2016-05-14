using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ShowDefinitionsOnly : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			foreach (FoldMarker foldMarker in textArea.Document.FoldingManager.FoldMarker)
			{
				foldMarker.IsFolded = (foldMarker.FoldType == FoldType.MemberBody || foldMarker.FoldType == FoldType.Region);
			}
			textArea.Refresh();
			textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
		}
	}
}
