using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Acceleratio.SPDG.Generator
{
    public class Utils
    {
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

        public static SecureString StringToSecureString(string someString)
        {
            SecureString secureStr = new SecureString();
            for (int i = 0; i < someString.Length; i++)
            {
                secureStr.AppendChar(someString[i]);
            }
            secureStr.MakeReadOnly();

            return secureStr;
        }

        public static int GetNextAvailablePort(int startingPort)
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

        class WinApi
        {
            public const int LOGON32_LOGON_INTERACTIVE = 2;
            public const int LOGON32_PROVIDER_DEFAULT = 0;


            [DllImport("advapi32.dll")]
            public static extern int LogonUserA(String lpszUserName,
                String lpszDomain,
                String lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                ref IntPtr phToken);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int DuplicateToken(IntPtr hToken,
                int impersonationLevel,
                ref IntPtr hNewToken);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool RevertToSelf();

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern bool CloseHandle(IntPtr handle);



            [DllImport("fusion.dll")]
            public static extern int CreateAssemblyCache(
                out IAssemblyCache ppAsmCache, int reserved);


            // GAC Interfaces - IAssemblyCache. As a sample, non used vtable entries     
            [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
            Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
            internal interface IAssemblyCache
            {
                int Dummy1();
                [PreserveSig()]
                int QueryAssemblyInfo(
                    int flags,
                    [MarshalAs(UnmanagedType.LPWStr)]
            String assemblyName,
                    ref ASSEMBLY_INFO assemblyInfo);

                int Dummy2();
                int Dummy3();
                int Dummy4();
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct ASSEMBLY_INFO
            {
                public int cbAssemblyInfo;
                public int assemblyFlags;
                public long assemblySizeInKB;

                [MarshalAs(UnmanagedType.LPWStr)]
                public String currentAssemblyPath;

                public int cchBuf;
            }
        }

        private static WindowsImpersonationContext _impersonationContext;

        public static bool ImpersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (WinApi.RevertToSelf())
            {
                if (WinApi.LogonUserA(userName, domain, password, WinApi.LOGON32_LOGON_INTERACTIVE, WinApi.LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (WinApi.DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        _impersonationContext = tempWindowsIdentity.Impersonate();
                        if (_impersonationContext != null)
                        {
                            WinApi.CloseHandle(token);
                            WinApi.CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                WinApi.CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                WinApi.CloseHandle(tokenDuplicate);
            return false;
        }

        public static void UndoImpersonation()
        {
            _impersonationContext.Undo();
        }


        // If assemblyName is not fully qualified, a random matching may be 
        public static String QueryAssemblyInfo(String assemblyName)
        {
            WinApi.ASSEMBLY_INFO assembyInfo = new WinApi.ASSEMBLY_INFO();
            assembyInfo.cchBuf = 512;
            assembyInfo.currentAssemblyPath = new String('\0',
                assembyInfo.cchBuf);

            WinApi.IAssemblyCache assemblyCache = null;

            // Get IAssemblyCache pointer
            int hr = WinApi.CreateAssemblyCache(out assemblyCache, 0);
            if (hr == 0)
            {
                hr = assemblyCache.QueryAssemblyInfo(1, assemblyName, ref assembyInfo);
                if (hr != 0)
                {
                    Marshal.ThrowExceptionForHR(hr);
                }
            }
            else
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            return assembyInfo.currentAssemblyPath;
        }
    }

    public static class ListExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}