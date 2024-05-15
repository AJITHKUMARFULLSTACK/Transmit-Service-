using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Tranzmit___Client
{
    public partial class Client : Form
    {
        TcpClient tcpClient1;
        NetworkStream networkStream;
        OpenFileDialog op;
        int SendBufferSize = 1024;

        public Client()
        {
            InitializeComponent();
        }

        private void Connet_Click_1(object sender, EventArgs e)
        {
            try
            {
                tcpClient1 = new TcpClient("172.16.16.19", 8088);
                networkStream = tcpClient1.GetStream();
                if (tcpClient1.Connected)
                {
                    MessageBox.Show("CONNECTED TO THE LISTENER");
                }
                else
                {
                    MessageBox.Show("Unable to establish TCP Connection");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ConnectTCP : " + ex.Message);
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = op.FileName;
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient1.Connected)
                {
                    using (FileStream fileStream = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read))
                    {
                        int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fileStream.Length) / SendBufferSize));
                        byte[] SendingBuffer = new byte[SendBufferSize];
                        int TotalLength = (int)fileStream.Length, CurrentPacketLength;

                        for (int j = 0; j < NoOfPackets; j++)
                        {
                            CurrentPacketLength = fileStream.Read(SendingBuffer, 0, SendBufferSize);
                            networkStream.Write(SendingBuffer, 0, CurrentPacketLength);
                        }
                        MessageBox.Show("File sent successfully");
                    }
                }
                else
                {
                    MessageBox.Show("TCP connection not established.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending file: " + ex.Message);
            }
        }

        
    }
}
