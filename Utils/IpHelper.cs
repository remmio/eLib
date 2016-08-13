using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace eLib.Utils
{
    //http://stackoverflow.com/questions/3253701/get-public-external-ip-address

    public static class IpHelper
    {
        public static async Task<string> GetPublicIp()
        {
            try
            {
                return (await new WebClient().DownloadStringTaskAsync(new Uri("http://checkip.amazonaws.com/"))).Replace("\n", "");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetLanIp()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
        }

        public static bool IsOnline() => NetworkInterface.GetIsNetworkAvailable();

        internal static string GetLocalIPv4(NetworkInterfaceType type = NetworkInterfaceType.Ethernet)
        {
            var output = default(string);
            foreach (var ip in from item in NetworkInterface.GetAllNetworkInterfaces()
                where item.NetworkInterfaceType == type && item.OperationalStatus == OperationalStatus.Up
                select item.GetIPProperties()
                into adapterProperties
                where adapterProperties.GatewayAddresses.FirstOrDefault() != null
                from ip in adapterProperties.UnicastAddresses.Where(
                    ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)
                select ip)
                output = ip.Address.ToString();

            return output;
        }

       public static string Test()
       {
           var ip1 = GetLanIp();
           var ip2 = GetPublicIp().Result;
           var lanIp = GetLocalIPv4();
            var wifIp = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            var isonline = IsOnline();
            return ip1 + ip2 + lanIp + wifIp + isonline;
       }
    }
}
