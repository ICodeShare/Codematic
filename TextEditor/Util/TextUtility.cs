using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Util
{
	public class TextUtility
	{
		public static bool RegionRegexMatches(IDocument document, int offset, int length, string regex)
		{
			return document.TextLength >= offset + length;
		}
		public static bool RegionMatches(IDocument document, int offset, int length, string word)
		{
			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (document.GetCharAt(offset + i) != word[i])
				{
					return false;
				}
			}
			return true;
		}
		public static bool RegionMatches(IDocument document, bool casesensitive, int offset, int length, string word)
		{
			if (casesensitive)
			{
				return TextUtility.RegionMatches(document, offset, length, word);
			}
			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (char.ToUpper(document.GetCharAt(offset + i)) != char.ToUpper(word[i]))
				{
					return false;
				}
			}
			return true;
		}
		public static bool RegionMatches(IDocument document, int offset, int length, char[] word)
		{
			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (document.GetCharAt(offset + i) != word[i])
				{
					return false;
				}
			}
			return true;
		}
		public static bool RegionMatches(IDocument document, bool casesensitive, int offset, int length, char[] word)
		{
			if (casesensitive)
			{
				return TextUtility.RegionMatches(document, offset, length, word);
			}
			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (char.ToUpper(document.GetCharAt(offset + i)) != char.ToUpper(word[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
