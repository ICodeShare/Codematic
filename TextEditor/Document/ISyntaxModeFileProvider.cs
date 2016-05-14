using System;
using System.Collections;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public interface ISyntaxModeFileProvider
	{
		ArrayList SyntaxModes
		{
			get;
		}
		XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode);
	}
}
