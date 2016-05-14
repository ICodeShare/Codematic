using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public class CodeCompletionListView : UserControl
	{
		private ICompletionData[] completionData;
		private int firstItem;
		private int selectedItem;
		private ImageList imageList;
        public event EventHandler SelectedItemChanged;
        public event EventHandler FirstItemChanged;
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				this.imageList = value;
			}
		}
		public int FirstItem
		{
			get
			{
				return this.firstItem;
			}
			set
			{
				this.firstItem = value;
				this.OnFirstItemChanged(EventArgs.Empty);
			}
		}
		public ICompletionData SelectedCompletionData
		{
			get
			{
				if (this.selectedItem < 0)
				{
					return null;
				}
				return this.completionData[this.selectedItem];
			}
		}
		public int ItemHeight
		{
			get
			{
				return Math.Max(this.imageList.ImageSize.Height, (int)((double)this.Font.Height * 1.25));
			}
		}
		public int MaxVisibleItem
		{
			get
			{
				return base.Height / this.ItemHeight;
			}
		}
		public CodeCompletionListView(ICompletionData[] completionData)
		{
			if (this.completionData != null)
			{
				Array.Clear(this.completionData, 0, completionData.Length);
			}
			Array.Sort<ICompletionData>(completionData);
			this.completionData = completionData;
		}
		public void Close()
		{
			if (this.completionData != null)
			{
				Array.Clear(this.completionData, 0, this.completionData.Length);
			}
			base.Dispose();
		}
		public void SelectIndex(int index)
		{
			index = Math.Max(0, index);
			int num = this.selectedItem;
			int num2 = this.firstItem;
			this.selectedItem = Math.Max(0, Math.Min(this.completionData.Length - 1, index));
			if (this.selectedItem < this.firstItem)
			{
				this.FirstItem = this.selectedItem;
			}
			if (this.firstItem + this.MaxVisibleItem <= this.selectedItem)
			{
				this.FirstItem = this.selectedItem - this.MaxVisibleItem + 1;
			}
			if (num != this.selectedItem)
			{
				if (this.firstItem != num2)
				{
					base.Invalidate();
				}
				else
				{
					int num3 = Math.Min(this.selectedItem, num) - this.firstItem;
					int num4 = Math.Max(this.selectedItem, num) - this.firstItem;
					base.Invalidate(new Rectangle(0, 1 + num3 * this.ItemHeight, base.Width, (num4 - num3 + 1) * this.ItemHeight));
				}
				base.Update();
				this.OnSelectedItemChanged(EventArgs.Empty);
			}
		}
		public void PageDown()
		{
			this.SelectIndex(this.selectedItem + this.MaxVisibleItem);
		}
		public void PageUp()
		{
			this.SelectIndex(this.selectedItem - this.MaxVisibleItem);
		}
		public void SelectNextItem()
		{
			this.SelectIndex(this.selectedItem + 1);
		}
		public void SelectPrevItem()
		{
			this.SelectIndex(this.selectedItem - 1);
		}
		public void SelectItemWithStart(char startCh)
		{
			for (int i = Math.Min(this.selectedItem + 1, this.completionData.Length - 1); i < this.completionData.Length; i++)
			{
				if (this.completionData[i].Text[0].ToLower()[0] == startCh)
				{
					this.SelectIndex(i);
					return;
				}
			}
			for (int j = 0; j < this.selectedItem; j++)
			{
				if (this.completionData[j].Text[0].ToLower()[0] == startCh)
				{
					this.SelectIndex(j);
					return;
				}
			}
			this.Refresh();
			this.OnSelectedItemChanged(EventArgs.Empty);
		}
		public void SelectItemWithStart(string startText)
		{
			startText = startText.ToLower();
			for (int i = 0; i < this.completionData.Length; i++)
			{
				if (this.completionData[i].Text[0].ToLower().StartsWith(startText))
				{
					this.SelectIndex(i);
					return;
				}
			}
			this.selectedItem = -1;
			this.Refresh();
			this.OnSelectedItemChanged(EventArgs.Empty);
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			float num = 1f;
			float num2 = (float)this.ItemHeight;
			int num3 = (int)(num2 * (float)this.imageList.ImageSize.Width / (float)this.imageList.ImageSize.Height);
			int num4 = this.firstItem;
			Graphics graphics = pe.Graphics;
			while (num4 < this.completionData.Length && num < (float)base.Height)
			{
				RectangleF rect = new RectangleF(1f, num, (float)(base.Width - 2), num2);
				if (rect.IntersectsWith(pe.ClipRectangle))
				{
					if (num4 == this.selectedItem)
					{
						graphics.FillRectangle(SystemBrushes.Highlight, rect);
					}
					else
					{
						graphics.FillRectangle(SystemBrushes.Window, rect);
					}
					int num5 = 0;
					if (this.imageList != null && this.completionData[num4].ImageIndex < this.imageList.Images.Count)
					{
						graphics.DrawImage(this.imageList.Images[this.completionData[num4].ImageIndex], new RectangleF(1f, num, (float)num3, num2));
						num5 = num3;
					}
					if (num4 == this.selectedItem)
					{
						graphics.DrawString(this.completionData[num4].Text[0], this.Font, SystemBrushes.HighlightText, (float)num5, num);
					}
					else
					{
						graphics.DrawString(this.completionData[num4].Text[0], this.Font, SystemBrushes.WindowText, (float)num5, num);
					}
				}
				num += num2;
				num4++;
			}
			graphics.DrawRectangle(SystemPens.Control, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			float num = 1f;
			int num2 = this.firstItem;
			float num3 = (float)this.ItemHeight;
			while (num2 < this.completionData.Length && num < (float)base.Height)
			{
				RectangleF rectangleF = new RectangleF(1f, num, (float)(base.Width - 2), num3);
				if (rectangleF.Contains((float)e.X, (float)e.Y))
				{
					this.SelectIndex(num2);
					return;
				}
				num += num3;
				num2++;
			}
		}
		protected override void OnMouseWheel(MouseEventArgs mea)
		{
			int i;
			for (i = mea.Delta * SystemInformation.MouseWheelScrollLines / 120; i > 0; i--)
			{
				this.SelectPrevItem();
			}
			while (i < 0)
			{
				this.SelectNextItem();
				i++;
			}
		}
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
		}
		protected virtual void OnSelectedItemChanged(EventArgs e)
		{
			if (this.SelectedItemChanged != null)
			{
				this.SelectedItemChanged(this, e);
			}
		}
		protected virtual void OnFirstItemChanged(EventArgs e)
		{
			if (this.FirstItemChanged != null)
			{
				this.FirstItemChanged(this, e);
			}
		}
	}
}
