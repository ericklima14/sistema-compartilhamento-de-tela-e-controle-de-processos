namespace Professor
{
    partial class FormApresentacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblEnviarMsg = new Label();
            lblMensagens = new Label();
            btnEnviar = new Button();
            txtMensagem = new TextBox();
            lstLog = new ListBox();
            lblTransmissaoAula = new Label();
            txtIpReciever = new TextBox();
            btnStopStream = new Button();
            btnStartStream = new Button();
            SuspendLayout();
            // 
            // lblEnviarMsg
            // 
            lblEnviarMsg.AutoSize = true;
            lblEnviarMsg.Location = new Point(629, 246);
            lblEnviarMsg.Name = "lblEnviarMsg";
            lblEnviarMsg.Size = new Size(101, 15);
            lblEnviarMsg.TabIndex = 12;
            lblEnviarMsg.Text = "Enviar Mensagem";
            // 
            // lblMensagens
            // 
            lblMensagens.AutoSize = true;
            lblMensagens.Location = new Point(616, 37);
            lblMensagens.Name = "lblMensagens";
            lblMensagens.Size = new Size(114, 15);
            lblMensagens.TabIndex = 11;
            lblMensagens.Text = "Caixa de Mensagens";
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(600, 210);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(143, 33);
            btnEnviar.TabIndex = 10;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(503, 181);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(330, 23);
            txtMensagem.TabIndex = 9;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(503, 66);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(330, 109);
            lstLog.TabIndex = 8;
            // 
            // lblTransmissaoAula
            // 
            lblTransmissaoAula.AutoSize = true;
            lblTransmissaoAula.Location = new Point(130, 67);
            lblTransmissaoAula.Name = "lblTransmissaoAula";
            lblTransmissaoAula.Size = new Size(149, 15);
            lblTransmissaoAula.TabIndex = 23;
            lblTransmissaoAula.Text = "Transmissão de Aula (beta)";
            // 
            // txtIpReciever
            // 
            txtIpReciever.Location = new Point(130, 96);
            txtIpReciever.Name = "txtIpReciever";
            txtIpReciever.Size = new Size(159, 23);
            txtIpReciever.TabIndex = 22;
            txtIpReciever.Text = "127.0.0.1";
            // 
            // btnStopStream
            // 
            btnStopStream.Location = new Point(130, 167);
            btnStopStream.Name = "btnStopStream";
            btnStopStream.Size = new Size(159, 36);
            btnStopStream.TabIndex = 21;
            btnStopStream.Text = "Parar Transmissão";
            btnStopStream.UseVisualStyleBackColor = true;
            btnStopStream.Click += btnStopStream_Click;
            // 
            // btnStartStream
            // 
            btnStartStream.Location = new Point(130, 125);
            btnStartStream.Name = "btnStartStream";
            btnStartStream.Size = new Size(159, 36);
            btnStartStream.TabIndex = 20;
            btnStartStream.Text = "Iniciar Transmissão";
            btnStartStream.UseVisualStyleBackColor = true;
            btnStartStream.Click += btnStartStream_Click;
            // 
            // FormApresentacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 322);
            Controls.Add(lblTransmissaoAula);
            Controls.Add(txtIpReciever);
            Controls.Add(btnStopStream);
            Controls.Add(btnStartStream);
            Controls.Add(lblEnviarMsg);
            Controls.Add(lblMensagens);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(lstLog);
            Name = "FormApresentacao";
            Text = "FormApresentacao";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnviarMsg;
        private Label lblMensagens;
        private Button btnEnviar;
        private TextBox txtMensagem;
        private ListBox lstLog;
        private Label lblTransmissaoAula;
        private TextBox txtIpReciever;
        private Button btnStopStream;
        private Button btnStartStream;
    }
}