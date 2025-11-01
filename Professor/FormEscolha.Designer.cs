namespace Professor
{
    partial class FormEscolha
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
            btnModoAvaliacao = new Button();
            btnModoApresentacao = new Button();
            lstAlunosConectados = new ListBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnModoAvaliacao
            // 
            btnModoAvaliacao.Location = new Point(515, 377);
            btnModoAvaliacao.Name = "btnModoAvaliacao";
            btnModoAvaliacao.Size = new Size(140, 41);
            btnModoAvaliacao.TabIndex = 16;
            btnModoAvaliacao.Text = "Modo Avaliação";
            btnModoAvaliacao.UseVisualStyleBackColor = true;
            btnModoAvaliacao.Visible = false;
            // 
            // btnModoApresentacao
            // 
            btnModoApresentacao.Location = new Point(210, 377);
            btnModoApresentacao.Name = "btnModoApresentacao";
            btnModoApresentacao.Size = new Size(140, 41);
            btnModoApresentacao.TabIndex = 15;
            btnModoApresentacao.Text = "Modo Apresentação";
            btnModoApresentacao.UseVisualStyleBackColor = true;
            btnModoApresentacao.Visible = false;
            // 
            // lstAlunosConectados
            // 
            lstAlunosConectados.FormattingEnabled = true;
            lstAlunosConectados.HorizontalScrollbar = true;
            lstAlunosConectados.ItemHeight = 15;
            lstAlunosConectados.Location = new Point(321, 113);
            lstAlunosConectados.Name = "lstAlunosConectados";
            lstAlunosConectados.Size = new Size(217, 199);
            lstAlunosConectados.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(374, 75);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 13;
            label1.Text = "Alunos Conectados";
            // 
            // FormEscolha
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(851, 471);
            Controls.Add(btnModoAvaliacao);
            Controls.Add(btnModoApresentacao);
            Controls.Add(lstAlunosConectados);
            Controls.Add(label1);
            Name = "FormEscolha";
            Text = "FormEscolha";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnModoAvaliacao;
        private Button btnModoApresentacao;
        private ListBox lstAlunosConectados;
        private Label label1;
    }
}