using System;
namespace LTP.TextEditor.Document
{
	public interface ITextBufferStrategy
	{
		int Length
		{
			get;
		}
		void Insert(int offset, string text);
		void Remove(int offset, int length);
		void Replace(int offset, int length, string text);
		string GetText(int offset, int length);
		char GetCharAt(int offset);
		void SetContent(string text);
	}
}
