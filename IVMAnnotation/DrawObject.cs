using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace IVMAnnotation
{
	/// <summary>
	/// Base class for all draw objects
	/// </summary>
    [ComVisible(false)]
    public abstract class DrawObject
    {
        #region Members

        // Object properties
        private bool  selected;
        private Color color;
        private int   penWidth;
        private bool  m_bShow = true;
        private Font  font;
        private int   m_nCurrentDrawAreaWidth;
        private int   m_nCurrentDrawAreaHeight;
        private int   m_nOriginalDrawAreaWidth;
        private int   m_nOriginalDrawAreaHeight;


        // Last used property values 
        private static Color defaultColor    = Color.Black;
        private static int   defaultPenWidth = 1;
        private static Font  defaultFont     = new Font("Arial", 16);

        // Entry names for serialization
        private const string entryColor    = "Color";
        private const string entryPenWidth = "PenWidth";
        private const string entryFont     = "FontName";
        private const string entryWidth    = "DrawAreaWidth";
        private const string entryHeight   = "DrawAreaHeight";		
        #endregion

        #region Properties

        /// <summary>
        /// Selection flag
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }

        /// <summary>
        /// Show/Hide flag
        /// </summary>
        public bool Show
        {
            get
            {
                return m_bShow;
            }
            set
            {
                m_bShow = value;
            }
        }

        /// <summary>
        /// Color
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Pen width
        /// </summary>
        public int PenWidth
        {
            get
            {
                return penWidth;
            }
            set
            {
                penWidth = value;
            }
        }

        /// <summary>
        /// Number of handles
        /// </summary>
        public virtual int HandleCount
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Last used color
        /// </summary>
        public static Color DefaultColor
        {
            get
            {
                return defaultColor;    
            }
            set
            {
                defaultColor = value;
            }
        }

        /// <summary>
        /// Last used pen width
        /// </summary>
        public static int DefaultPenWidth
        {
            get
            {
                return defaultPenWidth;
            }
            set
            {
                defaultPenWidth = value;
            }
        }

        /// <summary>
        /// Default Font
        /// </summary>
        public static Font DefaultFont
        {
            get
            {
                return defaultFont;
            }
            set
            {
                defaultFont = value;
            }
        }

        /// <summary>
        /// Last used font
        /// </summary>
        public Font CurrentFont
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }

        public int OriginalDrawAreaWidth
        {
            get
            {
                return m_nOriginalDrawAreaWidth;
            }
            set
            {
                m_nOriginalDrawAreaWidth = value;
            }
        }

        public int OriginalDrawAreaHeight
        {
            get
            {
                return m_nOriginalDrawAreaHeight;
            }
            set
            {
                m_nOriginalDrawAreaHeight = value;
            }
        }

        public int CurrentDrawAreaWidth
        {
            get
            {
                return m_nCurrentDrawAreaWidth;
            }
            set
            {
                m_nCurrentDrawAreaWidth = value;
            }
        }

        public int CurrentDrawAreaHeight
        {
            get
            {
                return m_nCurrentDrawAreaHeight;
            }
            set
            {
                m_nCurrentDrawAreaHeight = value;
            }
        }
        #endregion

        #region Virtual Functions

        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g, float dpi)
        {
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Point GetHandle(int handleNumber)
        {
            return new Point(0, 0);
        }

        /// <summary>
        /// Get handle rectangle by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);

            return new Rectangle(point.X - 3, point.Y - 3, 7, 7);
        }

        /// <summary>
        /// Draw tracker for selected object
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawTracker(Graphics g, float dpi)
        {
            if ( ! Selected )
                return;

            SolidBrush brush = new SolidBrush(Color.Black);

            for ( int i = 1; i <= HandleCount; i++ )
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }

            brush.Dispose();
        }

        /// <summary>
        /// Hit test.
        /// Return value: -1 - no hit
        ///                0 - hit anywhere
        ///                > 1 - handle number
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual int HitTest(Point point)
        {
            return -1;
        }


        /// <summary>
        /// Test whether point is inside of the object
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected virtual bool PointInObject(Point point)
        {
            return false;
        }
        

        /// <summary>
        /// Get curesor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public virtual bool IntersectsWith(Rectangle rectangle)
        {
            return false;
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual void Move(int deltaX, int deltaY)
        {
        }

        /// <summary>
        /// Move handle to the point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public virtual void MoveHandleTo(Point point, int handleNumber)
        {
        }

        /// <summary>
        /// Dump (for debugging)
        /// </summary>
        public virtual void Dump()
        {
            Trace.WriteLine("");
            Trace.WriteLine(this.GetType().Name);
            Trace.WriteLine("Selected = " + selected.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Normalize object.
        /// Call this function in the end of object resizing.
        /// </summary>
        public virtual void Normalize()
        {
        }


        /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public virtual void SaveToStream(SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryWidth, orderNumber),
                OriginalDrawAreaWidth);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryHeight, orderNumber),
                OriginalDrawAreaHeight);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}",
                    entryColor, orderNumber),
                Color.ToArgb());

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryPenWidth, orderNumber),
                PenWidth);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryFont, orderNumber),
                font);
        }		

		public virtual bool SaveToAnnToo()
		{
			return true;
		}
		private bool Parse_Version1_0_0(XmlElement element)
		{
			bool bOK = true;
			CurrentFont = DefaultFont;
			foreach (XmlElement sub in element)
			{
				switch(sub.Name)
				{
					case "Color":
						int n = Int32.Parse(sub.GetAttribute("value"));
						Color = Color.FromArgb(n);	
						break;
					case "PenWidth":
						PenWidth = Int32.Parse(sub.GetAttribute("value"));
						break;
					case "Font":
						bool bold = Boolean.Parse(sub.GetAttribute("bold"));
						string fontFamily = sub.GetAttribute("fontFamily");
						string gdcCharSet = sub.GetAttribute("gdiCharSet");
						//int height = Int32.Parse(sub.GetAttribute("height"));
						bool italic = Boolean.Parse(sub.GetAttribute("italic"));
						float size = float.Parse(sub.GetAttribute("size"), CultureInfo.InvariantCulture);
						//float sizeInPoints = float.Parse(sub.GetAttribute("sizeInPoints"));
						bool strikeout = Boolean.Parse(sub.GetAttribute("strikeout"));
						string style = sub.GetAttribute("style");
						bool underline = Boolean.Parse(sub.GetAttribute("underline"));
						string unit = sub.GetAttribute("unit");
						System.Drawing.GraphicsUnit currentFontUnit= System.Drawing.GraphicsUnit.Point;
						switch(unit)
						{
							case "Display":
								currentFontUnit= System.Drawing.GraphicsUnit.Display;
								break;
							case "Document":
								currentFontUnit= System.Drawing.GraphicsUnit.Document;
								break;
							case "Inch":
								currentFontUnit= System.Drawing.GraphicsUnit.Inch;
								break;
							case "Millimeter":
								currentFontUnit= System.Drawing.GraphicsUnit.Millimeter;
								break;
							case "Pixel":
								currentFontUnit= System.Drawing.GraphicsUnit.Pixel;
								break;
							case "Point":
								currentFontUnit= System.Drawing.GraphicsUnit.Point;
								break;
							case "World":
								currentFontUnit= System.Drawing.GraphicsUnit.World;
								break;
						}						
						FontStyle fontStyle = FontStyle.Regular;
						if (bold)
						{
							fontStyle |= FontStyle.Bold;
						}
						if (italic)
						{
							fontStyle |= FontStyle.Italic;
						}
						if (strikeout)
						{
							fontStyle |= FontStyle.Strikeout;
						}
						if (underline)
						{
							fontStyle |= FontStyle.Underline;
						}
						CurrentFont = new Font(fontFamily, size, fontStyle, currentFontUnit);
						break;
					default:
						bOK = false;
						break;
				}
			}

			return bOK;
		}

		public virtual bool Parse(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			bool bOK = true;
			OriginalDrawAreaWidth = originalDrawAreaWidth;
			OriginalDrawAreaHeight = originalDrawAreaHeight;
			switch(version)
			{
				case "1.0.0":
					bOK = Parse_Version1_0_0(element);
					break;
				default:
					bOK = false;
					break;
			}

			return bOK;
		}
		public virtual void WriteXmlInfo(TextWriter t, bool saveFontToo, ref int depth, string version)
		{				
				string tabs = new string('\t',depth);
				//t.WriteLine(tabs+"\t<Object name='"+this.GetType().ToString()+"'>");
				t.WriteLine(tabs+"\t<Object>");
				t.WriteLine(tabs+"\t\t<Color value='"+Color.ToArgb()+"'/>");
				t.WriteLine(tabs+"\t\t<PenWidth value='"+PenWidth+"'/>");				
				if (saveFontToo)
				{
					t.WriteLine(tabs+"\t\t<Font ");
					t.WriteLine(tabs+"\t\t\tbold='"+font.Bold.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tfontFamily='"+font.FontFamily.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tgdiCharSet='"+font.GdiCharSet.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tgdiVerticalFont='"+font.GdiVerticalFont.ToString()+"'");
					//t.WriteLine(tabs+"\t\t\theight='"+font.Height.ToString()+"'");
					t.WriteLine(tabs+"\t\t\titalic='"+font.Italic.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tname='"+font.Name.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tsize='"+font.Size.ToString(CultureInfo.InvariantCulture)+"'");
					//t.WriteLine(tabs+"\t\t\tsizeInPoints='"+font.SizeInPoints.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tstrikeout='"+font.Strikeout.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tstyle='"+font.Style.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tunderline='"+font.Underline.ToString()+"'");
					t.WriteLine(tabs+"\t\t\tunit='"+font.Unit.ToString()+"'");
					t.WriteLine(tabs+"\t\t/>");				
				}
				t.WriteLine(tabs+"\t</Object>");
		}

        /// <summary>
        /// Load object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        public virtual void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            OriginalDrawAreaWidth = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryWidth, orderNumber));

            OriginalDrawAreaHeight = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryHeight, orderNumber));

            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                    "{0}{1}",
                    entryColor, orderNumber));

            Color = Color.FromArgb(n);

            PenWidth = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryPenWidth, orderNumber));

            CurrentFont = (Font)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryFont, orderNumber),
                typeof(Font));
        }

        public virtual void ScaleObject()
        {
            int width  = CurrentDrawAreaWidth;			
            
            penWidth = (int)(penWidth * Decimal.Divide(width, OriginalDrawAreaWidth));
            if (penWidth < 1)
            {
                PenWidth = 1;
            }
        }

        public virtual void OnDoubleClick(DrawArea area, EventArgs e)
        {
			
        }
       
		public virtual void RemoveObject(){}
		#endregion

        #region Other functions

        /// <summary>
        /// Initialization
        /// </summary>
        protected void Initialize()
        {
            color    = defaultColor;
            penWidth = defaultPenWidth;
            font     = defaultFont;
        }

        #endregion
    }
}
