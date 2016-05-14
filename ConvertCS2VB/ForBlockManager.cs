using System;
namespace LTP.ConvertCS2VB
{
	public class ForBlockManager : BaseManager
	{
		private string DeclarationToken = "";
		private string BeginConditionToken = "";
		private string EndConditionToken = "";
		private string IncrementToken = "";
		private string ForBlockToken = "";
		private string WhileBlockToken = "";
		private object oParent;
		private string Execute()
		{
			string text = "";
			text += this.DeclarationToken;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this.BlankToken,
				"For ",
				this.BeginConditionToken,
				" To ",
				this.EndConditionToken,
				this.IncrementToken,
				"\n"
			});
			text += this.ForBlockToken;
			text = text + this.BlankToken + "Next";
			string text3 = text;
			text3 = text3.Replace("\n", "<newline/>");
			text3 = text3.Replace("\r", "<carriagereturn/>");
			return text;
		}
		public string GetForBlock(object toSender, string tcForCondition, string tcForBlock)
		{
			this.oParent = toSender;
			base.GetBlankToken(tcForBlock);
			this.ForBlockToken = base.GetCurrentBlock(tcForBlock);
			this.GetExpressions(tcForCondition);
			if (this.WhileBlockToken.Trim().Length == 0)
			{
				return this.Execute();
			}
			return this.WhileBlockToken;
		}
		private void GetExpressions(string tcExp)
		{
			int num = tcExp.IndexOf("(");
			string text = tcExp.Substring(num + 1);
			num = text.LastIndexOf(")");
			text = text.Substring(0, num).Trim();
			string[] array = text.Split(new char[]
			{
				';'
			});
			string text2 = array[0];
			if (text2.Trim().Length == 0)
			{
				if (array[1].Trim() == "")
				{
					array[1] = "true";
				}
				string text3 = this.BlankToken + "while(" + array[1] + ")";
				string str = string.Concat(new string[]
				{
					this.BlankToken,
					"{\n",
					this.ForBlockToken,
					"\n",
					this.BlankToken,
					"\t",
					array[2],
					"\n",
					this.BlankToken,
					"}"
				});
				WhileManager whileManager = new WhileManager();
				this.WhileBlockToken = whileManager.GetBlock(this.oParent, text3, text3 + str);
				return;
			}
			text2 = text2.Replace("=", " = ");
			text2 = text2.Replace("  ", " ");
			string[] array2 = text2.Split(null);
			if (!array2[1].StartsWith("="))
			{
				this.DeclarationToken = string.Concat(new string[]
				{
					"\n",
					this.BlankToken,
					"Dim ",
					array2[1],
					" As ",
					array2[0]
				});
				int startIndex = text2.IndexOf(" ");
				text2 = text2.Substring(startIndex);
			}
			this.BeginConditionToken = text2;
			string text4 = array[1];
			num = text4.IndexOf(">");
			if (num > 0)
			{
				this.EndConditionToken = text4.Substring(num + 1);
			}
			else
			{
				num = text4.IndexOf("<");
				if (num > 0)
				{
					this.EndConditionToken = text4.Substring(num + 1);
				}
				else
				{
					this.EndConditionToken = text4.Trim();
				}
			}
			if (text4.IndexOf("=") == -1)
			{
				this.EndConditionToken += "- 1 ";
			}
			this.EndConditionToken = this.EndConditionToken.Replace("=", "");
			this.EndConditionToken = this.EndConditionToken.Replace("!", "");
			string text5 = array[2];
			if (text5.IndexOf("++") > 0)
			{
				num = text5.IndexOf("+");
				text5 = " Step " + text5.Substring(0, num) + " + 1";
			}
			else
			{
				if (text5.IndexOf("--") > 0)
				{
					num = text5.IndexOf("-");
					text5 = " Step " + text5.Substring(0, num) + " - 1";
				}
				else
				{
					text5 = " Step " + text5.Trim();
				}
			}
			this.IncrementToken = text5;
		}
	}
}
