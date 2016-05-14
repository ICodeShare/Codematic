using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class WordBackspace : AbstractEditAction
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
			if (textArea.Caret.Offset > lineSegmentForOffset.Offset)
			{
				int num = TextUtilities.FindPrevWordStart(textArea.Document, textArea.Caret.Offset);
				if (num < textArea.Caret.Offset)
				{
					textArea.Document.Remove(num, textArea.Caret.Offset - num);
					textArea.Caret.Position = textArea.Document.OffsetToPosition(num);
				}
			}
			if (textArea.Caret.Offset == lineSegmentForOffset.Offset)
			{
				int lineNumberForOffset = textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset);
				if (lineNumberForOffset > 0)
				{
					LineSegment lineSegment = textArea.Document.GetLineSegment(lineNumberForOffset - 1);
					int num2 = lineSegment.Offset + lineSegment.Length;
					int length = textArea.Caret.Offset - num2;
					textArea.Document.Remove(num2, length);
					textArea.Caret.Position = textArea.Document.OffsetToPosition(num2);
				}
			}
			textArea.SetDesiredColumn();
			textArea.EndUpdate();
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd, new Point(0, textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset))));
			textArea.Document.CommitUpdate();
		}
	}
}
