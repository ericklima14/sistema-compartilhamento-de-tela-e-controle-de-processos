# Sistema Integrado para Supervisão de Avaliações e Compartilhamento de Tela

![Status](https://img.shields.io/badge/STATUS-CONCLUÍDO-brightgreen)
![Language](https://img.shields.io/badge/C%23-.NET%20Framework-purple)
![Platform](https://img.shields.io/badge/OS-Windows-blue)
![Tech](https://img.shields.io/badge/Video-FFmpeg%20%7C%20H.264-red)
![Tech](https://img.shields.io/badge/Compression-Zstandard-orange)

> Trabalho de Conclusão de Curso apresentado ao Centro Universitário Senac Santo Amaro para obtenção do título de Bacharel em Ciência da Computação.

## 📌 Sobre o Projeto

Este projeto consiste em um **protótipo funcional integrado para supervisão de avaliações presenciais em laboratórios de informática**. A solução aborda o desafio da integridade acadêmica em provas digitais, oferecendo ferramentas para que docentes possam monitorar as telas dos alunos e restringir o uso de softwares não autorizados.

Diferente de soluções puramente online, este sistema foi otimizado para **Redes Locais (LAN)**, utilizando uma arquitetura **Peer-to-Peer (P2P)** para garantir baixa latência na transmissão de vídeo e alta eficiência no bloqueio de processos.

### 🎥 Demonstração

Confira o vídeo de demonstração do projeto:
[**Assistir no YouTube**](https://youtu.be/jA-trMaSWwU)

---

## 🚀 Funcionalidades Principais

O sistema opera em dois modos principais:

### 1. 🎓 Modo Apresentação (Professor -> Alunos)
* **Compartilhamento de Tela:** O professor transmite sua tela em tempo real para todos os alunos conectados.
* **Transmissão Otimizada:** Utiliza broadcast/multicast para distribuir o vídeo sem saturar a rede.
* **Chat:** Envio de mensagens de texto com compressão para comunicação rápida.

### 2. 📝 Modo Avaliação (Monitoramento & Controle)
* **Grade de Monitoramento:** O professor visualiza as miniaturas das telas de todos os alunos simultaneamente (5 FPS para economia de banda).
* **Modo Foco:** Ao clicar em um aluno, a transmissão muda para alta qualidade (30 FPS) e baixa latência para inspeção detalhada.
* **Controle de Processos (Anti-Fraude):**
    * Bloqueio seletivo de programas (ex: navegadores, IDEs, chats).
    * Monitoramento via **WMI (Windows Management Instrumentation)** para encerrar processos proibidos instantaneamente assim que iniciados.
    * Listagem remota de processos em execução na máquina do aluno.

---

## 🛠️ Tecnologias Utilizadas

O projeto foi desenvolvido focando em desempenho e baixo uso de recursos:

* **Linguagem:** C# (.NET Framework / Windows Forms)
* **Vídeo & Codecs:**
    * **FFmpeg:** Para captura e codificação de tela.
    * **H.264 (AVC):** Codec de vídeo utilizado pela ampla compatibilidade e aceleração de hardware.
    * **RTP (Real-time Transport Protocol):** Para transmissão via UDP com baixa latência.
* **Compressão de Dados:**
    * **Zstandard (Zstd):** Utilizado para comprimir o tráfego de mensagens e listas de processos, reduzindo o payload em até 64%.
* **Gerenciamento do Sistema:**
    * **WMI:** Para fiscalização e encerramento de processos do SO.

---

## 📊 Resultados Obtidos

Durante os testes de campo realizados nos laboratórios do SENAC, o protótipo apresentou:

* **Latência:** Média inferior a **150 ms** em todos os cenários (Monitoramento, Foco e Apresentação).
* **Estabilidade:** Suporte a múltiplas conexões simultâneas sem degradação significativa da máquina do professor.
* **Perda de Pacotes:** Taxa de perda próxima a 0,90% em rede cabeada (aceitável para UDP).
* **Jitter:** Abaixo de 20ms, garantindo fluidez visual.

---

## ⚙️ Como Executar

### Pré-requisitos
* Sistema Operacional Windows (testado no Windows 10/11).
* **.NET Framework** instalado.
* **FFmpeg** configurado nas variáveis de ambiente ou presente na pasta do executável.

### Passos
1.  Clone este repositório:
    ```bash
    git clone https://github.com/ericklima14/sistema-compartilhamento-de-tela-e-controle-de-processos.git
    ```
2.  Abra a solução no **Visual Studio**.
3.  Instale os pacotes NuGet necessários.
4.  Compile o projeto (Build Solution).
5.  Execute o `AppProfessor` na máquina docente e o `AppAluno` nas máquinas discentes.
6.  No `AppAluno`, insira o IP fornecido pelo `AppProfessor` para conectar.

---

## 👥 Autores

* **Chrystian Medeiros de Oliveira**
* **Erick Costa Reimberg de Lima**
* **Gustavo Silva de Oliveira**

**Orientador:** Prof. Ms. Thyago Conchado Quintas

---

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT** - veja o arquivo [LICENSE.md](LICENSE.md) para mais detalhes.

---
*Este repositório contém o código-fonte desenvolvido para o TCC apresentado em 2025.*
