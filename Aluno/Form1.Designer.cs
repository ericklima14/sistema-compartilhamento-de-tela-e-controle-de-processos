namespace Aluno
{
    partial class Form1
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
            label1 = new Label();
            txtIpProfessor = new TextBox();
            btnConectar = new Button();
            lstBox = new ListBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(100, 81);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "IP do Professor";
            // 
            // txtIpProfessor
            // 
            txtIpProfessor.Location = new Point(100, 109);
            txtIpProfessor.Name = "txtIpProfessor";
            txtIpProfessor.Size = new Size(120, 23);
            txtIpProfessor.TabIndex = 1;
            txtIpProfessor.Text = "127.0.0.1";
            // 
            // btnConectar
            // 
            btnConectar.Location = new Point(100, 153);
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
            lstBox.Location = new Point(325, 109);
            lstBox.Name = "lstBox";
            lstBox.Size = new Size(120, 94);
            lstBox.TabIndex = 3;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(325, 235);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(120, 23);
            txtMensagem.TabIndex = 4;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(340, 277);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(95, 30);
            btnEnviar.TabIndex = 5;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 470);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(lstBox);
            Controls.Add(btnConectar);
            Controls.Add(txtIpProfessor);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
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
    }
}
