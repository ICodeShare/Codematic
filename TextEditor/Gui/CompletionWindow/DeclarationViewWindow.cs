using LTP.TextEditor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace LTP.TextEditor.Gui.CompletionWindow
{
	public class DeclarationViewWindow : Form, IDeclarationViewWindow
	{
		private string description = string.Empty;
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
				this.Refresh();
			}
		}
		public DeclarationViewWindow(Form parent)
		{
			base.SetStyle(ControlStyles.Selectable, false);
			base.StartPosition = FormStartPosition.Manual;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Owner = parent;
			base.ShowInTaskbar = false;
			base.Size = new Size(0, 0);
		}
		public void ShowDeclarationViewWindow()
		{
			AbstractCompletionWindow.ShowWindow(base.Handle, AbstractCompletionWindow.SW_SHOWNA);
		}
		public void CloseDeclarationViewWindow()
		{
			base.Close();
			base.Dispose();
		}
		protected override void OnPaint(PaintEventArgs pe)
		{
			TipPainterTools.DrawHelpTipFromCombinedDescription(this, pe.Graphics, this.Font, null, this.description);
		}
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
			if (this.description != null && this.description.Length > 0)
			{
				pe.Graphics.FillRectangle(SystemBrushes.Info, pe.ClipRectangle);
			}
		}
	}
}
