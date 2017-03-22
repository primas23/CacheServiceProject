using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using CSP.CacheService;

namespace CSP.Tests.HttpCacheServiceTests
{
    [TestClass]
    public class GetShould
    {
        [TestMethod]
        public void ThrowNullException_WhenItemNameIsNull()
        {
            // Arrange
            HttpCacheService cacheService = new HttpCacheService();
            Func<string> simpleString = () => "asdasdasdsbggagas";

            // Act and Assert
            ThrowsAssert.Throws<ArgumentNullException>(() => cacheService.Get(null, simpleString, 300));
        }

        [TestMethod]
        public void ReturnCachedItem_WhenItemIsValid()
        {
            // Arrange
            HttpCacheService cacheService = new HttpCacheService();
            Func<string> simpleString = () => "asdasdasdsbggagas";
            const string name = "asdasd";

            // Act
            string result = cacheService.Get(name, simpleString, 300);

            // Assert
            Assert.AreEqual(simpleString.Invoke(), result);
        }

        [TestMethod]
        public void ReturnCachedItem_WhenItemIsCalledTwoTime()
        {
            // Arrange
            Func<string> simpleString = () => "asdasdasdsbggagas";
            const string name = "asdasd";

            // Act
            string temp = new HttpCacheService().Get(name, simpleString, 300);
            string result = new HttpCacheService().Get(name, simpleString, 300);

            // Assert
            Assert.AreEqual(simpleString.Invoke(), result);
        }

        [TestMethod]
        public void ReturnCachedItem_WhenItemIsChagedBeforeTheSecondCall()
        {
            // Arrange
            Func<string> simpleString = () => "asdasdasdsbggagas";
            Func<string> simpleStringSecond = () => "asdasdasdsbggagas2332323253235";
            const string name = "asdasd";

            // Act
            string temp = new HttpCacheService().Get(name, simpleString, 300);
            string result = new HttpCacheService().Get(name, simpleStringSecond, 300);

            // Assert
            Assert.AreEqual(simpleString.Invoke(), result);
        }

        [TestMethod]
        public void ReturnNUllItem_WhenDataIsNull()
        {
            // Arrange
            Func<string> simpleString = () => null;
            const string name = "asdasd";

            // Act
            string result = new HttpCacheService().Get(name, simpleString, 300);

            // Assert
            Assert.AreEqual(simpleString.Invoke(), result);
        }
    }
}
