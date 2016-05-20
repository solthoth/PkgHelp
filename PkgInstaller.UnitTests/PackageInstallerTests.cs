using NUnit.Framework;
using PkgInstaller;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;

namespace PkgInstaller.Tests
{
    [TestFixture()]
    [Parallelizable]
    public class PackageInstallerTests
    {
        private List<string> simplePackages;
        private List<string> validPackages;
        private List<string> invalidPackages;

        [SetUp]
        public void InitPackages()
        {
            simplePackages = new List<string> {
                "KittenService: CamelCaser",
                "CamelCaser: "
            };
            validPackages = new List<string> {
                "KittenService: ",
                "Leetmeme: Cyberportal",
                "Cyberportal: Ice",
                "CamelCaser: KittenService",
                "Fraudstream: Leetmeme",
                "Ice: "
            };
            invalidPackages = new List<string> {
                "KittenService: ",
                "Leetmeme: Cyberportal",
                "Cyberportal: Ice",
                "CamelCaser: KittenService",
                "Fraudstream: ",
                "Ice: Leetmeme"
            };
        }

        [Test()]
        public void Given_A_Valid_String_When_ValidateInput_Then_Returns_True()
        {
            var installer = new PackageInstaller();

            var result = installer.ValidateInput(simplePackages.First());

            result.Should().BeTrue();
        }
    }
}