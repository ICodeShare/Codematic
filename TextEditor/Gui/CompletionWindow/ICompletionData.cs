using System;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public interface ICompletionData : IComparable
	{
		int ImageIndex
		{
			get;
		}
		string[] Text
		{
			get;
		}
		string Description
		{
			get;
		}
		void InsertAction(TextEditorControl control);
	}
}
