using System;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public class FontContainer
	{
		private static Font defaultfont;
		private static Font boldfont;
		private static Font italicfont;
		private static Font bolditalicfont;
		public static Font BoldFont
		{
			get
			{
				return FontContainer.boldfont;
			}
		}
		public static Font ItalicFont
		{
			get
			{
				return FontContainer.italicfont;
			}
		}
		public static Font BoldItalicFont
		{
			get
			{
				return FontContainer.bolditalicfont;
			}
		}
		public static Font DefaultFont
		{
			get
			{
				return FontContainer.defaultfont;
			}
			set
			{
				FontContainer.defaultfont = value;
				FontContainer.boldfont = new Font(FontContainer.defaultfont, FontStyle.Bold);
				FontContainer.italicfont = new Font(FontContainer.defaultfont, FontStyle.Italic);
				FontContainer.bolditalicfont = new Font(FontContainer.defaultfont, FontStyle.Bold | FontStyle.Italic);
			}
		}
		public static Font ParseFont(string font)
		{
			string[] array = font.Split(new char[]
			{
				',',
				'='
			});
			return new Font(array[1], float.Parse(array[3]));
		}
		static FontContainer()
		{
			FontContainer.DefaultFont = new Font("Courier New", 10f);
		}
	}
}
