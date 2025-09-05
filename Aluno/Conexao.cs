using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Aluno
{
    public class Conexao
    {
        private UdpClient Client;
        private IPEndPoint Port;

        public Conexao(UdpClient client)
        {
            Client = client;
        }
    }
}
