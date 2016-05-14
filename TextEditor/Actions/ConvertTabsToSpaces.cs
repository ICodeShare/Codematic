using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ConvertTabsToSpaces : AbstractSelectionFormatAction
	{
		protected override void Convert(IDocument document, int startOffset, int length)
		{
			string text = document.GetText(startOffset, length);
			string newValue = new string(' ', document.TextEditorProperties.TabIndent);
			document.Replace(startOffset, length, text.Replace("\t", newValue));
		}
	}
}
