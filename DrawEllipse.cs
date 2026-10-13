using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace IVMAnnotation
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	[ComVisible(false)]
    public class DrawEllipse : IVMAnnotation.DrawRectangle
	{
		public DrawEllipse()
		{
            SetRectangle(0, 0, 1, 1);
            Initialize();
		}

        public DrawEllipse(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);						

            Initialize();
        }

        public override void Draw(Graphics g, float dpi)
        {
            Pen pen = new Pen(Color, PenWidth);

            g.DrawEllipse(pen, DrawRectangle.GetNormalizedRectangle(Rectangle));						

            pen.Dispose();
        }

		public override void WriteXmlInfo(TextWriter t, bool saveFontToo, ref int depth, string version)
		{
			string tabs = new string('\t', depth);
			t.WriteLine(tabs+"\t<Ellipse>");
			depth++;
			base.WriteXmlInfo(t, saveFontToo, ref depth, version);
			t.WriteLine(tabs+"\t</Ellipse>");
		}	
		private bool Parse_Version1_0_0(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			// Changed false to true.
			bool bOK = true;			
			foreach (XmlElement sub in element)
			{
				switch(sub.Name)
				{
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
	

	}
}
