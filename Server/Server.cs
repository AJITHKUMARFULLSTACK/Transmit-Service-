using System.Net.Sockets;
using System.Net;
using System.Text;
using Microsoft.VisualBasic.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.Devices;
using System.Configuration;

namespace Server
{
    public partial class Server : Form
    {
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream networkStream;
        SaveFileDialog saveFileDialog;
        int ReceiveBufferSize = 1024;

        public Server()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Server_Start_Click(object sender, EventArgs e)
        {
            try
            {
                string ServerIP = string.Empty;
                string ServerPort = string.Empty;


                ServerIP = "172.16.16.47";
                ServerPort = "8088";

                tcpListener = new TcpListener(IPAddress.Parse(ServerIP), Int32.Parse(ServerPort));
                tcpListener.Start();
                MessageBox.Show("Server is listening on port 8088");

                tcpClient = tcpListener.AcceptTcpClient();
                networkStream = tcpClient.GetStream();
                if (tcpClient.Connected)
                {
                    MessageBox.Show("Connected to Client");
                }
                else
                {
                    MessageBox.Show("Unable to connect to Client");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("StartServer : " + ex.Message);
            }
        }

        private void Save_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient.Connected)
                {
                    saveFileDialog = new SaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)   
                    {
                        using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                        {
                            int bytesReceived;
                            byte[] receiveBuffer = new byte[ReceiveBufferSize];

                            while ((bytesReceived = networkStream.Read(receiveBuffer, 0, receiveBuffer.Length)) > 0)
                            {
                                fileStream.Write(receiveBuffer, 0, bytesReceived);
                            }
                            MessageBox.Show("File received successfully");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("TCP connection not established.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving file: " + ex.Message);
            }
        }
    }
}






