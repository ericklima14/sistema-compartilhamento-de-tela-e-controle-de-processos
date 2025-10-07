namespace Professor
{
    partial class FormGerenciarBloqueio
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
            lvInstalados = new ListView();
            columnProgramaInstalados = new ColumnHeader();
            imageListIcones = new ImageList(components);
            lvBloqueados = new ListView();
            columnProgramaBloq = new ColumnHeader();
            btnBloquear = new Button();
            btnDesbloquear = new Button();
            btnOk = new Button();
            Cancelar = new Button();
            lblInstalados = new Label();
            lblBloqueados = new Label();
            SuspendLayout();
            // 
            // lvInstalados
            // 
            lvInstalados.Columns.AddRange(new ColumnHeader[] { columnProgramaInstalados });
            lvInstalados.FullRowSelect = true;
            lvInstalados.Location = new Point(142, 139);
            lvInstalados.Name = "lvInstalados";
            lvInstalados.Size = new Size(192, 212);
            lvInstalados.SmallImageList = imageListIcones;
            lvInstalados.TabIndex = 0;
            lvInstalados.UseCompatibleStateImageBehavior = false;
            lvInstalados.View = View.Details;
            // 
            // columnProgramaInstalados
            // 
            columnProgramaInstalados.Text = "Programas";
            columnProgramaInstalados.Width = 250;
            // 
            // imageListIcones
            // 
            imageListIcones.ColorDepth = ColorDepth.Depth32Bit;
            imageListIcones.ImageSize = new Size(16, 16);
            imageListIcones.TransparentColor = Color.Transparent;
            // 
            // lvBloqueados
            // 
            lvBloqueados.Columns.AddRange(new ColumnHeader[] { columnProgramaBloq });
            lvBloqueados.FullRowSelect = true;
            lvBloqueados.Location = new Point(578, 139);
            lvBloqueados.Name = "lvBloqueados";
            lvBloqueados.Size = new Size(192, 212);
            lvBloqueados.SmallImageList = imageListIcones;
            lvBloqueados.TabIndex = 1;
            lvBloqueados.UseCompatibleStateImageBehavior = false;
            lvBloqueados.View = View.Details;
            // 
            // columnProgramaBloq
            // 
            columnProgramaBloq.Text = "Programas";
            columnProgramaBloq.Width = 250;
            // 
            // btnBloquear
            // 
            btnBloquear.Location = new Point(399, 197);
            btnBloquear.Name = "btnBloquear";
            btnBloquear.Size = new Size(100, 34);
            btnBloquear.TabIndex = 2;
            btnBloquear.Text = ">>";
            btnBloquear.UseVisualStyleBackColor = true;
            btnBloquear.Click += btnBloquear_Click;
            // 
            // btnDesbloquear
            // 
            btnDesbloquear.Location = new Point(399, 247);
            btnDesbloquear.Name = "btnDesbloquear";
            btnDesbloquear.Size = new Size(100, 34);
            btnDesbloquear.TabIndex = 3;
            btnDesbloquear.Text = "<<";
            btnDesbloquear.UseVisualStyleBackColor = true;
            btnDesbloquear.Click += btnDesbloquear_Click;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(309, 437);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(100, 34);
            btnOk.TabIndex = 4;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // Cancelar
            // 
            Cancelar.Location = new Point(494, 437);
            Cancelar.Name = "Cancelar";
            Cancelar.Size = new Size(100, 34);
            Cancelar.TabIndex = 5;
            Cancelar.Text = "Cancelar";
            Cancelar.UseVisualStyleBackColor = true;
            Cancelar.Click += Cancelar_Click;
            // 
            // lblInstalados
            // 
            lblInstalados.AutoSize = true;
            lblInstalados.Location = new Point(208, 104);
            lblInstalados.Name = "lblInstalados";
            lblInstalados.Size = new Size(60, 15);
            lblInstalados.TabIndex = 6;
            lblInstalados.Text = "Instalados";
            // 
            // lblBloqueados
            // 
            lblBloqueados.AutoSize = true;
            lblBloqueados.Location = new Point(637, 104);
            lblBloqueados.Name = "lblBloqueados";
            lblBloqueados.Size = new Size(69, 15);
            lblBloqueados.TabIndex = 7;
            lblBloqueados.Text = "Bloqueados";
            // 
            // FormGerenciarBloqueio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 542);
            Controls.Add(lblBloqueados);
            Controls.Add(lblInstalados);
            Controls.Add(Cancelar);
            Controls.Add(btnOk);
            Controls.Add(btnDesbloquear);
            Controls.Add(btnBloquear);
            Controls.Add(lvBloqueados);
            Controls.Add(lvInstalados);
            Name = "FormGerenciarBloqueio";
            Text = "FormGerenciarBloqueio";
            Load += FormGerenciarBloqueio_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvInstalados;
        private ListView lvBloqueados;
        private ColumnHeader columnProgramaInstalados;
        private ColumnHeader columnProgramaBloq;
        private Button btnBloquear;
        private Button btnDesbloquear;
        private Button btnOk;
        private Button Cancelar;
        private ImageList imageListIcones;
        private Label lblInstalados;
        private Label lblBloqueados;
    }
}