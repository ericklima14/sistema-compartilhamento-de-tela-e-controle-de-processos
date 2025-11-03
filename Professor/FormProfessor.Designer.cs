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
            components = new System.ComponentModel.Container();
            btnIniciarServidor = new Button();
            lstLog = new ListBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            lblMensagens = new Label();
            label1 = new Label();
            lstAlunosConectados = new ListBox();
            lblEnviarMsg = new Label();
            btnListarProcessos = new Button();
            lblProcessosAluno = new Label();
            lvProcessos = new ListView();
            headerPrograma = new ColumnHeader();
            imageListProcessos = new ImageList(components);
            btnMatarProcesso = new Button();
            btnGerenciarBloqueios = new Button();
            btnStartStream = new Button();
            btnStopStream = new Button();
            txtIpReciever = new TextBox();
            lblTransmissaoAula = new Label();
            SuspendLayout();
            // 
            // btnIniciarServidor
            // 
            btnIniciarServidor.Location = new Point(83, 198);
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
            lstLog.Location = new Point(64, 74);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(159, 109);
            lstLog.TabIndex = 1;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(328, 97);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(174, 23);
            txtMensagem.TabIndex = 2;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(345, 150);
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
            lblMensagens.Location = new Point(83, 41);
            lblMensagens.Name = "lblMensagens";
            lblMensagens.Size = new Size(114, 15);
            lblMensagens.TabIndex = 4;
            lblMensagens.Text = "Caixa de Mensagens";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(946, 24);
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
            lstAlunosConectados.Location = new Point(913, 57);
            lstAlunosConectados.Name = "lstAlunosConectados";
            lstAlunosConectados.Size = new Size(180, 154);
            lstAlunosConectados.TabIndex = 6;
            lstAlunosConectados.SelectedIndexChanged += lstAlunosConectados_SelectedIndexChanged;
            lstAlunosConectados.Leave += lstAlunosConectados_Leave;
            // 
            // lblEnviarMsg
            // 
            lblEnviarMsg.AutoSize = true;
            lblEnviarMsg.Location = new Point(363, 65);
            lblEnviarMsg.Name = "lblEnviarMsg";
            lblEnviarMsg.Size = new Size(101, 15);
            lblEnviarMsg.TabIndex = 7;
            lblEnviarMsg.Text = "Enviar Mensagem";
            // 
            // btnListarProcessos
            // 
            btnListarProcessos.Location = new Point(935, 229);
            btnListarProcessos.Name = "btnListarProcessos";
            btnListarProcessos.Size = new Size(140, 41);
            btnListarProcessos.TabIndex = 8;
            btnListarProcessos.Text = "Listar Processos";
            btnListarProcessos.UseVisualStyleBackColor = true;
            btnListarProcessos.Visible = false;
            btnListarProcessos.Click += btnListarProcessos_Click;
            // 
            // lblProcessosAluno
            // 
            lblProcessosAluno.AutoSize = true;
            lblProcessosAluno.Location = new Point(941, 297);
            lblProcessosAluno.Name = "lblProcessosAluno";
            lblProcessosAluno.Size = new Size(112, 15);
            lblProcessosAluno.TabIndex = 9;
            lblProcessosAluno.Text = "Processos do aluno:";
            lblProcessosAluno.Visible = false;
            // 
            // lvProcessos
            // 
            lvProcessos.CheckBoxes = true;
            lvProcessos.Columns.AddRange(new ColumnHeader[] { headerPrograma });
            lvProcessos.FullRowSelect = true;
            lvProcessos.Location = new Point(897, 321);
            lvProcessos.Name = "lvProcessos";
            lvProcessos.Size = new Size(210, 178);
            lvProcessos.SmallImageList = imageListProcessos;
            lvProcessos.TabIndex = 10;
            lvProcessos.UseCompatibleStateImageBehavior = false;
            lvProcessos.View = View.Details;
            // 
            // headerPrograma
            // 
            headerPrograma.Text = "Programa";
            headerPrograma.Width = 250;
            // 
            // imageListProcessos
            // 
            imageListProcessos.ColorDepth = ColorDepth.Depth32Bit;
            imageListProcessos.ImageSize = new Size(16, 16);
            imageListProcessos.TransparentColor = Color.Transparent;
            // 
            // btnMatarProcesso
            // 
            btnMatarProcesso.Location = new Point(931, 516);
            btnMatarProcesso.Name = "btnMatarProcesso";
            btnMatarProcesso.Size = new Size(140, 41);
            btnMatarProcesso.TabIndex = 11;
            btnMatarProcesso.Text = "Matar Processo";
            btnMatarProcesso.UseVisualStyleBackColor = true;
            btnMatarProcesso.Click += btnMatarProcesso_Click;
            // 
            // btnGerenciarBloqueios
            // 
            btnGerenciarBloqueios.Location = new Point(330, 297);
            btnGerenciarBloqueios.Name = "btnGerenciarBloqueios";
            btnGerenciarBloqueios.Size = new Size(172, 51);
            btnGerenciarBloqueios.TabIndex = 15;
            btnGerenciarBloqueios.Text = "Gerenciar Bloqueios";
            btnGerenciarBloqueios.UseVisualStyleBackColor = true;
            btnGerenciarBloqueios.Click += btnGerenciarBloqueios_Click;
            // 
            // btnStartStream
            // 
            btnStartStream.Location = new Point(64, 409);
            btnStartStream.Name = "btnStartStream";
            btnStartStream.Size = new Size(159, 36);
            btnStartStream.TabIndex = 16;
            btnStartStream.Text = "Iniciar Transmissão";
            btnStartStream.UseVisualStyleBackColor = true;
            btnStartStream.Click += btnStartStream_Click;
            // 
            // btnStopStream
            // 
            btnStopStream.Location = new Point(64, 451);
            btnStopStream.Name = "btnStopStream";
            btnStopStream.Size = new Size(159, 36);
            btnStopStream.TabIndex = 17;
            btnStopStream.Text = "Parar Transmissão";
            btnStopStream.UseVisualStyleBackColor = true;
            btnStopStream.Click += btnStopStream_Click;
            // 
            // txtIpReciever
            // 
            txtIpReciever.Location = new Point(64, 380);
            txtIpReciever.Name = "txtIpReciever";
            txtIpReciever.Size = new Size(159, 23);
            txtIpReciever.TabIndex = 18;
            txtIpReciever.Text = "127.0.0.1";
            // 
            // lblTransmissaoAula
            // 
            lblTransmissaoAula.AutoSize = true;
            lblTransmissaoAula.Location = new Point(64, 351);
            lblTransmissaoAula.Name = "lblTransmissaoAula";
            lblTransmissaoAula.Size = new Size(149, 15);
            lblTransmissaoAula.TabIndex = 19;
            lblTransmissaoAula.Text = "Transmissão de Aula (beta)";
            // 
            // FormProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 574);
            Controls.Add(lblTransmissaoAula);
            Controls.Add(txtIpReciever);
            Controls.Add(btnStopStream);
            Controls.Add(btnStartStream);
            Controls.Add(btnGerenciarBloqueios);
            Controls.Add(btnMatarProcesso);
            Controls.Add(lvProcessos);
            Controls.Add(lblProcessosAluno);
            Controls.Add(btnListarProcessos);
            Controls.Add(lblEnviarMsg);
            Controls.Add(lstAlunosConectados);
            Controls.Add(label1);
            Controls.Add(lblMensagens);
            Controls.Add(btnEnviar);
            Controls.Add(txtMensagem);
            Controls.Add(lstLog);
            Controls.Add(btnIniciarServidor);
            Name = "FormProfessor";
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
        private Button btnListarProcessos;
        private Label lblProcessosAluno;
        private ListView lvProcessos;
        private ColumnHeader headerPrograma;
        private ImageList imageListProcessos;
        private Button btnMatarProcesso;
        private ImageList imageListProcessosBloq;
        private Button btnGerenciarBloqueios;
        private Button btnStartStream;
        private Button btnStopStream;
        private TextBox txtIpReciever;
        private Label lblTransmissaoAula;
    }
}
