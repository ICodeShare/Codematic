using System;
using System.Drawing;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class HighlightBackground : HighlightColor
	{
		private Image backgroundImage;
		public Image BackgroundImage
		{
			get
			{
				return this.backgroundImage;
			}
		}
		public HighlightBackground(XmlElement el) : base(el)
		{
			if (el.Attributes["image"] != null)
			{
				this.backgroundImage = new Bitmap(el.Attributes["image"].InnerText);
			}
		}
		public HighlightBackground(Color color, Color backgroundcolor, bool bold, bool italic) : base(color, backgroundcolor, bold, italic)
		{
		}
		public HighlightBackground(string systemColor, string systemBackgroundColor, bool bold, bool italic) : base(systemColor, systemBackgroundColor, bold, italic)
		{
		}
	}
}
