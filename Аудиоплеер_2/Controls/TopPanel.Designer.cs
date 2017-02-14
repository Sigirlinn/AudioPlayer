namespace Аудиоплеер_2.Controls
{
    partial class TopPanel
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::Аудиоплеер_2.Properties.Resources.Панель;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.Name = "TopPanel";
            this.Size = new System.Drawing.Size(200, 35);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseUp);
            this.SizeChanged += new System.EventHandler(this.topPanel_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
