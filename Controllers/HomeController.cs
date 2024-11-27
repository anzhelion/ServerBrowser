using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ServerBrowser.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public bool[]      ServerIsOnline      = new bool[256];
        public int[]       ServerPlayerCount   = new int[256];
        public int[]       ServerTimestamp     = new int[256];
        public DateTime[]  ServerLatestActive  = new DateTime[256];

        private static Thread ?ServerListener;
        public void Listen()
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

        public HomeController(ILogger<HomeController> logger)
        {
            ServerListener = new Thread(new ThreadStart(Listen));
            ServerListener.Start();

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
