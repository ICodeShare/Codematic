using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Maticsoft.Utility;
namespace Codematic.UserControls
{
    /// <summary>
    /// 字段类型映射
    /// </summary>
    public partial class UcDatatype : UserControl
    {
        string filename = Application.StartupPath + "\\datatype.ini";
        INIFile datatype;

        public UcDatatype()
        {
            InitializeComponent();
            LoadSection();
            datatype = new INIFile(filename);
            cmb_Section.SelectedIndex = 0;
       
            //StreamReader srFile = new StreamReader(filename, Encoding.Default);
            //string strContents = srFile.ReadToEnd();
            //srFile.Close();
            //txtIniDatatype.Text = strContents;
        }

        private void LoadSection()
        {
            cmb_Section.Items.Add("DbToCS");
            cmb_Section.Items.Add("ToSQLProc");
            cmb_Section.Items.Add("ToOraProc");
            cmb_Section.Items.Add("ToOleDbProc");
            cmb_Section.Items.Add("IsAddMark");
            cmb_Section.Items.Add("isValueType");
            cmb_Section.Items.Add("AccessDbTypeMap");
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public void SaveIni()
        {
            //string[] strArray = txtIniDatatype.Lines;
            //StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);
            //for (int n = 0; n < strArray.Length; n++)
            //{
            //    if (strArray[n].Trim() != "")
            //    {
            //        sw.WriteLine(strArray[n]);
            //    }
            //}
            //sw.Close();

            if (cmb_Section.SelectedItem != null)
            {
                string section = cmb_Section.Text;
                string[] strArray = txtIniDatatype.Lines;
                datatype.ClearSection(section);
                for (int n = 0; n < strArray.Length; n++)
                {
                    if (strArray[n].Trim() != "")
                    {
                        string temp = strArray[n].Trim();
                        int m = temp.IndexOf("=");
                        string key = temp.Substring(0, m);
                        string val = temp.Substring(m + 1);
                        datatype.IniWriteValue(section, key, val);
                    }
                }
            }

        }

        private void cmb_Section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Section.SelectedItem != null)
            {
                string type = cmb_Section.Text;
                if ((type != "") && (type != "System.Data.DataRowView"))
                {
                    switch (type)
                    {
                        case "DbToCS":
                            label1.Text = "数据库字段类型对应的C#类型";
                            break;
                        case "ToSQLProc":
                            label1.Text = "数据库字段类型对应的SQLserver的SqlDbType字段类型";
                            break;
                        case "ToOraProc":
                            label1.Text = "数据库字段类型对应的Oracle的OracleType字段类型";
                            break;
                        case "ToOleDbProc":
                            label1.Text = "数据库字段类型对应的OleDb的OleDbType字段类型";
                            break;
                        case "IsAddMark":
                            label1.Text = "字段类型是否需要单引号括起来";
                            break;
                        case "isValueType":
                            label1.Text = "是否是C#的值（可空）类型";
                            break;
                        case "AccessDbTypeMap":
                            label1.Text = "Access字段类型索引对应的SQL数据库字段类型";
                            break;
                    }                 
                                        
                    string[] strkeys = datatype.IniReadValues(type);
                    StringPlus strSection = new StringPlus();
                    foreach(string key in strkeys)
                    {
                        if (key.Trim() != "")
                        {
                            string val = datatype.IniReadValue(type, key);
                            strSection.AppendLine(key + "=" + val);
                        }
                    }
                    txtIniDatatype.Text = strSection.ToString();
                       
                }
            }
        }
    }
}
