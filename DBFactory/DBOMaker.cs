using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Reflection;
namespace Maticsoft.DBFactory
{
	public class DBOMaker
	{
		private static Cache cache = new Cache();
		private static object CreateObject(string path, string TypeName)
		{
			object obj = DBOMaker.cache.GetObject(TypeName);
			if (obj == null)
			{
				try
				{
					obj = Assembly.Load(path).CreateInstance(TypeName);
					DBOMaker.cache.SaveCache(TypeName, obj);
				}
				catch (Exception ex)
				{
					string arg_31_0 = ex.Message;
				}
			}
			return obj;
		}
		public static IDbObject CreateDbObj(string dbTypename)
		{
			string typeName = "Maticsoft.DbObjects." + dbTypename + ".DbObject";
			object obj = DBOMaker.CreateObject("Maticsoft.DbObjects", typeName);
			return (IDbObject)obj;
		}
		public static IDbScriptBuilder CreateScript(string dbTypename)
		{
			string typeName = "Maticsoft.DbObjects." + dbTypename + ".DbScriptBuilder";
			object obj = DBOMaker.CreateObject("Maticsoft.DbObjects", typeName);
			return (IDbScriptBuilder)obj;
		}
	}
}
