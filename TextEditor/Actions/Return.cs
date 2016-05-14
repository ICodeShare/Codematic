using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class Return : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			textArea.BeginUpdate();
			textArea.InsertString(Environment.NewLine);
			int line = textArea.Caret.Line;
			textArea.Caret.Column = textArea.Document.FormattingStrategy.FormatLine(textArea, line, textArea.Caret.Offset, '\n');
			textArea.SetDesiredColumn();
			textArea.Document.UpdateQueue.Clear();
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd, new Point(0, line - 1)));
			textArea.EndUpdate();
		}
	}
}
