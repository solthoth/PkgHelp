using System;

namespace PkgInstaller
{
    public class PackageInstallerException : Exception
    {
        public PackageInstallerException(string message) : base(message)
        { }
    }
}