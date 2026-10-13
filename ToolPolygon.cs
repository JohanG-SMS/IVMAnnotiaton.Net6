using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
	/// <summary>
	/// Polygon tool
	/// </summary>
	[ComVisible(false)]
    public class ToolPolygon : IVMAnnotation.ToolObject
	{
		public ToolPolygon()
		{
            Cursor = new Cursor(GetType(), "cur_pen_il.cur");
        }

        private int lastX;
        private int lastY;
        private DrawPolygon newPolygon;
        private const int minDistance = 15*15;

        /// <summary>
        /// Left nouse button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            // Create new polygon, add it to the list
            // and keep reference to it
            Point point = new Point(e.X, e.Y);

            if (drawArea.Zoomed)
            {
                drawArea.GetZoomCorrectedCoordinates(point, out point);
            }

            newPolygon = new DrawPolygon(point.X, point.Y, point.X + 1, point.Y + 1);
            AddNewObject(drawArea, newPolygon);
            lastX = point.X;
            lastY = point.Y;
        }

        /// <summary>
        /// Mouse move - resize new polygon
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;

            if ( e.Button != MouseButtons.Left )
                return;

            if ( newPolygon == null )
                return;                 // precaution

            Point point = new Point(e.X, e.Y);

            if (drawArea.Zoomed)
            {
                drawArea.GetZoomCorrectedCoordinates(point, out point);
            }

            int distance = (point.X - lastX)*(point.X - lastX) + (point.Y - lastY)*(point.Y - lastY);

            if ( distance < minDistance )
            {
                // Distance between last two points is less than minimum -
                // move last point
                newPolygon.MoveHandleTo(point, newPolygon.HandleCount);
            }
            else
            {
                // Add new point
                newPolygon.AddPoint(point);
                lastX = point.X;
                lastY = point.Y;
            }

            drawArea.Refresh();
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            newPolygon = null;

            base.OnMouseUp (drawArea, e);


            int n = drawArea.GraphicsList.SelectionCount;
            for ( int i = 0; i < n; i++ )
            {
                DrawObject o = drawArea.GraphicsList.GetSelectedObject(i);
                ((DrawPolygon)o).OnClosePolygon(drawArea);
            }
            drawArea.Refresh();
        }
	}
}
