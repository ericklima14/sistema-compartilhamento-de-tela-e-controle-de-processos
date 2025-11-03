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
            SuspendLayout();
            // 
            // lblEnviarMsg
            // 
            lblEnviarMsg.AutoSize = true;
            lblEnviarMsg.Location = new Point(766, 319);
            lblEnviarMsg.Name = "lblEnviarMsg";
            lblEnviarMsg.Size = new Size(101, 15);
            lblEnviarMsg.TabIndex = 12;
            lblEnviarMsg.Text = "Enviar Mensagem";
            lblEnviarMsg.Click += lblEnviarMsg_Click;
            // 
            // lblMensagens
            // 
            lblMensagens.AutoSize = true;
            lblMensagens.Location = new Point(753, 31);
            lblMensagens.Name = "lblMensagens";
            lblMensagens.Size = new Size(114, 15);
            lblMensagens.TabIndex = 11;
            lblMensagens.Text = "Caixa de Mensagens";
            lblMensagens.Click += lblMensagens_Click;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(750, 401);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(143, 33);
            btnEnviar.TabIndex = 10;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(721, 352);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(194, 23);
            txtMensagem.TabIndex = 9;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(734, 64);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(159, 109);
            lstLog.TabIndex = 8;
            // 
            // FormApresentacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 537);
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
    }
}