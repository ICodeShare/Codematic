using System;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class CaretDown : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			Point position = textArea.Caret.Position;
			int y = position.Y;
			int visibleLine = textArea.Document.GetVisibleLine(y);
			if (visibleLine < textArea.Document.GetVisibleLine(textArea.Document.TotalNumberOfLines))
			{
				int drawingXPos = textArea.TextView.GetDrawingXPos(y, position.X);
				Point point = new Point(drawingXPos, textArea.TextView.DrawingPosition.Y + (visibleLine + 1) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y);
				textArea.Caret.Position = textArea.TextView.GetLogicalPosition(point.X, point.Y);
				textArea.SetCaretToDesiredColumn(textArea.Caret.Position.Y);
			}
		}
	}
}
