using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public string ExtractDependencyOrder(List<string> packages)
        {
            if (!ValidateList(packages))
            {
                throw new PackageInstallerException($"Invalid dependency format: [{packages.ToString('|')}]");
            }
            return TopologicalSort(packages, new List<string>());
        }

        private string TopologicalSort(List<string> packages, List<string> orderedPackages)
        {
            var recurse = false;
            packages = packages.OrderBy(element => element.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries).Length).ToList();
            for (var i = 0; i < packages.Count; i++)
            {
                var dependencies = packages[i].Split(new string[] { ": " }, System.StringSplitOptions.RemoveEmptyEntries);
                if (dependencies.Length == 1)
                {
                    orderedPackages.Add(dependencies.First());
                    // remove package from list
                    packages[i] = "";
                }
                else if (dependencies.Length > 1)
                {
                    recurse = true;
                    var dependency = dependencies.Last();
                    if (orderedPackages.Contains(dependency))
                    {
                        // clear dependency since it already exists within filtered collection
                        dependencies[dependencies.Length - 1] = "";
                        // reset packages index to new dependency settings
                        packages[i] = dependencies.ToString(':');
                    }
                }
            }
            return recurse ? TopologicalSort(packages, orderedPackages) : orderedPackages.ToString(',');
        }
    }
}