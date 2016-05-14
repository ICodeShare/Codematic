using System;
using System.Text;
namespace LTP.ConvertCS2VB
{
	public class MethodBlockManager : BaseManager
	{
		private string MethodBlockToken = "";
		private string MethodModifiersToken = "";
		private string MethodNameToken = "";
		private string ParameterToken = "";
		private string MethodType = "Function";
		private string ReturnValueToken = "";
		private string MethodParentToken = "";
		public string GetMethodBlock(string tcMethodLine, string tcMethodBlock)
		{
			if (!this.ValidateBlock(tcMethodBlock))
			{
				return tcMethodBlock;
			}
			base.GetBlankToken(tcMethodLine);
			this.BuildMethodDeclaration(tcMethodLine);
			this.MethodBlockToken = base.ExtractBlock(tcMethodBlock, "{", "}");
			return this.Execute();
		}
		private bool ValidateBlock(string tcMethodBlock)
		{
			int num = tcMethodBlock.IndexOf(">>");
			if (num > 0)
			{
				int num2 = tcMethodBlock.IndexOf("{");
				if (num2 > num)
				{
					return false;
				}
				int num3 = tcMethodBlock.IndexOf(";");
				if (num3 > num)
				{
					return false;
				}
			}
			return tcMethodBlock.Trim().EndsWith("}");
		}
		private void BuildMethodDeclaration(string tcLine)
		{
			int num = tcLine.IndexOf(":");
			if (num > 0)
			{
				this.MethodParentToken = tcLine.Substring(num + 1);
				tcLine = tcLine.Substring(0, num);
			}
			tcLine = ReplaceManager.GetSingledSpacedString(tcLine).Trim();
			int num2 = 0;
			int num3 = 0;
			string tcString = "";
			string text = tcLine;
			int num4 = tcLine.Length;
			while (0 < num4)
			{
				int num5 = num4 - 1;
				if (tcLine[num5] == ')')
				{
					num3++;
				}
				if (tcLine[num5] == '(')
				{
					num2++;
				}
				if (num2 != 0 && num3 != 0 && num2 - num3 == 0)
				{
					text = tcLine.Substring(0, num5);
					tcString = tcLine.Substring(num5).Trim();
					break;
				}
				num4--;
			}
			this.ParameterToken = this.GetParameters(tcString);
			text = ReplaceManager.GetSingledSpacedString(text);
			string[] array = text.Split(new char[]
			{
				' '
			});
			if (array.Length == 2)
			{
				text = "private " + text;
			}
			if (text.IndexOf("void ") >= 0)
			{
				this.MethodType = "Sub";
			}
			text = " " + text.Trim() + " ";
			text = ReplaceManager.HandleModifiers(text);
			text = text.Trim();
			int num6 = text.LastIndexOf(" ");
			this.MethodNameToken = text.Substring(num6).Trim();
			text = text.Substring(0, num6);
			if (this.MethodType != "Sub")
			{
				num6 = text.LastIndexOf(" ");
				if (num6 >= 0)
				{
					this.ReturnValueToken = " As " + text.Substring(num6).Trim();
					text = text.Substring(0, num6);
				}
			}
			if (num6 > 0)
			{
				this.MethodModifiersToken = text.Substring(0, num6) + " ";
			}
		}
		private string GetParameters(string tcString)
		{
			string text = base.ExtractBlock(tcString, "(", ")");
			string result = "()";
			if (text.Length > 0)
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				FieldManager fieldManager = new FieldManager();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i].Trim();
					string str;
					if (text2.IndexOf("ref ") >= 0 || text2.IndexOf("out ") >= 0)
					{
						str = "ByRef";
						text2 = text2.Replace("ref ", "");
						text2 = text2.Replace("out ", "");
					}
					else
					{
						str = "ByVal";
					}
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					text2 = fieldManager.GetConvertedExpression(text2 + ";").Trim();
					text2 = str + text2.Replace("Dim", "");
					stringBuilder.Append(text2);
				}
				result = "(" + stringBuilder.ToString() + ")";
			}
			return result;
		}
		private string Execute()
		{
			string text = string.Concat(new string[]
			{
				this.BlankToken,
				this.MethodModifiersToken,
				this.MethodType,
				" ",
				this.MethodNameToken,
				this.ParameterToken,
				this.ReturnValueToken
			});
			if (this.MethodParentToken.Length > 0)
			{
				text = text + " : " + this.MethodParentToken;
			}
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				this.MethodBlockToken,
				"\r\n",
				this.BlankToken,
				"End ",
				this.MethodType,
				"\r\n"
			});
		}
	}
}
