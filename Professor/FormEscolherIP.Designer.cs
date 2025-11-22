namespace Professor
{
    partial class FormEscolherIP
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
            lstInterfaces = new ListBox();
            btnOk = new Button();
            btnCancelar = new Button();
            lblMensagem = new Label();
            SuspendLayout();
            // 
            // lstInterfaces
            // 
            lstInterfaces.FormattingEnabled = true;
            lstInterfaces.ItemHeight = 15;
            lstInterfaces.Location = new Point(43, 53);
            lstInterfaces.Name = "lstInterfaces";
            lstInterfaces.Size = new Size(521, 154);
            lstInterfaces.TabIndex = 0;
            lstInterfaces.MouseDoubleClick += lstInterfaces_MouseDoubleClick;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(149, 235);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(124, 41);
            btnOk.TabIndex = 1;
            btnOk.Text = "Iniciar Servidor";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(330, 235);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(124, 41);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblMensagem
            // 
            lblMensagem.AutoSize = true;
            lblMensagem.Location = new Point(43, 35);
            lblMensagem.Name = "lblMensagem";
            lblMensagem.Size = new Size(145, 15);
            lblMensagem.TabIndex = 3;
            lblMensagem.Text = "Selecione o IP do servidor:";
            // 
            // FormEscolherIP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 314);
            Controls.Add(lblMensagem);
            Controls.Add(btnCancelar);
            Controls.Add(btnOk);
            Controls.Add(lstInterfaces);
            Name = "FormEscolherIP";
            Text = "FormEscolherIP";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstInterfaces;
        private Button btnOk;
        private Button btnCancelar;
        private Label lblMensagem;
    }
}