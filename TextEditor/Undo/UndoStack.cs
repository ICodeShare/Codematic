using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace LTP.TextEditor.Undo
{
	public class UndoStack
	{
		private class UndoableSetCaretPosition : IUndoableOperation
		{
			private UndoStack stack;
			private Point pos;
			private Point redoPos;
			public UndoableSetCaretPosition(UndoStack stack, Point pos)
			{
				this.stack = stack;
				this.pos = pos;
			}
			public void Undo()
			{
				this.redoPos = this.stack.TextEditorControl.ActiveTextAreaControl.Caret.Position;
				this.stack.TextEditorControl.ActiveTextAreaControl.Caret.Position = this.pos;
			}
			public void Redo()
			{
				this.stack.TextEditorControl.ActiveTextAreaControl.Caret.Position = this.redoPos;
			}
		}
		private Stack undostack = new Stack();
		private Stack redostack = new Stack();
		public TextEditorControlBase TextEditorControl;
		public bool AcceptChanges = true;
        public event EventHandler ActionUndone;
        public event EventHandler ActionRedone;
		internal Stack _UndoStack
		{
			get
			{
				return this.undostack;
			}
		}
		public bool CanUndo
		{
			get
			{
				return this.undostack.Count > 0;
			}
		}
		public bool CanRedo
		{
			get
			{
				return this.redostack.Count > 0;
			}
		}
		public void UndoLast(int x)
		{
			this.undostack.Push(new UndoQueue(this, x));
		}
		public void Undo()
		{
			if (this.undostack.Count > 0)
			{
				IUndoableOperation undoableOperation = (IUndoableOperation)this.undostack.Pop();
				this.redostack.Push(undoableOperation);
				undoableOperation.Undo();
				this.OnActionUndone();
			}
		}
		public void Redo()
		{
			if (this.redostack.Count > 0)
			{
				IUndoableOperation undoableOperation = (IUndoableOperation)this.redostack.Pop();
				this.undostack.Push(undoableOperation);
				undoableOperation.Redo();
				this.OnActionRedone();
			}
		}
		public void Push(IUndoableOperation operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("UndoStack.Push(UndoableOperation operation) : operation can't be null");
			}
			if (this.AcceptChanges)
			{
				this.undostack.Push(operation);
				if (this.TextEditorControl != null)
				{
					this.undostack.Push(new UndoStack.UndoableSetCaretPosition(this, this.TextEditorControl.ActiveTextAreaControl.Caret.Position));
					this.UndoLast(2);
				}
				this.ClearRedoStack();
			}
		}
		public void ClearRedoStack()
		{
			this.redostack.Clear();
		}
		public void ClearAll()
		{
			this.undostack.Clear();
			this.redostack.Clear();
		}
		protected void OnActionUndone()
		{
			if (this.ActionUndone != null)
			{
				this.ActionUndone(null, null);
			}
		}
		protected void OnActionRedone()
		{
			if (this.ActionRedone != null)
			{
				this.ActionRedone(null, null);
			}
		}
	}
}
