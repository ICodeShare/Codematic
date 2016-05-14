using System;
using System.Text;
namespace LTP.ConvertCS2VB
{
	public class ClassBlockManager : BaseManager
	{
		protected string ClassBlockToken = "";
		protected string ClassDeclarationToken = "";
		protected string ImplementationDeclationToken = "";
		protected string ClassNameToken = "";
		public string GetClassBlock(string tcClassDeclaration, string tcClassBlock)
		{
			base.GetBlankToken(tcClassDeclaration);
			this.BuildClassDeclaration(tcClassDeclaration);
			this.ClassBlockToken = base.ExtractBlock(tcClassBlock, "{", "}");
			this.HandleConstructors();
			return this.Execute();
		}
		private void HandleConstructors()
		{
			string[] array = this.ClassBlockToken.Split(new char[]
			{
				'\n'
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim().EndsWith(")") && array[i].IndexOf(this.ClassNameToken) >= 0)
				{
					if (array[i].IndexOf("~") >= 0)
					{
						array[i] = array[i].Replace(this.ClassNameToken, " void Finalize");
						array[i] = array[i].Replace("~", "");
					}
					else
					{
						array[i] = array[i].Replace(this.ClassNameToken, " void New");
					}
					int num = array[i].IndexOf(":");
					if (num > 0)
					{
						array[i] = array[i].Substring(0, num) + array[i].Substring(num).Replace("base", "MyBase.New");
					}
				}
				else
				{
					if (array[i].TrimEnd(new char[0]).EndsWith("]") && array[i].IndexOf(" this") >= 0)
					{
						array[i] = array[i].Replace(" this", " Item");
						int num2 = array[i].IndexOf("[");
						int num3 = array[i].LastIndexOf("]");
						array[i] = array[i].Substring(0, num2) + "(" + array[i].Substring(num2 + 1, num3 - num2 - 1) + ")";
						BaseManager baseManager = new BaseManager();
						baseManager.GetBlankToken(array[i]);
						array[i] = baseManager.BlankToken + "Default " + array[i].Trim();
					}
				}
				stringBuilder.Append(array[i] + "\n");
			}
			this.ClassBlockToken = stringBuilder.ToString();
		}
		private void BuildClassDeclaration(string tcline)
		{
			tcline = tcline.Replace(":", " : ");
			tcline = ReplaceManager.GetSingledSpacedString(tcline);
			int num = tcline.IndexOf(":");
			string text;
			string text2;
			if (num > 0)
			{
				text = tcline.Substring(0, num - 1);
				text2 = tcline.Substring(num).Trim();
			}
			else
			{
				text = tcline;
				text2 = "";
			}
			text = ReplaceManager.HandleTypes(text);
			this.ClassDeclarationToken = text.Trim();
			int num2 = this.ClassDeclarationToken.LastIndexOf(" ");
			this.ClassNameToken = this.ClassDeclarationToken.Substring(num2 + 1);
			text2 = text2.Replace(":", "").Trim();
			if (text2.Length == 0)
			{
				return;
			}
			string[] array = text2.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text3 = array[i].Trim();
				string text4 = " Inherits ";
				if (text3[0] == 'i' || text3[0] == 'I')
				{
					text4 = " Implements ";
				}
				string implementationDeclationToken = this.ImplementationDeclationToken;
				this.ImplementationDeclationToken = string.Concat(new string[]
				{
					implementationDeclationToken,
					"\n",
					'\t'.ToString(),
					text4,
					text3
				});
			}
		}
		protected virtual string Execute()
		{
			string text = this.BlankToken + this.ClassDeclarationToken + this.ImplementationDeclationToken;
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				this.ClassBlockToken,
				"\r\n",
				this.BlankToken,
				"End Class\r\n"
			});
		}
	}
}
