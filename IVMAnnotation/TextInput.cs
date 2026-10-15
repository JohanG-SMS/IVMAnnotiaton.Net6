using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IVMAnnotation
{
    [ComVisible(false)]
	/// <summary>
	/// Summary description for TextInpuit.
	/// </summary>
	public class TextInput : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public string TextBoxInput
        {
            get {return textBox1.Text;}
            set {textBox1.Text = value;}
        }

		public TextInput(string sMsg, string sOK, string sCancel)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            
            label1.Text     = sMsg;
            butOK.Text      = sOK;
            butCancel.Text  = sCancel;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(213, 69);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "";
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(157, 104);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(63, 20);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(90, 104);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(62, 20);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "OK";
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter text:";
            // 
            // TextInput
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(228, 142);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }
		#endregion

        private void butOK_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
	}
}
