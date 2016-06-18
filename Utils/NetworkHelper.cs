using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace eLib.Utils
{
    public partial class NetworkHelper
    {

        private static readonly List<Ping> Pingers = new List<Ping>();
        private static int _instances;

        private static readonly object Lock = new object();

        public static int Result;
        private static readonly int TimeOut = 250;

        private static readonly int Ttl = 5;

        public static void Start()
        {
            const string baseIp = "192.168.1.";

            CreatePingers(255);

            var po = new PingOptions(Ttl, true);
            var enc = new ASCIIEncoding();
            var data = enc.GetBytes("abababababababababababababababab");

            var wait = new SpinWait();
            var cnt = 1;

            foreach (var p in Pingers)
            {
                lock (Lock)
                    _instances += 1;

                p.SendAsync(string.Concat(baseIp, cnt.ToString()), TimeOut, data, po);
                cnt += 1;
            }

            while (_instances > 0)
                wait.SpinOnce();

            DestroyPingers();


            //foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            //{
            //    Console.WriteLine("Name: " + netInterface.Name);
            //    Console.WriteLine("Description: " + netInterface.Description);
            //    Console.WriteLine("Addresses: ");
            //    IPInterfaceProperties ipProps = netInterface.GetIPProperties();
            //    foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
            //    {
            //        Console.WriteLine(" " + addr.Address.ToString());
            //    }
            //    Console.WriteLine("");
            //}
        }

        public static void Ping_completed(object s, PingCompletedEventArgs e)
        {
            lock (Lock)
                _instances -= 1;

            if (e.Reply.Status == IPStatus.Success)
            {
                Console.WriteLine(string.Concat("Active IP: ", e.Reply.Address.ToString()));
                Result += 1;
            }
        }


        private static void CreatePingers(int cnt)
        {
            for (var i = 1; i <= cnt; i++)
            {
                var p = new Ping();
                p.PingCompleted += Ping_completed;
                Pingers.Add(p);
            }
        }

        private static void DestroyPingers()
        {
            foreach (var p in Pingers)
            {
                p.PingCompleted -= Ping_completed;
                p.Dispose();
            }

            Pingers.Clear();
        }

    }


   public partial class NetworkHelper
    {
       private static CountdownEvent _countdown;
       private static int _upCount;
       private static readonly object LockObj = new object();
        private static List<KeyValuePair<string, string>> _ips = new List<KeyValuePair<string, string>>();

        public static void Search()
        {
            _countdown = new CountdownEvent(1);
            var sw = new Stopwatch();
            sw.Start();
            const string ipBase = "10.22.4.";
            for (var i = 1; i < 255; i++)
            {
                var ip = ipBase + i;

                var p = new Ping();
                p.PingCompleted += p_PingCompleted;
                _countdown.AddCount();
                p.SendAsync(ip, 100, ip);
            }
            _countdown.Signal();
            _countdown.Wait();
            sw.Stop();
            Console.WriteLine("Took {0} milliseconds. {1} hosts active.", sw.ElapsedMilliseconds, _upCount);
            Console.ReadLine();
        }

       private static void p_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var ip = (string)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                string name;
                try
                {
                    var hostEntry = Dns.GetHostEntry(ip);
                    name = hostEntry.HostName;
                }
                catch (SocketException)
                {
                    name = "?";
                }
                Console.WriteLine("{0} ({1}) is up: ({2} ms)", ip, name, e.Reply.RoundtripTime);
                lock (LockObj)
                {
                    _upCount++;
                    _ips.Add(new KeyValuePair<string, string>(ip, name));
                }
            }
            else if (e.Reply == null)
            {
                Console.WriteLine("Pinging {0} failed. (Null Reply object?)", ip);
            }
            _countdown.Signal();
        }
    }


    public partial class NetworkHelper
    {
        public static List<string> StartSearch()
        {

            //Gets the machine names that are connected on LAN 

            var netUtility = new Process
            {
                StartInfo =
                {
                    FileName = "net.exe",
                    CreateNoWindow = true,
                    Arguments = "view",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                }
            };

            netUtility.Start(); 
 
            var ips = new List<string>();
 
            var streamReader = new StreamReader(netUtility.StandardOutput.BaseStream, netUtility.StandardOutput.CurrentEncoding);

            string line; 
 
            while ((line = streamReader.ReadLine()) != null) 
            {
                if (line.StartsWith("\\"))
                    ips.Add(line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ", StringComparison.Ordinal)).ToUpper());
            } 
 
            streamReader.Close();
            netUtility.WaitForExit(1000);
                return ips; 
        }

     }
}