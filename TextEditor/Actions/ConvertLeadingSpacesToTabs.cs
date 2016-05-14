using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ConvertLeadingSpacesToTabs : AbstractLineFormatAction
	{
		protected override void Convert(IDocument document, int y1, int y2)
		{
			int num = 0;
			for (int i = y2; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if (lineSegment.Length > 0)
				{
					string text = TextUtilities.LeadingWhiteSpaceToTabs(document.GetText(lineSegment.Offset, lineSegment.Length), document.TextEditorProperties.TabIndent);
					document.Replace(lineSegment.Offset, lineSegment.Length, text);
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
