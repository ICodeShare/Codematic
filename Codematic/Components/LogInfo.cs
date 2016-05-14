using Maticsoft.Utility;
using System;
using System.IO;
using System.Windows.Forms;
namespace Codematic
{
	public static class LogInfo
	{
		private static string logfilename = Application.StartupPath + "\\logInfo.txt";
		private static string cmcfgfile = Application.StartupPath + "\\cmcfg.ini";
		public static void WriteLog(string loginfo)
		{
			try
			{
				if (File.Exists(LogInfo.cmcfgfile))
				{
					INIFile iNIFile = new INIFile(LogInfo.cmcfgfile);
					string text = iNIFile.IniReadValue("loginfo", "save");
					if (text.Trim() == "1")
					{
						StreamWriter streamWriter = new StreamWriter(LogInfo.logfilename, true);
						streamWriter.WriteLine(DateTime.Now.ToString() + ":" + loginfo);
						streamWriter.Close();
					}
				}
			}
			catch
			{
			}
		}
		public static void WriteLog(Exception ex)
		{
			try
			{
				if (File.Exists(LogInfo.cmcfgfile))
				{
					INIFile iNIFile = new INIFile(LogInfo.cmcfgfile);
					string text = iNIFile.IniReadValue("loginfo", "save");
					if (text.Trim() == "1")
					{
						StreamWriter streamWriter = new StreamWriter(LogInfo.logfilename, true);
						streamWriter.WriteLine(DateTime.Now.ToString() + ":");
						streamWriter.WriteLine("错误信息：" + ex.Message);
						streamWriter.WriteLine("Stack Trace:" + ex.StackTrace);
						streamWriter.WriteLine("Source: " + ex.Source);
						streamWriter.WriteLine("");
						streamWriter.Close();
					}
				}
			}
			catch
			{
			}
		}
	}
}
