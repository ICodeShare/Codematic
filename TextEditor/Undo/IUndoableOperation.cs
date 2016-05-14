using System;
namespace LTP.TextEditor.Undo
{
	public interface IUndoableOperation
	{
		void Undo();
		void Redo();
	}
}
