using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
namespace LTP.TextEditor.Document
{
	public class SelectionManager
	{
		private IDocument document;
		private SelectionCollection selectionCollection = new SelectionCollection();
        public event EventHandler SelectionChanged;
		public SelectionCollection SelectionCollection
		{
			get
			{
				return this.selectionCollection;
			}
		}
		public bool HasSomethingSelected
		{
			get
			{
				return this.selectionCollection.Count > 0;
			}
		}
		public string SelectedText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ISelection current in this.selectionCollection)
				{
					stringBuilder.Append(current.SelectedText);
				}
				return stringBuilder.ToString();
			}
		}
		public SelectionManager(IDocument document)
		{
			this.document = document;
			document.DocumentChanged += new DocumentEventHandler(this.DocumentChanged);
		}
		public void Dispose()
		{
			if (this.document != null)
			{
				this.document.DocumentChanged -= new DocumentEventHandler(this.DocumentChanged);
				this.document = null;
			}
		}
		private void DocumentChanged(object sender, DocumentEventArgs e)
		{
			if (e.Text == null)
			{
				this.Remove(e.Offset, e.Length);
				return;
			}
			if (e.Length < 0)
			{
				this.Insert(e.Offset, e.Text);
				return;
			}
			this.Replace(e.Offset, e.Length, e.Text);
		}
		public void SetSelection(ISelection selection)
		{
			if (selection == null)
			{
				this.ClearSelection();
				return;
			}
			if (this.SelectionCollection.Count == 1 && selection.StartPosition == this.SelectionCollection[0].StartPosition && selection.EndPosition == this.SelectionCollection[0].EndPosition)
			{
				return;
			}
			this.ClearWithoutUpdate();
			this.selectionCollection.Add(selection);
			this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.LinesBetween, selection.StartPosition.Y, selection.EndPosition.Y));
			this.document.CommitUpdate();
			this.OnSelectionChanged(EventArgs.Empty);
		}
		public void SetSelection(Point startPosition, Point endPosition)
		{
			this.SetSelection(new DefaultSelection(this.document, startPosition, endPosition));
		}
		public bool GreaterEqPos(Point p1, Point p2)
		{
			return p1.Y > p2.Y || (p1.Y == p2.Y && p1.X >= p2.X);
		}
		public void ExtendSelection(Point oldPosition, Point newPosition)
		{
			if (oldPosition == newPosition)
			{
				Console.WriteLine("BLUB");
				return;
			}
			bool flag = this.GreaterEqPos(oldPosition, newPosition);
			Point startPosition;
			Point endPosition;
			if (flag)
			{
				startPosition = newPosition;
				endPosition = oldPosition;
			}
			else
			{
				startPosition = oldPosition;
				endPosition = newPosition;
			}
			if (!this.HasSomethingSelected)
			{
				this.SetSelection(new DefaultSelection(this.document, startPosition, endPosition));
				Console.WriteLine("SET");
				return;
			}
			ISelection selection = this.selectionCollection[0];
			bool flag2 = false;
			if (selection.ContainsPosition(newPosition))
			{
				if (flag)
				{
					if (selection.EndPosition != newPosition)
					{
						selection.EndPosition = newPosition;
						flag2 = true;
					}
				}
				else
				{
					if (selection.StartPosition != newPosition)
					{
						selection.StartPosition = newPosition;
						flag2 = true;
					}
				}
			}
			else
			{
				if (oldPosition == selection.StartPosition)
				{
					if (this.GreaterEqPos(newPosition, selection.EndPosition))
					{
						if (selection.StartPosition != selection.EndPosition || selection.EndPosition != newPosition)
						{
							selection.StartPosition = selection.EndPosition;
							selection.EndPosition = newPosition;
							flag2 = true;
						}
					}
					else
					{
						if (selection.StartPosition != newPosition)
						{
							selection.StartPosition = newPosition;
							flag2 = true;
						}
					}
				}
				else
				{
					if (this.GreaterEqPos(selection.StartPosition, newPosition))
					{
						if (selection.EndPosition != selection.StartPosition || selection.StartPosition != newPosition)
						{
						}
						selection.EndPosition = selection.StartPosition;
						selection.StartPosition = newPosition;
						flag2 = true;
					}
					else
					{
						if (selection.EndPosition != newPosition)
						{
							selection.EndPosition = newPosition;
							flag2 = true;
						}
					}
				}
			}
			Console.WriteLine("GGG" + flag2);
			if (flag2)
			{
				this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.LinesBetween, startPosition.Y, endPosition.Y));
				this.document.CommitUpdate();
				this.OnSelectionChanged(EventArgs.Empty);
			}
		}
		private void ClearWithoutUpdate()
		{
			while (this.selectionCollection.Count > 0)
			{
				ISelection selection = this.selectionCollection[this.selectionCollection.Count - 1];
				this.selectionCollection.RemoveAt(this.selectionCollection.Count - 1);
				this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.LinesBetween, selection.StartPosition.Y, selection.EndPosition.Y));
				this.OnSelectionChanged(EventArgs.Empty);
			}
		}
		public void ClearSelection()
		{
			this.ClearWithoutUpdate();
			this.document.CommitUpdate();
		}
		public void RemoveSelectedText()
		{
			ArrayList arrayList = new ArrayList();
			int num = -1;
			bool flag = true;
			foreach (ISelection current in this.selectionCollection)
			{
				if (flag)
				{
					int y = current.StartPosition.Y;
					if (y != current.EndPosition.Y)
					{
						flag = false;
					}
					else
					{
						arrayList.Add(y);
					}
				}
				num = current.Offset;
				this.document.Remove(current.Offset, current.Length);
			}
			this.ClearSelection();
			if (num != -1)
			{
				if (flag)
				{
					IEnumerator enumerator2 = arrayList.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							int singleLine = (int)enumerator2.Current;
							this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, singleLine));
						}
						goto IL_105;
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
				this.document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
				IL_105:
				this.document.CommitUpdate();
			}
		}
		private bool SelectionsOverlap(ISelection s1, ISelection s2)
		{
			return (s1.Offset <= s2.Offset && s2.Offset <= s1.Offset + s1.Length) || (s1.Offset <= s2.Offset + s2.Length && s2.Offset + s2.Length <= s1.Offset + s1.Length) || (s1.Offset >= s2.Offset && s1.Offset + s1.Length <= s2.Offset + s2.Length);
		}
		public bool IsSelected(int offset)
		{
			return this.GetSelectionAt(offset) != null;
		}
		public ISelection GetSelectionAt(int offset)
		{
			foreach (ISelection current in this.selectionCollection)
			{
				if (current.ContainsOffset(offset))
				{
					return current;
				}
			}
			return null;
		}
		public void Insert(int offset, string text)
		{
		}
		public void Remove(int offset, int length)
		{
		}
		public void Replace(int offset, int length, string text)
		{
		}
		public ColumnRange GetSelectionAtLine(int lineNumber)
		{
			foreach (ISelection current in this.selectionCollection)
			{
				int y = current.StartPosition.Y;
				int y2 = current.EndPosition.Y;
				if (y < lineNumber && lineNumber < y2)
				{
					ColumnRange result = ColumnRange.WholeColumn;
					return result;
				}
				if (y == lineNumber)
				{
					LineSegment lineSegment = this.document.GetLineSegment(y);
					int x = current.StartPosition.X;
					int endColumn = (y2 == lineNumber) ? current.EndPosition.X : (lineSegment.Length + 1);
					ColumnRange result = new ColumnRange(x, endColumn);
					return result;
				}
				if (y2 == lineNumber)
				{
					int x2 = current.EndPosition.X;
					ColumnRange result = new ColumnRange(0, x2);
					return result;
				}
			}
			return ColumnRange.NoColumn;
		}
		public void FireSelectionChanged()
		{
			this.OnSelectionChanged(EventArgs.Empty);
		}
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, e);
			}
		}
	}
}
