using LibVLCSharp.Shared;
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
    public partial class FormFoco : Form
    {
        private readonly LibVLC _libVLC;
        private readonly int _port;
        private MediaPlayer _mediaPlayer;

        public int Port { get; private set; }
        public string NomeAluno { get; private set; }

        public FormFoco(LibVLC libVLC, int port, string nomeAluno)
        {
            InitializeComponent();

            _libVLC = libVLC;
            _port = port;
            this.Text = $"Foco: {nomeAluno} (Porta {_port})";

            this.Port = port;
            this.NomeAluno = nomeAluno;

            this.lblProcessosAluno.Text = $"Processos: {this.NomeAluno}";
            this.btnMatarProcesso.Click += btnMatarProcesso_Click; // Conecta o evento

            this.Load += FormFoco_Load;
            this.FormClosing += FormFoco_FormClosing;
        }

        private async void FormFoco_Load(object sender, EventArgs e)
        {
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mediaPlayer;

            try
            {
                string sdpFilePath = Path.Combine(Path.GetTempPath(), $"prof_recv_{_port}.sdp");

                if (File.Exists(sdpFilePath))
                {
                    var media = new Media(_libVLC, new Uri(sdpFilePath));
                    // Opção para reduzir delay no foco
                    // media.AddOption(":network-caching=150"); 
                    _mediaPlayer.Play(media);
                }
                else
                {
                    MessageBox.Show("Arquivo de configuração de stream (SDP) não encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao focar no stream: {ex.Message}");
            }

            ProcessosManager.Instance.ListaProcessosAtualizada += AtualizarListaProcessos;
            await Task.Run(async () => await ProcessosManager.Instance.IniciarMonitoramento(NomeAluno));
        }

        private void FormFoco_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();

            ProcessosManager.Instance.ListaProcessosAtualizada -= AtualizarListaProcessos;
            Task.Run(async () => await ProcessosManager.Instance.PararMonitoramento());
        }

        private async void btnMatarProcesso_Click(object sender, EventArgs e)
        {
            List<string> processos = lvProcessos.CheckedItems.Cast<ListViewItem>().Select(item => item.Text).ToList();
            await ProcessosManager.Instance.MatarProcessos(processos);
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
    }
}
