using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ShiftTab : AbstractEditAction
	{
		private void RemoveTabs(IDocument document, ISelection selection, int y1, int y2)
		{
			int num = 0;
			for (int i = y2; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if ((i != y2 || lineSegment.Offset != selection.EndOffset) && lineSegment.Length > 0 && lineSegment.Length > 0)
				{
					int num2 = 0;
					if (document.GetCharAt(lineSegment.Offset) == '\t')
					{
						num2 = 1;
					}
					else
					{
						if (document.GetCharAt(lineSegment.Offset) == ' ')
						{
							int tabIndent = document.TextEditorProperties.TabIndent;
							int num3 = 1;
							while (num3 < lineSegment.Length && document.GetCharAt(lineSegment.Offset + num3) == ' ')
							{
								num3++;
							}
							if (num3 >= tabIndent)
							{
								num2 = tabIndent;
							}
							else
							{
								if (lineSegment.Length > num3 && document.GetCharAt(lineSegment.Offset + num3) == '\t')
								{
									num2 = num3 + 1;
								}
								else
								{
									num2 = num3;
								}
							}
						}
					}
					if (num2 > 0)
					{
						document.Remove(lineSegment.Offset, num2);
						num++;
					}
				}
			}
			if (num > 0)
			{
				document.UndoStack.UndoLast(num);
			}
		}
		public override void Execute(TextArea textArea)
		{
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				foreach (ISelection current in textArea.SelectionManager.SelectionCollection)
				{
					int y = current.StartPosition.Y;
					int y2 = current.EndPosition.Y;
					textArea.BeginUpdate();
					this.RemoveTabs(textArea.Document, current, y, y2);
					textArea.Document.UpdateQueue.Clear();
					textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.LinesBetween, y, y2));
					textArea.EndUpdate();
				}
				textArea.AutoClearSelection = false;
				return;
			}
			LineSegment lineSegmentForOffset = textArea.Document.GetLineSegmentForOffset(textArea.Caret.Offset);
			textArea.Document.GetText(lineSegmentForOffset.Offset, textArea.Caret.Offset - lineSegmentForOffset.Offset);
			int tabIndent = textArea.Document.TextEditorProperties.TabIndent;
			int column = textArea.Caret.Column;
			int num = column % tabIndent;
			if (num == 0)
			{
				textArea.Caret.DesiredColumn = Math.Max(0, column - tabIndent);
			}
			else
			{
				textArea.Caret.DesiredColumn = Math.Max(0, column - num);
			}
			textArea.SetCaretToDesiredColumn(textArea.Caret.Line);
		}
	}
}
