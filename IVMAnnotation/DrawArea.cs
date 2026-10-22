using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Xml;
using System.Globalization;


namespace IVMAnnotation
{
    /// <summary>
    /// Working area.
    /// Handles mouse input and draws graphics objects.
    /// </summary>
    [GuidAttribute("AE558E46-5C60-3F7D-A331-45E692E1E0EB")]
    [ProgId("IVMAnnotation.DrawArea")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class DrawArea : ObservableObject
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
		protected ShapesDlg shapesDlg = new ShapesDlg();
		private float m_dpi=96;

		public ShapesDlg ShapesDlg
		{
			get
			{
				return shapesDlg;
			}
		}

        #region Constructor, Dispose
        public DrawArea()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }
        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // DrawArea
            // 
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "DrawArea";
            this.Size = new System.Drawing.Size(76, 68);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawArea_Paint);
            this.DoubleClick += new System.EventHandler(this.DrawArea_DoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseDown);

		}
        #endregion

        #region Members
        private GraphicsList graphicsList;    

        private AnnotationType activeTool;
		private bool PolygonShapeSet = false;

        private Tool[] tools;                 

		// group loaded image dimensions.
		private int imageWidth;
		private int imageHeight;

        // group selection rectangle
        private Rectangle netRectangle;
        private bool drawNetRectangle = false;

        // Information about owner form
        private DrawAreaFileIO m_DrawAreaIO;

        private bool m_bShowAnnotations = true;

        // zoom members
        private bool m_bZoom = false;
        private Rectangle m_ZoomRect;

        bool m_bDirty = false;
        bool m_bViewMode = false;

        private string m_sTextInputDlgMessage   = "Enter text:";
        private string m_sTextInputDlgOK        = "OK";
        private string m_sTextInputDlgCancel    = "Cancel";

        #endregion

        #region Properties
		/// <summary>
        /// Group selection rectangle. Used for drawing.
        /// </summary>
        [ComVisible(false)]
        public Rectangle NetRectangle
        {
            get
            {
                return netRectangle;
            }
            set
            {
                netRectangle = value;
				NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Flas is set to true if group selection rectangle should be drawn.
        /// </summary>
        [ComVisible(false)]
        public bool DrawNetRectangle
        {
            get
            {
                return drawNetRectangle;
            }
            set
            {
                drawNetRectangle = value;
				NotifyPropertyChanged();
			}
        }

        [ComVisible(false)]
        private DrawAreaFileIO DrawAreaIO
        {
            get
            {
                return m_DrawAreaIO;
            }
            set
            {
                m_DrawAreaIO = value;
            }
        }

        /// <summary>
        /// Active drawing tool.
        /// </summary>
        [ComVisible(false)]
        public AnnotationType ActiveTool
        {
            get
            {
                return activeTool;
            }
            set
            {
                activeTool = value;
				NotifyPropertyChanged();
			}
        }

        /// <summary>
        /// List of graphics objects.
        /// </summary>
        [ComVisible(false)]
        public GraphicsList GraphicsList
        {
            get
            {
                return graphicsList;
            }
            set
            {
                graphicsList = value;
				NotifyPropertyChanged();
			}
        }

        /// <summary>
        /// Sets the zoom on or off
        /// </summary>
        [ComVisible(false)]
        public bool Zoomed
        {
            get 
            {
                return m_bZoom;
            }
            set
            {
                m_bZoom = value;
				NotifyPropertyChanged();
			}
        }

        [ComVisible(false)]
        public bool ShowAnnotation
        {
            get 
            {
                return m_bShowAnnotations;
            }
            set
            {
                m_bShowAnnotations = value;
				NotifyPropertyChanged();
			}
        }

        [ComVisible(false)]
        public bool Dirty
        {
            get 
            {
                return m_bDirty;
            }
            set
            {
                //Console.WriteLine("Dirty = " + value.ToString());
                m_bDirty = value;
				NotifyPropertyChanged();
			}
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Draw graphic objects and 
        /// group selection rectangle (optionally)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if ( graphicsList != null )
            {
                if (m_bZoom)
                {
                    float newWidth  = (float)this.ClientSize.Width/m_ZoomRect.Width;
                    float newHeight = (float)this.ClientSize.Height/m_ZoomRect.Height;

                    e.Graphics.ScaleTransform(newWidth, newHeight);
                    e.Graphics.TranslateTransform(-m_ZoomRect.X, -m_ZoomRect.Y);
                }
				
                graphicsList.Draw(e.Graphics, e.Graphics.DpiY);				
            }

            DrawNetSelection(e.Graphics);
        }

		//protected override void OnPaintBackground(PaintEventArgs pevent)
		//{
		//    if (this.BackgroundImage != null)
		//    {
		//        if (m_bZoom)
		//        {
		//            pevent.Graphics.ScaleTransform((float)this.ClientSize.Width/m_ZoomRect.Width, 
		//                (float)this.ClientSize.Height/m_ZoomRect.Height);
		//            pevent.Graphics.TranslateTransform(-m_ZoomRect.X, -m_ZoomRect.Y);
		//        }

		//        pevent.Graphics.InterpolationMode =
		//            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

		//        pevent.Graphics.DrawImage(this.BackgroundImage, 0, 0,
		//            this.ClientRectangle.Width, this.ClientRectangle.Height);
		//    }
		//    else
		//    {
		//        base.OnPaintBackground(pevent);
		//    }
		//}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			if (this.BackgroundImage != null)
			{
				if (m_bZoom)
				{
					pevent.Graphics.ScaleTransform((float)this.ClientSize.Width / m_ZoomRect.Width,
						(float)this.ClientSize.Height / m_ZoomRect.Height);
					pevent.Graphics.TranslateTransform(-m_ZoomRect.X, -m_ZoomRect.Y);
				}

				pevent.Graphics.InterpolationMode =
					System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

				// Calculate dimensions to maintain aspect ratio
				float imageAspectRatio = (float)this.BackgroundImage.Width / this.BackgroundImage.Height;
				float clientAspectRatio = (float)this.ClientRectangle.Width / this.ClientRectangle.Height;

				float drawWidth, drawHeight;

				if (imageAspectRatio > clientAspectRatio)
				{
					// Image is wider than client area
					drawWidth = this.ClientRectangle.Width;
					drawHeight = this.ClientRectangle.Width / imageAspectRatio;
				}
				else
				{
					// Image is taller than client area
					drawHeight = this.ClientRectangle.Height;
					drawWidth = this.ClientRectangle.Height * imageAspectRatio;
				}

				// Center the image
				float offsetX = (this.ClientRectangle.Width - drawWidth) / 2;
				float offsetY = (this.ClientRectangle.Height - drawHeight) / 2;

				pevent.Graphics.DrawImage(this.BackgroundImage, offsetX, offsetY, drawWidth, drawHeight);
			}
			else
			{
				base.OnPaintBackground(pevent);
			}
		}

		/// <summary>
		/// Mouse down.
		/// Left button down event is passed to active tool.
		/// Right button down event is handled in this class.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawArea_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (!m_bViewMode)
                {
                    if (ShowAnnotation)
                    {
                        if ( e.Button == MouseButtons.Left )
                        {
                            tools[(int)activeTool].OnMouseDown(this, e);
                        }

                        this.Refresh();
                    }
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// Mouse move.
        /// Moving without button pressed or with left button pressed
        /// is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (!m_bViewMode)
                {
                    if (ShowAnnotation)
                    {
                        if ( e.Button == MouseButtons.Left  ||  e.Button == MouseButtons.None )
                            tools[(int)activeTool].OnMouseMove(this, e);
                        else
                            this.Cursor = Cursors.Default;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Mouse up event.
        /// Left button up event is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (!m_bViewMode)
                {
                    if (ShowAnnotation)
                    {
                        if ( e.Button == MouseButtons.Left )
                            tools[(int)activeTool].OnMouseUp(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
        }

        private void DrawArea_DoubleClick(object sender, EventArgs e)
        {
            try 
            {
                if (!m_bViewMode)
                {
                    if (ShowAnnotation)
                    {
                        int n = GraphicsList.SelectionCount;

                        for ( int i = 0; i < n; i++ )
                        {
                            DrawObject o = GraphicsList.GetSelectedObject(i);
                            o.OnDoubleClick(this, e);
                        }
                    }
                }
            }
            catch
            {
            }
        }

		#endregion

        #region Other Functions

        /// <summary>
        /// Initialization
        /// </summary>
        public void Initialize()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint | 
                ControlStyles.DoubleBuffer, 
                true);

            // set default tool
            activeTool = AnnotationType.SelectionPointer;
            DrawAreaIO = new DrawAreaFileIO(this);

            // create list of graphic objects
            graphicsList = new GraphicsList();

            // create array of drawing tools
            tools = new Tool[(int)AnnotationType.NumberOfDrawTools];
            tools[(int)AnnotationType.SelectionPointer] = new ToolPointer();
            tools[(int)AnnotationType.Rectangle] = new ToolRectangle();
            tools[(int)AnnotationType.Oval]      = new ToolEllipse();
            tools[(int)AnnotationType.Line]      = new ToolLine();
            tools[(int)AnnotationType.Polygon]   = new ToolPolygon();
            tools[(int)AnnotationType.Text]      = new ToolText();
            tools[(int)AnnotationType.Arrow]     = new ToolArrow();
            tools[(int)AnnotationType.Zoom]      = new ToolZoom();
			tools[(int)AnnotationType.ArrayOfPolygons] = new ToolArrayOfPolygons();

			// create array of shapes
			shapesDlg.InitShapesData();
        }

        /// <summary>
        ///  Draw group selection rectangle
        /// </summary>
        /// <param name="g"></param>
        [ComVisible(false)]
        public void DrawNetSelection(Graphics g)
        {
            if ( ! DrawNetRectangle )
                return;

            ControlPaint.DrawFocusRectangle(g, NetRectangle, Color.Black, Color.Transparent);
        }

        [ComVisible(false)]
        public void SetZoomFactor(bool bZoom, Rectangle ZoomRect)
        {
            m_bZoom = bZoom;
            m_ZoomRect = ZoomRect;


        }

        [ComVisible(false)]
        public void GetZoomCorrectedCoordinates(Point currentPoint, out Point newPoint)
        {
            newPoint = new Point(0,0);

            Decimal ZoomFactorHeight = Decimal.Divide(this.Height, m_ZoomRect.Height);
            Decimal ZoomFactorWidth  = Decimal.Divide(this.Width,  m_ZoomRect.Width);
                
            newPoint.X = (int)(m_ZoomRect.X + Decimal.Divide(currentPoint.X, ZoomFactorWidth));
            newPoint.Y = (int)(m_ZoomRect.Y + Decimal.Divide(currentPoint.Y, ZoomFactorHeight));
        }

        #endregion

        #region IAnnotation Implementation
        public void SetDrawingMode(AnnotationType type)
        {
            this.ActiveTool = type;
        }

		private void SetPolygonShapes()
		{
			if (!PolygonShapeSet)
			{
				// shapes constructor...
				IVMAnnotation.Shape shape = null;			
				ArrayList polygon = null;

				shape = shapesDlg.AddShape("Cross");
				polygon = shape.StartPolygon();
				shape.AddPolygonPoint(polygon,0,0);
				shape.AddPolygonPoint(polygon,1000,1000);
				shape.EndPolygon(polygon);
				polygon = shape.StartPolygon();
				shape.AddPolygonPoint(polygon,1000,0);
				shape.AddPolygonPoint(polygon,0,1000);
				shape.EndPolygon(polygon);	

				shape = shapesDlg.AddShape("Triangle");
				polygon = shape.StartPolygon();
				shape.AddPolygonPoint(polygon,500,0);
				shape.AddPolygonPoint(polygon,1000,1000);
				shape.AddPolygonPoint(polygon,0,1000);
				shape.AddPolygonPoint(polygon,500,0);
				shape.EndPolygon(polygon);
	
				shape = shapesDlg.AddShape("Square");
				polygon = shape.StartPolygon();
				shape.AddPolygonPoint(polygon,0,0);
				shape.AddPolygonPoint(polygon,1000,0);
				shape.AddPolygonPoint(polygon,1000,1000);
				shape.AddPolygonPoint(polygon,0,1000);
				shape.AddPolygonPoint(polygon,0,0);
				shape.EndPolygon(polygon);

				shape = shapesDlg.AddShape("Marker");
				polygon = shape.StartPolygon();
				shape.AddPolygonPoint(polygon,0,0);
				shape.AddPolygonPoint(polygon,1000,0);
				shape.AddPolygonPoint(polygon,1000,500);
				shape.AddPolygonPoint(polygon,500,1000);
				shape.AddPolygonPoint(polygon,0,500);
				shape.AddPolygonPoint(polygon,0,0);
				shape.EndPolygon(polygon);			

				shapesDlg.InitShapesData();

				shape=null;

				PolygonShapeSet = true;
			}
		}

		public void SetPolygonDrawingMode(PolygonType type)
		{
			// Set the correct shapetype.
			this.SetPolygonShapes();
			this.SetDrawingMode(IVMAnnotation.AnnotationType.ArrayOfPolygons);
			this.shapesDlg.setSelectedShape((int) type);
		}

        public void SetSelectionMode()
        {
            this.ActiveTool = AnnotationType.SelectionPointer;
        }

        public bool SetBitmap(string sBitmap)
        {
            bool bOK = true;

            try
            {
                if (this.BackgroundImage != null)
                {
                    this.BackgroundImage.Dispose();
                    this.BackgroundImage = null;
                }

                if (sBitmap != "")
                {
                    if (File.Exists(sBitmap))
                    {
                        Image image;
                        image = Image.FromFile(sBitmap);

						this.imageWidth=image.Width;
						this.imageHeight=image.Height;

                        this.BackgroundImage = image;
						m_dpi = BackgroundImage.VerticalResolution;
                    }
                    else
                    {
                        bOK = false;
                    }
                }
            }
            catch
            {
                bOK = false;
            }

            return bOK;
        }

        public bool SetBitmap(int hBitmap)
        {
            bool bOK = true;

            try
            {
                if (this.BackgroundImage != null)
                {
                    this.BackgroundImage.Dispose();
                    this.BackgroundImage = null;
                }

                if (hBitmap != -1)
                {
                    Image image;
                    image = Image.FromHbitmap(new IntPtr(hBitmap));

					this.imageWidth=image.Width;
					this.imageHeight=image.Height;

                    this.BackgroundImage = image;
                }
                else
                {
                    bOK = false;
                }
            }
            catch
            {
                bOK = false;
            }

            return bOK;
        }

        public void SetViewMode(bool bView)
        {
            m_bViewMode = bView;
        }

        public void RemoveAll()
        {
            try
            {
                if (GraphicsList.Count > 0)
                {
                    GraphicsList.Clear();
                    this.Dirty = true;
                    this.Refresh();
                }
            }
            catch {}
        }

        public void RemoveSelected()
        {
            try
            {
                if (GraphicsList.SelectionCount > 0)
                {
                    GraphicsList.DeleteSelection();
                    this.Dirty = true;
                    this.Refresh();
                }
            }
            catch {}
        }

        public void SetDefaultDrawingColor(int nRed, int nGreen, int nBlue)
        {
            try
            {
                Color color = Color.FromArgb(nRed, nGreen, nBlue);

                DrawObject.DefaultColor = color;
                this.Refresh();
            }
            catch {}
        }

        public void SetDefaultLineThickness(int nLineThickness)
        {
            try
            {
                if (nLineThickness < 1)
                {
                    nLineThickness = 1;
                }

                DrawObject.DefaultPenWidth = nLineThickness;
                this.Refresh();
            }
            catch {}
        }

        public void SetDefaultFont(string sFamilyName, double dSize, FontStyles style)
        {
            try
            {
                if (dSize < 8)
                {
                    dSize = 8;
                }

                Font font = new Font(sFamilyName, (float)dSize, (FontStyle)style);
                DrawObject.DefaultFont = font;
            
            
                font.Dispose();
                this.Refresh();
            }
            catch {}
        }

		
		public void LoadDefaultsFromXmlFile()
		{
			if (System.IO.File.Exists("IVMAnnotationDefaults.xml"))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load("IVMAnnotationDefaults.xml");
				XmlElement root = doc.DocumentElement;
				foreach (XmlElement sub in root)
				{
					switch(sub.Name)
					{
						case "Color":
							int red = Int32.Parse(sub.GetAttribute("red"));
							int green= Int32.Parse(sub.GetAttribute("green"));
							int blue = Int32.Parse(sub.GetAttribute("blue"));
							SetDefaultDrawingColor(red, green, blue);
							break;
						case "Line":
							int thickness = Int32.Parse(sub.GetAttribute("thickness"));
							SetDefaultLineThickness(thickness);
							break;
						case "Font":
							string fontFamily = sub.GetAttribute("fontFamily");
							float size = float.Parse(sub.GetAttribute("size"), CultureInfo.InvariantCulture);
							bool bold = Boolean.Parse(sub.GetAttribute("bold"));							
							bool italic = Boolean.Parse(sub.GetAttribute("italic"));
							bool underline = Boolean.Parse(sub.GetAttribute("underline"));
							bool strikeout = Boolean.Parse(sub.GetAttribute("strikeout"));
							FontStyles fontStyle = FontStyles.Normal;
							if (bold)
							{
								fontStyle |= FontStyles.Bold;
							}
							if (italic)
							{
								fontStyle |= FontStyles.Italic;
							}
							if (strikeout)
							{
								fontStyle |= FontStyles.Strikeout;
							}
							if (underline)
							{
								fontStyle |= FontStyles.Underline;
							}
							SetDefaultFont(fontFamily, size, fontStyle);
							break;
					}
				}
			}
		}
		public void SaveDefaultsToXmlFile()
		{
			TextWriter t = File.CreateText("IVMAnnotationDefaults.xml");
			t.WriteLine("<Defaults>");
			Color color = DrawObject.DefaultColor;
			int red=color.R;
			int green=color.G;
			int blue=color.B;
			int lineThickness=DrawObject.DefaultPenWidth;
			Font font = DrawObject.DefaultFont;
			string fontFamily=font.FontFamily.ToString();
			float size = font.Size;
			bool bold=font.Bold;
			bool italic=font.Italic;
			bool underline=font.Underline;
			bool strikeout=font.Strikeout;
			t.WriteLine ("\t<Color red='"+red+"' green='"+green+"' blue='"+blue+"'/>");
			t.WriteLine ("\t<Line thickness='"+lineThickness+"' />");
			t.WriteLine ("\t<Font fontFamily='"+fontFamily+"' size='"+size+"' bold='"+bold+"' italic='"+italic+"' underline='"+underline+"' strikeout='"+strikeout+"' />");
			t.WriteLine("</Defaults>");
			t.Close();
		}

		public bool OpenFile(string sFilename)
        {
            bool bOK = false;

            try
            {
                bOK = DrawAreaIO.OpenAnnotationFile(sFilename);
            
                if (bOK)
                {
                    this.Dirty = false;
                }
            }
            catch 
            {
                bOK = false;
            }

            return bOK;
        }

		public bool OpenAnnotationXMLFile(string sFilename)
		{
			bool bOK = false;

			try
			{
				bOK = DrawAreaIO.OpenAnnotationXMLFile(sFilename);
            
				if (bOK)
				{
					this.Dirty = false;
				}
			}
			catch 
			{
				bOK = false;
			}

			return bOK;
		}

        public bool SaveFile(string sFilename)
        {
            bool bOK = false;

            try
            {
                bOK = DrawAreaIO.SaveAnnotationFile(sFilename);

                if (bOK)
                {
                    this.Dirty = false;
                }
            }
            catch 
            {
                bOK = false;
            }
			SaveAnnotationXMLFile(sFilename+"X");
            return bOK;
        }

		public bool SaveAnnotationXMLFile(string sFileName)
		{
			bool bOK = false;
			try
			{
				bOK = DrawAreaIO.SaveAnnotationXMLFile(sFileName);
				if (bOK)
				{
					this.Dirty = false;
				}
			}
			catch
			{
				bOK = false;
			}
			return bOK;
		}
		public void ShowAnnotations(bool bShow)
        {
            try
            {
                ShowAnnotation = bShow;

                int nCount = this.graphicsList.Count;

                for (int i = 0; i < nCount; i++)
                {
                    DrawObject shape = graphicsList[i];
                    shape.Show = bShow;
                }
                this.Refresh();
            }
            catch {}
        }

        public void ExportImage(string sFilename, ExportType type)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.Refresh();

                Thread.Sleep(800);
                DrawAreaIO.SaveImageToFile(sFilename, type);
            }
            catch {}

            Cursor.Current = Cursors.Default;
        }

        public void ExportImageFromFile(string sSrcImgFile, string sSrcAnnFile, string sDestFile, ExportType type)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                DrawAreaIO.SaveImageFromFileToFile(sSrcImgFile, sSrcAnnFile, sDestFile, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Cursor.Current = Cursors.Default;
        }

		public bool ExportImageFromFile2(string sSrcImgFile, string sSrcAnnFile, string sDestFile, ExportType type, int width, int height)
		{
			bool m_OK = false;
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				m_OK = DrawAreaIO.SaveImageFromFileToFile2(sSrcImgFile, sSrcAnnFile, sDestFile, type, width, height);				
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}

			Cursor.Current = Cursors.Default;
			return m_OK;
		}

        public void SetDrawingColor(int nRed, int nGreen, int nBlue)
        {
            try 
            {
                Color color = Color.FromArgb(nRed, nGreen, nBlue);

                int n = GraphicsList.SelectionCount;

                for ( int i = 0; i < n; i++ )
                {
                    DrawObject o = GraphicsList.GetSelectedObject(i);
                    o.Color = color;
                    this.Dirty = true;
                }
            }
            catch
            {
            }
            this.Refresh();
        }

        public void SetLineThickness(int nLineThickness)
        {
            if (nLineThickness < 1)
            {
                nLineThickness = 1;
            }

            try 
            {
                int n = GraphicsList.SelectionCount;

                for ( int i = 0; i < n; i++ )
                {
                    DrawObject o = GraphicsList.GetSelectedObject(i);
                    o.PenWidth = nLineThickness;
                    this.Dirty = true;
                }
            }
            catch
            {
            }

            this.Refresh();
        }

        public void SetFont(string sFamilyName, double dSize, FontStyles style)
        {
            if (dSize < 8)
            {
                dSize = 8;
            }

            try 
            {
                int n = GraphicsList.SelectionCount;
                Font font = new Font(sFamilyName, (float)dSize, (FontStyle)style);

                for ( int i = 0; i < n; i++ )
                {
                    DrawObject o = GraphicsList.GetSelectedObject(i);
                    o.CurrentFont = font;
                    this.Dirty = true;
                }

                font.Dispose();
            }
            catch
            {
            }

            this.Refresh();
        }

        public void SetZoomMode(ZoomType zoom)
        {
            try
            {
                this.ActiveTool = AnnotationType.Zoom;
                ToolZoom zoomTool = (ToolZoom)(tools[(int)AnnotationType.Zoom]);
                zoomTool.SetZoom(zoom);
            }
            catch {}
        }

        public void ResetZoom()
        {
            try
            {
                ToolZoom zoom = (ToolZoom)(tools[(int)AnnotationType.Zoom]);
                zoom.ResetZoom();

                m_bZoom = false;
                m_ZoomRect = new Rectangle(new Point(0, 0), new Size(1, 1));
            }
            catch {}

            this.Refresh();
        }

        public int GetNumberOfSelectedItems()
        {
            return GraphicsList.SelectionCount;
        }

        public void UnselectAll()
        {
            try
            {
                graphicsList.UnselectAll();
            }
            catch {}                
                
            this.Refresh();
        }

        public bool IsDirty()
        {
            return Dirty;
        }
		
		public int getImgWidth()
		{
			return imageWidth;
		}

		public int getImgHeight()
		{
			return imageHeight;
		}
       
        #endregion

        #region Multi Language functions
        public void SetTextString(StringItems item, string sText)
        {
            if (item == StringItems.TextInputDlgMessage)
            {
                m_sTextInputDlgMessage = sText;
            }
            else if (item == StringItems.TextInputDlgOK)
            {
                m_sTextInputDlgOK = sText;
            }
            else if (item == StringItems.TextInputDlgCancel)
            {
                m_sTextInputDlgCancel = sText;
            }
        }
        
        [ComVisible(false)]
        public void GetTextInputDlgStrings(ref string sMsg, ref string sOK, ref string sCancel)
        {
            sMsg    = m_sTextInputDlgMessage;
            sOK     = m_sTextInputDlgOK;     
            sCancel = m_sTextInputDlgCancel; 
        }

        #endregion

        #region COM registration
        [ComRegisterFunction]
        static void ComRegister(Type t) 
        {                   
            string keyName = @"CLSID\" + t.GUID.ToString("B");
            using( RegistryKey key =
                       Registry.ClassesRoot.OpenSubKey(keyName, true) ) 
            {
                key.CreateSubKey("Control").Close();
                using( RegistryKey subkey = key.CreateSubKey("MiscStatus") ) 
                {
                    subkey.SetValue("", "131457");
                }
                using( RegistryKey subkey = key.CreateSubKey("TypeLib") ) 
                {
                    Guid libid = t.Assembly.GetCustomAttribute<GuidAttribute>() is GuidAttribute guidAttribute
                        ? new Guid(guidAttribute.Value)
                        : t.GUID;
                    subkey.SetValue("", libid.ToString("B"));
                }
                using( RegistryKey subkey = key.CreateSubKey("Version") ) 
                {
                    Version ver = t.Assembly.GetName().Version;
                    string version =
                        string.Format("{0}.{1}",
                        ver.Major,
                        ver.Minor);
                    if( version == "0.0" ) version = "1.0";
                    subkey.SetValue("", version);
                }
                string versionAss = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                using( RegistryKey subkey = key.OpenSubKey("InprocServer32", true)) 
                {
                    subkey.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
                }
                using( RegistryKey subkey = key.OpenSubKey("InprocServer32" + "\\" + versionAss, true)) 
                {
                    subkey.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase); 
                }
            }
        }

        [ComUnregisterFunction]
        static void ComUnregister(Type t) 
        {
            // Delete entire CLSID\{clsid} subtree
            string keyName = @"CLSID\" + t.GUID.ToString("B");
            Registry.ClassesRoot.DeleteSubKeyTree(keyName);
        }
        #endregion
	}
}
