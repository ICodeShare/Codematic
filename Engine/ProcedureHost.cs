using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.CodeEngine
{
	[Serializable]
	public class ProcedureHost : TableHost
	{
		public string ProcedureName
		{
			get
			{
				return base.TableName;
			}
		}
		public List<ColumnInfo> Parameterlist
		{
			get
			{
				return base.Fieldlist;
			}
		}
		public ColumnInfo OutParameter
		{
			get
			{
				ColumnInfo result = null;
				foreach (ColumnInfo current in this.Parameterlist)
				{
					if (current.Description == "isoutparam")
					{
						result = current;
					}
				}
				return result;
			}
		}
		public string GetMethodName(string ProcedureName)
		{
			return ProcedureName;
		}
	}
}
