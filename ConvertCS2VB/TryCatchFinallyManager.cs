using System;
namespace LTP.ConvertCS2VB
{
	public class TryCatchFinallyManager : BaseManager
	{
		private string TryBlockToken = "";
		private string CatchBlockToken = "";
		private string FinallyBlockToken = "";
		private string CatchToken = "";
		public string GetBlock(string tcTryBlock, string tcCatchBlock, string tcFinallyBlock)
		{
			base.GetBlankToken(tcTryBlock);
			if (tcTryBlock.Trim().StartsWith("try"))
			{
				int num = tcTryBlock.IndexOf("try");
				tcTryBlock = tcTryBlock.Substring(num + "try".Length);
			}
			this.TryBlockToken = base.GetCurrentBlock(tcTryBlock);
			if (this.TryBlockToken.Length > 0)
			{
				this.TryBlockToken = "Try\r\n" + this.TryBlockToken;
			}
			this.CatchBlockToken = tcCatchBlock;
			if (tcFinallyBlock.Trim().StartsWith("finally"))
			{
				int num2 = tcFinallyBlock.IndexOf("finally");
				tcFinallyBlock = tcFinallyBlock.Substring(num2 + "finally".Length);
			}
			this.FinallyBlockToken = base.GetCurrentBlock(tcFinallyBlock);
			if (this.FinallyBlockToken.Trim().Length > 0)
			{
				this.FinallyBlockToken = "Finally\r\n" + this.FinallyBlockToken;
			}
			return this.Build();
		}
		private void GetCatchToken(string tcCatchToken)
		{
			string text = ReplaceManager.GetSingledSpacedString(tcCatchToken).Trim();
			int num = text.IndexOf(" ");
			if (num > 0)
			{
				FieldManager fieldManager = new FieldManager();
				this.CatchToken = "Catch " + fieldManager.GetConvertedExpression(text.Substring(num) + ";");
				return;
			}
			this.CatchToken = "Catch";
		}
		private string Build()
		{
			string str = this.BlankToken + this.TryBlockToken;
			if (this.CatchBlockToken.Length > 0)
			{
				str = str + "\r\n" + this.BlankToken + this.CatchBlockToken;
			}
			if (this.FinallyBlockToken.Length > 0)
			{
				str = str + "\r\n" + this.BlankToken + this.FinallyBlockToken;
			}
			return str + "\r\n" + this.BlankToken + "End Try";
		}
	}
}
