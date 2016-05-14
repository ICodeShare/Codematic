using System;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class PrevMarker
	{
		private string what;
		private HighlightColor color;
		private bool markMarker;
		public string What
		{
			get
			{
				return this.what;
			}
		}
		public HighlightColor Color
		{
			get
			{
				return this.color;
			}
		}
		public bool MarkMarker
		{
			get
			{
				return this.markMarker;
			}
		}
		public PrevMarker(XmlElement mark)
		{
			this.color = new HighlightColor(mark);
			this.what = mark.InnerText;
			if (mark.Attributes["markmarker"] != null)
			{
				this.markMarker = bool.Parse(mark.Attributes["markmarker"].InnerText);
			}
		}
	}
}
