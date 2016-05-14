using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ConvertLeadingTabsToSpaces : AbstractLineFormatAction
	{
		protected override void Convert(IDocument document, int y1, int y2)
		{
			int num = 0;
			for (int i = y2; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if (lineSegment.Length > 0)
				{
					int num2 = 0;
					while (num2 < lineSegment.Length && char.IsWhiteSpace(document.GetCharAt(lineSegment.Offset + num2)))
					{
						num2++;
					}
					if (num2 > 0)
					{
						string text = document.GetText(lineSegment.Offset, num2);
						string text2 = text.Replace("\t", new string(' ', document.TextEditorProperties.TabIndent));
						document.Replace(lineSegment.Offset, num2, text2);
						num++;
					}
				}
			}
			if (num > 0)
			{
				document.UndoStack.UndoLast(num);
			}
		}
	}
}
