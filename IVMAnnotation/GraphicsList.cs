using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.Globalization;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;

namespace IVMAnnotation
{
	/// <summary>
	/// List of graphic objects
	/// </summary>
    [Serializable]
    [ComVisible(false)]
    public class GraphicsList : ISerializable
    {
        private ArrayList graphicsList;

        private const string entryCount = "Count";
        private const string entryType = "Type";

        public GraphicsList()
        {
            graphicsList = new ArrayList();
        }

        protected GraphicsList(SerializationInfo info, StreamingContext context)
        {
            graphicsList = new ArrayList();

            int n = info.GetInt32(entryCount);
            string typeName;
            object drawObject;

            for ( int i = 0; i < n; i++ )
            {
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                        "{0}{1}",
                    entryType, i));

                drawObject = Assembly.GetExecutingAssembly().CreateInstance(
                    typeName);

//                shape.CurrentDrawAreaHeight = m_drawArea.Height;
//                shape.CurrentDrawAreaWidth  = m_drawArea.Width;

                ((DrawObject)drawObject).LoadFromStream(info, i);
        
                graphicsList.Add(drawObject);
            }

        }

        /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			int cnt=0;
			foreach ( DrawObject o in graphicsList )
			{
				if (o.SaveToAnnToo())
				{
					cnt++;
				}
			}

            info.AddValue(entryCount, cnt);

            int i = 0;

            foreach ( DrawObject o in graphicsList )
            {
				if (o.SaveToAnnToo())
				{
					info.AddValue(
						String.Format(CultureInfo.InvariantCulture,
						"{0}{1}",
						entryType, i),
						o.GetType().FullName);				
					o.SaveToStream(info, i);
					i++;
				}                
            }
        }

		public void WriteXmlInfo(TextWriter t, string version)
		{
			if (graphicsList.Count>0)
			{
				t.WriteLine("<OriginalDrawAreaWidth value='"+((DrawObject)graphicsList[0]).OriginalDrawAreaWidth+"'/>");
				t.WriteLine("<OriginalDrawAreaHeight value='"+((DrawObject)graphicsList[0]).OriginalDrawAreaHeight+"'/>");			
				foreach ( DrawObject o in graphicsList )
				{
					int depth=0;	
					try
					{
						o.WriteXmlInfo(t, false, ref depth, version);
					} 
					catch(Exception ex)
					{
						t.WriteLine("<Exception message='"+ex.Message+"'");
						t.WriteLine(ex.StackTrace);
						t.WriteLine("</Exception>");
					}
				}
			}
		}

        public void Draw(Graphics g, float dpi)
        {
            int n = graphicsList.Count;
            DrawObject o;

            // Enumerate list in reverse order
            // to get first object on the top
            for (int i = n - 1; i >= 0; i-- )
            {
                o = (DrawObject)graphicsList[i];

                if (o.Show)
                {
                    o.Draw(g, dpi);

                    if ( o.Selected == true )
                    {
                        o.DrawTracker(g, dpi);
                    }
                }
            }
        }

        /// <summary>
        /// Clear all objects in the list
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool Clear()
        {
            bool result = (graphicsList.Count > 0);
			for (int i=0;i<graphicsList.Count;i++)
			{
				((DrawObject)graphicsList[i]).RemoveObject();
			}
            graphicsList.Clear();
            return result;
        }

        /// <summary>
        /// Count and this [nIndex] allow to read all graphics objects
        /// from GraphicsList in the loop.
        /// </summary>
        public int Count
        {
            get
            {
                return graphicsList.Count;
            }
        }

        public DrawObject this [int index]
        {
            get
            {
                if ( index < 0  ||  index >= graphicsList.Count )
                    return null;

                return ((DrawObject)graphicsList[index]);
            }
        }

        /// <summary>
        /// SelectedCount and GetSelectedObject allow to read
        /// selected objects in the loop
        /// </summary>
        public int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject o in graphicsList)
                {
                    if ( o.Selected )
                        n++;
                }

                return n;
            }
        }

        public DrawObject GetSelectedObject(int index)
        {
            int n = -1;

            foreach (DrawObject o in graphicsList)
            {
                if ( o.Selected )
                {
                    n++;

                    if ( n == index )
                        return o;
                }
            }

            return null;
        }

        public void Add(DrawObject obj)
        {
            // insert to the top of z-order
            graphicsList.Insert(0, obj);
        }

        public bool SelectInRectangle(Rectangle rectangle)
        {
			bool ret = false;
            UnselectAll();

            foreach (DrawObject o in graphicsList)
            {
				if ( o.IntersectsWith(rectangle) )
				{
					o.Selected = true;
					ret = true;
				}
            }
			return ret;
        }

        public void UnselectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = false;
            }
        }

        public void SelectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = true;
            }
        }

        /// <summary>
        /// Delete selected items
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool DeleteSelection()
        {
            bool result = false;

            int n = graphicsList.Count;

            for ( int i = n-1; i >= 0; i-- )
            {
                if ( ((DrawObject)graphicsList[i]).Selected )
                {
					((DrawObject)graphicsList[i]).RemoveObject();
                    graphicsList.RemoveAt(i);
                    result = true;
                }
            }

            return result;
        }
	}
}
