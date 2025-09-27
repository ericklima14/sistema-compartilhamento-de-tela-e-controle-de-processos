namespace Professor
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
            lblMensagens = new Label();
            label1 = new Label();
            lstAlunosConectados = new ListBox();
            lblEnviarMsg = new Label();
            SuspendLayout();
            // 
            // btnIniciarServidor
            // 
            btnIniciarServidor.Location = new Point(83, 249);
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
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(64, 125);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(159, 109);
            lstLog.TabIndex = 1;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(317, 173);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(174, 23);
            txtMensagem.TabIndex = 2;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(334, 226);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(143, 33);
            btnEnviar.TabIndex = 3;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // lblMensagens
            // 
            lblMensagens.AutoSize = true;
            lblMensagens.Location = new Point(83, 92);
            lblMensagens.Name = "lblMensagens";
            lblMensagens.Size = new Size(114, 15);
            lblMensagens.TabIndex = 4;
            lblMensagens.Text = "Caixa de Mensagens";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(604, 92);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 5;
            label1.Text = "Alunos Conectados";
            // 
            // lstAlunosConectados
            // 
            lstAlunosConectados.FormattingEnabled = true;
            lstAlunosConectados.HorizontalScrollbar = true;
            lstAlunosConectados.ItemHeight = 15;
            lstAlunosConectados.Location = new Point(582, 125);
            lstAlunosConectados.Name = "lstAlunosConectados";
            lstAlunosConectados.Size = new Size(163, 109);
            lstAlunosConectados.TabIndex = 6;
            // 
            // lblEnviarMsg
            // 
            lblEnviarMsg.AutoSize = true;
            lblEnviarMsg.Location = new Point(352, 141);
            lblEnviarMsg.Name = "lblEnviarMsg";
            lblEnviarMsg.Size = new Size(101, 15);
            lblEnviarMsg.TabIndex = 7;
            lblEnviarMsg.Text = "Enviar Mensagem";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblEnviarMsg);
            Controls.Add(lstAlunosConectados);
            Controls.Add(label1);
            Controls.Add(lblMensagens);
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
        private Label lblMensagens;
        private Label label1;
        private ListBox lstAlunosConectados;
        private Label lblEnviarMsg;
    }
}
