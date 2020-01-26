// <copyright file="Authentication.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using GPodder.NET.Exceptions;

    /// <summary>
    /// Handles authentication to gPodder.
    /// </summary>
    public class Authentication
    {
        private readonly ConfigurationManager configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentication"/> class.
        /// </summary>
        /// <param name="configurationManager"><see cref="ConfigurationManager"/> for settings required for requests.</param>
        public Authentication(ConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        /// <summary>
        /// Log into gPodder.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="password">Password for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous login operation.</returns>
        public async Task Login(string username, string password)
        {
            var clientConfig = await this.configurationManager.GetConfigTask();
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{clientConfig.MyGpo.BaseUrl}api/2/auth/{username}/login.json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var authResponse = await Utilities.HttpClient.SendAsync(httpRequest);
            if (authResponse.IsSuccessStatusCode)
            {
                if (!authResponse.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
                {
                    // this shouldn't happen but just in case
                    throw new Exception("Unable to retrieve 'Set-Cookie' header from response.");
                }

                Utilities.HttpClient.DefaultRequestHeaders.Add("Set-Cookie", setCookieValues);
                return;
            }

            switch (authResponse.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    // If the URL is accessed without login credentials provided
                    throw new UnauthorizedException("Login credentials were not provided or were invalid.");
                case HttpStatusCode.BadRequest:
                    // If the client provides a cookie, but for a different username than the one given
                    throw new BadRequestException("The provided session ID did not belong to the provided user.");
                default:
                    throw new GenericWebResponseException($"An error status code of {authResponse.StatusCode} was returned from gPodder.");
            }
        }

        /// <summary>
        /// Logs the user out of gPodder.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous logout operation.</returns>
        public async Task Logout(string username)
        {
            var clientConfig = await this.configurationManager.GetConfigTask();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{clientConfig.MyGpo.BaseUrl}api/2/auth/{username}/logout.json");
            var logoutResponse = await Utilities.HttpClient.SendAsync(httpRequest);
            if (logoutResponse.IsSuccessStatusCode)
            {
                Utilities.HttpClient.DefaultRequestHeaders.Remove("Set-Cookie");
                return;
            }

            switch (logoutResponse.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException("The provided session ID did not belong to the provided user.");
                default:
                    throw new GenericWebResponseException($"An error status code of {logoutResponse.StatusCode} was returned from gPodder.");
            }
        }
    }
}
