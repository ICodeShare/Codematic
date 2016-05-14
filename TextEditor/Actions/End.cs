using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Actions
{
	public class End : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			Point point = textArea.Caret.Position;
			bool flag = false;
			do
			{
				LineSegment lineSegment = textArea.Document.GetLineSegment(point.Y);
				point.X = lineSegment.Length;
				ArrayList foldingsFromPosition = textArea.Document.FoldingManager.GetFoldingsFromPosition(point.Y, point.X);
				flag = false;
				foreach (FoldMarker foldMarker in foldingsFromPosition)
				{
					if (foldMarker.IsFolded)
					{
						point = new Point(foldMarker.EndColumn, foldMarker.EndLine);
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
