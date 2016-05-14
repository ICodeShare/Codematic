using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class HighlightColor
	{
		private bool systemColor;
		private string systemColorName;
		private bool systemBgColor;
		private string systemBgColorName;
		private Color color;
		private Color backgroundcolor = Color.WhiteSmoke;
		private bool bold;
		private bool italic;
		private bool hasForgeground;
		private bool hasBackground;
		public bool HasForgeground
		{
			get
			{
				return this.hasForgeground;
			}
		}
		public bool HasBackground
		{
			get
			{
				return this.hasBackground;
			}
		}
		public bool Bold
		{
			get
			{
				return this.bold;
			}
		}
		public bool Italic
		{
			get
			{
				return this.italic;
			}
		}
		public Color BackgroundColor
		{
			get
			{
				if (!this.systemBgColor)
				{
					return this.backgroundcolor;
				}
				return this.ParseColorString(this.systemBgColorName);
			}
		}
		public Color Color
		{
			get
			{
				if (!this.systemColor)
				{
					return this.color;
				}
				return this.ParseColorString(this.systemColorName);
			}
		}
		public Font Font
		{
			get
			{
				if (this.Bold)
				{
					if (!this.Italic)
					{
						return FontContainer.BoldFont;
					}
					return FontContainer.BoldItalicFont;
				}
				else
				{
					if (!this.Italic)
					{
						return FontContainer.DefaultFont;
					}
					return FontContainer.ItalicFont;
				}
			}
		}
		private Color ParseColorString(string colorName)
		{
			string[] array = colorName.Split(new char[]
			{
				'*'
			});
			PropertyInfo property = typeof(SystemColors).GetProperty(array[0], BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			Color result = (Color)property.GetValue(null, null);
			if (array.Length == 2)
			{
				double num = double.Parse(array[1]) / 100.0;
				result = Color.FromArgb((int)((double)result.R * num), (int)((double)result.G * num), (int)((double)result.B * num));
			}
			return result;
		}
		public HighlightColor(XmlElement el)
		{
			if (el.Attributes["bold"] != null)
			{
				this.bold = bool.Parse(el.Attributes["bold"].InnerText);
			}
			if (el.Attributes["italic"] != null)
			{
				this.italic = bool.Parse(el.Attributes["italic"].InnerText);
			}
			if (el.Attributes["color"] != null)
			{
				string innerText = el.Attributes["color"].InnerText;
				if (innerText[0] == '#')
				{
					this.color = HighlightColor.ParseColor(innerText);
				}
				else
				{
					if (innerText.StartsWith("SystemColors."))
					{
						this.systemColor = true;
						this.systemColorName = innerText.Substring("SystemColors.".Length);
					}
					else
					{
						this.color = (Color)this.Color.GetType().InvokeMember(innerText, BindingFlags.GetProperty, null, this.Color, new object[0]);
					}
				}
				this.hasForgeground = true;
			}
			else
			{
				this.color = Color.Transparent;
			}
			if (el.Attributes["bgcolor"] != null)
			{
				string innerText2 = el.Attributes["bgcolor"].InnerText;
				if (innerText2[0] == '#')
				{
					this.backgroundcolor = HighlightColor.ParseColor(innerText2);
				}
				else
				{
					if (innerText2.StartsWith("SystemColors."))
					{
						this.systemBgColor = true;
						this.systemBgColorName = innerText2.Substring("SystemColors.".Length);
					}
					else
					{
						this.backgroundcolor = (Color)this.Color.GetType().InvokeMember(innerText2, BindingFlags.GetProperty, null, this.Color, new object[0]);
					}
				}
				this.hasBackground = true;
			}
		}
		public HighlightColor(XmlElement el, HighlightColor defaultColor)
		{
			if (el.Attributes["bold"] != null)
			{
				this.bold = bool.Parse(el.Attributes["bold"].InnerText);
			}
			else
			{
				this.bold = defaultColor.Bold;
			}
			if (el.Attributes["italic"] != null)
			{
				this.italic = bool.Parse(el.Attributes["italic"].InnerText);
			}
			else
			{
				this.italic = defaultColor.Italic;
			}
			if (el.Attributes["color"] != null)
			{
				string innerText = el.Attributes["color"].InnerText;
				if (innerText[0] == '#')
				{
					this.color = HighlightColor.ParseColor(innerText);
				}
				else
				{
					if (innerText.StartsWith("SystemColors."))
					{
						this.systemColor = true;
						this.systemColorName = innerText.Substring("SystemColors.".Length);
					}
					else
					{
						this.color = (Color)this.Color.GetType().InvokeMember(innerText, BindingFlags.GetProperty, null, this.Color, new object[0]);
					}
				}
				this.hasForgeground = true;
			}
			else
			{
				this.color = defaultColor.color;
			}
			if (el.Attributes["bgcolor"] != null)
			{
				string innerText2 = el.Attributes["bgcolor"].InnerText;
				if (innerText2[0] == '#')
				{
					this.backgroundcolor = HighlightColor.ParseColor(innerText2);
				}
				else
				{
					if (innerText2.StartsWith("SystemColors."))
					{
						this.systemBgColor = true;
						this.systemBgColorName = innerText2.Substring("SystemColors.".Length);
					}
					else
					{
						this.backgroundcolor = (Color)this.Color.GetType().InvokeMember(innerText2, BindingFlags.GetProperty, null, this.Color, new object[0]);
					}
				}
				this.hasBackground = true;
				return;
			}
			this.backgroundcolor = defaultColor.BackgroundColor;
		}
		public HighlightColor(Color color, bool bold, bool italic)
		{
			this.hasForgeground = true;
			this.color = color;
			this.bold = bold;
			this.italic = italic;
		}
		public HighlightColor(Color color, Color backgroundcolor, bool bold, bool italic)
		{
			this.hasForgeground = true;
			this.hasBackground = true;
			this.color = color;
			this.backgroundcolor = backgroundcolor;
			this.bold = bold;
			this.italic = italic;
		}
		public HighlightColor(string systemColor, string systemBackgroundColor, bool bold, bool italic)
		{
			this.hasForgeground = true;
			this.hasBackground = true;
			this.systemColor = true;
			this.systemColorName = systemColor;
			this.systemBgColor = true;
			this.systemBgColorName = systemBackgroundColor;
			this.bold = bold;
			this.italic = italic;
		}
		private static Color ParseColor(string c)
		{
			int alpha = 255;
			int num = 0;
			if (c.Length > 7)
			{
				num = 2;
				alpha = int.Parse(c.Substring(1, 2), NumberStyles.HexNumber);
			}
			int red = int.Parse(c.Substring(1 + num, 2), NumberStyles.HexNumber);
			int green = int.Parse(c.Substring(3 + num, 2), NumberStyles.HexNumber);
			int blue = int.Parse(c.Substring(5 + num, 2), NumberStyles.HexNumber);
			return Color.FromArgb(alpha, red, green, blue);
		}
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[HighlightColor: Bold = ",
				this.Bold,
				", Italic = ",
				this.Italic,
				", Color = ",
				this.Color,
				", BackgroundColor = ",
				this.BackgroundColor,
				"]"
			});
		}
	}
}
