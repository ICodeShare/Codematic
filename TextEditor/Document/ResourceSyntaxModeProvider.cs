using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		private ArrayList syntaxModes;
		public ArrayList SyntaxModes
		{
			get
			{
				return this.syntaxModes;
			}
		}
		public ResourceSyntaxModeProvider()
		{
			Stream baseStream = new StreamReader(Application.StartupPath + "\\TextStyle\\SyntaxModes.xml", Encoding.Default).BaseStream;
			if (baseStream == null)
			{
				throw new ApplicationException();
			}
			this.syntaxModes = SyntaxMode.GetSyntaxModes(baseStream);
		}
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			Stream baseStream = new StreamReader(Application.StartupPath + "\\TextStyle\\" + syntaxMode.FileName, Encoding.Default).BaseStream;
			return new XmlTextReader(baseStream);
		}
	}
}
