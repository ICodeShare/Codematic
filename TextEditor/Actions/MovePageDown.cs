using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class MovePageDown : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			int line = textArea.Caret.Line;
			int num = Math.Min(textArea.Document.GetNextVisibleLineAbove(line, textArea.TextView.VisibleLineCount), textArea.Document.TotalNumberOfLines - 1);
			if (line != num)
			{
				textArea.Caret.Position = new Point(textArea.Caret.DesiredColumn, num);
			}
		}
	}
}
