using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Threading;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        TcpListener tcp_Listener;
        private const int BufferSize = 1024;
        private bool isRunning = true;
        private Timer fileTransferTimer;
        private EventLog eventLog;

        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Service1()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }
            eventLog.Source = "MySource";
            eventLog.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                log.Info("Service Started");

                log.Info("Initializing TCP Listener...");
                InitializeTcpListener();

                log.Info("TCP Listener initialized successfully.");
            }
            catch (Exception ex)
            {
                log.Error("Error in OnStart: " + ex.Message);
                throw; // This will allow Windows to catch the exception and print it in the event log.
            }
        }

        protected override void OnStop()
        {
            try
            {
                log.Info("Service Stopped");
                isRunning = false;
                tcp_Listener.Stop();
                fileTransferTimer.Dispose();
            }
            catch (Exception ex)
            {
                log.Error("Error in OnStop: " + ex.Message);
                throw; // This will allow Windows to catch the exception and print it in the event log.
            }
        }

        private void InitializeTcpListener()
        {
            try
            {
                string serverIp = ConfigurationManager.AppSettings["TCP_IP_EJSERVER"];
                int serverPort = int.Parse(ConfigurationManager.AppSettings["TCP_PORT_EJSERVER"]);
                int maxConnections = int.Parse(ConfigurationManager.AppSettings["MAX_CON"]);

                log.Info($"Server IP: {serverIp}, Port: {serverPort}, Max Connections: {maxConnections}");

                tcp_Listener = new TcpListener(IPAddress.Parse(serverIp), serverPort);
                log.Info("TCP Listener created successfully.");
                tcp_Listener.Start();
                log.Info("TCP Listener started successfully.");

                Thread thread = new Thread(() =>
                {
                    try
                    {
                        CreateListenerInstance(maxConnections);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error in thread: " + ex.Message);
                    }
                });
                thread.Start();
                log.Info("Listener thread started successfully.");
            }
            catch (Exception ex)
            {
                log.Error("Error in InitializeTcpListener: " + ex.Message);

                throw; // This will allow Windows to catch the exception and print it in the event log.
            }
        }

        private void CreateListenerInstance(int noInstance)
        {
            try
            {
                for (int i = 0; i < noInstance; i++)
                {
                    Thread listenerThread = new Thread(new ThreadStart(ListenForClients));
                    listenerThread.IsBackground = true;
                    listenerThread.Start();
                    log.Info("LISTENER THREAD STARTED...");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in CreateListenerInstance: " + ex.Message);
                throw; // This will allow Windows to catch the exception and print it in the event log.
            }
        }

        private void ListenForClients()
        {
            try
            {
                while (isRunning)
                {
                    log.Info("CLIENT STARTED...");
                    using (Socket socketForClient = tcp_Listener.AcceptSocket())
                    {
                        NetworkStream network = new NetworkStream(socketForClient);
                        ReceiveFile(network);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error in ListenForClients: " + ex.Message);
            }
        }

        private void ReceiveFile(NetworkStream networkStream)
        {
            try
            {
                byte[] buffer = new byte[BufferSize];
                int bytesRead;
                int totalBytesRead = 0;
                log.Info("Network Stream File Spotted...");
                string saveDirectory = ConfigurationManager.AppSettings["SAVE_DIRECTORY"];
                string saveFileName = Path.Combine(saveDirectory, "ReceivedFile.txt");

                using (FileStream fileStream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write))
                {
                    while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                    }
                }

                // File received successfully
            }
            catch (Exception ex)
            {
                log.Info("Error in ReceiveFile: " + ex.Message);
            }
        }

        private void FileTransferCallback(object state)
        {
            try
            {
                log.Info("Time Start Hitting ...");
                string timeToTransfer = ConfigurationManager.AppSettings["TRANSFER_TIME"];
                string currentDateTime = DateTime.Now.ToString("HH:mm");
                if (timeToTransfer == currentDateTime)
                {
                    string filePath = ConfigurationManager.AppSettings["FILE_TO_SHARE"];
                    SendFile(filePath);
                    log.Info("File Saved in the Path ...");
                }
            }
            catch (Exception ex)
            {
                log.Info("Error in FileTransferCallback: " + ex.Message);
            }
        }

        private void SendFile(string filePath)
        {
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    string serverIp = ConfigurationManager.AppSettings["TCP_IP_EJSERVER"];
                    int serverPort = int.Parse(ConfigurationManager.AppSettings["TCP_PORT_EJSERVER"]);
                    log.Info("IP & PORT Connected");
                    tcpClient.Connect(serverIp, serverPort);

                    using (NetworkStream networkStream = tcpClient.GetStream())
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[BufferSize];
                        int bytesRead;

                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            networkStream.Write(buffer, 0, bytesRead);
                            log.Info("File Started Writing...");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error in SendFile: " + ex.Message);
            }
        }
    }
}