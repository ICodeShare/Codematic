using System;
using System.Collections;
using System.Runtime.CompilerServices;
namespace LTP.TextEditor.Document
{
	public class BookmarkManager : IBookMarkManager
	{
		private ArrayList bookmark = new ArrayList();
        public event EventHandler BeforeChanged;
        public event EventHandler Changed;
		public ArrayList Marks
		{
			get
			{
				return this.bookmark;
			}
		}
		public int FirstMark
		{
			get
			{
				if (this.bookmark.Count < 1)
				{
					return -1;
				}
				int num = (int)this.bookmark[0];
				for (int i = 1; i < this.bookmark.Count; i++)
				{
					num = Math.Min(num, (int)this.bookmark[i]);
				}
				return num;
			}
		}
		public int LastMark
		{
			get
			{
				if (this.bookmark.Count < 1)
				{
					return -1;
				}
				int num = (int)this.bookmark[0];
				for (int i = 1; i < this.bookmark.Count; i++)
				{
					num = Math.Max(num, (int)this.bookmark[i]);
				}
				return num;
			}
		}
		public BookmarkManager(ILineManager lineTracker)
		{
			lineTracker.LineCountChanged += new LineManagerEventHandler(this.MoveIndices);
		}
		private void OnChanged()
		{
			if (this.Changed != null)
			{
				this.Changed(this, null);
			}
		}
		private void OnBeforeChanged()
		{
			if (this.BeforeChanged != null)
			{
				this.BeforeChanged(this, null);
			}
		}
		public void ToggleMarkAt(int lineNr)
		{
			this.OnBeforeChanged();
			for (int i = 0; i < this.bookmark.Count; i++)
			{
				if ((int)this.bookmark[i] == lineNr)
				{
					this.bookmark.RemoveAt(i);
					this.OnChanged();
					return;
				}
			}
			this.bookmark.Add(lineNr);
			this.OnChanged();
		}
		public bool IsMarked(int lineNr)
		{
			for (int i = 0; i < this.bookmark.Count; i++)
			{
				if ((int)this.bookmark[i] == lineNr)
				{
					return true;
				}
			}
			return false;
		}
		private void MoveIndices(object sender, LineManagerEventArgs e)
		{
			bool flag = false;
			this.OnBeforeChanged();
			for (int i = 0; i < this.bookmark.Count; i++)
			{
				int num = (int)this.bookmark[i];
				if (e.LinesMoved < 0 && num + 1 >= e.LineStart && num + 1 < e.LineStart - e.LinesMoved)
				{
					this.bookmark.RemoveAt(i);
					i--;
					flag = true;
				}
				else
				{
					if (num >= e.LineStart + 1 || (e.LinesMoved < 0 && num > e.LineStart))
					{
						flag = true;
						this.bookmark[i] = num + e.LinesMoved;
					}
				}
			}
			if (flag)
			{
				this.OnChanged();
			}
		}
		public void Clear()
		{
			this.OnBeforeChanged();
			this.bookmark.Clear();
			this.OnChanged();
		}
		public int GetNextMark(int curLineNr)
		{
			int num = -1;
			for (int i = 0; i < this.bookmark.Count; i++)
			{
				int num2 = (int)this.bookmark[i];
				if (num2 > curLineNr)
				{
					num = ((num == -1) ? num2 : Math.Min(num, num2));
				}
			}
			if (num != -1)
			{
				return num;
			}
			return this.FirstMark;
		}
		public int GetPrevMark(int curLineNr)
		{
			int num = -1;
			for (int i = 0; i < this.bookmark.Count; i++)
			{
				int num2 = (int)this.bookmark[i];
				if (num2 < curLineNr)
				{
					num = ((num == -1) ? num2 : Math.Max(num, num2));
				}
			}
			if (num != -1)
			{
				return num;
			}
			return this.LastMark;
		}
	}
}
