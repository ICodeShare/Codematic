using Maticsoft.CodeHelper;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
namespace Maticsoft.CodeEngine
{
	public class CodeGenerator
	{
		public CodeInfo GenerateCode(string input, TemplateHost host)
		{
			CodeInfo codeInfo = new CodeInfo();
			Engine engine = new Engine();
			codeInfo.Code = engine.ProcessTemplate(input, host);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CompilerError compilerError in host.ErrorCollection)
			{
				stringBuilder.AppendLine(compilerError.ToString());
			}
			codeInfo.ErrorMsg = stringBuilder.ToString();
			codeInfo.FileExtension = host.FileExtension;
			return codeInfo;
		}
		public CodeInfo BatchGenerateCode(string[] templatefile)
		{
			if (templatefile.Length == 0)
			{
				throw new Exception("you must provide a text template file path");
			}
			string text = templatefile[0];
			if (text == null)
			{
				throw new ArgumentNullException("the file name cannot be null");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("the file cannot be found");
			}
			TemplateHost templateHost = new TemplateHost();
			templateHost.TemplateFile = text;
			string input = File.ReadAllText(text);
			return this.GenerateCode(input, templateHost);
		}
	}
}
