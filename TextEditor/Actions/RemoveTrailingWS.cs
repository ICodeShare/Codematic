using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class RemoveTrailingWS : AbstractLineFormatAction
	{
		protected override void Convert(IDocument document, int y1, int y2)
		{
			int num = 0;
			for (int i = y2 - 1; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				int num2 = 0;
				int num3 = lineSegment.Offset + lineSegment.Length - 1;
				while (num3 >= lineSegment.Offset && char.IsWhiteSpace(document.GetCharAt(num3)))
				{
					num2++;
					num3--;
				}
				if (num2 > 0)
				{
					document.Remove(lineSegment.Offset + lineSegment.Length - num2, num2);
					num++;
				}
			}
			if (num > 0)
			{
				document.UndoStack.UndoLast(num);
			}
		}
	}
}
