using System;
namespace LTP.TextEditor.Document
{
	public class HighlightInfo
	{
		public bool BlockSpanOn;
		public bool Span;
		public Span CurSpan;
		public HighlightInfo(Span curSpan, bool span, bool blockSpanOn)
		{
			this.CurSpan = curSpan;
			this.Span = span;
			this.BlockSpanOn = blockSpanOn;
		}
	}
}
