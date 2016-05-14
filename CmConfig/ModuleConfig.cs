using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	public class ModuleConfig
	{
		public static ModuleSettings GetSettings()
		{
			ModuleSettings result = null;
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModuleSettings));
			try
			{
				string startupPath = Application.StartupPath;
				string path = startupPath + "\\config.xml";
				FileStream fileStream = new FileStream(path, FileMode.Open);
				result = (ModuleSettings)xmlSerializer.Deserialize(fileStream);
				fileStream.Close();
			}
			catch
			{
				result = new ModuleSettings();
			}
			return result;
		}
		public static void SaveSettings(ModuleSettings data)
		{
			string startupPath = Application.StartupPath;
			string path = startupPath + "\\config.xml";
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModuleSettings));
			FileStream fileStream = new FileStream(path, FileMode.Create);
			xmlSerializer.Serialize(fileStream, data);
			fileStream.Close();
		}
	}
}
