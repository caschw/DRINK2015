using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Drink2015.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t = new DRINK2015.PartyConfiguration().GetUpdatedPartyList();
            var s = 10;
            Assert.IsTrue(true);
        }
    }
}
