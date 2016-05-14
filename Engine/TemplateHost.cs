using Maticsoft.CmConfig;
using Maticsoft.CodeHelper;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Maticsoft.CodeEngine
{
	[Serializable]
	public class TemplateHost : ITextTemplatingEngineHost
	{
		internal string _templateFileValue;
		private string _namespace = "Maticsoft";
		private string _fileExtensionValue = ".cs";
		private Encoding _fileEncodingValue = Encoding.UTF8;
		private CompilerErrorCollection _ErrorCollection;
		public string TemplateFile
		{
			get
			{
				return this._templateFileValue;
			}
			set
			{
				this._templateFileValue = value;
			}
		}
		public IList<string> StandardAssemblyReferences
		{
			get
			{
				return new string[]
				{
					typeof(Uri).Assembly.Location,
					typeof(ColumnInfo).Assembly.Location,
					typeof(CodeCommon).Assembly.Location,
					typeof(DbSettings).Assembly.Location,
					typeof(DatabaseHost).Assembly.Location,
					typeof(TableHost).Assembly.Location,
					typeof(ProcedureHost).Assembly.Location,
					typeof(TemplateHost).Assembly.Location
				};
			}
		}
		public IList<string> StandardImports
		{
			get
			{
				return new string[]
				{
					"System",
					"System.Text",
					"System.Collections.Generic",
					"Maticsoft.CodeHelper",
					"Maticsoft.CodeEngine"
				};
			}
		}
		public string NameSpace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}
		public string FileExtension
		{
			get
			{
				return this._fileExtensionValue;
			}
		}
		public Encoding FileEncoding
		{
			get
			{
				return this._fileEncodingValue;
			}
		}
		public CompilerErrorCollection ErrorCollection
		{
			get
			{
				return this._ErrorCollection;
			}
		}
		public object GetHostOption(string optionName)
		{
			object result;
			if (optionName != null && optionName == "CacheAssemblies")
			{
				result = true;
			}
			else
			{
				result = null;
			}
			return result;
		}
		public bool LoadIncludeText(string requestFileName, out string content, out string location)
		{
			content = string.Empty;
			location = string.Empty;
			if (File.Exists(requestFileName))
			{
				content = File.ReadAllText(requestFileName);
				return true;
			}
			return false;
		}
		public void LogErrors(CompilerErrorCollection errors)
		{
			this._ErrorCollection = errors;
		}
		public AppDomain ProvideTemplatingAppDomain(string content)
		{
			return AppDomain.CreateDomain("Generation App Domain");
		}
		public string ResolveAssemblyReference(string assemblyReference)
		{
			if (File.Exists(assemblyReference))
			{
				return assemblyReference;
			}
			string text = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
			if (File.Exists(text))
			{
				return text;
			}
			return "";
		}
		public Type ResolveDirectiveProcessor(string processorName)
		{
			string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase);
			throw new Exception("没有找到指令处理器");
		}
		public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
		{
			if (directiveId == null)
			{
				throw new ArgumentNullException("the directiveId cannot be null");
			}
			if (processorName == null)
			{
				throw new ArgumentNullException("the processorName cannot be null");
			}
			if (parameterName == null)
			{
				throw new ArgumentNullException("the parameterName cannot be null");
			}
			return string.Empty;
		}
		public string ResolvePath(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("the file name cannot be null");
			}
			if (File.Exists(fileName))
			{
				return fileName;
			}
			string text = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
			if (File.Exists(text))
			{
				return text;
			}
			return fileName;
		}
		public void SetFileExtension(string extension)
		{
			this._fileExtensionValue = extension;
		}
		public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
		{
			this._fileEncodingValue = encoding;
		}
	}
}
