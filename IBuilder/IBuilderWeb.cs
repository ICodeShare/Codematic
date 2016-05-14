using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.IBuilder
{
	public interface IBuilderWeb
	{
		string NameSpace
		{
			get;
			set;
		}
		string Folder
		{
			get;
			set;
		}
		string ModelName
		{
			get;
			set;
		}
		string BLLName
		{
			get;
			set;
		}
		List<ColumnInfo> Fieldlist
		{
			get;
			set;
		}
		List<ColumnInfo> Keys
		{
			get;
			set;
		}
		string GetAddAspx();
		string GetUpdateAspx();
		string GetShowAspx();
		string GetListAspx();
		string GetWebHtmlCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm);
		string GetAddAspxCs();
		string GetUpdateAspxCs();
		string GetUpdateShowAspxCs();
		string GetShowAspxCs();
		string GetDeleteAspxCs();
		string GetListAspxCs();
		string CreatSearchForm();
		string GetWebCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm);
		string GetAddDesigner();
		string GetUpdateDesigner();
		string GetShowDesigner();
		string GetListDesigner();
	}
}
