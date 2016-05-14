using System;
using System.IO;
using System.Text;
using System.Xml;
namespace Maticsoft.Utility
{
	public class VSProject
	{
		public void AddClass(string filename, string classname)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			string name = xmlDocument.DocumentElement.FirstChild.Name;
			string a;
			if ((a = name) != null)
			{
				if (a == "CSHARP")
				{
					this.AddClass2003(filename, classname);
					return;
				}
				if (!(a == "PropertyGroup"))
				{
					return;
				}
				this.AddClass2005(filename, classname);
			}
		}
		public void AddClass2003(string filename, string classname)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			foreach (XmlElement xmlElement in xmlDocument.DocumentElement.ChildNodes)
			{
				foreach (XmlElement xmlElement2 in xmlElement)
				{
					if (xmlElement2.Name == "Files")
					{
						foreach (XmlElement xmlElement3 in xmlElement2)
						{
							if (xmlElement3.Name == "Include")
							{
								XmlElement xmlElement4 = xmlDocument.CreateElement("File", xmlDocument.DocumentElement.NamespaceURI);
								xmlElement4.SetAttribute("RelPath", classname);
								xmlElement4.SetAttribute("SubType", "Code");
								xmlElement4.SetAttribute("BuildAction", "Compile");
								xmlElement3.AppendChild(xmlElement4);
								break;
							}
						}
					}
				}
			}
			xmlDocument.Save(filename);
		}
		public void AddClass2005(string filename, string classname)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			foreach (XmlElement xmlElement in xmlDocument.DocumentElement.ChildNodes)
			{
				if (xmlElement.Name == "ItemGroup")
				{
					string arg_51_0 = xmlElement.ChildNodes[0].InnerText;
					string name = xmlElement.ChildNodes[0].Name;
					if (name == "Compile")
					{
						XmlElement xmlElement2 = xmlDocument.CreateElement("Compile", xmlDocument.DocumentElement.NamespaceURI);
						xmlElement2.SetAttribute("Include", classname);
						xmlElement.AppendChild(xmlElement2);
						break;
					}
				}
			}
			xmlDocument.Save(filename);
		}
		public void AddClass2008(string filename, string classname)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			foreach (XmlElement xmlElement in xmlDocument.DocumentElement.ChildNodes)
			{
				if (xmlElement.Name == "ItemGroup")
				{
					string arg_51_0 = xmlElement.ChildNodes[0].InnerText;
					string name = xmlElement.ChildNodes[0].Name;
					if (name == "Compile")
					{
						XmlElement xmlElement2 = xmlDocument.CreateElement("Compile", xmlDocument.DocumentElement.NamespaceURI);
						xmlElement2.SetAttribute("Include", classname);
						xmlElement.AppendChild(xmlElement2);
						break;
					}
				}
			}
			xmlDocument.Save(filename);
		}
		public void AddClass2005Aspx(string filename, string aspxname)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			foreach (XmlElement xmlElement in xmlDocument.DocumentElement.ChildNodes)
			{
				if (xmlElement.Name == "ItemGroup")
				{
					string arg_51_0 = xmlElement.ChildNodes[0].InnerText;
					string name = xmlElement.ChildNodes[0].Name;
					if (name == "Compile")
					{
						XmlElement xmlElement2 = xmlDocument.CreateElement("Compile", xmlDocument.DocumentElement.NamespaceURI);
						xmlElement2.SetAttribute("Include", aspxname);
						xmlElement.AppendChild(xmlElement2);
						break;
					}
				}
			}
			xmlDocument.Save(filename);
		}
		public void AddMethodToClass(string ClassFile, string strContent)
		{
			if (File.Exists(ClassFile))
			{
				string text = File.ReadAllText(ClassFile, Encoding.Default);
				if (text.IndexOf(" class ") > 0)
				{
					int num = text.LastIndexOf("}");
					string text2 = text.Substring(0, num - 1);
					int num2 = text2.LastIndexOf("}");
					string str = text.Substring(0, num2 - 1);
					string value = str + "\r\n" + strContent + "\r\n}\r\n}";
					StreamWriter streamWriter = new StreamWriter(ClassFile, false, Encoding.Default);
					streamWriter.Write(value);
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
		}
	}
}
