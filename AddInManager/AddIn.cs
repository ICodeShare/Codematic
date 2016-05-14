using Maticsoft.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Maticsoft.AddInManager
{
	public class AddIn
	{
		private string fileAddin = Application.StartupPath + "\\CodeDAL.addin";
		private Cache cache = new Cache();
		private string _guid;
		private string _name;
		private string _desc;
		private string _assembly;
		private string _classname;
		private string _version;
		public string Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				this._guid = value;
			}
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string Decription
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}
		public string Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}
		public string Classname
		{
			get
			{
				return this._classname;
			}
			set
			{
				this._classname = value;
			}
		}
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}
		public AddIn()
		{
		}
		public AddIn(string AssemblyGuid)
		{
			if (this.cache.GetObject(AssemblyGuid) == null)
			{
				try
				{
					object addIn = this.GetAddIn(AssemblyGuid);
					if (addIn != null)
					{
						this.cache.SaveCache(AssemblyGuid, addIn);
						DataRow dataRow = (DataRow)addIn;
						this._guid = dataRow["Guid"].ToString();
						this._name = dataRow["Name"].ToString();
						this._desc = dataRow["Decription"].ToString();
						this._assembly = dataRow["Assembly"].ToString();
						this._classname = dataRow["Classname"].ToString();
						this._version = dataRow["Version"].ToString();
					}
				}
				catch (Exception ex)
				{
					string arg_E8_0 = ex.Message;
				}
			}
		}
		public DataSet GetAddInList()
		{
			DataSet result;
			try
			{
				DataSet dataSet = new DataSet();
				if (File.Exists(this.fileAddin))
				{
					dataSet.ReadXml(this.fileAddin);
					if (dataSet.Tables.Count > 0)
					{
						result = dataSet;
						return result;
					}
				}
				result = null;
			}
			catch (SystemException ex)
			{
				string arg_3D_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public DataSet GetAddInList(string InterfaceName)
		{
			DataSet result;
			try
			{
				DataSet dataSet = new DataSet();
				if (File.Exists(this.fileAddin))
				{
					dataSet.ReadXml(this.fileAddin);
					if (dataSet.Tables.Count > 0)
					{
						List<DataRow> list = new List<DataRow>();
						foreach (DataRow dataRow in dataSet.Tables[0].Rows)
						{
							string assemblyString = dataRow["Assembly"].ToString();
							bool flag = false;
							try
							{
								Assembly assembly = System.Reflection.Assembly.Load(assemblyString);
								Type[] types = assembly.GetTypes();
								Type[] array = types;
								for (int i = 0; i < array.Length; i++)
								{
									Type type = array[i];
									Type[] interfaces = type.GetInterfaces();
									Type[] array2 = interfaces;
									for (int j = 0; j < array2.Length; j++)
									{
										Type type2 = array2[j];
										if (type2.FullName == InterfaceName)
										{
											flag = true;
										}
									}
								}
							}
							catch
							{
							}
							if (!flag)
							{
								list.Add(dataRow);
							}
						}
						foreach (DataRow current in list)
						{
							dataSet.Tables[0].Rows.Remove(current);
						}
						result = dataSet;
						return result;
					}
				}
				result = null;
			}
			catch (SystemException ex)
			{
				string arg_16A_0 = ex.Message;
				result = null;
			}
			return result;
		}
		private DataRow GetAddIn(string AssemblyGuid)
		{
			DataSet addInList = this.GetAddInList();
			if (addInList.Tables.Count > 0)
			{
				DataRow[] array = addInList.Tables[0].Select("Guid='" + AssemblyGuid + "'");
				if (array.Length > 0)
				{
					return array[0];
				}
			}
			return null;
		}
		public DataRow GetAddInByCache(string AssemblyGuid)
		{
			object obj = this.cache.GetObject(AssemblyGuid);
			if (obj == null)
			{
				try
				{
					obj = this.GetAddIn(AssemblyGuid);
					this.cache.SaveCache(AssemblyGuid, obj);
				}
				catch (Exception ex)
				{
					string arg_2E_0 = ex.Message;
				}
			}
			return (DataRow)obj;
		}
		public void AddAddIn()
		{
			DataSet dataSet = new DataSet();
			if (File.Exists(this.fileAddin))
			{
				dataSet.ReadXml(this.fileAddin);
				if (dataSet.Tables.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].NewRow();
					dataRow["Guid"] = this._guid;
					dataRow["Name"] = this._name;
					dataRow["Decription"] = this._desc;
					dataRow["Assembly"] = this._assembly;
					dataRow["Classname"] = this._classname;
					dataRow["Version"] = this._version;
					dataSet.Tables[0].Rows.Add(dataRow);
					XmlTextWriter xmlTextWriter = new XmlTextWriter(this.fileAddin, Encoding.Default);
					xmlTextWriter.WriteStartDocument();
					dataSet.WriteXml(xmlTextWriter);
					xmlTextWriter.Close();
				}
			}
		}
		public void DeleteAddIn(string AssemblyGuid)
		{
			DataSet addInList = this.GetAddInList();
			if (addInList.Tables.Count > 0)
			{
				addInList.Tables[0].Select("Guid='" + AssemblyGuid + "'")[0].Delete();
				addInList.WriteXml(this.fileAddin);
			}
		}
		public string LoadFile()
		{
			StreamReader streamReader = new StreamReader(this.fileAddin, Encoding.Default);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}
	}
}
