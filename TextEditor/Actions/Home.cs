using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class Home : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			Point point = textArea.Caret.Position;
			bool flag = false;
			do
			{
				LineSegment lineSegment = textArea.Document.GetLineSegment(point.Y);
				if (TextUtilities.IsEmptyLine(textArea.Document, point.Y))
				{
					if (point.X != 0)
					{
						point.X = 0;
					}
					else
					{
						point.X = lineSegment.Length;
					}
				}
				else
				{
					int firstNonWSChar = TextUtilities.GetFirstNonWSChar(textArea.Document, lineSegment.Offset);
					int num = firstNonWSChar - lineSegment.Offset;
					if (point.X == num)
					{
						point.X = 0;
					}
					else
					{
						point.X = num;
					}
				}
				ArrayList foldingsFromPosition = textArea.Document.FoldingManager.GetFoldingsFromPosition(point.Y, point.X);
				flag = false;
				foreach (FoldMarker foldMarker in foldingsFromPosition)
				{
					if (foldMarker.IsFolded)
					{
						point = new Point(foldMarker.StartColumn, foldMarker.StartLine);
						flag = true;
						break;
					}
				}
			}
			while (flag);
			if (point != textArea.Caret.Position)
			{
				textArea.Caret.Position = point;
				textArea.SetDesiredColumn();
			}
		}
	}
}
