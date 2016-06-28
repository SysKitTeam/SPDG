using System.Text.RegularExpressions;

namespace Acceleratio.SPDG.Generator.Utilities
{
    public static class Path
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
    }
}
