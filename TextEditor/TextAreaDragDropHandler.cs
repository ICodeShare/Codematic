using LTP.TextEditor.Document;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class TextAreaDragDropHandler
	{
		private TextArea textArea;
		public void Attach(TextArea textArea)
		{
			this.textArea = textArea;
			textArea.AllowDrop = true;
			textArea.DragEnter += new DragEventHandler(this.OnDragEnter);
			textArea.DragDrop += new DragEventHandler(this.OnDragDrop);
			textArea.DragOver += new DragEventHandler(this.OnDragOver);
			textArea.Disposed += new EventHandler(this.OnDisposed);
		}
		protected void OnDisposed(object sender, EventArgs e)
		{
			this.textArea.DragEnter -= new DragEventHandler(this.OnDragEnter);
			this.textArea.DragDrop -= new DragEventHandler(this.OnDragDrop);
			this.textArea.DragOver -= new DragEventHandler(this.OnDragOver);
			this.textArea.Disposed -= new EventHandler(this.OnDisposed);
			this.textArea = null;
		}
		private static DragDropEffects GetDragDropEffect(DragEventArgs e)
		{
			if ((e.AllowedEffect & DragDropEffects.Move) > DragDropEffects.None && (e.AllowedEffect & DragDropEffects.Copy) > DragDropEffects.None)
			{
				if ((e.KeyState & 8) <= 0)
				{
					return DragDropEffects.Move;
				}
				return DragDropEffects.Copy;
			}
			else
			{
				if ((e.AllowedEffect & DragDropEffects.Move) > DragDropEffects.None)
				{
					return DragDropEffects.Move;
				}
				if ((e.AllowedEffect & DragDropEffects.Copy) > DragDropEffects.None)
				{
					return DragDropEffects.Copy;
				}
				return DragDropEffects.None;
			}
		}
		protected void OnDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = TextAreaDragDropHandler.GetDragDropEffect(e);
			}
		}
		private void InsertString(int offset, string str)
		{
			this.textArea.Document.Insert(offset, str);
			this.textArea.SelectionManager.SetSelection(new DefaultSelection(this.textArea.Document, this.textArea.Document.OffsetToPosition(offset), this.textArea.Document.OffsetToPosition(offset + str.Length)));
			this.textArea.Caret.Position = this.textArea.Document.OffsetToPosition(offset + str.Length);
			this.textArea.Refresh();
		}
		protected void OnDragDrop(object sender, DragEventArgs e)
		{
			this.textArea.PointToClient(new Point(e.X, e.Y));
			if (!this.textArea.EnableCutOrPaste)
			{
				return;
			}
			if (e.Data.GetDataPresent(typeof(string)))
			{
				bool flag = false;
				this.textArea.BeginUpdate();
				try
				{
					int num = this.textArea.Caret.Offset;
					if (e.Data.GetDataPresent(typeof(DefaultSelection)))
					{
						ISelection selection = (ISelection)e.Data.GetData(typeof(DefaultSelection));
						if (selection.ContainsPosition(this.textArea.Caret.Position))
						{
							return;
						}
						if (TextAreaDragDropHandler.GetDragDropEffect(e) == DragDropEffects.Move)
						{
							int length = selection.Length;
							this.textArea.Document.Remove(selection.Offset, length);
							if (selection.Offset < num)
							{
								num -= length;
							}
						}
						flag = true;
					}
					this.textArea.SelectionManager.ClearSelection();
					this.InsertString(num, (string)e.Data.GetData(typeof(string)));
					if (flag)
					{
						this.textArea.Document.UndoStack.UndoLast(2);
					}
					this.textArea.Document.UpdateQueue.Clear();
					this.textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
				}
				finally
				{
					this.textArea.EndUpdate();
				}
			}
		}
		protected void OnDragOver(object sender, DragEventArgs e)
		{
			if (!this.textArea.Focused)
			{
				this.textArea.Focus();
			}
			Point point = this.textArea.PointToClient(new Point(e.X, e.Y));
			if (this.textArea.TextView.DrawingPosition.Contains(point.X, point.Y))
			{
				Point logicalPosition = this.textArea.TextView.GetLogicalPosition(point.X - this.textArea.TextView.DrawingPosition.X, point.Y - this.textArea.TextView.DrawingPosition.Y);
				int y = Math.Min(this.textArea.Document.TotalNumberOfLines - 1, Math.Max(0, logicalPosition.Y));
				this.textArea.Caret.Position = new Point(logicalPosition.X, y);
				this.textArea.SetDesiredColumn();
				if (!this.textArea.EnableCutOrPaste)
				{
					e.Effect = DragDropEffects.None;
					return;
				}
				if (e.Data.GetDataPresent(typeof(string)))
				{
					e.Effect = TextAreaDragDropHandler.GetDragDropEffect(e);
				}
			}
		}
	}
}
