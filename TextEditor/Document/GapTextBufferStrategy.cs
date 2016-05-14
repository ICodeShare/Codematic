using System;
using System.Text;
namespace LTP.TextEditor.Document
{
	public class GapTextBufferStrategy : ITextBufferStrategy
	{
		private char[] buffer = new char[0];
		private int gapBeginOffset;
		private int gapEndOffset;
		private int minGapLength = 32;
		private int maxGapLength = 256;
		public int Length
		{
			get
			{
				return this.buffer.Length - this.GapLength;
			}
		}
		private int GapLength
		{
			get
			{
				return this.gapEndOffset - this.gapBeginOffset;
			}
		}
		public void SetContent(string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}
			this.buffer = text.ToCharArray();
			this.gapBeginOffset = (this.gapEndOffset = 0);
		}
		public char GetCharAt(int offset)
		{
			if (offset >= this.gapBeginOffset)
			{
				return this.buffer[offset + this.GapLength];
			}
			return this.buffer[offset];
		}
		public string GetText(int offset, int length)
		{
			int num = offset + length;
			if (num < this.gapBeginOffset)
			{
				return new string(this.buffer, offset, length);
			}
			if (offset > this.gapBeginOffset)
			{
				return new string(this.buffer, offset + this.GapLength, length);
			}
			int num2 = this.gapBeginOffset - offset;
			int num3 = num - this.gapBeginOffset;
			StringBuilder stringBuilder = new StringBuilder(num2 + num3);
			stringBuilder.Append(this.buffer, offset, num2);
			stringBuilder.Append(this.buffer, this.gapEndOffset, num3);
			return stringBuilder.ToString();
		}
		public void Insert(int offset, string text)
		{
			this.Replace(offset, 0, text);
		}
		public void Remove(int offset, int length)
		{
			this.Replace(offset, length, string.Empty);
		}
		public void Replace(int offset, int length, string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}
			this.PlaceGap(offset + length, Math.Max(text.Length - length, 0));
			text.CopyTo(0, this.buffer, offset, text.Length);
			this.gapBeginOffset += text.Length - length;
		}
		private void PlaceGap(int offset, int length)
		{
			int num = this.GapLength - length;
			if (this.minGapLength > num || num > this.maxGapLength)
			{
				int gapLength = this.GapLength;
				int num2 = this.maxGapLength + length;
				int num3 = offset + num2;
				char[] array = new char[this.buffer.Length + num2 - gapLength];
				if (gapLength == 0)
				{
					Array.Copy(this.buffer, 0, array, 0, offset);
					Array.Copy(this.buffer, offset, array, num3, array.Length - num3);
				}
				else
				{
					if (offset < this.gapBeginOffset)
					{
						int num4 = this.gapBeginOffset - offset;
						Array.Copy(this.buffer, 0, array, 0, offset);
						Array.Copy(this.buffer, offset, array, num3, num4);
						Array.Copy(this.buffer, this.gapEndOffset, array, num3 + num4, this.buffer.Length - this.gapEndOffset);
					}
					else
					{
						int num5 = offset - this.gapBeginOffset;
						Array.Copy(this.buffer, 0, array, 0, this.gapBeginOffset);
						Array.Copy(this.buffer, this.gapEndOffset, array, this.gapBeginOffset, num5);
						Array.Copy(this.buffer, this.gapEndOffset + num5, array, num3, array.Length - num3);
					}
				}
				this.buffer = array;
				this.gapBeginOffset = offset;
				this.gapEndOffset = num3;
				return;
			}
			int num6 = this.gapBeginOffset - offset;
			if (offset == this.gapBeginOffset)
			{
				return;
			}
			if (offset < this.gapBeginOffset)
			{
				int num7 = this.gapEndOffset - this.gapBeginOffset;
				Array.Copy(this.buffer, offset, this.buffer, offset + num7, num6);
			}
			else
			{
				Array.Copy(this.buffer, this.gapEndOffset, this.buffer, this.gapBeginOffset, -num6);
			}
			this.gapBeginOffset -= num6;
			this.gapEndOffset -= num6;
		}
	}
}
