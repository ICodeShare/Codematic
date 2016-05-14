using LTP.TextEditor.Document;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	
	public class TextEditorControl : TextEditorControlBase
	{
		protected Panel textAreaPanel = new Panel();
		private TextAreaControl primaryTextArea;
		private Splitter textAreaSplitter;
		private TextAreaControl secondaryTextArea;
		private PrintDocument printDocument;
		private int curLineNr;
		private float curTabIndent;
		private StringFormat printingStringFormat;
		public PrintDocument PrintDocument
		{
			get
			{
				if (this.printDocument == null)
				{
					this.printDocument = new PrintDocument();
					this.printDocument.BeginPrint += new PrintEventHandler(this.BeginPrint);
					this.printDocument.PrintPage += new PrintPageEventHandler(this.PrintPage);
				}
				return this.printDocument;
			}
		}
		public override TextAreaControl ActiveTextAreaControl
		{
			get
			{
				return this.primaryTextArea;
			}
		}
		public bool EnableUndo
		{
			get
			{
				return base.Document.UndoStack.CanUndo;
			}
		}
		public bool EnableRedo
		{
			get
			{
				return base.Document.UndoStack.CanRedo;
			}
		}
		public TextEditorControl()
		{
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.Selectable, true);
			this.textAreaPanel.Dock = DockStyle.Fill;
			base.Document = new DocumentFactory().CreateDocument();
			base.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy();
			this.primaryTextArea = new TextAreaControl(this);
			this.primaryTextArea.Dock = DockStyle.Fill;
			this.textAreaPanel.Controls.Add(this.primaryTextArea);
			this.InitializeTextAreaControl(this.primaryTextArea);
			base.Controls.Add(this.textAreaPanel);
			base.ResizeRedraw = true;
			base.Document.UpdateCommited += new EventHandler(this.CommitUpdateRequested);
			this.OptionsChanged();
		}
		protected virtual void InitializeTextAreaControl(TextAreaControl newControl)
		{
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.printDocument != null)
				{
					this.printDocument.BeginPrint -= new PrintEventHandler(this.BeginPrint);
					this.printDocument.PrintPage -= new PrintPageEventHandler(this.PrintPage);
					this.printDocument = null;
				}
				base.Document.UndoStack.ClearAll();
				base.Document.UpdateCommited -= new EventHandler(this.CommitUpdateRequested);
				if (this.textAreaPanel != null)
				{
					if (this.secondaryTextArea != null)
					{
						this.secondaryTextArea.Dispose();
						this.textAreaSplitter.Dispose();
						this.secondaryTextArea = null;
						this.textAreaSplitter = null;
					}
					if (this.primaryTextArea != null)
					{
						this.primaryTextArea.Dispose();
					}
					this.textAreaPanel.Dispose();
					this.textAreaPanel = null;
				}
			}
			base.Dispose(disposing);
		}
		public override void OptionsChanged()
		{
			this.primaryTextArea.OptionsChanged();
			if (this.secondaryTextArea != null)
			{
				this.secondaryTextArea.OptionsChanged();
			}
		}
		public void Split()
		{
			if (this.secondaryTextArea == null)
			{
				this.secondaryTextArea = new TextAreaControl(this);
				this.secondaryTextArea.Dock = DockStyle.Bottom;
				this.secondaryTextArea.Height = base.Height / 2;
				this.textAreaSplitter = new Splitter();
				this.textAreaSplitter.BorderStyle = BorderStyle.FixedSingle;
				this.textAreaSplitter.Height = 8;
				this.textAreaSplitter.Dock = DockStyle.Bottom;
				this.textAreaPanel.Controls.Add(this.textAreaSplitter);
				this.textAreaPanel.Controls.Add(this.secondaryTextArea);
				this.InitializeTextAreaControl(this.secondaryTextArea);
				this.secondaryTextArea.OptionsChanged();
				return;
			}
			this.textAreaPanel.Controls.Remove(this.secondaryTextArea);
			this.textAreaPanel.Controls.Remove(this.textAreaSplitter);
			this.secondaryTextArea.Dispose();
			this.textAreaSplitter.Dispose();
			this.secondaryTextArea = null;
			this.textAreaSplitter = null;
		}
		public void Undo()
		{
			if (base.Document.ReadOnly)
			{
				return;
			}
			if (base.Document.UndoStack.CanUndo)
			{
				this.BeginUpdate();
				base.Document.UndoStack.Undo();
				base.Document.UpdateQueue.Clear();
				base.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
				this.primaryTextArea.TextArea.UpdateMatchingBracket();
				if (this.secondaryTextArea != null)
				{
					this.secondaryTextArea.TextArea.UpdateMatchingBracket();
				}
				this.EndUpdate();
			}
		}
		public void Redo()
		{
			if (base.Document.ReadOnly)
			{
				return;
			}
			if (base.Document.UndoStack.CanRedo)
			{
				this.BeginUpdate();
				base.Document.UndoStack.Redo();
				base.Document.UpdateQueue.Clear();
				base.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
				this.primaryTextArea.TextArea.UpdateMatchingBracket();
				if (this.secondaryTextArea != null)
				{
					this.secondaryTextArea.TextArea.UpdateMatchingBracket();
				}
				this.EndUpdate();
			}
		}
		public void SetHighlighting(string name)
		{
			base.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(name);
		}
		public override void EndUpdate()
		{
			base.EndUpdate();
			base.Document.CommitUpdate();
		}
		private void CommitUpdateRequested(object sender, EventArgs e)
		{
			if (base.IsUpdating)
			{
				return;
			}
			foreach (TextAreaUpdate textAreaUpdate in base.Document.UpdateQueue)
			{
				switch (textAreaUpdate.TextAreaUpdateType)
				{
				case TextAreaUpdateType.WholeTextArea:
					this.primaryTextArea.TextArea.Invalidate();
					if (this.secondaryTextArea != null)
					{
						this.secondaryTextArea.TextArea.Invalidate();
					}
					break;
				case TextAreaUpdateType.SingleLine:
				case TextAreaUpdateType.PositionToLineEnd:
					this.primaryTextArea.TextArea.UpdateLine(textAreaUpdate.Position.Y);
					if (this.secondaryTextArea != null)
					{
						this.secondaryTextArea.TextArea.UpdateLine(textAreaUpdate.Position.Y);
					}
					break;
				case TextAreaUpdateType.SinglePosition:
					this.primaryTextArea.TextArea.UpdateLine(textAreaUpdate.Position.Y, textAreaUpdate.Position.X, textAreaUpdate.Position.X);
					if (this.secondaryTextArea != null)
					{
						this.secondaryTextArea.TextArea.UpdateLine(textAreaUpdate.Position.Y, textAreaUpdate.Position.X, textAreaUpdate.Position.X);
					}
					break;
				case TextAreaUpdateType.PositionToEnd:
					this.primaryTextArea.TextArea.UpdateToEnd(textAreaUpdate.Position.Y);
					if (this.secondaryTextArea != null)
					{
						this.secondaryTextArea.TextArea.UpdateToEnd(textAreaUpdate.Position.Y);
					}
					break;
				case TextAreaUpdateType.LinesBetween:
					this.primaryTextArea.TextArea.UpdateLines(textAreaUpdate.Position.X, textAreaUpdate.Position.Y);
					if (this.secondaryTextArea != null)
					{
						this.secondaryTextArea.TextArea.UpdateLines(textAreaUpdate.Position.X, textAreaUpdate.Position.Y);
					}
					break;
				}
			}
			base.Document.UpdateQueue.Clear();
			this.primaryTextArea.TextArea.Update();
			if (this.secondaryTextArea != null)
			{
				this.secondaryTextArea.TextArea.Update();
			}
		}
		private void BeginPrint(object sender, PrintEventArgs ev)
		{
			this.curLineNr = 0;
			this.printingStringFormat = (StringFormat)StringFormat.GenericTypographic.Clone();
			float[] array = new float[100];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (float)base.TabIndent * this.primaryTextArea.TextArea.TextView.GetWidth(' ');
			}
			this.printingStringFormat.SetTabStops(0f, array);
		}
		private void Advance(ref float x, ref float y, float maxWidth, float size, float fontHeight)
		{
			if (x + size < maxWidth)
			{
				x += size;
				return;
			}
			x = this.curTabIndent;
			y += fontHeight;
		}
		private float MeasurePrintingHeight(Graphics g, LineSegment line, float maxWidth)
		{
			float num = 0f;
			float num2 = 0f;
			float height = this.Font.GetHeight(g);
			this.curTabIndent = 0f;
			foreach (TextWord textWord in line.Words)
			{
				switch (textWord.Type)
				{
				case TextWordType.Word:
					this.Advance(ref num, ref num2, maxWidth, g.MeasureString(textWord.Word, textWord.Font, new SizeF(maxWidth, height * 100f), this.printingStringFormat).Width, height);
					break;
				case TextWordType.Space:
					this.Advance(ref num, ref num2, maxWidth, this.primaryTextArea.TextArea.TextView.GetWidth(' '), height);
					break;
				case TextWordType.Tab:
					this.Advance(ref num, ref num2, maxWidth, (float)base.TabIndent * this.primaryTextArea.TextArea.TextView.GetWidth(' '), height);
					break;
				}
			}
			return num2 + height;
		}
		private void DrawLine(Graphics g, LineSegment line, float yPos, RectangleF margin)
		{
			float num = 0f;
			float height = this.Font.GetHeight(g);
			this.curTabIndent = 0f;
			foreach (TextWord textWord in line.Words)
			{
				switch (textWord.Type)
				{
				case TextWordType.Word:
				{
					g.DrawString(textWord.Word, textWord.Font, BrushRegistry.GetBrush(textWord.Color), num + margin.X, yPos);
					SizeF sizeF = g.MeasureString(textWord.Word, textWord.Font, new SizeF(margin.Width, height * 100f), this.printingStringFormat);
					this.Advance(ref num, ref yPos, margin.Width, sizeF.Width, height);
					break;
				}
				case TextWordType.Space:
					this.Advance(ref num, ref yPos, margin.Width, this.primaryTextArea.TextArea.TextView.GetWidth(' '), height);
					break;
				case TextWordType.Tab:
					this.Advance(ref num, ref yPos, margin.Width, (float)base.TabIndent * this.primaryTextArea.TextArea.TextView.GetWidth(' '), height);
					break;
				}
			}
		}
		private void PrintPage(object sender, PrintPageEventArgs ev)
		{
			Graphics graphics = ev.Graphics;
			float num = (float)ev.MarginBounds.Top;
			while (this.curLineNr < base.Document.TotalNumberOfLines)
			{
				LineSegment lineSegment = base.Document.GetLineSegment(this.curLineNr);
				if (lineSegment.Words != null)
				{
					float num2 = this.MeasurePrintingHeight(graphics, lineSegment, (float)ev.MarginBounds.Width);
					if (num2 + num > (float)ev.MarginBounds.Bottom)
					{
						break;
					}
					this.DrawLine(graphics, lineSegment, num, ev.MarginBounds);
					num += num2;
				}
				this.curLineNr++;
			}
			ev.HasMorePages = (this.curLineNr < base.Document.TotalNumberOfLines);
		}
	}
}
