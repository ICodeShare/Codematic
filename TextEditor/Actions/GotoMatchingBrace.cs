using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class GotoMatchingBrace : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.TextView.Highlight != null)
			{
				Point point = new Point(textArea.TextView.Highlight.CloseBrace.X + 1, textArea.TextView.Highlight.CloseBrace.Y);
				Point position = new Point(textArea.TextView.Highlight.OpenBrace.X + 1, textArea.TextView.Highlight.OpenBrace.Y);
				if (point == textArea.Caret.Position)
				{
					if (textArea.Document.TextEditorProperties.BracketMatchingStyle == BracketMatchingStyle.After)
					{
						textArea.Caret.Position = position;
					}
					else
					{
						textArea.Caret.Position = new Point(position.X - 1, position.Y);
					}
				}
				else
				{
					if (textArea.Document.TextEditorProperties.BracketMatchingStyle == BracketMatchingStyle.After)
					{
						textArea.Caret.Position = point;
					}
					else
					{
						textArea.Caret.Position = new Point(point.X - 1, point.Y);
					}
				}
				textArea.SetDesiredColumn();
			}
		}
	}
}
