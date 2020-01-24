// <copyright file="ConfigurationManagerTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the <see cref="ConfigurationManager"/> class.
    /// </summary>
    [TestClass]
    public class ConfigurationManagerTests
    {
        /// <summary>
        /// Tests the <see cref="ConfigurationManager"/> constructor.
        /// </summary>
        [TestMethod]
        public void TestContructor()
        {
            Task.Run(async () =>
            {
                var configManager = new ConfigurationManager();
                var clientConfig = await configManager.GetConfigTask();
                Assert.IsNotNull(clientConfig);
                Assert.IsFalse(string.IsNullOrEmpty(clientConfig.MyGpo.BaseUrl));
                Assert.IsFalse(string.IsNullOrEmpty(clientConfig.MyGpoFeedService.BaseUrl));
                Assert.IsTrue(clientConfig.UpdateTimeout > 0);
                Assert.IsTrue(clientConfig.UpdateTimeoutMilliseconds > 0);
            }).GetAwaiter().GetResult();
        }
    }
}
