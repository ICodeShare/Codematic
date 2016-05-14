using Maticsoft.AddInManager;
using Maticsoft.IBuilder;
using Maticsoft.Utility;
using System;
using System.Reflection;
namespace Maticsoft.CodeBuild
{
	public class BuilderFactory
	{
		private static Cache cache = new Cache();
		private static object CreateObject(string path, string TypeName)
		{
			object obj = BuilderFactory.cache.GetObject(TypeName);
			if (obj == null)
			{
				try
				{
					obj = Assembly.Load(path).CreateInstance(TypeName, true);
					BuilderFactory.cache.SaveCache(TypeName, obj);
				}
				catch (Exception ex)
				{
					string arg_32_0 = ex.Message;
				}
			}
			return obj;
		}
		public static IBuilderDAL CreateDALObj(string AssemblyGuid)
		{
			IBuilderDAL result;
			try
			{
				if (AssemblyGuid == "")
				{
					result = null;
				}
				else
				{
					AddIn addIn = new AddIn(AssemblyGuid);
					string assembly = addIn.Assembly;
					string classname = addIn.Classname;
					object obj = BuilderFactory.CreateObject(assembly, classname);
					result = (IBuilderDAL)obj;
				}
			}
			catch (SystemException ex)
			{
				string arg_42_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public static IBuilderDALTran CreateDALTranObj(string AssemblyGuid)
		{
			IBuilderDALTran result;
			try
			{
				if (AssemblyGuid == "")
				{
					result = null;
				}
				else
				{
					AddIn addIn = new AddIn(AssemblyGuid);
					string assembly = addIn.Assembly;
					string classname = addIn.Classname;
					object obj = BuilderFactory.CreateObject(assembly, classname);
					result = (IBuilderDALTran)obj;
				}
			}
			catch (SystemException ex)
			{
				string arg_42_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public static IBuilderDALMTran CreateDALMTranObj(string AssemblyGuid)
		{
			IBuilderDALMTran result;
			try
			{
				if (AssemblyGuid == "")
				{
					result = null;
				}
				else
				{
					AddIn addIn = new AddIn(AssemblyGuid);
					string assembly = addIn.Assembly;
					string classname = addIn.Classname;
					object obj = BuilderFactory.CreateObject(assembly, classname);
					result = (IBuilderDALMTran)obj;
				}
			}
			catch (SystemException ex)
			{
				string arg_42_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public static IBuilderIDAL CreateIDALObj()
		{
			IBuilderIDAL result;
			try
			{
				object obj = BuilderFactory.CreateObject("Maticsoft.BuilderIDAL", "Maticsoft.BuilderIDAL.BuilderIDAL");
				result = (IBuilderIDAL)obj;
			}
			catch (SystemException ex)
			{
				string arg_20_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public static IBuilderBLL CreateBLLObj(string AssemblyGuid)
		{
			IBuilderBLL result;
			try
			{
				if (AssemblyGuid == "")
				{
					result = null;
				}
				else
				{
					AddIn addIn = new AddIn(AssemblyGuid);
					string assembly = addIn.Assembly;
					string classname = addIn.Classname;
					object obj = BuilderFactory.CreateObject(assembly, classname);
					result = (IBuilderBLL)obj;
				}
			}
			catch (SystemException ex)
			{
				string arg_42_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public static IBuilderModel CreateModelObj(string AssemblyGuid)
		{
			IBuilderModel result;
			try
			{
				if (AssemblyGuid == "")
				{
					result = null;
				}
				else
				{
					AddIn addIn = new AddIn(AssemblyGuid);
					string assembly = addIn.Assembly;
					string classname = addIn.Classname;
					object obj = BuilderFactory.CreateObject(assembly, classname);
					result = (IBuilderModel)obj;
				}
			}
			catch (SystemException ex)
			{
				string arg_42_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public static IBuilderWeb CreateWebObj(string AssemblyGuid)
		{
			IBuilderWeb result;
			try
			{
				if (AssemblyGuid == "")
				{
					result = null;
				}
				else
				{
					AddIn addIn = new AddIn(AssemblyGuid);
					string assembly = addIn.Assembly;
					string classname = addIn.Classname;
					object obj = BuilderFactory.CreateObject(assembly, classname);
					result = (IBuilderWeb)obj;
				}
			}
			catch (SystemException ex)
			{
				string arg_42_0 = ex.Message;
				result = null;
			}
			return result;
		}
	}
}
