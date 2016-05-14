using System;
namespace LTP.ConvertCS2VB
{
	public class ForEachManager : BaseManager
	{
		private string DeclarationToken = "";
		private string ConditionToken = "";
		private string ForEachBlockToken = "";
		public string GetBlock(string tcForEachCondition, string tcForEachBlock)
		{
			base.GetBlankToken(tcForEachCondition);
			this.GetCondition(tcForEachCondition);
			int length = tcForEachCondition.Length;
			int num = tcForEachBlock.IndexOf("{", length);
			int num2 = tcForEachBlock.IndexOf(";", length);
			if (num2 < num || (num2 > 0 && num == -1))
			{
				this.ForEachBlockToken = tcForEachBlock.Substring(length + 1, num2 - length);
			}
			else
			{
				this.ForEachBlockToken = base.ExtractBlock(tcForEachBlock, "{", "}");
			}
			return this.Execute();
		}
		private void GetCondition(string tcLine)
		{
			string text = base.ExtractBlock(tcLine, "(", ")");
			text = ReplaceManager.GetSingledSpacedString(text);
			string[] array = text.Split(new char[]
			{
				' '
			});
			if (array.Length > 3)
			{
				FieldManager fieldManager = new FieldManager();
				this.DeclarationToken = this.BlankToken + fieldManager.GetConvertedExpression(array[0] + " " + array[1] + ";") + "\n";
				int startIndex = text.IndexOf(" ");
				this.ConditionToken = text.Substring(startIndex).Trim();
			}
			else
			{
				this.ConditionToken = text.Trim();
			}
			this.ConditionToken = this.ConditionToken.Replace(" in ", " In ");
		}
		private string Execute()
		{
			string text = "";
			text += this.DeclarationToken;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this.BlankToken,
				"For Each ",
				this.ConditionToken,
				"\n"
			});
			text += this.ForEachBlockToken;
			return text + "\n" + this.BlankToken + "Next";
		}
	}
}
