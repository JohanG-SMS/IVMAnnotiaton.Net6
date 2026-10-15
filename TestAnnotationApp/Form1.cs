using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using IVMAnnotation;

namespace TestAnnotationApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private IVMAnnotation.DrawArea drawArea;

        private System.Windows.Forms.Button RemoveAll;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button butSetColor;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butOpen;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private IVMAnnotation.DrawArea drawArea1;
        private IVMAnnotation.DrawArea drawArea2;
        private IVMAnnotation.DrawArea drawArea3;
        private System.Windows.Forms.Button butSaveDA2;
        private System.Windows.Forms.Button butOpenDA2;
        private System.Windows.Forms.Button butSaveDA4;
        private System.Windows.Forms.Button butOpenDA4;
        private System.Windows.Forms.Button butSaveDA3;
        private System.Windows.Forms.Button butOpenDA3;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button butExportFile;   // file name from command line


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

            drawArea.Initialize();
            drawArea1.Initialize();
            drawArea2.Initialize();
            drawArea3.Initialize();

            try
            {
                string sFile = "2004-12-13080149.BMP";
                if (File.Exists(sFile))
                {
                    drawArea.SetBitmap(sFile);
                    drawArea1.SetBitmap(sFile);
                    drawArea2.SetBitmap(sFile);
                    drawArea3.SetBitmap(sFile);
                }
            }
            catch
            {
            }

            button10.BackColor = Color.Black;
            button12.BackColor = Color.White;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.drawArea = new IVMAnnotation.DrawArea();
            this.RemoveAll = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.butSetColor = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.butOpen = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.drawArea1 = new IVMAnnotation.DrawArea();
            this.drawArea2 = new IVMAnnotation.DrawArea();
            this.butSaveDA2 = new System.Windows.Forms.Button();
            this.butOpenDA2 = new System.Windows.Forms.Button();
            this.butSaveDA4 = new System.Windows.Forms.Button();
            this.butOpenDA4 = new System.Windows.Forms.Button();
            this.drawArea3 = new IVMAnnotation.DrawArea();
            this.butSaveDA3 = new System.Windows.Forms.Button();
            this.butOpenDA3 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.butExportFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(7, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 27);
            this.button1.TabIndex = 3;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(7, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 28);
            this.button2.TabIndex = 4;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(7, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 28);
            this.button3.TabIndex = 7;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(7, 125);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(26, 28);
            this.button4.TabIndex = 8;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(7, 153);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(26, 27);
            this.button5.TabIndex = 9;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(7, 97);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(26, 28);
            this.button6.TabIndex = 10;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(7, 180);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(26, 28);
            this.button7.TabIndex = 11;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // drawArea
            // 
            this.drawArea.ActiveTool = IVMAnnotation.AnnotationType.SelectionPointer;
            this.drawArea.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("drawArea.BackgroundImage")));
            this.drawArea.Dirty = false;
            this.drawArea.DrawNetRectangle = false;
            this.drawArea.ForeColor = System.Drawing.SystemColors.Window;
            this.drawArea.GraphicsList = null;
            this.drawArea.Location = new System.Drawing.Point(127, 0);
            this.drawArea.Name = "drawArea";
            this.drawArea.NetRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.drawArea.ShowAnnotation = true;
            this.drawArea.Size = new System.Drawing.Size(653, 534);
            this.drawArea.TabIndex = 12;
            this.drawArea.Zoomed = false;
            // 
            // RemoveAll
            // 
            this.RemoveAll.Location = new System.Drawing.Point(40, 167);
            this.RemoveAll.Name = "RemoveAll";
            this.RemoveAll.Size = new System.Drawing.Size(80, 23);
            this.RemoveAll.TabIndex = 13;
            this.RemoveAll.Text = "RemoveAll";
            this.RemoveAll.Click += new System.EventHandler(this.RemoveAll_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(40, 138);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(80, 22);
            this.Remove.TabIndex = 14;
            this.Remove.Text = "Remove";
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // butSetColor
            // 
            this.butSetColor.Location = new System.Drawing.Point(26, 83);
            this.butSetColor.Name = "butSetColor";
            this.butSetColor.Size = new System.Drawing.Size(41, 23);
            this.butSetColor.TabIndex = 15;
            this.butSetColor.Text = "Color";
            this.butSetColor.Click += new System.EventHandler(this.butSetColor_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(7, 35);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(100, 45);
            this.trackBar1.TabIndex = 16;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Line thickness:";
            // 
            // butOpen
            // 
            this.butOpen.Image = ((System.Drawing.Image)(resources.GetObject("butOpen.Image")));
            this.butOpen.Location = new System.Drawing.Point(40, 14);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(27, 28);
            this.butOpen.TabIndex = 18;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // butSave
            // 
            this.butSave.Image = ((System.Drawing.Image)(resources.GetObject("butSave.Image")));
            this.butSave.Location = new System.Drawing.Point(67, 14);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(26, 28);
            this.butSave.TabIndex = 19;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 21);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Show";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(13, 49);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 20);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.Text = "Hide";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(40, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(80, 76);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show/Hide";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(40, 76);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 24);
            this.button8.TabIndex = 23;
            this.button8.Tag = "";
            this.button8.Text = "Export";
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.Location = new System.Drawing.Point(40, 292);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(27, 28);
            this.button9.TabIndex = 24;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.butSetColor);
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.Controls.Add(this.button15);
            this.groupBox2.Location = new System.Drawing.Point(8, 336);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(113, 111);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Defaults";
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Location = new System.Drawing.Point(5, 85);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(19, 19);
            this.button10.TabIndex = 26;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(67, 83);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(41, 23);
            this.button15.TabIndex = 30;
            this.button15.Text = "Font";
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trackBar2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.button11);
            this.groupBox3.Controls.Add(this.button12);
            this.groupBox3.Controls.Add(this.button16);
            this.groupBox3.Location = new System.Drawing.Point(8, 456);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(113, 111);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Set properties:";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(7, 35);
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(100, 45);
            this.trackBar2.TabIndex = 16;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Line thickness:";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(26, 83);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(41, 23);
            this.button11.TabIndex = 15;
            this.button11.Text = "Color";
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.Window;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Location = new System.Drawing.Point(5, 85);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(20, 20);
            this.button12.TabIndex = 26;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(67, 83);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(41, 23);
            this.button16.TabIndex = 31;
            this.button16.Text = "Font";
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(40, 46);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(80, 23);
            this.button13.TabIndex = 28;
            this.button13.Text = "Import";
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(93, 14);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(27, 28);
            this.button14.TabIndex = 29;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button17
            // 
            this.button17.Image = ((System.Drawing.Image)(resources.GetObject("button17.Image")));
            this.button17.Location = new System.Drawing.Point(67, 292);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(26, 28);
            this.button17.TabIndex = 30;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Image = ((System.Drawing.Image)(resources.GetObject("button18.Image")));
            this.button18.Location = new System.Drawing.Point(93, 292);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(27, 28);
            this.button18.TabIndex = 31;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(7, 208);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(26, 28);
            this.button19.TabIndex = 32;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(7, 236);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(26, 27);
            this.button20.TabIndex = 33;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Enabled = false;
            this.button21.Location = new System.Drawing.Point(7, 263);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(26, 28);
            this.button21.TabIndex = 34;
            // 
            // drawArea1
            // 
            this.drawArea1.ActiveTool = IVMAnnotation.AnnotationType.SelectionPointer;
            this.drawArea1.Dirty = false;
            this.drawArea1.DrawNetRectangle = false;
            this.drawArea1.ForeColor = System.Drawing.SystemColors.Window;
            this.drawArea1.GraphicsList = null;
            this.drawArea1.Location = new System.Drawing.Point(792, 16);
            this.drawArea1.Name = "drawArea1";
            this.drawArea1.NetRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.drawArea1.ShowAnnotation = true;
            this.drawArea1.Size = new System.Drawing.Size(216, 184);
            this.drawArea1.TabIndex = 35;
            this.drawArea1.Zoomed = false;
            // 
            // drawArea2
            // 
            this.drawArea2.ActiveTool = IVMAnnotation.AnnotationType.SelectionPointer;
            this.drawArea2.Dirty = false;
            this.drawArea2.DrawNetRectangle = false;
            this.drawArea2.ForeColor = System.Drawing.SystemColors.Window;
            this.drawArea2.GraphicsList = null;
            this.drawArea2.Location = new System.Drawing.Point(792, 256);
            this.drawArea2.Name = "drawArea2";
            this.drawArea2.NetRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.drawArea2.ShowAnnotation = true;
            this.drawArea2.Size = new System.Drawing.Size(216, 432);
            this.drawArea2.TabIndex = 36;
            this.drawArea2.Zoomed = false;
            // 
            // butSaveDA2
            // 
            this.butSaveDA2.Image = ((System.Drawing.Image)(resources.GetObject("butSaveDA2.Image")));
            this.butSaveDA2.Location = new System.Drawing.Point(984, 208);
            this.butSaveDA2.Name = "butSaveDA2";
            this.butSaveDA2.Size = new System.Drawing.Size(26, 28);
            this.butSaveDA2.TabIndex = 38;
            this.butSaveDA2.Click += new System.EventHandler(this.butSaveDA2_Click);
            // 
            // butOpenDA2
            // 
            this.butOpenDA2.Image = ((System.Drawing.Image)(resources.GetObject("butOpenDA2.Image")));
            this.butOpenDA2.Location = new System.Drawing.Point(957, 208);
            this.butOpenDA2.Name = "butOpenDA2";
            this.butOpenDA2.Size = new System.Drawing.Size(27, 28);
            this.butOpenDA2.TabIndex = 37;
            this.butOpenDA2.Click += new System.EventHandler(this.butOpenDA2_Click);
            // 
            // butSaveDA4
            // 
            this.butSaveDA4.Image = ((System.Drawing.Image)(resources.GetObject("butSaveDA4.Image")));
            this.butSaveDA4.Location = new System.Drawing.Point(812, 696);
            this.butSaveDA4.Name = "butSaveDA4";
            this.butSaveDA4.Size = new System.Drawing.Size(26, 28);
            this.butSaveDA4.TabIndex = 40;
            this.butSaveDA4.Click += new System.EventHandler(this.butSaveDA4_Click);
            // 
            // butOpenDA4
            // 
            this.butOpenDA4.Image = ((System.Drawing.Image)(resources.GetObject("butOpenDA4.Image")));
            this.butOpenDA4.Location = new System.Drawing.Point(784, 696);
            this.butOpenDA4.Name = "butOpenDA4";
            this.butOpenDA4.Size = new System.Drawing.Size(27, 28);
            this.butOpenDA4.TabIndex = 39;
            this.butOpenDA4.Click += new System.EventHandler(this.butOpenDA4_Click);
            // 
            // drawArea3
            // 
            this.drawArea3.ActiveTool = IVMAnnotation.AnnotationType.SelectionPointer;
            this.drawArea3.Dirty = false;
            this.drawArea3.DrawNetRectangle = false;
            this.drawArea3.ForeColor = System.Drawing.SystemColors.Window;
            this.drawArea3.GraphicsList = null;
            this.drawArea3.Location = new System.Drawing.Point(128, 544);
            this.drawArea3.Name = "drawArea3";
            this.drawArea3.NetRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.drawArea3.ShowAnnotation = true;
            this.drawArea3.Size = new System.Drawing.Size(656, 176);
            this.drawArea3.TabIndex = 41;
            this.drawArea3.Zoomed = false;
            // 
            // butSaveDA3
            // 
            this.butSaveDA3.Image = ((System.Drawing.Image)(resources.GetObject("butSaveDA3.Image")));
            this.butSaveDA3.Location = new System.Drawing.Point(984, 696);
            this.butSaveDA3.Name = "butSaveDA3";
            this.butSaveDA3.Size = new System.Drawing.Size(26, 28);
            this.butSaveDA3.TabIndex = 43;
            this.butSaveDA3.Click += new System.EventHandler(this.butSaveDA3_Click);
            // 
            // butOpenDA3
            // 
            this.butOpenDA3.Image = ((System.Drawing.Image)(resources.GetObject("butOpenDA3.Image")));
            this.butOpenDA3.Location = new System.Drawing.Point(957, 696);
            this.butOpenDA3.Name = "butOpenDA3";
            this.butOpenDA3.Size = new System.Drawing.Size(27, 28);
            this.butOpenDA3.TabIndex = 42;
            this.butOpenDA3.Click += new System.EventHandler(this.butOpenDA3_Click);
            // 
            // button22
            // 
            this.button22.Enabled = false;
            this.button22.Location = new System.Drawing.Point(7, 292);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(26, 28);
            this.button22.TabIndex = 44;
            // 
            // butExportFile
            // 
            this.butExportFile.Location = new System.Drawing.Point(40, 107);
            this.butExportFile.Name = "butExportFile";
            this.butExportFile.Size = new System.Drawing.Size(80, 24);
            this.butExportFile.TabIndex = 45;
            this.butExportFile.Tag = "";
            this.butExportFile.Text = "Export file";
            this.butExportFile.Click += new System.EventHandler(this.butExportFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.butExportFile);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.butSaveDA3);
            this.Controls.Add(this.butOpenDA3);
            this.Controls.Add(this.drawArea3);
            this.Controls.Add(this.butSaveDA4);
            this.Controls.Add(this.butOpenDA4);
            this.Controls.Add(this.butSaveDA2);
            this.Controls.Add(this.butOpenDA2);
            this.Controls.Add(this.drawArea2);
            this.Controls.Add(this.drawArea1);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butOpen);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.RemoveAll);
            this.Controls.Add(this.drawArea);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Image & Video Manager Annotations Demo";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void button1_Click(object sender, System.EventArgs e)
        {
            drawArea.SetDrawingMode(AnnotationType.Rectangle);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            drawArea.SetSelectionMode();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            drawArea.SetDrawingMode(AnnotationType.Oval);
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            drawArea.SetDrawingMode(AnnotationType.Line);
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            drawArea.SetDrawingMode(AnnotationType.Arrow);
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            drawArea.SetDrawingMode(AnnotationType.Polygon);
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            drawArea.SetDrawingMode(AnnotationType.Text);
        }

        private void Remove_Click(object sender, System.EventArgs e)
        {
            drawArea.RemoveSelected();        
        }

        private void RemoveAll_Click(object sender, System.EventArgs e)
        {
            drawArea.RemoveAll();
        }

        private void butSetColor_Click(object sender, System.EventArgs e)
        {
            Color selectedColor = button10.BackColor;

            // Sets the initial color.
            colorDialog1.Color = selectedColor;
    
            // Update the text box color if the user clicks OK 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedColor =  colorDialog1.Color;

                drawArea.SetDefaultDrawingColor(selectedColor.R, selectedColor.G, selectedColor.B);
                button10.BackColor = selectedColor;
            }
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            int nLine = trackBar1.Value;
            drawArea.SetDefaultLineThickness(nLine);
        }

        private void butOpen_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = openFileDialog1.FileName;

                drawArea.OpenFile(sFile);
                drawArea1.OpenFile(sFile);
                drawArea2.OpenFile(sFile);
                drawArea3.OpenFile(sFile);
            }
        }

        private void butSave_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "all files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1 ;
            saveFileDialog1.RestoreDirectory = true ;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = saveFileDialog1.FileName;

                drawArea.SaveFile(sFile);        
            }
        }

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                drawArea.ShowAnnotations(true);
            }
            else
            {
                drawArea.ShowAnnotations(false);
            }
        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "BMP files (*.bmp)|*.bmp|Jpg files (*.Jpg)|*.jpg|Png files (*.png)|*.png|Tiff files (*.tiff)|*.tiff"  ;
            saveFileDialog1.FilterIndex = 2 ;
            saveFileDialog1.RestoreDirectory = true ;

            saveFileDialog1.AddExtension = true;

            string sFile = "temp.jpg";
            ExportType type = ExportType.Jpg;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sFile = saveFileDialog1.FileName;

                if (saveFileDialog1.FilterIndex == 1)
                {
                    type = ExportType.Bmp;
                }
                else if (saveFileDialog1.FilterIndex == 2)
                {
                    type = ExportType.Jpg;
                }
                else if (saveFileDialog1.FilterIndex == 3)
                {
                    type = ExportType.Png;
                }
                else if (saveFileDialog1.FilterIndex == 4)
                {
                    type = ExportType.Tiff;
                }
            }

            saveFileDialog1 = null;

            drawArea.ExportImage(sFile, type);
        }

        private void trackBar2_Scroll(object sender, System.EventArgs e)
        {
            int nLine = trackBar2.Value;
            drawArea.SetLineThickness(nLine);
        }

        private void button11_Click(object sender, System.EventArgs e)
        {
            Color selectedColor = button12.BackColor;

            // Sets the initial color.
            colorDialog1.Color = selectedColor;
    
            // Update the text box color if the user clicks OK 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedColor =  colorDialog1.Color;

                drawArea.SetDrawingColor(selectedColor.R, selectedColor.G, selectedColor.B);
                button12.BackColor = selectedColor;
            }
        }

        private void button12_Click(object sender, System.EventArgs e)
        {
            Color selectedColor = button12.BackColor;
            drawArea.SetDrawingColor(selectedColor.R, selectedColor.G, selectedColor.B);
        }

        private void button10_Click(object sender, System.EventArgs e)
        {
            Color selectedColor = button10.BackColor;
            drawArea.SetDefaultDrawingColor(selectedColor.R, selectedColor.G, selectedColor.B);
        }

        private void button15_Click(object sender, System.EventArgs e)
        {
            Font selectedFont = fontDialog1.Font;
    
            fontDialog1.Font = selectedFont;

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFont =  fontDialog1.Font;

                drawArea.SetDefaultFont(selectedFont.Name, selectedFont.Size, (FontStyles)selectedFont.Style);
            }
        }

        private void button16_Click(object sender, System.EventArgs e)
        {
            Font selectedFont = fontDialog1.Font;
    
            fontDialog1.Font = selectedFont;

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFont =  fontDialog1.Font;

                drawArea.SetFont(selectedFont.Name, selectedFont.Size, (FontStyles)selectedFont.Style);
            }
        }

        private void button9_Click(object sender, System.EventArgs e)
        {
            drawArea.SetZoomMode(IVMAnnotation.ZoomType.ZoomIn);        
        }

        private void button17_Click(object sender, System.EventArgs e)
        {
            drawArea.SetZoomMode(IVMAnnotation.ZoomType.ZoomOut);        
        }

        private void button18_Click(object sender, System.EventArgs e)
        {
            drawArea.ResetZoom();
        }

        private void button19_Click(object sender, System.EventArgs e)
        {
            drawArea.UnselectAll();
        }

        private void button14_Click(object sender, System.EventArgs e)
        {
            drawArea.SetTextString(IVMAnnotation.StringItems.TextInputDlgMessage, "asdfert");
            drawArea.SetTextString(IVMAnnotation.StringItems.TextInputDlgOK,        "---");
            drawArea.SetTextString(IVMAnnotation.StringItems.TextInputDlgCancel, "xxx");
        }

        private void butOpenDA4_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = openFileDialog1.FileName;

                drawArea3.OpenFile(sFile);
            }
        }

        private void butSaveDA4_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "all files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1 ;
            saveFileDialog1.RestoreDirectory = true ;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = saveFileDialog1.FileName;

                drawArea3.SaveFile(sFile);        
            }
        
        }

        private void butOpenDA3_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = openFileDialog1.FileName;

                drawArea2.OpenFile(sFile);
            }
        }

        private void butSaveDA3_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "all files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1 ;
            saveFileDialog1.RestoreDirectory = true ;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = saveFileDialog1.FileName;

                drawArea2.SaveFile(sFile);        
            }
        }

        private void butOpenDA2_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = openFileDialog1.FileName;

                drawArea1.OpenFile(sFile);
            }
         }

        private void butSaveDA2_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "all files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1 ;
            saveFileDialog1.RestoreDirectory = true ;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = saveFileDialog1.FileName;

                drawArea1.SaveFile(sFile);        
            }
        }

        private void button13_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Filter = "bmp files (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1 ;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFile = openFileDialog1.FileName;

                drawArea.SetBitmap(sFile);
                drawArea1.SetBitmap(sFile);
                drawArea2.SetBitmap(sFile);
                drawArea3.SetBitmap(sFile);
            }
       
        }

        private void button20_Click(object sender, System.EventArgs e)
        {
            drawArea.SetBitmap("");
            drawArea1.SetBitmap("");
            drawArea2.SetBitmap("");
            drawArea3.SetBitmap("");
        }

        private void butExportFile_Click(object sender, System.EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BMP files (*.bmp)|*.bmp"  ;

            string sSrcFile = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sSrcFile = openFileDialog1.FileName;

                saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "BMP files (*.bmp)|*.bmp|Jpg files (*.Jpg)|*.jpg|Png files (*.png)|*.png|Tiff files (*.tiff)|*.tiff"  ;
                saveFileDialog1.FilterIndex = 2 ;
                saveFileDialog1.RestoreDirectory = true ;

                saveFileDialog1.AddExtension = true;

                string sFile = "temp.jpg";
                ExportType type = ExportType.Jpg;

                string sFile1 = "";
                string sFile2 = "";
                string sFile3 = "";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sFile = saveFileDialog1.FileName;
                    sFile1 = "C:\\test-1.jpg";
                    sFile2 = "C:\\test-2.jpg";
                    sFile3 = "C:\\test-3.jpg";

                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        type = ExportType.Bmp;
                    }
                    else if (saveFileDialog1.FilterIndex == 2)
                    {
                        type = ExportType.Jpg;
                    }
                    else if (saveFileDialog1.FilterIndex == 3)
                    {
                        type = ExportType.Png;
                    }
                    else if (saveFileDialog1.FilterIndex == 4)
                    {
                        type = ExportType.Tiff;
                    }
                }

                string sSrcAnnFile = Path.ChangeExtension(sSrcFile, ".ann");
                drawArea.ExportImageFromFile(sSrcFile, sSrcAnnFile, sFile, type);
                drawArea1.ExportImageFromFile(sSrcFile, sSrcAnnFile, sFile1, type);
                drawArea2.ExportImageFromFile(sSrcFile, sSrcAnnFile, sFile2, type);
                drawArea3.ExportImageFromFile(sSrcFile, sSrcAnnFile, sFile3, type);
            }

            saveFileDialog1 = null;
            openFileDialog1 = null;
        }
        
	}
}
