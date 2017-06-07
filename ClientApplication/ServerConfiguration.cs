using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class ServerConfiguration : Form
    {
        public byte[] IPAddress;
        public int port;
        public string login;
        public string password;
        public ServerConfiguration()
        {
            IPAddress = new byte[4];
            IPAddress[0] = 150;
            IPAddress[1] = 254;
            IPAddress[2] = 41;
            IPAddress[3] = 195;
            port = 8080;
            login = "kuba";
            password = "noga111";
            InitializeComponent();
        }

        private void IP1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress[0] = byte.Parse(IP1.Text);
            }
            catch
            {
                if (IP1.Text != "")
                MessageBox.Show("Adres IP może zawierać tylko liczby z zakresu 0-255");
            }
        }

        private void IP2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress[1] = byte.Parse(IP2.Text);
            }
            catch
            {
                if (IP2.Text != "")
                MessageBox.Show("Adres IP może zawierać tylko liczby z zakresu 0-255");
            }
        }

        private void IP3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress[2] = byte.Parse(IP3.Text);
            }
            catch
            {
                if (IP3.Text != "")
                MessageBox.Show("Adres IP może zawierać tylko liczby z zakresu 0-255");
            }
        }

        private void IP4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress[3] = byte.Parse(IP4.Text);
            }
            catch
            {
                if(IP4.Text != "")
                    MessageBox.Show("Adres IP może zawierać tylko liczby z zakresu 0-255");
            }
        }

        private void port_tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                port = int.Parse(port_tb.Text);
            }
            catch
            {
                if(port_tb.Text != "")
                    MessageBox.Show("Port musi być liczbą");
            }
        }

        private void login_tb_TextChanged(object sender, EventArgs e)
        {
            login = login_tb.Text;
        }

        private void pwd_tb_TextChanged(object sender, EventArgs e)
        {
            password = pwd_tb.Text;
        }

        private void ServerConfiguration_Load(object sender, EventArgs e)
        {

        }

        private void hide_bt_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
