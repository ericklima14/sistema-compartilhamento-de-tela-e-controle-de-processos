using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Professor
{
    public partial class FormApresentacao : Form
    {
        private string mensagemProfessor;

        public FormApresentacao()
        {
            VideoTransmissaoManager.Instance.LogAtualizado += msg => AdicionarLog(msg);
            ConexaoService.Instance.LogAtualizado += msg => AdicionarLog(msg);
            VideoTransmissaoManager.Instance.StreamIniciado += OnStreamIniciado;

            InitializeComponent();

            txtMensagem.KeyDown += txtMensagem_KeyDown;
        }

        // Enviar o caminho da stream RTP pelo TCP.
        private void OnStreamIniciado(string ip, int port)
        {
            string comando = $"CMD_STREAM_INFO|{ip}|{port}";

            Task.Run(async () => await ConexaoService.Instance.BroadcastMessage(comando));

            AdicionarLog($"Anunciando stream para alunos: {ip}:{port}");
        }

        private void btnStartStream_Click(object sender, EventArgs e)
        {
            int preset = int.Parse(txtBoxPreset.Text);

            // TODO: adicionar tratamento de erro pra ips invalidos.
            VideoTransmissaoManager.Instance.StreamAddress = txtIpTransmissao.Text;
            VideoTransmissaoManager.Instance.StartStream(preset, txtBoxFrameRate.Text, txtBoxBitRate.Text);
            btnStartStream.Enabled = false;
            btnStopStream.Enabled = true;
        }

        private void btnStopStream_Click(object sender, EventArgs e)
        {
            VideoTransmissaoManager.Instance.PrepareForClosing();
            btnStartStream.Enabled = true;
            btnStopStream.Enabled = false;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            VideoTransmissaoManager.Instance.PrepareForClosing();
            VideoTransmissaoManager.Instance.LogAtualizado -= AdicionarLog;
            VideoTransmissaoManager.Instance.StreamIniciado -= OnStreamIniciado;
            //ProcessosManager.Instance.LogAtualizado -= AdicionarLog;

            base.OnFormClosed(e);
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

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMensagem.Text))
            {
                mensagemProfessor = $"Professor: {txtMensagem.Text}";
                await ConexaoService.Instance.BroadcastMessage(mensagemProfessor);
                AdicionarLog(mensagemProfessor);
                txtMensagem.Clear();
            }
        }

        private void txtMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Tira som do windows zoado
                e.SuppressKeyPress = true;

                btnEnviar.PerformClick();
            }
        }
    }
}
