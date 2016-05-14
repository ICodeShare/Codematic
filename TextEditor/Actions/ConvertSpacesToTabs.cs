using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ConvertSpacesToTabs : AbstractSelectionFormatAction
	{
		protected override void Convert(IDocument document, int startOffset, int length)
		{
			string text = document.GetText(startOffset, length);
			string oldValue = new string(' ', document.TextEditorProperties.TabIndent);
			document.Replace(startOffset, length, text.Replace(oldValue, "\t"));
		}
	}
}
