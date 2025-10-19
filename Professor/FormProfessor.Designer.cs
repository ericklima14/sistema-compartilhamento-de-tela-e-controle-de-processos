namespace Professor
{
    partial class FormProfessor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartStream = new Button();
            btnStopStream = new Button();
            SuspendLayout();
            // 
            // btnStartStream
            // 
            btnStartStream.ImageAlign = ContentAlignment.BottomLeft;
            btnStartStream.Location = new Point(166, 193);
            btnStartStream.Name = "btnStartStream";
            btnStartStream.Size = new Size(116, 23);
            btnStartStream.TabIndex = 0;
            btnStartStream.Text = "Iniciar Transmissão";
            btnStartStream.UseVisualStyleBackColor = true;
            btnStartStream.Click += btnStartStream_Click;
            // 
            // btnStopStream
            // 
            btnStopStream.Location = new Point(477, 193);
            btnStopStream.Name = "btnStopStream";
            btnStopStream.Size = new Size(117, 23);
            btnStopStream.TabIndex = 1;
            btnStopStream.Text = "Parar Transmissão";
            btnStopStream.UseVisualStyleBackColor = true;
            btnStopStream.Click += btnStopStream_Click;
            // 
            // FormProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnStopStream);
            Controls.Add(btnStartStream);
            Name = "FormProfessor";
            Text = "Form1";
            FormClosing += FormProfessor_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button btnStartStream;
        private Button btnStopStream;
    }
}
