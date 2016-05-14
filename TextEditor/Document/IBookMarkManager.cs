using System;
using System.Collections;
namespace LTP.TextEditor.Document
{
	public interface IBookMarkManager
	{
		event EventHandler BeforeChanged;
		event EventHandler Changed;
		ArrayList Marks
		{
			get;
		}
		int FirstMark
		{
			get;
		}
		int LastMark
		{
			get;
		}
		void ToggleMarkAt(int lineNr);
		bool IsMarked(int lineNr);
		void Clear();
		int GetNextMark(int lineNr);
		int GetPrevMark(int lineNr);
	}
}
