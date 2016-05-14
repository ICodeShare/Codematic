using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class ToggleComment : AbstractEditAction
	{
		private int firstLine;
		private int lastLine;
		private void RemoveCommentAt(IDocument document, string comment, ISelection selection, int y1, int y2)
		{
			int num = 0;
			this.firstLine = y1;
			this.lastLine = y2;
			for (int i = y2; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if (selection != null && i == y2 && lineSegment.Offset == selection.Offset + selection.Length)
				{
					this.lastLine--;
				}
				else
				{
					string text = document.GetText(lineSegment.Offset, lineSegment.Length);
					if (text.Trim().StartsWith(comment))
					{
						document.Remove(lineSegment.Offset + text.IndexOf(comment), comment.Length);
						num++;
					}
				}
			}
			if (num > 0)
			{
				document.UndoStack.UndoLast(num);
			}
		}
		private void SetCommentAt(IDocument document, string comment, ISelection selection, int y1, int y2)
		{
			int num = 0;
			this.firstLine = y1;
			this.lastLine = y2;
			for (int i = y2; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if (selection != null && i == y2 && lineSegment.Offset == selection.Offset + selection.Length)
				{
					this.lastLine--;
				}
				else
				{
					document.GetText(lineSegment.Offset, lineSegment.Length);
					document.Insert(lineSegment.Offset, comment);
					num++;
				}
			}
			if (num > 0)
			{
				document.UndoStack.UndoLast(num);
			}
		}
		private bool ShouldComment(IDocument document, string comment, ISelection selection, int startLine, int endLine)
		{
			for (int i = endLine; i >= startLine; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if (selection != null && i == endLine && lineSegment.Offset == selection.Offset + selection.Length)
				{
					this.lastLine--;
				}
				else
				{
					string text = document.GetText(lineSegment.Offset, lineSegment.Length);
					if (!text.Trim().StartsWith(comment))
					{
						return true;
					}
				}
			}
			return false;
		}
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			string text = null;
			if (textArea.Document.HighlightingStrategy.Properties["LineComment"] != null)
			{
				text = textArea.Document.HighlightingStrategy.Properties["LineComment"].ToString();
			}
			if (text == null || text.Length == 0)
			{
				return;
			}
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				bool flag = true;
				foreach (ISelection current in textArea.SelectionManager.SelectionCollection)
				{
					if (!this.ShouldComment(textArea.Document, text, current, current.StartPosition.Y, current.EndPosition.Y))
					{
						flag = false;
						break;
					}
				}
				foreach (ISelection current2 in textArea.SelectionManager.SelectionCollection)
				{
					textArea.BeginUpdate();
					if (flag)
					{
						this.SetCommentAt(textArea.Document, text, current2, current2.StartPosition.Y, current2.EndPosition.Y);
					}
					else
					{
						this.RemoveCommentAt(textArea.Document, text, current2, current2.StartPosition.Y, current2.EndPosition.Y);
					}
					textArea.Document.UpdateQueue.Clear();
					textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.LinesBetween, this.firstLine, this.lastLine));
					textArea.EndUpdate();
				}
				textArea.Document.CommitUpdate();
				textArea.AutoClearSelection = false;
				return;
			}
			textArea.BeginUpdate();
			int line = textArea.Caret.Line;
			if (this.ShouldComment(textArea.Document, text, null, line, line))
			{
				this.SetCommentAt(textArea.Document, text, null, line, line);
			}
			else
			{
				this.RemoveCommentAt(textArea.Document, text, null, line, line);
			}
			textArea.Document.UpdateQueue.Clear();
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, line));
			textArea.EndUpdate();
		}
	}
}
