using System;
using System.Collections;
using System.Text;
namespace LTP.ConvertCS2VB
{
	public class IfBlockManager : BaseManager
	{
		private string IfExpressionToken = "";
		private string IfBlockToken = "";
		private ArrayList ElseBlocks = new ArrayList();
		private object oParent;
		public string GetIfBlock(object toSender, string tcIfBlock, string tcIfLine, ArrayList taElseBlocks)
		{
			this.oParent = toSender;
			base.GetBlankToken(tcIfLine);
			if (tcIfLine.TrimEnd(new char[0]).EndsWith(";"))
			{
				int num = tcIfBlock.IndexOf("(");
				int num2 = 0;
				int num3 = 0;
				for (int i = num; i < tcIfBlock.Length; i++)
				{
					if (tcIfBlock[i] == '(')
					{
						num2++;
					}
					if (tcIfBlock[i] == ')')
					{
						num3++;
					}
					if (num2 == num3)
					{
						num = i;
						tcIfLine = tcIfLine.Substring(0, num + 1);
						break;
					}
				}
				tcIfBlock = string.Concat(new string[]
				{
					"{",
					this.BlankToken,
					"\t",
					tcIfBlock.Substring(num + 1),
					"}"
				});
			}
			this.IfExpressionToken = this.GetExpression(tcIfLine);
			this.IfBlockToken = this.GetBlock(tcIfBlock);
			this.HandleElseBlocks(taElseBlocks);
			return this.Execute();
		}
		private void HandleElseBlocks(ArrayList taElseBlocks)
		{
			foreach (ElseBlockToken elseBlockToken in taElseBlocks)
			{
				ElseBlockToken elseBlockToken2 = default(ElseBlockToken);
				string expression = this.GetExpression(elseBlockToken.ElseLine);
				elseBlockToken2.ElseBlock = this.GetBlock(elseBlockToken.ElseBlock);
				if (expression.Trim() == "else")
				{
					elseBlockToken2.ElseLine = "";
				}
				else
				{
					elseBlockToken2.ElseLine = expression;
				}
				this.ElseBlocks.Add(elseBlockToken2);
			}
		}
		private string GetBlock(string tcBlock)
		{
			if (tcBlock.IndexOf("{") >= 0 && tcBlock.IndexOf("}") >= 0)
			{
				return base.ExtractBlock(tcBlock, "{", "}");
			}
			string[] array = tcBlock.Trim().Split(new char[]
			{
				'\r'
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.Append(array[i]);
			}
			return stringBuilder.ToString();
		}
		private string GetExpression(string tcLine)
		{
			tcLine = ((CSharpToVBConverter)this.oParent).HandleCasting(tcLine);
			return ReplaceManager.HandleExpression(base.ExtractBlock(tcLine, "(", ")"));
		}
		private string Execute()
		{
			string text = "";
			text = this.BlankToken + "If " + this.IfExpressionToken + " Then\n";
			text = text + this.IfBlockToken + "\n";
			foreach (ElseBlockToken elseBlockToken in this.ElseBlocks)
			{
				text = text + this.BlankToken + "Else ";
				if (elseBlockToken.ElseLine.Trim().Length > 0)
				{
					text = text + "If " + elseBlockToken.ElseLine + " Then \n";
				}
				else
				{
					text += "\n";
				}
				text = text + elseBlockToken.ElseBlock + "\n";
			}
			text = text + this.BlankToken + "End If\n";
			return text;
		}
	}
}
