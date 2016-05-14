using System;
namespace LTP.TextEditor.Actions
{
	public class ScrollLineDown : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.AutoClearSelection = false;
			textArea.MotherTextAreaControl.VScrollBar.Value = Math.Min(textArea.MotherTextAreaControl.VScrollBar.Maximum, textArea.VirtualTop.Y + textArea.TextView.FontHeight);
		}
	}
}
