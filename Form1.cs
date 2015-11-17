using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

namespace asgn5v1
{
	/// <summary>
	/// Summary description for Transformer.
	/// </summary>
	public class Transformer : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		//private bool GetNewData();

		// basic data for Transformer

		int numpts = 0;
		int numlines = 0;
		bool gooddata = false;
        //continuous rotation variables to be changed w/user click
        char rotationAxis = 'x';
		double[,] vertices;
		double[,] scrnpts;
        double[,] tNet;
        double[,] translationMatrix = new double[4,4];
        double[,] scalingMatrix = new double[4,4];
        double[,] rotationMatix = new double[4,4]; 
        double cosine = Math.Cos(0.05);
        double sine = Math.Sin(0.05);
		double[,] ctrans = new double[4,4];  //your main transformation matrix
        double xcorrect = 0.0;
        double ycorrect = 0.0;
        bool rotateFlag = false;
		private System.Windows.Forms.ImageList tbimages;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton transleftbtn;
		private System.Windows.Forms.ToolBarButton transrightbtn;
		private System.Windows.Forms.ToolBarButton transupbtn;
		private System.Windows.Forms.ToolBarButton transdownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton scaleupbtn;
		private System.Windows.Forms.ToolBarButton scaledownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton rotxby1btn;
		private System.Windows.Forms.ToolBarButton rotyby1btn;
		private System.Windows.Forms.ToolBarButton rotzby1btn;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton rotxbtn;
		private System.Windows.Forms.ToolBarButton rotybtn;
		private System.Windows.Forms.ToolBarButton rotzbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton shearrightbtn;
		private System.Windows.Forms.ToolBarButton shearleftbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;
		int[,] lines;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

		public Transformer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		    t.Interval = 20; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			Text = "COMP 4560:  Assignment 5 (200830) (Jessica Tekenos)";
			ResizeRedraw = true;
			BackColor = Color.Black;
			MenuItem miNewDat = new MenuItem("New &Data...",
				new EventHandler(MenuNewDataOnClick));
			MenuItem miExit = new MenuItem("&Exit", 
				new EventHandler(MenuFileExitOnClick));
			MenuItem miDash = new MenuItem("-");
			MenuItem miFile = new MenuItem("&File",
				new MenuItem[] {miNewDat, miDash, miExit});
			MenuItem miAbout = new MenuItem("&About",
				new EventHandler(MenuAboutOnClick));
			Menu = new MainMenu(new MenuItem[] {miFile, miAbout});

			
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Transformer());
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
			double temp;
			int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans
                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //MessageBox.Show("Scrnpts " + scrnpts[0, 1]);

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
		} // end of OnPaint

		void MenuNewDataOnClick(object obj, EventArgs ea)
		{
			//MessageBox.Show("New Data item clicked.");
			gooddata = GetNewData();
			RestoreInitialImage();			
		}

		void MenuFileExitOnClick(object obj, EventArgs ea)
		{
			Close();
		}

		void MenuAboutOnClick(object obj, EventArgs ea)
		{
			AboutDialogBox dlg = new AboutDialogBox();
			dlg.ShowDialog();
		}

		void RestoreInitialImage()
		{
			Invalidate();
		} // end of RestoreInitialImage

		bool GetNewData()
		{
            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity
			string strinputfile,text;
			ArrayList coorddata = new ArrayList();
			ArrayList linesdata = new ArrayList();
			OpenFileDialog opendlg = new OpenFileDialog();
			opendlg.Title = "Choose File with Coordinates of Vertices";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;				
				FileInfo coordfile = new FileInfo(strinputfile);
				StreamReader reader = coordfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) coorddata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeCoords(coorddata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Coordinates File***");
				return false;
			}
            
			opendlg.Title = "Choose File with Data Specifying Lines";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;
				FileInfo linesfile = new FileInfo(strinputfile);
				StreamReader reader = linesfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) linesdata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeLines(linesdata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Line Data File***");
				return false;
			}
			scrnpts = new double[numpts,4];
			
			return true;
		} // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            PictureBox display = new PictureBox();

            display.Width = ClientRectangle.Width;
            display.Height = ClientRectangle.Height;
            double midw = (double)display.Width / 2.0d;
            double midh = (double)display.Height / 2.0d;
            double scaleFactor = midh * .05000;

            //this may allocate slightly more rows that necessary
            vertices = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;

            //find minimum x and y values
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.0d) break;
                vertices[numpts, 1] = double.Parse(text[1]);
                vertices[numpts, 2] = double.Parse(text[2]);
                vertices[numpts, 3] = 1.0d;
                numpts++;
            }

            double xmax = 0; double xmin = 10000.0;
            double ymax = 0; double ymin = 10000.0;

            for (int i = 1; i < numpts; i++)
            {
                if (vertices[i, 0] < 0.0d) break;

                if (xmax < vertices[i, 0])
                {
                    xmax = vertices[i, 0];
                }
                if (xmin > vertices[i, 0])
                {
                    xmin = vertices[i, 0];
                }
                if (ymax < vertices[i, 1])
                {
                    ymax = vertices[i, 1];
                }
                if (ymin > vertices[i, 1])
                {
                    ymin = vertices[i, 1];
                }
            }

            numpts = 0;
            ycorrect = scaleFactor * ymin;
            xcorrect = scaleFactor * xmin;

            //scale and translate image to center and half height of screen
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.000d)
                {
                    break;
                }
                else
                {
                    vertices[numpts, 0] = ((double.Parse(text[0]) * scaleFactor) + midw) - (midh * .500) - xcorrect;
                }
                vertices[numpts, 1] = (midh - (double.Parse(text[1]) * scaleFactor)) + (midh * .500) + xcorrect;
                vertices[numpts, 2] = (double.Parse(text[2]) * scaleFactor) - (midh * 0.500);
                vertices[numpts, 3] = 1.0d;
                numpts++;
            }
        }// end of DecodeCoords


		void DecodeLines(ArrayList linesdata)
		{
			//this may allocate slightly more rows that necessary
			lines = new int[linesdata.Count,2];
			numlines = 0;
			string [] text = null;
			for (int i = 0; i < linesdata.Count; i++)
			{
				text = linesdata[i].ToString().Split(' ',',');
				lines[numlines,0]=int.Parse(text[0]);
				if (lines[numlines,0] < 0) break;
				lines[numlines,1]=int.Parse(text[1]);
				numlines++;						
			}
		} // end of DecodeLines

		void setIdentity(double[,] A,int nrow,int ncol)
		{
			for (int i = 0; i < nrow;i++) 
			{
				for (int j = 0; j < ncol; j++) A[i,j] = 0.0d;
				A[i,i] = 1.0d;
			}
		}// end of setIdentity


        ///<summary>
        ///Multiplies two matrices together. 
        ///</summary>
        ///<param name="A">First matrix</param>
        ///<param name="B">Second matrix</param>
        ///<returns>Resulting matrix</returns>
        private double[,] multiplyMatrices(double[,] A, double[,] B)
        {
            tNet = new double[4, 4];
            double temp = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp = 0.000d;
                    for (int k = 0; k < 4; k++)
                    {
                        temp += A[i, k] * B[k, j];
                    }
                    tNet[i, j] = temp;
                }
            }
            return tNet;
        }

        private double[,] translation(double x, double y, double z)
        {
            double[,] result ={{1.0, 0.0, 0.0, 0.0},
                               {0.0, 1.0, 0.0, 0.0},
                               {0.0, 0.0, 1.0, 0.0}, 
                               {x,   y,   z,   1.0}};
            return result;
        }

        ///<summary>
        ///Returns matrix for scaling with specified x, y, z factors.
        ///</summary>
        ///<param name="x">x scaling factor</param>
        ///<param name="y">y scale factor</param>
        ///<param name="z">z scale factor</param>
        ///<returns>Result matrix for the rotation.</returns>
        private double[,] scaling(double x, double y, double z)
        {
            double[,] result ={{x, 0.0, 0.0, 0.0},
                               {0.0, y, 0.0, 0.0},
                               {0.0, 0.0, z, 0.0}, 
                               {0.0, 0.0, 0.0, 1.0}};
            return result;
        }

        ///<summary>
        ///Returns matrix for rotations around x axis.
        ///</summary>
        ///<param name="theta">The angle of rotation in radians.</param>
        ///<returns>Result matrix for the rotation.</returns>
        private double[,] rotationX(double theta)
        {
            double cos = Math.Cos(theta);
            double sin = Math.Sin(theta);
            double[,] result ={{1.0, 0.0, 0.0, 0.0},
                               {0.0, cos, sin, 0.0},
                               {0.0, -sin, cos, 0.0}, 
                               {0.0, 0.0, 0.0, 1.0}};
            return result;
        }

        ///<summary>
        ///Returns matrix for rotations around y axis.
        ///</summary>
        ///<param name="theta">The angle of rotation in radians.</param>
        ///<returns>Result matrix for the rotation.</returns>
        private double[,] rotationY(double theta)
        {
            double cos = Math.Cos(theta);
            double sin = Math.Sin(theta);
            double[,] result ={{cos, 0.0, -sin, 0.0},
                               {0.0, 1.0, 0.0, 0.0},
                               {sin, 0.0, cos, 0.0}, 
                               {0.0, 0.0, 0.0, 1.0}};
            return result;
        }

        ///<summary>
        ///Returns matrix for rotations around z axis.
        ///</summary>
        ///<param name="theta">The angle of rotation in radians.</param>
        ///<returns>Result matrix for the rotation.</returns>
        private double[,] rotationZ(double theta)
        {
            double cos = Math.Cos(theta);
            double sin = Math.Sin(theta);
            double[,] result ={{cos, sin, 0.0, 0.0},
                               {-sin, cos, 0.0, 0.0},
                               {0.0, 0.0, 1.0, 0.0}, 
                               {0.0, 0.0, 0.0, 1.0}};
            return result;
        }

        private double[,] shear(double s)
        {
            double[,] result ={{1.0, 0.0, 0.0, 0.0},
                               {s, 1.0, 0.0, 0.0},
                               {0.0, 0.0, 1.0, 0.0}, 
                               {0.0, 0.0, 0.0, 1.0}};
            return result;
        }

        ///<summary>
        ///Translates a shape to the origin.
        ///</summary>
        ///<returns>Translation matrix.</returns>
        private double[,] moveToOrigin() 
        {
            double[,] result = {{1.0, 0.0, 0.0, 0.0},
                                {0.0, 1.0, 0.0, 0.0},
                                {0.0, 0.0, 1.0, 0.0}, 
                                {-scrnpts[0,0], -scrnpts[0,1], 0.0, 1.0}};
            return result;
        }

        ///<summary>
        ///Translates a shape from the origin.
        ///</summary>
        ///<returns>Translation matrix.</returns>
        private double[,] moveBack()
        {
            double[,] result = {{1.0, 0.0, 0.0, 0.0},
                                {0.0, 1.0, 0.0, 0.0},
                                {0.0, 0.0, 1.0, 0.0}, 
                                {scrnpts[0,0], scrnpts[0,1], 0.0, 1.0}};
            return result;
        }

        ///<summary>
        ///Performs a 3-step scaling, moving to origin, scaling, returning to origin. 
        ///</summary>
        ///<param name="x">scaling factor for x</param>
        ///<param name="y">scaling factor for y</param>
        ///<param name="z">scaling factor for z</param>
        ///<returns> abTimesC: the net transformation matrix.</returns>
        private double[,] scalingOp(double x, double y, double z)
        {
            var scale = scaling(x, y, z);
            var translate = moveToOrigin();
            var translateBack = moveBack();
            var aTimesB = multiplyMatrices(translate, scale);
            var abTimesC = multiplyMatrices(aTimesB, translateBack);
            return abTimesC;
        }

        ///<summary>
        ///Performs a 3-step rotation, moving to origin, rotating, returning to origin. 
        ///</summary>
        ///<param name="axis">The axis on which rotation is performed.</param>
        ///<param name="theta">Rotation in radians.</param>
        private double[,] rotateOp(char axis, double theta)
        {
            var rotate = new double[4,4]; 
            switch (axis)
            {
                case 'x':
                    rotate = rotationX(theta);
                    break;
                case 'y':
                    rotate = rotationY(theta);
                    break;
                case 'z':
                    rotate = rotationZ(theta);
                    break;
                default:
                    rotate = rotationX(theta);
                    break;
            }

            var translate = moveToOrigin();
            var translateBack = moveBack();
            var aTimesB = multiplyMatrices(translate, rotate);
            var abTimesC = multiplyMatrices(aTimesB, translateBack);
            return abTimesC;
        }

        ///<summary>
        ///Performs a 3-step rotation, moving to origin, shear on x axis, returning to origin. 
        ///</summary>
        ///<param name="dir">char (l or r) setting direction of shear.</param>
        ///<param name="s">double, specifies shear factor.</param>
        ///<returns>The net transformation matrix for a shearing of any object.</returns>
        private double[,] shearOp(char dir, double s) {

            //sets the direction of the shear
            switch (dir)
            {
                case 'r':
                    s = -s;
                    break;
            }

            //find lowest y value
            double minY = 1000000.0;
            for (int i = 1; i < numpts; i++)
            {
                if (minY > scrnpts[i, 1])
                {
                    minY = scrnpts[i, 1];
                }
            }

            //get half height of shape
            double yCorretion = scrnpts[0,1] - minY;
            var translate = moveToOrigin();
            var alignWithX = new double[4, 4];
            var reverseAlignWithX = new double[4,4];
            var reverse = new double[4, 4];
            var shearing = new double[4, 4];

            //checks if the shape's original position is above x axis or not
            if (ycorrect < 0.0)
            {
                alignWithX = translation(0, -yCorretion, 0);
                shearing = shear(s);
                reverse = moveBack();
                //Net matrix to return lower edge of shape
                reverseAlignWithX = translation(0, yCorretion, 0);
            }
            else
            {
                //if shape isnt aligned with y = 0, adds additional correction 
                //so scale happens on shape's y = 0
                alignWithX = translation(0, -yCorretion - ycorrect, 0);
                shearing = shear(s);
                reverse = moveBack();
                //Net matrix to return lower edge of shape
                reverseAlignWithX = translation(0, yCorretion + ycorrect, 0);
            }

            //apply transformations
            var fullreverse = multiplyMatrices(reverseAlignWithX, reverse);
            var aTimesB = multiplyMatrices(translate, alignWithX);
            var abTimesC = multiplyMatrices(aTimesB, shearing);
            var abcTimesD = multiplyMatrices(abTimesC, fullreverse);
            return abcTimesD;

        }

        ///<summary>
        ///Increment parameter by set amt
        ///(in this case for continuous rotation)
        ///</summary>
        ///<param name="x">(double) to be incremented</param>
        private double incrementByRads(double x) 
        {
                return x+0.05;
        }

		private void Transformer_Load(object sender, System.EventArgs e)
		{
            
		}

        private void contRotation()
        {
            double i = 0.05;
            var resultOfRotation = rotateOp(rotationAxis, i);
            var applyRotation = multiplyMatrices(ctrans, resultOfRotation);
            ctrans = applyRotation;
            i += 0.05;
            Refresh();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            contRotation();
        }

        void setRotateFalse() 
        {
             rotateFlag = false;
             t.Stop();
        }

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == transleftbtn)
			{
                setRotateFalse();
                ctrans[3, 0] += -75.0;
				Refresh();
			}
			if (e.Button == transrightbtn) 
			{
                setRotateFalse();
                ctrans[3, 0] += 75.0;
				Refresh();
			}
			if (e.Button == transupbtn)
			{
                setRotateFalse();
                ctrans[3, 1] += -35.0;
				Refresh();
			}
			
			if(e.Button == transdownbtn)
			{
                setRotateFalse();
                ctrans[3, 1] += 35.0;
				Refresh();
			}
			if (e.Button == scaleupbtn) 
			{
                setRotateFalse();
                var resultOfScaling = scalingOp(1.1, 1.1, 1.1);
                var applyScaling = multiplyMatrices(ctrans, resultOfScaling);
                ctrans = applyScaling;
                double minX = 100000.0;
                double minY = 100000.0;
                Refresh();

                for (int i = 1; i < numpts; i++)
                {
                    if (minY > scrnpts[i, 1])
                    {
                        minY = scrnpts[i, 1];
                    }
                    if (minX > scrnpts[i, 0])
                    {
                        minX = scrnpts[i, 0];
                    }
                }
			}
			if (e.Button == scaledownbtn) 
			{
                setRotateFalse();
                var resultOfScaling = scalingOp(0.9, 0.9, 0.9);
                var applyScaling = multiplyMatrices(ctrans, resultOfScaling);
                ctrans = applyScaling;
                Refresh();
			}
			if (e.Button == rotxby1btn) 
			{
                setRotateFalse();
                var resultOfRotation = rotateOp('x', 0.05);
                var applyRotation = multiplyMatrices(ctrans, resultOfRotation);
                ctrans = applyRotation;
                Refresh();	
			}
			if (e.Button == rotyby1btn) 
			{
                setRotateFalse();
                var resultOfRotation = rotateOp('y', 0.05);
                var applyRotation = multiplyMatrices(ctrans, resultOfRotation);
                ctrans = applyRotation;
                Refresh();
			}
			if (e.Button == rotzby1btn) 
			{
                setRotateFalse();
                rotateFlag = false;
                t.Stop();
                var resultOfRotation = rotateOp('z', 0.05);
                var applyRotation = multiplyMatrices(ctrans, resultOfRotation);
                ctrans = applyRotation;
                Refresh();
			}
            
            
			if (e.Button == rotxbtn) 
			{
                rotationAxis = 'x';
                if (rotateFlag)
                {
                    rotateFlag = false;
                    t.Stop();
                }
                else
                {
                    rotateFlag = true;
                    t.Start();
                }
			}

			if (e.Button == rotybtn) 
			{
                rotationAxis = 'y';
                if (rotateFlag)
                {
                    rotateFlag = false;
                    t.Stop();
                }
                else
                {
                    rotateFlag = true;
                    t.Start();
                }
			}
			if (e.Button == rotzbtn) 
			{
                rotationAxis = 'z';
                if (rotateFlag)
                {
                    rotateFlag = false;
                    t.Stop();
                }
                else
                {
                    rotateFlag = true;
                    t.Start();
                }
			}
			if(e.Button == shearleftbtn)
			{
                setRotateFalse();
                var resultOfShear = shearOp('l', 0.1);
                var applyShearing = multiplyMatrices(ctrans, resultOfShear);
                ctrans = applyShearing;
				Refresh();
			}
			if (e.Button == shearrightbtn) 
			{
                setRotateFalse();
                var resultOfShear = shearOp('r', 0.1);
                var applyShearing = multiplyMatrices(ctrans, resultOfShear);
                ctrans = applyShearing;
				Refresh();
			}
			if (e.Button == resetbtn)
			{
                setRotateFalse();
				setIdentity(ctrans, 4, 4);
                Refresh();
			}
			if(e.Button == exitbtn) 
			{
				Close();
			}

		}


		
	}

	
}
