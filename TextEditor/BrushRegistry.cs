using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace LTP.TextEditor
{
	public class BrushRegistry
	{
		private static Hashtable brushes = new Hashtable();
		private static Hashtable pens = new Hashtable();
		private static Hashtable dotPens = new Hashtable();
		public static Brush GetBrush(Color color)
		{
			if (!BrushRegistry.brushes.Contains(color))
			{
				Brush brush = new SolidBrush(color);
				BrushRegistry.brushes.Add(color, brush);
				return brush;
			}
			return BrushRegistry.brushes[color] as Brush;
		}
		public static Pen GetPen(Color color)
		{
			if (!BrushRegistry.pens.Contains(color))
			{
				Pen pen = new Pen(color);
				BrushRegistry.pens.Add(color, pen);
				return pen;
			}
			return BrushRegistry.pens[color] as Pen;
		}
		public static Pen GetDotPen(Color bgColor, Color fgColor)
		{
			bool flag = BrushRegistry.dotPens.Contains(bgColor);
			if (!flag || !((Hashtable)BrushRegistry.dotPens[bgColor]).Contains(fgColor))
			{
				if (!flag)
				{
					BrushRegistry.dotPens[bgColor] = new Hashtable();
				}
				HatchBrush brush = new HatchBrush(HatchStyle.Percent50, bgColor, fgColor);
				Pen pen = new Pen(brush);
				((Hashtable)BrushRegistry.dotPens[bgColor])[fgColor] = pen;
				return pen;
			}
			return ((Hashtable)BrushRegistry.dotPens[bgColor])[fgColor] as Pen;
		}
	}
}
