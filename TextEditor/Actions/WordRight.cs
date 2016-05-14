using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class WordRight : CaretRight
	{
		public override void Execute(TextArea textArea)
		{
			LineSegment lineSegment = textArea.Document.GetLineSegment(textArea.Caret.Position.Y);
			Point position = textArea.Caret.Position;
			Point position2;
			if (textArea.Caret.Column >= lineSegment.Length)
			{
				position2 = new Point(0, textArea.Caret.Line + 1);
			}
			else
			{
				int offset = TextUtilities.FindNextWordStart(textArea.Document, textArea.Caret.Offset);
				position2 = textArea.Document.OffsetToPosition(offset);
			}
			ArrayList foldingsFromPosition = textArea.Document.FoldingManager.GetFoldingsFromPosition(position2.Y, position2.X);
			foreach (FoldMarker foldMarker in foldingsFromPosition)
			{
				if (foldMarker.IsFolded)
				{
					if (position.X == foldMarker.StartColumn && position.Y == foldMarker.StartLine)
					{
						position2 = new Point(foldMarker.EndColumn, foldMarker.EndLine);
						break;
					}
					position2 = new Point(foldMarker.StartColumn, foldMarker.StartLine);
					break;
				}
			}
			textArea.Caret.Position = position2;
			textArea.SetDesiredColumn();
		}
	}
}
