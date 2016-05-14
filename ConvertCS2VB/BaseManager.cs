using System;
using System.Text;
namespace LTP.ConvertCS2VB
{
	public class BaseManager
	{
		public string BlankToken = "";
		protected string ExtractBlock(string tcBlock, string tcStartString, string tcEndString)
		{
			if (tcBlock.Trim().Length == 0)
			{
				return "";
			}
			int num = tcBlock.IndexOf(tcStartString);
			int num2 = tcBlock.LastIndexOf(tcEndString);
			if (num < 0 || num2 < 0)
			{
				return tcBlock;
			}
			return tcBlock.Substring(num + tcStartString.Length, num2 - num - tcStartString.Length).TrimEnd(new char[0]);
		}
		public void GetBlankToken(string tcLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < tcLine.Length && char.IsWhiteSpace(tcLine[num]))
			{
				stringBuilder.Append(tcLine[num]);
				num++;
			}
			this.BlankToken = stringBuilder.ToString();
		}
		protected string GetCurrentBlock(string tcBlock)
		{
			if (tcBlock.IndexOf("{") >= 0 && tcBlock.IndexOf("}") >= 0)
			{
				return this.ExtractBlock(tcBlock, "{", "}");
			}
			string[] array = tcBlock.Trim().Split(new char[]
			{
				'\n'
			});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.BlankToken);
			stringBuilder.Append("\t");
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i]);
			}
			return stringBuilder.ToString();
		}
	}
}
