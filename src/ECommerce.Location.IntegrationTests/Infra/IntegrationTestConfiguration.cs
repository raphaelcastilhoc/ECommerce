using ECommerce.Location.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ECommerce.Location.IntegrationTests.Infra
{
    [TestClass]
    public class IntegrationTestConfiguration : BaseIntegrationTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Initialize<Startup>();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            Dispose();
        }
    }
}
