using System;
namespace LTP.ConvertCS2VB
{
	public class StructureBlockManager : ClassBlockManager
	{
		public string GetBlock(string tcDeclaration, string tcBlock)
		{
			return base.GetClassBlock(tcDeclaration, tcBlock);
		}
		protected override string Execute()
		{
			string text = this.BlankToken + this.ClassDeclarationToken + this.ImplementationDeclationToken;
			string text2 = text;
			return string.Concat(new string[]
			{
				text2,
				this.ClassBlockToken,
				"\r\n",
				this.BlankToken,
				"End Structure\r\n"
			});
		}
	}
}
