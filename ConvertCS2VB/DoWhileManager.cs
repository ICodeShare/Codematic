using System;
namespace LTP.ConvertCS2VB
{
	public class DoWhileManager : BaseManager
	{
		private string ExpresionToken = "";
		private string DoWhileBlockToken = "";
		private object oParent;
		public string GetBlock(object toSender, string tcDoWhileCondition, string tcDoWhileBlock)
		{
			this.oParent = toSender;
			base.GetBlankToken(tcDoWhileCondition);
			if (tcDoWhileBlock.LastIndexOf("{") == -1)
			{
				int num = tcDoWhileBlock.IndexOf("do");
				tcDoWhileBlock = tcDoWhileBlock.Substring(num + "do".Length);
				this.GetCondition(tcDoWhileBlock);
			}
			else
			{
				int num2 = tcDoWhileBlock.LastIndexOf("}");
				int num3 = tcDoWhileBlock.LastIndexOf(";");
				string tcLine = tcDoWhileBlock.Substring(num2 + 1, num3 - num2 - 1);
				this.GetCondition(tcLine);
				this.DoWhileBlockToken = base.ExtractBlock(tcDoWhileBlock, "{", "}");
			}
			return this.Execute();
		}
		private void GetCondition(string tcLine)
		{
			tcLine = ((CSharpToVBConverter)this.oParent).HandleCasting(tcLine);
			string text = base.ExtractBlock(tcLine, "(", ")");
			text = ReplaceManager.GetSingledSpacedString(text);
			this.ExpresionToken = ReplaceManager.HandleExpression(text);
		}
		private string Execute()
		{
			string text = "";
			text = text + this.BlankToken + "Do \n";
			text += this.DoWhileBlockToken;
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				"\n",
				this.BlankToken,
				"Loop While ",
				this.ExpresionToken
			});
		}
	}
}
