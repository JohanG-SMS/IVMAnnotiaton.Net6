using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
	/// <summary>
	/// Base class for all tools which create new graphic object
	/// </summary>
	[ComVisible(false)]
    public abstract class ToolObject : IVMAnnotation.Tool
	{
        private Cursor cursor;

        /// <summary>
        /// Tool cursor.
        /// </summary>
        protected Cursor Cursor
        {
            get
            {
                return cursor;
            }
            set
            {
                cursor = value;
            }
        }

        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.GraphicsList[0].Normalize();
            drawArea.ActiveTool = AnnotationType.SelectionPointer;

            drawArea.Capture = false;
            drawArea.Refresh();
        }

        /// <summary>
        /// Add new object to draw area.
        /// Function is called when user left-clicks draw area,
        /// and one of ToolObject-derived tools is active.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="o"></param>
        protected void AddNewObject(DrawArea drawArea, DrawObject o)
        {
            drawArea.GraphicsList.UnselectAll();

            o.Selected = false;
            o.OriginalDrawAreaHeight = drawArea.ReferenceHeight;
            o.OriginalDrawAreaWidth  = drawArea.ReferenceWidth;
            o.CurrentDrawAreaHeight  = drawArea.ReferenceHeight;
            o.CurrentDrawAreaWidth   = drawArea.ReferenceWidth;
            drawArea.GraphicsList.Add(o);

            drawArea.Dirty   = true;
            drawArea.Capture = true;
            drawArea.Refresh();
        }
	}
}
