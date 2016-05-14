using System;
using System.Collections;
using System.Runtime.CompilerServices;
namespace LTP.TextEditor.Document
{
	internal class DefaultLineManager : ILineManager
	{
		public class DelimiterSegment : ISegment
		{
			private int offset;
			private int length;
			public int Offset
			{
				get
				{
					return this.offset;
				}
				set
				{
					this.offset = value;
				}
			}
			public int Length
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
		}
		private ArrayList lineCollection = new ArrayList();
		private IDocument document;
		private IHighlightingStrategy highlightingStrategy;
		private int textLength;
		private ArrayList markLines = new ArrayList();
		private DefaultLineManager.DelimiterSegment delimiterSegment = new DefaultLineManager.DelimiterSegment();
        public event LineLengthEventHandler LineLengthChanged;
        public event LineManagerEventHandler LineCountChanged;
		public ArrayList LineSegmentCollection
		{
			get
			{
				return this.lineCollection;
			}
		}
		public int TotalNumberOfLines
		{
			get
			{
				if (this.lineCollection.Count == 0)
				{
					return 1;
				}
				if (((LineSegment)this.lineCollection[this.lineCollection.Count - 1]).DelimiterLength <= 0)
				{
					return this.lineCollection.Count;
				}
				return this.lineCollection.Count + 1;
			}
		}
		public IHighlightingStrategy HighlightingStrategy
		{
			get
			{
				return this.highlightingStrategy;
			}
			set
			{
				if (this.highlightingStrategy != value)
				{
					this.highlightingStrategy = value;
					if (this.highlightingStrategy != null)
					{
						this.highlightingStrategy.MarkTokens(this.document);
					}
				}
			}
		}
		public DefaultLineManager(IDocument document, IHighlightingStrategy highlightingStrategy)
		{
			this.document = document;
			this.highlightingStrategy = highlightingStrategy;
		}
		public int GetLineNumberForOffset(int offset)
		{
			if (offset < 0 || offset > this.textLength)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "should be between 0 and " + this.textLength);
			}
			if (offset != this.textLength)
			{
				return this.FindLineNumber(offset);
			}
			if (this.lineCollection.Count == 0)
			{
				return 0;
			}
			LineSegment lineSegment = (LineSegment)this.lineCollection[this.lineCollection.Count - 1];
			if (lineSegment.DelimiterLength <= 0)
			{
				return this.lineCollection.Count - 1;
			}
			return this.lineCollection.Count;
		}
		public LineSegment GetLineSegmentForOffset(int offset)
		{
			if (offset < 0 || offset > this.textLength)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "should be between 0 and " + this.textLength);
			}
			if (offset != this.textLength)
			{
				return this.GetLineSegment(this.FindLineNumber(offset));
			}
			if (this.lineCollection.Count == 0)
			{
				return new LineSegment(0, 0);
			}
			LineSegment lineSegment = (LineSegment)this.lineCollection[this.lineCollection.Count - 1];
			if (lineSegment.DelimiterLength <= 0)
			{
				return lineSegment;
			}
			return new LineSegment(this.textLength, 0);
		}
		public LineSegment GetLineSegment(int lineNr)
		{
			if (lineNr < 0 || lineNr > this.lineCollection.Count)
			{
				throw new ArgumentOutOfRangeException("lineNr", lineNr, "should be between 0 and " + this.lineCollection.Count);
			}
			if (lineNr != this.lineCollection.Count)
			{
				return (LineSegment)this.lineCollection[lineNr];
			}
			if (this.lineCollection.Count == 0)
			{
				return new LineSegment(0, 0);
			}
			LineSegment lineSegment = (LineSegment)this.lineCollection[this.lineCollection.Count - 1];
			if (lineSegment.DelimiterLength <= 0)
			{
				return lineSegment;
			}
			return new LineSegment(lineSegment.Offset + lineSegment.TotalLength, 0);
		}
		private int Insert(int lineNumber, int offset, string text)
		{
			if (text == null || text.Length == 0)
			{
				return 0;
			}
			this.textLength += text.Length;
			if (this.lineCollection.Count == 0 || lineNumber >= this.lineCollection.Count)
			{
				return this.CreateLines(text, this.lineCollection.Count, offset);
			}
			LineSegment lineSegment = (LineSegment)this.lineCollection[lineNumber];
			ISegment segment = this.NextDelimiter(text, 0);
			if (segment == null || segment.Offset < 0)
			{
				lineSegment.TotalLength += text.Length;
				this.markLines.Add(lineSegment);
				this.OnLineLengthChanged(new LineLengthEventArgs(this.document, lineNumber, offset - lineSegment.Offset, text.Length));
				return 0;
			}
			int num = lineSegment.Offset + lineSegment.TotalLength - offset;
			if (num > 0)
			{
				LineSegment lineSegment2 = new LineSegment(offset, num);
				lineSegment2.DelimiterLength = lineSegment.DelimiterLength;
				lineSegment2.Offset += text.Length;
				this.markLines.Add(lineSegment2);
				if (num - lineSegment.DelimiterLength < 0)
				{
					throw new ApplicationException("tried to insert inside delimiter string " + lineSegment2.ToString() + "!!!");
				}
				this.lineCollection.Insert(lineNumber + 1, lineSegment2);
				this.OnLineCountChanged(new LineManagerEventArgs(this.document, lineNumber - 1, 1));
			}
			lineSegment.DelimiterLength = segment.Length;
			int num2 = offset + segment.Offset + segment.Length;
			lineSegment.TotalLength = num2 - lineSegment.Offset;
			this.markLines.Add(lineSegment);
			text = text.Substring(segment.Offset + segment.Length);
			return this.CreateLines(text, lineNumber + 1, num2) + 1;
		}
		private bool Remove(int lineNumber, int offset, int length)
		{
			if (length == 0)
			{
				return false;
			}
			int num = this.GetNumberOfLines(lineNumber, offset, length) - 1;
			LineSegment lineSegment = (LineSegment)this.lineCollection[lineNumber];
			if (lineNumber == this.lineCollection.Count - 1 && num > 0)
			{
				lineSegment.TotalLength -= length;
				lineSegment.DelimiterLength = 0;
			}
			else
			{
				lineNumber++;
				for (int i = 1; i <= num; i++)
				{
					if (lineNumber == this.lineCollection.Count)
					{
						lineSegment.DelimiterLength = 0;
						break;
					}
					LineSegment lineSegment2 = (LineSegment)this.lineCollection[lineNumber];
					lineSegment.TotalLength += lineSegment2.TotalLength;
					lineSegment.DelimiterLength = lineSegment2.DelimiterLength;
					this.lineCollection.RemoveAt(lineNumber);
				}
				lineSegment.TotalLength -= length;
				if (lineNumber < this.lineCollection.Count && num > 0)
				{
					this.markLines.Add(this.lineCollection[lineNumber]);
				}
			}
			this.textLength -= length;
			if (lineSegment.TotalLength == 0)
			{
				this.lineCollection.Remove(lineSegment);
				this.OnLineCountChanged(new LineManagerEventArgs(this.document, lineNumber, -num));
				return true;
			}
			this.markLines.Add(lineSegment);
			this.OnLineCountChanged(new LineManagerEventArgs(this.document, lineNumber, -num));
			return false;
		}
		public void Insert(int offset, string text)
		{
			this.Replace(offset, 0, text);
		}
		public void Remove(int offset, int length)
		{
			this.Replace(offset, length, string.Empty);
		}
		public void Replace(int offset, int length, string text)
		{
			int num = this.GetLineNumberForOffset(offset);
			int lineNumber = num;
			if (this.Remove(num, offset, length))
			{
				num--;
			}
			num += this.Insert(lineNumber, offset, text);
			int num2 = -length;
			if (text != null)
			{
				num2 = text.Length + num2;
			}
			if (num2 != 0)
			{
				this.AdaptLineOffsets(num, num2);
			}
			this.RunHighlighter();
		}
		private void RunHighlighter()
		{
			DateTime arg_05_0 = DateTime.Now;
			if (this.highlightingStrategy != null)
			{
				this.highlightingStrategy.MarkTokens(this.document, this.markLines);
			}
			this.markLines.Clear();
		}
		public void SetContent(string text)
		{
			this.lineCollection.Clear();
			if (text != null)
			{
				this.textLength = text.Length;
				this.CreateLines(text, 0, 0);
				this.RunHighlighter();
			}
		}
		private void AdaptLineOffsets(int lineNumber, int delta)
		{
			for (int i = lineNumber + 1; i < this.lineCollection.Count; i++)
			{
				((LineSegment)this.lineCollection[i]).Offset += delta;
			}
		}
		private int GetNumberOfLines(int startLine, int offset, int length)
		{
			if (length == 0)
			{
				return 1;
			}
			int num = offset + length;
			LineSegment lineSegment = (LineSegment)this.lineCollection[startLine];
			if (lineSegment.DelimiterLength == 0)
			{
				return 1;
			}
			if (lineSegment.Offset + lineSegment.TotalLength > num)
			{
				return 1;
			}
			if (lineSegment.Offset + lineSegment.TotalLength == num)
			{
				return 2;
			}
			return this.GetLineNumberForOffset(num) - startLine + 1;
		}
		private int FindLineNumber(int offset)
		{
			if (this.lineCollection.Count == 0)
			{
				return -1;
			}
			int i = 0;
			int num = this.lineCollection.Count - 1;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				LineSegment lineSegment = (LineSegment)this.lineCollection[num2];
				if (offset < lineSegment.Offset)
				{
					num = num2 - 1;
				}
				else
				{
					if (offset <= lineSegment.Offset)
					{
						i = num2;
						break;
					}
					i = num2 + 1;
				}
			}
			if (((LineSegment)this.lineCollection[i]).Offset <= offset)
			{
				return i;
			}
			return i - 1;
		}
		private int CreateLines(string text, int insertPosition, int offset)
		{
			int num = 0;
			int num2 = 0;
			ISegment segment = this.NextDelimiter(text, 0);
			while (segment != null && segment.Offset >= 0)
			{
				int num3 = segment.Offset + (segment.Length - 1);
				LineSegment value = new LineSegment(offset + num2, offset + num3, segment.Length);
				this.markLines.Add(value);
				if (insertPosition + num >= this.lineCollection.Count)
				{
					this.lineCollection.Add(value);
				}
				else
				{
					this.lineCollection.Insert(insertPosition + num, value);
				}
				num++;
				num2 = num3 + 1;
				segment = this.NextDelimiter(text, num2);
			}
			if (num2 < text.Length)
			{
				if (insertPosition + num < this.lineCollection.Count)
				{
					LineSegment lineSegment = (LineSegment)this.lineCollection[insertPosition + num];
					int num4 = text.Length - num2;
					lineSegment.Offset -= num4;
					lineSegment.TotalLength += num4;
				}
				else
				{
					LineSegment value2 = new LineSegment(offset + num2, text.Length - num2);
					this.markLines.Add(value2);
					this.lineCollection.Add(value2);
					num++;
				}
			}
			this.OnLineCountChanged(new LineManagerEventArgs(this.document, insertPosition - 1, num));
			return num;
		}
		public int GetVisibleLine(int logicalLineNumber)
		{
			if (!this.document.TextEditorProperties.EnableFolding)
			{
				return logicalLineNumber;
			}
			int num = 0;
			int num2 = 0;
			ArrayList topLevelFoldedFoldings = this.document.FoldingManager.GetTopLevelFoldedFoldings();
			foreach (FoldMarker foldMarker in topLevelFoldedFoldings)
			{
				if (foldMarker.StartLine >= logicalLineNumber)
				{
					break;
				}
				if (foldMarker.StartLine >= num2)
				{
					num += foldMarker.StartLine - num2;
					if (foldMarker.EndLine > logicalLineNumber)
					{
						return num;
					}
					num2 = foldMarker.EndLine;
				}
			}
			num += logicalLineNumber - num2;
			return num;
		}
		public int GetFirstLogicalLine(int visibleLineNumber)
		{
			if (!this.document.TextEditorProperties.EnableFolding)
			{
				return visibleLineNumber;
			}
			int num = 0;
			int num2 = 0;
			ArrayList arrayList = this.document.FoldingManager.GetTopLevelFoldedFoldings();
			foreach (FoldMarker foldMarker in arrayList)
			{
				if (foldMarker.StartLine >= num2)
				{
					if (num + foldMarker.StartLine - num2 >= visibleLineNumber)
					{
						break;
					}
					num += foldMarker.StartLine - num2;
					num2 = foldMarker.EndLine;
				}
			}
			arrayList.Clear();
			arrayList = null;
			return num2 + visibleLineNumber - num;
		}
		public int GetLastLogicalLine(int visibleLineNumber)
		{
			if (!this.document.TextEditorProperties.EnableFolding)
			{
				return visibleLineNumber;
			}
			return this.GetFirstLogicalLine(visibleLineNumber + 1) - 1;
		}
		public int GetNextVisibleLineAbove(int lineNumber, int lineCount)
		{
			int num = lineNumber;
			if (this.document.TextEditorProperties.EnableFolding)
			{
				for (int i = 0; i < lineCount; i++)
				{
					if (num >= this.TotalNumberOfLines)
					{
						break;
					}
					num++;
					while (num < this.TotalNumberOfLines && (num >= this.lineCollection.Count || !this.document.FoldingManager.IsLineVisible(num)))
					{
						num++;
					}
				}
			}
			else
			{
				num += lineCount;
			}
			return Math.Min(this.TotalNumberOfLines - 1, num);
		}
		public int GetNextVisibleLineBelow(int lineNumber, int lineCount)
		{
			int num = lineNumber;
			if (this.document.TextEditorProperties.EnableFolding)
			{
				for (int i = 0; i < lineCount; i++)
				{
					num--;
					while (num >= 0 && !this.document.FoldingManager.IsLineVisible(num))
					{
						num--;
					}
				}
			}
			else
			{
				num -= lineCount;
			}
			return Math.Max(0, num);
		}
		protected virtual void OnLineCountChanged(LineManagerEventArgs e)
		{
			if (this.LineCountChanged != null)
			{
				this.LineCountChanged(this, e);
			}
		}
		private ISegment NextDelimiter(string text, int offset)
		{
			int i = offset;
			while (i < text.Length)
			{
				char c = text[i];
				if (c != '\n')
				{
					if (c != '\r')
					{
						i++;
						continue;
					}
					if (i + 1 < text.Length && text[i + 1] == '\n')
					{
						this.delimiterSegment.Offset = i;
						this.delimiterSegment.Length = 2;
						return this.delimiterSegment;
					}
				}
				this.delimiterSegment.Offset = i;
				this.delimiterSegment.Length = 1;
				return this.delimiterSegment;
			}
			return null;
		}
		protected virtual void OnLineLengthChanged(LineLengthEventArgs e)
		{
			if (this.LineLengthChanged != null)
			{
				this.LineLengthChanged(this, e);
			}
		}
	}
}
