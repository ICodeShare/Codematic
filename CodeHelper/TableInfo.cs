using System;
namespace Maticsoft.CodeHelper
{
	[Serializable]
	public class TableInfo
	{
		private string _tabName = "";
		private string _tabUser = "";
		private string _tabType = "";
		private string _tabDate = "";
		public string TabName
		{
			get
			{
				return this._tabName;
			}
			set
			{
				this._tabName = value;
			}
		}
		public string TabUser
		{
			get
			{
				return this._tabUser;
			}
			set
			{
				this._tabUser = value;
			}
		}
		public string TabType
		{
			get
			{
				return this._tabType;
			}
			set
			{
				this._tabType = value;
			}
		}
		public string TabDate
		{
			get
			{
				return this._tabDate;
			}
			set
			{
				this._tabDate = value;
			}
		}
	}
}
