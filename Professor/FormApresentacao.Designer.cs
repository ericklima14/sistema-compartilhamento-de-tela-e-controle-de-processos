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
            btnEnviar = new Button();
            txtMensagem = new TextBox();
            lstLog = new ListBox();
            txtIpTransmissao = new TextBox();
            btnStopStream = new Button();
            btnStartStream = new Button();
            txtBoxPreset = new TextBox();
            lblPreset = new Label();
            lblBitRate = new Label();
            txtBoxBitRate = new TextBox();
            lblFrameRate = new Label();
            txtBoxFrameRate = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(436, 254);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(126, 23);
            btnEnviar.TabIndex = 10;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(3, 254);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(427, 23);
            txtMensagem.TabIndex = 9;
            // 
            // lstLog
            // 
            lstLog.Dock = DockStyle.Top;
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(3, 19);
            lstLog.Name = "lstLog";
            lstLog.RightToLeft = RightToLeft.No;
            lstLog.Size = new Size(559, 229);
            lstLog.TabIndex = 8;
            // 
            // txtIpTransmissao
            // 
            txtIpTransmissao.Dock = DockStyle.Top;
            txtIpTransmissao.Location = new Point(3, 19);
            txtIpTransmissao.Name = "txtIpTransmissao";
            txtIpTransmissao.Size = new Size(142, 23);
            txtIpTransmissao.TabIndex = 22;
            txtIpTransmissao.Text = "239.0.0.1";
            // 
            // btnStopStream
            // 
            btnStopStream.Dock = DockStyle.Top;
            btnStopStream.Location = new Point(3, 78);
            btnStopStream.Name = "btnStopStream";
            btnStopStream.Size = new Size(142, 36);
            btnStopStream.TabIndex = 21;
            btnStopStream.Text = "Parar Transmissão";
            btnStopStream.UseVisualStyleBackColor = true;
            btnStopStream.Click += btnStopStream_Click;
            // 
            // btnStartStream
            // 
            btnStartStream.Dock = DockStyle.Top;
            btnStartStream.Location = new Point(3, 42);
            btnStartStream.Name = "btnStartStream";
            btnStartStream.Size = new Size(142, 36);
            btnStartStream.TabIndex = 20;
            btnStartStream.Text = "Iniciar Transmissão";
            btnStartStream.UseVisualStyleBackColor = true;
            btnStartStream.Click += btnStartStream_Click;
            // 
            // txtBoxPreset
            // 
            txtBoxPreset.Dock = DockStyle.Top;
            txtBoxPreset.Location = new Point(3, 34);
            txtBoxPreset.Name = "txtBoxPreset";
            txtBoxPreset.Size = new Size(142, 23);
            txtBoxPreset.TabIndex = 24;
            txtBoxPreset.Text = "1";
            // 
            // lblPreset
            // 
            lblPreset.AutoSize = true;
            lblPreset.Dock = DockStyle.Top;
            lblPreset.Location = new Point(3, 19);
            lblPreset.Name = "lblPreset";
            lblPreset.Size = new Size(39, 15);
            lblPreset.TabIndex = 25;
            lblPreset.Text = "Preset";
            // 
            // lblBitRate
            // 
            lblBitRate.AutoSize = true;
            lblBitRate.Dock = DockStyle.Top;
            lblBitRate.Location = new Point(3, 95);
            lblBitRate.Name = "lblBitRate";
            lblBitRate.Size = new Size(47, 15);
            lblBitRate.TabIndex = 27;
            lblBitRate.Text = "Bit Rate";
            // 
            // txtBoxBitRate
            // 
            txtBoxBitRate.Dock = DockStyle.Top;
            txtBoxBitRate.Location = new Point(3, 110);
            txtBoxBitRate.Name = "txtBoxBitRate";
            txtBoxBitRate.Size = new Size(142, 23);
            txtBoxBitRate.TabIndex = 26;
            txtBoxBitRate.Text = "2000";
            // 
            // lblFrameRate
            // 
            lblFrameRate.AutoSize = true;
            lblFrameRate.Dock = DockStyle.Top;
            lblFrameRate.Location = new Point(3, 57);
            lblFrameRate.Name = "lblFrameRate";
            lblFrameRate.Size = new Size(66, 15);
            lblFrameRate.TabIndex = 29;
            lblFrameRate.Text = "Frame Rate";
            // 
            // txtBoxFrameRate
            // 
            txtBoxFrameRate.Dock = DockStyle.Top;
            txtBoxFrameRate.Location = new Point(3, 72);
            txtBoxFrameRate.Name = "txtBoxFrameRate";
            txtBoxFrameRate.Size = new Size(142, 23);
            txtBoxFrameRate.TabIndex = 28;
            txtBoxFrameRate.Text = "5";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnStopStream);
            groupBox1.Controls.Add(btnStartStream);
            groupBox1.Controls.Add(txtIpTransmissao);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(148, 127);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;
            groupBox1.Text = "Transmissão de Aula";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtBoxBitRate);
            groupBox2.Controls.Add(lblBitRate);
            groupBox2.Controls.Add(txtBoxFrameRate);
            groupBox2.Controls.Add(lblFrameRate);
            groupBox2.Controls.Add(txtBoxPreset);
            groupBox2.Controls.Add(lblPreset);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 136);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(148, 143);
            groupBox2.TabIndex = 32;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parâmetros";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lstLog);
            groupBox3.Controls.Add(btnEnviar);
            groupBox3.Controls.Add(txtMensagem);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(163, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(565, 282);
            groupBox3.TabIndex = 33;
            groupBox3.TabStop = false;
            groupBox3.Text = "Caixa de Mensagens";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(groupBox3, 1, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(731, 288);
            tableLayoutPanel2.TabIndex = 31;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel3.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 47.36842F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 52.63158F));
            tableLayoutPanel3.Size = new Size(154, 282);
            tableLayoutPanel3.TabIndex = 32;
            // 
            // FormApresentacao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 288);
            Controls.Add(tableLayoutPanel2);
            Name = "FormApresentacao";
            Text = "FormApresentacao";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnEnviar;
        private TextBox txtMensagem;
        private ListBox lstLog;
        private TextBox txtIpTransmissao;
        private Button btnStopStream;
        private Button btnStartStream;
        private TextBox txtBoxPreset;
        private Label lblPreset;
        private Label lblBitRate;
        private TextBox txtBoxBitRate;
        private Label lblFrameRate;
        private TextBox txtBoxFrameRate;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
    }
}