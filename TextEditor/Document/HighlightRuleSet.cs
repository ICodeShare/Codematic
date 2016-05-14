using LTP.TextEditor.Util;
using System;
using System.Collections;
using System.Xml;
namespace LTP.TextEditor.Document
{
	public class HighlightRuleSet
	{
		private LookupTable keyWords;
		private ArrayList spans = new ArrayList();
		private LookupTable prevMarkers;
		private LookupTable nextMarkers;
		private IHighlightingStrategy highlighter;
		private bool noEscapeSequences;
		private bool ignoreCase;
		private string name;
		private bool[] delimiters = new bool[256];
		private string reference;
		public ArrayList Spans
		{
			get
			{
				return this.spans;
			}
		}
		internal IHighlightingStrategy Highlighter
		{
			get
			{
				return this.highlighter;
			}
			set
			{
				this.highlighter = value;
			}
		}
		public LookupTable KeyWords
		{
			get
			{
				return this.keyWords;
			}
		}
		public LookupTable PrevMarkers
		{
			get
			{
				return this.prevMarkers;
			}
		}
		public LookupTable NextMarkers
		{
			get
			{
				return this.nextMarkers;
			}
		}
		public bool[] Delimiters
		{
			get
			{
				return this.delimiters;
			}
		}
		public bool NoEscapeSequences
		{
			get
			{
				return this.noEscapeSequences;
			}
		}
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
		}
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}
		public string Reference
		{
			get
			{
				return this.reference;
			}
		}
		public HighlightRuleSet()
		{
			this.keyWords = new LookupTable(false);
			this.prevMarkers = new LookupTable(false);
			this.nextMarkers = new LookupTable(false);
		}
		public HighlightRuleSet(XmlElement el)
		{
			XmlNodeList elementsByTagName = el.GetElementsByTagName("KeyWords");
			if (el.Attributes["name"] != null)
			{
				this.Name = el.Attributes["name"].InnerText;
			}
			if (el.Attributes["noescapesequences"] != null)
			{
				this.noEscapeSequences = bool.Parse(el.Attributes["noescapesequences"].InnerText);
			}
			if (el.Attributes["reference"] != null)
			{
				this.reference = el.Attributes["reference"].InnerText;
			}
			if (el.Attributes["ignorecase"] != null)
			{
				this.ignoreCase = bool.Parse(el.Attributes["ignorecase"].InnerText);
			}
			for (int i = 0; i < this.Delimiters.Length; i++)
			{
				this.Delimiters[i] = false;
			}
			if (el["Delimiters"] != null)
			{
				string innerText = el["Delimiters"].InnerText;
				string text = innerText;
				for (int j = 0; j < text.Length; j++)
				{
					char c = text[j];
					this.Delimiters[(int)c] = true;
				}
			}
			this.keyWords = new LookupTable(!this.IgnoreCase);
			this.prevMarkers = new LookupTable(!this.IgnoreCase);
			this.nextMarkers = new LookupTable(!this.IgnoreCase);
			foreach (XmlElement xmlElement in elementsByTagName)
			{
				HighlightColor value = new HighlightColor(xmlElement);
				XmlNodeList elementsByTagName2 = xmlElement.GetElementsByTagName("Key");
				foreach (XmlElement xmlElement2 in elementsByTagName2)
				{
					this.keyWords[xmlElement2.Attributes["word"].InnerText] = value;
				}
			}
			elementsByTagName = el.GetElementsByTagName("Span");
			foreach (XmlElement span in elementsByTagName)
			{
				this.Spans.Add(new Span(span));
			}
			elementsByTagName = el.GetElementsByTagName("MarkPrevious");
			foreach (XmlElement mark in elementsByTagName)
			{
				PrevMarker prevMarker = new PrevMarker(mark);
				this.prevMarkers[prevMarker.What] = prevMarker;
			}
			elementsByTagName = el.GetElementsByTagName("MarkFollowing");
			foreach (XmlElement mark2 in elementsByTagName)
			{
				NextMarker nextMarker = new NextMarker(mark2);
				this.nextMarkers[nextMarker.What] = nextMarker;
			}
		}
	}
}
