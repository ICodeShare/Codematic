using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Maticsoft.BuilderModel
{
    /// <summary>
    /// ÐèÒªÐÞ¸Ä
    /// </summary>
    public class BuilderModel : IBuilderModel
    {
        private Dictionary<string, int> dtCollection;
        protected string _modelname = "";
        protected string _namespace = "Maticsoft";
        protected string _modelpath = "";
        protected string _tabledescription = "";
        protected List<ColumnInfo> _fieldlist;
        public string NameSpace
        {
            get
            {
                return this._namespace;
            }
            set
            {
                this._namespace = value;
            }
        }
        public string Modelpath
        {
            get
            {
                return this._modelpath;
            }
            set
            {
                this._modelpath = value;
            }
        }
        public string ModelName
        {
            get
            {
                return this._modelname;
            }
            set
            {
                this._modelname = value;
            }
        }
        public string TableDescription
        {
            get
            {
                return this._tabledescription;
            }
            set
            {
                this._tabledescription = value;
            }
        }
        public List<ColumnInfo> Fieldlist
        {
            get
            {
                return this._fieldlist;
            }
            set
            {
                this._fieldlist = value;
            }
        }
        public Hashtable Languagelist
        {
            get
            {
                return Language.LoadFromCfg("BuilderModel.lan");
            }
        }
        public string CreatModel()
        {
            StringPlus stringPlus = new StringPlus();
            stringPlus.AppendLine("using System;");
            stringPlus.AppendLine("namespace " + this.Modelpath);
            stringPlus.AppendLine("{");
            stringPlus.AppendSpaceLine(1, "/// <summary>");
            if (this.TableDescription.Length > 0)
            {
                stringPlus.AppendSpaceLine(1, "/// " + this.TableDescription.Replace("\r\n", "\r\n\t///"));
            }
            else
            {
                stringPlus.AppendSpaceLine(1, "/// " + this._modelname + ":" + this.Languagelist["summary"].ToString());
            }
            stringPlus.AppendSpaceLine(1, "/// </summary>");
            stringPlus.AppendSpaceLine(1, "[Serializable]");
            stringPlus.AppendSpaceLine(1, "public partial class " + this._modelname);
            stringPlus.AppendSpaceLine(1, "{");
            stringPlus.AppendSpaceLine(2, "public " + this._modelname + "()");
            stringPlus.AppendSpaceLine(2, "{}");
            stringPlus.AppendLine(this.CreatModelMethod());
            stringPlus.AppendSpaceLine(1, "}");
            stringPlus.AppendLine("}");
            stringPlus.AppendLine("");
            return stringPlus.ToString();
        }
        public string CreatModelMethod()
        {
            StringPlus stringPlus = new StringPlus();
            StringPlus stringPlus2 = new StringPlus();
            StringPlus stringPlus3 = new StringPlus();
            stringPlus.AppendSpaceLine(2, "#region Model");
            foreach (ColumnInfo current in this.Fieldlist)
            {
                string columnName = current.ColumnName;
                string typeName = current.TypeName;
                bool isIdentity = current.IsIdentity;
                bool isPrimaryKey = current.IsPrimaryKey;
                bool nullable = current.Nullable;
                string description = current.Description;
                string text = CodeCommon.DbTypeToCS(typeName);
                string text2 = "";
                if (CodeCommon.isValueType(text) && !isIdentity && !isPrimaryKey && nullable)
                {
                    text2 = "?";
                }
                stringPlus2.AppendSpace(2, string.Concat(new string[]
                {
                    "private ",
                    text,
                    text2,
                    " _",
                    columnName.ToLower()
                }));
                string key;
                if (current.DefaultVal.Length > 0 && (key = text.ToLower()) != null)
                {
                    if (dtCollection == null)
					{
                        dtCollection = new Dictionary<string, int>(16)
                        {{"int",0},{"long",1},{"bool",2},{"bit",3},{"nchar",4},{"ntext",5},
                         {"nvarchar",6},{"char",7},{"text",8},{"varchar",9},{"string",10},
                         {"datetime",11},{"uniqueidentifier",12},{"decimal",13},{"double",14},{"float",15}};
                    }
                    int num;
                    if (dtCollection.TryGetValue(key, out num))
                    {
                        switch (num)
                        {
                            case 0:
                            case 1:
                                stringPlus2.Append("=" + current.DefaultVal.Trim().Replace("'", ""));
                                break;
                            case 2:
                            case 3:
                                {
                                    string a = current.DefaultVal.Trim().Replace("'", "").ToLower();
                                    if (a == "1" || a == "true")
                                    {
                                        stringPlus2.Append("= true");
                                    }
                                    else
                                    {
                                        stringPlus2.Append("= false");
                                    }
                                    break;
                                }
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                                if (current.DefaultVal.Trim().StartsWith("N'"))
                                {
                                    stringPlus2.Append("=" + current.DefaultVal.Trim().Remove(0, 1).Replace("'", "\""));
                                }
                                else
                                {
                                    if (current.DefaultVal.Trim().IndexOf("'") > -1)
                                    {
                                        stringPlus2.Append("=" + current.DefaultVal.Trim().Replace("'", "\""));
                                    }
                                    else
                                    {
                                        stringPlus2.Append("= \"" + current.DefaultVal.Trim().Replace("(", "").Replace(")", "") + "\"");
                                    }
                                }
                                break;
                            case 11:
                                if (current.DefaultVal == "getdate" || current.DefaultVal == "Now()" || current.DefaultVal == "Now" || current.DefaultVal == "CURRENT_TIME" || current.DefaultVal == "CURRENT_DATE")
                                {
                                    stringPlus2.Append("= DateTime.Now");
                                }
                                else
                                {
                                    stringPlus2.Append("= Convert.ToDateTime(" + current.DefaultVal.Trim().Replace("'", "\"") + ")");
                                }
                                break;
                            case 13:
                            case 14:
                            case 15:
                                stringPlus2.Append("=" + current.DefaultVal.Replace("'", "").Replace("(", "").Replace(")", "").ToLower() + "M");
                                break;
                        }
                    }
                }
                stringPlus2.AppendLine(";");
                stringPlus3.AppendSpaceLine(2, "/// <summary>");
                stringPlus3.AppendSpaceLine(2, "/// " + description.Replace("\r\n", "\r\n\t///"));
                stringPlus3.AppendSpaceLine(2, "/// </summary>");
                stringPlus3.AppendSpaceLine(2, string.Concat(new string[]
                {
                    "public ",
                    text,
                    text2,
                    " ",
                    columnName
                }));
                stringPlus3.AppendSpaceLine(2, "{");
                stringPlus3.AppendSpaceLine(3, "set{ _" + columnName.ToLower() + "=value;}");
                stringPlus3.AppendSpaceLine(3, "get{return _" + columnName.ToLower() + ";}");
                stringPlus3.AppendSpaceLine(2, "}");
            }
            stringPlus.Append(stringPlus2.Value);
            stringPlus.Append(stringPlus3.Value);
            stringPlus.AppendSpaceLine(2, "#endregion Model");
            return stringPlus.ToString();
        }
    }
}
