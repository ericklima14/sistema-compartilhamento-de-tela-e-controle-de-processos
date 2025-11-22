namespace Professor
{
    public partial class FormIniciarConexao : Form
    {
        public FormIniciarConexao()
        {
            InitializeComponent();
        }

        private void btnIniciarServidor_Click(object sender, EventArgs e)
        {
            btnIniciarServidor.Enabled = false;
            btnIniciarServidor.Text = "Iniciando...";

            var interfaces = ConexaoService.ObterIntefaces();

            Task.Delay(500).ContinueWith(_ =>
            {
                this.Invoke(() =>
                {
                    var formEscolherIP = new FormEscolherIP(interfaces);
                    formEscolherIP.Show();

                    formEscolherIP.FormClosed += (s, args) => this.Close();

                    this.Hide();
                });
            });
        }
    }
}
