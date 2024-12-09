using System.Net.Sockets;
using System.Net;

namespace ServerBrowser
{
    public static class Global
    {
        public static bool[] ServerIsOnline = new bool[256];
        public static int[] ServerPlayerCount = new int[256];
        public static int[] ServerTimestamp = new int[256];
        public static DateTime[] ServerLatestActive = new DateTime[256];

        private static Thread? ServerListener;
        public static void Listen()
        {
            int ListenPort = 36114;

            UdpClient udpServer = new UdpClient(ListenPort);
            Console.WriteLine("Listening..");
            for (; ; )
            {
                while (udpServer.Available > 0)
                {
                    IPEndPoint ClientEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = udpServer.Receive(ref ClientEP);
                    if (data.Length == 4)
                    {
                        int ServerID = data[0];
                        int Timestamp = data[1];
                        int PlayerCount = data[2];
                        if (ServerID < 256)
                        {
                            ServerIsOnline[ServerID] = true;
                            ServerLatestActive[ServerID] = DateTime.Now;
                            if (Timestamp > ServerTimestamp[ServerID])
                            {
                                ServerTimestamp[ServerID] = Timestamp;
                                ServerPlayerCount[ServerID] = PlayerCount;
                                Console.WriteLine($"Received len({data.Length}), server {ServerID}  from {ClientEP.Address.ToString()}:{ClientEP.Port}");
                            }
                            else
                            {
                                Console.WriteLine($"Old Timestamp discarded. {Timestamp} < {ServerTimestamp[ServerID]} for server {ServerID}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Warning! ServerID out of bounds {ServerID}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Warning! Incorrect Length! {data.Length}");
                    }
                }

                //
                // Mark dead clients as offline
                //

                for (int ServerID = 0; ServerID < 256; ++ServerID)
                {
                    if (ServerIsOnline[ServerID])
                    {
                        if (DateTime.Now - ServerLatestActive[ServerID] > TimeSpan.FromSeconds(5))
                        {
                            ServerIsOnline[ServerID] = false;
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
