using System;
using System.Text;
namespace Maticsoft.Utility
{
	public class StringPlus
	{
		private StringBuilder str;
		public string Value
		{
			get
			{
				return this.str.ToString();
			}
		}
		public StringPlus()
		{
			this.str = new StringBuilder();
		}
		public string Space(int SpaceNum)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < SpaceNum; i++)
			{
				stringBuilder.Append("\t");
			}
			return stringBuilder.ToString();
		}
		public string Append(string Text)
		{
			this.str.Append(Text);
			return this.str.ToString();
		}
		public string AppendLine()
		{
			this.str.Append("\r\n");
			return this.str.ToString();
		}
		public string AppendLine(string Text)
		{
			this.str.Append(Text + "\r\n");
			return this.str.ToString();
		}
		public string AppendSpace(int SpaceNum, string Text)
		{
			this.str.Append(this.Space(SpaceNum));
			this.str.Append(Text);
			return this.str.ToString();
		}
		public string AppendSpaceLine(int SpaceNum, string Text)
		{
			this.str.Append(this.Space(SpaceNum));
			this.str.Append(Text);
			this.str.Append("\r\n");
			return this.str.ToString();
		}
		public override string ToString()
		{
			return this.str.ToString();
		}
		public void DelLastComma()
		{
			string text = this.str.ToString();
			int num = text.LastIndexOf(",");
			if (num > 0)
			{
				this.str = new StringBuilder();
				this.str.Append(text.Substring(0, num));
			}
		}
		public void DelLastChar(string strchar)
		{
			string text = this.str.ToString();
			int num = text.LastIndexOf(strchar);
			if (num > 0)
			{
				this.str = new StringBuilder();
				this.str.Append(text.Substring(0, num));
			}
		}
		public void Remove(int Start, int Num)
		{
			this.str.Remove(Start, Num);
		}
	}
}
