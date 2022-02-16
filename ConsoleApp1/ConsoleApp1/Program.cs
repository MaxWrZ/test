using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        const int ECHO_PORT = 8080;
        static void Main(string[] args)
        {
            Console.WriteLine("your name:");
            string name = Console.ReadLine();
            Console.WriteLine("--logged in--");



            try
            {
                TcpClient eClient = new TcpClient("127.0.01", ECHO_PORT);

                StreamReader readerStream = new StreamReader(eClient.GetStream());
                NetworkStream writerStream = eClient.GetStream();

                string dataToSend;

                dataToSend = name;
                dataToSend += "\r\n";

                byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                writerStream.Write(data, 0, data.Length);

                while (true)
                {
                    Console.Write(name + ":");

                    dataToSend = Console.ReadLine();
                    dataToSend += "\r\n";

                    data = Encoding.ASCII.GetBytes(dataToSend);
                    writerStream.Write(data, 0, data.Length);

                    if (dataToSend.IndexOf("QUIT") > -1)
                        break;

                    string returnData;
                    returnData = readerStream.ReadLine();
                    Console.WriteLine("Server: " + returnData);
                }
                eClient.Close();
            }
            catch(Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
            }
        }
    }
}
