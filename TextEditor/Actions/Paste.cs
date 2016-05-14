using System;
namespace LTP.TextEditor.Actions
{
	public class Paste : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			textArea.ClipboardHandler.Paste(null, null);
		}
	}
}
