using System;
namespace LTP.TextEditor.Actions
{
	public class ToggleEditMode : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			switch (textArea.Caret.CaretMode)
			{
			case CaretMode.InsertMode:
				textArea.Caret.CaretMode = CaretMode.OverwriteMode;
				return;
			case CaretMode.OverwriteMode:
				textArea.Caret.CaretMode = CaretMode.InsertMode;
				return;
			default:
				return;
			}
		}
	}
}
