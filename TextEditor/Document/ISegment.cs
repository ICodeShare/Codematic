using System;
namespace LTP.TextEditor.Document
{
	public interface ISegment
	{
		int Offset
		{
			get;
			set;
		}
		int Length
		{
			get;
			set;
		}
	}
}
