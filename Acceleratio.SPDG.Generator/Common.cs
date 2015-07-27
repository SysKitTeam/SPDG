using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Text.RegularExpressions;

namespace Acceleratio.SPDG.Generator
{
    internal class Common
    {
        internal static SecureString StringToSecureString(string someString)
        {
            SecureString secureStr = new SecureString();
            for (int i = 0; i < someString.Length; i++)
            {
                secureStr.AppendChar(someString[i]);
            }
            secureStr.MakeReadOnly();

            return secureStr;
        }

        internal static int GetNextAvailablePort(int startingPort)
        {
            List<int> portArray;

            IPEndPoint[] endPoints;
            portArray = new List<int>();

            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            //getting active connections
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            portArray.AddRange(from n in connections
                               where n.LocalEndPoint.Port >= startingPort
                               select n.LocalEndPoint.Port);

            //getting active tcp listners - WCF service listening in tcp
            endPoints = properties.GetActiveTcpListeners();
            portArray.AddRange(from n in endPoints
                               where n.Port >= startingPort
                               select n.Port);

            //getting active udp listeners
            endPoints = properties.GetActiveUdpListeners();
            portArray.AddRange(from n in endPoints
                               where n.Port >= startingPort
                               select n.Port);

            portArray.Sort();

            for (int i = startingPort; i < UInt16.MaxValue; i++)
                if (!portArray.Contains(i))
                    return i;

            return 0;
        }

        public static string GenerateSlug(string phrase, int maxLength)
        {
            string str = phrase.ToLower();

            str = Regex.Replace(str, @"dž", "dj");
            str = Regex.Replace(str, @"ž", "z");
            str = Regex.Replace(str, @"č", "c");
            str = Regex.Replace(str, @"ć", "c");
            str = Regex.Replace(str, @"š", "s");

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();

            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();

            return str;
        }

        
    }

    
}
