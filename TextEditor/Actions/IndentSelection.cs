using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public class IndentSelection : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Document.ReadOnly)
			{
				return;
			}
			textArea.BeginUpdate();
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				SelectionCollection.ISelectionEnumerator enumerator = textArea.SelectionManager.SelectionCollection.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ISelection current = enumerator.Current;
						textArea.Document.FormattingStrategy.IndentLines(textArea, current.StartPosition.Y, current.EndPosition.Y);
					}
					goto IL_A5;
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
			textArea.Document.FormattingStrategy.IndentLines(textArea, 0, textArea.Document.TotalNumberOfLines - 1);
			IL_A5:
			textArea.EndUpdate();
			textArea.Refresh();
		}
	}
}
