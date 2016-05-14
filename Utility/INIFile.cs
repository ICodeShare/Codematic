using System;
using System.Runtime.InteropServices;
using System.Text;
namespace Maticsoft.Utility
{
	public class INIFile
	{
		public string path;
		public INIFile(string INIPath)
		{
			this.path = INIPath;
		}
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);
		public void IniWriteValue(string Section, string Key, string Value)
		{
			INIFile.WritePrivateProfileString(Section, Key, Value, this.path);
		}
		public void ClearAllSection()
		{
			this.IniWriteValue(null, null, null);
		}
		public void ClearSection(string Section)
		{
			this.IniWriteValue(Section, null, null);
		}
		public string IniReadValue(string Section, string Key)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			INIFile.GetPrivateProfileString(Section, Key, "", stringBuilder, 255, this.path);
			return stringBuilder.ToString();
		}
		public byte[] IniReadValues(string section, string key)
		{
			byte[] array = new byte[255];
			INIFile.GetPrivateProfileString(section, key, "", array, 255, this.path);
			return array;
		}
		public string[] IniReadValues()
		{
			byte[] sectionByte = this.IniReadValues(null, null);
			return this.ByteToString(sectionByte);
		}
		private string[] ByteToString(byte[] sectionByte)
		{
			ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
			string @string = aSCIIEncoding.GetString(sectionByte);
			string arg_17_0 = @string;
			char[] separator = new char[1];
			return arg_17_0.Split(separator);
		}
		public string[] IniReadValues(string Section)
		{
			byte[] sectionByte = this.IniReadValues(Section, null);
			return this.ByteToString(sectionByte);
		}
	}
}
