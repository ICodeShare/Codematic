using System;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public class TextMarker : AbstractSegment
	{
		private TextMarkerType textMarkerType;
		private Color color;
		private string toolTip;
		public TextMarkerType TextMarkerType
		{
			get
			{
				return this.textMarkerType;
			}
		}
		public Color Color
		{
			get
			{
				return this.color;
			}
		}
		public string ToolTip
		{
			get
			{
				return this.toolTip;
			}
			set
			{
				this.toolTip = value;
			}
		}
		public TextMarker(int offset, int length, TextMarkerType textMarkerType) : this(offset, length, textMarkerType, Color.Red)
		{
		}
		public TextMarker(int offset, int length, TextMarkerType textMarkerType, Color color)
		{
			this.offset = offset;
			this.length = length;
			this.textMarkerType = textMarkerType;
			this.color = color;
		}
	}
}
