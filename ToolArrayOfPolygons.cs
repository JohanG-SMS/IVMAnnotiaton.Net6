using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
	/// <summary>
	/// Summary description for ToolArrayOfPolygons.
	/// </summary>
	[ComVisible(false)]
	public class ToolArrayOfPolygons : IVMAnnotation.ToolRectangle
	{
		private bool    m_bNewObject = false;
		private Point   m_startPoint;
		private Point   m_endPoint;		

		public ToolArrayOfPolygons()
		{
			Cursor = new Cursor(GetType(), "cur_PolyHandle.cur");
		}

		public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
		{
			Point point = new Point(e.X, e.Y);

			m_startPoint = point;
			m_endPoint   = point;

			if (drawArea.Zoomed)
			{
				drawArea.GetZoomCorrectedCoordinates(point, out point);
			}

			m_bNewObject = true;

			AddNewObject(drawArea, new DrawArrayOfPolygons(point.X, point.Y, 1, 1));			
		}

		public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
		{
			base.OnMouseUp(drawArea, e);

			if (m_bNewObject)
			{
				DrawObject o = drawArea.GraphicsList[0];
				o.OnDoubleClick(drawArea, null);

				m_bNewObject = false;
			}           			

			drawArea.Refresh();
			drawArea.SetDrawingMode(IVMAnnotation.AnnotationType.ArrayOfPolygons);
		}

	}	
}
