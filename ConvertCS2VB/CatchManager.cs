using System;
namespace LTP.ConvertCS2VB
{
	public class CatchManager : BaseManager
	{
		private string CatchBlockToken = "";
		private string CatchToken = "";
		public string GetBlock(string tcCatchLine, string tcCatchBlock)
		{
			base.GetBlankToken(tcCatchLine);
			this.GetCatchToken(tcCatchLine);
			tcCatchBlock = tcCatchBlock.Substring(tcCatchLine.Length);
			this.CatchBlockToken = base.GetCurrentBlock(tcCatchBlock);
			if (this.CatchBlockToken.Length > 0)
			{
				this.CatchBlockToken = this.CatchToken + "\r\n" + this.CatchBlockToken;
			}
			return this.Execute();
		}
		private void GetCatchToken(string tcCatchToken)
		{
			string text = base.ExtractBlock(ReplaceManager.GetSingledSpacedString(tcCatchToken).Trim(), "(", ")").Trim();
			int num = text.IndexOf(" ");
			if (num > 0)
			{
				FieldManager fieldManager = new FieldManager();
				this.CatchToken = fieldManager.GetConvertedExpression(text + ";").Replace("Dim ", "Catch ");
				return;
			}
			this.CatchToken = "Catch";
		}
		private string Execute()
		{
			string text = "";
			if (this.CatchBlockToken.Length > 0)
			{
				text = text + "\r\n" + this.BlankToken + this.CatchBlockToken;
			}
			return text;
		}
	}
}
