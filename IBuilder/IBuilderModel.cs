using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.IBuilder
{
	public interface IBuilderModel
	{
		string ModelName
		{
			get;
			set;
		}
		string NameSpace
		{
			get;
			set;
		}
		string Modelpath
		{
			get;
			set;
		}
		List<ColumnInfo> Fieldlist
		{
			get;
			set;
		}
		string CreatModel();
		string CreatModelMethod();
	}
}
