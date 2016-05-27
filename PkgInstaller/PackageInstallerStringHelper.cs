using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PkgInstaller
{
    public static class PackageInstallerStringHelper
    {
        public static string ToString(this string[] stringArray, char delimiter)
        {
            var outputString = "";
            foreach (var element in stringArray)
            {
                outputString += $"{element}{delimiter} ";
            }
            return outputString.Remove(outputString.LastIndexOf($"{delimiter} "));
        }

        public static string ToString(this List<string> stringList, char delimtier)
        {
            return $"{stringList.ToArray().ToString(delimtier)}";
        }
    }
}
