using System;
namespace Maticsoft.IBuilder
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public class IBuilderAttribute : Attribute
	{
		private string _guid;
		private string _name;
		private string _desc;
		private string _assembly;
		private string _classname;
		private string _version;
		public string Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				this._guid = value;
			}
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string Decription
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}
		public string Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}
		public string Classname
		{
			get
			{
				return this._classname;
			}
			set
			{
				this._classname = value;
			}
		}
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}
	}
}
