using System;
using System.Collections;
using System.Drawing;
using System.Text;
namespace LTP.TextEditor.Document
{
	public class LineSegment : AbstractSegment
	{
		private int delimiterLength;
		private ArrayList words;
		private Stack highlightSpanStack;
		public override int Length
		{
			get
			{
				return this.length - this.delimiterLength;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
		public int TotalLength
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}
		public int DelimiterLength
		{
			get
			{
				return this.delimiterLength;
			}
			set
			{
				this.delimiterLength = value;
			}
		}
		public ArrayList Words
		{
			get
			{
				return this.words;
			}
			set
			{
				this.words = value;
			}
		}
		public Stack HighlightSpanStack
		{
			get
			{
				return this.highlightSpanStack;
			}
			set
			{
				this.highlightSpanStack = value;
			}
		}
		public HighlightColor GetColorForPosition(int x)
		{
			if (this.Words != null)
			{
				int num = 0;
				foreach (TextWord textWord in this.Words)
				{
					if (x < num + textWord.Length)
					{
						return textWord.SyntaxColor;
					}
					num += textWord.Length;
				}
			}
			return new HighlightColor(Color.Black, false, false);
		}
		public LineSegment(int offset, int end, int delimiterLength)
		{
			this.offset = offset;
			this.delimiterLength = delimiterLength;
			this.TotalLength = end - offset + 1;
		}
		public LineSegment(int offset, int length)
		{
			this.offset = offset;
			this.length = length;
			this.delimiterLength = 0;
		}
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[LineSegment: Offset = ",
				this.offset,
				", Length = ",
				this.Length,
				", TotalLength = ",
				this.TotalLength,
				", DelimiterLength = ",
				this.delimiterLength,
				"]"
			});
		}
		internal string GetRegString(char[] expr, int index, IDocument document)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < expr.Length && index + num < this.Length)
			{
				char c = expr[i];
				if (c == '@')
				{
					i++;
					char c2 = expr[i];
					if (c2 != '!')
					{
						if (c2 == '@')
						{
							stringBuilder.Append(document.GetCharAt(this.Offset + index + num));
						}
					}
					else
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						i++;
						while (i < expr.Length)
						{
							if (expr[i] == '@')
							{
								break;
							}
							stringBuilder2.Append(expr[i++]);
						}
					}
				}
				else
				{
					if (expr[i] != document.GetCharAt(this.Offset + index + num))
					{
						return stringBuilder.ToString();
					}
					stringBuilder.Append(document.GetCharAt(this.Offset + index + num));
				}
				i++;
				num++;
			}
			return stringBuilder.ToString();
		}
		internal bool MatchExpr(char[] expr, int index, IDocument document)
		{
			int i = 0;
			int num = 0;
			while (i < expr.Length)
			{
				char c = expr[i];
				if (c == '@')
				{
					i++;
					if (i < expr.Length)
					{
						char c2 = expr[i];
						if (c2 != '!')
						{
							if (c2 != '-')
							{
								if (c2 == '@')
								{
									if (index + num >= this.Length || '@' != document.GetCharAt(this.Offset + index + num))
									{
										return false;
									}
								}
							}
							else
							{
								StringBuilder stringBuilder = new StringBuilder();
								i++;
								while (i < expr.Length && expr[i] != '@')
								{
									stringBuilder.Append(expr[i++]);
								}
								if (index - stringBuilder.Length >= 0)
								{
									int num2 = 0;
									while (num2 < stringBuilder.Length && document.GetCharAt(this.Offset + index - stringBuilder.Length + num2) == stringBuilder[num2])
									{
										num2++;
									}
									if (num2 >= stringBuilder.Length)
									{
										return false;
									}
								}
							}
						}
						else
						{
							StringBuilder stringBuilder2 = new StringBuilder();
							i++;
							while (i < expr.Length && expr[i] != '@')
							{
								stringBuilder2.Append(expr[i++]);
							}
							if (this.Offset + index + num + stringBuilder2.Length < document.TextLength)
							{
								int num3 = 0;
								while (num3 < stringBuilder2.Length && document.GetCharAt(this.Offset + index + num + num3) == stringBuilder2[num3])
								{
									num3++;
								}
								if (num3 >= stringBuilder2.Length)
								{
									return false;
								}
							}
						}
					}
				}
				else
				{
					if (index + num >= this.Length || expr[i] != document.GetCharAt(this.Offset + index + num))
					{
						return false;
					}
				}
				i++;
				num++;
			}
			return true;
		}
	}
}
