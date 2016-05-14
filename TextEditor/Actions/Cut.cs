using System;
namespace LTP.TextEditor.Actions
{
	public class Cut : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			textArea.ClipboardHandler.Cut(null, null);
		}
	}
}
