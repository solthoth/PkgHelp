using System.Collections.Generic;
using System.Linq;

namespace PkgInstaller
{
    public class PackageInstaller
    {
        public List<string> Packages { get; set; }

        public bool ValidateInput(string package)
        {
            return package.Split(new string[] { ": " }, System.StringSplitOptions.None).Length > 1;            
        }

        public bool ValidateList(List<string> packages)
        {
            return packages.All(ValidateInput);
        }
    }
}
