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

            ConexaoService.Instance.IniciarServidor();

            Task.Delay(500).ContinueWith(_ =>
            {
                this.Invoke(() =>
                {
                    var formEscolha = new FormEscolha();
                    formEscolha.Show();
                    this.Hide();
                });
            });
        }
    }
}
