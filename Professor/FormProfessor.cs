using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Professor
{
    public partial class FormProfessor : Form
    {
        private string _alunoAtual = null;

        public FormProfessor()
        {
            InitializeComponent();

            ConexaoService.Instance.ListaDeAlunosAtualizada += AtualizarListaAlunos;

            ProcessosManager.Instance.LogAtualizado += msg => AdicionarLog(msg);
            ProcessosManager.Instance.ListaProcessosAtualizada += AtualizarListaProcessos;

            AtualizarListaAlunos();
        }

        private void AtualizarListaProcessos(string[] processos)
        {
            if (lvProcessos.InvokeRequired)
            {
                lvProcessos.Invoke(new Action(() => AtualizarListaProcessos(processos)));
                return;
            }

            var checkedItems = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(i => i.Text).ToHashSet();
            int topIndex = lvProcessos.TopItem?.Index ?? 0;

            lvProcessos.BeginUpdate();
            lvProcessos.Items.Clear();
            imageListProcessos.Images.Clear();
            imageListProcessos.Images.Add(SystemIcons.Application);

            var iconCache = new Dictionary<string, int>();
            foreach (var proc in processos)
            {
                if (string.IsNullOrEmpty(proc)) continue;

                int iconIndex = 0;
                if (ProcessosManager.Instance.CaminhosDeIcone.TryGetValue(proc, out var caminho) && !string.IsNullOrEmpty(caminho))
                {
                    try
                    {
                        var icon = Icon.ExtractAssociatedIcon(caminho);
                        if (icon != null)
                        {
                            imageListProcessos.Images.Add(icon);
                            iconIndex = imageListProcessos.Images.Count - 1;
                        }
                    }
                    catch { }
                }

                var item = new ListViewItem(proc, iconIndex) { Checked = checkedItems.Contains(proc) };
                lvProcessos.Items.Add(item);
            }

            lvProcessos.EndUpdate();
            if (lvProcessos.Items.Count > topIndex)
                lvProcessos.EnsureVisible(topIndex);
        }

        private void AtualizarListaAlunos()
        {
            if (lstAlunosConectados.InvokeRequired)
            {
                lstAlunosConectados.Invoke(new Action(AtualizarListaAlunos));
                return;
            }

            var selecionado = lstAlunosConectados.SelectedItem?.ToString();
            lstAlunosConectados.Items.Clear();
            lock (ConexaoService.Instance.Clients)
            {
                foreach (var aluno in ConexaoService.Instance.Clients.Values)
                {
                    lstAlunosConectados.Items.Add(aluno);
                }
            }
            if (selecionado != null && lstAlunosConectados.Items.Contains(selecionado))
                lstAlunosConectados.SelectedItem = selecionado;
        }

        private void lstAlunosConectados_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnListarProcessos.Visible = true;
            btnListarProcessos.Enabled = lstAlunosConectados.SelectedItem != null;
        }


        private void AdicionarLog(string msg)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action<string>(AdicionarLog), msg);
                return;
            }
            lstLog.Items.Add(msg);
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private async void btnListarProcessos_Click(object sender, EventArgs e)
        {
            if (lstAlunosConectados.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione um aluno na lista.", "Nenhum Aluno Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblProcessosAluno.Visible = true;
            lvProcessos.Visible = true;

            lblProcessosAluno.Text = $"Processos do aluno: {lstAlunosConectados.SelectedItem}";

            string selectedIdentifier = lstAlunosConectados.SelectedItem.ToString();

            await ProcessosManager.Instance.IniciarMonitoramento(selectedIdentifier);
        }

        private async void btnMatarProcesso_Click(object sender, EventArgs e)
        {
            List<string> processos = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(item => item.Text).ToList();
            await ProcessosManager.Instance.MatarProcessos(processos);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ConexaoService.Instance.ListaDeAlunosAtualizada -= AtualizarListaAlunos;
            ProcessosManager.Instance.LogAtualizado -= AdicionarLog;
            ProcessosManager.Instance.ListaProcessosAtualizada -= AtualizarListaProcessos;
            base.OnFormClosed(e);
        }

        private async void btnGerenciarBloqueios_Click(object sender, EventArgs e)
        {
            using (var form = new FormGerenciarBloqueio(ProcessosManager.Instance.ProcessosBloqueados.ToList()))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var novosIcones = form.ProgramasEncontrados
                        .Where(p => !string.IsNullOrEmpty(p.CaminhoIcone))
                        .ToDictionary(p => p.NomeProcesso, p => p.CaminhoIcone);

                    ProcessosManager.Instance.AtualizarBlocklist(form.ListaBloqueioFinal, novosIcones);
                }
            }
        }
    }
}
