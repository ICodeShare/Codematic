using System;
using System.Windows.Forms;
namespace LTP.TextEditor.Actions
{
	public interface IEditAction
	{
		Keys[] Keys
		{
			get;
			set;
		}
		void Execute(TextArea textArea);
	}
}
