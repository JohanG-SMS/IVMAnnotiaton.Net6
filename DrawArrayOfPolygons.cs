using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Xml;

namespace IVMAnnotation
{
	/// <summary>
	/// Summary description for DrawArrayOfPolygons.
	/// </summary>
	[ComVisible(false)]	
	public class DrawArrayOfPolygons : IVMAnnotation.DrawRectangle
	{
		private Shape shape;
		private bool m_bDrawing = false;	
		private int m_number=0;		
		private static ArrayList m_usedNumbers = new ArrayList();
		private string m_freeText="";

		private const string entryShapeName = "ShapeName";
		private const string entryNumber = "ShapeNumber";		
		private const string entryShapePolygonCount = "ShapePolygonCount";
		private const string entryLength = "Length";
		private const string entryPoint = "Point";		

		public DrawArrayOfPolygons()
		{			
			Initialize();
		}

		public DrawArrayOfPolygons(int x, int y, int width, int height)
		{			
			Initialize();
			Rectangle = new Rectangle(x, y, width, height);		
			int newNumber=1;
			while (m_usedNumbers.Contains(newNumber))
			{
				newNumber++;
			}
			m_number = newNumber;	
			m_usedNumbers.Add(m_number);
		}

		/// <summary>
		/// Draw rectangle and array of polygons
		/// </summary>
		/// <param name="g"></param>
		public override void Draw(Graphics g, float dpi)
		{
			if (shape!=null)
			{
				Pen  pen   = new Pen(Color, PenWidth);				
				Brush brush = new SolidBrush(Color);
				Font  font  = new Font(CurrentFont.FontFamily, CurrentFont.Size*96.0F/dpi, CurrentFont.Style);

				int height = Rectangle.Height;
				int width  = Rectangle.Width;

				if (height < 10)
				{
					height = 10;
				}
				if (width < 10)
				{
					width = 10;
				}
				int x = Rectangle.X;
				int y = Rectangle.Y;
				SetRectangle(x, y, width, height);

				if (m_bDrawing)
				{
					//g.DrawRectangle(pen, x, y, width, height);		
					for (int i=0;i<shape.ShapeData.Count;i++)
					{
						System.Drawing.Point [] pointsOrig = (System.Drawing.Point [])shape.ShapeData[i];
						System.Drawing.Point [] pointsDest = new Point[pointsOrig.Length];
						for (int k=0;k<pointsOrig.Length;k++)
						{
							pointsDest[k].X = pointsOrig[k].X*width/1000+x;
							pointsDest[k].Y = pointsOrig[k].Y*height/1000+y;							
						}
						g.DrawPolygon(pen, pointsDest);			
					}
					g.DrawString(m_number.ToString(),font, brush, x+width,y+height);
				}            								

				pen.Dispose();
			}
		}


		public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
		{			
			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryShapeName, orderNumber),
				shape.ShapeName);

			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryNumber, orderNumber),
				m_number);			

			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryShapePolygonCount, orderNumber),
				shape.ShapeData.Count);	

			for(int k=0;k<shape.ShapeData.Count;k++)
			{
				System.Drawing.Point []pointArray = (System.Drawing.Point [])shape.ShapeData[k];
		
				info.AddValue(
					String.Format(CultureInfo.InvariantCulture,
					"{0}{1}{2}",
					entryLength, orderNumber,k),
					pointArray.Length);

				int i = 0;
				foreach ( Point p in pointArray )
				{
					info.AddValue(
						String.Format(CultureInfo.InvariantCulture,
						"{0}{1}{2}{3}",
						entryPoint, orderNumber, i++, k),
						p);
				}
			}

			base.SaveToStream (info, orderNumber);  						
		}

		public override bool SaveToAnnToo()
		{
			return false;
		}

		public override void WriteXmlInfo(TextWriter t, bool saveFontToo, ref int depth, string version)
		{
			if (shape!=null)
			{
				string tabs = new string('\t', depth);
				t.WriteLine(tabs+"\t<ArrayOfPolygons>");
				t.WriteLine(tabs+"\t\t<Shape name='"+shape.ShapeName+"' id='"+m_number+"'/>");
				t.WriteLine(tabs+"\t\t<FreeText>");
				t.WriteLine(m_freeText);
				t.WriteLine(tabs+"\t\t</FreeText>");
				t.WriteLine(tabs+"\t\t\t<ShapeData count='"+shape.ShapeData.Count+"'>");
				for (int i=0;i<shape.ShapeData.Count;i++)
				{
					System.Drawing.Point [] polygon = (System.Drawing.Point [])shape.ShapeData[i];
					t.WriteLine(tabs+"\t\t\t\t<Polygon length='"+polygon.Length+"'>");
					for (int n=0;n<polygon.Length;n++)
					{
						t.WriteLine(tabs+"\t\t\t\t\t<P x='"+polygon[n].X+"' y='"+polygon[n].Y+"'/>");
					}
					t.WriteLine(tabs+"\t\t\t\t</Polygon>");
				}
				t.WriteLine(tabs+"\t\t\t</ShapeData>");			
				depth++;
				base.WriteXmlInfo(t, true, ref depth, version);
				t.WriteLine(tabs+"\t</ArrayOfPolygons>");
			}
		}		
		public override void LoadFromStream(SerializationInfo info, int orderNumber)
		{
			/*
			m_bDrawing = false;		

			string shapeName = (string)info.GetValue(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryShapeName, orderNumber),
				typeof(string));

			shape = new Shape(shapeName);		

			m_number = info.GetInt32(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryNumber, orderNumber));

			m_usedNumbers.Add(m_number);

			int m = info.GetInt32(
				String.Format(CultureInfo.InvariantCulture,
				"{0}{1}",
				entryShapePolygonCount, orderNumber));

			for (int k=0;k<m;k++)
			{
				Point point;
				int n = info.GetInt32(
					String.Format(CultureInfo.InvariantCulture,
					"{0}{1}{2}",
					entryLength, orderNumber, k));
				
				ArrayList polygon = shape.StartPolygon();			
				for ( int i = 0; i < n; i++ )
				{
					point = (Point)info.GetValue(
						String.Format(CultureInfo.InvariantCulture,
						"{0}{1}{2}{3}",
						entryPoint, orderNumber, i, k),
						typeof(Point));
					shape.AddPolygonPoint(polygon,point.X, point.Y);
				}
				shape.EndPolygon(polygon);
			}

			base.LoadFromStream (info, orderNumber);

			m_bDrawing = true;		
			*/	
		}

		private bool Parse_Version1_0_0(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			// Changed false to true.
			bool bOK = true;
			foreach (XmlElement sub in element)
			{
				switch(sub.Name)
				{
					case "Shape":
						string name = sub.GetAttribute("name");
						m_number = Int32.Parse(sub.GetAttribute("id"));
						m_usedNumbers.Add(m_number);
						shape = new Shape(name);						
						break;
					case "FreeText":
						m_freeText = sub.InnerText;
						break;
					case "ShapeData":						
						int count = Int32.Parse(sub.GetAttribute("count"));
						foreach (XmlElement subsub in sub)
						{
							switch(subsub.Name)
							{
								case "Polygon":
									ArrayList polygon = shape.StartPolygon();
									foreach(XmlElement sub3 in subsub)
									{
										switch (sub3.Name)
										{
											case "P":
												int x = Int32.Parse(sub3.GetAttribute("x"));
												int y = Int32.Parse(sub3.GetAttribute("y"));
												shape.AddPolygonPoint(polygon,x,y);
												break;
										}
									}
									shape.EndPolygon(polygon);									
									break;
							}
						}						
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
			m_bDrawing = true;	
			return bOK;
		}
		public override void OnDoubleClick(DrawArea area, EventArgs e)
		{
			m_bDrawing = false;	
	
			string sMsg = "";
			string sOK = "";
			string sCancel = "";
			area.GetTextInputDlgStrings(ref sMsg, ref sOK, ref sCancel);					

			if (shape==null)
			{
				shape = area.ShapesDlg.SelectedShape;				
			}
			else
			{
				shape = SelectPrefArrayOfPolygons(area.ShapesDlg, sOK);			
			}
			m_bDrawing = true;	
		}

		public override void RemoveObject()
		{
			if (m_usedNumbers.Contains(m_number))
			{
				m_usedNumbers.Remove(m_number);
			}
		}
		public Shape SelectPrefArrayOfPolygons(ShapesDlg shapesDlg, string sOK)
		{
			Shape sResult = shape;

			if (sResult==null)
			{
				sResult = new Shape("empty");
			}
			
			shapesDlg.SetLanguageStrings(sOK);
			shapesDlg.FreeText = m_freeText;
			DialogResult result = shapesDlg.ShowDialog();

			if (result == DialogResult.OK)
			{
				if (shapesDlg.ShapeSelected)
				{
					sResult = shapesDlg.SelectedShape;
				}				
				m_freeText = shapesDlg.FreeText;
			} 

			return sResult;
		}
	}
}
