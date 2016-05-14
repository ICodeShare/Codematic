using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
namespace LTP.TextEditor.Document
{
	public class FoldingManager
	{
		private ArrayList foldMarker = new ArrayList();
		private IFoldingStrategy foldingStrategy;
		private IDocument document;
        public event EventHandler FoldingsChanged;
		public ArrayList FoldMarker
		{
			get
			{
				return this.foldMarker;
			}
		}
		public IFoldingStrategy FoldingStrategy
		{
			get
			{
				return this.foldingStrategy;
			}
			set
			{
				this.foldingStrategy = value;
			}
		}
		public FoldingManager(IDocument document, ILineManager lineTracker)
		{
			this.document = document;
			document.DocumentChanged += new DocumentEventHandler(this.DocumentChanged);
		}
		private void DocumentChanged(object sender, DocumentEventArgs e)
		{
			int count = this.foldMarker.Count;
			this.document.UpdateSegmentListOnDocumentChange(this.foldMarker, e);
			if (count != this.foldMarker.Count)
			{
				this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
			}
		}
		public ArrayList GetFoldingsFromPosition(int line, int column)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				for (int i = 0; i < this.foldMarker.Count; i++)
				{
					FoldMarker foldMarker = (FoldMarker)this.foldMarker[i];
					if ((foldMarker.StartLine == line && column > foldMarker.StartColumn && (foldMarker.EndLine != line || column < foldMarker.EndColumn)) || (foldMarker.EndLine == line && column < foldMarker.EndColumn && (foldMarker.StartLine != line || column > foldMarker.StartColumn)) || (line > foldMarker.StartLine && line < foldMarker.EndLine))
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public ArrayList GetFoldingsWithStart(int lineNumber)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.StartLine == lineNumber)
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public ArrayList GetFoldedFoldingsWithStart(int lineNumber)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.IsFolded && foldMarker.StartLine == lineNumber)
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public ArrayList GetFoldedFoldingsWithStartAfterColumn(int lineNumber, int column)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.IsFolded && foldMarker.StartLine == lineNumber && foldMarker.StartColumn > column)
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public ArrayList GetFoldingsWithEnd(int lineNumber)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.EndLine == lineNumber)
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public ArrayList GetFoldedFoldingsWithEnd(int lineNumber)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.IsFolded && foldMarker.EndLine == lineNumber)
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public bool IsFoldStart(int lineNumber)
		{
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.StartLine == lineNumber)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
		public bool IsFoldEnd(int lineNumber)
		{
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.EndLine == lineNumber)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
		public ArrayList GetFoldingsContainsLineNumber(int lineNumber)
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.StartLine < lineNumber && lineNumber < foldMarker.EndLine)
					{
						arrayList.Add(foldMarker);
					}
				}
			}
			return arrayList;
		}
		public bool IsBetweenFolding(int lineNumber)
		{
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.StartLine < lineNumber && lineNumber < foldMarker.EndLine)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
		public bool IsLineVisible(int lineNumber)
		{
			if (this.foldMarker != null)
			{
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.IsFolded && foldMarker.StartLine < lineNumber && lineNumber < foldMarker.EndLine)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}
		public ArrayList GetTopLevelFoldedFoldings()
		{
			ArrayList arrayList = new ArrayList();
			if (this.foldMarker != null)
			{
				Point point = new Point(0, 0);
				foreach (FoldMarker foldMarker in this.foldMarker)
				{
					if (foldMarker.IsFolded && (foldMarker.StartLine > point.Y || (foldMarker.StartLine == point.Y && foldMarker.StartColumn >= point.X)))
					{
						arrayList.Add(foldMarker);
						point = new Point(foldMarker.EndColumn, foldMarker.EndLine);
					}
				}
			}
			return arrayList;
		}
		public void UpdateFoldings(string fileName, object parseInfo)
		{
			int count = this.foldMarker.Count;
			Monitor.Enter(this);
			try
			{
				ArrayList arrayList = this.foldingStrategy.GenerateFoldMarkers(this.document, fileName, parseInfo);
				if (arrayList != null && arrayList.Count != 0)
				{
					arrayList.Sort();
					if (this.foldMarker.Count == arrayList.Count)
					{
						for (int i = 0; i < this.foldMarker.Count; i++)
						{
							((FoldMarker)arrayList[i]).IsFolded = ((FoldMarker)this.foldMarker[i]).IsFolded;
						}
						this.foldMarker = arrayList;
					}
					else
					{
						int num = 0;
						int num2 = 0;
						while (num < this.foldMarker.Count && num2 < arrayList.Count)
						{
							int num3 = ((FoldMarker)arrayList[num2]).CompareTo(this.foldMarker[num]);
							if (num3 > 0)
							{
								num++;
							}
							else
							{
								if (num3 == 0)
								{
									((FoldMarker)arrayList[num2]).IsFolded = ((FoldMarker)this.foldMarker[num]).IsFolded;
								}
								num2++;
							}
						}
					}
				}
				if (arrayList != null)
				{
					this.foldMarker = arrayList;
				}
				else
				{
					this.foldMarker.Clear();
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
			if (count != this.foldMarker.Count)
			{
				this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
				this.document.CommitUpdate();
			}
		}
		public string SerializeToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FoldMarker foldMarker in this.foldMarker)
			{
				stringBuilder.Append(foldMarker.Offset);
				stringBuilder.Append("\n");
				stringBuilder.Append(foldMarker.Length);
				stringBuilder.Append("\n");
				stringBuilder.Append(foldMarker.FoldText);
				stringBuilder.Append("\n");
				stringBuilder.Append(foldMarker.IsFolded);
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}
		public void DeserializeFromString(string str)
		{
			try
			{
				string[] array = str.Split(new char[]
				{
					'\n'
				});
				int num = 0;
				while (num < array.Length && array[num].Length > 0)
				{
					int num2 = int.Parse(array[num]);
					int num3 = int.Parse(array[num + 1]);
					string foldText = array[num + 2];
					bool isFolded = bool.Parse(array[num + 3]);
					bool flag = false;
					foreach (FoldMarker foldMarker in this.foldMarker)
					{
						if (foldMarker.Offset == num2 && foldMarker.Length == num3)
						{
							foldMarker.IsFolded = isFolded;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.foldMarker.Add(new FoldMarker(this.document, num2, num3, foldText, isFolded));
					}
					num += 4;
				}
				if (array.Length > 0)
				{
					this.NotifyFoldingsChanged(EventArgs.Empty);
				}
			}
			catch (Exception)
			{
			}
		}
		public void NotifyFoldingsChanged(EventArgs e)
		{
			if (this.FoldingsChanged != null)
			{
				this.FoldingsChanged(this, e);
			}
		}
	}
}
