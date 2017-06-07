using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using W2VPClient;

namespace ClientApplication
{
    public partial class MainApp : Form
    {
        W2VPC client;
        ServerConfiguration configurationForm = new ServerConfiguration();
        bool connected = false;
        public MainApp()
        {
            InitializeComponent();
            disconnectToolStripMenuItem.Enabled = false;
            client = new W2VPC(new System.Net.IPAddress(configurationForm.IPAddress), configurationForm.port);
            client.Auth += client_Auth;
            client.Logout_event += client_Logout_event;
            client.RecieveModel += client_RecieveModel;
            client.UpdateReadingProgress += client_UpdateReadingProgress;
        }

        


        #region cross thread
        delegate void changeConnectsDel();
        void changeConnects()
        {
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
        }
        delegate void updateModelListDel(string name);
        void updateModelList(string name)
        {
            lb_models.Items.Add(name);
        }
        delegate void updateConnectedNameDel(string name);
        void updateConnectedName(string name)
        {
            connected_label.Text = "Connected as " + name;
        }
        
        delegate void updateProgressDel(int current, int size);
        void updateProgress(int current, int size)
        {
            readingProgress.Value = (current * 100) / size;
        }
        #endregion
        #region events
        void client_RecieveModel(string modelName)
        {
            updateModelListDel f = new updateModelListDel(updateModelList);
            this.Invoke(f,modelName);
        }
        void client_Logout_event()
        {
            changeConnectsDel f = new changeConnectsDel(changeConnects);
            this.Invoke(f);
            connected_label.Text = "Disconnected";
        }

        void client_Auth(string login)
        {
            updateConnectedNameDel f = new updateConnectedNameDel(updateConnectedName);
            this.Invoke(f, login);
            client.GetModels();
        }
        void client_UpdateReadingProgress(int current, int size)
        {
            updateProgressDel f = new updateProgressDel(updateProgress);
            this.Invoke(f, current, size);
        }
        #endregion
        private void configureServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configurationForm.Show();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!client.Connect())
            {
                MessageBox.Show("Server is unavailable");
                return;
            }
            connected = true;
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;
            client.Login(configurationForm.login, configurationForm.password);
        }
        
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Logout();
        }

        private void bt_readModel_Click(object sender, EventArgs e)
        {
            if(lb_models.SelectedItems.Count == 1)
            {
                client.ReadModel((string)lb_models.Items[lb_models.SelectedIndex]);
            }
            else
            {
                MessageBox.Show("You can read only one model at once");
            }
        }
    }
}
