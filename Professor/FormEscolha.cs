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
            var formProfessor = new FormGerenciarBloqueio();
            formProfessor.Show();
            this.Hide();
        }

        private void btnModoApresentacao_Click(object sender, EventArgs e)
        {
            //TODO: temos que mudar essa bomba para o que vamos de fato chamar
            var formProfessor = new FormProfessor();
            formProfessor.Show();
            this.Hide();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ConexaoService.Instance.ListaDeAlunosAtualizada -= AtualizarListaAlunos;
            base.OnFormClosed(e);
        }
    }
}
