using System;
namespace LTP.ConvertCS2VB
{
	public class NameSpaceManager : BaseManager
	{
		public string GetNameSpaceBlock(string tcNameSpace, string tcBlock)
		{
			base.GetBlankToken(tcNameSpace);
			string str = this.BlankToken + tcNameSpace.Trim().Replace("namespace", "Namespace") + "\r\n";
			str += base.ExtractBlock(tcBlock, "{", "}");
			return str + "\r\n" + this.BlankToken + "End Namespace\r\n";
		}
	}
}
