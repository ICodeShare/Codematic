using System;
using System.Text;
namespace LTP.ConvertCS2VB
{
	public class PropertyManager : BaseManager
	{
		private string ReturnValueToken = "";
		private string PropertyModifiersToken = "";
		private string PropertyNameToken = "";
		private string GetBlockToken = "";
		private string SetBlockToken = "";
		private string PropertyParameterToken = "() ";
		public string GetBlock(string tcLine, string tcBlock)
		{
			base.GetBlankToken(tcLine);
			this.BuildPropertyDeclaration(tcLine);
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			string[] array = tcBlock.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.StartsWith("get"))
				{
					int num = 0;
					int num2 = 0;
					for (int j = i + 1; j < array.Length; j++)
					{
						stringBuilder.Append(array[j] + "\n");
						if (array[j].Trim().StartsWith("{"))
						{
							num++;
						}
						if (array[j].Trim().StartsWith("}"))
						{
							num2++;
						}
						if (num != 0 && num2 != 0 && num - num2 == 0)
						{
							break;
						}
					}
					break;
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				string text2 = array[k].Trim();
				if (text2.StartsWith("set"))
				{
					int num3 = 0;
					int num4 = 0;
					for (int l = k + 1; l < array.Length; l++)
					{
						stringBuilder2.Append(array[l] + "\n");
						if (array[l].Trim().StartsWith("{"))
						{
							num3++;
						}
						if (array[l].Trim().StartsWith("}"))
						{
							num4++;
						}
						if (num3 != 0 && num4 != 0 && num3 - num4 == 0)
						{
							break;
						}
					}
					break;
				}
			}
			this.GetBlockToken = base.ExtractBlock(stringBuilder.ToString(), "{", "}");
			this.SetBlockToken = base.ExtractBlock(stringBuilder2.ToString(), "{", "}");
			return this.Execute();
		}
		private void BuildPropertyDeclaration(string tcLine)
		{
			tcLine = ReplaceManager.GetSingledSpacedString(tcLine).Trim();
			if (tcLine.EndsWith(")"))
			{
				int length = tcLine.IndexOf("(");
				this.PropertyParameterToken = base.ExtractBlock(tcLine, "(", ")");
				FieldManager fieldManager = new FieldManager();
				this.PropertyParameterToken = "(" + fieldManager.GetConvertedExpression(this.PropertyParameterToken) + ") ";
				this.PropertyParameterToken = this.PropertyParameterToken.Replace("Dim ", "");
				tcLine = tcLine.Substring(0, length).Trim();
			}
			int num = tcLine.LastIndexOf(" ");
			this.PropertyNameToken = tcLine.Substring(num + 1);
			string text = tcLine.Substring(0, num).Trim();
			text = ReplaceManager.HandleModifiers(text);
			text = text.Trim();
			num = text.LastIndexOf(" ");
			this.ReturnValueToken = "As " + text.Substring(num).Trim();
			text = text.Substring(0, num);
			this.PropertyModifiersToken = text;
		}
		private string Execute()
		{
			if (this.GetBlockToken.Trim().Length > 0 && this.SetBlockToken.Trim().Length == 0)
			{
				this.PropertyModifiersToken += " ReadOnly";
			}
			else
			{
				if (this.SetBlockToken.Trim().Length > 0 && this.GetBlockToken.Trim().Length == 0)
				{
					this.PropertyModifiersToken += " WriteOnly";
				}
			}
			string text = string.Concat(new string[]
			{
				this.BlankToken,
				this.PropertyModifiersToken,
				" Property ",
				this.PropertyNameToken,
				this.PropertyParameterToken,
				this.ReturnValueToken,
				"\n"
			});
			if (this.GetBlockToken.Trim().Length > 0)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					this.BlankToken,
					"\tGet \n",
					this.GetBlockToken,
					"\n",
					this.BlankToken,
					"\tEnd Get\n"
				});
			}
			if (this.SetBlockToken.Trim().Length > 0)
			{
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					this.BlankToken,
					"\tSet (ByVal Value ",
					this.ReturnValueToken,
					") \n",
					this.SetBlockToken,
					"\n",
					this.BlankToken,
					"\tEnd Set\n"
				});
			}
			return text + this.BlankToken + "End Property\n";
		}
	}
}
