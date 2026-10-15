using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
	/// <summary>
	/// Pointer tool
	/// </summary>
	[ComVisible(false)]
    public class ToolZoom : IVMAnnotation.ToolObject
	{
        private bool      m_bZoom       = false;
        private ZoomType  m_ZoomType    = ZoomType.ZoomIn;
        private Rectangle m_ZoomRect    = new Rectangle(new Point(0, 0), new Size(0, 0));
        private Rectangle m_CenterRect  = new Rectangle(new Point(0, 0), new Size(0, 0));
        private Rectangle m_OldZoomRect = new Rectangle(new Point(-1, -1), new Size(0, 0));

        private Point lastPoint  = new Point(0,0);
        private Point startPoint = new Point(0,0);


		public ToolZoom()
		{
		}

        #region MouseHandlers
        /// <summary>
        /// Left mouse button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            if (m_ZoomType == ZoomType.ZoomOut)
            {
                Cursor = new Cursor(Helper.GetCursorStream("cur_zoomout.cur"));
            }
            else
            {
                Cursor = new Cursor(Helper.GetCursorStream("cur_zoomin.cur"));
            }

            drawArea.Cursor = Cursor;

            // reset zoom state
            m_bZoom = false;

            int X = e.X;
            int Y = e.Y;

            if (m_OldZoomRect.X != -1)
            {
                // calculate new zoom rectangle
                Decimal ZoomFactorHeight = Decimal.Divide(drawArea.Height, m_OldZoomRect.Height);
                Decimal ZoomFactorWidth  = Decimal.Divide(drawArea.Width,  m_OldZoomRect.Width);
                    
                X = (int)(m_OldZoomRect.X + Decimal.Divide(e.X, ZoomFactorWidth));
                Y = (int)(m_OldZoomRect.Y + Decimal.Divide(e.Y, ZoomFactorHeight));
            }

            lastPoint.X  = X;
            lastPoint.Y  = Y;
            startPoint.X = X;
            startPoint.Y = Y;
            
            // sets the starting point of the zoomed area
            m_ZoomRect = new Rectangle(new Point(X, Y), new Size(0, 0));

            drawArea.DrawNetRectangle = true;
            drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(m_ZoomRect);
            
            if (m_ZoomType == ZoomType.ZoomOut)
            {
                // When zooming out set the boolean to true. 
                // Moving is not needed for zooming out. However, 
                // for zooming in moving is needed so this boolean 
                // is set in the move handler
                m_bZoom = true;
            }
            drawArea.Refresh();
        }


        /// <summary>
        /// Mouse is moved.
        /// None button is pressed, or left button is pressed.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            if (m_ZoomType == ZoomType.ZoomOut)
            {
                Cursor = new Cursor(Helper.GetCursorStream("cur_zoomout.cur"));
            }
            else
            {
                Cursor = new Cursor(Helper.GetCursorStream("cur_zoomin.cur"));
            }

            drawArea.Cursor = Cursor;

            if (m_ZoomType == ZoomType.ZoomOut)
                return;

            if (e.Button != MouseButtons.Left) 
                return;

            int X = e.X;
            int Y = e.Y;

            if (m_OldZoomRect.X != -1)
            {
                // calculate new zoom rectangle
                Decimal ZoomFactorHeight = Decimal.Divide(drawArea.Height, m_OldZoomRect.Height);
                Decimal ZoomFactorWidth  = Decimal.Divide(drawArea.Width,  m_OldZoomRect.Width);
                    
                X = (int)(m_OldZoomRect.X + Decimal.Divide(e.X, ZoomFactorWidth));
                Y = (int)(m_OldZoomRect.Y + Decimal.Divide(e.Y, ZoomFactorHeight));
            }

            lastPoint.X = X;
            lastPoint.Y = Y;

            if (m_ZoomRect.X >= X)
            {
                m_ZoomRect.Width += Math.Abs(X - m_ZoomRect.X);
            }
            else
            {
                m_ZoomRect.Width = Math.Abs(X - m_ZoomRect.X);
            }

            if (m_ZoomRect.Y >= Y) 
            {
                m_ZoomRect.Height += Math.Abs(Y - m_ZoomRect.Y);
            }
            else 
            {
                m_ZoomRect.Height = Math.Abs(Y - m_ZoomRect.Y);
            }

            if (m_ZoomRect.X > X) 
            {
                    m_ZoomRect.X = X;
            }
            if (m_ZoomRect.Y > Y) 
            {
                    m_ZoomRect.Y = Y;
            }

            drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);
            m_bZoom = true;
            drawArea.Refresh();
        }

        /// <summary>
        /// mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            if (m_bZoom)
            {
                if (m_ZoomType == ZoomType.ZoomIn)
                {
                    if (m_ZoomRect.Height > 2)
                    {
                        CreateZoomInCenterRectangle(drawArea);
                        drawArea.SetZoomFactor(m_bZoom, m_CenterRect);
                        m_OldZoomRect = m_CenterRect;
                    }
                }
                else
                {
                    CreateZoomOutCenterRectangle(drawArea);
                    drawArea.SetZoomFactor(m_bZoom, m_CenterRect);
                    m_OldZoomRect = m_CenterRect;
                }

                drawArea.DrawNetRectangle = false;
                drawArea.Refresh();
            }

            drawArea.ActiveTool = AnnotationType.SelectionPointer;
        }
        #endregion

        private void CreateZoomInCenterRectangle(DrawArea drawArea)
        {
            // create a rectangle with the same Height/Width ratio as the drawarea
            // to avoid deforming of the annotations.
            int parentHeight = drawArea.Height;
            int parentWidth  = drawArea.Width;

            // Calculate center point
            Point centerPoint = new Point((int)(m_ZoomRect.X + (0.5 * m_ZoomRect.Width)), (int)(m_ZoomRect.Y + (0.5 * m_ZoomRect.Height)));

            int newWidth  = m_ZoomRect.Width;
            int newHeight = m_ZoomRect.Height;

            if (m_ZoomRect.Width > m_ZoomRect.Height)
            {
                newHeight = (int)(m_ZoomRect.Width * (Decimal.Divide(parentHeight, parentWidth)));
            }
            else
            {
                newWidth = (int)(m_ZoomRect.Height * (Decimal.Divide(parentWidth, parentHeight)));
            }
        
            // check bounderies
            if ((centerPoint.X - (0.5 * newWidth)) < 0)
            {
                centerPoint.X = 0 + (int)(0.5 * newWidth);
            }
            if ((centerPoint.X + (0.5 * newWidth)) > parentWidth)
            {
                centerPoint.X = parentWidth - (int)(0.5 * newWidth);
            }

            if ((centerPoint.Y - (0.5 * newHeight)) < 0)
            {
                centerPoint.Y = 0 + (int)(0.5 * newHeight);
            }
            if ((centerPoint.Y + (0.5 * newHeight)) > parentHeight)
            {
                centerPoint.Y = parentHeight - (int)(0.5 * newHeight);
            }

            // create new UpperLeft Point for the reslting rectangle
            Point upperLeft = new Point(centerPoint.X - (int)(0.5 * newWidth), centerPoint.Y - (int)(0.5 * newHeight));

            Size newSize = new Size(newWidth, newHeight);
            m_CenterRect = new Rectangle(upperLeft, newSize);
        }

        private void CreateZoomOutCenterRectangle(DrawArea drawArea)
        {
            // create a rectangle with the same Height/Width ratio as the drawarea
            // to avoid deforming of the annotations.
            int parentHeight = drawArea.Height;
            int parentWidth  = drawArea.Width;
            double newHeight = parentHeight;
            double newWidth  = parentWidth;

            if (m_OldZoomRect.X != -1)
            {
                newWidth = m_OldZoomRect.Width;
                newHeight = m_OldZoomRect.Height;
            }
            // set zoom out by enlarging the view area
            newWidth  *= 1.5;
            newHeight *= 1.5;

            // calculate centerpoint
            Point centerPoint = new Point(m_ZoomRect.X, m_ZoomRect.Y);

            // check bounderies
            if ((centerPoint.X - (0.5 * newWidth)) < 0)
            {
                centerPoint.X = 0 + (int)(0.5 * newWidth);
            }
            if ((centerPoint.X + (0.5 * newWidth)) > parentWidth)
            {
                centerPoint.X = parentWidth - (int)(0.5 * newWidth);
            }

            if ((centerPoint.Y - (0.5 * newHeight)) < 0)
            {
                centerPoint.Y = 0 + (int)(0.5 * newHeight);
            }
            if ((centerPoint.Y + (0.5 * newHeight)) > parentHeight)
            {
                centerPoint.Y = parentHeight - (int)(0.5 * newHeight);
            }

            if ((newWidth > parentWidth) || (newHeight > parentHeight))
            {
                centerPoint.X = (int)(0.5 * parentWidth);
                centerPoint.Y = (int)(0.5 * parentHeight);
            }

            // create new UpperLeft Point for the reslting rectangle
            Point upperLeft = new Point(centerPoint.X - (int)(0.5 * newWidth), centerPoint.Y - (int)(0.5 * newHeight));

            Size newSize = new Size((int)(newWidth), (int)(newHeight));
            m_CenterRect = new Rectangle(upperLeft, newSize);

        }

        public void ResetZoom()
        {
            m_ZoomRect    = new Rectangle(new Point(0, 0),   new Size(0, 0));
            m_OldZoomRect = new Rectangle(new Point(-1, -1), new Size(0, 0));
            m_bZoom = false;
        }

        
        public void SetZoom(ZoomType zoom)
        {
            m_ZoomType = zoom;
        }
	}
}
