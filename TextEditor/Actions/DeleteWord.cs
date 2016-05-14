using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class DeleteWord : Delete
	{
		public override void Execute(TextArea textArea)
		{
			textArea.BeginUpdate();
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				textArea.SelectionManager.RemoveSelectedText();
				textArea.ScrollToCaret();
			}
			LineSegment lineSegmentForOffset = textArea.Document.GetLineSegmentForOffset(textArea.Caret.Offset);
			if (textArea.Caret.Offset == lineSegmentForOffset.Offset + lineSegmentForOffset.Length)
			{
				base.Execute(textArea);
			}
			else
			{
				int num = TextUtilities.FindNextWordStart(textArea.Document, textArea.Caret.Offset);
				if (num > textArea.Caret.Offset)
				{
					textArea.Document.Remove(textArea.Caret.Offset, num - textArea.Caret.Offset);
				}
			}
			textArea.UpdateMatchingBracket();
			textArea.EndUpdate();
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd, new Point(0, textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset))));
			textArea.Document.CommitUpdate();
		}
	}
}
