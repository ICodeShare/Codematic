using System;
using System.Windows.Forms;
namespace LTP.TextEditor.Actions
{
	public abstract class AbstractEditAction : IEditAction
	{
		private Keys[] keys;
		public Keys[] Keys
		{
			get
			{
				return this.keys;
			}
			set
			{
				this.keys = value;
			}
		}
		public abstract void Execute(TextArea textArea);
	}
}
