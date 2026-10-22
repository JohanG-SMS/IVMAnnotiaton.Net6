using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;


using IVMAnnotation.CaptureDll;

namespace IVMAnnotation
{
	/// <summary>
	/// Summary description for DrawAreaFileIO.
	/// </summary>
	[ComVisible(false)]
    public class DrawAreaFileIO
	{
        private DrawArea m_drawArea;

		public DrawAreaFileIO(DrawArea drawArea)
		{
            m_drawArea = drawArea;
		}

        public bool OpenAnnotationFile(string sFilename)
        {
            bool bOK = false;

            try 
            {
                FileStream stream = new FileStream(sFilename, FileMode.Open, FileAccess.Read);
                try
                {
                    // Deserialize object from text format
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new Version1ToVersion2DeserializationBinder();


                    // raise event to load document from file
                    m_drawArea.GraphicsList = (GraphicsList)formatter.Deserialize(stream);

                    int nCount = m_drawArea.GraphicsList.Count;

                    for (int i = 0; i < nCount; i++)
                    {
                        DrawObject shape = m_drawArea.GraphicsList[i];
                        shape.CurrentDrawAreaHeight = m_drawArea.ReferenceHeight;
                        shape.CurrentDrawAreaWidth  = m_drawArea.ReferenceWidth;
                        shape.ScaleObject();
                    }

                    m_drawArea.Refresh();
                    bOK = true;
                }
                catch (SerializationException e) 
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bOK = false;
            }

            return bOK;
        }

		public bool OpenAnnotationXMLFile(string sFilename)
		{
			bool bOK = false;
			System.Xml.XmlDocument doc = new XmlDocument();
			try 
			{								
				doc.Load(sFilename);
				XmlElement root = doc.DocumentElement;
				string version = root.GetAttribute("version");				
				switch(version)
				{
					case "1.0.0":
						bOK = ParseAnnotationXMLFileVersion1_0_0(root, version);
						break;
					default:
						bOK = false;
						Console.WriteLine("Version "+version+"is not supported.");
						break;
				}
				int nCount = m_drawArea.GraphicsList.Count;

				for (int i = 0; i < nCount; i++)
				{
					DrawObject shape = m_drawArea.GraphicsList[i];
					shape.CurrentDrawAreaHeight = m_drawArea.ReferenceHeight;
					shape.CurrentDrawAreaWidth  = m_drawArea.ReferenceWidth;
					shape.ScaleObject();
				}

				m_drawArea.Refresh();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				bOK = false;
			}

			return bOK;
		}
		
		public bool ParseAnnotationXMLFileVersion1_0_0(XmlElement root, string version)
		{
			// Changed false to true.
			bool bOK = true;
			int originalDrawAreaWidth = -1;
			int originalDrawAreaHeight = -1;
			foreach(XmlElement element in root)
			{
				switch (element.Name)
				{
					case "OriginalDrawAreaWidth":
						originalDrawAreaWidth = Int32.Parse(element.GetAttribute("value"));
						break;
					case "OriginalDrawAreaHeight":
						originalDrawAreaHeight = Int32.Parse(element.GetAttribute("value"));
						break;
					default:
						string name = "IVMAnnotation.Draw"+element.Name;
						Type type = Type.GetType(name);
						Object [] args=null;						
						ConstructorInfo[] constructorInfo = type.GetConstructors();						
						ConstructorInfo baseConstructor=null;
						for (int i=0;i<constructorInfo.Length;i++)
						{
							System.Reflection.ParameterInfo []parameterInfo =constructorInfo[i].GetParameters();
							if (parameterInfo.Length ==0)
							{								
								baseConstructor = constructorInfo[i];
							}
						}
						if (baseConstructor==null)
						{
							System.Console.WriteLine("Error '"+name+"' has no base constructor.");
						}
						else
						{
							DrawObject o = (DrawObject)constructorInfo[0].Invoke(args);
							m_drawArea.GraphicsList.Add(o);
							bOK &= o.Parse(element, version, originalDrawAreaWidth, originalDrawAreaHeight);
						}
						break;
				}
			}
			return bOK;
		}
		public GraphicsList ParseAnnotationXMLFileForExportVersion1_0_0(XmlElement root, string version)
		{
			GraphicsList list = new GraphicsList();
			int originalDrawAreaWidth = -1;
			int originalDrawAreaHeight = -1;
			foreach(XmlElement element in root)
			{
				switch (element.Name)
				{
					case "OriginalDrawAreaWidth":
						originalDrawAreaWidth = Int32.Parse(element.GetAttribute("value"));
						break;
					case "OriginalDrawAreaHeight":
						originalDrawAreaHeight = Int32.Parse(element.GetAttribute("value"));
						break;
					default:
						string name = "IVMAnnotation.Draw"+element.Name;
						Type type = Type.GetType(name);
						Object [] args=null;						
						ConstructorInfo[] constructorInfo = type.GetConstructors();						
						ConstructorInfo baseConstructor=null;
						for (int i=0;i<constructorInfo.Length;i++)
						{
							System.Reflection.ParameterInfo []parameterInfo =constructorInfo[i].GetParameters();
							if (parameterInfo.Length ==0)
							{								
								baseConstructor = constructorInfo[i];
							}
						}
						if (baseConstructor==null)
						{
							System.Console.WriteLine("Error '"+name+"' has no base constructor.");
						}
						else
						{
							DrawObject o = (DrawObject)constructorInfo[0].Invoke(args);
							list.Add(o);
							o.Parse(element, version, originalDrawAreaWidth, originalDrawAreaHeight);
						}
						break;
				}
			}
			return list;
		}
		public bool SaveAnnotationFile(string sFilename)
        {
            bool bOK = false;

            try 
            {
                int nCount = m_drawArea.GraphicsList.Count;

                for (int i = 0; i < nCount; i++)
                {
                    DrawObject shape = m_drawArea.GraphicsList[i];
                    shape.OriginalDrawAreaHeight = m_drawArea.ReferenceHeight;
                    shape.OriginalDrawAreaWidth  = m_drawArea.ReferenceWidth;
                }

                using( Stream stream = new FileStream(
                           sFilename, FileMode.Create, FileAccess.Write) ) 
                {
                    // Serialize object to text format
                    IFormatter formatter = new BinaryFormatter();

                    formatter.Serialize(stream, m_drawArea.GraphicsList);
                    bOK = true;
                }
            }
            catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
                bOK = false;
            }

            return bOK;
        }

		public bool SaveAnnotationXMLFile(string sFilename)
		{
			bool bOK = false;
			try
			{
				int nCount = m_drawArea.GraphicsList.Count;
				for (int i=0;i<nCount;i++)
				{
					DrawObject shape = m_drawArea.GraphicsList[i];
					shape.OriginalDrawAreaHeight = m_drawArea.ReferenceHeight;
					shape.OriginalDrawAreaWidth = m_drawArea.ReferenceWidth;
				}

				if (File.Exists(sFilename))
				{
					File.Delete(sFilename);
				}
				TextWriter t = File.CreateText(sFilename);
				string version="1.0.0";			
				t.WriteLine("<Annotation version='"+version+"'>");
				m_drawArea.GraphicsList.WriteXmlInfo(t, version);
				t.WriteLine("</Annotation>");	
				t.Close();
				bOK = true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				bOK = false;
			}
			return bOK;
		}

        public void SaveImageToFile(string sFilename, ExportType type)
        {
            Bitmap bmp = CaptureDrawArea();

            string sPath = Path.GetDirectoryName(sFilename) + "\\";
            string sFile = sPath + Path.GetFileNameWithoutExtension(sFilename);

            switch (type)
            {
                case ExportType.Bmp:
                {
                    bmp.Save(sFile + ".bmp", ImageFormat.Bmp);
                    break;
                }
                case ExportType.Jpg:
                {
                    bmp.Save(sFile + ".jpg", ImageFormat.Jpeg);
                    break;
                }
                case ExportType.Png:
                {
                    bmp.Save(sFile + ".png", ImageFormat.Png);
                    break;
                }
                case ExportType.Tiff:
                {
                    bmp.Save(sFile + ".tiff", ImageFormat.Tiff);
                    break;
                }
                default:
                {
                    bmp.Save(sFile + ".bmp", ImageFormat.Bmp);
                    break;
                }
            }
        }

		public void SaveImageFromFileToFile(string sSrcFile, string sSrcAnnFile, string sDestFile, ExportType type)
		{
			SaveImageFromFileToFile2(sSrcFile, sSrcAnnFile, sDestFile, type, -1,-1);
		}

        public bool SaveImageFromFileToFile2(string sSrcFile, string sSrcAnnFile, string sDestFile, ExportType type, int width, int height)
        {
			bool m_OK = false;
            //open the file
            Image srcImage = Image.FromFile(sSrcFile);

			Image tempImage = null;

			if (width<0 || height<0)
			{
				width  = srcImage.Width;
				height = srcImage.Height;
			
				//create temporary
				tempImage = new Bitmap(width, height);
				((Bitmap)tempImage).SetResolution(srcImage.VerticalResolution, srcImage.VerticalResolution);

				//get graphics
				Graphics graphics = Graphics.FromImage(tempImage);

				//copy original
				graphics.DrawImage(srcImage,0,0);

				GraphicsList list = null;

				list = OpenAnnFileForExport(sSrcAnnFile);

				int nCount = list.Count;

				for (int i = 0; i < nCount; i++)
				{
					DrawObject shape = list[i];
					shape.CurrentDrawAreaHeight = srcImage.Height;
					shape.CurrentDrawAreaWidth  = srcImage.Width;
					shape.ScaleObject();
				}

				list.Draw(graphics, srcImage.VerticalResolution);

				//close original
				srcImage.Dispose();

				graphics.Dispose();
				m_OK=true;
			}
			else
			{
				//create temporary
				tempImage = new Bitmap(width, height);
				((Bitmap)tempImage).SetResolution(srcImage.VerticalResolution, srcImage.VerticalResolution);

				//get graphics
				Graphics graphics = Graphics.FromImage(tempImage);

				//copy original				
				//graphics.DrawImage(srcImage,0,0);

				graphics.DrawImage(srcImage,0,0,width,height);

				GraphicsList list = OpenAnnFileForExport(sSrcAnnFile);

				int nCount = list.Count;

				for (int i = 0; i < nCount; i++)
				{
					DrawObject shape = list[i];
					shape.CurrentDrawAreaHeight = height;
					shape.CurrentDrawAreaWidth  = width;
					shape.ScaleObject();
				}

				list.Draw(graphics, srcImage.VerticalResolution);

				//close original
				srcImage.Dispose();

				graphics.Dispose();
				m_OK=true;
			}

            string sPath = Path.GetDirectoryName(sDestFile) + "\\";
            string sFile = sPath + Path.GetFileNameWithoutExtension(sDestFile);								


//			encoderParameters = new EncoderParameters(1);
//			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            switch (type)
            {
                case ExportType.Bmp:
                {
                    tempImage.Save(sFile + ".bmp", ImageFormat.Bmp);
                    break;
                }
                case ExportType.Jpg:
                {
                    tempImage.Save(sFile + ".jpg", ImageFormat.Jpeg);
                    break;
                }
                case ExportType.Png:
                {
                    tempImage.Save(sFile + ".png", ImageFormat.Png);
                    break;
                }
                case ExportType.Tiff:
                {
                    tempImage.Save(sFile + ".tiff", ImageFormat.Tiff);
                    break;
                }
                default:
                {
                    tempImage.Save(sFile + ".bmp", ImageFormat.Bmp);
                    break;
                }
            }

            tempImage.Dispose();
            tempImage = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
			return m_OK;
        }

		private GraphicsList OpenAnnFileForExportInternal(string sFilename)
		{
			GraphicsList list = null;

			try 
			{
				FileStream stream = new FileStream(sFilename, FileMode.Open, FileAccess.Read);
				try
				{
					// Deserialize object from text format
					IFormatter formatter = new BinaryFormatter();
					formatter.Binder = new Version1ToVersion2DeserializationBinder();


					// raise event to load document from file
					list = (GraphicsList)formatter.Deserialize(stream);

				}
				catch (SerializationException e) 
				{
					Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
					throw;
				}
				finally
				{
					stream.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return list;
		}

		private GraphicsList OpenAnnXmlFileForExportInternal(string sFilename)
		{
			GraphicsList list = null;
			System.Xml.XmlDocument doc = new XmlDocument();

			try
			{
				doc.Load(sFilename);
				XmlElement root = doc.DocumentElement;
				string version = root.GetAttribute("version");				
				switch(version)
				{
					case "1.0.0":
						list = ParseAnnotationXMLFileForExportVersion1_0_0(root, version);
						break;
					default:						
						Console.WriteLine("Version "+version+"is not supported.");
						break;
				}				
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;				
			}
			return list;
		}

        private GraphicsList OpenAnnFileForExport(string sFilename)
        {
            GraphicsList list = null;

			if (sFilename.ToUpper().EndsWith(".ANNX"))
			{										
				list = OpenAnnXmlFileForExportInternal(sFilename+"X");
			}
			else
			{
				if (System.IO.File.Exists(sFilename+"X"))
				{
					list = OpenAnnXmlFileForExportInternal(sFilename+"X");
				}
				else
				{
					if (System.IO.File.Exists(sFilename))
					{
						list = OpenAnnFileForExportInternal(sFilename);							
					}
				}
			}

            return list;
        }



        private Bitmap CaptureDrawArea()
        {
            Bitmap		bmp;	// capture bitmap
            Rectangle	ctlR;	// capture area rectangle in control coordinates
            Rectangle	scrR;	// capture area rectangle in screen coordinates
			
            // Get capture rectangle in control coordinates
            ctlR = m_drawArea.ClientRectangle;
            // Get capture rectangle in screen coordinates
            scrR = m_drawArea.RectangleToScreen(ctlR);
            
            // Get the Handle to the desktop window 
            IntPtr	desktopHWND = USER32.GetDesktopWindow();	
            // Get the desktop window device context
            IntPtr	wndHDC = USER32.GetDC(desktopHWND);

            // Create compatibile capture context and bitmap
            IntPtr captureHDC = GDI32.CreateCompatibleDC(wndHDC);	
            IntPtr captureBMP = GDI32.CreateCompatibleBitmap(wndHDC, scrR.Width, scrR.Height);

            //	select compatible bitmap in compatible context
            //	copy window context to compatible context
            //  select previous bitmap back into compatible context
            IntPtr	prvHDC = (IntPtr)GDI32.SelectObject(captureHDC, captureBMP); 
            GDI32.BitBlt(captureHDC, 0, 0, scrR.Width, scrR.Height, wndHDC, scrR.X, scrR.Y, GDI32.SRCCOPY); 
            GDI32.SelectObject(captureHDC, prvHDC);
        	
            //	create GDI+ bitmap for window
            bmp = System.Drawing.Image.FromHbitmap(captureBMP); 
        		
            //	release window and capture resources
            USER32.ReleaseDC(desktopHWND, wndHDC);  // release window context
            GDI32.DeleteDC(captureHDC);             // delete capture context
            GDI32.DeleteObject(captureBMP);         // delete capture bitmap

            return bmp;
        }
	}

    sealed class Version1ToVersion2DeserializationBinder : SerializationBinder 
    {
        public override Type BindToType(string assemblyName, string typeName) 
        {
            Type typeToDeserialize = null;

            assemblyName = Assembly.GetExecutingAssembly().FullName;

            // The following line of code returns the type.
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", 
                typeName, assemblyName));

            return typeToDeserialize;
        }
    }
}
