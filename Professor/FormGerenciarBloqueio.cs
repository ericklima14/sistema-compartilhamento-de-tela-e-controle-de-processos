using Microsoft.Win32;
using System.Data;


namespace Professor
{
    public partial class FormGerenciarBloqueio : Form
    {
        public enum ModoGerenciamento
        {
            FluxoInicial,
            EdicaoProcessos
        }

        public class ProgramaInstalado
        {
            public string Nome { get; set; }
            public string NomeProcesso { get; set; }
            public string CaminhoIcone { get; set; }
        }

        private ModoGerenciamento Modo { get; set; }
        public List<string> ListaBloqueioFinal { get; private set; }
        public List<ProgramaInstalado> ProgramasEncontrados { get; private set; }

        public FormGerenciarBloqueio(List<string> listaProcessos, ModoGerenciamento modo = ModoGerenciamento.EdicaoProcessos)
        {
            InitializeComponent();
            ListaBloqueioFinal = new List<string>(listaProcessos);
            Modo = modo;
            ConfigurarBotoes();
        }

        private void ConfigurarBotoes()
        {
            btnOk.Visible = btnCancelar.Visible = btnProximo.Visible = false;

            if (Modo == ModoGerenciamento.FluxoInicial)
            {
                btnProximo.Visible = true;
                this.Text = "Configurar Bloqueios (Obrigatório)";
                this.AcceptButton = btnProximo;
            }
            else
            {
                btnOk.Visible = btnCancelar.Visible = true;
                this.Text = "Gerenciar Bloqueios";
                this.AcceptButton = btnOk;
                this.CancelButton = btnCancelar;
            }
        }

        private void FormGerenciarBloqueio_Load(object sender, EventArgs e)
        {
            CarregarProgramas();
        }

        private void CarregarProgramas()
        {
            ProgramasEncontrados = GetProgramasInstalados();

            var processosManuais = ProcessosManager.Instance.ProcessosManuaisConhecidos;

            lvInstalados.BeginUpdate();
            lvBloqueados.BeginUpdate();

            lvInstalados.Items.Clear();
            lvBloqueados.Items.Clear();

            imageListIcones.Images.Clear();
            imageListIcones.Images.Add(SystemIcons.Application); // Default icon

            var processosJaAdicionados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var programa in ProgramasEncontrados.OrderBy(p => p.Nome))
            {
                int iconIndex = 0;
                if (!string.IsNullOrEmpty(programa.CaminhoIcone))
                {
                    try
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(programa.CaminhoIcone);
                        if (icon != null)
                        {
                            imageListIcones.Images.Add(icon);
                            iconIndex = imageListIcones.Images.Count - 1;
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Usando icone padrao para o programa {programa.Nome}");
                    }
                }

                var item = new ListViewItem(programa.Nome, iconIndex)
                {
                    Tag = programa.NomeProcesso
                };

                if (ListaBloqueioFinal.Contains(programa.NomeProcesso, StringComparer.OrdinalIgnoreCase))
                {
                    lvBloqueados.Items.Add(item);
                }
                else
                {
                    lvInstalados.Items.Add(item);
                }

                processosJaAdicionados.Add(programa.NomeProcesso);
            }

            foreach (string nomeProcessoManual in processosManuais)
            {
                // Se este processo manual AINDA não foi adicionado (ex: não estava no registro)
                if (!processosJaAdicionados.Contains(nomeProcessoManual))
                {
                    var itemManual = new ListViewItem($"{nomeProcessoManual} (Manual)", 0) // Usa ícone padrão
                    {
                        Tag = nomeProcessoManual
                    };

                    // Verifica onde ele deve ir:
                    if (ListaBloqueioFinal.Contains(nomeProcessoManual, StringComparer.OrdinalIgnoreCase))
                    {
                        lvBloqueados.Items.Add(itemManual);
                    }
                    else
                    {
                        // AQUI ESTÁ A MÁGICA!
                        // Se ele está na "memória" mas NÃO está na lista de bloqueio,
                        // ele (corretamente) vai para a lista de INSTALADOS (Desbloqueados).
                        lvInstalados.Items.Add(itemManual);
                    }
                }
            }

            lvInstalados.EndUpdate();
            lvBloqueados.EndUpdate();
        }

        private List<ProgramaInstalado> GetProgramasInstalados()
        {
            var programas = new List<ProgramaInstalado>();
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            string registry_key_wow6432 = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            VarrerChaveDoRegistro(Registry.LocalMachine, registry_key, programas);
            VarrerChaveDoRegistro(Registry.LocalMachine, registry_key_wow6432, programas);
            VarrerChaveDoRegistro(Registry.CurrentUser, registry_key, programas);
            VarrerChaveDoRegistro(Registry.CurrentUser, registry_key_wow6432, programas);

            return programas.DistinctBy(p => p.NomeProcesso).ToList();
        }

        private void VarrerChaveDoRegistro(RegistryKey key, string keyPath, List<ProgramaInstalado> programas)
        {
            try
            {
                using (RegistryKey newKey = key.OpenSubKey(keyPath))
                {
                    if (newKey == null) return;
                    foreach (string subkey_name in newKey.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = newKey.OpenSubKey(subkey_name))
                        {
                            var programa = CriarProgramaSubkey(subkey);
                            if (programa != null) programas.Add(programa);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o registro em {keyPath}: {ex.Message}");
            }
        }

        private ProgramaInstalado CriarProgramaSubkey(RegistryKey subkey)
        {
            string displayName = subkey.GetValue("DisplayName") as string;
            string displayIcon = subkey.GetValue("DisplayIcon") as string;
            string uninstallString = subkey.GetValue("UninstallString") as string;

            if (string.IsNullOrEmpty(displayName))
                return null;

            if (displayName.Contains("Update") || displayName.Contains("Security Update"))
                return null;

            var programa = new ProgramaInstalado { Nome = displayName };
            string nomeProcesso = null;
            string caminhoIcone = null;

            if (!string.IsNullOrEmpty(displayIcon))
            {
                caminhoIcone = displayIcon.Split(',')[0].Trim('"');
                nomeProcesso = Path.GetFileNameWithoutExtension(caminhoIcone);
            }

            //if (string.IsNullOrEmpty(nomeProcesso) && !string.IsNullOrEmpty(uninstallString))
            //{
            //    try
            //    {
            //        // Tenta extrair o nome do processo da string de desinstalação
            //        // Exemplo: "C:\Program Files\App\unins000.exe" -> "unins000"
            //        nomeProcesso = Path.GetFileNameWithoutExtension(uninstallString.Split(new[] { ".exe" }, StringSplitOptions.None)[0]);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.Write($"Não foi possivel extrair o nome do processo. Erro: {ex}");
            //    }
            //}

            if (string.IsNullOrEmpty(nomeProcesso))
                return null;

            programa.NomeProcesso = nomeProcesso;
            programa.CaminhoIcone = caminhoIcone;

            return programa;
        }

        private void MoverItems(ListView origem, ListView destino)
        {
            foreach (ListViewItem item in origem.SelectedItems)
            {
                origem.Items.Remove(item);
                destino.Items.Add(item);
            }
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            MoverItems(lvInstalados, lvBloqueados);
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            MoverItems(lvBloqueados, lvInstalados);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SalvarListaBloqueio();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SalvarListaBloqueio()
        {
            ListaBloqueioFinal = lvBloqueados.Items
                .Cast<ListViewItem>()
                .Select(i => i.Tag.ToString())
                .ToList();

            var novosIcones = ProgramasEncontrados
                   .Where(p => !string.IsNullOrEmpty(p.CaminhoIcone))
                   .ToDictionary(p => p.NomeProcesso, p => p.CaminhoIcone);

            ProcessosManager.Instance.AtualizarBlocklist(ListaBloqueioFinal, novosIcones);
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            SalvarListaBloqueio();
            var formAvaliacao = new FormProfessor();
            formAvaliacao.Show();

            formAvaliacao.FormClosed += (s, args) => this.Close();
            this.Hide();
        }
    }
}
