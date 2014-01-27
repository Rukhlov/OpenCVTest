namespace OpenCVTest
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
            //ReleaseData();

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
            this.components = new System.ComponentModel.Container();
            this.OriginalImageBox = new Emgu.CV.UI.ImageBox();
            this.StratButton = new System.Windows.Forms.Button();
            this.CannyImageBox = new Emgu.CV.UI.ImageBox();
            this.SmoothImageBox = new Emgu.CV.UI.ImageBox();
            this.ThresholdImageBox = new Emgu.CV.UI.ImageBox();
            this.ThresholdSpinBox = new System.Windows.Forms.NumericUpDown();
            this.MaxThresholdSpinBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoggerTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SmoothBlurSpinBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CannyImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SmoothImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdSpinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxThresholdSpinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SmoothBlurSpinBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImageBox
            // 
            this.OriginalImageBox.Location = new System.Drawing.Point(12, 12);
            this.OriginalImageBox.Name = "OriginalImageBox";
            this.OriginalImageBox.Size = new System.Drawing.Size(268, 249);
            this.OriginalImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OriginalImageBox.TabIndex = 2;
            this.OriginalImageBox.TabStop = false;
            // 
            // StratButton
            // 
            this.StratButton.Location = new System.Drawing.Point(614, 27);
            this.StratButton.Name = "StratButton";
            this.StratButton.Size = new System.Drawing.Size(106, 35);
            this.StratButton.TabIndex = 3;
            this.StratButton.Text = "Start";
            this.StratButton.UseVisualStyleBackColor = true;
            this.StratButton.Click += new System.EventHandler(this.StratButton_Click);
            // 
            // CannyImageBox
            // 
            this.CannyImageBox.Location = new System.Drawing.Point(297, 278);
            this.CannyImageBox.Name = "CannyImageBox";
            this.CannyImageBox.Size = new System.Drawing.Size(268, 249);
            this.CannyImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CannyImageBox.TabIndex = 5;
            this.CannyImageBox.TabStop = false;
            // 
            // SmoothImageBox
            // 
            this.SmoothImageBox.Location = new System.Drawing.Point(297, 12);
            this.SmoothImageBox.Name = "SmoothImageBox";
            this.SmoothImageBox.Size = new System.Drawing.Size(268, 249);
            this.SmoothImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SmoothImageBox.TabIndex = 6;
            this.SmoothImageBox.TabStop = false;
            // 
            // ThresholdImageBox
            // 
            this.ThresholdImageBox.Location = new System.Drawing.Point(12, 278);
            this.ThresholdImageBox.Name = "ThresholdImageBox";
            this.ThresholdImageBox.Size = new System.Drawing.Size(268, 249);
            this.ThresholdImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ThresholdImageBox.TabIndex = 7;
            this.ThresholdImageBox.TabStop = false;
            // 
            // ThresholdSpinBox
            // 
            this.ThresholdSpinBox.Location = new System.Drawing.Point(830, 47);
            this.ThresholdSpinBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ThresholdSpinBox.Name = "ThresholdSpinBox";
            this.ThresholdSpinBox.Size = new System.Drawing.Size(53, 20);
            this.ThresholdSpinBox.TabIndex = 9;
            this.ThresholdSpinBox.ThousandsSeparator = true;
            //this.ThresholdSpinBox.ValueChanged += new System.EventHandler(this.ThresholdSpinBox_ValueChanged);
            // 
            // MaxThresholdSpinBox
            // 
            this.MaxThresholdSpinBox.Location = new System.Drawing.Point(830, 19);
            this.MaxThresholdSpinBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.MaxThresholdSpinBox.Name = "MaxThresholdSpinBox";
            this.MaxThresholdSpinBox.Size = new System.Drawing.Size(53, 20);
            this.MaxThresholdSpinBox.TabIndex = 13;
            this.MaxThresholdSpinBox.ThousandsSeparator = true;
            //this.MaxThresholdSpinBox.ValueChanged += new System.EventHandler(this.MaxThresholdSpinBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(750, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "MaxThreshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(770, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Threshold";
            // 
            // LoggerTextBox
            // 
            this.LoggerTextBox.Location = new System.Drawing.Point(580, 143);
            this.LoggerTextBox.Name = "LoggerTextBox";
            this.LoggerTextBox.Size = new System.Drawing.Size(327, 400);
            this.LoggerTextBox.TabIndex = 16;
            this.LoggerTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(763, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "SmoothBlur";
            // 
            // SmoothBlurSpinBox
            // 
            this.SmoothBlurSpinBox.Location = new System.Drawing.Point(830, 76);
            this.SmoothBlurSpinBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.SmoothBlurSpinBox.Name = "SmoothBlurSpinBox";
            this.SmoothBlurSpinBox.Size = new System.Drawing.Size(53, 20);
            this.SmoothBlurSpinBox.TabIndex = 17;
            this.SmoothBlurSpinBox.ThousandsSeparator = true;
            //this.SmoothBlurSpinBox.ValueChanged += new System.EventHandler(this.SmoothBlurSpinBox_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 555);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SmoothBlurSpinBox);
            this.Controls.Add(this.LoggerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaxThresholdSpinBox);
            this.Controls.Add(this.ThresholdSpinBox);
            this.Controls.Add(this.ThresholdImageBox);
            this.Controls.Add(this.SmoothImageBox);
            this.Controls.Add(this.CannyImageBox);
            this.Controls.Add(this.StratButton);
            this.Controls.Add(this.OriginalImageBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CannyImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SmoothImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdSpinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxThresholdSpinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SmoothBlurSpinBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox OriginalImageBox;
        private System.Windows.Forms.Button StratButton;
        private Emgu.CV.UI.ImageBox CannyImageBox;
        private Emgu.CV.UI.ImageBox SmoothImageBox;
        private Emgu.CV.UI.ImageBox ThresholdImageBox;
        private System.Windows.Forms.NumericUpDown ThresholdSpinBox;
        private System.Windows.Forms.NumericUpDown MaxThresholdSpinBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox LoggerTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown SmoothBlurSpinBox;
    }
}

