using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class CaretRight : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			LineSegment lineSegment = textArea.Document.GetLineSegment(textArea.Caret.Line);
			Point position = textArea.Caret.Position;
			ArrayList foldedFoldingsWithStart = textArea.Document.FoldingManager.GetFoldedFoldingsWithStart(position.Y);
			FoldMarker foldMarker = null;
			foreach (FoldMarker foldMarker2 in foldedFoldingsWithStart)
			{
				if (foldMarker2.StartColumn == position.X)
				{
					foldMarker = foldMarker2;
					break;
				}
			}
			if (foldMarker != null)
			{
				position.Y = foldMarker.EndLine;
				position.X = foldMarker.EndColumn;
			}
			else
			{
				if (position.X < lineSegment.Length || textArea.TextEditorProperties.AllowCaretBeyondEOL)
				{
					position.X++;
				}
				else
				{
					if (position.Y + 1 < textArea.Document.TotalNumberOfLines)
					{
						position.Y++;
						position.X = 0;
					}
				}
			}
			textArea.Caret.Position = position;
			textArea.SetDesiredColumn();
		}
	}
}
