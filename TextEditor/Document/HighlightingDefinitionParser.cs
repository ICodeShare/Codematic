using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
namespace LTP.TextEditor.Document
{
	internal class HighlightingDefinitionParser
	{
		private static ArrayList errors;
		private HighlightingDefinitionParser()
		{
		}
		public static DefaultHighlightingStrategy Parse(SyntaxMode syntaxMode, XmlTextReader xmlTextReader)
		{
			DefaultHighlightingStrategy result;
			try
			{
				XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(xmlTextReader);
				Stream baseStream = new StreamReader(Application.StartupPath + "\\TextStyle\\Mode.xsd", Encoding.Default).BaseStream;
				xmlValidatingReader.Schemas.Add("", new XmlTextReader(baseStream));
				xmlValidatingReader.ValidationType = ValidationType.Schema;
				xmlValidatingReader.ValidationEventHandler += new ValidationEventHandler(HighlightingDefinitionParser.ValidationHandler);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlValidatingReader);
				DefaultHighlightingStrategy defaultHighlightingStrategy = new DefaultHighlightingStrategy(xmlDocument.DocumentElement.Attributes["name"].InnerText);
				if (xmlDocument.DocumentElement.Attributes["extensions"] != null)
				{
					defaultHighlightingStrategy.Extensions = xmlDocument.DocumentElement.Attributes["extensions"].InnerText.Split(new char[]
					{
						';',
						'|'
					});
				}
				XmlElement xmlElement = xmlDocument.DocumentElement["Environment"];
				if (xmlElement != null)
				{
					foreach (XmlNode xmlNode in xmlElement.ChildNodes)
					{
						if (xmlNode is XmlElement)
						{
							XmlElement xmlElement2 = (XmlElement)xmlNode;
							defaultHighlightingStrategy.SetColorFor(xmlElement2.Name, xmlElement2.HasAttribute("bgcolor") ? new HighlightBackground(xmlElement2) : new HighlightColor(xmlElement2));
						}
					}
				}
				if (xmlDocument.DocumentElement["Properties"] != null)
				{
					foreach (XmlElement xmlElement3 in xmlDocument.DocumentElement["Properties"].ChildNodes)
					{
						defaultHighlightingStrategy.Properties[xmlElement3.Attributes["name"].InnerText] = xmlElement3.Attributes["value"].InnerText;
					}
				}
				if (xmlDocument.DocumentElement["Digits"] != null)
				{
					defaultHighlightingStrategy.DigitColor = new HighlightColor(xmlDocument.DocumentElement["Digits"]);
				}
				XmlNodeList elementsByTagName = xmlDocument.DocumentElement.GetElementsByTagName("RuleSet");
				foreach (XmlElement el in elementsByTagName)
				{
					defaultHighlightingStrategy.AddRuleSet(new HighlightRuleSet(el));
				}
				xmlTextReader.Close();
				if (HighlightingDefinitionParser.errors != null)
				{
					HighlightingDefinitionParser.ReportErrors(syntaxMode.FileName);
					HighlightingDefinitionParser.errors = null;
					result = null;
				}
				else
				{
					result = defaultHighlightingStrategy;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not load mode definition file.\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				result = null;
			}
			return result;
		}
		private static void ValidationHandler(object sender, ValidationEventArgs args)
		{
			if (HighlightingDefinitionParser.errors == null)
			{
				HighlightingDefinitionParser.errors = new ArrayList();
			}
			HighlightingDefinitionParser.errors.Add(args);
		}
		private static void ReportErrors(string fileName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Could not load mode definition file. Reason:\n\n");
			foreach (ValidationEventArgs validationEventArgs in HighlightingDefinitionParser.errors)
			{
				stringBuilder.Append(validationEventArgs.Message);
				stringBuilder.Append(Console.Out.NewLine);
			}
			MessageBox.Show(stringBuilder.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
		}
	}
}
