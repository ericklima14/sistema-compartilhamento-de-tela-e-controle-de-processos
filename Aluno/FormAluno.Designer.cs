namespace Aluno
{
    partial class FormAluno
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
            txtIpProfessor = new TextBox();
            btnConectar = new Button();
            lstBox = new ListBox();
            txtMensagem = new TextBox();
            btnEnviar = new Button();
            processTimer = new System.Windows.Forms.Timer(components);
            videoView = new LibVLCSharp.WinForms.VideoView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // txtIpProfessor
            // 
            txtIpProfessor.Dock = DockStyle.Top;
            txtIpProfessor.Location = new Point(3, 19);
            txtIpProfessor.Name = "txtIpProfessor";
            txtIpProfessor.Size = new Size(108, 23);
            txtIpProfessor.TabIndex = 1;
            txtIpProfessor.Text = "127.0.0.1";
            // 
            // btnConectar
            // 
            btnConectar.Dock = DockStyle.Top;
            btnConectar.Location = new Point(3, 42);
            btnConectar.Margin = new Padding(5);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(108, 32);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // lstBox
            // 
            lstBox.Dock = DockStyle.Fill;
            lstBox.FormattingEnabled = true;
            lstBox.ItemHeight = 15;
            lstBox.Location = new Point(0, 0);
            lstBox.Margin = new Padding(0);
            lstBox.Name = "lstBox";
            lstBox.Size = new Size(440, 141);
            lstBox.TabIndex = 3;
            // 
            // txtMensagem
            // 
            txtMensagem.Dock = DockStyle.Fill;
            txtMensagem.Location = new Point(0, 0);
            txtMensagem.Margin = new Padding(0);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(340, 23);
            txtMensagem.TabIndex = 4;
            // 
            // btnEnviar
            // 
            btnEnviar.Dock = DockStyle.Top;
            btnEnviar.Location = new Point(340, 0);
            btnEnviar.Margin = new Padding(0);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(100, 23);
            btnEnviar.TabIndex = 5;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // processTimer
            // 
            processTimer.Interval = 3000;
            processTimer.Tick += processTimer_Tick;
            // 
            // videoView
            // 
            videoView.BackColor = Color.Black;
            videoView.Dock = DockStyle.Fill;
            videoView.Location = new Point(3, 173);
            videoView.MediaPlayer = null;
            videoView.Name = "videoView";
            videoView.Size = new Size(560, 315);
            videoView.TabIndex = 7;
            videoView.Text = "videoView1";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(videoView, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(566, 491);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(566, 170);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnConectar);
            groupBox1.Controls.Add(txtIpProfessor);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(114, 164);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "IP do Professor";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(lstBox, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(123, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
            tableLayoutPanel3.Size = new Size(440, 164);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel4.Controls.Add(txtMensagem, 0, 0);
            tableLayoutPanel4.Controls.Add(btnEnviar, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 141);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(440, 23);
            tableLayoutPanel4.TabIndex = 4;
            // 
            // FormAluno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 491);
            Controls.Add(tableLayoutPanel1);
            Name = "FormAluno";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtIpProfessor;
        private Button btnConectar;
        private ListBox lstBox;
        private TextBox txtMensagem;
        private Button btnEnviar;
        private System.Windows.Forms.Timer processTimer;
        private LibVLCSharp.WinForms.VideoView videoView;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
    }
}
