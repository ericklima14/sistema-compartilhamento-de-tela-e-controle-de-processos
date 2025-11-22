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
            imageListProcessos = new ImageList(components);
            btnGerenciarBloqueios = new Button();
            btnIniciarTelas = new Button();
            btnPararTelas = new Button();
            lblMonitorarTelas = new Label();
            lblMensagens = new Label();
            lstLog = new ListBox();
            flpStudentStreams = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1134, 26);
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
            lstAlunosConectados.Location = new Point(1101, 59);
            lstAlunosConectados.Name = "lstAlunosConectados";
            lstAlunosConectados.Size = new Size(180, 154);
            lstAlunosConectados.TabIndex = 6;
            lstAlunosConectados.DoubleClick += lstAlunosConectados_DoubleClick;
            // 
            // imageListProcessos
            // 
            imageListProcessos.ColorDepth = ColorDepth.Depth32Bit;
            imageListProcessos.ImageSize = new Size(16, 16);
            imageListProcessos.TransparentColor = Color.Transparent;
            // 
            // btnGerenciarBloqueios
            // 
            btnGerenciarBloqueios.Location = new Point(851, 391);
            btnGerenciarBloqueios.Name = "btnGerenciarBloqueios";
            btnGerenciarBloqueios.Size = new Size(172, 51);
            btnGerenciarBloqueios.TabIndex = 15;
            btnGerenciarBloqueios.Text = "Gerenciar Bloqueios";
            btnGerenciarBloqueios.UseVisualStyleBackColor = true;
            btnGerenciarBloqueios.Click += btnGerenciarBloqueios_Click;
            // 
            // btnIniciarTelas
            // 
            btnIniciarTelas.Location = new Point(41, 50);
            btnIniciarTelas.Name = "btnIniciarTelas";
            btnIniciarTelas.Size = new Size(159, 36);
            btnIniciarTelas.TabIndex = 16;
            btnIniciarTelas.Text = "Iniciar Monitoramento";
            btnIniciarTelas.UseVisualStyleBackColor = true;
            btnIniciarTelas.Click += btnIniciarTelas_Click;
            // 
            // btnPararTelas
            // 
            btnPararTelas.Location = new Point(41, 92);
            btnPararTelas.Name = "btnPararTelas";
            btnPararTelas.Size = new Size(159, 36);
            btnPararTelas.TabIndex = 17;
            btnPararTelas.Text = "Parar Monitoramento";
            btnPararTelas.UseVisualStyleBackColor = true;
            btnPararTelas.Click += btnPararTelas_Click;
            // 
            // lblMonitorarTelas
            // 
            lblMonitorarTelas.AutoSize = true;
            lblMonitorarTelas.Location = new Point(41, 32);
            lblMonitorarTelas.Name = "lblMonitorarTelas";
            lblMonitorarTelas.Size = new Size(89, 15);
            lblMonitorarTelas.TabIndex = 19;
            lblMonitorarTelas.Text = "Monitorar Telas";
            // 
            // lblMensagens
            // 
            lblMensagens.AutoSize = true;
            lblMensagens.Location = new Point(877, 26);
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
            lstLog.Location = new Point(792, 59);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(268, 154);
            lstLog.TabIndex = 22;
            // 
            // flpStudentStreams
            // 
            flpStudentStreams.AutoScroll = true;
            flpStudentStreams.BorderStyle = BorderStyle.FixedSingle;
            flpStudentStreams.Location = new Point(26, 154);
            flpStudentStreams.Name = "flpStudentStreams";
            flpStudentStreams.Padding = new Padding(10);
            flpStudentStreams.Size = new Size(754, 493);
            flpStudentStreams.TabIndex = 24;
            // 
            // FormProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 672);
            Controls.Add(flpStudentStreams);
            Controls.Add(lblMensagens);
            Controls.Add(lstLog);
            Controls.Add(lblMonitorarTelas);
            Controls.Add(btnPararTelas);
            Controls.Add(btnIniciarTelas);
            Controls.Add(btnGerenciarBloqueios);
            Controls.Add(lstAlunosConectados);
            Controls.Add(label1);
            Name = "FormProfessor";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ListBox lstAlunosConectados;
        private ImageList imageListProcessos;
        private ImageList imageListProcessosBloq;
        private Button btnGerenciarBloqueios;
        private Button btnIniciarTelas;
        private Button btnPararTelas;
        private Label lblMonitorarTelas;
        private Label lblMensagens;
        private ListBox lstLog;
        private FlowLayoutPanel flpStudentStreams;
    }
}
