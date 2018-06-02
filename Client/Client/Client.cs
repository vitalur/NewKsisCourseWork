using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static IPHostEntry ipHost = Dns.GetHostEntry("localhost");
        static IPAddress ipAddr = ipHost.AddressList[0];
        static IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

        Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        public Client() { }

        public void sendmsg(string username, string hash)
        {
            try {
                socket.Connect(ipEndPoint);

                string message = username + " " + hash;// +"-";
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                int bytesSend = socket.Send(buffer);

                string msg = null;
                byte[] send = new byte[1];
                socket.Receive(send);
                
                if (send[0] == 100)
                {
                    Congratulation congrat = new Congratulation();
                    congrat.Show();
                }         
            } catch (SocketException e)
            {
                ServerIsNotStart serverIsNotStart = new ServerIsNotStart();
                serverIsNotStart.Show();
            }
        }

        public void stopConectToServer()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public int calculate(int id, string word, string password)
        {
            return id.GetHashCode() + word.GetHashCode() + password.GetHashCode();
        }


    }
}
