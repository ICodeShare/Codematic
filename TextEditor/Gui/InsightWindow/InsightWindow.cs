using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Util;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor.Gui.InsightWindow
{
	public class InsightWindow : AbstractCompletionWindow
	{
		private class InsightDataProviderStackElement
		{
			public int currentData;
			public IInsightDataProvider dataProvider;
			public InsightDataProviderStackElement(IInsightDataProvider dataProvider)
			{
				this.currentData = 0;
				this.dataProvider = dataProvider;
			}
		}
		private Stack insightDataProviderStack = new Stack();
		private int CurrentData
		{
			get
			{
				return ((InsightWindow.InsightDataProviderStackElement)this.insightDataProviderStack.Peek()).currentData;
			}
			set
			{
				((InsightWindow.InsightDataProviderStackElement)this.insightDataProviderStack.Peek()).currentData = value;
			}
		}
		private IInsightDataProvider DataProvider
		{
			get
			{
				if (this.insightDataProviderStack.Count == 0)
				{
					return null;
				}
				return ((InsightWindow.InsightDataProviderStackElement)this.insightDataProviderStack.Peek()).dataProvider;
			}
		}
		public InsightWindow(Form parentForm, TextEditorControl control, string fileName) : base(parentForm, control, fileName)
		{
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
		}
		public void ShowInsightWindow()
		{
			if (!base.Visible)
			{
				if (this.insightDataProviderStack.Count > 0)
				{
					base.ShowCompletionWindow();
					return;
				}
			}
			else
			{
				this.Refresh();
			}
		}
		protected override bool ProcessTextAreaKey(Keys keyData)
		{
			if (!base.Visible)
			{
				return false;
			}
			switch (keyData)
			{
			case Keys.Up:
				if (this.DataProvider != null && this.DataProvider.InsightDataCount > 0)
				{
					this.CurrentData = (this.CurrentData + this.DataProvider.InsightDataCount - 1) % this.DataProvider.InsightDataCount;
					this.Refresh();
				}
				return true;
			case Keys.Down:
				if (this.DataProvider != null && this.DataProvider.InsightDataCount > 0)
				{
					this.CurrentData = (this.CurrentData + 1) % this.DataProvider.InsightDataCount;
					this.Refresh();
				}
				return true;
			}
			return base.ProcessTextAreaKey(keyData);
		}
		protected override void CaretOffsetChanged(object sender, EventArgs e)
		{
			Point position = this.control.ActiveTextAreaControl.Caret.Position;
			int arg_1D_0 = position.Y;
			int arg_38_0 = this.control.ActiveTextAreaControl.TextArea.TextView.FontHeight;
			int arg_57_0 = this.control.ActiveTextAreaControl.TextArea.VirtualTop.Y;
			int arg_7B_0 = this.control.ActiveTextAreaControl.TextArea.TextView.DrawingPosition.Y;
			int drawingXPos = this.control.ActiveTextAreaControl.TextArea.TextView.GetDrawingXPos(position.Y, position.X);
			int num = (this.control.ActiveTextAreaControl.Document.GetVisibleLine(position.Y) + 1) * this.control.ActiveTextAreaControl.TextArea.TextView.FontHeight - this.control.ActiveTextAreaControl.TextArea.VirtualTop.Y;
			int num2 = this.control.TextEditorProperties.ShowHorizontalRuler ? this.control.ActiveTextAreaControl.TextArea.TextView.FontHeight : 0;
			Point location = this.control.ActiveTextAreaControl.PointToScreen(new Point(drawingXPos, num + num2));
			if (location.Y != base.Location.Y)
			{
				base.Location = location;
			}
			while (this.DataProvider != null && this.DataProvider.CaretOffsetChanged())
			{
				this.CloseCurrentDataProvider();
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.control.ActiveTextAreaControl.TextArea.Focus();
			if (TipPainterTools.DrawingRectangle1.Contains(e.X, e.Y))
			{
				this.CurrentData = (this.CurrentData + this.DataProvider.InsightDataCount - 1) % this.DataProvider.InsightDataCount;
				this.Refresh();
			}
			if (TipPainterTools.DrawingRectangle2.Contains(e.X, e.Y))
			{
				this.CurrentData = (this.CurrentData + 1) % this.DataProvider.InsightDataCount;
				this.Refresh();
			}
		}
		public void HandleMouseWheel(MouseEventArgs e)
		{
			if (this.DataProvider != null && this.DataProvider.InsightDataCount > 0)
			{
				if (e.Delta > 0)
				{
					if (this.control.TextEditorProperties.MouseWheelScrollDown)
					{
						this.CurrentData = (this.CurrentData + 1) % this.DataProvider.InsightDataCount;
					}
					else
					{
						this.CurrentData = (this.CurrentData + this.DataProvider.InsightDataCount - 1) % this.DataProvider.InsightDataCount;
					}
				}
				if (e.Delta < 0)
				{
					if (this.control.TextEditorProperties.MouseWheelScrollDown)
					{
						this.CurrentData = (this.CurrentData + this.DataProvider.InsightDataCount - 1) % this.DataProvider.InsightDataCount;
					}
					else
					{
						this.CurrentData = (this.CurrentData + 1) % this.DataProvider.InsightDataCount;
					}
				}
				this.Refresh();
			}
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			string countMessage = null;
			string description;
			if (this.DataProvider == null || this.DataProvider.InsightDataCount < 1)
			{
				description = "Unknown Method";
			}
			else
			{
				if (this.DataProvider.InsightDataCount > 1)
				{
					countMessage = this.control.GetRangeDescription(this.CurrentData + 1, this.DataProvider.InsightDataCount);
				}
				description = this.DataProvider.GetInsightData(this.CurrentData);
			}
			this.drawingSize = TipPainterTools.GetDrawingSizeHelpTipFromCombinedDescription(this, pe.Graphics, this.Font, countMessage, description);
			if (this.drawingSize != base.Size)
			{
				this.SetLocation();
				return;
			}
			TipPainterTools.DrawHelpTipFromCombinedDescription(this, pe.Graphics, this.Font, countMessage, description);
		}
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
			pe.Graphics.FillRectangle(SystemBrushes.Info, pe.ClipRectangle);
		}
		public void AddInsightDataProvider(IInsightDataProvider provider)
		{
			provider.SetupDataProvider(this.fileName, this.control.ActiveTextAreaControl.TextArea);
			if (provider.InsightDataCount > 0)
			{
				this.insightDataProviderStack.Push(new InsightWindow.InsightDataProviderStackElement(provider));
			}
		}
		private void CloseCurrentDataProvider()
		{
			this.insightDataProviderStack.Pop();
			if (this.insightDataProviderStack.Count == 0)
			{
				base.Close();
				return;
			}
			this.Refresh();
		}
	}
}
