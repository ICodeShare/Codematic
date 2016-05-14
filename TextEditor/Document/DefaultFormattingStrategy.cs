using System;
using System.Text;
namespace LTP.TextEditor.Document
{
	public class DefaultFormattingStrategy : IFormattingStrategy
	{
		protected string GetIndentation(TextArea textArea, int lineNumber)
		{
			if (lineNumber < 0 || lineNumber > textArea.Document.TotalNumberOfLines)
			{
				throw new ArgumentOutOfRangeException("lineNumber");
			}
			string lineAsString = TextUtilities.GetLineAsString(textArea.Document, lineNumber);
			StringBuilder stringBuilder = new StringBuilder();
			string text = lineAsString;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}
		protected virtual int AutoIndentLine(TextArea textArea, int lineNumber)
		{
			string text = (lineNumber != 0) ? this.GetIndentation(textArea, lineNumber - 1) : "";
			if (text.Length > 0)
			{
				string text2 = text + TextUtilities.GetLineAsString(textArea.Document, lineNumber).Trim();
				LineSegment lineSegment = textArea.Document.GetLineSegment(lineNumber);
				textArea.Document.Replace(lineSegment.Offset, lineSegment.Length, text2);
			}
			return text.Length;
		}
		protected virtual int SmartIndentLine(TextArea textArea, int line)
		{
			return this.AutoIndentLine(textArea, line);
		}
		public virtual int FormatLine(TextArea textArea, int line, int cursorOffset, char ch)
		{
			if (ch == '\n')
			{
				return this.IndentLine(textArea, line);
			}
			return 0;
		}
		public int IndentLine(TextArea textArea, int line)
		{
			switch (textArea.Document.TextEditorProperties.IndentStyle)
			{
			case IndentStyle.Auto:
				return this.AutoIndentLine(textArea, line);
			case IndentStyle.Smart:
				return this.SmartIndentLine(textArea, line);
			}
			return 0;
		}
		public virtual void IndentLines(TextArea textArea, int begin, int end)
		{
			int num = 0;
			for (int i = begin; i <= end; i++)
			{
				if (this.IndentLine(textArea, i) > 0)
				{
					num++;
				}
			}
			if (num > 0)
			{
				textArea.Document.UndoStack.UndoLast(num);
			}
		}
		public virtual int SearchBracketBackward(IDocument document, int offset, char openBracket, char closingBracket)
		{
			int num = -1;
			for (int i = offset; i > 0; i--)
			{
				char charAt = document.GetCharAt(i);
				if (charAt == openBracket)
				{
					num++;
					if (num == 0)
					{
						return i;
					}
				}
				else
				{
					if (charAt == closingBracket)
					{
						num--;
					}
					else
					{
						if (charAt == '"' || charAt == '\'' || (charAt == '/' && i > 0 && (document.GetCharAt(i - 1) == '/' || document.GetCharAt(i - 1) == '*')))
						{
							break;
						}
					}
				}
			}
			return -1;
		}
		public virtual int SearchBracketForward(IDocument document, int offset, char openBracket, char closingBracket)
		{
			int num = 1;
			for (int i = offset; i < document.TextLength; i++)
			{
				char charAt = document.GetCharAt(i);
				if (charAt == openBracket)
				{
					num++;
				}
				else
				{
					if (charAt == closingBracket)
					{
						num--;
						if (num == 0)
						{
							return i;
						}
					}
					else
					{
						if (charAt == '"' || charAt == '\'')
						{
							break;
						}
						if (charAt == '/' && i > 0)
						{
							if (document.GetCharAt(i - 1) == '/')
							{
								break;
							}
						}
						else
						{
							if (charAt == '*' && i > 0 && document.GetCharAt(i - 1) == '/')
							{
								break;
							}
						}
					}
				}
			}
			return -1;
		}
	}
}
