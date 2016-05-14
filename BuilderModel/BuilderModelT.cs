using Maticsoft.Utility;
using System;
namespace Maticsoft.BuilderModel
{
	public class BuilderModelT : BuilderModel
	{
		private string _modelnameson = "";
		public string ModelNameSon
		{
			get
			{
				return this._modelnameson;
			}
			set
			{
				this._modelnameson = value;
			}
		}
		public string CreatModelMethodT()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine(base.CreatModelMethod());
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"private List<",
				this.ModelNameSon,
				"> _",
				this.ModelNameSon.ToLower(),
				"s;"
			}));
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 子类 ");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public List<",
				this.ModelNameSon,
				"> ",
				this.ModelNameSon,
				"s"
			}));
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "set{ _" + this.ModelNameSon.ToLower() + "s=value;}");
			stringPlus.AppendSpaceLine(3, "get{return _" + this.ModelNameSon.ToLower() + "s;}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
	}
}
