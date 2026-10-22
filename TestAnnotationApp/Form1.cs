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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			button5 = new Button();
			button6 = new Button();
			button7 = new Button();
			drawArea = new DrawArea();
			RemoveAll = new Button();
			Remove = new Button();
			colorDialog1 = new ColorDialog();
			butSetColor = new Button();
			trackBar1 = new TrackBar();
			label1 = new Label();
			butOpen = new Button();
			butSave = new Button();
			radioButton1 = new RadioButton();
			radioButton2 = new RadioButton();
			groupBox1 = new GroupBox();
			button8 = new Button();
			button9 = new Button();
			groupBox2 = new GroupBox();
			button10 = new Button();
			button15 = new Button();
			groupBox3 = new GroupBox();
			trackBar2 = new TrackBar();
			label2 = new Label();
			button11 = new Button();
			button12 = new Button();
			button16 = new Button();
			button13 = new Button();
			button14 = new Button();
			fontDialog1 = new FontDialog();
			button17 = new Button();
			button18 = new Button();
			button19 = new Button();
			button20 = new Button();
			button21 = new Button();
			openFileDialog1 = new OpenFileDialog();
			saveFileDialog1 = new SaveFileDialog();
			drawArea1 = new DrawArea();
			drawArea2 = new DrawArea();
			butSaveDA2 = new Button();
			butOpenDA2 = new Button();
			butSaveDA4 = new Button();
			butOpenDA4 = new Button();
			drawArea3 = new DrawArea();
			butSaveDA3 = new Button();
			butOpenDA3 = new Button();
			button22 = new Button();
			butExportFile = new Button();
			((ISupportInitialize)trackBar1).BeginInit();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			((ISupportInitialize)trackBar2).BeginInit();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Image = (Image)resources.GetObject("button1.Image");
			button1.Location = new Point(8, 52);
			button1.Name = "button1";
			button1.Size = new Size(32, 33);
			button1.TabIndex = 3;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Image = (Image)resources.GetObject("button2.Image");
			button2.Location = new Point(8, 17);
			button2.Name = "button2";
			button2.Size = new Size(32, 35);
			button2.TabIndex = 4;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Image = (Image)resources.GetObject("button3.Image");
			button3.Location = new Point(8, 85);
			button3.Name = "button3";
			button3.Size = new Size(32, 34);
			button3.TabIndex = 7;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Image = (Image)resources.GetObject("button4.Image");
			button4.Location = new Point(8, 154);
			button4.Name = "button4";
			button4.Size = new Size(32, 34);
			button4.TabIndex = 8;
			button4.Click += button4_Click;
			// 
			// button5
			// 
			button5.Image = (Image)resources.GetObject("button5.Image");
			button5.Location = new Point(8, 188);
			button5.Name = "button5";
			button5.Size = new Size(32, 34);
			button5.TabIndex = 9;
			button5.Click += button5_Click;
			// 
			// button6
			// 
			button6.Image = (Image)resources.GetObject("button6.Image");
			button6.Location = new Point(8, 119);
			button6.Name = "button6";
			button6.Size = new Size(32, 35);
			button6.TabIndex = 10;
			button6.Click += button6_Click;
			// 
			// button7
			// 
			button7.Image = (Image)resources.GetObject("button7.Image");
			button7.Location = new Point(8, 222);
			button7.Name = "button7";
			button7.Size = new Size(32, 34);
			button7.TabIndex = 11;
			button7.Click += button7_Click;
			// 
			// drawArea
			// 
			drawArea.ActiveTool = AnnotationType.SelectionPointer;
			drawArea.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			drawArea.BackgroundImage = (Image)resources.GetObject("drawArea.BackgroundImage");
			drawArea.Dirty = false;
			drawArea.DrawNetRectangle = false;
			drawArea.ForeColor = SystemColors.Window;
			drawArea.GraphicsList = null;
			drawArea.Location = new Point(152, 0);
			drawArea.Name = "drawArea";
			drawArea.NetRectangle = new Rectangle(0, 0, 0, 0);
			drawArea.ShowAnnotation = true;
			drawArea.Size = new Size(784, 657);
			drawArea.TabIndex = 12;
			drawArea.Zoomed = false;
			// 
			// RemoveAll
			// 
			RemoveAll.Location = new Point(48, 206);
			RemoveAll.Name = "RemoveAll";
			RemoveAll.Size = new Size(96, 28);
			RemoveAll.TabIndex = 13;
			RemoveAll.Text = "RemoveAll";
			RemoveAll.Click += RemoveAll_Click;
			// 
			// Remove
			// 
			Remove.Location = new Point(48, 170);
			Remove.Name = "Remove";
			Remove.Size = new Size(96, 27);
			Remove.TabIndex = 14;
			Remove.Text = "Remove";
			Remove.Click += Remove_Click;
			// 
			// butSetColor
			// 
			butSetColor.Location = new Point(31, 102);
			butSetColor.Name = "butSetColor";
			butSetColor.Size = new Size(49, 28);
			butSetColor.TabIndex = 15;
			butSetColor.Text = "Color";
			butSetColor.Click += butSetColor_Click;
			// 
			// trackBar1
			// 
			trackBar1.Location = new Point(8, 43);
			trackBar1.Minimum = 1;
			trackBar1.Name = "trackBar1";
			trackBar1.Size = new Size(120, 45);
			trackBar1.TabIndex = 16;
			trackBar1.TickStyle = TickStyle.TopLeft;
			trackBar1.Value = 1;
			trackBar1.Scroll += trackBar1_Scroll;
			// 
			// label1
			// 
			label1.Location = new Point(8, 26);
			label1.Name = "label1";
			label1.Size = new Size(96, 18);
			label1.TabIndex = 17;
			label1.Text = "Line thickness:";
			// 
			// butOpen
			// 
			butOpen.Image = (Image)resources.GetObject("butOpen.Image");
			butOpen.Location = new Point(48, 17);
			butOpen.Name = "butOpen";
			butOpen.Size = new Size(32, 35);
			butOpen.TabIndex = 18;
			butOpen.Click += butOpen_Click;
			// 
			// butSave
			// 
			butSave.Image = (Image)resources.GetObject("butSave.Image");
			butSave.Location = new Point(80, 17);
			butSave.Name = "butSave";
			butSave.Size = new Size(32, 35);
			butSave.TabIndex = 19;
			butSave.Click += butSave_Click;
			// 
			// radioButton1
			// 
			radioButton1.Checked = true;
			radioButton1.Location = new Point(16, 26);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new Size(64, 26);
			radioButton1.TabIndex = 20;
			radioButton1.TabStop = true;
			radioButton1.Text = "Show";
			radioButton1.CheckedChanged += radioButton1_CheckedChanged;
			// 
			// radioButton2
			// 
			radioButton2.Location = new Point(16, 60);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new Size(64, 25);
			radioButton2.TabIndex = 21;
			radioButton2.Text = "Hide";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(radioButton1);
			groupBox1.Controls.Add(radioButton2);
			groupBox1.Location = new Point(48, 246);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(96, 94);
			groupBox1.TabIndex = 22;
			groupBox1.TabStop = false;
			groupBox1.Text = "Show/Hide";
			// 
			// button8
			// 
			button8.Location = new Point(48, 94);
			button8.Name = "button8";
			button8.Size = new Size(96, 29);
			button8.TabIndex = 23;
			button8.Tag = "";
			button8.Text = "Export";
			button8.Click += button8_Click;
			// 
			// button9
			// 
			button9.Image = (Image)resources.GetObject("button9.Image");
			button9.Location = new Point(48, 359);
			button9.Name = "button9";
			button9.Size = new Size(32, 35);
			button9.TabIndex = 24;
			button9.Click += button9_Click;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(trackBar1);
			groupBox2.Controls.Add(label1);
			groupBox2.Controls.Add(butSetColor);
			groupBox2.Controls.Add(button10);
			groupBox2.Controls.Add(button15);
			groupBox2.Location = new Point(10, 414);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(135, 136);
			groupBox2.TabIndex = 25;
			groupBox2.TabStop = false;
			groupBox2.Text = "Defaults";
			// 
			// button10
			// 
			button10.Enabled = false;
			button10.FlatStyle = FlatStyle.Flat;
			button10.Location = new Point(6, 105);
			button10.Name = "button10";
			button10.Size = new Size(23, 23);
			button10.TabIndex = 26;
			button10.Click += button10_Click;
			// 
			// button15
			// 
			button15.Location = new Point(80, 102);
			button15.Name = "button15";
			button15.Size = new Size(50, 28);
			button15.TabIndex = 30;
			button15.Text = "Font";
			button15.Click += button15_Click;
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(trackBar2);
			groupBox3.Controls.Add(label2);
			groupBox3.Controls.Add(button11);
			groupBox3.Controls.Add(button12);
			groupBox3.Controls.Add(button16);
			groupBox3.Location = new Point(10, 561);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(135, 137);
			groupBox3.TabIndex = 27;
			groupBox3.TabStop = false;
			groupBox3.Text = "Set properties:";
			// 
			// trackBar2
			// 
			trackBar2.Location = new Point(8, 43);
			trackBar2.Minimum = 1;
			trackBar2.Name = "trackBar2";
			trackBar2.Size = new Size(120, 45);
			trackBar2.TabIndex = 16;
			trackBar2.TickStyle = TickStyle.TopLeft;
			trackBar2.Value = 1;
			trackBar2.Scroll += trackBar2_Scroll;
			// 
			// label2
			// 
			label2.Location = new Point(8, 26);
			label2.Name = "label2";
			label2.Size = new Size(96, 18);
			label2.TabIndex = 17;
			label2.Text = "Line thickness:";
			// 
			// button11
			// 
			button11.Location = new Point(31, 102);
			button11.Name = "button11";
			button11.Size = new Size(49, 28);
			button11.TabIndex = 15;
			button11.Text = "Color";
			button11.Click += button11_Click;
			// 
			// button12
			// 
			button12.BackColor = SystemColors.Window;
			button12.FlatStyle = FlatStyle.Flat;
			button12.Location = new Point(6, 105);
			button12.Name = "button12";
			button12.Size = new Size(24, 24);
			button12.TabIndex = 26;
			button12.UseVisualStyleBackColor = false;
			button12.Click += button12_Click;
			// 
			// button16
			// 
			button16.Location = new Point(80, 102);
			button16.Name = "button16";
			button16.Size = new Size(50, 28);
			button16.TabIndex = 31;
			button16.Text = "Font";
			button16.Click += button16_Click;
			// 
			// button13
			// 
			button13.Location = new Point(48, 57);
			button13.Name = "button13";
			button13.Size = new Size(96, 28);
			button13.TabIndex = 28;
			button13.Text = "Import";
			button13.Click += button13_Click;
			// 
			// button14
			// 
			button14.Location = new Point(112, 17);
			button14.Name = "button14";
			button14.Size = new Size(32, 35);
			button14.TabIndex = 29;
			button14.Click += button14_Click;
			// 
			// button17
			// 
			button17.Image = (Image)resources.GetObject("button17.Image");
			button17.Location = new Point(80, 359);
			button17.Name = "button17";
			button17.Size = new Size(32, 35);
			button17.TabIndex = 30;
			button17.Click += button17_Click;
			// 
			// button18
			// 
			button18.Image = (Image)resources.GetObject("button18.Image");
			button18.Location = new Point(112, 359);
			button18.Name = "button18";
			button18.Size = new Size(32, 35);
			button18.TabIndex = 31;
			button18.Click += button18_Click;
			// 
			// button19
			// 
			button19.Location = new Point(8, 256);
			button19.Name = "button19";
			button19.Size = new Size(32, 34);
			button19.TabIndex = 32;
			button19.Click += button19_Click;
			// 
			// button20
			// 
			button20.Location = new Point(8, 290);
			button20.Name = "button20";
			button20.Size = new Size(32, 34);
			button20.TabIndex = 33;
			button20.Click += button20_Click;
			// 
			// button21
			// 
			button21.Enabled = false;
			button21.Location = new Point(8, 324);
			button21.Name = "button21";
			button21.Size = new Size(32, 34);
			button21.TabIndex = 34;
			// 
			// drawArea1
			// 
			drawArea1.ActiveTool = AnnotationType.SelectionPointer;
			drawArea1.Dirty = false;
			drawArea1.DrawNetRectangle = false;
			drawArea1.ForeColor = SystemColors.Window;
			drawArea1.GraphicsList = null;
			drawArea1.Location = new Point(950, 20);
			drawArea1.Name = "drawArea1";
			drawArea1.NetRectangle = new Rectangle(0, 0, 0, 0);
			drawArea1.ShowAnnotation = true;
			drawArea1.Size = new Size(260, 226);
			drawArea1.TabIndex = 35;
			drawArea1.Visible = false;
			drawArea1.Zoomed = false;
			// 
			// drawArea2
			// 
			drawArea2.ActiveTool = AnnotationType.SelectionPointer;
			drawArea2.Dirty = false;
			drawArea2.DrawNetRectangle = false;
			drawArea2.ForeColor = SystemColors.Window;
			drawArea2.GraphicsList = null;
			drawArea2.Location = new Point(950, 315);
			drawArea2.Name = "drawArea2";
			drawArea2.NetRectangle = new Rectangle(0, 0, 0, 0);
			drawArea2.ShowAnnotation = true;
			drawArea2.Size = new Size(260, 532);
			drawArea2.TabIndex = 36;
			drawArea2.Visible = false;
			drawArea2.Zoomed = false;
			// 
			// butSaveDA2
			// 
			butSaveDA2.Image = (Image)resources.GetObject("butSaveDA2.Image");
			butSaveDA2.Location = new Point(1181, 256);
			butSaveDA2.Name = "butSaveDA2";
			butSaveDA2.Size = new Size(31, 34);
			butSaveDA2.TabIndex = 38;
			butSaveDA2.Visible = false;
			butSaveDA2.Click += butSaveDA2_Click;
			// 
			// butOpenDA2
			// 
			butOpenDA2.Image = (Image)resources.GetObject("butOpenDA2.Image");
			butOpenDA2.Location = new Point(1148, 256);
			butOpenDA2.Name = "butOpenDA2";
			butOpenDA2.Size = new Size(33, 34);
			butOpenDA2.TabIndex = 37;
			butOpenDA2.Visible = false;
			butOpenDA2.Click += butOpenDA2_Click;
			// 
			// butSaveDA4
			// 
			butSaveDA4.Image = (Image)resources.GetObject("butSaveDA4.Image");
			butSaveDA4.Location = new Point(974, 857);
			butSaveDA4.Name = "butSaveDA4";
			butSaveDA4.Size = new Size(32, 34);
			butSaveDA4.TabIndex = 40;
			butSaveDA4.Visible = false;
			butSaveDA4.Click += butSaveDA4_Click;
			// 
			// butOpenDA4
			// 
			butOpenDA4.Image = (Image)resources.GetObject("butOpenDA4.Image");
			butOpenDA4.Location = new Point(941, 857);
			butOpenDA4.Name = "butOpenDA4";
			butOpenDA4.Size = new Size(32, 34);
			butOpenDA4.TabIndex = 39;
			butOpenDA4.Visible = false;
			butOpenDA4.Click += butOpenDA4_Click;
			// 
			// drawArea3
			// 
			drawArea3.ActiveTool = AnnotationType.SelectionPointer;
			drawArea3.Dirty = false;
			drawArea3.DrawNetRectangle = false;
			drawArea3.ForeColor = SystemColors.Window;
			drawArea3.GraphicsList = null;
			drawArea3.Location = new Point(154, 670);
			drawArea3.Name = "drawArea3";
			drawArea3.NetRectangle = new Rectangle(0, 0, 0, 0);
			drawArea3.ShowAnnotation = true;
			drawArea3.Size = new Size(787, 216);
			drawArea3.TabIndex = 41;
			drawArea3.Visible = false;
			drawArea3.Zoomed = false;
			// 
			// butSaveDA3
			// 
			butSaveDA3.Image = (Image)resources.GetObject("butSaveDA3.Image");
			butSaveDA3.Location = new Point(1181, 857);
			butSaveDA3.Name = "butSaveDA3";
			butSaveDA3.Size = new Size(31, 34);
			butSaveDA3.TabIndex = 43;
			butSaveDA3.Visible = false;
			butSaveDA3.Click += butSaveDA3_Click;
			// 
			// butOpenDA3
			// 
			butOpenDA3.Image = (Image)resources.GetObject("butOpenDA3.Image");
			butOpenDA3.Location = new Point(1148, 857);
			butOpenDA3.Name = "butOpenDA3";
			butOpenDA3.Size = new Size(33, 34);
			butOpenDA3.TabIndex = 42;
			butOpenDA3.Visible = false;
			butOpenDA3.Click += butOpenDA3_Click;
			// 
			// button22
			// 
			button22.Enabled = false;
			button22.Location = new Point(8, 359);
			button22.Name = "button22";
			button22.Size = new Size(32, 35);
			button22.TabIndex = 44;
			// 
			// butExportFile
			// 
			butExportFile.Location = new Point(48, 132);
			butExportFile.Name = "butExportFile";
			butExportFile.Size = new Size(96, 29);
			butExportFile.TabIndex = 45;
			butExportFile.Tag = "";
			butExportFile.Text = "Export file";
			butExportFile.Click += butExportFile_Click;
			// 
			// Form1
			// 
			AutoScaleBaseSize = new Size(6, 16);
			ClientSize = new Size(948, 720);
			Controls.Add(butExportFile);
			Controls.Add(button22);
			Controls.Add(butSaveDA3);
			Controls.Add(butOpenDA3);
			Controls.Add(drawArea3);
			Controls.Add(butSaveDA4);
			Controls.Add(butOpenDA4);
			Controls.Add(butSaveDA2);
			Controls.Add(butOpenDA2);
			Controls.Add(drawArea2);
			Controls.Add(drawArea1);
			Controls.Add(button21);
			Controls.Add(button20);
			Controls.Add(button19);
			Controls.Add(button18);
			Controls.Add(button17);
			Controls.Add(button14);
			Controls.Add(button13);
			Controls.Add(groupBox2);
			Controls.Add(button9);
			Controls.Add(button8);
			Controls.Add(groupBox1);
			Controls.Add(butSave);
			Controls.Add(butOpen);
			Controls.Add(Remove);
			Controls.Add(RemoveAll);
			Controls.Add(drawArea);
			Controls.Add(button7);
			Controls.Add(button6);
			Controls.Add(button5);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(groupBox3);
			MaximizeBox = false;
			Name = "Form1";
			Text = "Image & Video Manager Annotations Demo";
			((ISupportInitialize)trackBar1).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			((ISupportInitialize)trackBar2).EndInit();
			ResumeLayout(false);

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
