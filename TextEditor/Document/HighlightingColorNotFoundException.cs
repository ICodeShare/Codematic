using System;
namespace LTP.TextEditor.Document
{
	public class HighlightingColorNotFoundException : Exception
	{
		public HighlightingColorNotFoundException(string name) : base(name)
		{
		}
	}
}
