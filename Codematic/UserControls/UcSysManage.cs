using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Codematic.UserControls
{
    /// <summary>
    /// 系统管理设置
    /// </summary>
    public partial class UcSysManage : UserControl
    {
        string cmcfgfile = Application.StartupPath + @"\cmcfg.ini";
        Maticsoft.Utility.INIFile cfgfile;

        public UcSysManage()
        {
            InitializeComponent();

            if (File.Exists(cmcfgfile))
            {
                cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
                string val = cfgfile.IniReadValue("dbo", "dbosp");
                if (val.Trim() == "1")
                {
                    radbtnDBO_SP.Checked = true;                    
                }
                else
                {
                    radbtnDBO_SQL.Checked = true;
                }
                string val2 = cfgfile.IniReadValue("loginfo", "save");
                if (val2.Trim() == "1")
                {
                    chkLoginfo.Checked = true;
                }
                else
                {
                    chkLoginfo.Checked = false;
                }
            }
        }

        public void SaveDBO()
        {
            cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
            if (radbtnDBO_SP.Checked)
            {
                cfgfile.IniWriteValue("dbo", "dbosp", "1");
            }
            else
            {
                cfgfile.IniWriteValue("dbo", "dbosp", "0");
            }
            if (chkLoginfo.Checked)
            {
                cfgfile.IniWriteValue("loginfo", "save", "1");
            }
            else
            {
                cfgfile.IniWriteValue("loginfo", "save", "0");
            }
        }


    }
}
