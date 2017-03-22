using System;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSP.CacheService;

namespace CSP.Tests.HttpCacheServiceTests
{
    [TestClass]
    public class RemoveShould
    {
        [TestMethod]
        public void RemoveCorrect_WhenCorrectIsSupplied()
        {
            // Arrange
            Func<string> simpleString = () => "asdasdasdsbggagas";
            const string name = "asdasd";

            // Act
            new HttpCacheService().Get(name, simpleString, 300);
            new HttpCacheService().Remove(name);

            // Assert
            Assert.IsNull(HttpRuntime.Cache[name]);
        }

        [TestMethod]
        public void NotRemove_WhenDifferentNameIsSupplied()
        {
            // Arrange
            Func<string> simpleString = () => "asdasdasdsbggagas";
            const string name = "asdasd";

            // Act
            new HttpCacheService().Get(name, simpleString, 300);
            new HttpCacheService().Remove(name + "721");

            // Assert
            Assert.IsNotNull(HttpRuntime.Cache[name]);
        }

        [TestMethod]
        public void NotRemove_WhenNameIsNotSupplied()
        {
            // Arrange
            Func<string> simpleString = () => "asdasdasdsbggagas";
            const string name = "asdasd";

            // Act
            new HttpCacheService().Get(name, simpleString, 300);
            new HttpCacheService().Remove(null);

            // Assert
            Assert.IsNotNull(HttpRuntime.Cache[name]);
        }
    }
}
