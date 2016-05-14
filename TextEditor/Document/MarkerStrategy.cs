using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public class MarkerStrategy
	{
		private ArrayList textMarker = new ArrayList();
		private IDocument document;
		private Hashtable markersTable = new Hashtable();
		public IDocument Document
		{
			get
			{
				return this.document;
			}
		}
		public ArrayList TextMarker
		{
			get
			{
				return this.textMarker;
			}
		}
		public MarkerStrategy(IDocument document)
		{
			this.document = document;
			document.DocumentChanged += new DocumentEventHandler(this.DocumentChanged);
		}
		public ArrayList GetMarkers(int offset)
		{
			if (!this.markersTable.Contains(offset))
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this.textMarker.Count; i++)
				{
					TextMarker textMarker = (TextMarker)this.textMarker[i];
					if (textMarker.Offset <= offset && offset <= textMarker.Offset + textMarker.Length)
					{
						arrayList.Add(textMarker);
					}
				}
				this.markersTable[offset] = arrayList;
			}
			return (ArrayList)this.markersTable[offset];
		}
		public ArrayList GetMarkers(int offset, int length)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.textMarker.Count; i++)
			{
				TextMarker textMarker = (TextMarker)this.textMarker[i];
				if ((textMarker.Offset <= offset && offset <= textMarker.Offset + textMarker.Length) || (textMarker.Offset <= offset + length && offset + length <= textMarker.Offset + textMarker.Length) || (offset <= textMarker.Offset && textMarker.Offset <= offset + length) || (offset <= textMarker.Offset + textMarker.Length && textMarker.Offset + textMarker.Length <= offset + length))
				{
					arrayList.Add(textMarker);
				}
			}
			return arrayList;
		}
		public ArrayList GetMarkers(Point position)
		{
			if (position.Y >= this.document.TotalNumberOfLines || position.Y < 0)
			{
				return new ArrayList();
			}
			LineSegment lineSegment = this.document.GetLineSegment(position.Y);
			return this.GetMarkers(lineSegment.Offset + position.X);
		}
		private void DocumentChanged(object sender, DocumentEventArgs e)
		{
			this.markersTable.Clear();
			this.document.UpdateSegmentListOnDocumentChange(this.textMarker, e);
		}
	}
}
