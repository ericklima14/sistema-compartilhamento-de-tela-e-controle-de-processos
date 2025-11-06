namespace Aluno
{
    partial class FormAluno
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            txtIpProfessor = new TextBox();
            btnConectar = new Button();
            lstBox = new ListBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            processTimer = new System.Windows.Forms.Timer(components);
            videoView = new LibVLCSharp.WinForms.VideoView();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 41);
            label1.Name = "label1";
            label1.Size = new Size(118, 15);
            label1.TabIndex = 0;
            label1.Text = "IP do Professor (TCP)";
            // 
            // txtIpProfessor
            // 
            txtIpProfessor.Location = new Point(39, 59);
            txtIpProfessor.Name = "txtIpProfessor";
            txtIpProfessor.Size = new Size(120, 23);
            txtIpProfessor.TabIndex = 1;
            txtIpProfessor.Text = "127.0.0.1";
            // 
            // btnConectar
            // 
            btnConectar.Location = new Point(39, 88);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(120, 34);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // lstBox
            // 
            lstBox.FormattingEnabled = true;
            lstBox.ItemHeight = 15;
            lstBox.Location = new Point(243, 26);
            lstBox.Name = "lstBox";
            lstBox.Size = new Size(376, 94);
            lstBox.TabIndex = 3;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(655, 48);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(120, 23);
            txtMensagem.TabIndex = 4;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(670, 90);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(95, 30);
            btnEnviar.TabIndex = 5;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // processTimer
            // 
            processTimer.Interval = 3000;
            processTimer.Tick += processTimer_Tick;
            // 
            // videoView
            // 
            videoView.BackColor = Color.Black;
            videoView.Location = new Point(259, 156);
            videoView.MediaPlayer = null;
            videoView.Name = "videoView";
            videoView.Size = new Size(487, 286);
            videoView.TabIndex = 7;
            videoView.Text = "videoView1";
            // 
            // FormAluno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 470);
            Controls.Add(videoView);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(lstBox);
            Controls.Add(btnConectar);
            Controls.Add(txtIpProfessor);
            Controls.Add(label1);
            Name = "FormAluno";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIpProfessor;
        private Button btnConectar;
        private ListBox lstBox;
        private TextBox txtMensagem;
        private Button btnEnviar;
        private System.Windows.Forms.Timer processTimer;
        private LibVLCSharp.WinForms.VideoView videoView;
    }
}
