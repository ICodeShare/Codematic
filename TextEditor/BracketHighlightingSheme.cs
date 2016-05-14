using LTP.TextEditor.Document;
using System;
using System.Drawing;
namespace LTP.TextEditor
{
	public class BracketHighlightingSheme
	{
		private char opentag;
		private char closingtag;
		public char OpenTag
		{
			get
			{
				return this.opentag;
			}
			set
			{
				this.opentag = value;
			}
		}
		public char ClosingTag
		{
			get
			{
				return this.closingtag;
			}
			set
			{
				this.closingtag = value;
			}
		}
		public BracketHighlightingSheme(char opentag, char closingtag)
		{
			this.opentag = opentag;
			this.closingtag = closingtag;
		}
		public Highlight GetHighlight(IDocument document, int offset)
		{
			int num;
			if (document.TextEditorProperties.BracketMatchingStyle == BracketMatchingStyle.After)
			{
				num = offset;
			}
			else
			{
				num = offset + 1;
			}
			char charAt = document.GetCharAt(Math.Max(0, Math.Min(document.TextLength - 1, num)));
			Point closeBrace = document.OffsetToPosition(offset);
			if (charAt == this.opentag)
			{
				if (offset < document.TextLength)
				{
					int num2 = TextUtilities.SearchBracketForward(document, num + 1, this.opentag, this.closingtag);
					if (num2 >= 0)
					{
						Point openBrace = document.OffsetToPosition(num2);
						return new Highlight(openBrace, closeBrace);
					}
				}
			}
			else
			{
				if (charAt == this.closingtag && offset > 0)
				{
					int num3 = TextUtilities.SearchBracketBackward(document, num - 1, this.opentag, this.closingtag);
					if (num3 >= 0)
					{
						Point openBrace2 = document.OffsetToPosition(num3);
						return new Highlight(openBrace2, closeBrace);
					}
				}
			}
			return null;
		}
	}
}
