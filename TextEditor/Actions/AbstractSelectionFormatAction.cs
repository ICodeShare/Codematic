using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Actions
{
	public abstract class AbstractSelectionFormatAction : AbstractEditAction
	{
		protected TextArea textArea;
		protected abstract void Convert(IDocument document, int offset, int length);
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
						this.Convert(textArea.Document, current.Offset, current.Length);
					}
					goto IL_7F;
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
			this.Convert(textArea.Document, 0, textArea.Document.TextLength);
			IL_7F:
			textArea.Caret.ValidateCaretPos();
			textArea.EndUpdate();
			textArea.Refresh();
		}
	}
}
