using System;
using System.Collections;
using System.Xml;
namespace Maticsoft.CmConfig
{
	public static class DatatypeMap
	{
		public static Hashtable LoadFromCfg(XmlDocument doc, string TypeName)
		{
			Hashtable result;
			try
			{
				Hashtable hashtable = new Hashtable();
				XmlNode xmlNode = doc.SelectSingleNode("Map/" + TypeName);
				if (xmlNode != null)
				{
					foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
					{
						hashtable.Add(xmlNode2.Attributes["key"].Value, xmlNode2.Attributes["value"].Value);
					}
				}
				result = hashtable;
			}
			catch (Exception ex)
			{
				throw new Exception("Load DatatypeMap file fail:" + ex.Message);
			}
			return result;
		}
		public static Hashtable LoadFromCfg(string filename, string xpathTypeName)
		{
			Hashtable result;
			try
			{
				Hashtable hashtable = new Hashtable();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(filename);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("Map/" + xpathTypeName);
				if (xmlNode != null)
				{
					foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
					{
						hashtable.Add(xmlNode2.Attributes["key"].Value, xmlNode2.Attributes["value"].Value);
					}
				}
				result = hashtable;
			}
			catch (Exception ex)
			{
				throw new Exception("Load DatatypeMap file fail:" + ex.Message);
			}
			return result;
		}
		public static string GetValueFromCfg(string filename, string xpathTypeName, string Key)
		{
			string result;
			try
			{
				Hashtable hashtable = new Hashtable();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(filename);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("Map/" + xpathTypeName);
				if (xmlNode != null)
				{
					foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
					{
						hashtable.Add(xmlNode2.Attributes["key"].Value, xmlNode2.Attributes["value"].Value);
					}
				}
				object obj = hashtable[Key];
				if (obj != null)
				{
					result = obj.ToString();
				}
				else
				{
					result = "";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Load DatatypeMap file fail:" + ex.Message);
			}
			return result;
		}
		public static bool SaveCfg(string filename, string NodeText, Hashtable list)
		{
			bool result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode newChild = xmlDocument.CreateNode(XmlNodeType.XmlDeclaration, "", "");
				xmlDocument.AppendChild(newChild);
				XmlElement xmlElement = xmlDocument.CreateElement("", NodeText, "");
				xmlDocument.AppendChild(xmlElement);
				foreach (DictionaryEntry dictionaryEntry in list)
				{
					XmlElement xmlElement2 = xmlDocument.CreateElement("", NodeText, "");
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("key");
					xmlAttribute.Value = dictionaryEntry.Key.ToString();
					xmlElement2.Attributes.Append(xmlAttribute);
					XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("value");
					xmlAttribute2.Value = dictionaryEntry.Value.ToString();
					xmlElement2.Attributes.Append(xmlAttribute2);
					xmlElement.AppendChild(xmlElement2);
				}
				xmlDocument.Save(filename);
				result = true;
			}
			catch (Exception ex)
			{
				throw new Exception("Save DatatypeMap file fail:" + ex.Message);
			}
			return result;
		}
	}
}
