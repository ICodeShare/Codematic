using System;
namespace LTP.TextEditor.Actions
{
	public class ScrollLineUp : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.AutoClearSelection = false;
			textArea.MotherTextAreaControl.VScrollBar.Value = Math.Max(textArea.MotherTextAreaControl.VScrollBar.Minimum, textArea.VirtualTop.Y - textArea.TextView.FontHeight);
		}
	}
}
