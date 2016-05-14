using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
namespace LTP.ConvertCS2VB
{
	public class CSharpToVBConverter
	{
		private string[] Modifiers;
		private string[] DataTypes;
		private string[] MethodModifiers = new string[]
		{
			"new",
			"public",
			"protected",
			"internal",
			"private",
			"static",
			"virtual",
			"abstract",
			"override",
			"sealed",
			"extern",
			"void"
		};
		private ArrayList Stack = new ArrayList();
		private static int nStackCounter = -1;
		private string TokenStarter = "<__";
		private string TokenEnder = "__>";
		private string BlankLineToken = "<__0__>";
		private string EndOfAssignedArray = " _ 'CSharp2VBArray";
		public CSharpToVBConverter()
		{
			this.Modifiers = new string[7];
			this.Modifiers[0] = "internal";
			this.Modifiers[1] = "new";
			this.Modifiers[2] = "private";
			this.Modifiers[3] = "protected";
			this.Modifiers[4] = "public";
			this.Modifiers[5] = "readonly";
			this.Modifiers[6] = "static";
			this.DataTypes = new string[15];
			this.DataTypes[0] = "sbyte";
			this.DataTypes[1] = "short";
			this.DataTypes[2] = "int";
			this.DataTypes[3] = "long";
			this.DataTypes[4] = "byte";
			this.DataTypes[5] = "ushort";
			this.DataTypes[6] = "unit";
			this.DataTypes[7] = "ulong";
			this.DataTypes[8] = "float";
			this.DataTypes[9] = "double";
			this.DataTypes[10] = "decimal";
			this.DataTypes[11] = "bool";
			this.DataTypes[12] = "char";
			this.DataTypes[13] = "object";
			this.DataTypes[14] = "string";
		}
		public string Execute(string tcText)
		{
			string str = "";
			CSharpToVBConverter.nStackCounter = -1;
			tcText = this.FixLines(this.HandleBlankLines(tcText));
			tcText = this.FixLines(this.PushQuotesToStack(tcText, "\""));
			tcText = this.FixLines(this.PushQuotesToStack(tcText, "'"));
			tcText = this.FixLines(this.PushCommentsToStack(tcText));
			tcText = this.FixLines(this.HandleLineBreakDown(tcText));
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleMultiLineComments(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Multi-Line comments \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleAttributes(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Attributes \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "foreach", 0));
			}
			catch
			{
				tcText = "'Error: Converting For Each-Loops \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "do", 0));
			}
			catch
			{
				tcText = "'Error: Converting Do-While Loops \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "while", 0));
			}
			catch
			{
				tcText = "'Error: Converting While-Loops \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleForBlocks(tcText));
			}
			catch
			{
				tcText = "'Error: Converting For-Loops \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleTryCatchFinally(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Try-Catch-Finally Blocks \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "catch", 1));
			}
			catch
			{
				tcText = "'Error: Converting Catch bolcks in Try-Catch-Finally \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleIfBlock(tcText));
			}
			catch
			{
				tcText = "'Error: Converting If-Else-End If Blocks \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "switch", 0));
			}
			catch
			{
				tcText = "'Error: Converting Switch Blocks \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "struct ", 1));
			}
			catch
			{
				tcText = "'Error: Converting Structures \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "class ", 1));
			}
			catch
			{
				tcText = "'Error: Converting Classes \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "interface ", 1));
			}
			catch
			{
				tcText = "'Error: Converting Interfaces \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "enum", 1));
			}
			catch
			{
				tcText = "'Error: Converting Enumerators \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "namespace", 0));
			}
			catch
			{
				tcText = "'Error: Converting Namespaces \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleMathOperations(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Math operations Object++ to Object = Object + 1 \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleDeclaration(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Declarations and Fields \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleConditionalOperator(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Conditional operator ?: \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleMethod(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Methods, Functions and Constructors \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleProperties(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Properties \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.CommonHandler(tcText, "case", 0));
			}
			catch
			{
				tcText = "'Error: Converting Case Blocks \r\n" + str;
			}
			try
			{
				str = tcText;
				tcText = this.FixLines(this.HandleCasting(tcText));
			}
			catch
			{
				tcText = "'Error: Converting Casting from C# to VB.Net \r\n" + str;
			}
			tcText = this.HandleReplacements(tcText);
			tcText = this.FixLines(this.PopQuotesFromStack(tcText));
			tcText = this.HandlePostReplacements(tcText);
			tcText = tcText.TrimEnd(new char[0]) + "\r\n\r\n" + this.GetFooter();
			return tcText;
		}
		protected string FixLines(string tcText)
		{
			tcText = tcText.Replace("\r", "");
			return tcText.Replace("\n", "\r\n");
		}
		protected void HandleBrakets(ref StringBuilder tsb, string tcString, string tcBlankToken)
		{
			tsb.Append(tcBlankToken);
			string text = tcString.Trim();
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '{' || text[i] == '}')
				{
					tsb.Append("\n" + tcBlankToken + text[i].ToString() + "\n");
					if (text[i] == '{')
					{
						tsb.Append(tcBlankToken + "\t");
					}
				}
				else
				{
					tsb.Append(text[i]);
				}
			}
		}
		protected string GetNewToken()
		{
			CSharpToVBConverter.nStackCounter++;
			return this.TokenStarter + CSharpToVBConverter.nStackCounter.ToString() + this.TokenEnder;
		}
		private string HandleBlankLines(string tcText)
		{
			string newToken = this.GetNewToken();
			string cOriginal = " ";
			MappingToken mappingToken = default(MappingToken);
			mappingToken.cOriginal = cOriginal;
			mappingToken.cToken = newToken;
			this.Stack.Add(mappingToken);
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length == 0)
				{
					array[i] = newToken;
				}
				stringBuilder.Append(array[i] + "\n");
			}
			return stringBuilder.ToString();
		}
		private string PopQuotesFromStack(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string tcLine = array[i].Trim();
				if (this.IsHandled(tcLine))
				{
					stringBuilder.Append(array[i] + "\n");
				}
				else
				{
					int num = array[i].IndexOf("<__");
					if (num < 0)
					{
						stringBuilder.Append(array[i] + "\n");
					}
					else
					{
						int num2 = array[i].IndexOf("__>", num + 1);
						if (num2 < 0)
						{
							stringBuilder.Append(array[i] + "\n");
						}
						else
						{
							string s = array[i].Substring(num + 3, num2 - (num + 3));
							string cOriginal = ((MappingToken)this.Stack[int.Parse(s)]).cOriginal;
							array[i] = array[i].Substring(0, num) + cOriginal + array[i].Substring(num2 + 3);
							i--;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
		private string PushQuotesToStack(string tcText, string tcCheckString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string tcLine = array[i].Trim();
				if (this.IsHandled(tcLine))
				{
					stringBuilder.Append(array[i] + "\n");
				}
				else
				{
					int num = array[i].IndexOf(tcCheckString);
					if (num < 0)
					{
						stringBuilder.Append(array[i] + "\n");
					}
					else
					{
						int num2 = array[i].IndexOf(tcCheckString, num + 1);
						if (num2 < 0)
						{
							stringBuilder.Append(array[i] + "\n");
						}
						else
						{
							string text = array[i].Substring(num, num2 - num + 1);
							string newToken = this.GetNewToken();
							array[i] = array[i].Substring(0, num) + newToken + array[i].Substring(num2 + 1);
							if (tcCheckString == "'")
							{
								text = text.Replace("'", "\"") + "c";
							}
							MappingToken mappingToken = default(MappingToken);
							mappingToken.cOriginal = text;
							mappingToken.cToken = newToken;
							this.Stack.Add(mappingToken);
							i--;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
		private string PushCommentsToStack(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Trim();
				int num = array[i].IndexOf("//");
				if (num < 0)
				{
					stringBuilder.Append(array[i] + "\n");
				}
				else
				{
					string cOriginal = "'" + array[i].Substring(num + 2);
					string newToken = this.GetNewToken();
					array[i] = array[i].Substring(0, num) + newToken;
					stringBuilder.Append(array[i] + "\n");
					MappingToken mappingToken = default(MappingToken);
					mappingToken.cOriginal = cOriginal;
					mappingToken.cToken = newToken;
					this.Stack.Add(mappingToken);
				}
			}
			return stringBuilder.ToString();
		}
		private string HandleLineBreakDown(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			BaseManager baseManager = new BaseManager();
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (this.IsHandled(text))
				{
					stringBuilder.Append(array[i] + "\n");
				}
				else
				{
					if (text.StartsWith("{") && text.Length == 1)
					{
						stringBuilder.Append(array[i] + "\n");
					}
					else
					{
						if (text.StartsWith("}") && text.Length == 1)
						{
							stringBuilder.Append(array[i] + "\n");
						}
						else
						{
							if (text.IndexOf("}") >= 0 || text.IndexOf("}") >= 0)
							{
								if (text.IndexOf("while") > 0 || text.IndexOf("until") > 0)
								{
									stringBuilder.Append(array[i] + "\n");
								}
								else
								{
									baseManager.GetBlankToken(array[i]);
									this.HandleBrakets(ref stringBuilder, array[i], baseManager.BlankToken);
									stringBuilder.Append("\n");
								}
							}
							else
							{
								if (text.EndsWith("{"))
								{
									baseManager.GetBlankToken(array[i]);
									this.HandleBrakets(ref stringBuilder, array[i], baseManager.BlankToken);
									stringBuilder.Append("\n");
								}
								else
								{
									stringBuilder.Append(array[i] + "\n");
								}
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
		private string HandleReplacements(string tcText)
		{
			tcText = Regex.Replace(tcText, "using ", "Imports ");
			tcText = Regex.Replace(tcText, "==", "=");
			this.ReplaceTypes(ref tcText, "bool", "Boolean");
			this.ReplaceTypes(ref tcText, "byte", "Byte");
			this.ReplaceTypes(ref tcText, "char", "Char");
			this.ReplaceTypes(ref tcText, "decimal", "Decimal");
			this.ReplaceTypes(ref tcText, "double", "Double");
			this.ReplaceTypes(ref tcText, "float", "single");
			this.ReplaceTypes(ref tcText, "int", "Integer");
			this.ReplaceTypes(ref tcText, "long", "Long");
			this.ReplaceTypes(ref tcText, "object", "Object");
			this.ReplaceTypes(ref tcText, "short", "Short");
			this.ReplaceTypes(ref tcText, "string", "String");
			this.ReplaceTypes(ref tcText, "sbyte", "System.SByte");
			this.ReplaceTypes(ref tcText, "uint", "System.UInt32");
			this.ReplaceTypes(ref tcText, "ulong", "System.UInt64");
			this.ReplaceTypes(ref tcText, "ushort", "System.UInt16");
			this.ReplaceTypes(ref tcText, "this", "Me");
			this.ReplaceTypes(ref tcText, "base", "MyBase");
			tcText = Regex.Replace(tcText, "true", "True");
			tcText = Regex.Replace(tcText, "false", "False");
			tcText = Regex.Replace(tcText, "typeof", "Type.GetType");
			tcText = Regex.Replace(tcText, "public ", "Public ");
			tcText = Regex.Replace(tcText, "///", "'--");
			tcText = Regex.Replace(tcText, "//", "'");
			tcText = Regex.Replace(tcText, "new", "New");
			tcText = Regex.Replace(tcText, "try", "Try");
			tcText = Regex.Replace(tcText, "catch", "Catch");
			tcText = Regex.Replace(tcText, "finally", "Finally");
			tcText = Regex.Replace(tcText, "return", "Return");
			tcText = Regex.Replace(tcText, "throw ", "Throw ");
			tcText = Regex.Replace(tcText, "!=", "<>");
			tcText = Regex.Replace(tcText, "! =", "<>");
			tcText = Regex.Replace(tcText, ";", "");
			tcText = Regex.Replace(tcText, "goto", "GoTo");
			tcText = tcText.Replace("[", "(");
			tcText = tcText.Replace("]", ")");
			tcText = tcText.Replace(">>", ">");
			tcText = tcText.Replace("<<", "<");
			tcText = tcText.Replace(" || ", " Or ");
			tcText = tcText.Replace(" && ", " And ");
			this.ReplaceTypes(ref tcText, "ref", "");
			this.ReplaceTypes(ref tcText, "out", "");
			tcText = Regex.Replace(tcText, "#endregion", "#End Region");
			tcText = Regex.Replace(tcText, "null", "Nothing");
			tcText = Regex.Replace(tcText, "!", "Not ");
			tcText = this.FixLines(this.FixArrays(tcText));
			tcText = this.FixLines(this.HandleExits(tcText));
			return tcText;
		}
		private string HandlePostReplacements(string tcText)
		{
			tcText = Regex.Replace(tcText, "(@\"([^\"]))", "\"$2");
			tcText = Regex.Replace(tcText, "^#region ((\\w*\\s*)*)", "#Region \"$1\"");
			tcText = Regex.Replace(tcText, "=([a-zA-Z0-9 \\.\\(\\)]*)[%]([a-zA-Z0-9 \\.\\(\\)]*)", "= Decimal.Remainder($1, $2)");
			return tcText;
		}
		private string FixArrays(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].EndsWith(this.EndOfAssignedArray))
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					array[i] = array[i].Replace(this.EndOfAssignedArray, "");
					stringBuilder2.Append(array[i] + " ");
					i++;
					while (true)
					{
						stringBuilder2.Append(array[i].Trim());
						if (array[i].Trim().IndexOf("}") >= 0)
						{
							break;
						}
						i++;
					}
					stringBuilder.Append(stringBuilder2.ToString() + "\r");
				}
				else
				{
					stringBuilder.Append(array[i] + "\r");
				}
			}
			return stringBuilder.ToString();
		}
		private void FixExit(ref string[] aText, int nStart)
		{
			for (int i = nStart; i > 0; i--)
			{
				string text = aText[i].TrimStart(new char[0]);
				if (text.IndexOf("Sub ") >= 0)
				{
					aText[nStart] = aText[nStart].Replace("break", "Exit Sub");
					return;
				}
				if (text.IndexOf("Function ") >= 0)
				{
					aText[nStart] = aText[nStart].Replace("break", "Exit Function");
					return;
				}
				if (text.IndexOf("Property ") >= 0)
				{
					aText[nStart] = aText[nStart].Replace("break", "Exit Property");
					return;
				}
				if (text.StartsWith("Do "))
				{
					aText[nStart] = aText[nStart].Replace("break", "Exit Do");
					return;
				}
				if (text.StartsWith("For "))
				{
					aText[nStart] = aText[nStart].Replace("break", "Exit For");
					return;
				}
				if (text.StartsWith("While "))
				{
					aText[nStart] = aText[nStart].Replace("break", "Exit While");
					return;
				}
			}
		}
		private string HandleExits(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim() == "break")
				{
					this.FixExit(ref array, i);
				}
				stringBuilder.Append(array[i] + "\r");
			}
			return stringBuilder.ToString();
		}
		private string GetFooter()
		{
			string str = "";
			str += "'----------------------------------------------------------------\r\n";
			str += "' 转换 C# 到 VB .NET By LiTianPing \r\n";
			return str + "'----------------------------------------------------------------\r\n";
		}
		protected string HandleMultiLineComments(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (flag)
				{
					array[i] = "// " + array[i].Replace("\n", "") + "\n";
					if (array[i].IndexOf("*/") >= 0)
					{
						int num = array[i].IndexOf("*/");
						array[i] = array[i].Substring(0, num + 2) + "\n" + array[i].Substring(num + 2);
						flag = false;
					}
				}
				else
				{
					if (!array[i].Trim().StartsWith("//") && array[i].IndexOf("/*") >= 0)
					{
						flag = true;
						array[i] = array[i].Replace("/*", "");
						i--;
					}
				}
			}
			return this.Build(ref array);
		}
		public void ReplaceTypes(ref string tcText, string tcOld, string tcNew)
		{
			tcText = tcText.Replace(" " + tcOld + " ", " " + tcNew + " ");
			tcText = tcText.Replace("\t" + tcOld + " ", "\t" + tcNew + " ");
			tcText = tcText.Replace("(" + tcOld + ")", "(" + tcNew + ")");
			tcText = tcText.Replace("(" + tcOld + " ", "(" + tcNew + " ");
			tcText = tcText.Replace("\n" + tcOld + " ", "\n" + tcNew + " ");
			tcText = tcText.Replace(tcOld + ".", tcNew + ".");
			tcText = tcText.Replace(" " + tcOld + "(", " " + tcNew + "(");
			tcText = tcText.Replace("\t" + tcOld + "(", "\t" + tcNew + "(");
			tcText = tcText.Replace(" " + tcOld + "\r", " " + tcNew + "\r");
			tcText = tcText.Replace("\t" + tcOld + "\r", "\t" + tcNew + "\r");
			tcText = tcText.Replace(" " + tcOld + "\n", " " + tcNew + "\n");
			tcText = tcText.Replace("\t" + tcOld + "\n", "\t" + tcNew + "\n");
			tcText = tcText.Replace(" " + tcOld + ";", " " + tcNew + ";");
			tcText = tcText.Replace("\t" + tcOld + ";", "\t" + tcNew + ";");
			tcText = tcText.Replace("=" + tcOld + "(", "=" + tcNew + "(");
			tcText = tcText.Replace(" " + tcOld + "(", " " + tcNew + "(");
			tcText = tcText.Replace("=" + tcOld + "[", "=" + tcNew + "[");
			tcText = tcText.Replace(" " + tcOld + "[", " " + tcNew + "[");
			tcText = tcText.Replace(" " + tcOld + ")", " " + tcNew + ")");
			tcText = tcText.Replace("," + tcOld + ")", "," + tcNew + ")");
		}
		protected string HandleAttributes(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim().StartsWith("["))
				{
					for (int j = i; j < array.Length; j++)
					{
						if (array[j].Trim().EndsWith("]"))
						{
							int num = array[i].IndexOf("[");
							array[i] = array[i].Substring(0, num) + "<<" + array[i].Substring(num + 1);
							num = array[j].IndexOf("]");
							array[j] = array[j].Substring(0, num) + ">> _ " + array[j].Substring(num + 1);
							break;
						}
					}
				}
			}
			return this.Build(ref array);
		}
		protected string HandleMathOperations(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			MathManager mathManager = new MathManager();
			for (int i = 0; i < array.Length; i++)
			{
				if (!this.IsHandled(array[i]) && (array[i].IndexOf("++") >= 0 || array[i].IndexOf("--") >= 0) && !array[i].Trim().StartsWith("for"))
				{
					array[i] = mathManager.GetBlock(array[i] + "\r\n");
				}
			}
			return this.Build(ref array);
		}
		protected string HandleConditionalOperator(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (!this.IsHandled(array[i]) && (array[i].IndexOf("?") >= 0 || array[i].IndexOf(":") >= 0))
				{
					string pattern;
					string replacement;
					if (array[i].Trim().StartsWith("return"))
					{
						pattern = "([ ]*return[ ]*)([a-zA-Z0-9 \\.\\(\\)]*)[?]([a-zA-Z0-9 \\.\\(\\)]*)[:]([a-zA-Z0-9 \\.\\(\\)]*)";
						replacement = "$1 IIf($2, $3, $4)";
					}
					else
					{
						pattern = "([a-zA-Z0-9 \\.\\(\\)]*)[=]([a-zA-Z0-9 \\.\\(\\)]*)[?]([a-zA-Z0-9 \\.\\(\\)]*)[:]([a-zA-Z0-9 \\.\\(\\)]*)";
						replacement = "$1= IIf($2, $3, $4)";
					}
					string str = Regex.Replace(array[i], pattern, replacement);
					array[i] = str + "\r\n";
				}
			}
			return this.Build(ref array);
		}
		protected bool IsNextCharValidVariable(string tcLine, int tnStart)
		{
			if (tcLine.Trim().EndsWith(";") || tcLine.Trim().EndsWith(")"))
			{
				tcLine = tcLine.Replace(") ", ")");
			}
			if (tnStart == tcLine.Length)
			{
				return false;
			}
			char c = tcLine[tnStart + 1];
			int num = (int)c;
			return num == 95 || num == 40 || (num >= 65 && num <= 90) || (num >= 97 && num <= 122);
		}
		public string HandleCasting(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IndexOf("(") > 0)
				{
					this.GetFixedCasting(ref array[i]);
				}
				stringBuilder.Append(array[i] + "\n");
			}
			return stringBuilder.ToString();
		}
		protected void GetFixedCasting(ref string tcLine)
		{
			int num = -1;
			while (true)
			{
				int num2 = tcLine.IndexOf("(", num + 1);
				num = num2;
				if (num2 < 0)
				{
					break;
				}
				tcLine = tcLine.Replace("( ", "(");
				tcLine = tcLine.Replace(" )", ")");
				if (this.IsNextCharValidVariable(tcLine, num))
				{
					int num3 = tcLine.IndexOf(")", num + 1);
					if (num3 < 0)
					{
						return;
					}
					num = tcLine.Substring(0, num3).LastIndexOf("(");
					num2 = num;
					int num4 = tcLine.IndexOf(" ", num + 1);
					if (num4 != -1 && num4 <= num3)
					{
						return;
					}
					if (this.IsNextCharValidVariable(tcLine, num3))
					{
						int num5 = -1;
						bool flag = false;
						int num6 = 0;
						int num7 = 0;
						int i = num3 + 1;
						while (i < tcLine.Length)
						{
							char c = tcLine[i];
							switch (c)
							{
							case '(':
								num6++;
								break;
							case ')':
								num7++;
								break;
							default:
								if (c == ';')
								{
									flag = true;
								}
								break;
							}
							if (flag)
							{
								goto IL_FB;
							}
							if (num7 > num6)
							{
								flag = true;
								goto IL_FB;
							}
							IL_107:
							i++;
							continue;
							IL_FB:
							if (flag)
							{
								num5 = i - 1;
								break;
							}
							goto IL_107;
						}
						if (flag)
						{
							string str = tcLine.Substring(num3 + 1, num5 - num3).Trim();
							string text = tcLine;
							string str2 = tcLine.Substring(0, num2);
							string str3 = "CType(" + str + ", ";
							string text2 = tcLine.Substring(num2 + 1, num3 - (num2 + 1));
							if (text2.Trim().StartsWith("(") && text2.Trim().EndsWith(")"))
							{
								text2 = text2.TrimStart(new char[0]).Substring(1);
							}
							else
							{
								text2 += ")";
							}
							string str4 = tcLine.Substring(num5 + 1).TrimStart(new char[0]);
							text = str2 + str3 + text2 + str4;
							tcLine = text;
						}
					}
				}
			}
		}
		private string HandleForBlocks(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder();
				string tcForBlock = "";
				if (text.Trim().StartsWith("for"))
				{
					int num = 0;
					int num2 = 0;
					string tcForCondition = text;
					bool flag2 = false;
					int j;
					for (j = i + 1; j < array.Length; j++)
					{
						if (this.IsHandled(array[j]))
						{
							stringBuilder.Append(array[j] + "\n");
						}
						else
						{
							if (!flag2)
							{
								int num3;
								int num4;
								if (array[j].StartsWith("for"))
								{
									int startIndex = array[j].LastIndexOf(")");
									num3 = array[j].IndexOf(";", startIndex);
									num4 = array[j].IndexOf("{", startIndex);
								}
								else
								{
									num3 = array[j].IndexOf(";");
									num4 = array[j].IndexOf("{");
								}
								if (num4 > num3)
								{
									flag2 = true;
								}
								else
								{
									if (num3 > num4)
									{
										stringBuilder.Append(array[j]);
										tcForBlock = stringBuilder.ToString();
										break;
									}
								}
							}
							if (!flag)
							{
								if (array[j].IndexOf("{") > 0)
								{
									num++;
								}
								if (array[j].IndexOf("}") > 0)
								{
									num2++;
								}
								stringBuilder.Append(array[j]);
								if (num != 0 && num2 != 0 && num - num2 == 0)
								{
									tcForBlock = stringBuilder.ToString();
									break;
								}
							}
						}
					}
					ForBlockManager forBlockManager = new ForBlockManager();
					string forBlock = forBlockManager.GetForBlock(this, tcForCondition, tcForBlock);
					this.UpdateIfBlock(ref array, forBlock, i, j);
				}
			}
			return this.Build(ref array);
		}
		protected string Build(ref string[] taText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < taText.Length; i++)
			{
				stringBuilder.Append(taText[i]);
			}
			return stringBuilder.ToString();
		}
		protected string GetErrLine()
		{
			return "'----- Error occured when converting -----'\n";
		}
		private bool IsMethod(string tcLine)
		{
			tcLine = ReplaceManager.GetSingledSpacedString(tcLine);
			if (tcLine.StartsWith("Default ") || tcLine.StartsWith("While ") || tcLine.StartsWith("Select "))
			{
				return false;
			}
			bool result = false;
			if (tcLine.EndsWith(")"))
			{
				if (tcLine.IndexOf("[") >= 0)
				{
					if (tcLine.IndexOf("(") < tcLine.IndexOf("["))
					{
						result = true;
					}
				}
				else
				{
					result = true;
				}
			}
			return result;
		}
		private string HandleMethod(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (!this.IsHandled(text) && this.IsMethod(text))
				{
					int num = 0;
					int num2 = 0;
					int nEnd = i;
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = false;
					for (int j = i; j < array.Length; j++)
					{
						stringBuilder.Append(array[j]);
						if (!this.IsHandled(array[j]))
						{
							if (array[j].IndexOf("{") > 0)
							{
								num++;
							}
							if (array[j].IndexOf("}") > 0)
							{
								num2++;
							}
							if (num != 0 && num2 != 0 && num - num2 == 0)
							{
								nEnd = j;
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						MethodBlockManager methodBlockManager = new MethodBlockManager();
						string methodBlock = methodBlockManager.GetMethodBlock(text, stringBuilder.ToString());
						this.UpdateIfBlock(ref array, methodBlock, i, nEnd);
					}
				}
			}
			return this.Build(ref array);
		}
		private bool IsProperty(string tcLine)
		{
			tcLine = ReplaceManager.GetSingledSpacedString(tcLine);
			if (tcLine.StartsWith("Default "))
			{
				return true;
			}
			if (!tcLine.EndsWith(")") && !tcLine.EndsWith(";") && !tcLine.EndsWith("="))
			{
				for (int i = 0; i < this.MethodModifiers.Length; i++)
				{
					if (tcLine.StartsWith(this.MethodModifiers[i]) && tcLine.IndexOf("enum") < 0 && tcLine.IndexOf("interface") < 0 && tcLine.IndexOf("struct") < 0 && tcLine.IndexOf("class") < 0)
					{
						return true;
					}
				}
			}
			return false;
		}
		private string HandleProperties(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string tcLine = array[i];
				if (this.IsProperty(tcLine))
				{
					int num = 0;
					int num2 = 0;
					int nEnd = i;
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = i; j < array.Length; j++)
					{
						stringBuilder.Append(array[j] + "\r\n");
						if (!this.IsHandled(array[j]))
						{
							if (array[j].IndexOf("{") > 0)
							{
								num++;
							}
							if (array[j].IndexOf("}") > 0)
							{
								num2++;
							}
							if (num != 0 && num2 != 0 && num - num2 == 0)
							{
								nEnd = j;
								break;
							}
						}
					}
					PropertyManager propertyManager = new PropertyManager();
					string block = propertyManager.GetBlock(tcLine, stringBuilder.ToString());
					this.UpdateIfBlock(ref array, block, i, nEnd);
				}
			}
			return this.Build(ref array);
		}
		protected bool CheckIfFound(string tcStr, string tcSearch, int tnType)
		{
			if (tnType == 0)
			{
				return tcStr.Trim().StartsWith(tcSearch);
			}
			return tcStr.Trim().IndexOf(tcSearch) >= 0;
		}
		private string CommonHandler(string tcText, string cSearch, int tnType)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (!this.IsHandled(text) && this.CheckIfFound(text, cSearch, tnType))
				{
					int num = 0;
					int num2 = 0;
					int nEnd = i;
					string cSource = "";
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = false;
					for (int j = i; j < array.Length; j++)
					{
						stringBuilder.Append(array[j]);
						if (!this.IsHandled(array[j]))
						{
							if (!flag)
							{
								int num3 = array[j].IndexOf(";");
								int num4 = array[j].IndexOf("{");
								if (num4 > num3)
								{
									flag = true;
								}
								else
								{
									if (num3 > num4)
									{
										nEnd = j;
										break;
									}
								}
								if (array[j].IndexOf("{") > 0)
								{
									num++;
								}
								if (array[j].IndexOf("}") > 0)
								{
									num2++;
								}
							}
							else
							{
								if (array[j].IndexOf("{") > 0)
								{
									num++;
								}
								if (array[j].IndexOf("}") > 0)
								{
									num2++;
								}
								if (num != 0 && num2 != 0 && num - num2 == 0)
								{
									nEnd = j;
									break;
								}
							}
						}
					}
					string key;
					switch (key = cSearch.Trim())
					{
					case "class":
					{
						ClassBlockManager classBlockManager = new ClassBlockManager();
						cSource = classBlockManager.GetClassBlock(text, stringBuilder.ToString());
						break;
					}
					case "struct":
					{
						StructureBlockManager structureBlockManager = new StructureBlockManager();
						cSource = structureBlockManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					case "interface":
					{
						InterfaceBlockManager interfaceBlockManager = new InterfaceBlockManager();
						cSource = interfaceBlockManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					case "namespace":
					{
						NameSpaceManager nameSpaceManager = new NameSpaceManager();
						cSource = nameSpaceManager.GetNameSpaceBlock(text, stringBuilder.ToString());
						break;
					}
					case "foreach":
					{
						ForEachManager forEachManager = new ForEachManager();
						cSource = forEachManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					case "do":
					{
						if (!ReplaceManager.IsNextCharValid(text, "do"))
						{
							goto IL_37E;
						}
						DoWhileManager doWhileManager = new DoWhileManager();
						cSource = doWhileManager.GetBlock(this, text, stringBuilder.ToString());
						break;
					}
					case "while":
					{
						WhileManager whileManager = new WhileManager();
						cSource = whileManager.GetBlock(this, text, stringBuilder.ToString());
						break;
					}
					case "switch":
					{
						SwitchManager switchManager = new SwitchManager();
						cSource = switchManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					case "case":
					{
						CaseManager caseManager = new CaseManager();
						cSource = caseManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					case "catch":
					{
						CatchManager catchManager = new CatchManager();
						cSource = catchManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					case "enum":
					{
						EnumManager enumManager = new EnumManager();
						cSource = enumManager.GetBlock(text, stringBuilder.ToString());
						break;
					}
					}
					this.UpdateIfBlock(ref array, cSource, i, nEnd);
				}
				IL_37E:;
			}
			return this.Build(ref array);
		}
		private string GetBlock(ref string[] aText, ref int nCurrent, string cSearchFor, ref int nFoundAt, ref string tcNextStartWith)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = nCurrent; i < aText.Length; i++)
			{
				if (!this.IsHandled(aText[i]))
				{
					int num = 0;
					int num2 = 0;
					bool flag = false;
					if (aText[i].Trim().StartsWith(cSearchFor))
					{
						nFoundAt = i;
						for (int j = i; j < aText.Length; j++)
						{
							stringBuilder.Append(aText[j] + "\r\n");
							if (!aText[j].Trim().StartsWith("//"))
							{
								if (!flag)
								{
									int num3 = aText[j].IndexOf(";");
									int num4 = aText[j].IndexOf("{");
									if (num4 > num3)
									{
										flag = true;
									}
									else
									{
										if (num3 > num4)
										{
											tcNextStartWith = ";";
											nCurrent = j;
											return stringBuilder.ToString();
										}
									}
								}
								if (aText[j].IndexOf("{") >= 0)
								{
									num++;
								}
								if (aText[j].IndexOf("}") >= 0)
								{
									num2++;
								}
								if (num != 0 && num2 != 0 && num - num2 == 0)
								{
									nCurrent = j;
									tcNextStartWith = "}";
									return stringBuilder.ToString();
								}
							}
						}
					}
				}
			}
			return "";
		}
		private bool IsImmediatelyAfter(ref string[] tcSearchIn, ref int nStart, string tcAfter, string tcSearchFor)
		{
			for (int i = nStart; i < tcSearchIn.Length; i++)
			{
				if (tcSearchIn[i].Trim().Length != 0 && !tcSearchIn[i].Trim().StartsWith("//"))
				{
					int num = tcSearchIn[i].IndexOf(tcAfter);
					if (num < 0)
					{
						break;
					}
					if (tcSearchIn[i].IndexOf(tcSearchFor, num) > 0)
					{
						nStart = i;
						return true;
					}
					for (int j = i + 1; j < tcSearchIn.Length; j++)
					{
						if (tcSearchIn[j].Trim().Length != 0 && !tcSearchIn[j].Trim().StartsWith(this.BlankLineToken) && !tcSearchIn[j].Trim().StartsWith("//"))
						{
							bool flag = tcSearchIn[j].Trim().StartsWith(tcSearchFor);
							if (flag)
							{
								nStart = j;
							}
							return flag;
						}
					}
				}
			}
			return false;
		}
		private string HandleIfBlock(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (!this.IsHandled(text) && text.Trim().StartsWith("if"))
				{
					ArrayList arrayList = new ArrayList();
					int nCurrent = i;
					int num = 0;
					int nEnd = i;
					string tcAfter = "";
					string block = this.GetBlock(ref array, ref nEnd, "if", ref num, ref tcAfter);
					string tcIfLine = array[num];
					while (this.IsImmediatelyAfter(ref array, ref nEnd, tcAfter, "else"))
					{
						string block2 = this.GetBlock(ref array, ref nEnd, "else", ref num, ref tcAfter);
						string elseLine = array[num];
						arrayList.Add(new ElseBlockToken
						{
							ElseLine = elseLine,
							ElseBlock = block2
						});
					}
					IfBlockManager ifBlockManager = new IfBlockManager();
					string ifBlock = ifBlockManager.GetIfBlock(this, block, tcIfLine, arrayList);
					this.UpdateIfBlock(ref array, ifBlock, nCurrent, nEnd);
				}
			}
			return this.Build(ref array);
		}
		private string HandleTryCatchFinally(string tcText)
		{
			string[] array = tcText.Split(new char[]
			{
				'\r'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (!this.IsHandled(text) && text.Trim().StartsWith("try"))
				{
					string tcFinallyBlock = "";
					string text2 = "";
					int nCurrent = i;
					int num = 0;
					int nEnd = i;
					string tcAfter = "";
					string block = this.GetBlock(ref array, ref nEnd, "try", ref num, ref tcAfter);
					while (this.IsImmediatelyAfter(ref array, ref nEnd, tcAfter, "catch"))
					{
						text2 += this.GetBlock(ref array, ref nEnd, "catch", ref num, ref tcAfter);
						string arg_B6_0 = array[num];
					}
					if (this.IsImmediatelyAfter(ref array, ref nEnd, tcAfter, "finally"))
					{
						tcFinallyBlock = this.GetBlock(ref array, ref nEnd, "finally", ref num, ref tcAfter);
					}
					TryCatchFinallyManager tryCatchFinallyManager = new TryCatchFinallyManager();
					string block2 = tryCatchFinallyManager.GetBlock(block, text2, tcFinallyBlock);
					this.UpdateIfBlock(ref array, block2, nCurrent, nEnd);
				}
			}
			return this.Build(ref array);
		}
		private void UpdateIfBlock(ref string[] aDest, string cSource, int nCurrent, int nEnd)
		{
			string[] array = cSource.Split(new char[]
			{
				'\n'
			});
			for (int i = nCurrent; i <= nEnd; i++)
			{
				try
				{
					aDest[i] = "";
				}
				catch
				{
				}
			}
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Trim().Length > 0)
				{
					if (num == nEnd - nCurrent + 1)
					{
						aDest[nCurrent + num - 1] = aDest[nCurrent + num - 1] + "\n" + array[j];
					}
					else
					{
						aDest[nCurrent + num] = "\n" + array[j];
						num++;
					}
				}
			}
		}
		private bool IsHandled(string tcLine)
		{
			bool result = false;
			if (tcLine.Trim().Length == 0)
			{
				result = true;
			}
			if (tcLine.Trim().StartsWith("//"))
			{
				result = true;
			}
			if (tcLine.Trim().StartsWith("using"))
			{
				result = true;
			}
			if (tcLine.Trim().StartsWith("throw"))
			{
				result = true;
			}
			return result;
		}
		private string HandleDeclaration(string tcText)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = tcText.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				string text = array[i];
				if (!this.IsHandled(text) && text.IndexOf("+=") < 0 && (text.IndexOf(";") > 0 || text.Trim().EndsWith("]") || (text.IndexOf("[") > 0 && text.Trim().EndsWith("="))) && !flag)
				{
					string text2 = text.Trim();
					string singledSpacedString = ReplaceManager.GetSingledSpacedString(text2);
					if (singledSpacedString.IndexOf("new ") > 0 && singledSpacedString.EndsWith("];"))
					{
						text = text.Replace(";", " {};");
						text2 = text;
					}
					int num = text2.IndexOf('=');
					if (num > 0)
					{
						text2 = text2.Substring(0, num).Trim();
					}
					text2 = ReplaceManager.GetSingledSpacedString(text2);
					if (text2.EndsWith(";"))
					{
						text2 = text2.Substring(0, text2.Length - 1).Trim();
					}
					for (int j = 0; j < ReplaceManager.Modifiers.Length; j++)
					{
						if (text2.StartsWith(ReplaceManager.Modifiers[j]))
						{
							text2 = text2.Substring(ReplaceManager.Modifiers[j].Length + 1);
							j = 0;
						}
					}
					text2 = text2.Trim();
					string[] array2 = text2.Split(new char[]
					{
						' '
					});
					if (array2.Length == 2)
					{
						FieldManager fieldManager = new FieldManager();
						text = fieldManager.GetConvertedExpression(text);
					}
				}
				stringBuilder.Append(text + "\n");
			}
			return stringBuilder.ToString();
		}
	}
}
