namespace Аудиоплеер_2.Forms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainBottomPanel1 = new Аудиоплеер_2.Controls.MainBottomPanel();
            this.OGL = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Панель;
            this.bottomPanel1.Controls.Add(this.mainBottomPanel1);
            this.bottomPanel1.Location = new System.Drawing.Point(0, 360);
            this.bottomPanel1.Size = new System.Drawing.Size(400, 40);
            // 
            // topPanel1
            // 
            this.topPanel1.BackColor = System.Drawing.Color.White;
            this.topPanel1.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Панель;
            this.topPanel1.Size = new System.Drawing.Size(400, 30);
            this.topPanel1.SizeChanged += new System.EventHandler(this.topPanel1_SizeChanged);
            // 
            // mainBottomPanel1
            // 
            this.mainBottomPanel1.BackColor = System.Drawing.Color.Transparent;
            this.mainBottomPanel1.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Панель;
            this.mainBottomPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainBottomPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBottomPanel1.Location = new System.Drawing.Point(0, 0);
            this.mainBottomPanel1.Name = "mainBottomPanel1";
            this.mainBottomPanel1.Size = new System.Drawing.Size(400, 40);
            this.mainBottomPanel1.TabIndex = 2;
            // 
            // OGL
            // 
            this.OGL.AccumBits = ((byte)(0));
            this.OGL.AutoCheckErrors = false;
            this.OGL.AutoFinish = false;
            this.OGL.AutoMakeCurrent = true;
            this.OGL.AutoSwapBuffers = true;
            this.OGL.BackColor = System.Drawing.Color.Black;
            this.OGL.ColorBits = ((byte)(32));
            this.OGL.DepthBits = ((byte)(16));
            this.OGL.Location = new System.Drawing.Point(0, 55);
            this.OGL.Name = "OGL";
            this.OGL.Size = new System.Drawing.Size(400, 285);
            this.OGL.StencilBits = ((byte)(0));
            this.OGL.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(5, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(5, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 14);
            this.label2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(265, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 14);
            this.label3.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Location = new System.Drawing.Point(106, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(11, 100);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.Location = new System.Drawing.Point(48, 342);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 15);
            this.panel2.TabIndex = 7;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OGL);
            this.Controls.Add(this.panel2);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.TransparencyKey = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.OGL, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.topPanel1, 0);
            this.Controls.SetChildIndex(this.bottomPanel1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Аудиоплеер_2.Controls.MainBottomPanel mainBottomPanel1;
        public Tao.Platform.Windows.SimpleOpenGlControl OGL;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
    }
}