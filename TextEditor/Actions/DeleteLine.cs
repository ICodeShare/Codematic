using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class DeleteLine : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			int line = textArea.Caret.Line;
			LineSegment lineSegment = textArea.Document.GetLineSegment(line);
			textArea.Document.Remove(lineSegment.Offset, lineSegment.TotalLength);
			textArea.Caret.Position = textArea.Document.OffsetToPosition(lineSegment.Offset);
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd, new Point(0, line)));
			textArea.UpdateMatchingBracket();
			textArea.Document.CommitUpdate();
		}
	}
}
