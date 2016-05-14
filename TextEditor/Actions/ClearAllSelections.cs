using System;
namespace LTP.TextEditor.Actions
{
	public class ClearAllSelections : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.SelectionManager.ClearSelection();
		}
	}
}
