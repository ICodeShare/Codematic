using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class SelectWholeDocument : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.AutoClearSelection = false;
			Point point = new Point(0, 0);
			Point point2 = textArea.Document.OffsetToPosition(textArea.Document.TextLength);
			if (textArea.SelectionManager.HasSomethingSelected && textArea.SelectionManager.SelectionCollection[0].StartPosition == point && textArea.SelectionManager.SelectionCollection[0].EndPosition == point2)
			{
				return;
			}
			textArea.SelectionManager.SetSelection(new DefaultSelection(textArea.Document, point, point2));
		}
	}
}
