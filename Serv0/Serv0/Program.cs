using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace Serv0
{
    class Program
    {
        public static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(5);
                while (true) {
                    Socket client = sListener.Accept();
                    String username = null;
                    String hash = null;
                    Console.WriteLine("new person");
                    
                    byte[] buffer = new byte[256];
                    client.Receive(buffer);
                    username += Encoding.ASCII.GetString(buffer);

                    Console.WriteLine("You are welcome " + username);

                    hash = Hash(username);
                    ArrayList list = new ArrayList();
                    list = GetStringFromFile(@"E:\Serv0\Serv0\bin\Debug\text.txt");
                    string user = User(username);
                    string password = Parser(list, user);

                    byte[] send = new byte[1];
                    
                    if (AcceptUser(Convert.ToInt32(hash), DateTime.DaysInMonth(2018, 5), "word", password))
                    {
                        send[0] = 100;                   
                        Console.WriteLine("User accepted");
                    }
                    else
                    {
                        Console.WriteLine("Sorry but you cannot accepted(Wrong password)");
                    }
                    client.Send(send);                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                sListener.Close();
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static string User(string userAndHash)
        {

            char[] mas = userAndHash.ToCharArray();
            int index = 0;
            StringBuilder user = new StringBuilder();
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] == ' ')
                {
                    index = i;
                }
            }
            for (int i = 0; i < index; i++)
            {
                user.Append(mas[i]);
            }
            return user.ToString();
        }

        private static string Hash(string userAndHash)
        {

            char[] mas = userAndHash.ToCharArray();
            int index = 0;
            StringBuilder hash = new StringBuilder();
            for(int i = 0; i < mas.Length; i ++)
            {
                if (mas[i] == ' ')
                {
                    index = i;
                }
            }
            for( int i = index + 1; i < mas.Length; i++)
            {
                hash.Append(mas[i]);
            }
            return hash.ToString();
        }

        private static string Parser(ArrayList list, string username)
        {
            foreach (string line in list)
            {
                if (line != null) {             
                    string[] strings = line.Split(' ');
                    string name = strings[0];
                    if (strings[0].Equals(username))
                    {                     
                        return strings[1];
                    }
                }
                
            }
            return null;
        }

        private static ArrayList GetStringFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            ArrayList text = new ArrayList();
            string temp = String.Empty;
            while (temp != null)
            {
                temp = sr.ReadLine();
                text.Add(temp);
            }
            return text;
        }

        private static bool AcceptUser(int hash, int id, string word, string password)
        {
            int buf = id.GetHashCode() + word.GetHashCode() + password.GetHashCode();
            if (hash == buf)
            {           
                return true;
            }
            return false;
        }
    }
}
