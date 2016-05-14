using System;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public interface IDeclarationViewWindow
	{
		string Description
		{
			get;
			set;
		}
		void ShowDeclarationViewWindow();
		void CloseDeclarationViewWindow();
	}
}
