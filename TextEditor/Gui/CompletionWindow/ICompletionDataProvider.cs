using System;
using System.Windows.Forms;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public interface ICompletionDataProvider
	{
		ImageList ImageList
		{
			get;
		}
		string PreSelection
		{
			get;
		}
		ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped);
	}
}
