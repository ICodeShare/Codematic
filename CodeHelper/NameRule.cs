using Maticsoft.CmConfig;
using System;
namespace Maticsoft.CodeHelper
{
	public class NameRule
	{
		public static string GetModelClass(string TabName, DbSettings dbset)
		{
			return dbset.ModelPrefix + NameRule.TabNameRuled(TabName, dbset) + dbset.ModelSuffix;
		}
		public static string GetBLLClass(string TabName, DbSettings dbset)
		{
			return dbset.BLLPrefix + NameRule.TabNameRuled(TabName, dbset) + dbset.BLLSuffix;
		}
		public static string GetDALClass(string TabName, DbSettings dbset)
		{
			return dbset.DALPrefix + NameRule.TabNameRuled(TabName, dbset) + dbset.DALSuffix;
		}
		private static string TabNameRuled(string TabName, DbSettings dbset)
		{
			string text = TabName;
			if (dbset.ReplacedOldStr.Length > 0)
			{
				text = text.Replace(dbset.ReplacedOldStr, dbset.ReplacedNewStr);
			}
			string a;
			if ((a = dbset.TabNameRule.ToLower()) != null)
			{
				if (!(a == "lower"))
				{
					if (!(a == "upper"))
					{
						if (!(a == "firstupper"))
						{
							if (!(a == "same"))
							{
							}
						}
						else
						{
							string str = text.Substring(0, 1).ToUpper();
							text = str + text.Substring(1);
						}
					}
					else
					{
						text = text.ToUpper();
					}
				}
				else
				{
					text = text.ToLower();
				}
			}
			return text;
		}
	}
}
