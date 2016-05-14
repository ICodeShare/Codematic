using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public abstract class AbstractLineFormatAction : AbstractEditAction
	{
		protected TextArea textArea;
		protected abstract void Convert(IDocument document, int startLine, int endLine);
		public override void Execute(TextArea textArea)
		{
			this.textArea = textArea;
			textArea.BeginUpdate();
			if (textArea.SelectionManager.HasSomethingSelected)
			{
				SelectionCollection.ISelectionEnumerator enumerator = textArea.SelectionManager.SelectionCollection.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ISelection current = enumerator.Current;
						this.Convert(textArea.Document, current.StartPosition.Y, current.EndPosition.Y);
					}
					goto IL_94;
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
			this.Convert(textArea.Document, 0, textArea.Document.TotalNumberOfLines - 1);
			IL_94:
			textArea.Caret.ValidateCaretPos();
			textArea.EndUpdate();
			textArea.Refresh();
		}
	}
}
