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
            label1 = new Label();
            lstAlunosConectados = new ListBox();
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
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            videoView2 = new LibVLCSharp.WinForms.VideoView();
            lblMensagens = new Label();
            lstLog = new ListBox();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)videoView2).BeginInit();
            SuspendLayout();
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
            btnGerenciarBloqueios.Location = new Point(362, 495);
            btnGerenciarBloqueios.Name = "btnGerenciarBloqueios";
            btnGerenciarBloqueios.Size = new Size(172, 51);
            btnGerenciarBloqueios.TabIndex = 15;
            btnGerenciarBloqueios.Text = "Gerenciar Bloqueios";
            btnGerenciarBloqueios.UseVisualStyleBackColor = true;
            btnGerenciarBloqueios.Click += btnGerenciarBloqueios_Click;
            // 
            // btnStartStream
            // 
            btnStartStream.Location = new Point(385, 82);
            btnStartStream.Name = "btnStartStream";
            btnStartStream.Size = new Size(159, 36);
            btnStartStream.TabIndex = 16;
            btnStartStream.Text = "Iniciar Transmissão";
            btnStartStream.UseVisualStyleBackColor = true;
            // 
            // btnStopStream
            // 
            btnStopStream.Location = new Point(385, 124);
            btnStopStream.Name = "btnStopStream";
            btnStopStream.Size = new Size(159, 36);
            btnStopStream.TabIndex = 17;
            btnStopStream.Text = "Parar Transmissão";
            btnStopStream.UseVisualStyleBackColor = true;
            // 
            // txtIpReciever
            // 
            txtIpReciever.Location = new Point(385, 53);
            txtIpReciever.Name = "txtIpReciever";
            txtIpReciever.Size = new Size(159, 23);
            txtIpReciever.TabIndex = 18;
            txtIpReciever.Text = "127.0.0.1";
            // 
            // lblTransmissaoAula
            // 
            lblTransmissaoAula.AutoSize = true;
            lblTransmissaoAula.Location = new Point(385, 24);
            lblTransmissaoAula.Name = "lblTransmissaoAula";
            lblTransmissaoAula.Size = new Size(149, 15);
            lblTransmissaoAula.TabIndex = 19;
            lblTransmissaoAula.Text = "Transmissão de Aula (beta)";
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Location = new Point(237, 247);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(177, 124);
            videoView1.TabIndex = 20;
            videoView1.Text = "videoView1";
            // 
            // videoView2
            // 
            videoView2.BackColor = Color.Black;
            videoView2.Location = new Point(484, 247);
            videoView2.MediaPlayer = null;
            videoView2.Name = "videoView2";
            videoView2.Size = new Size(177, 124);
            videoView2.TabIndex = 21;
            videoView2.Text = "videoView2";
            // 
            // lblMensagens
            // 
            lblMensagens.AutoSize = true;
            lblMensagens.Location = new Point(732, 24);
            lblMensagens.Name = "lblMensagens";
            lblMensagens.Size = new Size(114, 15);
            lblMensagens.TabIndex = 23;
            lblMensagens.Text = "Caixa de Mensagens";
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(713, 57);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(159, 109);
            lstLog.TabIndex = 22;
            // 
            // FormProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 574);
            Controls.Add(lblMensagens);
            Controls.Add(lstLog);
            Controls.Add(videoView2);
            Controls.Add(videoView1);
            Controls.Add(lblTransmissaoAula);
            Controls.Add(txtIpReciever);
            Controls.Add(btnStopStream);
            Controls.Add(btnStartStream);
            Controls.Add(btnGerenciarBloqueios);
            Controls.Add(btnMatarProcesso);
            Controls.Add(lvProcessos);
            Controls.Add(lblProcessosAluno);
            Controls.Add(btnListarProcessos);
            Controls.Add(lstAlunosConectados);
            Controls.Add(label1);
            Name = "FormProfessor";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)videoView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ListBox lstAlunosConectados;
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
        private LibVLCSharp.WinForms.VideoView videoView1;
        private LibVLCSharp.WinForms.VideoView videoView2;
        private Label lblMensagens;
        private ListBox lstLog;
    }
}
