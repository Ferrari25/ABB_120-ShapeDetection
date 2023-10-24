using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using Emgu.Util;

namespace EmguCvProyect
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Exit = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // Exit
            // 
            Exit.BackColor = Color.IndianRed;
            Exit.Location = new Point(1536, 749);
            Exit.Name = "Exit";
            Exit.Size = new Size(90, 41);
            Exit.TabIndex = 0;
            Exit.Text = "Exit";
            Exit.UseVisualStyleBackColor = false;
            Exit.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(-1, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 373);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(605, 27);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(605, 373);
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(182, 9);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 7;
            label1.Text = "IMAGEN ORIGINAL";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(836, 9);
            label2.Name = "label2";
            label2.Size = new Size(182, 15);
            label2.TabIndex = 8;
            label2.Text = "ESCALA DE GRISES / GRAY SCALE";
            label2.Click += label2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(220, 435);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(798, 364);
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(493, 417);
            label3.Name = "label3";
            label3.Size = new Size(117, 15);
            label3.TabIndex = 10;
            label3.Text = "EDGE / CONTORNOS";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.Location = new Point(1412, 749);
            button1.Name = "button1";
            button1.Size = new Size(118, 41);
            button1.TabIndex = 13;
            button1.Text = "Capturar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += Boton_Capturar;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1651, 802);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(pictureBox3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(Exit);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button Exit;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox3;
        private Label label3;
        private Button button1;
    }
}