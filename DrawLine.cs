using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace IVMAnnotation
{
	/// <summary>
	/// Line graphic object
	/// </summary>
	[ComVisible(false)]
    public class DrawLine : IVMAnnotation.DrawObject
	{
        private Point startPoint;
        private Point endPoint;

        private const string entryStart = "Start";
        private const string entryEnd = "End";

        /// <summary>
        ///  Graphic objects for hit test
        /// </summary>
        private GraphicsPath areaPath = null;
        private Pen areaPen = null;
        private Region areaRegion = null;


		public DrawLine()
		{
            startPoint.X = 0;
            startPoint.Y = 0;
            endPoint.X = 1;
            endPoint.Y = 1;

            Initialize();
		}

        public DrawLine(int x1, int y1, int x2, int y2)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;

            Initialize();
        }


        public override void Draw(Graphics g, float dpi)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color, PenWidth);

            g.DrawLine(pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);

            pen.Dispose();
        }

        public override int HandleCount
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            if ( handleNumber == 1 )
                return startPoint;
            else
                return endPoint;
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override int HitTest(Point point)
        {
            if ( Selected )
            {
                for ( int i = 1; i <= HandleCount; i++ )
                {
                    if ( GetHandleRectangle(i).Contains(point) )
                        return i;
                }
            }

            if ( PointInObject(point) )
                return 0;

            return -1;
        }

        protected override bool PointInObject(Point point)
        {
            CreateObjects();

            return AreaRegion.IsVisible(point);
        }

        public override bool IntersectsWith(Rectangle rectangle)
        {
            CreateObjects();

            return AreaRegion.IsVisible(rectangle);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch ( handleNumber )
            {
                case 1:
                case 2:
                    return Cursors.SizeAll;
                default:
                    return Cursors.Default;
            }
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if ( handleNumber == 1 )
                startPoint = point;
            else
                endPoint = point;

            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;

            Invalidate();
        }

        public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryStart, orderNumber),
                startPoint);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryEnd, orderNumber),
                endPoint);

            base.SaveToStream (info, orderNumber);
        }
		public override void WriteXmlInfo(TextWriter t, bool saveFontToo, ref int depth, string version)
		{			
			string tabs = new string('\t', depth);
			t.WriteLine(tabs+"\t<Line>");
			t.WriteLine(tabs+"\t\t<StartP x='"+startPoint.X+"' y='"+startPoint.Y+"'/>");
			t.WriteLine(tabs+"\t\t<EndP x='"+endPoint.X+"' y='"+endPoint.Y+"'/>");
			depth++;
			base.WriteXmlInfo(t, saveFontToo, ref depth, version);
			t.WriteLine(tabs+"\t</Line>");
		}		
		public override void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            startPoint = (Point)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryStart, orderNumber),
                typeof(Point));

            endPoint = (Point)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryEnd, orderNumber),
                typeof(Point));

            base.LoadFromStream (info, orderNumber);
        }

		private bool Parse_Version1_0_0(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			// Changed false to true.
			bool bOK = true;			
			foreach (XmlElement sub in element)
			{
				switch(sub.Name)
				{
					case "StartP":
						startPoint.X = Int32.Parse(sub.GetAttribute("x"));
						startPoint.Y = Int32.Parse(sub.GetAttribute("y"));
						break;
					case "EndP":
						endPoint.X = Int32.Parse(sub.GetAttribute("x"));
						endPoint.Y = Int32.Parse(sub.GetAttribute("y"));
						break;
					default:
						bOK &= base.Parse(sub, version, originalDrawAreaWidth, originalDrawAreaHeight);
						break;
				}
			}

			return bOK;
		}

		public override bool Parse(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			bool bOK = false;
			switch(version)
			{
				case "1.0.0":
					bOK = Parse_Version1_0_0(element, version, originalDrawAreaWidth, originalDrawAreaHeight);
					break;
				default:
					break;
			}
			return bOK;
		}

		public override void ScaleObject()
        {
            int width  = CurrentDrawAreaWidth;
            int height = CurrentDrawAreaHeight;

            startPoint.X = (int)(startPoint.X * Decimal.Divide(width, OriginalDrawAreaWidth));
            startPoint.Y = (int)(startPoint.Y * Decimal.Divide(height, OriginalDrawAreaHeight));

            endPoint.X = (int)(endPoint.X * Decimal.Divide(width, OriginalDrawAreaWidth));
            endPoint.Y = (int)(endPoint.Y * Decimal.Divide(height, OriginalDrawAreaHeight));

            base.ScaleObject();
        }

        /// <summary>
        /// Invalidate object.
        /// When object is invalidated, path used for hit test
        /// is released and should be created again.
        /// </summary>
        protected void Invalidate()
        {
            if ( AreaPath != null )
            {
                AreaPath.Dispose();
                AreaPath = null;
            }

            if ( AreaPen != null )
            {
                AreaPen.Dispose();
                AreaPen = null;
            }

            if ( AreaRegion != null )
            {
                AreaRegion.Dispose();
                AreaRegion = null;
            }
        }

        /// <summary>
        /// Create graphic objects used from hit test.
        /// </summary>
        protected virtual void CreateObjects()
        {
            if ( AreaPath != null )
                return;

            // Create path which contains wide line
            // for easy mouse selection
            AreaPath = new GraphicsPath();
            AreaPen = new Pen(Color.Black, 7);
            AreaPath.AddLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            AreaPath.Widen(AreaPen);

            // Create region from the path
            AreaRegion = new Region(AreaPath);
        }

        protected GraphicsPath AreaPath
        {
            get
            {
                return areaPath;
            }
            set
            {
                areaPath = value;
            }
        }

        protected Pen AreaPen
        {
            get
            {
                return areaPen;
            }
            set
            {
                areaPen = value;
            }
        }

        protected Region AreaRegion
        {
            get
            {
                return areaRegion;
            }
            set
            {
                areaRegion = value;
            }
        }

	}
}
