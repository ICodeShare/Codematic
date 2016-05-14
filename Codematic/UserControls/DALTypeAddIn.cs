using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Maticsoft.AddInManager;
namespace Codematic.UserControls
{
    /// <summary>
    /// 插件列表控件
    /// </summary>
    public partial class DALTypeAddIn : UserControl
    {
        
        private string _name="";
        private string _guid="";
        
        public string Name
        {
            set { _name = value; }
            get { return _name; }
 
        }
        public string AppGuid
        {
            set { _guid = value; }
            get { return _guid; }
        }
        public string Title
        {
            set { lblTitle.Text = value; }
            get { return lblTitle.Text; }

        }
        public DALTypeAddIn()
        {
            InitializeComponent();
            
            #region 加载插件   
            AddIn addin = new AddIn();
            DataSet dsAddin = addin.GetAddInList();
            if (dsAddin != null)
            {
                int c = dsAddin.Tables[0].Rows.Count;
                if (c > 0)
                {                    
                    cmbox_DALType.DataSource = dsAddin.Tables[0].DefaultView;
                    cmbox_DALType.ValueMember = "Guid";
                    cmbox_DALType.DisplayMember = "Name";                    
                }
                if (c == 1)
                {
                    _guid = dsAddin.Tables[0].Rows[0]["Guid"].ToString();
                    _name = dsAddin.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            #endregion
        }
        public DALTypeAddIn(string InterfaceName)
        {
            InitializeComponent();

            #region 加载插件
            AddIn addin = new AddIn();
            DataSet dsAddin = addin.GetAddInList(InterfaceName);
            if (dsAddin != null)
            {
                int c = dsAddin.Tables[0].Rows.Count;
                if (c > 0)
                {
                    cmbox_DALType.DataSource = dsAddin.Tables[0].DefaultView;
                    cmbox_DALType.ValueMember = "Guid";
                    cmbox_DALType.DisplayMember = "Name";                    
                }
                if (c == 1)
                {
                    _guid=dsAddin.Tables[0].Rows[0]["Guid"].ToString();
                    _name = dsAddin.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            #endregion
        }
        /// <summary>
        /// 选中某项
        /// </summary>
        /// <param name="appguid"></param>
        public void SetSelectedDALType(string appguid)
        {
            for (int i = 0; i < cmbox_DALType.Items.Count; ++i)
            {
                DataRow dr = (cmbox_DALType.Items[i] as DataRowView).Row;
                if (dr[cmbox_DALType.ValueMember].ToString() == appguid)
                {
                    cmbox_DALType.SelectedIndex = i;
                    _guid = appguid;
                    _name = dr[cmbox_DALType.DisplayMember].ToString();
                    AddIn addin = new AddIn(_guid);
                    lblTip.Text = addin.Decription;
                    break;
                }                
            }
        }
        
        private void cmbox_DALType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbox_DALType.SelectedItem != null)
            {
                _name = cmbox_DALType.Text;
                _guid = cmbox_DALType.SelectedValue.ToString();
                if ((_guid != "") && (_guid != "System.Data.DataRowView"))
                {
                    AddIn addin = new AddIn(_guid);
                    lblTip.Text = addin.Decription;
                }                
            }

        }
    }
}
