using LTP.TextEditor.Document;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public abstract class AbstractMargin
	{
		protected Rectangle drawingPosition = new Rectangle(0, 0, 0, 0);
		protected TextArea textArea;
        public event MarginPaintEventHandler Painted;
        public event MarginMouseEventHandler MouseDown;
        public event MarginMouseEventHandler MouseMove;
        public event EventHandler MouseLeave;
		public Rectangle DrawingPosition
		{
			get
			{
				return this.drawingPosition;
			}
			set
			{
				this.drawingPosition = value;
			}
		}
		public TextArea TextArea
		{
			get
			{
				return this.textArea;
			}
		}
		public IDocument Document
		{
			get
			{
				return this.textArea.Document;
			}
		}
		public ITextEditorProperties TextEditorProperties
		{
			get
			{
				return this.textArea.Document.TextEditorProperties;
			}
		}
		public virtual Cursor Cursor
		{
			get
			{
				return Cursors.Default;
			}
		}
		public virtual Size Size
		{
			get
			{
				return new Size(-1, -1);
			}
		}
		public virtual bool IsVisible
		{
			get
			{
				return true;
			}
		}
		protected AbstractMargin(TextArea textArea)
		{
			this.textArea = textArea;
		}
		public virtual void HandleMouseDown(Point mousepos, MouseButtons mouseButtons)
		{
			if (this.MouseDown != null)
			{
				this.MouseDown(this, mousepos, mouseButtons);
			}
		}
		public virtual void HandleMouseMove(Point mousepos, MouseButtons mouseButtons)
		{
			if (this.MouseMove != null)
			{
				this.MouseMove(this, mousepos, mouseButtons);
			}
		}
		public virtual void HandleMouseLeave(EventArgs e)
		{
			if (this.MouseLeave != null)
			{
				this.MouseLeave(this, e);
			}
		}
		public virtual void Paint(Graphics g, Rectangle rect)
		{
			if (this.Painted != null)
			{
				this.Painted(this, g, rect);
			}
		}
	}
}
