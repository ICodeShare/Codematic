using System;
using System.Collections;
namespace LTP.TextEditor.Undo
{
	public class UndoQueue : IUndoableOperation
	{
		private ArrayList undolist = new ArrayList();
		public UndoQueue(UndoStack stack, int numops)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			for (int i = 0; i < numops; i++)
			{
				if (stack._UndoStack.Count > 0)
				{
					this.undolist.Add(stack._UndoStack.Pop());
				}
			}
		}
		public void Undo()
		{
			for (int i = 0; i < this.undolist.Count; i++)
			{
				((IUndoableOperation)this.undolist[i]).Undo();
			}
		}
		public void Redo()
		{
			for (int i = this.undolist.Count - 1; i >= 0; i--)
			{
				((IUndoableOperation)this.undolist[i]).Redo();
			}
		}
	}
}
