using System;
using System.IO;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	public class ProConfig
	{
		public static ProSettings GetSettings()
		{
			ProSettings result = null;
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProSettings));
			try
			{
				string path = "ProConfig.config";
				FileStream fileStream = new FileStream(path, FileMode.Open);
				result = (ProSettings)xmlSerializer.Deserialize(fileStream);
				fileStream.Close();
			}
			catch
			{
				result = new ProSettings();
			}
			return result;
		}
		public static void SaveSettings(ProSettings data)
		{
			string path = "ProConfig.config";
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProSettings));
			FileStream fileStream = new FileStream(path, FileMode.Create);
			xmlSerializer.Serialize(fileStream, data);
			fileStream.Close();
		}
	}
}
