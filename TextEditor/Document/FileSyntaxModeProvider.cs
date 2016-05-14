using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class FileSyntaxModeProvider : ISyntaxModeFileProvider
	{
		private string directory;
		private ArrayList syntaxModes;
		public ArrayList SyntaxModes
		{
			get
			{
				return this.syntaxModes;
			}
		}
		public FileSyntaxModeProvider(string directory)
		{
			this.directory = directory;
			string path = Path.Combine(directory, "SyntaxModes.xml");
			if (File.Exists(path))
			{
				Stream stream = File.OpenRead(path);
				this.syntaxModes = SyntaxMode.GetSyntaxModes(stream);
				stream.Close();
				return;
			}
			this.syntaxModes = this.ScanDirectory(directory);
		}
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			string text = Path.Combine(this.directory, syntaxMode.FileName);
			if (!File.Exists(text))
			{
				MessageBox.Show("Can't load highlighting definition " + text + " (file not found)!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				return null;
			}
			return new XmlTextReader(File.OpenRead(text));
		}
		private ArrayList ScanDirectory(string directory)
		{
			string[] files = Directory.GetFiles(directory);
			ArrayList arrayList = new ArrayList();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (Path.GetExtension(text).ToUpper() == ".XSHD")
				{
					XmlTextReader xmlTextReader = new XmlTextReader(text);
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType == XmlNodeType.Element)
						{
							string name;
							if ((name = xmlTextReader.Name) != null && name == "SyntaxDefinition")
							{
								string attribute = xmlTextReader.GetAttribute("name");
								string attribute2 = xmlTextReader.GetAttribute("extensions");
								arrayList.Add(new SyntaxMode(Path.GetFileName(text), attribute, attribute2));
								break;
							}
							MessageBox.Show("Unknown root node in syntax highlighting file :" + xmlTextReader.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
							break;
						}
					}
					xmlTextReader.Close();
				}
			}
			return arrayList;
		}
	}
}
