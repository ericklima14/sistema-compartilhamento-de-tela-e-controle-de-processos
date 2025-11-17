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
            splitContainer1 = new SplitContainer();
            btnMatarProcesso = new Button();
            lvProcessos = new ListView();
            headerPrograma = new ColumnHeader();
            imageListProcessos = new ImageList(components);
            lblProcessosAluno = new Label();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Dock = DockStyle.Fill;
            videoView1.Location = new Point(0, 0);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(597, 450);
            videoView1.TabIndex = 0;
            videoView1.Text = "videoView1";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(videoView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnMatarProcesso);
            splitContainer1.Panel2.Controls.Add(lvProcessos);
            splitContainer1.Panel2.Controls.Add(lblProcessosAluno);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 597;
            splitContainer1.TabIndex = 1;
            // 
            // btnMatarProcesso
            // 
            btnMatarProcesso.Location = new Point(-1, 409);
            btnMatarProcesso.Name = "btnMatarProcesso";
            btnMatarProcesso.Size = new Size(200, 41);
            btnMatarProcesso.TabIndex = 14;
            btnMatarProcesso.Text = "Matar Processo";
            btnMatarProcesso.UseVisualStyleBackColor = true;
            // 
            // lvProcessos
            // 
            lvProcessos.CheckBoxes = true;
            lvProcessos.Columns.AddRange(new ColumnHeader[] { headerPrograma });
            lvProcessos.FullRowSelect = true;
            lvProcessos.Location = new Point(-1, 18);
            lvProcessos.Name = "lvProcessos";
            lvProcessos.Size = new Size(200, 385);
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
            // imageListProcessos
            // 
            imageListProcessos.ColorDepth = ColorDepth.Depth32Bit;
            imageListProcessos.ImageSize = new Size(16, 16);
            imageListProcessos.TransparentColor = Color.Transparent;
            // 
            // lblProcessosAluno
            // 
            lblProcessosAluno.AutoSize = true;
            lblProcessosAluno.Location = new Point(-1, 0);
            lblProcessosAluno.Name = "lblProcessosAluno";
            lblProcessosAluno.Size = new Size(112, 15);
            lblProcessosAluno.TabIndex = 12;
            lblProcessosAluno.Text = "Processos do aluno:";
            lblProcessosAluno.Visible = false;
            // 
            // FormFoco
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "FormFoco";
            Text = "FormFoco";
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView videoView1;
        private SplitContainer splitContainer1;
        private Button btnMatarProcesso;
        private ListView lvProcessos;
        private ColumnHeader headerPrograma;
        private Label lblProcessosAluno;
        private ImageList imageListProcessos;
    }
}