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
            InitializeComponent();
        }

        private void btnStartStream_Click(object sender, EventArgs e)
        {
            VideoTransmissaoManager.Instance.StartStream();
        }

        private void btnStopStream_Click(object sender, EventArgs e)
        {
            VideoTransmissaoManager.Instance.PrepareForClosing();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            VideoTransmissaoManager.Instance.PrepareForClosing();
            VideoTransmissaoManager.Instance.LogAtualizado -= AdicionarLog;
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
    }
}
