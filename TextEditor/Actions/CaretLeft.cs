using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class CaretLeft : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			Point position = textArea.Caret.Position;
			ArrayList foldedFoldingsWithEnd = textArea.Document.FoldingManager.GetFoldedFoldingsWithEnd(position.Y);
			FoldMarker foldMarker = null;
			foreach (FoldMarker foldMarker2 in foldedFoldingsWithEnd)
			{
				if (foldMarker2.EndColumn == position.X)
				{
					foldMarker = foldMarker2;
					break;
				}
			}
			if (foldMarker != null)
			{
				position.Y = foldMarker.StartLine;
				position.X = foldMarker.StartColumn;
			}
			else
			{
				if (position.X > 0)
				{
					position.X--;
				}
				else
				{
					if (position.Y > 0)
					{
						LineSegment lineSegment = textArea.Document.GetLineSegment(position.Y - 1);
						position = new Point(lineSegment.Length, position.Y - 1);
					}
				}
			}
			textArea.Caret.Position = position;
			textArea.SetDesiredColumn();
		}
	}
}
