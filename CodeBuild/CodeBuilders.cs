using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
namespace Maticsoft.CodeBuild
{
	public class CodeBuilders
	{
        private Dictionary<string, int> dtCollection;
        private IDbObject dbobj;
		private IBuilderWeb ibw;
		private string _dbtype;
		private string _dbconnectStr;
		private string _dbname;
		private string _tablename;
		private string _tabledescription = "";
		private string _modelname;
		private string _bllname;
		private string _dalname;
		private string _namespace = "Maticsoft";
		private string _folder;
		private string _dbhelperName = "DbHelperSQL";
		private List<ColumnInfo> _keys;
		private List<ColumnInfo> _fieldlist;
		private string _procprefix;
		private string _modelpath;
		private string _dalpath;
		private string _idalpath;
		private string _bllpath;
		private string _factoryclass;
		public string DbType
		{
			get
			{
				return this._dbtype;
			}
			set
			{
				this._dbtype = value;
			}
		}
		public string DbConnectStr
		{
			get
			{
				return this._dbconnectStr;
			}
			set
			{
				this._dbconnectStr = value;
			}
		}
		public string DbName
		{
			get
			{
				return this._dbname;
			}
			set
			{
				this._dbname = value;
			}
		}
		public string TableName
		{
			get
			{
				return this._tablename;
			}
			set
			{
				this._tablename = value;
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
		public string Folder
		{
			get
			{
				return this._folder;
			}
			set
			{
				this._folder = value;
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
		public string BLLName
		{
			get
			{
				return this._bllname;
			}
			set
			{
				this._bllname = value;
			}
		}
		public string DALName
		{
			get
			{
				return this._dalname;
			}
			set
			{
				this._dalname = value;
			}
		}
		public string DbHelperName
		{
			get
			{
				return this._dbhelperName;
			}
			set
			{
				this._dbhelperName = value;
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
		public List<ColumnInfo> Keys
		{
			get
			{
				return this._keys;
			}
			set
			{
				this._keys = value;
			}
		}
		public string ProcPrefix
		{
			get
			{
				return this._procprefix;
			}
			set
			{
				this._procprefix = value;
			}
		}
		public string Modelpath
		{
			get
			{
				this._modelpath = this._namespace + ".Model";
				if (this._folder.Trim() != "")
				{
					this._modelpath = this._modelpath + "." + this._folder;
				}
				return this._modelpath;
			}
			set
			{
				this._modelpath = value;
			}
		}
		public string ModelSpace
		{
			get
			{
				return this.Modelpath + "." + this.ModelName;
			}
		}
		public string DALpath
		{
			get
			{
				string text = this._namespace + "." + this._dbtype + "DAL";
				if (this._folder.Trim() != "")
				{
					text = text + "." + this._folder;
				}
				return text;
			}
			set
			{
				this._dalpath = value;
			}
		}
		public string IDALpath
		{
			get
			{
				this._idalpath = this._namespace + ".IDAL";
				if (this._folder.Trim() != "")
				{
					this._idalpath = this._idalpath + "." + this._folder;
				}
				return this._idalpath;
			}
		}
		public string IClass
		{
			get
			{
				return "I" + this.DALName;
			}
		}
		public string BLLpath
		{
			get
			{
				string text = this._namespace + ".BLL";
				if (this._folder.Trim() != "")
				{
					text = text + "." + this._folder;
				}
				return text;
			}
			set
			{
				this._bllpath = value;
			}
		}
		public string BLLSpace
		{
			get
			{
				return this.BLLpath + "." + this.BLLName;
			}
		}
		public string Factorypath
		{
			get
			{
				string text = this._namespace + ".DALFactory";
				if (this._folder.Trim() != "")
				{
					text = text + "." + this._folder;
				}
				return text;
			}
		}
		public string FactoryClass
		{
			get
			{
				this._factoryclass = this._namespace + ".DALFactory";
				if (this._folder.Trim() != "")
				{
					this._factoryclass = this._factoryclass + "." + this._folder;
				}
				this._factoryclass = this._factoryclass + "." + this._modelname;
				return this._factoryclass;
			}
		}
		public bool IsHasIdentity
		{
			get
			{
				bool result = false;
				if (this.Keys.Count > 0)
				{
					foreach (ColumnInfo current in this.Keys)
					{
						if (current.IsIdentity)
						{
							result = true;
						}
					}
				}
				return result;
			}
		}
		public CodeBuilders(IDbObject idbobj)
		{
			this.dbobj = idbobj;
			this.DbType = idbobj.DbType;
			string dbType;
			if (this._dbhelperName == "" && (dbType = this.DbType) != null)
			{
				if (dtCollection == null)
				{
                    dtCollection = new Dictionary<string, int>(7)
					{

						{
							"SQL2000",
							0
						},

						{
							"SQL2005",
							1
						},

						{
							"SQL2008",
							2
						},

						{
							"SQL2012",
							3
						},

						{
							"Oracle",
							4
						},

						{
							"MySQL",
							5
						},

						{
							"OleDb",
							6
						}
					};
				}
				int num;
				if (dtCollection.TryGetValue(dbType, out num))
				{
					switch (num)
					{
					case 0:
					case 1:
					case 2:
					case 3:
						this._dbhelperName = "DbHelperSQL";
						return;
					case 4:
						this._dbhelperName = "DbHelperOra";
						return;
					case 5:
						this._dbhelperName = "DbHelperMySQL";
						return;
					case 6:
						this._dbhelperName = "DbHelperOleDb";
						break;
					default:
						return;
					}
				}
			}
		}
		public string GetCodeFrameOne(string DALtype, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			BuilderFrameOne builderFrameOne = new BuilderFrameOne(this.dbobj, this.DbName, this.TableName, this.ModelName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameOne.GetCode(DALtype, Maxid, Exists, Add, Update, Delete, GetModel, List, this.ProcPrefix);
		}
		public string GetCodeFrameS3Model()
		{
			BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameS.GetModelCode();
		}
		public string GetCodeFrameS3DAL(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameS.GetDALCode(AssemblyGuid, Maxid, Exists, Add, Update, Delete, GetModel, List, this.ProcPrefix);
		}
		public string GetCodeFrameS3BLL(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool GetModelByCache, bool List)
		{
			BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameS.GetBLLCode(AssemblyGuid, Maxid, Exists, Add, Update, Delete, GetModel, GetModelByCache, List);
		}
		public string GetCodeFrameF3Model()
		{
			BuilderFrameF3 builderFrameF = new BuilderFrameF3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameF.GetModelCode();
		}
		public string GetCodeFrameF3DAL(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			BuilderFrameF3 builderFrameF = new BuilderFrameF3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameF.GetDALCode(AssemblyGuid, Maxid, Exists, Add, Update, Delete, GetModel, List, this.ProcPrefix);
		}
		public string GetCodeFrameF3IDAL(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List, bool ListProc)
		{
			BuilderFrameF3 builderFrameF = new BuilderFrameF3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameF.GetIDALCode(Maxid, Exists, Add, Update, Delete, GetModel, List, ListProc);
		}
		public string GetCodeFrameF3DALFactory()
		{
			BuilderFrameF3 builderFrameF = new BuilderFrameF3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameF.GetDALFactoryCode();
		}
		public string GetCodeFrameF3DALFactoryMethod()
		{
			BuilderFrameF3 builderFrameF = new BuilderFrameF3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameF.GetDALFactoryMethodCode();
		}
		public string GetCodeFrameF3BLL(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool GetModelByCache, bool List)
		{
			BuilderFrameF3 builderFrameF = new BuilderFrameF3(this.dbobj, this.DbName, this.TableName, this.TableDescription, this.ModelName, this.BLLName, this.DALName, this.Fieldlist, this.Keys, this.NameSpace, this.Folder, this.DbHelperName);
			return builderFrameF.GetBLLCode(AssemblyGuid, Maxid, Exists, Add, Update, Delete, GetModel, GetModelByCache, List, List);
		}
		public IBuilderWeb CreatBuilderWeb(string AssemblyGuid)
		{
			this.ibw = BuilderFactory.CreateWebObj(AssemblyGuid);
			this.ibw.NameSpace = this.NameSpace;
			this.ibw.Fieldlist = this.Fieldlist;
			this.ibw.Keys = this.Keys;
			this.ibw.ModelName = this.ModelName;
			this.ibw.Folder = this.Folder;
			this.ibw.BLLName = this.BLLName;
			return this.ibw;
		}
		public string GetAddAspx()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetAddAspx();
		}
		public string GetAddAspxCs()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			string addAspxCs = this.ibw.GetAddAspxCs();
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "protected void btnSave_Click(object sender, EventArgs e)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, addAspxCs);
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string GetAddDesigner()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetAddDesigner();
		}
		public string GetUpdateAspx()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetUpdateAspx();
		}
		public string GetUpdateAspxCs()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			string updateAspxCs = this.ibw.GetUpdateAspxCs();
			string updateShowAspxCs = this.ibw.GetUpdateShowAspxCs();
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "protected void Page_Load(object sender, EventArgs e)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "if (!Page.IsPostBack)");
			stringPlus.AppendSpaceLine(3, "{");
			if (this._keys.Count == 1)
			{
				string columnName = this._keys[0].ColumnName;
				stringPlus.AppendSpaceLine(4, "if (Request.Params[\"id\"] != null && Request.Params[\"id\"].Trim() != \"\")");
				stringPlus.AppendSpaceLine(4, "{");
				string a;
				if ((a = CodeCommon.DbTypeToCS(this._keys[0].TypeName).ToLower()) != null)
				{
					if (a == "int")
					{
						stringPlus.AppendSpaceLine(5, "int " + columnName + "=(Convert.ToInt32(Request.Params[\"id\"]));");
						goto IL_1AE;
					}
					if (a == "long")
					{
						stringPlus.AppendSpaceLine(5, "long " + columnName + "=(Convert.ToInt64(Request.Params[\"id\"]));");
						goto IL_1AE;
					}
					if (a == "decimal")
					{
						stringPlus.AppendSpaceLine(5, "decimal " + columnName + "=(Convert.ToDecimal(Request.Params[\"id\"]));");
						goto IL_1AE;
					}
					if (a == "bool")
					{
						stringPlus.AppendSpaceLine(5, "bool " + columnName + "=(Convert.ToBoolean(Request.Params[\"id\"]));");
						goto IL_1AE;
					}
					if (a == "guid")
					{
						stringPlus.AppendSpaceLine(5, "Guid " + columnName + "=new Guid(Request.Params[\"id\"]);");
						goto IL_1AE;
					}
				}
				stringPlus.AppendSpaceLine(5, "string " + columnName + "= Request.Params[\"id\"];");
				IL_1AE:
				stringPlus.AppendSpaceLine(5, "ShowInfo(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
				stringPlus.AppendSpaceLine(4, "}");
			}
			else
			{
				ColumnInfo identityKey = CodeCommon.GetIdentityKey(this._keys);
				if (identityKey != null)
				{
					string columnName = identityKey.ColumnName;
					stringPlus.AppendSpaceLine(4, "if (Request.Params[\"id\"] != null && Request.Params[\"id\"].Trim() != \"\")");
					stringPlus.AppendSpaceLine(4, "{");
					string a2;
					if ((a2 = CodeCommon.DbTypeToCS(identityKey.TypeName).ToLower()) != null)
					{
						if (a2 == "int")
						{
							stringPlus.AppendSpaceLine(5, "int " + columnName + "=(Convert.ToInt32(Request.Params[\"id\"]));");
							goto IL_2EC;
						}
						if (a2 == "long")
						{
							stringPlus.AppendSpaceLine(5, "long " + columnName + "=(Convert.ToInt64(Request.Params[\"id\"]));");
							goto IL_2EC;
						}
						if (a2 == "decimal")
						{
							stringPlus.AppendSpaceLine(5, "decimal " + columnName + "=(Convert.ToDecimal(Request.Params[\"id\"]));");
							goto IL_2EC;
						}
						if (a2 == "guid")
						{
							stringPlus.AppendSpaceLine(5, "Guid " + columnName + "=new Guid(Request.Params[\"id\"]);");
							goto IL_2EC;
						}
					}
					stringPlus.AppendSpaceLine(5, "string " + columnName + "= Request.Params[\"id\"];");
					IL_2EC:
					stringPlus.AppendSpaceLine(5, "ShowInfo(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
					stringPlus.AppendSpaceLine(4, "}");
				}
				else
				{
					int i = 0;
					while (i < this._keys.Count)
					{
						string columnName = this._keys[i].ColumnName;
						string text = CodeCommon.DbTypeToCS(this._keys[i].TypeName);
						string a3;
						if ((a3 = text.ToLower()) == null)
						{
							goto IL_400;
						}
						if (!(a3 == "int") && !(a3 == "long") && !(a3 == "decimal"))
						{
							if (!(a3 == "bool"))
							{
								if (!(a3 == "guid"))
								{
									goto IL_400;
								}
								stringPlus.AppendSpaceLine(4, text + " " + columnName + ";");
							}
							else
							{
								stringPlus.AppendSpaceLine(4, text + " " + columnName + " = false;");
							}
						}
						else
						{
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " = -1;");
						}
						IL_41A:
						stringPlus.AppendSpaceLine(4, string.Concat(new string[]
						{
							"if (Request.Params[\"id",
							i.ToString(),
							"\"] != null && Request.Params[\"id",
							i.ToString(),
							"\"].Trim() != \"\")"
						}));
						stringPlus.AppendSpaceLine(4, "{");
						string a4;
						if ((a4 = text) == null)
						{
							goto IL_572;
						}
						if (!(a4 == "int"))
						{
							if (!(a4 == "long"))
							{
								if (!(a4 == "decimal"))
								{
									if (!(a4 == "bool"))
									{
										if (!(a4 == "guid"))
										{
											goto IL_572;
										}
										stringPlus.AppendSpaceLine(5, columnName + "=new Guid(Request.Params[\"id" + i.ToString() + "\"]);");
									}
									else
									{
										stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToBoolean(Request.Params[\"id" + i.ToString() + "\"]));");
									}
								}
								else
								{
									stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToDecimal(Request.Params[\"id" + i.ToString() + "\"]));");
								}
							}
							else
							{
								stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToInt64(Request.Params[\"id" + i.ToString() + "\"]));");
							}
						}
						else
						{
							stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToInt32(Request.Params[\"id" + i.ToString() + "\"]));");
						}
						IL_591:
						stringPlus.AppendSpaceLine(4, "}");
						i++;
						continue;
						IL_572:
						stringPlus.AppendSpaceLine(5, columnName + "= Request.Params[\"id" + i.ToString() + "\"];");
						goto IL_591;
						IL_400:
						stringPlus.AppendSpaceLine(4, text + " " + columnName + " = \"\";");
						goto IL_41A;
					}
					stringPlus.AppendSpaceLine(4, "#warning 代码生成提示：显示页面,请检查确认该语句是否正确");
					stringPlus.AppendSpaceLine(4, "ShowInfo(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
				}
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(3, updateShowAspxCs);
			stringPlus.AppendSpaceLine(2, "public void btnSave_Click(object sender, EventArgs e)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, updateAspxCs);
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string GetUpdateDesigner()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetUpdateDesigner();
		}
		public string GetUpdateShowAspxCs()
		{
			return this.ibw.GetUpdateShowAspxCs();
		}
		public string GetShowAspx()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetShowAspx();
		}
		public string GetShowAspxCs()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			string showAspxCs = this.ibw.GetShowAspxCs();
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "public string strid=\"\"; ");
			stringPlus.AppendSpaceLine(2, "protected void Page_Load(object sender, EventArgs e)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "if (!Page.IsPostBack)");
			stringPlus.AppendSpaceLine(3, "{");
			if (this._keys.Count == 1)
			{
				string columnName = this._keys[0].ColumnName;
				stringPlus.AppendSpaceLine(4, "if (Request.Params[\"id\"] != null && Request.Params[\"id\"].Trim() != \"\")");
				stringPlus.AppendSpaceLine(4, "{");
				stringPlus.AppendSpaceLine(5, "strid = Request.Params[\"id\"];");
				string a;
				if ((a = CodeCommon.DbTypeToCS(this._keys[0].TypeName).ToLower()) != null)
				{
					if (a == "int")
					{
						stringPlus.AppendSpaceLine(5, "int " + columnName + "=(Convert.ToInt32(strid));");
						goto IL_1BC;
					}
					if (a == "long")
					{
						stringPlus.AppendSpaceLine(5, "long " + columnName + "=(Convert.ToInt64(strid));");
						goto IL_1BC;
					}
					if (a == "decimal")
					{
						stringPlus.AppendSpaceLine(5, "decimal " + columnName + "=(Convert.ToDecimal(strid));");
						goto IL_1BC;
					}
					if (a == "bool")
					{
						stringPlus.AppendSpaceLine(5, "bool " + columnName + "=(Convert.ToBoolean(strid));");
						goto IL_1BC;
					}
					if (a == "guid")
					{
						stringPlus.AppendSpaceLine(5, "Guid " + columnName + "=new Guid(strid);");
						goto IL_1BC;
					}
				}
				stringPlus.AppendSpaceLine(5, "string " + columnName + "= strid;");
				IL_1BC:
				stringPlus.AppendSpaceLine(5, "ShowInfo(" + columnName + ");");
				stringPlus.AppendSpaceLine(4, "}");
			}
			else
			{
				ColumnInfo identityKey = CodeCommon.GetIdentityKey(this._keys);
				if (identityKey != null)
				{
					string columnName = identityKey.ColumnName;
					stringPlus.AppendSpaceLine(4, "if (Request.Params[\"id\"] != null && Request.Params[\"id\"].Trim() != \"\")");
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, "strid = Request.Params[\"id\"];");
					string a2;
					if ((a2 = CodeCommon.DbTypeToCS(identityKey.TypeName).ToLower()) != null)
					{
						if (a2 == "int")
						{
							stringPlus.AppendSpaceLine(5, "int " + columnName + "=(Convert.ToInt32(strid));");
							goto IL_326;
						}
						if (a2 == "long")
						{
							stringPlus.AppendSpaceLine(5, "long " + columnName + "=(Convert.ToInt64(strid));");
							goto IL_326;
						}
						if (a2 == "decimal")
						{
							stringPlus.AppendSpaceLine(5, "decimal " + columnName + "=(Convert.ToDecimal(strid));");
							goto IL_326;
						}
						if (a2 == "bool")
						{
							stringPlus.AppendSpaceLine(5, "bool " + columnName + "=(Convert.ToBoolean(strid));");
							goto IL_326;
						}
						if (a2 == "guid")
						{
							stringPlus.AppendSpaceLine(5, "Guid " + columnName + "=new Guid(strid);");
							goto IL_326;
						}
					}
					stringPlus.AppendSpaceLine(5, "string " + columnName + "= strid;");
					IL_326:
					stringPlus.AppendSpaceLine(5, "ShowInfo(" + columnName + ");");
					stringPlus.AppendSpaceLine(4, "}");
				}
				else
				{
					int i = 0;
					while (i < this._keys.Count)
					{
						string columnName = this._keys[i].ColumnName;
						string text = CodeCommon.DbTypeToCS(this._keys[i].TypeName);
						string key;
						if ((key = text.ToLower()) == null)
						{
							goto IL_478;
						}
						if (dtCollection == null)
						{
                            dtCollection = new Dictionary<string, int>(6)
							{

								{
									"int",
									0
								},

								{
									"long",
									1
								},

								{
									"decimal",
									2
								},

								{
									"bool",
									3
								},

								{
									"guid",
									4
								},

								{
									"Guid",
									5
								}
							};
						}
						int num;
						if (!dtCollection.TryGetValue(key, out num))
						{
							goto IL_478;
						}
						switch (num)
						{
						case 0:
						case 1:
						case 2:
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " = -1;");
							break;
						case 3:
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " = false;");
							break;
						case 4:
						case 5:
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " ;");
							break;
						default:
							goto IL_478;
						}
						IL_492:
						stringPlus.AppendSpaceLine(4, string.Concat(new string[]
						{
							"if (Request.Params[\"id",
							i.ToString(),
							"\"] != null && Request.Params[\"id",
							i.ToString(),
							"\"].Trim() != \"\")"
						}));
						stringPlus.AppendSpaceLine(4, "{");
						string a3;
						if ((a3 = text.ToLower()) == null)
						{
							goto IL_5E0;
						}
						if (!(a3 == "int"))
						{
							if (!(a3 == "long"))
							{
								if (!(a3 == "decimal"))
								{
									if (!(a3 == "bool"))
									{
										if (!(a3 == "guid"))
										{
											goto IL_5E0;
										}
										stringPlus.AppendSpaceLine(5, columnName + "=new Guid(Request.Params[\"id\"]);");
									}
									else
									{
										stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToBoolean(Request.Params[\"id" + i.ToString() + "\"]));");
									}
								}
								else
								{
									stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToDecimal(Request.Params[\"id" + i.ToString() + "\"]));");
								}
							}
							else
							{
								stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToInt64(Request.Params[\"id" + i.ToString() + "\"]));");
							}
						}
						else
						{
							stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToInt32(Request.Params[\"id" + i.ToString() + "\"]));");
						}
						IL_5FF:
						stringPlus.AppendSpaceLine(4, "}");
						i++;
						continue;
						IL_5E0:
						stringPlus.AppendSpaceLine(5, columnName + "= Request.Params[\"id" + i.ToString() + "\"];");
						goto IL_5FF;
						IL_478:
						stringPlus.AppendSpaceLine(4, text + " " + columnName + " = \"\";");
						goto IL_492;
					}
					stringPlus.AppendSpaceLine(4, "#warning 代码生成提示：显示页面,请检查确认该语句是否正确");
					stringPlus.AppendSpaceLine(4, "ShowInfo(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
				}
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(2, showAspxCs);
			return stringPlus.ToString();
		}
		public string GetShowDesigner()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetShowDesigner();
		}
		public string GetDeleteAspxCs()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(3, "if (!Page.IsPostBack)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, this.BLLSpace + " bll=new " + this.BLLSpace + "();");
			if (this._keys.Count == 1)
			{
				string columnName = this._keys[0].ColumnName;
				stringPlus.AppendSpaceLine(4, "if (Request.Params[\"id\"] != null && Request.Params[\"id\"].Trim() != \"\")");
				stringPlus.AppendSpaceLine(4, "{");
				string a;
				if ((a = CodeCommon.DbTypeToCS(this._keys[0].TypeName).ToLower()) != null)
				{
					if (a == "int")
					{
						stringPlus.AppendSpaceLine(5, "int " + columnName + "=(Convert.ToInt32(Request.Params[\"id\"]));");
						goto IL_19F;
					}
					if (a == "long")
					{
						stringPlus.AppendSpaceLine(5, "long " + columnName + "=(Convert.ToInt64(Request.Params[\"id\"]));");
						goto IL_19F;
					}
					if (a == "decimal")
					{
						stringPlus.AppendSpaceLine(5, "decimal " + columnName + "=(Convert.ToDecimal(Request.Params[\"id\"]));");
						goto IL_19F;
					}
					if (a == "bool")
					{
						stringPlus.AppendSpaceLine(5, "bool " + columnName + "=(Convert.ToBoolean(Request.Params[\"id\"]));");
						goto IL_19F;
					}
					if (a == "guid")
					{
						stringPlus.AppendSpaceLine(5, "Guid " + columnName + "=new Guid(Request.Params[\"id\"]);");
						goto IL_19F;
					}
				}
				stringPlus.AppendSpaceLine(5, "string " + columnName + "= Request.Params[\"id\"];");
				IL_19F:
				stringPlus.AppendSpaceLine(5, "bll.Delete(" + columnName + ");");
				stringPlus.AppendSpaceLine(5, "Response.Redirect(\"list.aspx\");");
				stringPlus.AppendSpaceLine(4, "}");
			}
			else
			{
				ColumnInfo identityKey = CodeCommon.GetIdentityKey(this._keys);
				if (identityKey != null)
				{
					string columnName = identityKey.ColumnName;
					stringPlus.AppendSpaceLine(4, "if (Request.Params[\"id\"] != null && Request.Params[\"id\"].Trim() != \"\")");
					stringPlus.AppendSpaceLine(4, "{");
					string a2;
					if ((a2 = CodeCommon.DbTypeToCS(identityKey.TypeName).ToLower()) != null)
					{
						if (a2 == "int")
						{
							stringPlus.AppendSpaceLine(5, "int " + columnName + "=(Convert.ToInt32(Request.Params[\"id\"]));");
							goto IL_309;
						}
						if (a2 == "long")
						{
							stringPlus.AppendSpaceLine(5, "long " + columnName + "=(Convert.ToInt64(Request.Params[\"id\"]));");
							goto IL_309;
						}
						if (a2 == "decimal")
						{
							stringPlus.AppendSpaceLine(5, "decimal " + columnName + "=(Convert.ToDecimal(Request.Params[\"id\"]));");
							goto IL_309;
						}
						if (a2 == "bool")
						{
							stringPlus.AppendSpaceLine(5, "bool " + columnName + "=(Convert.ToBoolean(Request.Params[\"id\"]));");
							goto IL_309;
						}
						if (a2 == "guid")
						{
							stringPlus.AppendSpaceLine(5, "Guid " + columnName + "=new Guid(Request.Params[\"id\"]);");
							goto IL_309;
						}
					}
					stringPlus.AppendSpaceLine(5, "string " + columnName + "= Request.Params[\"id\"];");
					IL_309:
					stringPlus.AppendSpaceLine(4, "bll.Delete(" + columnName + ");");
					stringPlus.AppendSpaceLine(4, "}");
				}
				else
				{
					int i = 0;
					while (i < this._keys.Count)
					{
						string columnName = this._keys[i].ColumnName;
						string text = CodeCommon.DbTypeToCS(this._keys[i].TypeName);
						string key;
						if ((key = text) == null)
						{
							goto IL_453;
						}
						if (dtCollection == null)
						{
                            dtCollection = new Dictionary<string, int>(6)
							{

								{
									"int",
									0
								},

								{
									"long",
									1
								},

								{
									"decimal",
									2
								},

								{
									"bool",
									3
								},

								{
									"guid",
									4
								},

								{
									"Guid",
									5
								}
							};
						}
						int num;
						if (!dtCollection.TryGetValue(key, out num))
						{
							goto IL_453;
						}
						switch (num)
						{
						case 0:
						case 1:
						case 2:
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " = -1;");
							break;
						case 3:
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " = false;");
							break;
						case 4:
						case 5:
							stringPlus.AppendSpaceLine(4, text + " " + columnName + " ;");
							break;
						default:
							goto IL_453;
						}
						IL_46D:
						stringPlus.AppendSpaceLine(4, string.Concat(new string[]
						{
							"if (Request.Params[\"id",
							i.ToString(),
							"\"] != null && Request.Params[\"id",
							i.ToString(),
							"\"].Trim() != \"\")"
						}));
						stringPlus.AppendSpaceLine(4, "{");
						string a3;
						if ((a3 = text.ToLower()) == null)
						{
							goto IL_5BB;
						}
						if (!(a3 == "int"))
						{
							if (!(a3 == "long"))
							{
								if (!(a3 == "decimal"))
								{
									if (!(a3 == "bool"))
									{
										if (!(a3 == "guid"))
										{
											goto IL_5BB;
										}
										stringPlus.AppendSpaceLine(5, columnName + "=new Guid(Request.Params[\"id\"]);");
									}
									else
									{
										stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToBoolean(Request.Params[\"id" + i.ToString() + "\"]));");
									}
								}
								else
								{
									stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToDecimal(Request.Params[\"id" + i.ToString() + "\"]));");
								}
							}
							else
							{
								stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToInt64(Request.Params[\"id" + i.ToString() + "\"]));");
							}
						}
						else
						{
							stringPlus.AppendSpaceLine(5, columnName + "=(Convert.ToInt32(Request.Params[\"id" + i.ToString() + "\"]));");
						}
						IL_5DA:
						stringPlus.AppendSpaceLine(4, "}");
						i++;
						continue;
						IL_5BB:
						stringPlus.AppendSpaceLine(5, columnName + "= Request.Params[\"id" + i.ToString() + "\"];");
						goto IL_5DA;
						IL_453:
						stringPlus.AppendSpaceLine(4, text + " " + columnName + " = \"\";");
						goto IL_46D;
					}
					stringPlus.AppendSpaceLine(4, "#warning 代码生成提示：删除页面,请检查确认传递过来的参数是否正确");
					stringPlus.AppendSpaceLine(4, "// bll.Delete(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
				}
			}
			stringPlus.AppendSpaceLine(3, "}");
			return stringPlus.ToString();
		}
		public string GetListAspx()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetListAspx();
		}
		public string GetListAspxCs()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetListAspxCs();
		}
		public string GetListDesigner()
		{
			if (this.ibw == null)
			{
				return "//请选择有效的表示层代码组件！";
			}
			return this.ibw.GetListDesigner();
		}
		public string GetWebHtmlCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm)
		{
			StringPlus stringPlus = new StringPlus();
			if (AddForm)
			{
				stringPlus.AppendLine(" <!--******************************增加页面代码********************************-->");
				stringPlus.AppendLine(this.GetAddAspx());
			}
			if (UpdateForm)
			{
				stringPlus.AppendLine(" <!--******************************修改页面代码********************************-->");
				stringPlus.AppendLine(this.GetUpdateAspx());
			}
			if (ShowForm)
			{
				stringPlus.AppendLine("  <!--******************************显示页面代码********************************-->");
				stringPlus.AppendLine(this.GetShowAspx());
			}
			if (SearchForm)
			{
				stringPlus.AppendLine("  <!--******************************列表页面代码********************************-->");
				stringPlus.AppendLine(this.GetListAspx());
			}
			return stringPlus.ToString();
		}
		public string GetWebCode(bool ExistsKey, bool AddForm, bool UpdateForm, bool ShowForm, bool SearchForm)
		{
			StringPlus stringPlus = new StringPlus();
			if (AddForm)
			{
				stringPlus.AppendLine("  /******************************增加窗体代码********************************/");
				stringPlus.AppendLine(this.GetAddAspxCs());
			}
			if (UpdateForm)
			{
				stringPlus.AppendLine("  /******************************修改窗体代码********************************/");
				stringPlus.AppendLine("  /*修改代码-提交更新 */");
				stringPlus.AppendLine(this.GetUpdateAspxCs());
			}
			if (ShowForm)
			{
				stringPlus.AppendLine("  /******************************显示窗体代码********************************/");
				stringPlus.AppendLine(this.GetShowAspxCs());
			}
			if (SearchForm)
			{
				stringPlus.AppendLine("  /******************************列表窗体代码********************************/");
				stringPlus.AppendLine(this.GetListAspxCs());
			}
			return stringPlus.Value;
		}
	}
}
