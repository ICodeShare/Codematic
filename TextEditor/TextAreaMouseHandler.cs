using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	public class TextAreaMouseHandler
	{
		private TextArea textArea;
		private bool doubleclick;
		private Point mousepos = new Point(0, 0);
		private int selbegin;
		private int selend;
		private bool clickedOnSelectedText;
		private MouseButtons button;
		private static readonly Point nilPoint = new Point(-1, -1);
		private Point mousedownpos = TextAreaMouseHandler.nilPoint;
		private Point lastmousedownpos = TextAreaMouseHandler.nilPoint;
		private Point selectionStartPos = TextAreaMouseHandler.nilPoint;
		private bool gotmousedown;
		private bool dodragdrop;
		private DateTime lastTime = DateTime.Now;
		private Point minSelection = TextAreaMouseHandler.nilPoint;
		private Point maxSelection = TextAreaMouseHandler.nilPoint;
		public TextAreaMouseHandler(TextArea textArea)
		{
			this.textArea = textArea;
		}
		public void Attach()
		{
			this.textArea.Click += new EventHandler(this.TextAreaClick);
			this.textArea.MouseMove += new MouseEventHandler(this.TextAreaMouseMove);
			this.textArea.MouseDown += new MouseEventHandler(this.OnMouseDown);
			this.textArea.DoubleClick += new EventHandler(this.OnDoubleClick);
			this.textArea.MouseLeave += new EventHandler(this.OnMouseLeave);
			this.textArea.MouseUp += new MouseEventHandler(this.OnMouseUp);
			this.textArea.LostFocus += new EventHandler(this.TextAreaLostFocus);
			this.textArea.Disposed += new EventHandler(this.TextAreaDisposed);
		}
		private void TextAreaDisposed(object sender, EventArgs e)
		{
			this.textArea.Click -= new EventHandler(this.TextAreaClick);
			this.textArea.MouseMove -= new MouseEventHandler(this.TextAreaMouseMove);
			this.textArea.MouseDown -= new MouseEventHandler(this.OnMouseDown);
			this.textArea.DoubleClick -= new EventHandler(this.OnDoubleClick);
			this.textArea.MouseLeave -= new EventHandler(this.OnMouseLeave);
			this.textArea.MouseUp -= new MouseEventHandler(this.OnMouseUp);
			this.textArea.LostFocus -= new EventHandler(this.TextAreaLostFocus);
			this.textArea.Disposed -= new EventHandler(this.TextAreaDisposed);
			this.textArea = null;
		}
		private void ShowHiddenCursor()
		{
			if (TextArea.HiddenMouseCursor)
			{
				Cursor.Show();
				TextArea.HiddenMouseCursor = false;
			}
		}
		private void TextAreaLostFocus(object sender, EventArgs e)
		{
			this.ShowHiddenCursor();
		}
		private void OnMouseLeave(object sender, EventArgs e)
		{
			this.ShowHiddenCursor();
			this.gotmousedown = false;
			this.mousedownpos = TextAreaMouseHandler.nilPoint;
		}
		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			this.gotmousedown = false;
			this.mousedownpos = TextAreaMouseHandler.nilPoint;
		}
		private void TextAreaClick(object sender, EventArgs e)
		{
			if (this.dodragdrop)
			{
				return;
			}
			if (this.clickedOnSelectedText && this.textArea.TextView.DrawingPosition.Contains(this.mousepos.X, this.mousepos.Y))
			{
				this.textArea.SelectionManager.ClearSelection();
				Point logicalPosition = this.textArea.TextView.GetLogicalPosition(this.mousepos.X - this.textArea.TextView.DrawingPosition.X, this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
				this.textArea.Caret.Position = logicalPosition;
				this.textArea.SetDesiredColumn();
			}
		}
		private void TextAreaMouseMove(object sender, MouseEventArgs e)
		{
			this.ShowHiddenCursor();
			if (this.dodragdrop)
			{
				this.dodragdrop = false;
				return;
			}
			this.doubleclick = false;
			this.mousepos = new Point(e.X, e.Y);
			if (this.clickedOnSelectedText)
			{
				if (Math.Abs(this.mousedownpos.X - this.mousepos.X) >= SystemInformation.DragSize.Width / 2 || Math.Abs(this.mousedownpos.Y - this.mousepos.Y) >= SystemInformation.DragSize.Height / 2)
				{
					this.clickedOnSelectedText = false;
					ISelection selectionAt = this.textArea.SelectionManager.GetSelectionAt(this.textArea.Caret.Offset);
					if (selectionAt != null && this.textArea.EnableCutOrPaste)
					{
						string selectedText = selectionAt.SelectedText;
						if (selectedText != null && selectedText.Length > 0)
						{
							DataObject dataObject = new DataObject();
							dataObject.SetData(DataFormats.UnicodeText, true, selectedText);
							dataObject.SetData(selectionAt);
							this.dodragdrop = true;
							this.textArea.DoDragDrop(dataObject, DragDropEffects.All);
						}
					}
				}
				return;
			}
			if (e.Button == MouseButtons.None)
			{
				FoldMarker foldMarkerFromPosition = this.textArea.TextView.GetFoldMarkerFromPosition(this.mousepos.X - this.textArea.TextView.DrawingPosition.X, this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
				if (foldMarkerFromPosition != null && foldMarkerFromPosition.IsFolded)
				{
					StringBuilder stringBuilder = new StringBuilder(foldMarkerFromPosition.InnerText);
					int num = 0;
					for (int i = 0; i < stringBuilder.Length; i++)
					{
						if (stringBuilder[i] == '\n')
						{
							num++;
							if (num >= 10)
							{
								stringBuilder.Remove(i + 1, stringBuilder.Length - i - 1);
								stringBuilder.Append(Environment.NewLine);
								stringBuilder.Append("...");
								break;
							}
						}
					}
					stringBuilder.Replace("\t", "    ");
					this.textArea.SetToolTip(stringBuilder.ToString());
					return;
				}
				Point logicalPosition = this.textArea.TextView.GetLogicalPosition(this.mousepos.X - this.textArea.TextView.DrawingPosition.X, this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
				ArrayList markers = this.textArea.Document.MarkerStrategy.GetMarkers(logicalPosition);
				foreach (TextMarker textMarker in markers)
				{
					if (textMarker.ToolTip != null)
					{
						this.textArea.SetToolTip(textMarker.ToolTip.Replace("\t", "    "));
						return;
					}
				}
			}
			if (e.Button == MouseButtons.Left && this.gotmousedown)
			{
				this.ExtendSelectionToMouse();
			}
		}
		private void ExtendSelectionToMouse()
		{
			Point point = this.textArea.TextView.GetLogicalPosition(Math.Max(0, this.mousepos.X - this.textArea.TextView.DrawingPosition.X), this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
			int arg_68_0 = point.Y;
			point = this.textArea.Caret.ValidatePosition(point);
			Point position = this.textArea.Caret.Position;
			if (position == point)
			{
				return;
			}
			this.textArea.Caret.Position = point;
			if (this.minSelection != TextAreaMouseHandler.nilPoint && this.textArea.SelectionManager.SelectionCollection.Count > 0)
			{
				ISelection arg_ED_0 = this.textArea.SelectionManager.SelectionCollection[0];
				Point point2 = this.textArea.SelectionManager.GreaterEqPos(this.minSelection, this.maxSelection) ? this.maxSelection : this.minSelection;
				Point point3 = this.textArea.SelectionManager.GreaterEqPos(this.minSelection, this.maxSelection) ? this.minSelection : this.maxSelection;
				if (this.textArea.SelectionManager.GreaterEqPos(point3, position) && this.textArea.SelectionManager.GreaterEqPos(position, point2))
				{
					this.textArea.SelectionManager.SetSelection(point2, point3);
				}
				else
				{
					if (this.textArea.SelectionManager.GreaterEqPos(point3, position))
					{
						this.textArea.SelectionManager.SetSelection(position, point3);
					}
					else
					{
						this.textArea.SelectionManager.SetSelection(point2, position);
					}
				}
			}
			else
			{
				this.textArea.SelectionManager.ExtendSelection(position, this.textArea.Caret.Position);
			}
			this.textArea.SetDesiredColumn();
		}
		private void DoubleClickSelectionExtend()
		{
			this.textArea.SelectionManager.ClearSelection();
			if (this.textArea.TextView.DrawingPosition.Contains(this.mousepos.X, this.mousepos.Y))
			{
				FoldMarker foldMarkerFromPosition = this.textArea.TextView.GetFoldMarkerFromPosition(this.mousepos.X - this.textArea.TextView.DrawingPosition.X, this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
				if (foldMarkerFromPosition != null && foldMarkerFromPosition.IsFolded)
				{
					foldMarkerFromPosition.IsFolded = false;
					this.textArea.MotherTextAreaControl.AdjustScrollBars(null, null);
				}
				if (this.textArea.Caret.Offset < this.textArea.Document.TextLength)
				{
					char charAt = this.textArea.Document.GetCharAt(this.textArea.Caret.Offset);
					if (charAt == '"')
					{
						if (this.textArea.Caret.Offset < this.textArea.Document.TextLength)
						{
							int num = this.FindNext(this.textArea.Document, this.textArea.Caret.Offset + 1, '"');
							this.minSelection = this.textArea.Caret.Position;
							this.maxSelection = this.textArea.Document.OffsetToPosition((num > this.textArea.Caret.Offset) ? (num + 1) : num);
						}
					}
					else
					{
						this.minSelection = this.textArea.Document.OffsetToPosition(this.FindWordStart(this.textArea.Document, this.textArea.Caret.Offset));
						this.maxSelection = this.textArea.Document.OffsetToPosition(this.FindWordEnd(this.textArea.Document, this.textArea.Caret.Offset));
					}
					Console.WriteLine("EXTEND");
					this.textArea.SelectionManager.ExtendSelection(this.minSelection, this.maxSelection);
				}
				this.textArea.Refresh();
			}
		}
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (this.dodragdrop)
			{
				return;
			}
			if (this.doubleclick)
			{
				this.doubleclick = false;
				return;
			}
			this.gotmousedown = true;
			this.button = e.Button;
			if ((DateTime.Now - this.lastTime).Milliseconds < SystemInformation.DoubleClickTime)
			{
				int num = Math.Abs(this.lastmousedownpos.X - e.X);
				int num2 = Math.Abs(this.lastmousedownpos.Y - e.Y);
				if (num <= SystemInformation.DoubleClickSize.Width && num2 <= SystemInformation.DoubleClickSize.Height)
				{
					this.DoubleClickSelectionExtend();
					this.lastTime = DateTime.Now;
					this.lastmousedownpos = new Point(e.X, e.Y);
					return;
				}
			}
			this.minSelection = TextAreaMouseHandler.nilPoint;
			this.maxSelection = TextAreaMouseHandler.nilPoint;
			this.lastTime = DateTime.Now;
			this.lastmousedownpos = (this.mousedownpos = new Point(e.X, e.Y));
			if (this.textArea.TextView.DrawingPosition.Contains(this.mousepos.X, this.mousepos.Y) && this.button == MouseButtons.Left)
			{
				FoldMarker foldMarkerFromPosition = this.textArea.TextView.GetFoldMarkerFromPosition(this.mousepos.X - this.textArea.TextView.DrawingPosition.X, this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
				if (foldMarkerFromPosition != null && foldMarkerFromPosition.IsFolded)
				{
					if (this.textArea.SelectionManager.HasSomethingSelected)
					{
						this.clickedOnSelectedText = true;
					}
					this.textArea.SelectionManager.SetSelection(new DefaultSelection(this.textArea.TextView.Document, new Point(foldMarkerFromPosition.StartColumn, foldMarkerFromPosition.StartLine), new Point(foldMarkerFromPosition.EndColumn, foldMarkerFromPosition.EndLine)));
					this.textArea.Focus();
					return;
				}
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					this.ExtendSelectionToMouse();
				}
				else
				{
					Point logicalPosition = this.textArea.TextView.GetLogicalPosition(this.mousepos.X - this.textArea.TextView.DrawingPosition.X, this.mousepos.Y - this.textArea.TextView.DrawingPosition.Y);
					this.clickedOnSelectedText = false;
					int offset = this.textArea.Document.PositionToOffset(logicalPosition);
					if (this.textArea.SelectionManager.HasSomethingSelected && this.textArea.SelectionManager.IsSelected(offset))
					{
						this.clickedOnSelectedText = true;
					}
					else
					{
						this.selbegin = (this.selend = offset);
						this.textArea.SelectionManager.ClearSelection();
						if (this.mousepos.Y > 0 && this.mousepos.Y < this.textArea.TextView.DrawingPosition.Height)
						{
							Point position = default(Point);
							position.Y = Math.Min(this.textArea.Document.TotalNumberOfLines - 1, logicalPosition.Y);
							position.X = logicalPosition.X;
							this.textArea.Caret.Position = position;
							this.textArea.SetDesiredColumn();
						}
					}
				}
			}
			this.textArea.Focus();
		}
		private int FindNext(IDocument document, int offset, char ch)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			int num = lineSegmentForOffset.Offset + lineSegmentForOffset.Length;
			while (offset < num && document.GetCharAt(offset) != ch)
			{
				offset++;
			}
			return offset;
		}
		private bool IsSelectableChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_';
		}
		private int FindWordStart(IDocument document, int offset)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			if (offset > 0 && char.IsWhiteSpace(document.GetCharAt(offset - 1)) && char.IsWhiteSpace(document.GetCharAt(offset)))
			{
				while (offset > lineSegmentForOffset.Offset)
				{
					if (!char.IsWhiteSpace(document.GetCharAt(offset - 1)))
					{
						break;
					}
					offset--;
				}
			}
			else
			{
				if (!this.IsSelectableChar(document.GetCharAt(offset)))
				{
					if (offset <= 0 || !char.IsWhiteSpace(document.GetCharAt(offset)) || !this.IsSelectableChar(document.GetCharAt(offset - 1)))
					{
						if (offset > 0 && !char.IsWhiteSpace(document.GetCharAt(offset - 1)) && !this.IsSelectableChar(document.GetCharAt(offset - 1)))
						{
							return Math.Max(0, offset - 1);
						}
						return offset;
					}
				}
				while (offset > lineSegmentForOffset.Offset)
				{
					if (!this.IsSelectableChar(document.GetCharAt(offset - 1)))
					{
						break;
					}
					offset--;
				}
			}
			return offset;
		}
		private int FindWordEnd(IDocument document, int offset)
		{
			LineSegment lineSegmentForOffset = document.GetLineSegmentForOffset(offset);
			int num = lineSegmentForOffset.Offset + lineSegmentForOffset.Length;
			if (this.IsSelectableChar(document.GetCharAt(offset)))
			{
				while (offset < num)
				{
					if (!this.IsSelectableChar(document.GetCharAt(offset)))
					{
						break;
					}
					offset++;
				}
			}
			else
			{
				if (!char.IsWhiteSpace(document.GetCharAt(offset)))
				{
					return Math.Max(0, offset + 1);
				}
				if (offset > 0 && char.IsWhiteSpace(document.GetCharAt(offset - 1)))
				{
					while (offset < num)
					{
						if (!char.IsWhiteSpace(document.GetCharAt(offset)))
						{
							break;
						}
						offset++;
					}
				}
			}
			return offset;
		}
		private void OnDoubleClick(object sender, EventArgs e)
		{
			if (this.dodragdrop)
			{
				return;
			}
			this.doubleclick = true;
		}
	}
}
