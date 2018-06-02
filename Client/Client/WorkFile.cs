using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class WorkFile
    {
        public WorkFile()
        {
        }

        public static void writeInFile(string login, string password)
        {
            using (StreamWriter sw = new StreamWriter(@"E:\Serv0\Serv0\bin\Debug\text.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(login + " " + password);
            }
        }

        public static bool readFromFile(string login)
        {
            using (StreamReader sr = new StreamReader(@"E:\Serv0\Serv0\bin\Debug\text.txt", System.Text.Encoding.Default))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                    {
                        string firstLineWord = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        if (firstLineWord.Equals(login))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public static string getpass(string login)
        {
            string pass = " ";
            using (StreamReader sr = new StreamReader("test.txt", System.Text.Encoding.Default))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    string firstLineWord = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (firstLineWord.Equals(login))
                    {
                        if (!string.IsNullOrEmpty(line.Trim()))
                        {
                            string[] mas = line.Split(new char[] { ' ' });
                            pass = mas[1];

                        }
                    }

                }
            }
            return pass;
        }
    }
}
