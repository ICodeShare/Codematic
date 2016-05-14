using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
namespace Maticsoft.BuilderWeb
{
	public class BuilderWeb : IBuilderWeb
	{
        protected Dictionary<string ,int> dtCollection;
		protected string _key = "ID";
		protected string _keyType = "int";
		protected string _namespace = "Maticsoft";
		private string _folder = "";
		protected string _modelname;
		protected string _bllname;
		protected List<ColumnInfo> _fieldlist;
		protected List<ColumnInfo> _keys;
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
		public string ModelSpace
		{
			get
			{
				string str = this._namespace + ".Model";
				if (this._folder.Trim() != "")
				{
					str = str + "." + this._folder;
				}
				return str + "." + this.ModelName;
			}
		}
		private string BLLSpace
		{
			get
			{
				string str = this._namespace + ".BLL";
				if (this._folder.Trim() != "")
				{
					str = str + "." + this._folder;
				}
				return str + "." + this.BLLName;
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
		protected string Key
		{
			get
			{
				foreach (ColumnInfo current in this._keys)
				{
					this._key = current.ColumnName;
					this._keyType = current.TypeName;
					if (current.IsIdentity)
					{
						this._key = current.ColumnName;
						this._keyType = CodeCommon.DbTypeToCS(current.TypeName);
						break;
					}
				}
				return this._key;
			}
		}
		private bool isFilterColume(string columnName)
		{
			return false;
		}
		public string GetAddAspx()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
			bool flag = false;
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string text = current.Description;
				bool arg_52_0 = current.IsPrimaryKey;
				if (!current.IsIdentity && !this.isFilterColume(columnName) && !(typeName.Trim().ToLower() == "uniqueidentifier"))
				{
					text = CodeCommon.CutDescText(text, 15, columnName);
					stringPlus.AppendSpaceLine(1, "<tr>");
					stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
					stringPlus.AppendSpaceLine(2, text);
					stringPlus.AppendSpaceLine(1, "：</td>");
					stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
					string a;
					if ((a = typeName.Trim().ToLower()) == null)
					{
						goto IL_17C;
					}
					if (!(a == "datetime") && !(a == "smalldatetime"))
					{
						if (!(a == "bit"))
						{
							if (!(a == "uniqueidentifier"))
							{
								goto IL_17C;
							}
						}
						else
						{
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"<asp:CheckBox ID=\"chk",
								columnName,
								"\" Text=\"",
								text,
								"\" runat=\"server\" Checked=\"False\" />"
							}));
						}
					}
					else
					{
						stringPlus.AppendSpaceLine(2, "<asp:TextBox ID=\"txt" + columnName + "\" runat=\"server\" Width=\"70px\"  onfocus=\"setday(this)\"></asp:TextBox>");
						flag = true;
					}
					IL_194:
					stringPlus.AppendSpaceLine(1, "</td></tr>");
					continue;
					IL_17C:
					stringPlus.AppendSpaceLine(2, "<asp:TextBox id=\"txt" + columnName + "\" runat=\"server\" Width=\"200px\"></asp:TextBox>");
					goto IL_194;
				}
			}
			stringPlus.AppendLine("</table>");
			if (flag)
			{
				stringPlus.AppendLine("<script src=\"/js/calendar1.js\" type=\"text/javascript\"></script>");
			}
			return stringPlus.ToString();
		}
		public string GetUpdateAspx()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("");
			stringPlus.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
			bool flag = false;
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string text = current.Description;
				bool isPrimaryKey = current.IsPrimaryKey;
				bool isIdentity = current.IsIdentity;
				text = CodeCommon.CutDescText(text, 15, columnName);
				if (!this.isFilterColume(columnName))
				{
					if (!isPrimaryKey && !isIdentity && !(typeName.Trim().ToLower() == "uniqueidentifier"))
					{
						stringPlus.AppendSpaceLine(1, "<tr>");
						stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
						stringPlus.AppendSpaceLine(2, text);
						stringPlus.AppendSpaceLine(1, "：</td>");
						stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
						string a;
						if ((a = typeName.Trim().ToLower()) == null)
						{
							goto IL_1DA;
						}
						if (!(a == "datetime") && !(a == "smalldatetime"))
						{
							if (!(a == "bit"))
							{
								goto IL_1DA;
							}
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"<asp:CheckBox ID=\"chk",
								columnName,
								"\" Text=\"",
								text,
								"\" runat=\"server\" Checked=\"False\" />"
							}));
						}
						else
						{
							stringPlus.AppendSpaceLine(2, "<asp:TextBox ID=\"txt" + columnName + "\" runat=\"server\" Width=\"70px\"  onfocus=\"setday(this)\"></asp:TextBox>");
							flag = true;
						}
						IL_1F2:
						stringPlus.AppendSpaceLine(1, "</td></tr>");
						continue;
						IL_1DA:
						stringPlus.AppendSpaceLine(2, "<asp:TextBox id=\"txt" + columnName + "\" runat=\"server\" Width=\"200px\"></asp:TextBox>");
						goto IL_1F2;
					}
					stringPlus.AppendSpaceLine(1, "<tr>");
					stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
					stringPlus.AppendSpaceLine(2, text);
					stringPlus.AppendSpaceLine(1, "：</td>");
					stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
					stringPlus.AppendSpaceLine(2, "<asp:label id=\"lbl" + columnName + "\" runat=\"server\"></asp:label>");
					stringPlus.AppendSpaceLine(1, "</td></tr>");
				}
			}
			stringPlus.AppendLine("</table>");
			if (flag)
			{
				stringPlus.AppendLine("<script src=\"/js/calendar1.js\" type=\"text/javascript\"></script>");
			}
			return stringPlus.Value;
		}
		public string GetShowAspx()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string text = current.Description;
				text = CodeCommon.CutDescText(text, 15, columnName);
				stringPlus.AppendSpaceLine(1, "<tr>");
				stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"30%\" align=\"right\">");
				stringPlus.AppendSpaceLine(2, text);
				stringPlus.AppendSpaceLine(1, "：</td>");
				stringPlus.AppendSpaceLine(1, "<td height=\"25\" width=\"*\" align=\"left\">");
				typeName.Trim().ToLower();
				stringPlus.AppendSpaceLine(2, "<asp:Label id=\"lbl" + columnName + "\" runat=\"server\"></asp:Label>");
				stringPlus.AppendSpaceLine(1, "</td></tr>");
			}
			stringPlus.AppendLine("</table>");
			return stringPlus.ToString();
		}
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
				stringPlus.AppendLine("  /*修改代码-显示 */");
				stringPlus.AppendLine(this.GetUpdateShowAspxCs());
				stringPlus.AppendLine("  /*修改代码-提交更新 */");
				stringPlus.AppendLine(this.GetUpdateAspxCs());
			}
			if (ShowForm)
			{
				stringPlus.AppendLine("  /******************************显示窗体代码********************************/");
				stringPlus.AppendLine(this.GetShowAspxCs());
			}
			return stringPlus.Value;
		}
		public string GetAddAspxCs()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(3, "string strErr=\"\";");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string text = current.Description;
				bool arg_69_0 = current.IsPrimaryKey;
				if (!current.IsIdentity && !("uniqueidentifier" == typeName.ToLower()))
				{
					text = CodeCommon.CutDescText(text, 15, columnName);
					string key;
					if ((key = CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower()) == null)
					{
						goto IL_3DB;
					}
					if (dtCollection == null)
					{
						dtCollection = new Dictionary<string, int>(11)
						{

							{
								"int",
								0
							},

							{
								"smallint",
								1
							},

							{
								"float",
								2
							},

							{
								"numeric",
								3
							},

							{
								"decimal",
								4
							},

							{
								"datetime",
								5
							},

							{
								"smalldatetime",
								6
							},

							{
								"bool",
								7
							},

							{
								"byte[]",
								8
							},

							{
								"guid",
								9
							},

							{
								"uniqueidentifier",
								10
							}
						};
					}
					int num;
					if (!dtCollection.TryGetValue(key, out num))
					{
						goto IL_3DB;
					}
					switch (num)
					{
					case 0:
					case 1:
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"int ",
							columnName,
							"=int.Parse(this.txt",
							columnName,
							".Text);"
						}));
						stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsNumber(txt" + columnName + ".Text))");
						stringPlus3.AppendSpaceLine(3, "{");
						stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
						stringPlus3.AppendSpaceLine(3, "}");
						break;
					case 2:
					case 3:
					case 4:
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"decimal ",
							columnName,
							"=decimal.Parse(this.txt",
							columnName,
							".Text);"
						}));
						stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsDecimal(txt" + columnName + ".Text))");
						stringPlus3.AppendSpaceLine(3, "{");
						stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
						stringPlus3.AppendSpaceLine(3, "}");
						break;
					case 5:
					case 6:
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"DateTime ",
							columnName,
							"=DateTime.Parse(this.txt",
							columnName,
							".Text);"
						}));
						stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsDateTime(txt" + columnName + ".Text))");
						stringPlus3.AppendSpaceLine(3, "{");
						stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
						stringPlus3.AppendSpaceLine(3, "}");
						break;
					case 7:
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"bool ",
							columnName,
							"=this.chk",
							columnName,
							".Checked;"
						}));
						break;
					case 8:
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"byte[] ",
							columnName,
							"= new UnicodeEncoding().GetBytes(this.txt",
							columnName,
							".Text);"
						}));
						break;
					case 9:
					case 10:
						break;
					default:
						goto IL_3DB;
					}
					IL_465:
					stringPlus4.AppendSpaceLine(3, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=",
						columnName,
						";"
					}));
					continue;
					IL_3DB:
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"string ",
						columnName,
						"=this.txt",
						columnName,
						".Text;"
					}));
					stringPlus3.AppendSpaceLine(3, "if(this.txt" + columnName + ".Text.Trim().Length==0)");
					stringPlus3.AppendSpaceLine(3, "{");
					stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "不能为空！\\\\n\";\t");
					stringPlus3.AppendSpaceLine(3, "}");
					goto IL_465;
				}
			}
			stringPlus.AppendLine(stringPlus3.ToString());
			stringPlus.AppendSpaceLine(3, "if(strErr!=\"\")");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "MessageBox.Show(this,strErr);");
			stringPlus.AppendSpaceLine(4, "return;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendLine(stringPlus2.ToString());
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model=new " + this.ModelSpace + "();");
			stringPlus.AppendLine(stringPlus4.ToString());
			stringPlus.AppendSpaceLine(3, this.BLLSpace + " bll=new " + this.BLLSpace + "();");
			stringPlus.AppendSpaceLine(3, "bll.Add(model);");
			stringPlus.AppendSpaceLine(3, "Maticsoft.Common.MessageBox.ShowAndRedirect(this,\"保存成功！\",\"add.aspx\");");
			return stringPlus.Value;
		}
		public string GetUpdateAspxCs()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(3, "string strErr=\"\";");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string text = current.Description;
				bool isPrimaryKey = current.IsPrimaryKey;
				bool isIdentity = current.IsIdentity;
				text = CodeCommon.CutDescText(text, 15, columnName);
				string key;
				if ((key = CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower()) == null)
				{
					goto IL_583;
				}
				if (dtCollection == null)
				{
					dtCollection = new Dictionary<string, int>(12)
					{

						{
							"int",
							0
						},

						{
							"smallint",
							1
						},

						{
							"long",
							2
						},

						{
							"float",
							3
						},

						{
							"numeric",
							4
						},

						{
							"decimal",
							5
						},

						{
							"datetime",
							6
						},

						{
							"smalldatetime",
							7
						},

						{
							"bool",
							8
						},

						{
							"byte[]",
							9
						},

						{
							"guid",
							10
						},

						{
							"uniqueidentifier",
							11
						}
					};
				}
				int num;
				if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_583;
				}
				switch (num)
				{
				case 0:
				case 1:
					if (isPrimaryKey || isIdentity)
					{
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"int ",
							columnName,
							"=int.Parse(this.lbl",
							columnName,
							".Text);"
						}));
					}
					else
					{
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"int ",
							columnName,
							"=int.Parse(this.txt",
							columnName,
							".Text);"
						}));
						stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsNumber(txt" + columnName + ".Text))");
						stringPlus3.AppendSpaceLine(3, "{");
						stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
						stringPlus3.AppendSpaceLine(3, "}");
					}
					break;
				case 2:
					if (isPrimaryKey || isIdentity)
					{
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"long ",
							columnName,
							"=long.Parse(this.lbl",
							columnName,
							".Text);"
						}));
					}
					else
					{
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"long ",
							columnName,
							"=long.Parse(this.txt",
							columnName,
							".Text);"
						}));
						stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsNumber(txt" + columnName + ".Text))");
						stringPlus3.AppendSpaceLine(3, "{");
						stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
						stringPlus3.AppendSpaceLine(3, "}");
					}
					break;
				case 3:
				case 4:
				case 5:
					if (isPrimaryKey || isIdentity)
					{
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"decimal ",
							columnName,
							"=decimal.Parse(this.lbl",
							columnName,
							".Text);"
						}));
					}
					else
					{
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"decimal ",
							columnName,
							"=decimal.Parse(this.txt",
							columnName,
							".Text);"
						}));
						stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsDecimal(txt" + columnName + ".Text))");
						stringPlus3.AppendSpaceLine(3, "{");
						stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
						stringPlus3.AppendSpaceLine(3, "}");
					}
					break;
				case 6:
				case 7:
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"DateTime ",
						columnName,
						"=DateTime.Parse(this.txt",
						columnName,
						".Text);"
					}));
					stringPlus3.AppendSpaceLine(3, "if(!PageValidate.IsDateTime(txt" + columnName + ".Text))");
					stringPlus3.AppendSpaceLine(3, "{");
					stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "格式错误！\\\\n\";\t");
					stringPlus3.AppendSpaceLine(3, "}");
					break;
				case 8:
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"bool ",
						columnName,
						"=this.chk",
						columnName,
						".Checked;"
					}));
					break;
				case 9:
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"byte[] ",
						columnName,
						"= new UnicodeEncoding().GetBytes(this.txt",
						columnName,
						".Text);"
					}));
					break;
				case 10:
				case 11:
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"Guid ",
						columnName,
						"= new Guid(this.lbl",
						columnName,
						".Text);"
					}));
					break;
				default:
					goto IL_583;
				}
				IL_658:
				stringPlus4.AppendSpaceLine(3, string.Concat(new string[]
				{
					"model.",
					columnName,
					"=",
					columnName,
					";"
				}));
				continue;
				IL_583:
				if (isPrimaryKey || isIdentity)
				{
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"string ",
						columnName,
						"=this.lbl",
						columnName,
						".Text;"
					}));
					goto IL_658;
				}
				stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
				{
					"string ",
					columnName,
					"=this.txt",
					columnName,
					".Text;"
				}));
				stringPlus3.AppendSpaceLine(3, "if(this.txt" + columnName + ".Text.Trim().Length==0)");
				stringPlus3.AppendSpaceLine(3, "{");
				stringPlus3.AppendSpaceLine(4, "strErr+=\"" + text + "不能为空！\\\\n\";\t");
				stringPlus3.AppendSpaceLine(3, "}");
				goto IL_658;
			}
			stringPlus.AppendLine(stringPlus3.ToString());
			stringPlus.AppendSpaceLine(3, "if(strErr!=\"\")");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "MessageBox.Show(this,strErr);");
			stringPlus.AppendSpaceLine(4, "return;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendLine(stringPlus2.ToString());
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model=new " + this.ModelSpace + "();");
			stringPlus.AppendLine(stringPlus4.ToString());
			stringPlus.AppendSpaceLine(3, this.BLLSpace + " bll=new " + this.BLLSpace + "();");
			stringPlus.AppendSpaceLine(3, "bll.Update(model);");
			stringPlus.AppendSpaceLine(3, "Maticsoft.Common.MessageBox.ShowAndRedirect(this,\"保存成功！\",\"list.aspx\");");
			return stringPlus.ToString();
		}
		public string GetUpdateShowAspxCs()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			string arg_13_0 = this.Key;
			stringPlus.AppendSpaceLine(1, "private void ShowInfo(" + CodeCommon.GetInParameter(this.Keys, true) + ")");
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, this.BLLSpace + " bll=new " + this.BLLSpace + "();");
			stringPlus.AppendSpaceLine(2, this.ModelSpace + " model=bll.GetModel(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string arg_BE_0 = current.Description;
				bool isPrimaryKey = current.IsPrimaryKey;
				bool isIdentity = current.IsIdentity;
				if (!this.isFilterColume(columnName))
				{
					string key;
					switch (key = CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower())
					{
					case "int":
					case "long":
					case "smallint":
					case "float":
					case "numeric":
					case "decimal":
					case "datetime":
					case "smalldatetime":
						if (isPrimaryKey || isIdentity)
						{
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"this.lbl",
								columnName,
								".Text=model.",
								columnName,
								".ToString();"
							}));
							continue;
						}
						stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"this.txt",
							columnName,
							".Text=model.",
							columnName,
							".ToString();"
						}));
						continue;
					case "bool":
						stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"this.chk",
							columnName,
							".Checked=model.",
							columnName,
							";"
						}));
						continue;
					case "byte[]":
						stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"this.txt",
							columnName,
							".Text=model.",
							columnName,
							".ToString();"
						}));
						continue;
					case "guid":
					case "uniqueidentifier":
						stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"this.lbl",
							columnName,
							".Text=model.",
							columnName,
							".ToString();"
						}));
						continue;
					}
					if (isPrimaryKey || isIdentity)
					{
						stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"this.lbl",
							columnName,
							".Text=model.",
							columnName,
							";"
						}));
					}
					else
					{
						stringPlus.AppendSpaceLine(2, string.Concat(new string[]
						{
							"this.txt",
							columnName,
							".Text=model.",
							columnName,
							";"
						}));
					}
				}
			}
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(1, "}");
			return stringPlus.Value;
		}
		public string GetShowAspxCs()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			string arg_13_0 = this.Key;
			stringPlus.AppendSpaceLine(1, "private void ShowInfo(" + CodeCommon.GetInParameter(this.Keys, true) + ")");
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, this.BLLSpace + " bll=new " + this.BLLSpace + "();");
			stringPlus.AppendSpaceLine(2, this.ModelSpace + " model=bll.GetModel(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string arg_BE_0 = current.Description;
				bool arg_C5_0 = current.IsPrimaryKey;
				bool arg_CC_0 = current.IsIdentity;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower())
				{
				case "int":
				case "long":
				case "smallint":
				case "float":
				case "numeric":
				case "decimal":
				case "datetime":
				case "smalldatetime":
					stringPlus.AppendSpaceLine(2, string.Concat(new string[]
					{
						"this.lbl",
						columnName,
						".Text=model.",
						columnName,
						".ToString();"
					}));
					continue;
				case "bool":
					stringPlus.AppendSpaceLine(2, string.Concat(new string[]
					{
						"this.lbl",
						columnName,
						".Text=model.",
						columnName,
						"?\"是\":\"否\";"
					}));
					continue;
				case "byte[]":
					stringPlus.AppendSpaceLine(2, string.Concat(new string[]
					{
						"this.lbl",
						columnName,
						".Text=model.",
						columnName,
						".ToString();"
					}));
					continue;
				case "guid":
				case "uniqueidentifier":
					stringPlus.AppendSpaceLine(2, string.Concat(new string[]
					{
						"this.lbl",
						columnName,
						".Text=model.",
						columnName,
						".ToString();"
					}));
					continue;
				}
				stringPlus.AppendSpaceLine(2, string.Concat(new string[]
				{
					"this.lbl",
					columnName,
					".Text=model.",
					columnName,
					";"
				}));
			}
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(1, "}");
			return stringPlus.ToString();
		}
		public string GetListAspxCs()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpace(2, this.BLLSpace + " bll = new " + this.BLLSpace + "();");
			return stringPlus.ToString();
		}
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
		public string CreatSearchForm()
		{
			StringPlus stringPlus = new StringPlus();
			return stringPlus.Value;
		}
		public string GetAddDesigner()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string arg_3B_0 = current.Description;
				bool arg_42_0 = current.IsPrimaryKey;
				if (!current.IsIdentity && !this.isFilterColume(columnName) && !("uniqueidentifier" == typeName.ToLower()))
				{
					string a;
					if ((a = CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower()) != null)
					{
						if (a == "datetime" || a == "smalldatetime")
						{
							stringPlus.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
							continue;
						}
						if (a == "bool")
						{
							stringPlus.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.CheckBox chk" + columnName + ";");
							continue;
						}
					}
					stringPlus.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
				}
			}
			stringPlus.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnSave;");
			stringPlus.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnCancel;");
			return stringPlus.ToString();
		}
		public string GetUpdateDesigner()
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string descText = current.Description;
				bool isPrimaryKey = current.IsPrimaryKey;
				bool isIdentity = current.IsIdentity;
				descText = CodeCommon.CutDescText(descText, 15, columnName);
				if (!this.isFilterColume(columnName))
				{
					if (isPrimaryKey || isIdentity || typeName.Trim().ToLower() == "uniqueidentifier")
					{
						stringPlus.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Label lbl" + columnName + ";");
					}
					else
					{
						string a;
						if ((a = CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower()) != null)
						{
							if (a == "datetime" || a == "smalldatetime")
							{
								stringPlus.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
								continue;
							}
							if (a == "bool")
							{
								stringPlus.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.CheckBox chk" + columnName + ";");
								continue;
							}
						}
						stringPlus.AppendSpaceLine(2, "protected global::System.Web.UI.WebControls.TextBox txt" + columnName + ";");
					}
				}
			}
			stringPlus.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnSave;");
			stringPlus.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Button btnCancel;");
			return stringPlus.Value;
		}
		public string GetShowDesigner()
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string descText = current.Description;
				descText = CodeCommon.CutDescText(descText, 15, columnName);
				CodeCommon.DbTypeToCS(typeName.Trim().ToLower()).ToLower();
				stringPlus.AppendSpaceLine(1, "protected global::System.Web.UI.WebControls.Label lbl" + columnName + ";");
			}
			return stringPlus.ToString();
		}
		public string GetListDesigner()
		{
			StringPlus stringPlus = new StringPlus();
			return stringPlus.ToString();
		}
	}
}
