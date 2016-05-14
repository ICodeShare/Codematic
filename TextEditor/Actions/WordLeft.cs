using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class WordLeft : CaretLeft
	{
		public override void Execute(TextArea textArea)
		{
			Point position = textArea.Caret.Position;
			if (textArea.Caret.Column == 0)
			{
				base.Execute(textArea);
				return;
			}
			textArea.Document.GetLineSegment(textArea.Caret.Position.Y);
			int offset = TextUtilities.FindPrevWordStart(textArea.Document, textArea.Caret.Offset);
			Point position2 = textArea.Document.OffsetToPosition(offset);
			ArrayList foldingsFromPosition = textArea.Document.FoldingManager.GetFoldingsFromPosition(position2.Y, position2.X);
			foreach (FoldMarker foldMarker in foldingsFromPosition)
			{
				if (foldMarker.IsFolded)
				{
					if (position.X == foldMarker.EndColumn && position.Y == foldMarker.EndLine)
					{
						position2 = new Point(foldMarker.StartColumn, foldMarker.StartLine);
						break;
					}
					position2 = new Point(foldMarker.EndColumn, foldMarker.EndLine);
					break;
				}
			}
			textArea.Caret.Position = position2;
			textArea.SetDesiredColumn();
		}
	}
}
