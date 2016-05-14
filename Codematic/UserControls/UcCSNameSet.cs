using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Codematic.UserControls
{
    public partial class UcCSNameSet : UserControl
    {
        
        public UcCSNameSet()
        {
            InitializeComponent();

            Maticsoft.CmConfig.ModuleSettings setting = Maticsoft.CmConfig.ModuleConfig.GetSettings();
            txtBLL_Prefix.Text = setting.BLLPrefix;
            txtBLL_Suffix.Text = setting.BLLSuffix;
            txtDAL_Prefix.Text = setting.DALPrefix;
            txtDAL_Suffix.Text = setting.DALSuffix;
            txtModel_Prefix.Text = setting.ModelPrefix;
            txtModel_Suffix.Text = setting.ModelSuffix;
            switch (setting.TabNameRule.ToLower())
            {
                case "same":
                    radbtn_Same.Checked = true;
                    break;
                case "lower":
                    radbtn_Lower.Checked = true;
                    break;
                case "upper":
                    radbtn_Upper.Checked = true;
                    break;
                case "firstupper":
                    radbtn_firstUpper.Checked = true;
                    break;
                default:
                    radbtn_Same.Checked = true;
                    break;
            }

        }

      
    }
}
