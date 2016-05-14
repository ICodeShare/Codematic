using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public interface ICustomLineManager
	{
		event EventHandler BeforeChanged;
		event EventHandler Changed;
		ArrayList CustomLines
		{
			get;
		}
		Color GetCustomColor(int lineNr, Color defaultColor);
		bool IsReadOnly(int lineNr, bool defaultReadOnly);
		bool IsReadOnly(ISelection selection, bool defaultReadOnly);
		void AddCustomLine(int lineNr, Color customColor, bool readOnly);
		void AddCustomLine(int startLineNr, int endLineNr, Color customColor, bool readOnly);
		void RemoveCustomLine(int lineNr);
		void Clear();
	}
}
