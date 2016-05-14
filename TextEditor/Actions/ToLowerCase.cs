using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ToLowerCase : AbstractSelectionFormatAction
	{
		protected override void Convert(IDocument document, int startOffset, int length)
		{
			string text = document.GetText(startOffset, length).ToLower();
			document.Replace(startOffset, length, text);
		}
	}
}
