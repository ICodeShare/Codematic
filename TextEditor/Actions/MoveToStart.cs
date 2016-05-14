using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class MoveToStart : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Caret.Line != 0 || textArea.Caret.Column != 0)
			{
				textArea.Caret.Position = new Point(0, 0);
				textArea.SetDesiredColumn();
			}
		}
	}
}
