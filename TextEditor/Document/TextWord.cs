using System;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public class TextWord
	{
		public class SpaceTextWord : TextWord
		{
			public override TextWordType Type
			{
				get
				{
					return TextWordType.Space;
				}
			}
			public override bool IsWhiteSpace
			{
				get
				{
					return true;
				}
			}
			public SpaceTextWord()
			{
				this.length = 1;
			}
			public SpaceTextWord(HighlightColor color)
			{
				this.length = 1;
				this.color = color;
			}
		}
		public class TabTextWord : TextWord
		{
			public override TextWordType Type
			{
				get
				{
					return TextWordType.Tab;
				}
			}
			public override bool IsWhiteSpace
			{
				get
				{
					return true;
				}
			}
			public TabTextWord()
			{
				this.length = 1;
			}
			public TabTextWord(HighlightColor color)
			{
				this.length = 1;
				this.color = color;
			}
		}
		private HighlightColor color;
		private LineSegment line;
		private IDocument document;
		private int offset;
		private int length;
		private static TextWord spaceWord = new TextWord.SpaceTextWord();
		private static TextWord tabWord = new TextWord.TabTextWord();
		public bool hasDefaultColor;
		public static TextWord Space
		{
			get
			{
				return TextWord.spaceWord;
			}
		}
		public static TextWord Tab
		{
			get
			{
				return TextWord.tabWord;
			}
		}
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}
		public int Length
		{
			get
			{
				return this.length;
			}
		}
		public bool HasDefaultColor
		{
			get
			{
				return this.hasDefaultColor;
			}
		}
		public virtual TextWordType Type
		{
			get
			{
				return TextWordType.Word;
			}
		}
		public string Word
		{
			get
			{
				if (this.document == null)
				{
					return string.Empty;
				}
				return this.document.GetText(this.line.Offset + this.offset, this.length);
			}
		}
		public Font Font
		{
			get
			{
				return this.color.Font;
			}
		}
		public Color Color
		{
			get
			{
				return this.color.Color;
			}
		}
		public HighlightColor SyntaxColor
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}
		public virtual bool IsWhiteSpace
		{
			get
			{
				return false;
			}
		}
		protected TextWord()
		{
		}
		public TextWord(IDocument document, LineSegment line, int offset, int length, HighlightColor color, bool hasDefaultColor)
		{
			this.document = document;
			this.line = line;
			this.offset = offset;
			this.length = length;
			this.color = color;
			this.hasDefaultColor = hasDefaultColor;
		}
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[TextWord: Word = ",
				this.Word,
				", Font = ",
				this.Font,
				", Color = ",
				this.Color,
				"]"
			});
		}
	}
}
