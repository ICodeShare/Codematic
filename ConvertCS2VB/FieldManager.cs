using System;
using System.Collections;
namespace LTP.ConvertCS2VB
{
	public class FieldManager : BaseManager
	{
		private string ModifierToken = "";
		private string VariableToken = "";
		private string AssignmentToken = "";
		private string DataTypeToken = "";
		private ArrayList Stack = new ArrayList();
		private int StackCount = -1;
		private ArrayList CommaSeperatedDeclarations = new ArrayList();
		private Queue ModifersQueue = new Queue();
		protected string CheckTokens(string tcLine, char tcStart, char tcEnd)
		{
			for (int i = 0; i < tcLine.Length; i++)
			{
				if (tcLine[i] == tcStart)
				{
					int num = 1;
					int num2 = 0;
					for (int j = i + 1; j < tcLine.Length; j++)
					{
						if (tcLine[j] == tcStart)
						{
							num++;
						}
						if (tcLine[j] == tcEnd)
						{
							num2++;
						}
						if (num != 0 && num2 != 0 && num - num2 == 0)
						{
							MappingToken mappingToken = default(MappingToken);
							this.StackCount++;
							mappingToken.cOriginal = tcLine.Substring(i, j - i + 1);
							mappingToken.cToken = string.Concat(new string[]
							{
								tcStart.ToString(),
								"__",
								this.StackCount.ToString(),
								"__",
								tcEnd.ToString()
							});
							this.Stack.Add(mappingToken);
							tcLine = tcLine.Substring(0, i) + mappingToken.cToken + tcLine.Substring(j + 1);
							break;
						}
					}
				}
			}
			return tcLine;
		}
		private string PopTokens(string tcText)
		{
			if (this.StackCount == -1)
			{
				return tcText;
			}
			foreach (MappingToken mappingToken in this.Stack)
			{
				tcText = tcText.Replace(mappingToken.cToken, mappingToken.cOriginal);
			}
			return tcText;
		}
		private void ExtractModifiers(ref string tcLine)
		{
			tcLine = tcLine.Trim();
			for (int i = 0; i < ReplaceManager.Modifiers.Length; i++)
			{
				if (tcLine.StartsWith(ReplaceManager.Modifiers[i]))
				{
					this.ModifersQueue.Enqueue(ReplaceManager.Modifiers[i]);
					tcLine = tcLine.Substring(ReplaceManager.Modifiers[i].Length + 1);
					i = 0;
				}
			}
			this.UpdateModifierToken();
		}
		private void UpdateModifierToken()
		{
			IEnumerator enumerator = this.ModifersQueue.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.ModifierToken += ReplaceManager.HandleModifiers((string)enumerator.Current + " ");
			}
		}
		public string GetConvertedExpression(string tcLine)
		{
			base.GetBlankToken(tcLine);
			this.ExtractModifiers(ref tcLine);
			tcLine = this.BlankToken + ReplaceManager.GetSingledSpacedString(tcLine);
			tcLine = tcLine.Replace("==", "=");
			tcLine = tcLine.Replace("=", " = ");
			tcLine = this.CheckTokens(tcLine, '(', ')');
			tcLine = this.CheckTokens(tcLine, '[', ']');
			if (this.valid(tcLine))
			{
				return this.PopTokens(tcLine);
			}
			if (tcLine.Trim().IndexOf(' ') < 0)
			{
				return this.PopTokens(tcLine);
			}
			if (this.ModifierToken.Trim().Length == 0)
			{
				this.ModifierToken = "Dim ";
			}
			this.GetDataType(tcLine);
			return this.PopTokens(this.Build());
		}
		private bool valid(string tcLine)
		{
			bool result = false;
			if (tcLine.Trim().StartsWith("return"))
			{
				result = true;
			}
			if (tcLine.Trim().ToLower().StartsWith("while"))
			{
				result = true;
			}
			if (tcLine.Trim().ToLower().StartsWith("do"))
			{
				result = ReplaceManager.IsNextCharValid(tcLine, "do");
			}
			if (tcLine.Trim().ToLower().StartsWith("loop"))
			{
				result = true;
			}
			return result;
		}
		private void GetDataType(string tcLine)
		{
			string text = tcLine.Trim();
			int num = text.IndexOf(' ');
			this.DataTypeToken = text.Substring(0, num);
			int num2 = text.IndexOf(",");
			if (num2 >= 0)
			{
				text = text.Substring(0, num2);
				string[] array = tcLine.Split(new char[]
				{
					','
				});
				for (int i = 1; i < array.Length; i++)
				{
					this.CommaSeperatedDeclarations.Add(this.BlankToken + this.DataTypeToken + " " + array[i].Trim());
				}
			}
			int num3 = text.IndexOf(' ', num + 1);
			if (num3 < 0)
			{
				text = text.Replace(';', ' ');
				this.VariableToken = text.Substring(num + 1).Trim();
				return;
			}
			this.VariableToken = text.Substring(num + 1, num3 - num - 1);
			this.GetExpressionToken(text);
		}
		private void GetExpressionToken(string tcLine)
		{
			int num = tcLine.IndexOf('=');
			if (num > 0)
			{
				this.AssignmentToken = " " + tcLine.Substring(num).Trim();
			}
			else
			{
				this.AssignmentToken = "";
			}
			if (this.DataTypeToken.IndexOf('[') > 0)
			{
				int num2 = tcLine.IndexOf(";");
				if (num2 == -1)
				{
					this.AssignmentToken += " _ 'CSharp2VBArray";
				}
			}
			this.AssignmentToken = this.AssignmentToken.Replace(';', ' ');
		}
		private string Build()
		{
			int num = this.DataTypeToken.IndexOf('[');
			if (num > 0)
			{
				this.VariableToken += this.DataTypeToken.Substring(num);
				this.DataTypeToken = this.DataTypeToken.Substring(0, num);
			}
			string text = "";
			text = text + this.BlankToken + this.ModifierToken;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this.VariableToken,
				" As ",
				this.DataTypeToken,
				this.AssignmentToken,
				";"
			});
			foreach (string tcLine in this.CommaSeperatedDeclarations)
			{
				text += new FieldManager
				{
					ModifierToken = ","
				}.GetConvertedExpression(tcLine).Trim();
			}
			return text;
		}
	}
}
