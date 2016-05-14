using System;
using System.Collections;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class BookmarkManagerMemento
	{
		private ArrayList bookmarks = new ArrayList();
		public ArrayList Bookmarks
		{
			get
			{
				return this.bookmarks;
			}
			set
			{
				this.bookmarks = value;
			}
		}
		public void CheckMemento(IDocument document)
		{
			for (int i = 0; i < this.bookmarks.Count; i++)
			{
				int num = (int)this.bookmarks[i];
				if (num < 0 || num >= document.TotalNumberOfLines)
				{
					this.bookmarks.RemoveAt(i);
					i--;
				}
			}
		}
		public BookmarkManagerMemento()
		{
		}
		public BookmarkManagerMemento(XmlElement element)
		{
			foreach (XmlElement xmlElement in element.ChildNodes)
			{
				this.bookmarks.Add(int.Parse(xmlElement.Attributes["line"].InnerText));
			}
		}
		public BookmarkManagerMemento(ArrayList bookmarks)
		{
			this.bookmarks = bookmarks;
		}
		public object FromXmlElement(XmlElement element)
		{
			return new BookmarkManagerMemento(element);
		}
		public XmlElement ToXmlElement(XmlDocument doc)
		{
			XmlElement xmlElement = doc.CreateElement("Bookmarks");
			foreach (int num in this.bookmarks)
			{
				XmlElement xmlElement2 = doc.CreateElement("Mark");
				XmlAttribute xmlAttribute = doc.CreateAttribute("line");
				xmlAttribute.InnerText = num.ToString();
				xmlElement2.Attributes.Append(xmlAttribute);
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}
	}
}
