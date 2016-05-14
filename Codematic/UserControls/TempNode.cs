using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Codematic.UserControls
{
    public class TempNode:TreeNode
    {
        private string nodeid;
        private string filepath;
        private string nodetype;
        private string parentid;

        public string NodeID
        {
            set { nodeid = value; }
            get { return nodeid; }
        }
        public string FilePath
        {
            set { filepath = value; }
            get { return filepath; }
        }
        public string NodeType
        {
            set { nodetype = value; }
            get { return nodetype; }
        }
        public string ParentID
        {
            set { parentid = value; }
            get { return parentid; }
        }



        public TempNode()
        { 
        }
        public TempNode(string NodeName)
        {
            Text = NodeName;
        }
       
    }
}
