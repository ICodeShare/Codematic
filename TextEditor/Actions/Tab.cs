using LTP.TextEditor.Document;
using System;
using System.Text;
namespace LTP.TextEditor.Actions
{
	public class Tab : AbstractEditAction
	{
		public static string GetIndentationString(IDocument document)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (document.TextEditorProperties.ConvertTabsToSpaces)
			{
				stringBuilder.Append(new string(' ', document.TextEditorProperties.TabIndent));
			}
			else
			{
				stringBuilder.Append('\t');
			}
			return stringBuilder.ToString();
		}
		private void InsertTabs(IDocument document, ISelection selection, int y1, int y2)
		{
			int num = 0;
			string indentationString = Tab.GetIndentationString(document);
			for (int i = y2; i >= y1; i--)
			{
				LineSegment lineSegment = document.GetLineSegment(i);
				if (i != y2 || i != selection.EndPosition.Y || selection.EndPosition.X != 0)
				{
					document.Insert(lineSegment.Offset, indentationString);
					num++;
				}
			}
			if (num > 0)
			{
				document.UndoStack.UndoLast(num);
			}
		}
		private void InsertTabAtCaretPosition(TextArea textArea)
		{
			switch (textArea.Caret.CaretMode)
			{
			case CaretMode.InsertMode:
				textArea.InsertString(Tab.GetIndentationString(textArea.Document));
				break;
			case CaretMode.OverwriteMode:
			{
				string indentationString = Tab.GetIndentationString(textArea.Document);
				textArea.ReplaceChar(indentationString[0]);
				if (indentationString.Length > 1)
				{
					textArea.InsertString(indentationString.Substring(1));
				}
				break;
			}
			}
			textArea.SetDesiredColumn();
		}
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				foreach (ISelection current in textArea.SelectionManager.SelectionCollection)
				{
					int y = current.StartPosition.Y;
					int y2 = current.EndPosition.Y;
					if (y == y2)
					{
						this.InsertTabAtCaretPosition(textArea);
						break;
					}
					textArea.BeginUpdate();
					this.InsertTabs(textArea.Document, current, y, y2);
					textArea.Document.UpdateQueue.Clear();
					textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.LinesBetween, y, y2));
					textArea.EndUpdate();
				}
				textArea.Document.CommitUpdate();
				textArea.AutoClearSelection = false;
				return;
			}
			this.InsertTabAtCaretPosition(textArea);
		}
	}
}
