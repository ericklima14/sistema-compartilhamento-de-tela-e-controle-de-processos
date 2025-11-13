using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Professor
{
    public partial class FormEscolha : Form
    {
        public FormEscolha()
        {
            InitializeComponent();
            ConexaoService.Instance.ListaDeAlunosAtualizada += AtualizarListaAlunos;

            lblProfessorIP.Text = ConexaoService.Instance.ProfessorIP;
        }

        public void AtualizarListaAlunos()
        {
            if (lstAlunosConectados.InvokeRequired)
            {
                lstAlunosConectados.Invoke(new Action(AtualizarListaAlunos));
                return;
            }

            lstAlunosConectados.Items.Clear();
            lock (ConexaoService.Instance.Clients)
            {
                foreach (var aluno in ConexaoService.Instance.Clients.Values)
                {
                    lstAlunosConectados.Items.Add(aluno);
                }
            }
        }

        private void btnModoAvaliacao_Click(object sender, EventArgs e)
        {
            this.Hide();

            var formGerenciarBloqueio = new FormGerenciarBloqueio(ProcessosManager.Instance.ProcessosBloqueados.ToList(),
                FormGerenciarBloqueio.ModoGerenciamento.FluxoInicial);
            formGerenciarBloqueio.Show();

            formGerenciarBloqueio.FormClosed += (s, args) => this.Show();
        }

        private void btnModoApresentacao_Click(object sender, EventArgs e)
        {
            this.Hide();

            var formApresentacao = new FormApresentacao();
            formApresentacao.Show();

            formApresentacao.FormClosed += (s, args) => this.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ConexaoService.Instance.ListaDeAlunosAtualizada -= AtualizarListaAlunos;
            base.OnFormClosed(e);
        }

        private void lblProfessorIP_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(lblProfessorIP.Text);
        }
    }
}
