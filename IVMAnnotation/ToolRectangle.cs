using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IVMAnnotation
{
	/// <summary>
	/// Rectangle tool
	/// </summary>
    [ComVisible(false)]
    public class ToolRectangle : IVMAnnotation.ToolObject
	{

		public ToolRectangle()
		{
			Cursor = new Cursor(Helper.GetCursorStream("cur_Rectangle.cur"));
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);

            if (drawArea.Zoomed)
            {
                drawArea.GetZoomCorrectedCoordinates(point, out point);
            }

            AddNewObject(drawArea, new DrawRectangle(point.X, point.Y, 1, 1));
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

                drawArea.GraphicsList[0].MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }
	}
}
