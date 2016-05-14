using System;
using System.Windows.Forms;
namespace Codematic
{
	public static class FormCommon
	{
		public static DbView DbViewForm
		{
			get
			{
				if (Application.OpenForms["DbView"] == null)
				{
					return null;
				}
				return (DbView)Application.OpenForms["DbView"];
			}
		}
		public static string GetDbViewSelServer()
		{
			if (Application.OpenForms["DbView"] == null)
			{
				return "";
			}
			DbView dbView = (DbView)Application.OpenForms["DbView"];
			TreeNode selectedNode = dbView.treeView1.SelectedNode;
			if (selectedNode == null)
			{
				return "";
			}
			string result = "";
			string key;
			switch (key = selectedNode.Tag.ToString())
			{
			case "serverlist":
				return "";
			case "server":
				result = selectedNode.Text;
				break;
			case "db":
				result = selectedNode.Parent.Text;
				break;
			case "tableroot":
			case "viewroot":
				result = selectedNode.Parent.Parent.Text;
				break;
			case "table":
			case "view":
			case "proc":
				result = selectedNode.Parent.Parent.Parent.Text;
				break;
			case "column":
				result = selectedNode.Parent.Parent.Parent.Parent.Text;
				break;
			}
			return result;
		}
	}
}
