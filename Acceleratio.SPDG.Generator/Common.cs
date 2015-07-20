using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

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

        
    }

    
}
