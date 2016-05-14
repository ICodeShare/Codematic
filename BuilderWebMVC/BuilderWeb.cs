using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Maticsoft.IDBO;
using Maticsoft.Utility;
using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;

namespace Maticsoft.BuilderWebMVC
{
    /// <summary>
    /// 修改
    /// </summary>
    public class BuilderWeb : IBuilderWeb
    {
        #region 私有字段
        protected string _key = "ID";//默认第一个主键字段		
        protected string _keyType = "int";//默认第一个主键类型        
        protected string _namespace = "Maticsoft"; //默认顶级命名空间名
        private string _folder = "";//所在文件夹
        protected string _modelname; //model类名           
        protected string _bllname; //model类名
        protected List<ColumnInfo> _fieldlist;
        protected List<ColumnInfo> _keys;
        #endregion

        #region 公有属性
        /// <summary>
        /// 顶级命名空间名 
        /// </summary>        
        public string NameSpace
        {
            set { _namespace = value; }
            get { return _namespace; }
        }
        /// <summary>
        /// 所在文件夹名
        /// </summary>
        public string Folder
        {
            set { _folder = value; }
            get { return _folder; }
        }
        /// <summary>
        /// Model类名
        /// </summary>
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }
        /// <summary>
        /// BLL类名
        /// </summary>
        public string BLLName
        {
            set { _bllname = value; }
            get { return _bllname; }
        }

        /// <summary>
        /// 实体类的整个命名空间+类名
        /// </summary>
        public string ModelSpace
        {
            get
            {
                string _modelspace = _namespace + "." + "Model";
                if (_folder.Trim() != "")
                {
                    _modelspace += "." + _folder;
                }
                _modelspace += "." + ModelName;
                return _modelspace;
            }
        }

        /// <summary>
        /// 业务逻辑层的操作类名称定义
        /// </summary>
        private string BLLSpace
        {
            get
            {
                string _bllspace = _namespace + "." + "BLL";
                if (_folder.Trim() != "")
                {
                    _bllspace += "." + _folder;
                }
                _bllspace += "." + BLLName;
                return _bllspace;
            }
        }
        /// <summary>
        /// 选择的字段集合
        /// </summary>
        public List<ColumnInfo> Fieldlist
        {
            set { _fieldlist = value; }
            get { return _fieldlist; }
        }
        /// <summary>
        /// 主键或条件字段列表 
        /// </summary>
        public List<ColumnInfo> Keys
        {
            set { _keys = value; }
            get { return _keys; }
        }
        /// <summary>
        /// 主键标识字段
        /// </summary>
        protected string Key
        {
            get
            {
                foreach (ColumnInfo key in _keys)
                {
                    _key = key.ColumnName;
                    _keyType = key.TypeName;
                    if (key.IsIdentity)
                    {
                        _key = key.ColumnName;
                        _keyType = CodeCommon.DbTypeToCS(key.TypeName);
                        break;
                    }
                }
                return _key;
            }
        }
        #endregion

        #region Aspx页面html

        /// <summary>
        /// 得到表示层增加窗体的html代码
        /// </summary>      
        public string GetAddAspx()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            strclass.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.Description;
                bool ispk = field.IsPrimaryKey;
                bool IsIdentity = field.IsIdentity;
                if ((ispk) || (IsIdentity))
                {
                    continue;
                }
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                strclass.AppendSpaceLine(1, "<tr>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                strclass.AppendSpaceLine(2, deText);
                strclass.AppendSpaceLine(1, "：</td>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                switch (columnType.Trim().ToLower())
                {
                    case "datetime":
                    case "smalldatetime":
                        strclass.AppendSpaceLine(2, "<INPUT onselectstart=\"return false;\" onkeypress=\"return false\" id=\"txt" + columnName + "\" onfocus=\"setday(this)\"");
                        strclass.AppendSpaceLine(2, " readOnly type=\"text\" size=\"10\" name=\"Text1\" runat=\"server\">");
                        break;
                    case "bit":
                        strclass.AppendSpaceLine(2, "<asp:CheckBox ID=\"chk" + columnName + "\" Text=\"" + deText + "\" runat=\"server\" Checked=\"False\" />");
                        break;
                    default:
                        strclass.AppendSpaceLine(2, "<asp:TextBox id=\"txt" + columnName + "\" runat=\"server\" Width=\"200px\"></asp:TextBox>");
                        break;
                }
                strclass.AppendSpaceLine(1, "</td></tr>");
            }

            //按钮
            strclass.AppendSpaceLine(1, "<tr>");
            strclass.AppendSpaceLine(1, "<td height=\"25\" colspan=\"2\"><div align=\"center\">");
            strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnAdd\" runat=\"server\" Text=\"· 提交 ·\" OnClick=\"btnAdd_Click\" ></asp:Button>");
            //strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnCancel\" runat=\"server\" Text=\"· 重填 ·\" OnClick=\"btnCancel_Click\" ></asp:Button>");
            strclass.AppendSpaceLine(1, "</div></td></tr>");
            strclass.AppendLine("</table>");
            return strclass.ToString();

        }

        /// <summary>
        /// 得到表示层增加窗体的html代码
        /// </summary>      
        public string GetUpdateAspx()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine("");
            strclass.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.Description;
                bool ispk = field.IsPrimaryKey;
                bool IsIdentity = field.IsIdentity;
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                if ((ispk) || (IsIdentity))
                {
                    strclass.AppendSpaceLine(1, "<tr>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                    strclass.AppendSpaceLine(2, deText);
                    strclass.AppendSpaceLine(1, "：</td>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                    strclass.AppendSpaceLine(2, "<asp:label id=\"lbl" + columnName + "\" runat=\"server\"></asp:label>");
                    strclass.AppendSpaceLine(1, "</td></tr>");
                }
                else
                {
                    //
                    strclass.AppendSpaceLine(1, "<tr>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                    strclass.AppendSpaceLine(2, deText);
                    strclass.AppendSpaceLine(1, "：</td>");
                    strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                    switch (columnType.Trim())
                    {
                        case "datetime":
                        case "smalldatetime":
                            strclass.AppendSpaceLine(2, "<INPUT onselectstart=\"return false;\" onkeypress=\"return false\" id=\"txt" + columnName + "\" onfocus=\"setday(this)\"");
                            strclass.AppendSpaceLine(2, " readOnly type=\"text\" size=\"10\" name=\"Text1\" runat=\"server\">");
                            break;
                        case "bit":
                            strclass.AppendSpaceLine(2, "<asp:CheckBox ID=\"chk" + columnName + "\" Text=\"" + deText + "\" runat=\"server\" Checked=\"False\" />");
                            break;
                        default:
                            strclass.AppendSpaceLine(2, "<asp:TextBox id=\"txt" + columnName + "\" runat=\"server\" Width=\"200px\"></asp:TextBox>");
                            break;
                    }
                    strclass.AppendSpaceLine(1, "</td></tr>");
                }
            }

            //按钮
            strclass.AppendSpaceLine(1, "<tr>");
            strclass.AppendSpaceLine(1, "<td height=\"25\" colspan=\"2\"><div align=\"center\">");
            strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnAdd\" runat=\"server\" Text=\"· 提交 ·\" OnClick=\"btnAdd_Click\" ></asp:Button>");
            //strclass.AppendSpaceLine(2, "<asp:Button ID=\"btnCancel\" runat=\"server\" Text=\"· 取消 ·\" OnClick=\"btnCancel_Click\" ></asp:Button>");
            strclass.AppendSpaceLine(1, "</div></td></tr>");
            strclass.AppendLine("</table>");
            return strclass.Value;

        }

        /// <summary>
        /// 得到表示层显示窗体的html代码
        /// </summary>     
        public string GetShowAspx()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            strclass.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.Description;
                if (deText.Trim() == "")
                {
                    deText = columnName;
                }
                strclass.AppendSpaceLine(1, "<tr>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
                strclass.AppendSpaceLine(2, deText);
                strclass.AppendSpaceLine(1, "：</td>");
                strclass.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
                switch (columnType.Trim())
                {
                    case "bit":
                        strclass.AppendSpaceLine(2, "<asp:CheckBox ID=\"chk" + columnName + "\" Text=\"" + deText + "\" runat=\"server\" Checked=\"False\" />");
                        break;
                    default:
                        strclass.AppendSpaceLine(2, "<asp:Label id=\"lbl" + columnName + "\" runat=\"server\"></asp:Label>");
                        break;
                }
                strclass.AppendSpaceLine(1, "</td></tr>");
            }
            strclass.AppendLine("</table>");
            return strclass.ToString();

        }

        /// <summary>
        /// 增删改3个页面代码
        /// </summary>      
        public string GetWebHtmlCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm)
        {
            StringPlus strclass = new StringPlus();
            if (AddForm)
            {
                strclass.AppendLine(" <!--******************************增加页面代码********************************-->");
                strclass.AppendLine(GetAddAspx());
            }
            if (UpdateForm)
            {
                strclass.AppendLine(" <!--******************************修改页面代码********************************-->");
                strclass.AppendLine(GetUpdateAspx());
            }
            if (ShowForm)
            {
                strclass.AppendLine("  <!--******************************显示页面代码********************************-->");
                strclass.AppendLine(GetShowAspx());
            }
            return strclass.ToString();
        }
        #endregion

        #region 表示层 CS

        /// <summary>
        /// 生成表示层页面的CS代码
        /// </summary>
        /// <param name="ExistsKey"></param>
        /// <param name="AddForm">是否生成增加窗体的代码</param>
        /// <param name="UpdateForm">是否生成修改窗体的代码</param>
        /// <param name="ShowForm">是否生成显示窗体的代码</param>
        /// <param name="SearchForm">是否生成查询窗体的代码</param>
        /// <returns></returns>
        public string GetWebCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm)
        {
            StringPlus strclass = new StringPlus();
            if (AddForm)
            {
                strclass.AppendLine("  /******************************增加窗体代码********************************/");
                strclass.AppendLine(GetAddAspxCs());
            }
            if (UpdateForm)
            {
                strclass.AppendLine("  /******************************修改窗体代码********************************/");
                strclass.AppendLine("  /*修改代码-显示 */");
                strclass.AppendLine(GetUpdateShowAspxCs());
                strclass.AppendLine("  /*修改代码-提交更新 */");
                strclass.AppendLine(GetUpdateAspxCs());
            }
            if (ShowForm)
            {
                strclass.AppendLine("  /******************************显示窗体代码********************************/");
                strclass.AppendLine(GetShowAspxCs());
            }
            //if (DelForm)
            //{
            //    strclass.Append("  /******************************删除窗体代码********************************/" );
            //    strclass.Append("");
            //    strclass.Append(CreatDeleteForm() );
            //}
            return strclass.Value;
        }

        /// <summary>
        /// 得到表示层增加窗体的代码
        /// </summary>      
        public string GetAddAspxCs()
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass0 = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(3, "string strErr=\"\";");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.Description;
                bool ispk = field.IsPrimaryKey;
                bool IsIdentity = field.IsIdentity;
                if ((ispk) || (IsIdentity))
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                        strclass0.AppendSpaceLine(3, "int " + columnName + "=int.Parse(this.txt" + columnName + ".Text);");
                        strclass1.AppendSpaceLine(3, "if(!PageValidate.IsNumber(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不是数字！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                    case "float":
                    case "numeric":
                    case "decimal":
                        strclass0.AppendSpaceLine(3, "decimal " + columnName + "=decimal.Parse(this.txt" + columnName + ".Text);");
                        strclass1.AppendSpaceLine(3, "if(!PageValidate.IsDecimal(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不是数字！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                    case "datetime":
                    case "smalldatetime":
                        strclass0.AppendSpaceLine(3, "DateTime " + columnName + "=DateTime.Parse(this.txt" + columnName + ".Text);");
                        strclass1.AppendSpaceLine(3, "if(!PageValidate.IsDateTime(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不是时间格式！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                    case "bool":
                        strclass0.AppendSpaceLine(3, "bool " + columnName + "=this.chk" + columnName + ".Checked;");
                        break;
                    case "byte[]":
                        strclass0.AppendSpaceLine(3, "byte[] " + columnName + "= new UnicodeEncoding().GetBytes(this.txt" + columnName + ".Text);");
                        break;
                    default:
                        strclass0.AppendSpaceLine(3, "string " + columnName + "=this.txt" + columnName + ".Text;");
                        strclass1.AppendSpaceLine(3, "if(this.txt" + columnName + ".Text ==\"\")");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不能为空！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                }
                strclass2.AppendSpaceLine(3, "model." + columnName + "=" + columnName + ";");
            }
            strclass.AppendLine(strclass1.ToString());
            strclass.AppendSpaceLine(3, "if(strErr!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "MessageBox.Show(this,strErr);");
            strclass.AppendSpaceLine(4, "return;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendLine(strclass0.ToString());
            strclass.AppendSpaceLine(3, ModelSpace + " model=new " + ModelSpace + "();");
            strclass.AppendLine(strclass2.ToString());
            strclass.AppendSpaceLine(3, BLLSpace + " bll=new " + BLLSpace + "();");
            strclass.AppendSpaceLine(3, "bll.Add(model);");
            return strclass.Value;
        }

        /// <summary>
        /// 得到修改窗体的代码
        /// </summary>      
        public string GetUpdateAspxCs()
        {
            StringPlus strclass = new StringPlus();
            StringPlus strclass0 = new StringPlus();
            StringPlus strclass1 = new StringPlus();
            StringPlus strclass2 = new StringPlus();
            strclass.AppendLine();
            strclass.AppendSpaceLine(3, "string strErr=\"\";");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                bool ispk = field.IsPrimaryKey;
                bool IsIdentity = field.IsIdentity;
                if ((ispk) || (IsIdentity))
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                        strclass0.AppendSpaceLine(3, "int " + columnName + "=int.Parse(this.txt" + columnName + ".Text);");
                        strclass1.AppendSpaceLine(3, "if(!PageValidate.IsNumber(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不是数字！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                    case "float":
                    case "numeric":
                    case "decimal":
                        strclass0.AppendSpaceLine(3, "decimal " + columnName + "=decimal.Parse(this.txt" + columnName + ".Text);");
                        strclass1.AppendSpaceLine(3, "if(!PageValidate.IsDecimal(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不是数字！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                    case "datetime":
                    case "smalldatetime":
                        strclass0.AppendSpaceLine(3, "DateTime " + columnName + "=DateTime.Parse(this.txt" + columnName + ".Text);");
                        strclass1.AppendSpaceLine(3, "if(!PageValidate.IsDateTime(txt" + columnName + ".Text))");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不是时间格式！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                    case "bool":
                        strclass0.AppendSpaceLine(3, "bool " + columnName + "=this.chk" + columnName + ".Checked;");
                        break;
                    case "byte[]":
                        strclass0.AppendSpaceLine(3, "byte[] " + columnName + "= new UnicodeEncoding().GetBytes(this.txt" + columnName + ".Text);");
                        break;
                    default:
                        strclass0.AppendSpaceLine(3, "string " + columnName + "=this.txt" + columnName + ".Text;");
                        strclass1.AppendSpaceLine(3, "if(this.txt" + columnName + ".Text ==\"\")");
                        strclass1.AppendSpaceLine(3, "{");
                        strclass1.AppendSpaceLine(4, "strErr+=\"" + columnName + "不能为空！\\\\n\";	");
                        strclass1.AppendSpaceLine(3, "}");
                        break;
                }
                strclass2.AppendSpaceLine(3, "model." + columnName + "=" + columnName + ";");

            }
            strclass.AppendLine(strclass1.ToString());
            strclass.AppendSpaceLine(3, "if(strErr!=\"\")");
            strclass.AppendSpaceLine(3, "{");
            strclass.AppendSpaceLine(4, "MessageBox.Show(this,strErr);");
            strclass.AppendSpaceLine(4, "return;");
            strclass.AppendSpaceLine(3, "}");
            strclass.AppendLine(strclass0.ToString());
            strclass.AppendLine();
            strclass.AppendSpaceLine(3, ModelSpace + " model=new " + ModelSpace + "();");
            strclass.AppendLine(strclass2.ToString());
            strclass.AppendSpaceLine(3, BLLSpace + " bll=new " + BLLSpace + "();");
            strclass.AppendSpaceLine(3, "bll.Update(model);");
            return strclass.ToString();
        }

        /// <summary>
        /// 得到修改窗体的代码
        /// </summary>       
        public string GetUpdateShowAspxCs()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            string key = Key;
            strclass.AppendSpaceLine(1, "private void ShowInfo(" + CodeCommon.GetInParameter(Keys,true) + ")");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendSpaceLine(2, BLLSpace + " bll=new " + BLLSpace + "();");
            strclass.AppendSpaceLine(2, ModelSpace + " model=bll.GetModel(" + CodeCommon.GetFieldstrlist(Keys,true) + ");");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.Description;
                bool ispk = field.IsPrimaryKey;
                bool IsIdentity = field.IsIdentity;

                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                    case "float":
                    case "numeric":
                    case "decimal":
                    case "datetime":
                    case "smalldatetime":
                        if ((ispk) || (IsIdentity))
                        {
                            strclass.AppendSpaceLine(2, "this.lbl" + columnName + ".Text=model." + columnName + ".ToString();");
                        }
                        else
                        {
                            strclass.AppendSpaceLine(2, "this.txt" + columnName + ".Text=model." + columnName + ".ToString();");
                        }
                        break;
                    case "bool":
                        strclass.AppendSpaceLine(2, "this.chk" + columnName + ".Checked=model." + columnName + ";");
                        break;
                    case "byte[]":
                        strclass.AppendSpaceLine(2, "this.txt" + columnName + ".Text=model." + columnName + ".ToString();");
                        break;
                    default:
                        if ((ispk) || (IsIdentity))
                        {
                            strclass.AppendSpaceLine(2, "this.lbl" + columnName + ".Text=model." + columnName + ";");
                        }
                        else
                        {
                            strclass.AppendSpaceLine(2, "this.txt" + columnName + ".Text=model." + columnName + ";");
                        }
                        break;
                }
            }
            strclass.AppendLine();
            strclass.AppendSpaceLine(1, "}");
            return strclass.Value;
        }


        /// <summary>
        /// 得到表示层显示窗体的代码
        /// </summary>       
        public string GetShowAspxCs()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendLine();
            string key = Key;
            strclass.AppendSpaceLine(1, "private void ShowInfo(" + CodeCommon.GetInParameter(Keys,true) + ")");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendSpaceLine(2, BLLSpace + " bll=new " + BLLSpace + "();");
            strclass.AppendSpaceLine(2, ModelSpace + " model=bll.GetModel(" +CodeCommon.GetFieldstrlist(Keys,true) + ");");
            foreach (ColumnInfo field in Fieldlist)
            {
                string columnName = field.ColumnName;
                string columnType = field.TypeName;
                string deText = field.Description;
                bool ispk = field.IsPrimaryKey;
                bool IsIdentity = field.IsIdentity;
                if ((ispk) || (IsIdentity))
                {
                    continue;
                }
                switch (CodeCommon.DbTypeToCS(columnType.Trim().ToLower()).ToLower())
                {
                    case "int":
                    case "smallint":
                    case "float":
                    case "numeric":
                    case "decimal":
                    case "datetime":
                    case "smalldatetime":
                        strclass.AppendSpaceLine(2, "this.lbl" + columnName + ".Text=model." + columnName + ".ToString();");
                        break;
                    case "bool":
                        strclass.AppendSpaceLine(2, "this.chk" + columnName + ".Checked=model." + columnName + ";");
                        break;
                    case "byte[]":
                        strclass.AppendSpaceLine(2, "this.lbl" + columnName + ".Text=model." + columnName + ".ToString();");
                        break;
                    default:
                        strclass.AppendSpaceLine(2, "this.lbl" + columnName + ".Text=model." + columnName + ";");
                        break;
                }
            }
            strclass.AppendLine();
            strclass.AppendSpaceLine(1, "}");
            return strclass.ToString();
        }

        /// <summary>
        /// 删除页面
        /// </summary>
        /// <returns></returns>
        public string CreatDeleteForm()
        {
            StringPlus strclass = new StringPlus();
            strclass.AppendSpaceLine(1, "if(!Page.IsPostBack)");
            strclass.AppendSpaceLine(1, "{");
            strclass.AppendSpaceLine(2, BLLSpace + " bll=new " + BLLSpace + "();");
            switch (_keyType.Trim())
            {
                case "int":
                case "smallint":
                case "float":
                case "numeric":
                case "decimal":
                case "datetime":
                case "smalldatetime":
                    strclass.AppendSpaceLine(2, _keyType + " " + _key + "=" + _keyType + ".Parse(Request.Params[\"id\"]);");
                    break;
                default:
                    strclass.AppendSpaceLine(2, "string " + _key + "=Request.Params[\"id\"];");
                    break;
            }
            strclass.AppendSpaceLine(2, "bll.Delete(" + _key + ");");
            strclass.AppendSpaceLine(2, "Response.Redirect(\"index.aspx\");");
            strclass.AppendSpaceLine(1, "}");
            return strclass.Value;
        }

        public string CreatSearchForm()
        {
            StringPlus strclass = new StringPlus();
            return strclass.Value;
        }        
        #endregion//表示层

        #region  生成aspx.designer.cs
        /// <summary>
        /// 增加窗体的html代码
        /// </summary>      
        public string GetAddDesigner()
        {
            StringPlus strclass = new StringPlus();            
            return strclass.ToString();
        }

        /// <summary>
        /// 修改窗体的html代码
        /// </summary>      
        public string GetUpdateDesigner()
        {
            StringPlus strclass = new StringPlus();            
            return strclass.Value;
        }

        /// <summary>
        /// 显示窗体的html代码
        /// </summary>     
        public string GetShowDesigner()
        {
            StringPlus strclass = new StringPlus();            
            return strclass.ToString();
        }
        #endregion


        #region 新增
        private bool isFilterColume(string columnName)
        {
            return false;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public string GetListAspx()
        {
            StringPlus stringPlus = new StringPlus();
            stringPlus.AppendLine();
            foreach (ColumnInfo current in this.Fieldlist)
            {
                string columnName = current.ColumnName;
                string typeName = current.TypeName;
                string text = current.Description;
                bool arg_43_0 = current.IsPrimaryKey;
                bool isIdentity = current.IsIdentity;
                text = CodeCommon.CutDescText(text, 15, columnName);
                if (!isIdentity && !this.isFilterColume(columnName))
                {
                    string a;
                    if ((a = typeName.Trim().ToLower()) != null && (a == "bit" || a == "dateTime"))
                    {
                        stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"<asp:BoundField DataField=\"",
							columnName,
							"\" HeaderText=\"",
							text,
							"\" SortExpression=\"",
							columnName,
							"\" ItemStyle-HorizontalAlign=\"Center\"  /> "
						}));
                    }
                    else
                    {
                        stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"<asp:BoundField DataField=\"",
							columnName,
							"\" HeaderText=\"",
							text,
							"\" SortExpression=\"",
							columnName,
							"\" ItemStyle-HorizontalAlign=\"Center\"  /> "
						}));
                    }
                }
            }
            return stringPlus.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDeleteAspxCs()
        {
            StringPlus stringPlus = new StringPlus();
            stringPlus.AppendSpaceLine(1, "if(!Page.IsPostBack)");
            stringPlus.AppendSpaceLine(2, "{");
            stringPlus.AppendSpaceLine(3, this.BLLSpace + " bll=new " + this.BLLSpace + "();");
            string key;
            switch (key = this._keyType.Trim())
            {
                case "int":
                case "long":
                case "smallint":
                case "float":
                case "numeric":
                case "decimal":
                case "datetime":
                case "smalldatetime":
                    stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					this._keyType,
					" ",
					this._key,
					"=",
					this._keyType,
					".Parse(Request.Params[\"id\"]);"
				}));
                    goto IL_16B;
            }
            stringPlus.AppendSpaceLine(3, "string " + this._key + "=Request.Params[\"id\"];");
        IL_16B:
            stringPlus.AppendSpaceLine(3, "bll.Delete(" + this._key + ");");
            stringPlus.AppendSpaceLine(3, "Response.Redirect(\"list.aspx\");");
            stringPlus.AppendSpaceLine(2, "}");
            return stringPlus.Value;
        }

        public string GetListAspxCs()
        {
            StringPlus stringPlus = new StringPlus();
            stringPlus.AppendLine();
            stringPlus.AppendSpace(2, this.BLLSpace + " bll = new " + this.BLLSpace + "();");
            return stringPlus.ToString();
        }

        public string GetListDesigner()
        {
            StringPlus stringPlus = new StringPlus();
            return stringPlus.ToString();
        }

        #endregion
    }
}
