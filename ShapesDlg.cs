using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace IVMAnnotation
{
	/// <summary>
	/// Summary description for ShapesDlg.
	/// </summary>
	public class ShapesDlg : System.Windows.Forms.Form
	{		
		private System.Collections.ArrayList shapes;
		private int selectedShape=-1;		
		private System.Windows.Forms.ImageList uxImageList;
		private System.Windows.Forms.ToolStrip uxToolBar;			
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox uxFreeText;
		private System.Windows.Forms.Button uxClose;
		private ToolStrip shapesPopupToolBar=null;
		private bool shapeSelected=false;
		public ShapesDlg()
		{
			shapes = new ArrayList();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void SetLanguageStrings(string sClose)
		{			
			this.uxClose.Text = sClose;
		}

		private string DeEscapeText(string text)
		{
			string ret="";						
			ret = text.Replace("&lt;","<");			
			ret = ret.Replace("&#64;","@");
			ret = ret.Replace("&gt;",">");
			ret = ret.Replace("&quot;","\"");
			ret = ret.Replace("&#39;","'");					
			ret = ret.Replace("&#40;","(");
			ret = ret.Replace("&#41;",")");
			ret = ret.Replace("&#42;","*");					
			ret = ret.Replace("&#43;","+");			
			ret = ret.Replace("&#123;","{");			
			ret = ret.Replace("&#124;","}");
			ret = ret.Replace("&#91;","[");			
			ret = ret.Replace("&#93;","]");						
			ret = ret.Replace("&#63;","?");	
			ret = ret.Replace("&#33;","!");
			ret = ret.Replace("&#35;","#");			
			ret = ret.Replace("&#36;","$");			
			ret = ret.Replace("&#37;","%");						
			ret = ret.Replace("&amp;","&");
			ret = ret.Trim();
			return ret;
		}

		private string EscapeText(string text)
		{
			string ret="";
			ret = text.Replace("&","&amp;");			
			ret = ret.Replace("#","&#35;");			
			ret = ret.Replace("$","&#36;");			
			ret = ret.Replace("%","&#37;");						
			ret = ret.Replace("@","&#64;");
			ret = ret.Replace("*","");
			ret = ret.Replace("<","&lt;");
			ret = ret.Replace(">","&gt;");
			ret = ret.Replace("\"","&quot;");
			ret = ret.Replace("€","");
			ret = ret.Replace("(","&#40;");
			ret = ret.Replace(")","&#41;");
			ret = ret.Replace("*","&#42;");			
			ret = ret.Replace("+","&#43;");			
			ret = ret.Replace("{","&#123;");			
			ret = ret.Replace("}","&#124;");			
			ret = ret.Replace("[","&#91;");			
			ret = ret.Replace("]","&#93;");
			ret = ret.Replace("'","&#39;");						
			ret = ret.Replace("~","");	
			ret = ret.Replace("?","&#63;");	
			ret = ret.Replace("!","&#33;");	
			ret = ret.Trim();
			return ret;
		}

		public String FreeText
		{
			set
			{
				this.uxFreeText.Text = DeEscapeText(value);				
			}
			get
			{
				return EscapeText(this.uxFreeText.Text);
			}
		}
			
		public bool ShapeSelected
		{
			get
			{
				return shapeSelected;
			}
			set
			{
				shapeSelected=value;
			}
		}

		public void setSelectedShape(int shapeIndex)
		{
			selectedShape = shapeIndex;
		}


		public Shape AddShape(string shapeName)
		{
			Shape ret = new Shape(shapeName);
			shapes.Add(ret);
			return ret;
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.uxImageList = new System.Windows.Forms.ImageList(this.components);
			this.uxToolBar = new System.Windows.Forms.ToolStrip();
			this.uxClose = new System.Windows.Forms.Button();
			this.uxFreeText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// uxImageList
			// 
			this.uxImageList.ImageSize = new System.Drawing.Size(40, 40);
			this.uxImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// uxToolBar
			// 
			this.uxToolBar.AutoSize = false;
			this.uxToolBar.ImageScalingSize = new System.Drawing.Size(40, 40);
			this.uxToolBar.Location = new System.Drawing.Point(0, 0);
			this.uxToolBar.Name = "uxToolBar";
			this.uxToolBar.ShowItemToolTips = true;
			this.uxToolBar.Size = new System.Drawing.Size(328, 66);
			this.uxToolBar.TabIndex = 2;
			this.uxToolBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.uxToolBar_ButtonClick);
			// 
			// uxClose
			// 
			this.uxClose.Location = new System.Drawing.Point(0, 248);
			this.uxClose.Name = "uxClose";
			this.uxClose.TabIndex = 3;
			this.uxClose.Text = "Close";
			this.uxClose.Click += new System.EventHandler(this.uxCancel_Click);
			// 
			// uxFreeText
			// 
			this.uxFreeText.Location = new System.Drawing.Point(0, 80);
			this.uxFreeText.Multiline = true;
			this.uxFreeText.Name = "uxFreeText";
			this.uxFreeText.Size = new System.Drawing.Size(328, 160);
			this.uxFreeText.TabIndex = 4;
			this.uxFreeText.Text = "";
			// 
			// ShapesDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(328, 278);
			this.ControlBox = false;
			this.Controls.Add(this.uxFreeText);
			this.Controls.Add(this.uxClose);
			this.Controls.Add(this.uxToolBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.Name = "ShapesDlg";
			this.Text = "Select shape";
			this.Load += new System.EventHandler(this.ShapesDlg_Load);
			this.ResumeLayout(false);

		}
		#endregion
		
		public void InitShapesData()
		{
			this.uxToolBar.Items.Clear();			
			System.Drawing.Pen pen = new Pen(System.Drawing.Color.DarkBlue, 3);
			for (int i=0;i<shapes.Count;i++)
			{
				Shape shape = (Shape)shapes[i];
				System.Windows.Forms.ToolStripButton b = new ToolStripButton(shape.ShapeName);
				b.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
				b.TextImageRelation = TextImageRelation.ImageAboveText;

				System.Drawing.Image image = new System.Drawing.Bitmap(60,60);
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);				

				for (int k=0;k<shape.ShapeData.Count;k++)
				{
					System.Drawing.Point []pointsOrig = (System.Drawing.Point [])shape.ShapeData[k];
					System.Drawing.Point []pointsDest = new Point[pointsOrig.Length];
					for (int p=0;p<pointsOrig.Length;p++)
					{
						pointsDest[p].X = pointsOrig[p].X*32/1000+10;
						pointsDest[p].Y = pointsOrig[p].Y*32/1000+10;
					}
					g.DrawPolygon(pen,pointsDest);						
				}								
				b.Image = image;
				this.uxToolBar.Items.Add(b);
			}			
		}

		private void ShapesDlg_Load(object sender, System.EventArgs e)
		{
			shapeSelected=false;
			int totalButtonsWidth = 67*this.uxToolBar.Items.Count;
			if (totalButtonsWidth>400)
			{
				this.uxFreeText.Width = totalButtonsWidth;
				this.Width = this.uxFreeText.Width+15;
				this.uxFreeText.Left = 5;
			}
			else
			{
				this.uxFreeText.Width = 400;
				this.Width = this.uxFreeText.Width+15;
				this.uxFreeText.Left = 5;
			}			
		}

		public ToolStrip ShapesToolBar
		{
			get
			{					
				if (shapesPopupToolBar == null)
				{
					shapesPopupToolBar = new ToolStrip();
					shapesPopupToolBar.AutoSize=true;
					shapesPopupToolBar.ImageScalingSize = new Size(40,40);
					shapesPopupToolBar.Dock = System.Windows.Forms.DockStyle.None;
					shapesPopupToolBar.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
					for (int i=0;i<uxToolBar.Items.Count;i++)
					{
						ToolStripButton srcButton = uxToolBar.Items[i] as ToolStripButton;
						if (srcButton != null)
						{
							ToolStripButton cloneButton = new ToolStripButton(srcButton.Text, srcButton.Image);
							cloneButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
							cloneButton.TextImageRelation = TextImageRelation.ImageAboveText;
							shapesPopupToolBar.Items.Add(cloneButton);
						}
					}
					shapesPopupToolBar.Height = 600;
					shapesPopupToolBar.ItemClicked += new ToolStripItemClickedEventHandler(shapesPopupToolBar_ButtonClick);
				}
				return shapesPopupToolBar;
			}
		}

		private void uxCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Hide();
		}

		private void uxToolBar_ButtonClick(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
		{			
			for (int i=0;i<uxToolBar.Items.Count;i++)
			{						
				if(e.ClickedItem.Equals(uxToolBar.Items[i]))
				{
					shapeSelected=true;
					selectedShape = i;
				}
			}			
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Hide();
		}

		private void shapesPopupToolBar_ButtonClick(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
		{			
			for (int i=0;i<shapesPopupToolBar.Items.Count;i++)
			{						
				if(e.ClickedItem.Equals(shapesPopupToolBar.Items[i]))
				{
					selectedShape = i;
				}
			}						
		}

		private void uxOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Hide();		
		}

		public Shape SelectedShape
		{
			get
			{
				if (selectedShape==-1)
				{
					return null;
				}
				else
				{
					return (Shape)shapes[selectedShape];
				}
			}
		}
	}
	
	[Serializable]
	public class Shape
	{
		private System.Collections.ArrayList shapeData=new ArrayList();				
		private string shapeName = "newShape";		

		public Shape(string shapeName)
		{
			this.shapeName = shapeName;			
		}
		public ArrayList StartPolygon()
		{
			return new ArrayList();
		}

		public void AddPolygonPoint(ArrayList polygon, int x, int y)
		{
			polygon.Add(new Point(x,y));
		}

		public void EndPolygon(ArrayList polygon)
		{
			System.Drawing.Point []points = new System.Drawing.Point[polygon.Count];
			for(int i = 0;i<polygon.Count;i++)
			{
				points[i] = (System.Drawing.Point)polygon[i];
			}												
			shapeData.Add(points);					
		}

		public System.Collections.ArrayList ShapeData
		{
			get
			{
				return shapeData;
			}
			set
			{
				shapeData = value;
			}
		}
		public string ShapeName
		{
			get
			{
				return shapeName;
			}
			set
			{
				shapeName = value;
			}
		}
	}

}
