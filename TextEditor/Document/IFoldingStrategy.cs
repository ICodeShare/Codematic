using System;
using System.Collections;
namespace LTP.TextEditor.Document
{
	public interface IFoldingStrategy
	{
		ArrayList GenerateFoldMarkers(IDocument document, string fileName, object parseInformation);
	}
}
