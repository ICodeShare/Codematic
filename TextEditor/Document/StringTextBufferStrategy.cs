using System;
using System.IO;
namespace LTP.TextEditor.Document
{
	public class StringTextBufferStrategy : ITextBufferStrategy
	{
		private string storedText = "";
		public int Length
		{
			get
			{
				return this.storedText.Length;
			}
		}
		public void Insert(int offset, string text)
		{
			if (text != null)
			{
				this.storedText = this.storedText.Insert(offset, text);
			}
		}
		public void Remove(int offset, int length)
		{
			this.storedText = this.storedText.Remove(offset, length);
		}
		public void Replace(int offset, int length, string text)
		{
			this.Remove(offset, length);
			this.Insert(offset, text);
		}
		public string GetText(int offset, int length)
		{
			if (length == 0)
			{
				return "";
			}
			return this.storedText.Substring(offset, Math.Min(length, this.storedText.Length - offset));
		}
		public char GetCharAt(int offset)
		{
			if (offset == this.Length)
			{
				return '\0';
			}
			return this.storedText[offset];
		}
		public void SetContent(string text)
		{
			this.storedText = text;
		}
		public StringTextBufferStrategy()
		{
		}
		private StringTextBufferStrategy(string fileName)
		{
			StreamReader streamReader = File.OpenText(fileName);
			this.SetContent(streamReader.ReadToEnd());
			streamReader.Close();
		}
		public static ITextBufferStrategy CreateTextBufferFromFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName);
			}
			return new StringTextBufferStrategy(fileName);
		}
	}
}
