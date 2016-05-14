using System;
namespace LTP.ConvertCS2VB
{
	public class MathManager : BaseManager
	{
		private string ExpressionBlock = "";
		private string VariableBlock = "";
		private string DeclarationBlock = "";
		private string PaddingBlock = "";
		private void Initialize()
		{
			this.ExpressionBlock = "";
			this.VariableBlock = "";
			this.DeclarationBlock = "";
			this.PaddingBlock = "";
		}
		public string GetBlock(string tcLine)
		{
			this.Initialize();
			base.GetBlankToken(tcLine);
			tcLine = ReplaceManager.GetSingledSpacedString(tcLine);
			int num = tcLine.IndexOf("=");
			if (num > 0)
			{
				string tcLine2 = tcLine.Substring(0, num);
				FieldManager fieldManager = new FieldManager();
				this.DeclarationBlock = fieldManager.GetConvertedExpression(tcLine2).Replace(";", "") + " = ";
				tcLine = tcLine.Substring(num + 1).Trim();
			}
			int num2 = tcLine.IndexOf("++");
			if (num2 < 0)
			{
				num2 = tcLine.IndexOf("--");
				if (num2 < 0)
				{
					return tcLine;
				}
				this.ExpressionBlock = "- 1";
			}
			else
			{
				this.ExpressionBlock = "+ 1";
			}
			if (num2 != 0)
			{
				this.VariableBlock = tcLine.Substring(0, num2);
			}
			else
			{
				tcLine = tcLine.Replace(";", "").Trim();
				int num3 = tcLine.IndexOf(" ");
				if (num3 > 0)
				{
					this.PaddingBlock = tcLine.Substring(num3);
					tcLine = tcLine.Substring(0, num3);
				}
				this.VariableBlock = tcLine.Replace("++", "").Replace("--", "").Trim();
			}
			if (this.DeclarationBlock.Length == 0)
			{
				this.DeclarationBlock = this.VariableBlock + " = ";
			}
			return this.Execute();
		}
		private string Execute()
		{
			return string.Concat(new string[]
			{
				this.BlankToken,
				this.DeclarationBlock.Trim(),
				" ",
				this.VariableBlock.Trim(),
				" ",
				this.ExpressionBlock,
				this.PaddingBlock,
				"\r"
			});
		}
	}
}
