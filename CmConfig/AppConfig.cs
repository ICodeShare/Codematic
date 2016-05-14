using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	public class AppConfig
	{
		public static AppSettings GetSettings()
		{
			AppSettings result = null;
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
			try
			{
				string startupPath = Application.StartupPath;
				string path = startupPath + "\\appconfig.config";
				FileStream fileStream = new FileStream(path, FileMode.Open);
				result = (AppSettings)xmlSerializer.Deserialize(fileStream);
				fileStream.Close();
			}
			catch
			{
				result = new AppSettings();
			}
			return result;
		}
		public static void SaveSettings(AppSettings data)
		{
			try
			{
				string startupPath = Application.StartupPath;
				string path = startupPath + "\\appconfig.config";
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));
				FileStream fileStream = new FileStream(path, FileMode.Create);
				xmlSerializer.Serialize(fileStream, data);
				fileStream.Close();
			}
			catch
			{
			}
		}
	}
}
