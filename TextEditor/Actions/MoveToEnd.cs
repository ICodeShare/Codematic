using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class MoveToEnd : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			Point point = textArea.Document.OffsetToPosition(textArea.Document.TextLength);
			if (textArea.Caret.Position != point)
			{
				textArea.Caret.Position = point;
				textArea.SetDesiredColumn();
			}
		}
	}
}
