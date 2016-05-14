using System;
namespace Maticsoft.CodeHelper
{
	public class DbObjectTool
	{
        /// <summary>
        /// ÐèÒªÐÞ¸Ä
        /// </summary>
        /// <param name="DefaultVal"></param>
        /// <returns></returns>
		public static string DefaultValToCS(string DefaultVal)
		{
   //         DefaultVal.Substring(0, 2) == "N'";
			//DefaultVal == "N'";
            return DefaultVal;
		}
	}
}
