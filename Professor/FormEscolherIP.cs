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
    public partial class FormEscolherIP : Form
    {
        private class InterfaceDisplay
        {
            public string Ip { get; set; }
            public string Descricao { get; set; }

            public override string ToString()
            {
                return $"IP: {Ip}  ({Descricao})";
            }
        }

        public FormEscolherIP(Dictionary<string, string> interfaces)
        {
            InitializeComponent();

            if (interfaces == null || interfaces.Count == 0)
            {
                MessageBox.Show("Nenhuma interface de rede IPv4 ativa foi encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, e) => this.Close();
                return;
            }

            foreach (var entry in interfaces)
            {
                lstInterfaces.Items.Add(new InterfaceDisplay { Ip = entry.Key, Descricao = entry.Value });
            }

            // Seleciona o primeiro item por padrão
            lstInterfaces.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            btnOk.Text = "Iniciando...";

            var selected = lstInterfaces.SelectedItem as InterfaceDisplay;
            if (selected != null)
            {
                ConexaoService.Instance.ProfessorIP = selected.Ip;

                Task.Delay(500).ContinueWith(_ =>
                {
                    this.Invoke(() =>
                    {
                        ConexaoService.Instance.IniciarServidor();

                        var formEscolha = new FormEscolha();
                        formEscolha.Show();

                        formEscolha.FormClosed += (s, args) => this.Close();

                        this.Hide();
                    });
                });
            }
        }

        private void lstInterfaces_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
