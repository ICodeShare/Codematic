using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class RemoveLeadingWS : AbstractLineFormatAction
	{
		protected override void Convert(IDocument document, int y1, int y2)
		{
			int num = 0;
			for (int i = y1; i < y2; i++)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				int num2 = 0;
				int num3 = lineSegment.Offset;
				while (num3 < lineSegment.Offset + lineSegment.Length && char.IsWhiteSpace(document.GetCharAt(num3)))
				{
					num2++;
					num3++;
				}
				if (num2 > 0)
				{
					document.Remove(lineSegment.Offset, num2);
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
