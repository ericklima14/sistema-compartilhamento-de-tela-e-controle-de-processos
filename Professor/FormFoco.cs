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

            this.Load += FormFoco_Load;
            this.FormClosing += FormFoco_FormClosing;
        }

        private void FormFoco_Load(object sender, EventArgs e)
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
        }

        private void FormFoco_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();
        }
    }
}
