using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace LTP.TextEditor.Document
{
	public class CustomLineManager : ICustomLineManager
	{
		private ArrayList lines = new ArrayList();
        public event EventHandler BeforeChanged;
        public event EventHandler Changed;
		public ArrayList CustomLines
		{
			get
			{
				return this.lines;
			}
		}
		public CustomLineManager(ILineManager lineTracker)
		{
			lineTracker.LineCountChanged += new LineManagerEventHandler(this.MoveIndices);
		}
		public Color GetCustomColor(int lineNr, Color defaultColor)
		{
			foreach (CustomLine customLine in this.lines)
			{
				if (customLine.StartLineNr <= lineNr && customLine.EndLineNr >= lineNr)
				{
					return customLine.Color;
				}
			}
			return defaultColor;
		}
		public bool IsReadOnly(int lineNr, bool defaultReadOnly)
		{
			foreach (CustomLine customLine in this.lines)
			{
				if (customLine.StartLineNr <= lineNr && customLine.EndLineNr >= lineNr)
				{
					return customLine.ReadOnly;
				}
			}
			return defaultReadOnly;
		}
		public bool IsReadOnly(ISelection selection, bool defaultReadOnly)
		{
			int y = selection.StartPosition.Y;
			int y2 = selection.EndPosition.Y;
			foreach (CustomLine customLine in this.lines)
			{
				if (customLine.ReadOnly && (y >= customLine.StartLineNr || y2 >= customLine.StartLineNr) && (y <= customLine.EndLineNr || y2 <= customLine.EndLineNr))
				{
					return true;
				}
			}
			return defaultReadOnly;
		}
		public void Clear()
		{
			this.OnBeforeChanged();
			this.lines.Clear();
			this.OnChanged();
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
		public void AddCustomLine(int lineNr, Color customColor, bool readOnly)
		{
			this.OnBeforeChanged();
			this.lines.Add(new CustomLine(lineNr, customColor, readOnly));
			this.OnChanged();
		}
		public void AddCustomLine(int startLineNr, int endLineNr, Color customColor, bool readOnly)
		{
			this.OnBeforeChanged();
			this.lines.Add(new CustomLine(startLineNr, endLineNr, customColor, readOnly));
			this.OnChanged();
		}
		public void RemoveCustomLine(int lineNr)
		{
			for (int i = 0; i < this.lines.Count; i++)
			{
				if (((CustomLine)this.lines[i]).StartLineNr <= lineNr && ((CustomLine)this.lines[i]).EndLineNr >= lineNr)
				{
					this.OnBeforeChanged();
					this.lines.RemoveAt(i);
					this.OnChanged();
					return;
				}
			}
		}
		private void MoveIndices(object sender, LineManagerEventArgs e)
		{
			bool flag = false;
			this.OnBeforeChanged();
			for (int i = 0; i < this.lines.Count; i++)
			{
				int startLineNr = ((CustomLine)this.lines[i]).StartLineNr;
				int endLineNr = ((CustomLine)this.lines[i]).EndLineNr;
				if (e.LineStart >= startLineNr && e.LineStart < endLineNr)
				{
					flag = true;
					((CustomLine)this.lines[i]).EndLineNr += e.LinesMoved;
				}
				else
				{
					if (e.LineStart < startLineNr)
					{
						((CustomLine)this.lines[i]).StartLineNr += e.LinesMoved;
						((CustomLine)this.lines[i]).EndLineNr += e.LinesMoved;
					}
				}
			}
			if (flag)
			{
				this.OnChanged();
			}
		}
	}
}
