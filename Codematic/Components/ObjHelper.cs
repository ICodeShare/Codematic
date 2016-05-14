using Maticsoft.CmConfig;
using Maticsoft.CodeBuild;
using Maticsoft.DBFactory;
using Maticsoft.IDBO;
using System;
namespace Codematic
{
	internal class ObjHelper
	{
		public static IDbObject CreatDbObj(string longservername)
		{
			DbSettings setting = DbConfig.GetSetting(longservername);
			IDbObject dbObject = DBOMaker.CreateDbObj(setting.DbType);
			dbObject.DbConnectStr = setting.ConnectStr;
			return dbObject;
		}
		public static IDbScriptBuilder CreatDsb(string longservername)
		{
			DbSettings setting = DbConfig.GetSetting(longservername);
			IDbScriptBuilder dbScriptBuilder = DBOMaker.CreateScript(setting.DbType);
			dbScriptBuilder.DbConnectStr = setting.ConnectStr;
			return dbScriptBuilder;
		}
		public static CodeBuilders CreatCB(string longservername)
		{
			return new CodeBuilders(ObjHelper.CreatDbObj(longservername));
		}
	}
}
