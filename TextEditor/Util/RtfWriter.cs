using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
using System.Text;
namespace LTP.TextEditor.Util
{
	public class RtfWriter
	{
		private static Hashtable colors;
		private static int colorNum;
		private static StringBuilder colorString;
		public static string GenerateRtf(TextArea textArea)
		{
			try
			{
				RtfWriter.colors = new Hashtable();
				RtfWriter.colorNum = 0;
				RtfWriter.colorString = new StringBuilder();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1031");
				RtfWriter.BuildFontTable(textArea.Document, stringBuilder);
				stringBuilder.Append('\n');
				string value = RtfWriter.BuildFileContent(textArea);
				RtfWriter.BuildColorTable(textArea.Document, stringBuilder);
				stringBuilder.Append('\n');
				stringBuilder.Append("\\viewkind4\\uc1\\pard");
				stringBuilder.Append(value);
				stringBuilder.Append("}");
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return null;
		}
		private static void BuildColorTable(IDocument doc, StringBuilder rtf)
		{
			rtf.Append("{\\colortbl ;");
			rtf.Append(RtfWriter.colorString.ToString());
			rtf.Append("}");
		}
		private static void BuildFontTable(IDocument doc, StringBuilder rtf)
		{
			rtf.Append("{\\fonttbl");
			rtf.Append("{\\f0\\fmodern\\fprq1\\fcharset0 " + FontContainer.DefaultFont.Name + ";}");
			rtf.Append("}");
		}
		private static string BuildFileContent(TextArea textArea)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			Color right = Color.Black;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			foreach (ISelection current in textArea.SelectionManager.SelectionCollection)
			{
				int num = textArea.Document.PositionToOffset(current.StartPosition);
				int num2 = textArea.Document.PositionToOffset(current.EndPosition);
				for (int i = current.StartPosition.Y; i <= current.EndPosition.Y; i++)
				{
					LineSegment lineSegment = textArea.Document.GetLineSegment(i);
					int num3 = lineSegment.Offset;
					if (lineSegment.Words != null)
					{
						foreach (TextWord textWord in lineSegment.Words)
						{
							switch (textWord.Type)
							{
							case TextWordType.Word:
							{
								Color color = textWord.Color;
								if (num3 + textWord.Word.Length > num && num3 < num2)
								{
									string key = string.Concat(new object[]
									{
										color.R,
										", ",
										color.G,
										", ",
										color.B
									});
									if (RtfWriter.colors[key] == null)
									{
										RtfWriter.colors[key] = ++RtfWriter.colorNum;
										RtfWriter.colorString.Append(string.Concat(new object[]
										{
											"\\red",
											color.R,
											"\\green",
											color.G,
											"\\blue",
											color.B,
											";"
										}));
									}
									if (color != right || flag)
									{
										stringBuilder.Append("\\cf" + RtfWriter.colors[key].ToString());
										right = color;
										flag4 = true;
									}
									if (flag2 != textWord.Font.Italic)
									{
										if (textWord.Font.Italic)
										{
											stringBuilder.Append("\\i");
										}
										else
										{
											stringBuilder.Append("\\i0");
										}
										flag2 = textWord.Font.Italic;
										flag4 = true;
									}
									if (flag3 != textWord.Font.Bold)
									{
										if (textWord.Font.Bold)
										{
											stringBuilder.Append("\\b");
										}
										else
										{
											stringBuilder.Append("\\b0");
										}
										flag3 = textWord.Font.Bold;
										flag4 = true;
									}
									if (flag)
									{
										stringBuilder.Append("\\f0\\fs" + FontContainer.DefaultFont.Size * 2f);
										flag = false;
									}
									if (flag4)
									{
										stringBuilder.Append(' ');
										flag4 = false;
									}
									string text;
									if (num3 < num)
									{
										text = textWord.Word.Substring(num - num3);
									}
									else
									{
										if (num3 + textWord.Word.Length > num2)
										{
											text = textWord.Word.Substring(0, num3 + textWord.Word.Length - num2);
										}
										else
										{
											text = textWord.Word;
										}
									}
									stringBuilder.Append(text.Replace("{", "\\{").Replace("}", "\\}"));
								}
								num3 += textWord.Length;
								break;
							}
							case TextWordType.Space:
								if (current.ContainsOffset(num3))
								{
									stringBuilder.Append(' ');
								}
								num3++;
								break;
							case TextWordType.Tab:
								if (current.ContainsOffset(num3))
								{
									stringBuilder.Append("\\tab");
								}
								num3++;
								flag4 = true;
								break;
							}
						}
						if (num3 < num2)
						{
							stringBuilder.Append("\\par");
						}
						stringBuilder.Append('\n');
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
