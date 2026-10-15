using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace IVMAnnotation
{
	/// <summary>
	/// Polygon graphic object
	/// </summary>
	[ComVisible(false)]
    public class DrawPolygon : IVMAnnotation.DrawLine
	{
        private ArrayList pointArray;         // list of points
        private Cursor handleCursor;

        private const string entryLength = "Length";
        private const string entryPoint = "Point";

		public DrawPolygon()
		{
            pointArray = new ArrayList();

            LoadCursor();
            Initialize();
		}

        public DrawPolygon(int x1, int y1, int x2, int y2)
        {
            pointArray = new ArrayList();
            pointArray.Add(new Point(x1, y1));
            pointArray.Add(new Point(x2, y2));

            LoadCursor();
            Initialize();
        }

        public override void Draw(Graphics g, float dpi)
        {
            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color, PenWidth);

            IEnumerator enumerator = pointArray.GetEnumerator();

            if ( enumerator.MoveNext() )
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while ( enumerator.MoveNext() )
            {
                x2 = ((Point)enumerator.Current).X;
                y2 = ((Point)enumerator.Current).Y;

                g.DrawLine(pen, x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
            }

            pen.Dispose();
        }

        public void AddPoint(Point point)
        {
            pointArray.Add(point);
        }

        public override int HandleCount
        {
            get
            {
                return pointArray.Count;
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            if ( handleNumber < 1 )
                handleNumber = 1;

            if ( handleNumber > pointArray.Count )
                handleNumber = pointArray.Count;

            return ((Point)pointArray[handleNumber - 1]);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            return handleCursor;
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if ( handleNumber < 1 )
                handleNumber = 1;

            if ( handleNumber > pointArray.Count)
                handleNumber = pointArray.Count;

            pointArray[handleNumber-1] = point;

            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            int n = pointArray.Count;
            Point point;

            for ( int i = 0; i < n; i++ )
            {
                point = new Point( ((Point)pointArray[i]).X + deltaX, ((Point)pointArray[i]).Y + deltaY);

                pointArray[i] = point;
            }

            Invalidate();
        }

		public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
		{
			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryLength, orderNumber),
				pointArray.Count);

			int i = 0;
			foreach ( Point p in pointArray )
			{
				info.AddValue(
					String.Format(CultureInfo.InvariantCulture,
					"{0}{1}-{2}",
					entryPoint, orderNumber, i++),
					p);

			}

			base.SaveToStream (info, orderNumber);  // ??
		}

		public override void WriteXmlInfo(TextWriter t, bool saveFontToo, ref int depth, string version)
		{
			string tabs = new string('\t', depth);
			t.WriteLine(tabs+"\t<DrawPolygon count='+pointArray.Count+'>");
			for (int i=0;i<pointArray.Count;i++)
			{
				Point point = (Point)pointArray[i];
				t.WriteLine(tabs+"\t\t<P x='"+point.X+"' y='"+point.Y+"'/>");
			}			
			depth++;
			base.WriteXmlInfo(t, true, ref depth, version);
			t.WriteLine(tabs+"\t</DrawPolygon>");
		}		
		private bool Parse_Version1_0_0(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			// changed false to true
			bool bOK = true;			
			foreach (XmlElement sub in element)
			{
				switch(sub.Name)
				{
					case "P":
						int x = Int32.Parse(sub.GetAttribute("x"));
						int y = Int32.Parse(sub.GetAttribute("y"));
						pointArray.Add(new Point(x,y));						
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

		public override void LoadFromStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
		{
			Point point;
			int n = info.GetInt32(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryLength, orderNumber));

			for ( int i = 0; i < n; i++ )
			{
				point = (Point)info.GetValue(
					String.Format(CultureInfo.InvariantCulture,
					"{0}{1}-{2}",
					entryPoint, orderNumber, i),
					typeof(Point));

				pointArray.Add(point);
			}

			base.LoadFromStream (info, orderNumber);
		}

        public override void ScaleObject()
        {
            int width  = CurrentDrawAreaWidth;
            int height = CurrentDrawAreaHeight;

            int n = pointArray.Count;
            Point point;

            for ( int i = 0; i < n; i++ )
            {
                int x = (int)(((Point)pointArray[i]).X * Decimal.Divide(width, OriginalDrawAreaWidth));
                int y = (int)(((Point)pointArray[i]).Y * Decimal.Divide(height, OriginalDrawAreaHeight));

                point = new Point(x, y);

                pointArray[i] = point;
            }

            base.ScaleObject();
        }


        /// <summary>
        /// Create graphic object used for hit test
        /// </summary>
        protected override void CreateObjects()
        {
            if ( AreaPath != null )
                return;

            // Create closed path which contains all polygon vertexes
            AreaPath = new GraphicsPath();

            int x1 = 0, y1 = 0;     // previous point
            int x2, y2;             // current point

            IEnumerator enumerator = pointArray.GetEnumerator();

            if ( enumerator.MoveNext() )
            {
                x1 = ((Point)enumerator.Current).X;
                y1 = ((Point)enumerator.Current).Y;
            }

            while ( enumerator.MoveNext() )
            {
                x2 = ((Point)enumerator.Current).X;
                y2 = ((Point)enumerator.Current).Y;

                AreaPath.AddLine(x1, y1, x2, y2);

                x1 = x2;
                y1 = y2;
            }

            AreaPath.CloseFigure();

            // Create region from the path
            AreaRegion = new Region(AreaPath);
        }

        private void LoadCursor()
        {
            handleCursor = new Cursor(Helper.GetCursorStream("cur_PolyHandle.cur"));
        }

        public void OnClosePolygon(DrawArea area)
        {
            Point firstPoint = (Point)pointArray[0];
            pointArray.Add(firstPoint);
        }
	}
}
