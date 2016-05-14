using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class SyntaxMode
	{
		private string fileName;
		private string name;
		private string[] extensions;
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}
		public string[] Extensions
		{
			get
			{
				return this.extensions;
			}
			set
			{
				this.extensions = value;
			}
		}
		public SyntaxMode(string fileName, string name, string extensions)
		{
			this.fileName = fileName;
			this.name = name;
			this.extensions = extensions.Split(new char[]
			{
				';',
				'|',
				','
			});
		}
		public SyntaxMode(string fileName, string name, string[] extensions)
		{
			this.fileName = fileName;
			this.name = name;
			this.extensions = extensions;
		}
		public static ArrayList GetSyntaxModes(Stream xmlSyntaxModeStream)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(xmlSyntaxModeStream);
			ArrayList arrayList = new ArrayList();
			while (xmlTextReader.Read())
			{
				XmlNodeType nodeType = xmlTextReader.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					string a;
					if ((a = xmlTextReader.Name) != null)
					{
						if (!(a == "SyntaxModes"))
						{
							if (a == "Mode")
							{
								arrayList.Add(new SyntaxMode(xmlTextReader.GetAttribute("file"), xmlTextReader.GetAttribute("name"), xmlTextReader.GetAttribute("extensions")));
								continue;
							}
						}
						else
						{
							string attribute = xmlTextReader.GetAttribute("version");
							if (attribute != "1.0")
							{
								MessageBox.Show("Unknown syntax mode file defininition with version " + attribute, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
								return arrayList;
							}
							continue;
						}
					}
					MessageBox.Show("Unknown node in syntax mode file :" + xmlTextReader.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
					return arrayList;
				}
			}
			xmlTextReader.Close();
			return arrayList;
		}
		public override string ToString()
		{
			return string.Format("[SyntaxMode: FileName={0}, Name={1}, Extensions=({2})]", this.fileName, this.name, string.Join(",", this.extensions));
		}
	}
}
