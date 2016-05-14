using System;
using System.Collections;
namespace LTP.TextEditor.Document
{
	public interface IHighlightingStrategy
	{
		string Name
		{
			get;
		}
		string[] Extensions
		{
			get;
			set;
		}
		Hashtable Properties
		{
			get;
		}
		HighlightColor GetColorFor(string name);
		HighlightRuleSet GetRuleSet(Span span);
		HighlightColor GetColor(IDocument document, LineSegment keyWord, int index, int length);
		void MarkTokens(IDocument document, ArrayList lines);
		void MarkTokens(IDocument document);
	}
}
