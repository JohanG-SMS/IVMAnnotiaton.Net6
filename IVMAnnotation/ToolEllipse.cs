using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
	/// <summary>
	/// Ellipse tool
	/// </summary>
	[ComVisible(false)]
    public class ToolEllipse : IVMAnnotation.ToolRectangle
	{
		public ToolEllipse()
		{
            Cursor = new Cursor(Helper.GetCursorStream("cur_Ellipse.cur"));
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);

            if (drawArea.Zoomed)
            {
                drawArea.GetZoomCorrectedCoordinates(point, out point);
            }

            AddNewObject(drawArea, new DrawEllipse(point.X, point.Y, 1, 1));
        }

	}
}
