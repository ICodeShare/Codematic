using System;
using System.Text;
namespace LTP.ConvertCS2VB
{
	public class ReplaceManager
	{
		public static string[] Modifiers = new string[]
		{
			"internal",
			"new",
			"private",
			"protected",
			"public",
			"readonly",
			"static"
		};
		public static string HandleModifiers(string tcString)
		{
			tcString = tcString.Replace("new ", "Shadows ");
			tcString = tcString.Replace("static ", "Shared ");
			tcString = tcString.Replace("virtual ", "Overridable ");
			tcString = tcString.Replace("sealed ", "NotOverridable ");
			tcString = tcString.Replace("abstract ", "MustOverride ");
			tcString = tcString.Replace("override ", "Overrides ");
			tcString = tcString.Replace("public ", "Public ");
			tcString = tcString.Replace("protected ", "Protected ");
			tcString = tcString.Replace("internal ", "Friend ");
			tcString = tcString.Replace("private ", "Private ");
			tcString = tcString.Replace("void ", " ");
			tcString = tcString.Replace("readonly ", "ReadOnly ");
			tcString = tcString.Replace("volatile ", "<Volatile_Not_Supported> ");
			tcString = tcString.Replace("operator ", "<Operator_Overloading_Not_Supported> ");
			tcString = tcString.Replace("explicit ", "<Explicit_Not_Supported> ");
			tcString = tcString.Replace("implicit ", "<Implicit_Not_Supported> ");
			return tcString;
		}
		public static string HandleTypes(string tcString)
		{
			tcString = tcString.Replace("class", "Class");
			tcString = tcString.Replace("struct", "Structure");
			tcString = tcString.Replace("interface", "Interface");
			tcString = tcString.Replace("enum", "Enum");
			tcString = tcString.Replace("public ", "Public ");
			tcString = tcString.Replace("protected ", "Protected ");
			tcString = tcString.Replace("internal ", "Friend ");
			tcString = tcString.Replace("private ", "Private ");
			tcString = tcString.Replace("new ", "Shadows ");
			tcString = tcString.Replace("abstract ", "MustInherit ");
			tcString = tcString.Replace("sealed ", "NonInheritable ");
			return tcString;
		}
		public static string HandleExpression(string tcExpression)
		{
			tcExpression = ReplaceManager.GetSingledSpacedString(tcExpression);
			tcExpression = tcExpression.Trim();
			int num = tcExpression.IndexOf(" is ");
			if (num > 0)
			{
				tcExpression = "TypeOf " + tcExpression.Substring(0, num) + tcExpression.Substring(num);
				tcExpression = tcExpression.Replace(" is ", " Is ");
			}
			if (tcExpression.EndsWith("null"))
			{
				string text = tcExpression.Replace("!=", "!= ");
				text = text.Replace("==", "== ");
				if (text.EndsWith(" null"))
				{
					tcExpression = text;
					tcExpression = tcExpression.Replace(" null", " Nothing");
					if (tcExpression.IndexOf("!=") > 0)
					{
						tcExpression = tcExpression.Replace("!=", " Is ");
						tcExpression = "Not " + tcExpression;
					}
					else
					{
						tcExpression = tcExpression.Replace("==", " Is ");
					}
					tcExpression = ReplaceManager.GetSingledSpacedString(tcExpression);
				}
			}
			return tcExpression;
		}
		public static string HandleDataTypes(string tcString)
		{
			return tcString;
		}
		public static string GetSingledSpacedString(string tcLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			tcLine = tcLine.Replace(" [", "[");
			tcLine = tcLine.Replace(" ;", ";");
			tcLine = tcLine.Replace(" ,", ",");
			string[] array = tcLine.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim().Length > 0)
				{
					stringBuilder.Append(array[i]);
					if (!array[i].EndsWith(","))
					{
						stringBuilder.Append(" ");
					}
				}
			}
			stringBuilder.Replace(" (", "(");
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}
		public static bool IsNextCharValid(string tcLine, string tcCheck)
		{
			tcLine = tcLine.Trim();
			int length = tcCheck.Length;
			bool result = false;
			if (tcLine == tcCheck)
			{
				result = true;
			}
			else
			{
				int num = tcLine.IndexOf(tcCheck);
				if (tcLine[num + length + 1] == ' ' || tcLine[num + 3] == '\t' || tcLine[num + 3] == '{')
				{
					result = true;
				}
			}
			return result;
		}
	}
}
