using System;
namespace LTP.ConvertCS2VB
{
	public class EnumManager : BaseManager
	{
		private string EnumName = "";
		private string EnumReturnValue = "";
		private string EnumDeclarationToken = "";
		private string EnumBlock = "";
		public string GetBlock(string tcLine, string tcBlock)
		{
			base.GetBlankToken(tcLine);
			string text = ReplaceManager.GetSingledSpacedString(tcLine);
			int num = text.LastIndexOf(":");
			if (num > 0)
			{
				this.EnumReturnValue = "As " + text.Substring(num + 1).Trim();
				text = text.Substring(0, num).Trim();
			}
			text = ReplaceManager.HandleTypes(text);
			num = text.IndexOf(" ");
			this.EnumName = text.Substring(num + 1);
			this.EnumDeclarationToken = text.Substring(0, num).Trim();
			this.EnumBlock = base.ExtractBlock(tcBlock, "{", "}").Replace(",", "\r\n" + this.BlankToken + "\t");
			return this.Execute();
		}
		private string Execute()
		{
			string str = string.Concat(new string[]
			{
				this.BlankToken,
				this.EnumDeclarationToken,
				" ",
				this.EnumName,
				" ",
				this.EnumReturnValue,
				"\n"
			});
			str = str + this.EnumBlock + "\n";
			return str + this.BlankToken + "End Enum\n";
		}
	}
}
