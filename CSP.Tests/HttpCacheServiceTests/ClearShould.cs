using System;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSP.CacheService;

namespace CSP.Tests.HttpCacheServiceTests
{
    [TestClass]
    public class ClearShould
    {
        [TestMethod]
        public void ClearAll_WhenCalled()
        {
            // Arrange
            Func<string> simpleString = () => "ClearAll_WhenCalleddasdasdasdsbggagas";
            const string name = "ClearAll_WhenCalledasdasd";

            // Act
            new HttpCacheService().Get(name, simpleString, 300);
            new HttpCacheService().Get(name + name, simpleString, 300);
            new HttpCacheService().Clear();

            // Assert
            Assert.AreEqual(0, HttpRuntime.Cache.Count);
        }
    }
}
