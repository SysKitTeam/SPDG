using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Acceleratio.SPDG.Generator;

namespace Acceleratio.SPDG.UI
{
    static class Program
    {

        private static void Restart()
        {
            Process.Start(Application.ExecutablePath);
            Environment.Exit(-1);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            EnsureCorrectRuntime();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm01Connect(true));
                        
        }

        private static void EnsureCorrectRuntime()
        {
            if (Debugger.IsAttached)
            {
                return;
            }
            var version = DataGenerator.GetSharePointOnPremVersion();

            int prefferedTargetRuntime = 4;
            if (version != null && version.Major < 15)
            {
                //for SharePoint 2010 we MUST run under the CLR 2.0
                prefferedTargetRuntime = 2;
            }

            string configFileName = Process.GetCurrentProcess().ProcessName + ".exe.config";          
            //have to use pure xml and not configuration manager to modify this section because it is readOnly  
            XDocument doc = XDocument.Load(configFileName);
            var startupNode = (from q in doc.Descendants()
                where q.Name == "startup"
                select q).First();

            int currentPrefferedRuntime;
            if (startupNode.Elements().First().Attribute("version").Value == "v4.0")
            {
                currentPrefferedRuntime = 4;
            }
            else
            {
                currentPrefferedRuntime = 2;
            }

            //on SharePoint 2010 if we have .net 4.0 installed we still must preffer the .net 2.0 runtime
            //otherwise the SharePoint farm will not be accessible
            if (currentPrefferedRuntime != prefferedTargetRuntime)
            {
                if (prefferedTargetRuntime == 4)
                {
                    startupNode.Elements().First().Attribute("version").Value = "v4.0";
                    startupNode.Elements().ElementAt(1).Attribute("version").Value = "v2.0.50727";
                }
                else
                {
                    startupNode.Elements().First().Attribute("version").Value = "v2.0.50727";
                    startupNode.Elements().ElementAt(1).Attribute("version").Value = "v4.0";
                }
                doc.Save(configFileName, SaveOptions.None);
                Restart();                
            }            
        }
    }
}
