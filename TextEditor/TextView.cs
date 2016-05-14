using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class TextView : AbstractMargin
	{
		private int fontHeight;
		private Hashtable charWitdh = new Hashtable();
		private StringFormat measureStringFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
		private Highlight highlight;
		private int physicalColumn;
		public Highlight Highlight
		{
			get
			{
				return this.highlight;
			}
			set
			{
				this.highlight = value;
			}
		}
		public override Cursor Cursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}
		public int FirstPhysicalLine
		{
			get
			{
				return this.textArea.VirtualTop.Y / this.fontHeight;
			}
		}
		public int LineHeightRemainder
		{
			get
			{
				return this.textArea.VirtualTop.Y % this.fontHeight;
			}
		}
		public int FirstVisibleLine
		{
			get
			{
				return this.textArea.Document.GetFirstLogicalLine(this.textArea.VirtualTop.Y / this.fontHeight);
			}
			set
			{
				if (this.FirstVisibleLine != value)
				{
					this.textArea.VirtualTop = new Point(this.textArea.VirtualTop.X, this.textArea.Document.GetVisibleLine(value) * this.fontHeight);
				}
			}
		}
		public int VisibleLineDrawingRemainder
		{
			get
			{
				return this.textArea.VirtualTop.Y % this.fontHeight;
			}
		}
		public int FontHeight
		{
			get
			{
				return this.fontHeight;
			}
		}
		public int VisibleLineCount
		{
			get
			{
				return 1 + base.DrawingPosition.Height / this.fontHeight;
			}
		}
		public int VisibleColumnCount
		{
			get
			{
				return (int)((float)base.DrawingPosition.Width / this.GetWidth(' ')) - 1;
			}
		}
		public void Dispose()
		{
			this.measureStringFormat.Dispose();
		}
		public TextView(TextArea textArea) : base(textArea)
		{
			this.measureStringFormat.LineAlignment = StringAlignment.Near;
			this.measureStringFormat.FormatFlags = (StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
			this.OptionsChanged();
		}
		private static int GetFontHeight(Font font)
		{
			int height = font.Height;
			if (height >= 16)
			{
				return height;
			}
			return height + 1;
		}
		public void OptionsChanged()
		{
			this.fontHeight = TextView.GetFontHeight(base.TextEditorProperties.Font);
			if (this.charWitdh != null)
			{
				this.charWitdh.Clear();
			}
		}
		public override void Paint(Graphics g, Rectangle rect)
		{
			if (rect.Width <= 0 || rect.Height <= 0)
			{
				return;
			}
			if (this.fontHeight != TextView.GetFontHeight(base.TextEditorProperties.Font))
			{
				this.OptionsChanged();
				base.TextArea.Refresh();
				return;
			}
			int num = (int)((float)this.textArea.VirtualTop.X * this.GetWidth(g, ' '));
			if (num > 0)
			{
				g.SetClip(base.DrawingPosition);
			}
			for (int i = 0; i < (base.DrawingPosition.Height + this.VisibleLineDrawingRemainder) / this.fontHeight + 1; i++)
			{
				Rectangle rectangle = new Rectangle(base.DrawingPosition.X - num, base.DrawingPosition.Top + i * this.fontHeight - this.VisibleLineDrawingRemainder, base.DrawingPosition.Width + num, this.fontHeight);
				if (rect.IntersectsWith(rectangle))
				{
					this.textArea.Document.GetVisibleLine(this.FirstVisibleLine);
					int firstLogicalLine = this.textArea.Document.GetFirstLogicalLine(this.textArea.Document.GetVisibleLine(this.FirstVisibleLine) + i);
					this.PaintDocumentLine(g, firstLogicalLine, rectangle);
				}
			}
			if (num > 0)
			{
				g.ResetClip();
			}
		}
		private void PaintDocumentLine(Graphics g, int lineNumber, Rectangle lineRectangle)
		{
			Brush bgColorBrush = this.GetBgColorBrush(lineNumber);
			Brush brush = this.textArea.Enabled ? bgColorBrush : SystemBrushes.InactiveBorder;
			if (lineNumber >= this.textArea.Document.TotalNumberOfLines)
			{
				g.FillRectangle(brush, lineRectangle);
				if (base.TextEditorProperties.ShowInvalidLines)
				{
					this.DrawInvalidLineMarker(g, (float)lineRectangle.Left, (float)lineRectangle.Top);
				}
				if (base.TextEditorProperties.ShowVerticalRuler)
				{
					this.DrawVerticalRuler(g, lineRectangle);
				}
				return;
			}
			float num = (float)lineRectangle.X;
			int num2 = 0;
			this.physicalColumn = 0;
			if (base.TextEditorProperties.EnableFolding)
			{
				while (true)
				{
					ArrayList foldedFoldingsWithStartAfterColumn = this.textArea.Document.FoldingManager.GetFoldedFoldingsWithStartAfterColumn(lineNumber, num2 - 1);
					if (foldedFoldingsWithStartAfterColumn == null || foldedFoldingsWithStartAfterColumn.Count <= 0)
					{
						break;
					}
					FoldMarker foldMarker = (FoldMarker)foldedFoldingsWithStartAfterColumn[0];
					foreach (FoldMarker foldMarker2 in foldedFoldingsWithStartAfterColumn)
					{
						if (foldMarker2.StartColumn < foldMarker.StartColumn)
						{
							foldMarker = foldMarker2;
						}
					}
					foldedFoldingsWithStartAfterColumn.Clear();
					num = this.PaintLinePart(g, lineNumber, num2, foldMarker.StartColumn, lineRectangle, num);
					num2 = foldMarker.EndColumn;
					lineNumber = foldMarker.EndLine;
					ColumnRange selectionAtLine = this.textArea.SelectionManager.GetSelectionAtLine(lineNumber);
					bool drawSelected = ColumnRange.WholeColumn.Equals(selectionAtLine) || (foldMarker.StartColumn >= selectionAtLine.StartColumn && foldMarker.EndColumn <= selectionAtLine.EndColumn);
					num = this.PaintFoldingText(g, lineNumber, num, lineRectangle, foldMarker.FoldText, drawSelected);
				}
				if (lineNumber < this.textArea.Document.TotalNumberOfLines)
				{
					num = this.PaintLinePart(g, lineNumber, num2, this.textArea.Document.GetLineSegment(lineNumber).Length, lineRectangle, num);
				}
			}
			else
			{
				num = this.PaintLinePart(g, lineNumber, 0, this.textArea.Document.GetLineSegment(lineNumber).Length, lineRectangle, num);
			}
			if (lineNumber < this.textArea.Document.TotalNumberOfLines)
			{
				ColumnRange selectionAtLine2 = this.textArea.SelectionManager.GetSelectionAtLine(lineNumber);
				LineSegment lineSegment = this.textArea.Document.GetLineSegment(lineNumber);
				HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("Selection");
				float width = this.GetWidth(g, ' ');
				bool flag = selectionAtLine2.EndColumn > lineSegment.Length || ColumnRange.WholeColumn.Equals(selectionAtLine2);
				if (base.TextEditorProperties.ShowEOLMarker)
				{
					HighlightColor colorFor2 = this.textArea.Document.HighlightingStrategy.GetColorFor("EOLMarkers");
					num += this.DrawEOLMarker(g, colorFor2.Color, flag ? bgColorBrush : brush, num, (float)lineRectangle.Y);
				}
				else
				{
					if (flag)
					{
						g.FillRectangle(BrushRegistry.GetBrush(colorFor.BackgroundColor), new RectangleF(num, (float)lineRectangle.Y, width, (float)lineRectangle.Height));
						num += width;
					}
				}
				Brush brush2 = (flag && base.TextEditorProperties.AllowCaretBeyondEOL) ? bgColorBrush : brush;
				g.FillRectangle(brush2, new RectangleF(num, (float)lineRectangle.Y, (float)lineRectangle.Width - num + (float)lineRectangle.X, (float)lineRectangle.Height));
			}
			if (base.TextEditorProperties.ShowVerticalRuler)
			{
				this.DrawVerticalRuler(g, lineRectangle);
			}
		}
		private bool DrawLineMarkerAtLine(int lineNumber)
		{
			return lineNumber == this.textArea.Caret.Line && this.textArea.MotherTextAreaControl.TextEditorProperties.LineViewerStyle == LineViewerStyle.FullRow;
		}
		private Brush GetBgColorBrush(int lineNumber)
		{
			if (this.DrawLineMarkerAtLine(lineNumber))
			{
				HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("CaretMarker");
				return BrushRegistry.GetBrush(colorFor.Color);
			}
			HighlightBackground highlightBackground = (HighlightBackground)this.textArea.Document.HighlightingStrategy.GetColorFor("DefaultBackground");
			Color color = highlightBackground.BackgroundColor;
			if (this.textArea.MotherTextAreaControl.TextEditorProperties.UseCustomLine)
			{
				color = this.textArea.Document.CustomLineManager.GetCustomColor(lineNumber, color);
			}
			return BrushRegistry.GetBrush(color);
		}
		private float PaintFoldingText(Graphics g, int lineNumber, float physicalXPos, Rectangle lineRectangle, string text, bool drawSelected)
		{
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("Selection");
			Brush brush = drawSelected ? BrushRegistry.GetBrush(colorFor.BackgroundColor) : this.GetBgColorBrush(lineNumber);
			Brush brush2 = this.textArea.Enabled ? brush : SystemBrushes.InactiveBorder;
			float width = g.MeasureString(text, this.textArea.Font, 2147483647, this.measureStringFormat).Width;
			RectangleF rectangleF = new RectangleF(physicalXPos, (float)lineRectangle.Y, width, (float)(lineRectangle.Height - 1));
			g.FillRectangle(brush2, rectangleF);
			this.physicalColumn += text.Length;
			g.DrawString(text, this.textArea.Font, BrushRegistry.GetBrush(drawSelected ? colorFor.Color : Color.Gray), rectangleF, this.measureStringFormat);
			g.DrawRectangle(BrushRegistry.GetPen(drawSelected ? Color.DarkGray : Color.Gray), rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
			float num = (float)Math.Ceiling((double)(physicalXPos + width));
			if ((double)(num - (physicalXPos + width)) < 0.5)
			{
				num += 1f;
			}
			return num;
		}
		private void DrawMarker(Graphics g, TextMarker marker, RectangleF drawingRect)
		{
			float num = drawingRect.Bottom - 1f;
			switch (marker.TextMarkerType)
			{
			case TextMarkerType.SolidBlock:
				g.FillRectangle(BrushRegistry.GetBrush(marker.Color), drawingRect);
				return;
			case TextMarkerType.Underlined:
				g.DrawLine(BrushRegistry.GetPen(marker.Color), drawingRect.X, num, drawingRect.Right, num);
				return;
			case TextMarkerType.WaveLine:
			{
				int num2 = (int)drawingRect.X % 6;
				for (float num3 = drawingRect.X - (float)num2; num3 < drawingRect.Right + (float)num2; num3 += 6f)
				{
					g.DrawLine(BrushRegistry.GetPen(marker.Color), num3, num + 3f - 4f, num3 + 3f, num + 1f - 4f);
					g.DrawLine(BrushRegistry.GetPen(marker.Color), num3 + 3f, num + 1f - 4f, num3 + 6f, num + 3f - 4f);
				}
				return;
			}
			default:
				return;
			}
		}
		private Brush GetMarkerBrushAt(int offset, int length, out ArrayList markers)
		{
			markers = base.Document.MarkerStrategy.GetMarkers(offset, length);
			foreach (TextMarker textMarker in markers)
			{
				if (textMarker.TextMarkerType == TextMarkerType.SolidBlock)
				{
					return BrushRegistry.GetBrush(textMarker.Color);
				}
			}
			return null;
		}
		private float PaintLinePart(Graphics g, int lineNumber, int startColumn, int endColumn, Rectangle lineRectangle, float physicalXPos)
		{
			bool flag = this.DrawLineMarkerAtLine(lineNumber);
			Brush bgColorBrush = this.GetBgColorBrush(lineNumber);
			Brush brush = this.textArea.Enabled ? bgColorBrush : SystemBrushes.InactiveBorder;
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("Selection");
			ColumnRange selectionAtLine = this.textArea.SelectionManager.GetSelectionAtLine(lineNumber);
			HighlightColor colorFor2 = this.textArea.Document.HighlightingStrategy.GetColorFor("TabMarkers");
			HighlightColor colorFor3 = this.textArea.Document.HighlightingStrategy.GetColorFor("SpaceMarkers");
			float width = this.GetWidth(g, ' ');
			LineSegment lineSegment = this.textArea.Document.GetLineSegment(lineNumber);
			int num = startColumn;
			Brush brush2 = BrushRegistry.GetBrush(colorFor.BackgroundColor);
			Brush brush3 = brush;
			if (lineSegment.Words != null)
			{
				int num2 = 0;
				int num3 = 0;
				while (num2 < lineSegment.Words.Count && num3 < startColumn)
				{
					TextWord textWord = (TextWord)lineSegment.Words[num2];
					if (textWord.Type == TextWordType.Tab)
					{
						num3++;
					}
					else
					{
						if (textWord.Type == TextWordType.Space)
						{
							num3++;
						}
						else
						{
							num3 += textWord.Length;
						}
					}
					num2++;
				}
				int num4 = num2;
				while (num4 < lineSegment.Words.Count && num < endColumn)
				{
					ArrayList markers = base.Document.MarkerStrategy.GetMarkers(lineSegment.Offset + num3);
					foreach (TextMarker textMarker in markers)
					{
						if (textMarker.TextMarkerType == TextMarkerType.SolidBlock)
						{
							brush3 = BrushRegistry.GetBrush(textMarker.Color);
							break;
						}
					}
					TextWord textWord2 = (TextWord)lineSegment.Words[num4];
					switch (textWord2.Type)
					{
					case TextWordType.Word:
					{
						string word = textWord2.Word;
						float num5 = physicalXPos;
						Brush markerBrushAt = this.GetMarkerBrushAt(lineSegment.Offset + num, word.Length, out markers);
						Brush backBrush;
						if (!flag && markerBrushAt != null)
						{
							backBrush = markerBrushAt;
						}
						else
						{
							if (!flag && textWord2.SyntaxColor.HasBackground)
							{
								backBrush = BrushRegistry.GetBrush(textWord2.SyntaxColor.BackgroundColor);
							}
							else
							{
								backBrush = brush3;
							}
						}
						if (ColumnRange.WholeColumn.Equals(selectionAtLine) || (selectionAtLine.EndColumn - 1 >= word.Length + num && selectionAtLine.StartColumn <= num))
						{
							physicalXPos += this.DrawDocumentWord(g, word, new PointF(physicalXPos, (float)lineRectangle.Y), textWord2.Font, colorFor.HasForgeground ? colorFor.Color : textWord2.Color, brush2);
						}
						else
						{
							if (ColumnRange.NoColumn.Equals(selectionAtLine))
							{
								physicalXPos += this.DrawDocumentWord(g, word, new PointF(physicalXPos, (float)lineRectangle.Y), textWord2.Font, textWord2.Color, backBrush);
							}
							else
							{
								int num6 = Math.Min(word.Length, Math.Max(0, selectionAtLine.StartColumn - num));
								int num7 = Math.Max(num6, Math.Min(word.Length, selectionAtLine.EndColumn - num));
								physicalXPos += this.DrawDocumentWord(g, word.Substring(0, num6), new PointF(physicalXPos, (float)lineRectangle.Y), textWord2.Font, textWord2.Color, backBrush);
								physicalXPos += this.DrawDocumentWord(g, word.Substring(num6, num7 - num6), new PointF(physicalXPos, (float)lineRectangle.Y), textWord2.Font, colorFor.HasForgeground ? colorFor.Color : textWord2.Color, brush2);
								physicalXPos += this.DrawDocumentWord(g, word.Substring(num7), new PointF(physicalXPos, (float)lineRectangle.Y), textWord2.Font, textWord2.Color, backBrush);
							}
						}
						foreach (TextMarker textMarker2 in markers)
						{
							if (textMarker2.TextMarkerType != TextMarkerType.SolidBlock)
							{
								this.DrawMarker(g, textMarker2, new RectangleF(num5, (float)lineRectangle.Y, physicalXPos - num5, (float)lineRectangle.Height));
							}
						}
						if (this.highlight != null && ((this.highlight.OpenBrace.Y == lineNumber && this.highlight.OpenBrace.X == num) || (this.highlight.CloseBrace.Y == lineNumber && this.highlight.CloseBrace.X == num)))
						{
							this.DrawBracketHighlight(g, new Rectangle((int)num5, lineRectangle.Y, (int)(physicalXPos - num5) - 1, lineRectangle.Height - 1));
						}
						this.physicalColumn += word.Length;
						num += word.Length;
						break;
					}
					case TextWordType.Space:
					{
						RectangleF rectangleF = new RectangleF(physicalXPos, (float)lineRectangle.Y, (float)Math.Ceiling((double)width), (float)lineRectangle.Height);
						Brush brush4;
						if (ColumnRange.WholeColumn.Equals(selectionAtLine) || (num >= selectionAtLine.StartColumn && num < selectionAtLine.EndColumn))
						{
							brush4 = brush2;
						}
						else
						{
							Brush markerBrushAt2 = this.GetMarkerBrushAt(lineSegment.Offset + num, 1, out markers);
							if (!flag && markerBrushAt2 != null)
							{
								brush4 = markerBrushAt2;
							}
							else
							{
								if (!flag && textWord2.SyntaxColor != null && textWord2.SyntaxColor.HasBackground)
								{
									brush4 = BrushRegistry.GetBrush(textWord2.SyntaxColor.BackgroundColor);
								}
								else
								{
									brush4 = brush3;
								}
							}
						}
						g.FillRectangle(brush4, rectangleF);
						if (base.TextEditorProperties.ShowSpaces)
						{
							this.DrawSpaceMarker(g, colorFor3.Color, physicalXPos, (float)lineRectangle.Y);
						}
						foreach (TextMarker marker in markers)
						{
							this.DrawMarker(g, marker, rectangleF);
						}
						physicalXPos += width;
						num++;
						this.physicalColumn++;
						break;
					}
					case TextWordType.Tab:
					{
						int num8 = this.physicalColumn;
						this.physicalColumn += base.TextEditorProperties.TabIndent;
						this.physicalColumn = this.physicalColumn / base.TextEditorProperties.TabIndent * base.TextEditorProperties.TabIndent;
						float num9 = (float)(this.physicalColumn - num8) * width;
						RectangleF rectangleF2 = new RectangleF(physicalXPos, (float)lineRectangle.Y, (float)Math.Ceiling((double)num9), (float)lineRectangle.Height);
						Brush brush4;
						if (ColumnRange.WholeColumn.Equals(selectionAtLine) || (num >= selectionAtLine.StartColumn && num <= selectionAtLine.EndColumn - 1))
						{
							brush4 = brush2;
						}
						else
						{
							Brush markerBrushAt3 = this.GetMarkerBrushAt(lineSegment.Offset + num, 1, out markers);
							if (!flag && markerBrushAt3 != null)
							{
								brush4 = markerBrushAt3;
							}
							else
							{
								if (!flag && textWord2.SyntaxColor != null && textWord2.SyntaxColor.HasBackground)
								{
									brush4 = BrushRegistry.GetBrush(textWord2.SyntaxColor.BackgroundColor);
								}
								else
								{
									brush4 = brush3;
								}
							}
						}
						g.FillRectangle(brush4, rectangleF2);
						if (base.TextEditorProperties.ShowTabs)
						{
							this.DrawTabMarker(g, colorFor2.Color, physicalXPos, (float)lineRectangle.Y);
						}
						foreach (TextMarker marker2 in markers)
						{
							this.DrawMarker(g, marker2, rectangleF2);
						}
						physicalXPos += num9;
						num++;
						break;
					}
					}
					num4++;
				}
			}
			return physicalXPos;
		}
		private float DrawDocumentWord(Graphics g, string word, PointF position, Font font, Color foreColor, Brush backBrush)
		{
			if (word == null || word.Length == 0)
			{
				return 0f;
			}
			float width = g.MeasureString(word, font, 32768, this.measureStringFormat).Width;
			g.FillRectangle(backBrush, new RectangleF(position.X, position.Y, (float)Math.Ceiling((double)width), (float)this.FontHeight));
			g.DrawString(word, font, BrushRegistry.GetBrush(foreColor), position.X, position.Y, this.measureStringFormat);
			return width;
		}
		public float GetWidth(char ch)
		{
			if (!this.charWitdh.ContainsKey(ch))
			{
				using (Graphics graphics = this.textArea.CreateGraphics())
				{
					return this.GetWidth(graphics, ch);
				}
			}
			return (float)this.charWitdh[ch];
		}
		public float GetWidth(string text)
		{
			float num = 0f;
			for (int i = 0; i < text.Length; i++)
			{
				num += this.GetWidth(text[i]);
			}
			return num;
		}
		public float GetWidth(Graphics g, char ch)
		{
			if (!this.charWitdh.ContainsKey(ch))
			{
				this.charWitdh.Add(ch, g.MeasureString(ch.ToString(), base.TextEditorProperties.Font, 2000, this.measureStringFormat).Width);
			}
			return (float)this.charWitdh[ch];
		}
		public int GetVisualColumn(int logicalLine, int logicalColumn)
		{
			return this.GetVisualColumn(base.Document.GetLineSegment(logicalLine), logicalColumn);
		}
		public int GetVisualColumn(LineSegment line, int logicalColumn)
		{
			int tabIndent = base.Document.TextEditorProperties.TabIndent;
			int num = 0;
			for (int i = 0; i < logicalColumn; i++)
			{
				char c;
				if (i >= line.Length)
				{
					c = ' ';
				}
				else
				{
					c = base.Document.GetCharAt(line.Offset + i);
				}
				char c2 = c;
				if (c2 == '\t')
				{
					num += tabIndent;
					num = num / tabIndent * tabIndent;
				}
				else
				{
					num++;
				}
			}
			return num;
		}
		public Point GetLogicalPosition(int xPos, int yPos)
		{
			xPos += (int)((float)this.textArea.VirtualTop.X * this.GetWidth(' '));
			int lineNumber = Math.Max(0, (yPos + this.textArea.VirtualTop.Y) / this.fontHeight);
			int firstLogicalLine = base.Document.GetFirstLogicalLine(lineNumber);
			return this.GetLogicalColumn(firstLogicalLine, xPos);
		}
		public int GetLogicalLine(Point mousepos)
		{
			int lineNumber = this.FirstPhysicalLine + mousepos.Y / this.FontHeight;
			return base.Document.GetFirstLogicalLine(lineNumber);
		}
		public Point GetLogicalColumn(int firstLogicalLine, int xPos)
		{
			float width = this.GetWidth(' ');
			LineSegment lineSegment = (firstLogicalLine < base.Document.TotalNumberOfLines) ? base.Document.GetLineSegment(firstLogicalLine) : null;
			if (lineSegment == null)
			{
				return new Point((int)((float)xPos / width), firstLogicalLine);
			}
			int num = firstLogicalLine;
			int tabIndent = base.Document.TextEditorProperties.TabIndent;
			int num2 = 0;
			int num3 = 0;
			float num4 = 0f;
			ArrayList foldedFoldingsWithStart = this.textArea.Document.FoldingManager.GetFoldedFoldingsWithStart(num);
			while (true)
			{
				float num5 = num4;
				if (foldedFoldingsWithStart.Count > 0)
				{
					foreach (FoldMarker foldMarker in foldedFoldingsWithStart)
					{
						if (foldMarker.IsFolded && num3 >= foldMarker.StartColumn && (num3 < foldMarker.EndColumn || num != foldMarker.EndLine))
						{
							num2 += foldMarker.FoldText.Length;
							num4 += (float)foldMarker.FoldText.Length * width;
							if ((float)xPos <= num4 - (num4 - num5) / 2f)
							{
								return new Point(num3, num);
							}
							num3 = foldMarker.EndColumn;
							if (num != foldMarker.EndLine)
							{
								num = foldMarker.EndLine;
								lineSegment = base.Document.GetLineSegment(num);
								foldedFoldingsWithStart = this.textArea.Document.FoldingManager.GetFoldedFoldingsWithStart(num);
								break;
							}
							break;
						}
					}
				}
				char c = (num3 >= lineSegment.Length) ? ' ' : base.Document.GetCharAt(lineSegment.Offset + num3);
				char c2 = c;
				if (c2 == '\t')
				{
					int num6 = num2;
					num2 += tabIndent;
					num2 = num2 / tabIndent * tabIndent;
					num4 += (float)(num2 - num6) * width;
				}
				else
				{
					num4 += this.GetWidth(c);
					num2++;
				}
				if ((float)xPos <= num4 - (num4 - num5) / 2f)
				{
					break;
				}
				num3++;
			}
			return new Point(num3, num);
		}
		public FoldMarker GetFoldMarkerFromPosition(int xPos, int yPos)
		{
			xPos += (int)((float)this.textArea.VirtualTop.X * this.GetWidth(' '));
			int lineNumber = (yPos + this.textArea.VirtualTop.Y) / this.fontHeight;
			int firstLogicalLine = base.Document.GetFirstLogicalLine(lineNumber);
			return this.GetFoldMarkerFromColumn(firstLogicalLine, xPos);
		}
		private FoldMarker GetFoldMarkerFromColumn(int firstLogicalLine, int xPos)
		{
			float width = this.GetWidth(' ');
			LineSegment lineSegment = (firstLogicalLine < base.Document.TotalNumberOfLines) ? base.Document.GetLineSegment(firstLogicalLine) : null;
			if (lineSegment == null)
			{
				return null;
			}
			int num = firstLogicalLine;
			int tabIndent = base.Document.TextEditorProperties.TabIndent;
			int num2 = 0;
			int num3 = 0;
			float num4 = 0f;
			ArrayList foldedFoldingsWithStart = this.textArea.Document.FoldingManager.GetFoldedFoldingsWithStart(num);
			while (true)
			{
				float num5 = num4;
				if (foldedFoldingsWithStart.Count > 0)
				{
					foreach (FoldMarker foldMarker in foldedFoldingsWithStart)
					{
						if (foldMarker.IsFolded && num3 >= foldMarker.StartColumn && (num3 < foldMarker.EndColumn || num != foldMarker.EndLine))
						{
							num2 += foldMarker.FoldText.Length;
							num4 += (float)foldMarker.FoldText.Length * width;
							if ((float)xPos <= num4)
							{
								return foldMarker;
							}
							num3 = foldMarker.EndColumn;
							if (num != foldMarker.EndLine)
							{
								num = foldMarker.EndLine;
								lineSegment = base.Document.GetLineSegment(num);
								foldedFoldingsWithStart = this.textArea.Document.FoldingManager.GetFoldedFoldingsWithStart(num);
								break;
							}
							break;
						}
					}
				}
				char c = (num3 >= lineSegment.Length) ? ' ' : base.Document.GetCharAt(lineSegment.Offset + num3);
				char c2 = c;
				if (c2 == '\t')
				{
					int num6 = num2;
					num2 += tabIndent;
					num2 = num2 / tabIndent * tabIndent;
					num4 += (float)(num2 - num6) * width;
				}
				else
				{
					num4 += this.GetWidth(c);
					num2++;
				}
				if ((float)xPos <= num4 - (num4 - num5) / 2f)
				{
					break;
				}
				num3++;
			}
			return null;
		}
		private float CountColumns(ref int column, int start, int end, int logicalLine)
		{
			float width = this.GetWidth(' ');
			float num = 0f;
			int tabIndent = base.Document.TextEditorProperties.TabIndent;
			for (int i = start; i < end; i++)
			{
				LineSegment lineSegment = base.Document.GetLineSegment(logicalLine);
				char c;
				if (i >= lineSegment.Length)
				{
					c = ' ';
				}
				else
				{
					c = base.Document.GetCharAt(lineSegment.Offset + i);
				}
				char c2 = c;
				if (c2 == '\t')
				{
					int num2 = column;
					column += tabIndent;
					column = column / tabIndent * tabIndent;
					num += (float)(column - num2) * width;
				}
				else
				{
					column++;
					num += this.GetWidth(c);
				}
			}
			return num;
		}
		public int GetDrawingXPos(int logicalLine, int logicalColumn)
		{
			float width = this.GetWidth(' ');
			ArrayList topLevelFoldedFoldings = base.Document.FoldingManager.GetTopLevelFoldedFoldings();
			FoldMarker foldMarker = null;
			int i;
			for (i = topLevelFoldedFoldings.Count - 1; i >= 0; i--)
			{
				foldMarker = (FoldMarker)topLevelFoldedFoldings[i];
				if (foldMarker.StartLine < logicalLine || (foldMarker.StartLine == logicalLine && foldMarker.StartColumn < logicalColumn))
				{
					break;
				}
			}
			int num = 0;
			int arg_70_0 = base.Document.TextEditorProperties.TabIndent;
			float num2;
			if (foldMarker == null || (foldMarker.StartLine >= logicalLine && (foldMarker.StartLine != logicalLine || foldMarker.StartColumn >= logicalColumn)))
			{
				num2 = this.CountColumns(ref num, 0, logicalColumn, logicalLine);
				return (int)(num2 - (float)this.textArea.VirtualTop.X * width);
			}
			if (foldMarker.EndLine > logicalLine || (foldMarker.EndLine == logicalLine && foldMarker.EndColumn > logicalColumn))
			{
				logicalColumn = foldMarker.StartColumn;
				logicalLine = foldMarker.StartLine;
				i--;
			}
			int num3 = i;
			while (i >= 0)
			{
				foldMarker = (FoldMarker)topLevelFoldedFoldings[i];
				if (foldMarker.EndLine < logicalLine)
				{
					break;
				}
				i--;
			}
			int num4 = i + 1;
			if (num3 < num4)
			{
				num2 = this.CountColumns(ref num, 0, logicalColumn, logicalLine);
				return (int)(num2 - (float)this.textArea.VirtualTop.X * width);
			}
			int start = 0;
			num2 = 0f;
			for (i = num4; i <= num3; i++)
			{
				foldMarker = (FoldMarker)topLevelFoldedFoldings[i];
				num2 += this.CountColumns(ref num, start, foldMarker.StartColumn, foldMarker.StartLine);
				start = foldMarker.EndColumn;
				num += foldMarker.FoldText.Length;
				num2 += this.GetWidth(foldMarker.FoldText);
			}
			num2 += this.CountColumns(ref num, start, logicalColumn, logicalLine);
			return (int)(num2 - (float)this.textArea.VirtualTop.X * width);
		}
		private void DrawBracketHighlight(Graphics g, Rectangle rect)
		{
			g.FillRectangle(BrushRegistry.GetBrush(Color.FromArgb(50, 0, 0, 255)), rect);
			g.DrawRectangle(Pens.Blue, rect);
		}
		private void DrawInvalidLineMarker(Graphics g, float x, float y)
		{
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("InvalidLines");
			g.DrawString("~", colorFor.Font, BrushRegistry.GetBrush(colorFor.Color), x, y, this.measureStringFormat);
		}
		private void DrawSpaceMarker(Graphics g, Color color, float x, float y)
		{
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("SpaceMarkers");
			g.DrawString("·", colorFor.Font, BrushRegistry.GetBrush(color), x, y, this.measureStringFormat);
		}
		private void DrawTabMarker(Graphics g, Color color, float x, float y)
		{
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("TabMarkers");
			g.DrawString("»", colorFor.Font, BrushRegistry.GetBrush(color), x, y, this.measureStringFormat);
		}
		private float DrawEOLMarker(Graphics g, Color color, Brush backBrush, float x, float y)
		{
			float width = this.GetWidth(g, '¶');
			g.FillRectangle(backBrush, new RectangleF(x, y, width, (float)this.fontHeight));
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("EOLMarkers");
			g.DrawString("¶", colorFor.Font, BrushRegistry.GetBrush(color), x, y, this.measureStringFormat);
			return width;
		}
		private void DrawVerticalRuler(Graphics g, Rectangle lineRectangle)
		{
			if (base.TextEditorProperties.VerticalRulerRow < this.textArea.VirtualTop.X)
			{
				return;
			}
			HighlightColor colorFor = this.textArea.Document.HighlightingStrategy.GetColorFor("VRuler");
			int num = (int)((float)this.drawingPosition.Left + this.GetWidth(g, ' ') * (float)(base.TextEditorProperties.VerticalRulerRow - this.textArea.VirtualTop.X));
			g.DrawLine(BrushRegistry.GetPen(colorFor.Color), num, lineRectangle.Top, num, lineRectangle.Bottom);
		}
	}
}
