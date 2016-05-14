using System;
using System.Text;
namespace LTP.TextEditor.Document
{
	public sealed class TextUtilities
	{
		public enum CharacterType
		{
			LetterDigitOrUnderscore,
			WhiteSpace,
			Other
		}
		public static string LeadingWhiteSpaceToTabs(string line, int tabIndent)
		{
			StringBuilder stringBuilder = new StringBuilder(line.Length);
			int num = 0;
			int i;
			for (i = 0; i < line.Length; i++)
			{
				if (line[i] == ' ')
				{
					num++;
					if (num == tabIndent)
					{
						stringBuilder.Append('\t');
						num = 0;
					}
				}
				else
				{
					if (line[i] != '\t')
					{
						break;
					}
					stringBuilder.Append('\t');
					num = 0;
				}
			}
			if (i < line.Length)
			{
				stringBuilder.Append(line.Substring(i - num));
			}
			return stringBuilder.ToString();
		}
		public static bool IsLetterDigitOrUnderscore(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_';
		}
		public static string GetExpressionBeforeOffset(TextArea textArea, int initialOffset)
		{
			IDocument document = textArea.Document;
			int num = initialOffset;
			while (num - 1 > 0)
			{
				char charAt = document.GetCharAt(num - 1);
				if (charAt <= ')')
				{
					if (charAt <= '\r')
					{
						if (charAt == '\n' || charAt == '\r')
						{
							break;
						}
					}
					else
					{
						if (charAt != '"')
						{
							switch (charAt)
							{
							case '\'':
								if (num < initialOffset - 1)
								{
									return null;
								}
								return "'a'";
							case ')':
								num = TextUtilities.SearchBracketBackward(document, num - 2, '(', ')');
								continue;
							}
						}
						else
						{
							if (num < initialOffset - 1)
							{
								return null;
							}
							return "\"\"";
						}
					}
				}
				else
				{
					if (charAt <= '>')
					{
						if (charAt == '.')
						{
							num--;
							continue;
						}
						if (charAt == '>')
						{
							if (document.GetCharAt(num - 2) == '-')
							{
								num -= 2;
								continue;
							}
							break;
						}
					}
					else
					{
						if (charAt == ']')
						{
							num = TextUtilities.SearchBracketBackward(document, num - 2, '[', ']');
							continue;
						}
						if (charAt == '}')
						{
							break;
						}
					}
				}
				if (char.IsWhiteSpace(document.GetCharAt(num - 1)))
				{
					num--;
				}
				else
				{
					int num2 = num - 1;
					if (!TextUtilities.IsLetterDigitOrUnderscore(document.GetCharAt(num2)))
					{
						break;
					}
					while (num2 > 0 && TextUtilities.IsLetterDigitOrUnderscore(document.GetCharAt(num2 - 1)))
					{
						num2--;
					}
					string text = document.GetText(num2, num - num2).Trim();
					string a;
					if (((a = text) != null && (a == "ref" || a == "out" || a == "in" || a == "return" || a == "throw" || a == "case")) || (text.Length > 0 && !TextUtilities.IsLetterDigitOrUnderscore(text[0])))
					{
						break;
					}
					num = num2;
				}
			}
			string text2 = document.GetText(num, textArea.Caret.Offset - num).Trim();
			int num3 = text2.LastIndexOf('\n');
			if (num3 >= 0)
			{
				num += num3 + 1;
			}
			string text3 = document.GetText(num, textArea.Caret.Offset - num).Trim();
			Console.WriteLine("Expr: >" + text3 + "<");
			return text3;
		}
		public static TextUtilities.CharacterType GetCharacterType(char c)
		{
			if (TextUtilities.IsLetterDigitOrUnderscore(c))
			{
				return TextUtilities.CharacterType.LetterDigitOrUnderscore;
			}
			if (char.IsWhiteSpace(c))
			{
				return TextUtilities.CharacterType.WhiteSpace;
			}
			return TextUtilities.CharacterType.Other;
		}
		public static int GetFirstNonWSChar(IDocument document, int offset)
		{
			while (offset < document.TextLength && char.IsWhiteSpace(document.GetCharAt(offset)))
			{
				offset++;
			}
			return offset;
		}
		public static int FindWordEnd(IDocument document, int offset)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			int num = lineSegmentForOffset.Offset + lineSegmentForOffset.Length;
			while (offset < num && TextUtilities.IsLetterDigitOrUnderscore(document.GetCharAt(offset)))
			{
				offset++;
			}
			return offset;
		}
		public static int FindWordStart(IDocument document, int offset)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			while (offset > lineSegmentForOffset.Offset && !TextUtilities.IsLetterDigitOrUnderscore(document.GetCharAt(offset - 1)))
			{
				offset--;
			}
			return offset;
		}
		public static int FindNextWordStart(IDocument document, int offset)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			int num = lineSegmentForOffset.Offset + lineSegmentForOffset.Length;
			TextUtilities.CharacterType characterType = TextUtilities.GetCharacterType(document.GetCharAt(offset));
			while (offset < num)
			{
				if (TextUtilities.GetCharacterType(document.GetCharAt(offset)) != characterType)
				{
					break;
				}
				offset++;
			}
			while (offset < num && TextUtilities.GetCharacterType(document.GetCharAt(offset)) == TextUtilities.CharacterType.WhiteSpace)
			{
				offset++;
			}
			return offset;
		}
		public static int FindPrevWordStart(IDocument document, int offset)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			if (offset > 0)
			{
				TextUtilities.CharacterType characterType = TextUtilities.GetCharacterType(document.GetCharAt(offset - 1));
				while (offset > lineSegmentForOffset.Offset && TextUtilities.GetCharacterType(document.GetCharAt(offset - 1)) == characterType)
				{
					offset--;
				}
				if (characterType == TextUtilities.CharacterType.WhiteSpace && offset > lineSegmentForOffset.Offset)
				{
					characterType = TextUtilities.GetCharacterType(document.GetCharAt(offset - 1));
					while (offset > lineSegmentForOffset.Offset && TextUtilities.GetCharacterType(document.GetCharAt(offset - 1)) == characterType)
					{
						offset--;
					}
				}
			}
			return offset;
		}
		public static string GetLineAsString(IDocument document, int lineNumber)
		{
			LineSegment lineSegment = document.GetLineSegment(lineNumber);
			return document.GetText(lineSegment.Offset, lineSegment.Length);
		}
		public static int SearchBracketBackward(IDocument document, int offset, char openBracket, char closingBracket)
		{
			return document.FormattingStrategy.SearchBracketBackward(document, offset, openBracket, closingBracket);
		}
		public static int SearchBracketForward(IDocument document, int offset, char openBracket, char closingBracket)
		{
			return document.FormattingStrategy.SearchBracketForward(document, offset, openBracket, closingBracket);
		}
		public static bool IsEmptyLine(IDocument document, int lineNumber)
		{
			return TextUtilities.IsEmptyLine(document, document.GetLineSegment(lineNumber));
		}
		public static bool IsEmptyLine(IDocument document, LineSegment line)
		{
			for (int i = line.Offset; i < line.Offset + line.Length; i++)
			{
				char charAt = document.GetCharAt(i);
				if (!char.IsWhiteSpace(charAt))
				{
					return false;
				}
			}
			return true;
		}
		private static bool IsWordPart(char ch)
		{
			return TextUtilities.IsLetterDigitOrUnderscore(ch) || ch == '.';
		}
		public static string GetWordAt(IDocument document, int offset)
		{
			if (offset < 0 || offset >= document.TextLength - 1 || !TextUtilities.IsWordPart(document.GetCharAt(offset)))
			{
				return string.Empty;
			}
			int i = offset;
			int num = offset;
			while (i > 0)
			{
				if (!TextUtilities.IsWordPart(document.GetCharAt(i - 1)))
				{
					break;
				}
				i--;
			}
			while (num < document.TextLength - 1 && TextUtilities.IsWordPart(document.GetCharAt(num + 1)))
			{
				num++;
			}
			return document.GetText(i, num - i + 1);
		}
	}
}
