using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {   //Close
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //autorization
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                EmptyRegistration reg = new EmptyRegistration();
                reg.Show();
            }
            else if (WorkFile.readFromFile(textBox1.Text))
            {
                Client client = new Client();
                var x = DateTime.DaysInMonth(2018, 5).GetHashCode();
                if (x < 0)
                {
                    x *= -1;
                }
                string a = (x + "word".GetHashCode() + getMD5(textBox2.Text).GetHashCode()) + "";
                
                client.sendmsg(textBox1.Text, a);


            } else
            {
                NotRegistrated notRegistrated = new NotRegistrated();
                notRegistrated.Show();
            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {   //registration
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                EmptyRegistration reg = new EmptyRegistration();
                reg.Show();
            }
            else if (WorkFile.readFromFile(textBox1.Text))
            {
                NotCurrentLogin notCurrentLogin = new NotCurrentLogin();
                notCurrentLogin.Show();
            }
            else
            {
                string login = textBox1.Text;
                string password = getMD5(textBox2.Text);

                WorkFile.writeInFile(login, password);
                MessageBox.Show("You registrated!");
            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private string getMD5(string text)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] result = md.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
