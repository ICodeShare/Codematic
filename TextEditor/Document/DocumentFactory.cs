using System;
using System.IO;
namespace LTP.TextEditor.Document
{
	public class DocumentFactory
	{
		public IDocument CreateDocument()
		{
			DefaultDocument defaultDocument = new DefaultDocument();
			defaultDocument.TextBufferStrategy = new GapTextBufferStrategy();
			defaultDocument.FormattingStrategy = new DefaultFormattingStrategy();
			defaultDocument.LineManager = new DefaultLineManager(defaultDocument, null);
			defaultDocument.FoldingManager = new FoldingManager(defaultDocument, defaultDocument.LineManager);
			defaultDocument.FoldingManager.FoldingStrategy = new ParserFoldingStrategy();
			defaultDocument.MarkerStrategy = new MarkerStrategy(defaultDocument);
			defaultDocument.BookmarkManager = new BookmarkManager(defaultDocument.LineManager);
			defaultDocument.CustomLineManager = new CustomLineManager(defaultDocument.LineManager);
			return defaultDocument;
		}
		public IDocument CreateFromFile(string fileName)
		{
			IDocument document = this.CreateDocument();
			StreamReader streamReader = File.OpenText(fileName);
			document.TextContent = streamReader.ReadToEnd();
			streamReader.Close();
			return document;
		}
	}
}
