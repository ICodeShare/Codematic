using System;
namespace LTP.ConvertCS2VB
{
	public class WhileManager : BaseManager
	{
		private string ExpresionToken = "";
		private string WhileBlockToken = "";
		private object oParent;
		public string GetBlock(object toSender, string tcWhileCondition, string tcWhileBlock)
		{
			this.oParent = toSender;
			base.GetBlankToken(tcWhileCondition);
			this.GetCondition(tcWhileCondition);
			tcWhileBlock = tcWhileBlock.Substring(tcWhileCondition.Length);
			this.WhileBlockToken = base.GetCurrentBlock(tcWhileBlock);
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
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this.BlankToken,
				"While ",
				this.ExpresionToken,
				"\n"
			});
			text += this.WhileBlockToken;
			return text + "\n" + this.BlankToken + "End While";
		}
	}
}
