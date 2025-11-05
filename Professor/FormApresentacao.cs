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
    public partial class FormApresentacao : Form
    {
        public FormApresentacao()
        {
            VideoTransmissaoManager.Instance.LogAtualizado += msg => AdicionarLog(msg);
            InitializeComponent();
        }

        private void btnStartStream_Click(object sender, EventArgs e)
        {
            VideoTransmissaoManager.Instance.StartStream();
        }

        private void btnStopStream_Click(object sender, EventArgs e)
        {
            VideoTransmissaoManager.Instance.StopStream();
        }

        //private void FormApresentacao_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    VideoTransmissaoManager.Instance.StopStream();
        //    VideoTransmissaoManager.Instance.LogAtualizado -= AdicionarLog;
        //    Close();
        //}
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            VideoTransmissaoManager.Instance.StopStream();
            VideoTransmissaoManager.Instance.LogAtualizado -= AdicionarLog;
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
    }
}
