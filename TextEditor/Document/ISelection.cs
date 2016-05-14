using System;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public interface ISelection
	{
		Point StartPosition
		{
			get;
			set;
		}
		Point EndPosition
		{
			get;
			set;
		}
		int Offset
		{
			get;
		}
		int EndOffset
		{
			get;
		}
		int Length
		{
			get;
		}
		bool IsRectangularSelection
		{
			get;
		}
		bool IsEmpty
		{
			get;
		}
		string SelectedText
		{
			get;
		}
		bool ContainsOffset(int offset);
		bool ContainsPosition(Point position);
	}
}
