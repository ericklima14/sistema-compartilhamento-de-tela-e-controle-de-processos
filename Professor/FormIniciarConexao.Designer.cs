namespace Professor
{
    partial class FormIniciarConexao
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
            btnIniciarServidor = new Button();
            SuspendLayout();
            // 
            // btnIniciarServidor
            // 
            btnIniciarServidor.Location = new Point(338, 203);
            btnIniciarServidor.Name = "btnIniciarServidor";
            btnIniciarServidor.Size = new Size(151, 55);
            btnIniciarServidor.TabIndex = 5;
            btnIniciarServidor.Text = "Iniciar";
            btnIniciarServidor.UseVisualStyleBackColor = true;
            btnIniciarServidor.Click += btnIniciarServidor_Click;
            // 
            // FormIniciarConexao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 473);
            Controls.Add(btnIniciarServidor);
            Name = "FormIniciarConexao";
            Text = "FormIniciarConexao";
            ResumeLayout(false);
        }

        #endregion

        private Button btnIniciarServidor;
    }
}