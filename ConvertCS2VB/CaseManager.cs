using System;
namespace LTP.ConvertCS2VB
{
	public class CaseManager : BaseManager
	{
		private string CaseExpressionToken = "";
		private string CaseBlock = "";
		public string GetBlock(string tcCaseLine, string tcCaseBlock)
		{
			base.GetBlankToken(tcCaseLine);
			this.CaseExpressionToken = ReplaceManager.HandleExpression(base.ExtractBlock(tcCaseLine, "case", ":"));
			if (tcCaseBlock.IndexOf("{") >= 0)
			{
				this.CaseBlock = base.ExtractBlock(tcCaseBlock, "{", "}");
			}
			else
			{
				this.CaseBlock = base.ExtractBlock(tcCaseBlock, ":", ";");
			}
			return this.Execute();
		}
		private string Execute()
		{
			string str = this.BlankToken + "Case " + this.CaseExpressionToken + "\n";
			return str + this.CaseBlock + "\n";
		}
	}
}
