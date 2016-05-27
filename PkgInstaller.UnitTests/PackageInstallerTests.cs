using NUnit.Framework;
using PkgInstaller;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;

namespace PkgInstaller.Tests
{
    [TestFixture()]
    [Parallelizable(ParallelScope.Children)]
    public class PackageInstallerTests
    {
        private List<string> simplePackages;
        private List<string> validPackages;
        private List<string> validPackagesBadFormat;
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
            validPackagesBadFormat = new List<string> {
                "KittenService:",
                "Leetmeme:Cyberportal",
                "Cyberportal:Ice",
                "CamelCaser:KittenService",
                "Fraudstream:Leetmeme",
                "Ice:"
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

        [Test]
        public void Given_A_Valid_String_When_ValidateInput_Then_Returns_True()
        {
            var installer = new PackageInstaller();

            var result = installer.ValidateInput(simplePackages.First());

            result.Should().BeTrue();
        }

        [Test()]
        public void Given_A_Valid_String_List_When_ValidateList_Then_Returns_True()
        {
            var installer = new PackageInstaller();

            var result = installer.ValidateList(validPackages);

            result.Should().BeTrue();
        }

        [Test()]
        public void Given_A_ValidPackage_List_When_ExtractDependencyOrder_Then_Returns_Install_Order_List()
        {
            var installer = new PackageInstaller();

            var result = installer.ExtractDependencyOrder(validPackages);

            result.Should().Match("KittenService, Ice, Cyberportal, CamelCaser, Leetmeme, Fraudstream");
        }

        [Test()]
        public void Given_An_InvalidPackage_List_When_ExtractDependencyOrder_Then_Throws_InvalidPackageInstallerException()
        {
            var installer = new PackageInstaller();

            installer.Invoking(action => action.ExtractDependencyOrder(invalidPackages)).ShouldThrow<PackageInstallerException>().WithMessage("Invalid dependency references");
        }

        [Test()]
        public void Given_An_ValidPackage_List_With_Invalid_String_Format_When_ExtractDependencyOrder_Then_Throws_InvalidPackageInstallerException()
        {
            var installer = new PackageInstaller();

            installer.Invoking(action => action.ExtractDependencyOrder(validPackagesBadFormat)).ShouldThrow<PackageInstallerException>().WithMessage("Invalid dependency format: [KittenService:| Leetmeme:Cyberportal| Cyberportal:Ice| CamelCaser:KittenService| Fraudstream:Leetmeme| Ice:]");
        }
    }
}