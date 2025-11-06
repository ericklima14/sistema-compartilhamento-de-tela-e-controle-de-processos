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
            lblMensagens = new Label();
            btnEnviar = new Button();
            txtMensagem = new TextBox();
            lstLog = new ListBox();
            lblTransmissaoAula = new Label();
            txtIpTransmissao = new TextBox();
            btnStopStream = new Button();
            btnStartStream = new Button();
            SuspendLayout();
            // 
            // lblMensagens
            // 
            lblMensagens.AutoSize = true;
            lblMensagens.Location = new Point(241, 36);
            lblMensagens.Name = "lblMensagens";
            lblMensagens.Size = new Size(114, 15);
            lblMensagens.TabIndex = 11;
            lblMensagens.Text = "Caixa de Mensagens";
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(575, 169);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(126, 23);
            btnEnviar.TabIndex = 10;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(241, 169);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(328, 23);
            txtMensagem.TabIndex = 9;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(241, 54);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(460, 109);
            lstLog.TabIndex = 8;
            // 
            // lblTransmissaoAula
            // 
            lblTransmissaoAula.AutoSize = true;
            lblTransmissaoAula.Location = new Point(33, 39);
            lblTransmissaoAula.Name = "lblTransmissaoAula";
            lblTransmissaoAula.Size = new Size(149, 15);
            lblTransmissaoAula.TabIndex = 23;
            lblTransmissaoAula.Text = "Transmissão de Aula (beta)";
            // 
            // txtIpTransmissao
            // 
            txtIpTransmissao.Location = new Point(33, 68);
            txtIpTransmissao.Name = "txtIpTransmissao";
            txtIpTransmissao.Size = new Size(159, 23);
            txtIpTransmissao.TabIndex = 22;
            txtIpTransmissao.Text = "239.0.0.1";
            // 
            // btnStopStream
            // 
            btnStopStream.Location = new Point(33, 139);
            btnStopStream.Name = "btnStopStream";
            btnStopStream.Size = new Size(159, 36);
            btnStopStream.TabIndex = 21;
            btnStopStream.Text = "Parar Transmissão";
            btnStopStream.UseVisualStyleBackColor = true;
            btnStopStream.Click += btnStopStream_Click;
            // 
            // btnStartStream
            // 
            btnStartStream.Location = new Point(33, 97);
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
            ClientSize = new Size(731, 224);
            Controls.Add(lblTransmissaoAula);
            Controls.Add(txtIpTransmissao);
            Controls.Add(btnStopStream);
            Controls.Add(btnStartStream);
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
        private Label lblMensagens;
        private Button btnEnviar;
        private TextBox txtMensagem;
        private ListBox lstLog;
        private Label lblTransmissaoAula;
        private TextBox txtIpTransmissao;
        private Button btnStopStream;
        private Button btnStartStream;
    }
}