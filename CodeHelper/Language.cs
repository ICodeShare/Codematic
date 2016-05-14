using Maticsoft.CmConfig;
using System;
using System.Collections;
using System.Xml;
namespace Maticsoft.CodeHelper
{
	public static class Language
	{
		public static Hashtable LoadFromCfg(string filename)
		{
			Hashtable result;
			try
			{
				string language = AppConfig.GetSettings().Language;
				string filename2 = string.Concat(new string[]
				{
					AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[]
					{
						'\\'
					}),
					"\\language\\",
					language,
					"\\",
					filename
				});
				Hashtable hashtable = new Hashtable();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(filename2);
				XmlElement documentElement = xmlDocument.DocumentElement;
				foreach (XmlNode xmlNode in documentElement.ChildNodes)
				{
					hashtable.Add(xmlNode.Attributes["key"].Value, xmlNode.Attributes["value"].Value);
				}
				result = hashtable;
			}
			catch (Exception ex)
			{
				throw new Exception("Load language file fail:" + ex.Message);
			}
			return result;
		}
	}
}
