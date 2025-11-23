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
            lstAlunosConectados = new ListBox();
            imageListProcessos = new ImageList(components);
            btnGerenciarBloqueios = new Button();
            btnIniciarTelas = new Button();
            btnPararTelas = new Button();
            lstLog = new ListBox();
            flpStudentStreams = new FlowLayoutPanel();
            tlpPrincipal = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            tlpPrincipal.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // lstAlunosConectados
            // 
            lstAlunosConectados.Dock = DockStyle.Fill;
            lstAlunosConectados.FormattingEnabled = true;
            lstAlunosConectados.HorizontalScrollbar = true;
            lstAlunosConectados.ItemHeight = 15;
            lstAlunosConectados.Location = new Point(3, 19);
            lstAlunosConectados.Name = "lstAlunosConectados";
            lstAlunosConectados.Size = new Size(238, 500);
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
            btnGerenciarBloqueios.Dock = DockStyle.Top;
            btnGerenciarBloqueios.Location = new Point(3, 91);
            btnGerenciarBloqueios.Margin = new Padding(5);
            btnGerenciarBloqueios.Name = "btnGerenciarBloqueios";
            btnGerenciarBloqueios.Size = new Size(238, 36);
            btnGerenciarBloqueios.TabIndex = 15;
            btnGerenciarBloqueios.Text = "Gerenciar Bloqueios";
            btnGerenciarBloqueios.UseVisualStyleBackColor = true;
            btnGerenciarBloqueios.Click += btnGerenciarBloqueios_Click;
            // 
            // btnIniciarTelas
            // 
            btnIniciarTelas.Dock = DockStyle.Top;
            btnIniciarTelas.Location = new Point(3, 19);
            btnIniciarTelas.Margin = new Padding(5);
            btnIniciarTelas.Name = "btnIniciarTelas";
            btnIniciarTelas.Size = new Size(238, 36);
            btnIniciarTelas.TabIndex = 16;
            btnIniciarTelas.Text = "Iniciar Monitoramento";
            btnIniciarTelas.UseVisualStyleBackColor = true;
            btnIniciarTelas.Click += btnIniciarTelas_Click;
            // 
            // btnPararTelas
            // 
            btnPararTelas.Dock = DockStyle.Top;
            btnPararTelas.Location = new Point(3, 55);
            btnPararTelas.Margin = new Padding(5);
            btnPararTelas.Name = "btnPararTelas";
            btnPararTelas.Size = new Size(238, 36);
            btnPararTelas.TabIndex = 17;
            btnPararTelas.Text = "Parar Monitoramento";
            btnPararTelas.UseVisualStyleBackColor = true;
            btnPararTelas.Click += btnPararTelas_Click;
            // 
            // lstLog
            // 
            lstLog.Dock = DockStyle.Fill;
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(3, 19);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(1045, 116);
            lstLog.TabIndex = 22;
            // 
            // flpStudentStreams
            // 
            flpStudentStreams.AutoScroll = true;
            flpStudentStreams.BorderStyle = BorderStyle.FixedSingle;
            flpStudentStreams.Dock = DockStyle.Fill;
            flpStudentStreams.Location = new Point(253, 147);
            flpStudentStreams.Name = "flpStudentStreams";
            flpStudentStreams.Padding = new Padding(10);
            flpStudentStreams.Size = new Size(1051, 522);
            flpStudentStreams.TabIndex = 24;
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.ColumnCount = 2;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(groupBox1, 0, 0);
            tlpPrincipal.Controls.Add(groupBox2, 0, 1);
            tlpPrincipal.Controls.Add(flpStudentStreams, 1, 1);
            tlpPrincipal.Controls.Add(groupBox3, 1, 0);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Location = new Point(0, 0);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.RowCount = 2;
            tlpPrincipal.RowStyles.Add(new RowStyle());
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.Size = new Size(1307, 672);
            tlpPrincipal.TabIndex = 25;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGerenciarBloqueios);
            groupBox1.Controls.Add(btnPararTelas);
            groupBox1.Controls.Add(btnIniciarTelas);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(244, 138);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Controles da Aula";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lstAlunosConectados);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 147);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(244, 522);
            groupBox2.TabIndex = 26;
            groupBox2.TabStop = false;
            groupBox2.Text = "Alunos Online";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lstLog);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(253, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1051, 138);
            groupBox3.TabIndex = 27;
            groupBox3.TabStop = false;
            groupBox3.Text = "Log do Sistema";
            // 
            // FormProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 672);
            Controls.Add(tlpPrincipal);
            Name = "FormProfessor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Monitoramento de Laboratório - Professor";
            tlpPrincipal.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ListBox lstAlunosConectados;
        private ImageList imageListProcessos;
        private ImageList imageListProcessosBloq;
        private Button btnGerenciarBloqueios;
        private Button btnIniciarTelas;
        private Button btnPararTelas;
        private ListBox lstLog;
        private FlowLayoutPanel flpStudentStreams;
        private TableLayoutPanel tlpPrincipal;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
    }
}
