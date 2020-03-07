// <copyright file="AuthenticationTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="Authentication"/> class.
    /// </summary>
    [TestClass]
    public class AuthenticationTests
    {
        private static GPodderClient client;

        /// <summary>
        /// Initializes necessary components for tests.
        /// </summary>
        /// <param name="testContext">The <see cref="TestContext"/> for the unit tests.</param>
        [ClassInitialize]
        public static void InitTests(TestContext testContext)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddUserSecrets<AuthenticationTests>();
            var configuration = configBuilder.Build();
            client = new GPodderClient(configuration["GpodderUsername"], configuration["GPodderPassword"]);
        }

        /// <summary>
        /// Tests the <see cref="Authentication.Login"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestLogin()
        {
            await client.Authentication.Login();
        }

        /// <summary>
        /// Tests the <see cref="Authentication.Logout"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestLogOut()
        {
            await client.Authentication.Login();
            await client.Authentication.Logout();
        }
    }
}
