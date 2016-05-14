using System;
using System.Windows.Forms;
namespace Codematic
{
	public class CMTreeNode : TreeNode
	{
		public enum NodeType
		{
			Empty = -1,
			ServerList,
			Server,
			Database,
			TableRoot,
			ViewRoot,
			StoredProcedureRoot,
			FunctionRoot,
			Table,
			View,
			StoredProcedure,
			Function,
			Filed,
			Triggers,
			Trigger,
			Project,
			Unknown
		}
		public string nodeName;
		public string server;
		public string dbname;
		public string dbtype;
		public string nodetype;
		public CMTreeNode(string nodeName, string nodetype, string server, string dbname, string dbtype)
		{
			base.Text = nodeName;
			this.nodeName = nodeName;
			this.nodetype = nodetype;
			this.server = server;
			this.dbname = dbname;
			this.dbtype = dbtype;
		}
	}
}
