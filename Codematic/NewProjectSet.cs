using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Codematic
{
    public partial class NewProjectSet : Form
    {
        private string _outpath;
        private string _proname;
        private string _framework;
        /// <summary>
        /// 项目输出路径
        /// </summary>
        public string OutPath
        {
            set 
            {
                _outpath = value;
            }
            get 
            {
                return _outpath;
            }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProName
        {
            set
            {
                _proname = value;
            }
            get
            {
                return _proname;
            }
        }

        /// <summary>
        /// 架构类型
        /// </summary>
        public string FrameWork
        {
            set
            {
                _framework = value;
            }
            get
            {
                return _framework;
            }
        }

        Maticsoft.CodeBuild.CodeBuilders cb;//代码生成对象        
        Hashtable TabSelList;//选择的所有表
        public NewProjectSet(Maticsoft.CodeBuild.CodeBuilders codebuild,Hashtable Tablist)
        {
            InitializeComponent();
            cb = codebuild;
            TabSelList = Tablist;
        }
        public NewProjectSet(Maticsoft.CodeBuild.CodeBuilders codebuild, Hashtable Tablist,string outpath,string proname)
        {
            InitializeComponent();
            cb = codebuild;
            TabSelList = Tablist;
            _outpath = outpath;
            _proname = proname;
        }

        #region 按钮

        private void btn_Pri_Click(object sender, EventArgs e)
        {

        }

        private void btn_Build_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}