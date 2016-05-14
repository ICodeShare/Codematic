using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class Backspace : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				textArea.BeginUpdate();
				textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
				textArea.SelectionManager.RemoveSelectedText();
				textArea.ScrollToCaret();
				textArea.EndUpdate();
				return;
			}
			if (textArea.Caret.Offset > 0)
			{
				textArea.BeginUpdate();
				int lineNumberForOffset = textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset);
				int offset = textArea.Document.GetLineSegment(lineNumberForOffset).Offset;
				if (offset == textArea.Caret.Offset)
				{
					LineSegment lineSegment = textArea.Document.GetLineSegment(lineNumberForOffset - 1);
					int arg_C2_0 = textArea.Document.TotalNumberOfLines;
					int num = lineSegment.Offset + lineSegment.Length;
					int length = lineSegment.Length;
					textArea.Document.Remove(num, offset - num);
					textArea.Caret.Position = new Point(length, lineNumberForOffset - 1);
					textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd, new Point(0, lineNumberForOffset - 1)));
					textArea.EndUpdate();
					return;
				}
				int offset2 = textArea.Caret.Offset - 1;
				textArea.Caret.Position = textArea.Document.OffsetToPosition(offset2);
				textArea.Document.Remove(offset2, 1);
				textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToLineEnd, new Point(textArea.Caret.Offset - textArea.Document.GetLineSegment(lineNumberForOffset).Offset, lineNumberForOffset)));
				textArea.EndUpdate();
			}
		}
	}
}
