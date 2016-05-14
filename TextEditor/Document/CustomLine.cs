using System;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public class CustomLine
	{
		public int StartLineNr;
		public int EndLineNr;
		public Color Color;
		public bool ReadOnly;
		public CustomLine(int lineNr, Color customColor, bool readOnly)
		{
			this.EndLineNr = lineNr;
			this.StartLineNr = lineNr;
			this.Color = customColor;
			this.ReadOnly = readOnly;
		}
		public CustomLine(int startLineNr, int endLineNr, Color customColor, bool readOnly)
		{
			this.StartLineNr = startLineNr;
			this.EndLineNr = endLineNr;
			this.Color = customColor;
			this.ReadOnly = readOnly;
		}
	}
}
