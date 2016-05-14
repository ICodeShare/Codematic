using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ToUpperCase : AbstractSelectionFormatAction
	{
		protected override void Convert(IDocument document, int startOffset, int length)
		{
			string text = document.GetText(startOffset, length).ToUpper();
			document.Replace(startOffset, length, text);
		}
	}
}
