namespace Plumber
{
    partial class Form1
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
            this.exitBtn = new System.Windows.Forms.PictureBox();
            this.newGameBtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Image = global::Plumber.Properties.Resources.button_exit;
            this.exitBtn.Location = new System.Drawing.Point(355, 177);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(130, 30);
            this.exitBtn.TabIndex = 4;
            this.exitBtn.TabStop = false;
            this.exitBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.exitBtn_MouseDown);
            this.exitBtn.MouseEnter += new System.EventHandler(this.exitBtn_MouseEnter);
            this.exitBtn.MouseLeave += new System.EventHandler(this.exitBtn_MouseLeave);
            this.exitBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.exitBtn_MouseUp);
            // 
            // newGameBtn
            // 
            this.newGameBtn.Image = global::Plumber.Properties.Resources.button_new_game;
            this.newGameBtn.Location = new System.Drawing.Point(355, 141);
            this.newGameBtn.Name = "newGameBtn";
            this.newGameBtn.Size = new System.Drawing.Size(130, 30);
            this.newGameBtn.TabIndex = 3;
            this.newGameBtn.TabStop = false;
            this.newGameBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newGameBtn_MouseDown);
            this.newGameBtn.MouseEnter += new System.EventHandler(this.newGameBtn_MouseEnter);
            this.newGameBtn.MouseLeave += new System.EventHandler(this.newGameBtn_MouseLeave);
            this.newGameBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.newGameBtn_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 374);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.newGameBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox newGameBtn;
        private System.Windows.Forms.PictureBox exitBtn;






    }
}

