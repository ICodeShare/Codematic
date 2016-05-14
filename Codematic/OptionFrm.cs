using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Codematic.UserControls;
namespace Codematic
{
    public partial class OptionFrm : Form
    {

        private UcOptionsEnviroments optionsEnviroments ;
        private UcOptionEditor optionEditor ;
        private UcOptionsQuerySettings optionsQuerySettings ;
        private UcOptionStartUp optionStartUp ;
        //private UcDBSet ucdbset;
        private UcCSSet uccsset;
        private UcCSNameSet ucnameset;
        private MainForm mainForm;
        UcAddInManage ucAddin;
        UcDatatype ucDatatype;
        UcSysManage ucSysmanage;

        Maticsoft.CmConfig.AppSettings appsettings;
        Maticsoft.CmConfig.ModuleSettings setting;

        public OptionFrm(MainForm mainform)
        {
            InitializeComponent();
            mainForm = mainform;
            appsettings = Maticsoft.CmConfig.AppConfig.GetSettings();
            setting = Maticsoft.CmConfig.ModuleConfig.GetSettings();

            optionsEnviroments = new UcOptionsEnviroments();
            optionEditor = new UcOptionEditor();
            optionsQuerySettings = new UcOptionsQuerySettings();
            optionStartUp = new UcOptionStartUp(appsettings);//5-1-a-s-p-x
            //ucdbset = new UcDBSet();
            uccsset = new UcCSSet();
            ucnameset = new UcCSNameSet();
            ucAddin = new UcAddInManage();
            ucDatatype = new UcDatatype();
            ucSysmanage = new UcSysManage();

        }
        private void OptionFrm_Load(object sender, EventArgs e)
        {
            InitTreeView();
        }
        private void InitTreeView()
        {
            // Top
            TreeNode tnEnviroment = new TreeNode("环境", 0, 1);
            TreeNode tnCodeSet = new TreeNode("代码生成设置", 0, 1);
            TreeNode tnAddIn = new TreeNode("组件管理", 0, 1);
            TreeNode tnDbo = new TreeNode("系统管理", 0, 1);

            #region 环境
            TreeNode tnEditor = new TreeNode("编辑", 2, 3);
            TreeNode tnQuerySettings = new TreeNode("设置", 2, 3);
            TreeNode StartPage = new TreeNode("启动", 2, 3);
            tnEnviroment.Nodes.Add(StartPage);
            //tnEnviroment.Nodes.Add(tnEditor);
            //tnEnviroment.Nodes.Add(tnQuerySettings);
            #endregion

            #region  代码参数
            TreeNode tnDB = new TreeNode("数据库脚本", 2, 3);
            TreeNode tnCS = new TreeNode("基本设置", 2, 3);
            TreeNode tnNameRule = new TreeNode("类命名规则", 2, 3);
            TreeNode tnWeb = new TreeNode("Web页面", 2, 3);
            TreeNode tnDatatype = new TreeNode("字段类型映射", 2, 3);
            //tnCodeSet.Nodes.Add(tnDB);
            tnCodeSet.Nodes.Add(tnCS);
            tnCodeSet.Nodes.Add(tnNameRule);
            //tnCodeSet.Nodes.Add(tnWeb);
            tnCodeSet.Nodes.Add(tnDatatype);
            #endregion

            #region  组件管理
            //TreeNode tnaddin = new TreeNode("DAL代码组件", 2, 3);
            //TreeNode tnaddinbll = new TreeNode("BLL代码组件", 2, 3);
            //TreeNode tnproc = new TreeNode("存储过程代码插件", 2, 3);            
            
            //tnAddIn.Nodes.Add(tnaddin);
            //tnAddIn.Nodes.Add(tnaddinbll);
            #endregion

            this.treeView1.Nodes.Add(tnEnviroment);
            this.treeView1.Nodes.Add(tnCodeSet);
            this.treeView1.Nodes.Add(tnAddIn);
            this.treeView1.Nodes.Add(tnDbo);
            tnEnviroment.Expand();
            tnCodeSet.Expand();

            this.UserControlContainer.Controls.Add(optionsEnviroments);//环境
            this.UserControlContainer.Controls.Add(optionEditor);//编辑
            this.UserControlContainer.Controls.Add(optionsQuerySettings);//设置
            this.UserControlContainer.Controls.Add(optionStartUp);//启动
            //this.UserControlContainer.Controls.Add(ucdbset);
            this.UserControlContainer.Controls.Add(uccsset);//代码生成基本设置
            this.UserControlContainer.Controls.Add(ucnameset);//代码生成基本设置
            this.UserControlContainer.Controls.Add(ucAddin);//组件管理
            this.UserControlContainer.Controls.Add(ucDatatype);//字段类型映射
            this.UserControlContainer.Controls.Add(ucSysmanage);//系统管理

            ActivateOptionControl(optionsEnviroments);

        }
        private void ActivateOptionControl(System.Windows.Forms.UserControl optionControl)
        {
            foreach (UserControl uc in this.UserControlContainer.Controls)
                uc.Hide();
            optionControl.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode != null)
            {
                switch (selectedNode.Text)
                {
                    case "环境":
                        ActivateOptionControl(optionsEnviroments);
                        break;
                    case "编辑":
                        ActivateOptionControl(optionEditor);
                        break;
                    case "设置":
                        ActivateOptionControl(optionsQuerySettings);
                        break;
                    case "启动":
                        ActivateOptionControl(optionStartUp);
                        break;
                    //case "数据库脚本":
                    //    ActivateOptionControl(ucdbset);
                    //    break;
                    case "代码生成设置":                       
                    case "基本设置":
                        ActivateOptionControl(uccsset);
                        break;
                    case "类命名规则":
                        ActivateOptionControl(ucnameset);
                        break;
                    case "字段类型映射":
                        ActivateOptionControl(ucDatatype);
                        break;
                    case "组件管理":
                    case "DAL代码插件":
                        ActivateOptionControl(ucAddin);
                        break;                    
                    case "系统管理":
                        ActivateOptionControl(ucSysmanage);
                        break;
                }
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            #region 启动
            switch (optionStartUp.cmb_StartUpItem.SelectedIndex)
            {
                case 0:
                    appsettings.AppStart = "startuppage";
                    appsettings.StartUpPage = optionStartUp.txtStartUpPage.Text;
                    break;
                case 1:
                    appsettings.AppStart = "blank";
                    break;
                case 2:
                    appsettings.AppStart = "homepage";
                    appsettings.HomePage = optionStartUp.txtStartUpPage.Text;
                    break;
            }
            Maticsoft.CmConfig.AppConfig.SaveSettings(appsettings);
            #endregion

            #region 代码生成设置

            if (uccsset.radbtn_Frame_One.Checked)
            {
                setting.AppFrame = "One";
            }
            if (uccsset.radbtn_Frame_S3.Checked)
            {
                setting.AppFrame = "S3";
            }
            if (uccsset.radbtn_Frame_F3.Checked)
            {
                setting.AppFrame = "F3";
            }

            setting.DALType = uccsset.GetDALType();
            setting.BLLType = uccsset.GetBLLType();
            setting.WebType = uccsset.GetWebType(); 

            setting.Namepace = uccsset.txtNamepace.Text.Trim();
            setting.DbHelperName = uccsset.txtDbHelperName.Text.Trim();
            setting.ProjectName = uccsset.txtProjectName.Text.Trim();
            setting.ProcPrefix = uccsset.txtProcPrefix.Text.Trim();

            setting.BLLPrefix = ucnameset.txtBLL_Prefix.Text.Trim();
            setting.BLLSuffix = ucnameset.txtBLL_Suffix.Text.Trim();
            setting.DALPrefix = ucnameset.txtDAL_Prefix.Text.Trim();
            setting.DALSuffix = ucnameset.txtDAL_Suffix.Text.Trim();
            setting.ModelPrefix = ucnameset.txtModel_Prefix.Text.Trim();
            setting.ModelSuffix = ucnameset.txtModel_Suffix.Text.Trim();

            #region 表命名规则
            if (ucnameset.radbtn_Same.Checked)
            {
                setting.TabNameRule = "same";
            }
            if (ucnameset.radbtn_Lower.Checked)
            {
                setting.TabNameRule = "lower";
            }
            if (ucnameset.radbtn_Upper.Checked)
            {
                setting.TabNameRule = "upper";
            }
            if (ucnameset.radbtn_firstUpper.Checked)
            {
                setting.TabNameRule = "firstupper";
            }
            #endregion

            Maticsoft.CmConfig.ModuleConfig.SaveSettings(setting);
            #endregion

            #region 字段类型映射
            //保存字段映射配置文件
            ucDatatype.SaveIni();
            ucSysmanage.SaveDBO();
            #endregion
      
            Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}