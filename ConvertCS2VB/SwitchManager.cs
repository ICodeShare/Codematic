using System;
namespace LTP.ConvertCS2VB
{
	public class SwitchManager : BaseManager
	{
		private string SwitchExpressionToken = "";
		private string SwitchBlock = "";
		public string GetBlock(string tcSwitchLine, string tcSwitchBlock)
		{
			base.GetBlankToken(tcSwitchLine);
			this.SwitchExpressionToken = base.ExtractBlock(tcSwitchLine, "(", ")");
			this.SwitchBlock = base.ExtractBlock(tcSwitchBlock, "{", "}").Replace("default", "case Else");
			return this.Execute();
		}
		private string Execute()
		{
			string str = this.BlankToken + "Select Case " + this.SwitchExpressionToken + "\n";
			str = str + this.SwitchBlock + "\n";
			return str + this.BlankToken + "End Select\n";
		}
	}
}
