using LTP.TextEditor.Document;
using System;
namespace LTP.TextEditor.Util
{
	public class LookupTable
	{
		private class Node
		{
			public string word;
			public object color;
			public LookupTable.Node[] leaf = new LookupTable.Node[256];
			public Node(object color, string word)
			{
				this.word = word;
				this.color = color;
			}
		}
		private LookupTable.Node root = new LookupTable.Node(null, null);
		private bool casesensitive;
		private int length;
		public int Count
		{
			get
			{
				return this.length;
			}
		}
		public object this[IDocument document, LineSegment line, int offset, int length]
		{
			get
			{
				if (length == 0)
				{
					return null;
				}
				LookupTable.Node node = this.root;
				int num = line.Offset + offset;
				if (this.casesensitive)
				{
					for (int i = 0; i < length; i++)
					{
						int num2 = (int)(document.GetCharAt(num + i) % 'Ā');
						node = node.leaf[num2];
						if (node == null)
						{
							return null;
						}
						if (node.color != null && TextUtility.RegionMatches(document, num, length, node.word))
						{
							return node.color;
						}
					}
				}
				else
				{
					for (int j = 0; j < length; j++)
					{
						int num3 = (int)(char.ToUpper(document.GetCharAt(num + j)) % 'Ā');
						node = node.leaf[num3];
						if (node == null)
						{
							return null;
						}
						if (node.color != null && TextUtility.RegionMatches(document, this.casesensitive, num, length, node.word))
						{
							return node.color;
						}
					}
				}
				return null;
			}
		}
		public object this[string keyword]
		{
			set
			{
				LookupTable.Node node = this.root;
				LookupTable.Node node2 = this.root;
				if (!this.casesensitive)
				{
					keyword = keyword.ToUpper();
				}
				this.length++;
				for (int i = 0; i < keyword.Length; i++)
				{
					int num = (int)(keyword[i] % 'Ā');
					char arg_48_0 = keyword[i];
					node2 = node2.leaf[num];
					if (node2 == null)
					{
						node.leaf[num] = new LookupTable.Node(value, keyword);
						return;
					}
					if (node2.word != null && node2.word.Length != i)
					{
						string word = node2.word;
						object color = node2.color;
						node2.color = (node2.word = null);
						this[word] = color;
					}
					if (i == keyword.Length - 1)
					{
						node2.word = keyword;
						node2.color = value;
						return;
					}
					node = node2;
				}
			}
		}
		public LookupTable(bool casesensitive)
		{
			this.casesensitive = casesensitive;
		}
	}
}
