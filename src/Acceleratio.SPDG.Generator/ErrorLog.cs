using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acceleratio.SPDG.Generator
{
    public class Errors
    {
        public static void Log(Exception ex)
        {
            Acceleratio.SPDG.Generator.Log.Write("ERROR OCCURED: " + ex.Message);
        }
    }
}
