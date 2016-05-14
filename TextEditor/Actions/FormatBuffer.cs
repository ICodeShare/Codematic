using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class FormatBuffer : AbstractLineFormatAction
	{
		protected override void Convert(IDocument document, int startLine, int endLine)
		{
			document.FormattingStrategy.IndentLines(this.textArea, startLine, endLine);
		}
	}
}
