using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor.Document
{
	public class DefaultHighlightingStrategy : IHighlightingStrategy
	{
		private string name;
		private ArrayList rules = new ArrayList();
		private Hashtable environmentColors = new Hashtable();
		private Hashtable properties = new Hashtable();
		private string[] extensions;
		private HighlightColor digitColor;
		private HighlightRuleSet defaultRuleSet;
		private LineSegment currentLine;
		private Stack currentSpanStack;
		private bool inSpan;
		private Span activeSpan;
		private HighlightRuleSet activeRuleSet;
		private int currentOffset;
		private int currentLength;
		public HighlightColor DigitColor
		{
			get
			{
				return this.digitColor;
			}
			set
			{
				this.digitColor = value;
			}
		}
		public Hashtable Properties
		{
			get
			{
				return this.properties;
			}
		}
		public string Name
		{
			get
			{
				return this.name;
			}
		}
		public string[] Extensions
		{
			get
			{
				return this.extensions;
			}
			set
			{
				this.extensions = value;
			}
		}
		public ArrayList Rules
		{
			get
			{
				return this.rules;
			}
		}
		public DefaultHighlightingStrategy() : this("Default")
		{
		}
		public DefaultHighlightingStrategy(string name)
		{
			this.name = name;
			this.digitColor = new HighlightBackground("WindowText", "Window", false, false);
			this.environmentColors["DefaultBackground"] = new HighlightBackground("WindowText", "Window", false, false);
			this.environmentColors["Default"] = new HighlightColor(SystemColors.WindowText, false, false);
			this.environmentColors["Selection"] = new HighlightColor("HighlightText", "Highlight", false, false);
			this.environmentColors["VRuler"] = new HighlightColor("ControlLight", "Window", false, false);
			this.environmentColors["InvalidLines"] = new HighlightColor(Color.Red, false, false);
			this.environmentColors["CaretMarker"] = new HighlightColor(Color.Yellow, false, false);
			this.environmentColors["LineNumbers"] = new HighlightBackground("ControlDark", "Window", false, false);
			this.environmentColors["FoldLine"] = new HighlightColor(Color.FromArgb(128, 128, 128), Color.Black, false, false);
			this.environmentColors["FoldMarker"] = new HighlightColor(Color.FromArgb(128, 128, 128), Color.White, false, false);
			this.environmentColors["SelectedFoldLine"] = new HighlightColor(Color.Black, false, false);
			this.environmentColors["EOLMarkers"] = new HighlightColor("ControlLight", "Window", false, false);
			this.environmentColors["SpaceMarkers"] = new HighlightColor("ControlLight", "Window", false, false);
			this.environmentColors["TabMarkers"] = new HighlightColor("ControlLight", "Window", false, false);
		}
		public HighlightRuleSet FindHighlightRuleSet(string name)
		{
			foreach (HighlightRuleSet highlightRuleSet in this.rules)
			{
				if (highlightRuleSet.Name == name)
				{
					return highlightRuleSet;
				}
			}
			return null;
		}
		public void AddRuleSet(HighlightRuleSet aRuleSet)
		{
			this.rules.Add(aRuleSet);
		}
		internal void ResolveReferences()
		{
			this.ResolveRuleSetReferences();
			this.ResolveExternalReferences();
		}
		private void ResolveRuleSetReferences()
		{
			foreach (HighlightRuleSet highlightRuleSet in this.Rules)
			{
				if (highlightRuleSet.Name == null)
				{
					this.defaultRuleSet = highlightRuleSet;
				}
				foreach (Span span in highlightRuleSet.Spans)
				{
					if (span.Rule != null)
					{
						bool flag = false;
						foreach (HighlightRuleSet highlightRuleSet2 in this.Rules)
						{
							if (highlightRuleSet2.Name == span.Rule)
							{
								flag = true;
								span.RuleSet = highlightRuleSet2;
								break;
							}
						}
						if (!flag)
						{
							MessageBox.Show("The RuleSet " + span.Rule + " could not be found in mode definition " + this.Name, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
							span.RuleSet = null;
						}
					}
					else
					{
						span.RuleSet = null;
					}
				}
			}
			if (this.defaultRuleSet == null)
			{
				MessageBox.Show("No default RuleSet is defined for mode definition " + this.Name, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
		}
		private void ResolveExternalReferences()
		{
			foreach (HighlightRuleSet highlightRuleSet in this.Rules)
			{
				if (highlightRuleSet.Reference != null)
				{
					IHighlightingStrategy highlightingStrategy = HighlightingManager.Manager.FindHighlighter(highlightRuleSet.Reference);
					if (highlightingStrategy != null)
					{
						highlightRuleSet.Highlighter = highlightingStrategy;
					}
					else
					{
						MessageBox.Show(string.Concat(new string[]
						{
							"The mode defintion ",
							highlightRuleSet.Reference,
							" which is refered from the ",
							this.Name,
							" mode definition could not be found"
						}), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						highlightRuleSet.Highlighter = this;
					}
				}
				else
				{
					highlightRuleSet.Highlighter = this;
				}
			}
		}
		internal void SetColorFor(string name, HighlightColor color)
		{
			this.environmentColors[name] = color;
		}
		public HighlightColor GetColorFor(string name)
		{
			if (this.environmentColors[name] == null)
			{
				throw new Exception("Color : " + name + " not found!");
			}
			return (HighlightColor)this.environmentColors[name];
		}
		public HighlightColor GetColor(IDocument document, LineSegment currentSegment, int currentOffset, int currentLength)
		{
			return this.GetColor(this.defaultRuleSet, document, currentSegment, currentOffset, currentLength);
		}
		private HighlightColor GetColor(HighlightRuleSet ruleSet, IDocument document, LineSegment currentSegment, int currentOffset, int currentLength)
		{
			if (ruleSet == null)
			{
				return null;
			}
			if (ruleSet.Reference != null)
			{
				return ruleSet.Highlighter.GetColor(document, currentSegment, currentOffset, currentLength);
			}
			return (HighlightColor)ruleSet.KeyWords[document, currentSegment, currentOffset, currentLength];
		}
		public HighlightRuleSet GetRuleSet(Span aSpan)
		{
			if (aSpan == null)
			{
				return this.defaultRuleSet;
			}
			if (aSpan.RuleSet == null)
			{
				return null;
			}
			if (aSpan.RuleSet.Reference != null)
			{
				return aSpan.RuleSet.Highlighter.GetRuleSet(null);
			}
			return aSpan.RuleSet;
		}
		public void MarkTokens(IDocument document)
		{
			try
			{
				if (this.Rules.Count != 0)
				{
					for (int i = 0; i < document.TotalNumberOfLines; i++)
					{
						LineSegment lineSegment = (i > 0) ? document.GetLineSegment(i - 1) : null;
						if (i >= document.LineSegmentCollection.Count)
						{
							break;
						}
						this.currentSpanStack = ((lineSegment != null && lineSegment.HighlightSpanStack != null) ? ((Stack)lineSegment.HighlightSpanStack.Clone()) : null);
						if (this.currentSpanStack != null)
						{
							while (this.currentSpanStack.Count > 0 && ((Span)this.currentSpanStack.Peek()).StopEOL)
							{
								this.currentSpanStack.Pop();
							}
							if (this.currentSpanStack.Count == 0)
							{
								this.currentSpanStack = null;
							}
						}
						this.currentLine = (LineSegment)document.LineSegmentCollection[i];
						if (this.currentLine.Length == -1)
						{
							return;
						}
						ArrayList words = this.ParseLine(document);
						if (this.currentLine.Words != null)
						{
							this.currentLine.Words.Clear();
						}
						this.currentLine.Words = words;
						this.currentLine.HighlightSpanStack = ((this.currentSpanStack == null || this.currentSpanStack.Count == 0) ? null : this.currentSpanStack);
					}
					document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
					document.CommitUpdate();
				}
			}
			finally
			{
				this.currentLine = null;
			}
		}
		private bool MarkTokensInLine(IDocument document, int lineNumber, ref bool spanChanged)
		{
			bool result = false;
			LineSegment lineSegment = (lineNumber > 0) ? document.GetLineSegment(lineNumber - 1) : null;
			this.currentSpanStack = ((lineSegment != null && lineSegment.HighlightSpanStack != null) ? ((Stack)lineSegment.HighlightSpanStack.Clone()) : null);
			if (this.currentSpanStack != null)
			{
				while (this.currentSpanStack.Count > 0 && ((Span)this.currentSpanStack.Peek()).StopEOL)
				{
					this.currentSpanStack.Pop();
				}
				if (this.currentSpanStack.Count == 0)
				{
					this.currentSpanStack = null;
				}
			}
			this.currentLine = (LineSegment)document.LineSegmentCollection[lineNumber];
			if (this.currentLine.Length == -1)
			{
				return false;
			}
			ArrayList words = this.ParseLine(document);
			if (this.currentSpanStack != null && this.currentSpanStack.Count == 0)
			{
				this.currentSpanStack = null;
			}
			if (this.currentLine.HighlightSpanStack != this.currentSpanStack)
			{
				if (this.currentLine.HighlightSpanStack == null)
				{
					result = false;
					IEnumerator enumerator = this.currentSpanStack.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							Span span = (Span)enumerator.Current;
							if (!span.StopEOL)
							{
								spanChanged = true;
								result = true;
								break;
							}
						}
						goto IL_25A;
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
				if (this.currentSpanStack == null)
				{
					result = false;
					IEnumerator enumerator2 = this.currentLine.HighlightSpanStack.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							Span span2 = (Span)enumerator2.Current;
							if (!span2.StopEOL)
							{
								spanChanged = true;
								result = true;
								break;
							}
						}
						goto IL_25A;
					}
					finally
					{
						IDisposable disposable2 = enumerator2 as IDisposable;
						if (disposable2 != null)
						{
							disposable2.Dispose();
						}
					}
				}
				IEnumerator enumerator3 = this.currentSpanStack.GetEnumerator();
				IEnumerator enumerator4 = this.currentLine.HighlightSpanStack.GetEnumerator();
				bool flag = false;
				while (!flag)
				{
					bool flag2 = false;
					while (enumerator3.MoveNext())
					{
						if (!((Span)enumerator3.Current).StopEOL)
						{
							flag2 = true;
							break;
						}
					}
					bool flag3 = false;
					while (enumerator4.MoveNext())
					{
						if (!((Span)enumerator4.Current).StopEOL)
						{
							flag3 = true;
							break;
						}
					}
					if (flag2 || flag3)
					{
						if (flag2 && flag3)
						{
							if (enumerator3.Current != enumerator4.Current)
							{
								flag = true;
								result = true;
								spanChanged = true;
							}
						}
						else
						{
							spanChanged = true;
							flag = true;
							result = true;
						}
					}
					else
					{
						flag = true;
						result = false;
					}
				}
			}
			else
			{
				result = false;
			}
			IL_25A:
			if (this.currentLine.Words != null)
			{
				this.currentLine.Words.Clear();
			}
			this.currentLine.Words = words;
			this.currentLine.HighlightSpanStack = ((this.currentSpanStack != null && this.currentSpanStack.Count > 0) ? this.currentSpanStack : null);
			return result;
		}
		public void MarkTokens(IDocument document, ArrayList inputLines)
		{
			try
			{
				if (this.Rules.Count != 0)
				{
					Hashtable hashtable = new Hashtable();
					bool flag = false;
					foreach (LineSegment lineSegment in inputLines)
					{
						if (hashtable[lineSegment] == null)
						{
							int num = document.GetLineNumberForOffset(lineSegment.Offset);
							bool flag2 = true;
							if (num != -1)
							{
								while (flag2 && num < document.TotalNumberOfLines && num < document.LineSegmentCollection.Count)
								{
									flag2 = this.MarkTokensInLine(document, num, ref flag);
									hashtable[this.currentLine] = string.Empty;
									num++;
								}
							}
						}
					}
					if (flag)
					{
						document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
					}
					else
					{
						foreach (LineSegment lineSegment2 in inputLines)
						{
							document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, document.GetLineNumberForOffset(lineSegment2.Offset)));
						}
					}
					document.CommitUpdate();
				}
			}
			finally
			{
				this.currentLine = null;
			}
		}
		private void UpdateSpanStateVariables()
		{
			this.inSpan = (this.currentSpanStack != null && this.currentSpanStack.Count > 0);
			this.activeSpan = (this.inSpan ? ((Span)this.currentSpanStack.Peek()) : null);
			this.activeRuleSet = this.GetRuleSet(this.activeSpan);
		}
		private ArrayList ParseLine(IDocument document)
		{
			ArrayList arrayList = new ArrayList();
			HighlightColor highlightColor = null;
			this.currentOffset = 0;
			this.currentLength = 0;
			this.UpdateSpanStateVariables();
			int i = 0;
			while (i < this.currentLine.Length)
			{
				char charAt = document.GetCharAt(this.currentLine.Offset + i);
				char c = charAt;
				switch (c)
				{
				case '\t':
					this.PushCurWord(document, ref highlightColor, arrayList);
					if (this.activeSpan != null && this.activeSpan.Color.HasBackground)
					{
						arrayList.Add(new TextWord.TabTextWord(this.activeSpan.Color));
					}
					else
					{
						arrayList.Add(TextWord.Tab);
					}
					this.currentOffset++;
					break;
				case '\n':
				case '\r':
					this.PushCurWord(document, ref highlightColor, arrayList);
					this.currentOffset++;
					break;
				case '\v':
				case '\f':
					goto IL_1AA;
				default:
					if (c != ' ')
					{
						if (c != '\\')
						{
							goto IL_1AA;
						}
						if ((this.activeRuleSet != null && this.activeRuleSet.NoEscapeSequences) || (this.activeSpan != null && this.activeSpan.NoEscapeSequences))
						{
							goto IL_1AA;
						}
						this.currentLength++;
						if (i + 1 < this.currentLine.Length)
						{
							this.currentLength++;
						}
						this.PushCurWord(document, ref highlightColor, arrayList);
						i++;
					}
					else
					{
						this.PushCurWord(document, ref highlightColor, arrayList);
						if (this.activeSpan != null && this.activeSpan.Color.HasBackground)
						{
							arrayList.Add(new TextWord.SpaceTextWord(this.activeSpan.Color));
						}
						else
						{
							arrayList.Add(TextWord.Space);
						}
						this.currentOffset++;
					}
					break;
				}
				IL_83E:
				i++;
				continue;
				IL_1AA:
				if (!this.inSpan && (char.IsDigit(charAt) || (charAt == '.' && i + 1 < this.currentLine.Length && char.IsDigit(document.GetCharAt(this.currentLine.Offset + i + 1)))) && this.currentLength == 0)
				{
					bool flag = false;
					bool flag2 = false;
					if (charAt == '0' && i + 1 < this.currentLine.Length && char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1)) == 'X')
					{
						this.currentLength++;
						i++;
						this.currentLength++;
						flag = true;
						while (i + 1 < this.currentLine.Length)
						{
							if ("0123456789ABCDEF".IndexOf(char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1))) == -1)
							{
								break;
							}
							i++;
							this.currentLength++;
						}
					}
					else
					{
						this.currentLength++;
						while (i + 1 < this.currentLine.Length && char.IsDigit(document.GetCharAt(this.currentLine.Offset + i + 1)))
						{
							i++;
							this.currentLength++;
						}
					}
					if (!flag && i + 1 < this.currentLine.Length && document.GetCharAt(this.currentLine.Offset + i + 1) == '.')
					{
						flag2 = true;
						i++;
						this.currentLength++;
						while (i + 1 < this.currentLine.Length && char.IsDigit(document.GetCharAt(this.currentLine.Offset + i + 1)))
						{
							i++;
							this.currentLength++;
						}
					}
					if (i + 1 < this.currentLine.Length && char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1)) == 'E')
					{
						flag2 = true;
						i++;
						this.currentLength++;
						if (i + 1 < this.currentLine.Length && (document.GetCharAt(this.currentLine.Offset + i + 1) == '+' || document.GetCharAt(this.currentLine.Offset + i + 1) == '-'))
						{
							i++;
							this.currentLength++;
						}
						while (i + 1 < this.currentLine.Length && char.IsDigit(document.GetCharAt(this.currentLine.Offset + i + 1)))
						{
							i++;
							this.currentLength++;
						}
					}
					if (i + 1 < this.currentLine.Length)
					{
						char c2 = char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1));
						if (c2 == 'F' || c2 == 'M' || c2 == 'D')
						{
							flag2 = true;
							i++;
							this.currentLength++;
						}
					}
					if (!flag2)
					{
						bool flag3 = false;
						if (i + 1 < this.currentLine.Length && char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1)) == 'U')
						{
							i++;
							this.currentLength++;
							flag3 = true;
						}
						if (i + 1 < this.currentLine.Length && char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1)) == 'L')
						{
							i++;
							this.currentLength++;
							if (!flag3 && i + 1 < this.currentLine.Length && char.ToUpper(document.GetCharAt(this.currentLine.Offset + i + 1)) == 'U')
							{
								i++;
								this.currentLength++;
							}
						}
					}
					arrayList.Add(new TextWord(document, this.currentLine, this.currentOffset, this.currentLength, this.DigitColor, false));
					this.currentOffset += this.currentLength;
					this.currentLength = 0;
					goto IL_83E;
				}
				if (this.inSpan && this.activeSpan.End != null && !this.activeSpan.End.Equals("") && this.currentLine.MatchExpr(this.activeSpan.End, i, document))
				{
					this.PushCurWord(document, ref highlightColor, arrayList);
					string regString = this.currentLine.GetRegString(this.activeSpan.End, i, document);
					this.currentLength += regString.Length;
					arrayList.Add(new TextWord(document, this.currentLine, this.currentOffset, this.currentLength, this.activeSpan.EndColor, false));
					this.currentOffset += this.currentLength;
					this.currentLength = 0;
					i += regString.Length - 1;
					this.currentSpanStack.Pop();
					this.UpdateSpanStateVariables();
					goto IL_83E;
				}
				if (this.activeRuleSet != null)
				{
					foreach (Span span in this.activeRuleSet.Spans)
					{
						if (this.currentLine.MatchExpr(span.Begin, i, document))
						{
							this.PushCurWord(document, ref highlightColor, arrayList);
							string regString2 = this.currentLine.GetRegString(span.Begin, i, document);
							this.currentLength += regString2.Length;
							arrayList.Add(new TextWord(document, this.currentLine, this.currentOffset, this.currentLength, span.BeginColor, false));
							this.currentOffset += this.currentLength;
							this.currentLength = 0;
							i += regString2.Length - 1;
							if (this.currentSpanStack == null)
							{
								this.currentSpanStack = new Stack();
							}
							this.currentSpanStack.Push(span);
							this.UpdateSpanStateVariables();
							goto IL_83E;
						}
					}
				}
				if (this.activeRuleSet != null && charAt < 'Ā' && this.activeRuleSet.Delimiters[(int)charAt])
				{
					this.PushCurWord(document, ref highlightColor, arrayList);
					if (this.currentOffset + this.currentLength + 1 < this.currentLine.Length)
					{
						this.currentLength++;
						this.PushCurWord(document, ref highlightColor, arrayList);
						goto IL_83E;
					}
				}
				this.currentLength++;
				goto IL_83E;
			}
			this.PushCurWord(document, ref highlightColor, arrayList);
			return arrayList;
		}
		private void PushCurWord(IDocument document, ref HighlightColor markNext, ArrayList words)
		{
			if (this.currentLength > 0)
			{
				if (words.Count > 0 && this.activeRuleSet != null)
				{
					int i = words.Count - 1;
					while (i >= 0)
					{
						if (!((TextWord)words[i]).IsWhiteSpace)
						{
							TextWord textWord = (TextWord)words[i];
							if (!textWord.HasDefaultColor)
							{
								break;
							}
							PrevMarker prevMarker = (PrevMarker)this.activeRuleSet.PrevMarkers[document, this.currentLine, this.currentOffset, this.currentLength];
							if (prevMarker != null)
							{
								textWord.SyntaxColor = prevMarker.Color;
								break;
							}
							break;
						}
						else
						{
							i--;
						}
					}
				}
				if (this.inSpan)
				{
					bool hasDefaultColor = true;
					HighlightColor highlightColor;
					if (this.activeSpan.Rule == null)
					{
						highlightColor = this.activeSpan.Color;
					}
					else
					{
						highlightColor = this.GetColor(this.activeRuleSet, document, this.currentLine, this.currentOffset, this.currentLength);
						hasDefaultColor = false;
					}
					if (highlightColor == null)
					{
						highlightColor = this.activeSpan.Color;
						if (highlightColor.Color == Color.Transparent)
						{
							highlightColor = this.GetColorFor("Default");
						}
						hasDefaultColor = true;
					}
					words.Add(new TextWord(document, this.currentLine, this.currentOffset, this.currentLength, (markNext != null) ? markNext : highlightColor, hasDefaultColor));
				}
				else
				{
					HighlightColor highlightColor2 = (markNext != null) ? markNext : this.GetColor(this.activeRuleSet, document, this.currentLine, this.currentOffset, this.currentLength);
					if (highlightColor2 == null)
					{
						words.Add(new TextWord(document, this.currentLine, this.currentOffset, this.currentLength, this.GetColorFor("Default"), true));
					}
					else
					{
						words.Add(new TextWord(document, this.currentLine, this.currentOffset, this.currentLength, highlightColor2, false));
					}
				}
				if (this.activeRuleSet != null)
				{
					NextMarker nextMarker = (NextMarker)this.activeRuleSet.NextMarkers[document, this.currentLine, this.currentOffset, this.currentLength];
					if (nextMarker != null)
					{
						if (nextMarker.MarkMarker && words.Count > 0)
						{
							TextWord textWord2 = (TextWord)words[words.Count - 1];
							textWord2.SyntaxColor = nextMarker.Color;
						}
						markNext = nextMarker.Color;
					}
					else
					{
						markNext = null;
					}
				}
				this.currentOffset += this.currentLength;
				this.currentLength = 0;
			}
		}
	}
}
