using System;
namespace LTP.TextEditor
{
	public enum TextAreaUpdateType
	{
		WholeTextArea,
		SingleLine,
		SinglePosition,
		PositionToLineEnd,
		PositionToEnd,
		LinesBetween
	}
}
