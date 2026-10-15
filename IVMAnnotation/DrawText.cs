using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace IVMAnnotation
{
    
	/// <summary>
	/// Text graphic object
	/// </summary>
	[ComVisible(false)]
	public class DrawText : IVMAnnotation.DrawRectangle
	{
        private string entryText  = "Text";
        private string m_sText    = "";
        private bool   m_bDrawing = false;		

		public DrawText()
		{
            SetRectangle(0, 0, 1,1);
            Initialize();
		}
        
        public DrawText(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Initialize();
        }

        /// <summary>
        /// Draw rectangle and Text
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g, float dpi)
        {
            Pen   pen   = new Pen(Color, PenWidth);
            Font  font  = new Font(CurrentFont.FontFamily, CurrentFont.Size*96.0F/dpi, CurrentFont.Style);
            Brush brush = new SolidBrush(Color);

            int height = Rectangle.Height;
            int width  = Rectangle.Width;			

            if (height < 20)
            {
                height = 20;
            }
            if (width < 50)
            {
                width = 50;
            }
            int x = Rectangle.X;
            int y = Rectangle.Y;
			
			if (m_sText.Length >0)
			{
				StringFormat format = new StringFormat();
				format.SetMeasurableCharacterRanges(new CharacterRange[]{new CharacterRange(0, m_sText.Length)});
				Region[] r = g.MeasureCharacterRanges(m_sText, font, new System.Drawing.Rectangle(0, 0, 10000, 10000), format);
				if (r!=null && r.Length>0)
				{
					RectangleF rect = r[0].GetBounds(g);
					int tmpWidth=(int)(rect.Width*1.2+1.0);
					int tmpHeight=(int)(rect.Height+1.0);					
					if (tmpHeight>height)
					{
						height = tmpHeight;
						if (tmpWidth>width)
						{
							width=tmpWidth;
							if (width > 600)
							{
								width=600;								
							}							
						}
					}
					height = tmpHeight*((tmpWidth/width)+1);					
				}
			}

            SetRectangle(x, y, width, height);

            if (m_bDrawing)
            {
                g.DrawRectangle(pen, x, y, width, height);
            }
			
            g.DrawString(m_sText, font, brush, DrawRectangle.GetNormalizedRectangle(Rectangle));
            
            pen.Dispose();
            brush.Dispose();
            font.Dispose();
        }

        public override void SaveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryText, orderNumber),
                m_sText);

            base.SaveToStream (info, orderNumber);  // ??
        }
		public override void WriteXmlInfo(TextWriter t, bool saveFontToo, ref int depth, string version)
		{
			string tabs = new string('\t', depth);
			t.WriteLine(tabs+"\t<Text value='"+m_sText+"'>");
			depth++;
			base.WriteXmlInfo(t, true, ref depth, version);			
			t.WriteLine(tabs+"\t</Text>");
		}
		private bool Parse_Version1_0_0(XmlElement element, string version, int originalDrawAreaWidth, int originalDrawAreaHeight)
		{
			// Changed false to true.
			bool bOK = true;
			m_sText = element.GetAttribute("value");
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

        public override void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            m_bDrawing = false;

            m_sText = (string)info.GetValue(
                String.Format(CultureInfo.InvariantCulture,
                "{0}{1}",
                entryText, orderNumber),
                typeof(string));

            base.LoadFromStream (info, orderNumber);
        }

        public override void ScaleObject()
        {
            base.ScaleObject();
            
            int height = CurrentDrawAreaHeight;

            int size = (int)((int)CurrentFont.Size * Decimal.Divide(height, OriginalDrawAreaHeight));
			if (size<1)
			{
				size=1;
			}
            CurrentFont = new Font(CurrentFont.Name, size, CurrentFont.Style);
            
        }

        public override void OnDoubleClick(DrawArea area, EventArgs e)
        {
            m_bDrawing = false;

            // show text input popup
            string sMsg = "";
            string sOK = "";
            string sCancel = "";
            area.GetTextInputDlgStrings(ref sMsg, ref sOK, ref sCancel);						

            m_sText = GetTextInput(sMsg, sOK, sCancel);
        }

        private string GetTextInput(string sMsg, string sOK, string sCancel)
        {
            string sResult = m_sText;

            TextInput textForm = new TextInput(sMsg, sOK, sCancel);
            textForm.TextBoxInput = m_sText;

            DialogResult result = textForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                sResult = textForm.TextBoxInput;
            }

            return sResult;
        }
    }
}
