using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
    public enum StringItems
    {
        TextInputDlgMessage = 0,
        TextInputDlgOK      = 1,
        TextInputDlgCancel  = 2
    }

    public enum AnnotationType
    {
        SelectionPointer    = 0,
        Oval                = 1,
        Rectangle           = 2,
        Line                = 3,
        Arrow               = 4,
        Polygon             = 5,
        Text                = 6,
        Zoom                = 7,
		ArrayOfPolygons		= 8,
        NumberOfDrawTools 
    }

	public enum PolygonType
	{
		None                = -1,
		Cross               = 0,
		Triangle            = 1,
		Square              = 2,
		Marker              = 3
	}

    public enum FontStyles
    {
        Normal    = 0, 
        Bold      = 1, 
        Italic    = 2, 
        Underline = 4,
        Strikeout = 8 
    }

    public enum ZoomType
    {
        ZoomIn  = 0,
        ZoomOut = 1
    }

    public enum ExportType
    {
        Bmp     = 0,
        Jpg     = 1,
        Png     = 2,
        Tiff    = 3,
		AutoDetect = 4
    }

	public enum MarkerOnly
	{
		ShowAll,
		DisableOther,
		InvisibleOther
	}
}
