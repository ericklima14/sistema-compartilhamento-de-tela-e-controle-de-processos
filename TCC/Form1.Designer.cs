namespace TCC
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
            btnIniciarServidor = new Button();
            lstLog = new ListBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            SuspendLayout();
            // 
            // btnIniciarServidor
            // 
            btnIniciarServidor.Location = new Point(124, 249);
            btnIniciarServidor.Name = "btnIniciarServidor";
            btnIniciarServidor.Size = new Size(120, 45);
            btnIniciarServidor.TabIndex = 0;
            btnIniciarServidor.Text = "Iniciar Servidor";
            btnIniciarServidor.UseVisualStyleBackColor = true;
            btnIniciarServidor.Click += btnIniciarServidor_Click;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(124, 125);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(120, 94);
            lstLog.TabIndex = 1;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(387, 173);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(174, 23);
            txtMensagem.TabIndex = 2;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(407, 233);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(143, 33);
            btnEnviar.TabIndex = 3;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(lstLog);
            Controls.Add(btnIniciarServidor);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIniciarServidor;
        private ListBox lstLog;
        private TextBox txtMensagem;
        private Button btnEnviar;
    }
}
