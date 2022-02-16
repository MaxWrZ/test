using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        const int ECHO_PORT = 8080;
        public static int nClients = 0;
        static void Main(string[] args)
        {
            try
            {
                TcpListener clientListener = new TcpListener(ECHO_PORT);
                clientListener.Start();
                Console.WriteLine("Waiting for connections...");
                while (nClients < 3)
                {
                    TcpClient client = clientListener.AcceptTcpClient();
                    ClientHandler cHandler = new ClientHandler();
                    cHandler.clientSocket = client;
                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                    nClients++;
                }
                clientListener.Stop();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp);
            }
        }

    }
}

