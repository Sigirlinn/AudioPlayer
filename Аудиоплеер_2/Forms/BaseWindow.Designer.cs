using Аудиоплеер_2.Controls;
using Аудиоплеер_2.Classes;
namespace Аудиоплеер_2.Forms
{
    partial class BaseWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.bottomPanel1 = new Аудиоплеер_2.Controls.BottomPanel();
            this.topPanel1 = new Аудиоплеер_2.Controls.TopPanel();
            this.SuspendLayout();
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.Color.Transparent;
            this.bottomPanel1.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Панель;
            this.bottomPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bottomPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel1.Location = new System.Drawing.Point(0, 200);
            this.bottomPanel1.Name = "bottomPanel1";
            this.bottomPanel1.Size = new System.Drawing.Size(320, 40);
            this.bottomPanel1.TabIndex = 1;
            // 
            // topPanel1
            // 
            this.topPanel1.BackColor = System.Drawing.Color.Transparent;
            this.topPanel1.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Панель;
            this.topPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.topPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel1.Location = new System.Drawing.Point(0, 0);
            this.topPanel1.Name = "topPanel1";
            this.topPanel1.Size = new System.Drawing.Size(320, 30);
            this.topPanel1.TabIndex = 0;
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Фон;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Controls.Add(this.bottomPanel1);
            this.Controls.Add(this.topPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "BaseWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        public BottomPanel bottomPanel1;
        public TopPanel topPanel1;
    }
}

