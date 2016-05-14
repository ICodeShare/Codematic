using LTP.TextEditor.Document;
using System;
using System.Text;
namespace LTP.TextEditor.Actions
{
	public class InvertCaseAction : AbstractSelectionFormatAction
	{
		protected override void Convert(IDocument document, int startOffset, int length)
		{
			StringBuilder stringBuilder = new StringBuilder(document.GetText(startOffset, length));
			for (int i = 0; i < stringBuilder.Length; i++)
			{
				stringBuilder[i] = (char.IsUpper(stringBuilder[i]) ? char.ToLower(stringBuilder[i]) : char.ToUpper(stringBuilder[i]));
			}
			document.Replace(startOffset, length, stringBuilder.ToString());
		}
	}
}
