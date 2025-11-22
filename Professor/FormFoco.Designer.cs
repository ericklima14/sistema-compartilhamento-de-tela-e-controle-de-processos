namespace Professor
{
    partial class FormFoco
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
            components = new System.ComponentModel.Container();
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            imageListProcessos = new ImageList(components);
            btnMatarProcesso = new Button();
            lvProcessos = new ListView();
            headerPrograma = new ColumnHeader();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Dock = DockStyle.Fill;
            videoView1.Location = new Point(3, 3);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(800, 450);
            videoView1.TabIndex = 0;
            videoView1.Text = "videoView1";
            // 
            // imageListProcessos
            // 
            imageListProcessos.ColorDepth = ColorDepth.Depth32Bit;
            imageListProcessos.ImageSize = new Size(16, 16);
            imageListProcessos.TransparentColor = Color.Transparent;
            // 
            // btnMatarProcesso
            // 
            btnMatarProcesso.Dock = DockStyle.Fill;
            btnMatarProcesso.Location = new Point(3, 381);
            btnMatarProcesso.Name = "btnMatarProcesso";
            btnMatarProcesso.Size = new Size(232, 44);
            btnMatarProcesso.TabIndex = 14;
            btnMatarProcesso.Text = "Matar Processo";
            btnMatarProcesso.UseVisualStyleBackColor = true;
            // 
            // lvProcessos
            // 
            lvProcessos.CheckBoxes = true;
            lvProcessos.Columns.AddRange(new ColumnHeader[] { headerPrograma });
            lvProcessos.Dock = DockStyle.Fill;
            lvProcessos.FullRowSelect = true;
            lvProcessos.Location = new Point(3, 3);
            lvProcessos.Name = "lvProcessos";
            lvProcessos.Size = new Size(232, 372);
            lvProcessos.SmallImageList = imageListProcessos;
            lvProcessos.TabIndex = 13;
            lvProcessos.UseCompatibleStateImageBehavior = false;
            lvProcessos.View = View.Details;
            // 
            // headerPrograma
            // 
            headerPrograma.Text = "Programa";
            headerPrograma.Width = 250;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel1.Controls.Add(groupBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(videoView1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1056, 456);
            tableLayoutPanel1.TabIndex = 15;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(809, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(244, 450);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Processos do Aluno";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(lvProcessos, 0, 0);
            tableLayoutPanel2.Controls.Add(btnMatarProcesso, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.Size = new Size(238, 428);
            tableLayoutPanel2.TabIndex = 15;
            // 
            // FormFoco
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1056, 456);
            Controls.Add(tableLayoutPanel1);
            Name = "FormFoco";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormFoco";
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView videoView1;
        private ImageList imageListProcessos;
        private Button btnMatarProcesso;
        private ListView lvProcessos;
        private ColumnHeader headerPrograma;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}