using System;
using System.Drawing;
using System.Runtime.InteropServices;
namespace LTP.TextEditor
{
	internal class Ime
	{
		[StructLayout(LayoutKind.Sequential)]
		private class COMPOSITIONFORM
		{
			public int dwStyle;
			public Ime.POINT ptCurrentPos;
			public Ime.RECT rcArea;
		}
		[StructLayout(LayoutKind.Sequential)]
		private class POINT
		{
			public int x;
			public int y;
		}
		[StructLayout(LayoutKind.Sequential)]
		private class RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		[StructLayout(LayoutKind.Sequential)]
		private class LOGFONT
		{
			public int lfHeight;
			public int lfWidth;
			public int lfEscapement;
			public int lfOrientation;
			public int lfWeight;
			public byte lfItalic;
			public byte lfUnderline;
			public byte lfStrikeOut;
			public byte lfCharSet;
			public byte lfOutPrecision;
			public byte lfClipPrecision;
			public byte lfQuality;
			public byte lfPitchAndFamily;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}
		private const int WM_IME_CONTROL = 643;
		private const int IMC_SETCOMPOSITIONWINDOW = 12;
		private const int CFS_POINT = 2;
		private const int IMC_SETCOMPOSITIONFONT = 10;
		private Font font;
		private IntPtr hIMEWnd;
		private IntPtr hWnd;
		private Ime.LOGFONT lf;
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				if (this.font == null)
				{
					this.font = value;
					this.lf = null;
				}
				else
				{
					if (!this.font.Equals(value))
					{
						this.font = value;
						this.lf = null;
					}
				}
				this.SetIMEWindowFont(value);
			}
		}
		public IntPtr HWnd
		{
			set
			{
				if (this.hWnd != value)
				{
					this.hWnd = value;
					this.hIMEWnd = Ime.ImmGetDefaultIMEWnd(value);
					this.SetIMEWindowFont(this.font);
				}
			}
		}
		public Ime(IntPtr hWnd, Font font)
		{
			this.hWnd = hWnd;
			this.hIMEWnd = Ime.ImmGetDefaultIMEWnd(hWnd);
			this.font = font;
			this.SetIMEWindowFont(font);
		}
		[DllImport("imm32.dll")]
		private static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, Ime.COMPOSITIONFORM lParam);
		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct)] [In] Ime.LOGFONT lParam);
		private void SetIMEWindowFont(Font f)
		{
			if (this.lf == null)
			{
				this.lf = new Ime.LOGFONT();
				f.ToLogFont(this.lf);
				this.lf.lfFaceName = f.Name;
			}
			Ime.SendMessage(this.hIMEWnd, 643, 10, this.lf);
		}
		public void SetIMEWindowLocation(int x, int y)
		{
			Ime.POINT pOINT = new Ime.POINT();
			pOINT.x = x;
			pOINT.y = y;
			Ime.COMPOSITIONFORM cOMPOSITIONFORM = new Ime.COMPOSITIONFORM();
			cOMPOSITIONFORM.dwStyle = 2;
			cOMPOSITIONFORM.ptCurrentPos = pOINT;
			cOMPOSITIONFORM.rcArea = new Ime.RECT();
			Ime.SendMessage(this.hIMEWnd, 643, 12, cOMPOSITIONFORM);
		}
	}
}
