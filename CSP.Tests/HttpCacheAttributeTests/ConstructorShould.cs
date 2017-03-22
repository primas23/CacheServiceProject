using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using MSTestExtensions;

using CSP.CacheService;
using CSP.Common.Contracts;

namespace CSP.Tests.HttpCacheAttributeTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void AssignDuration_WhenDurationIsSupplied()
        {
            // Arrange
            int duration = 300;
            HttpCacheAttribute attribute = new HttpCacheAttribute(duration);

            // Act and Assert
            Assert.AreEqual(attribute.Duration, duration);
        }

        [TestMethod]
        public void AssignDefaultDuration_WhenDurationIsNotSupplied()
        {
            // Arrange
            HttpCacheAttribute attribute = new HttpCacheAttribute();

            // Act and Assert
            Assert.IsNotNull(attribute.Duration);
        }

        [TestMethod]
        public void AssignCorrectCacheService_WhenCacheServiceIsSupplied()
        {
            // Arrange
            var mockService = new Mock<ICacheService>();
            HttpCacheAttribute attribute = new HttpCacheAttribute(300, mockService.Object);
            
            // Act and Assert
            Assert.AreSame(attribute.CacheService, mockService.Object);
        }

        [TestMethod]
        public void AssignDefaultCacheService_WhenCacheServiceIsNotSupplied()
        {
            // Arrange
            HttpCacheAttribute attribute = new HttpCacheAttribute();

            // Act and Assert
            Assert.IsNotNull(attribute.CacheService);
        }

        [TestMethod]
        public void ThrowNulReference_WhenCacheServiceNotSupplied()
        {
            // Arrange, Act and Assert
            ThrowsAssert.Throws<ArgumentNullException>(() => new HttpCacheAttribute(300, null));
        }
    }
}
