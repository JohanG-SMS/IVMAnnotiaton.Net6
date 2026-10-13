using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
	/// <summary>
	/// Line tool
	/// </summary>
	[ComVisible(false)]
    public class ToolArrow : IVMAnnotation.ToolObject
	{
        public ToolArrow()
        {
            Cursor = new Cursor(GetType(), "cur_arrow.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);

            if (drawArea.Zoomed)
            {
                drawArea.GetZoomCorrectedCoordinates(point, out point);
            }

            AddNewObject(drawArea, new DrawArrow(point.X, point.Y, point.X + 1, point.Y + 1));
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;

            if ( e.Button == MouseButtons.Left )
            {
                Point point = new Point(e.X, e.Y);

                if (drawArea.Zoomed)
                {
                    drawArea.GetZoomCorrectedCoordinates(point, out point);
                }

                drawArea.GraphicsList[0].MoveHandleTo(point, 2);
                drawArea.Refresh();
            }
        }
    }
}
