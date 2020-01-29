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
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentication"/> class.
        /// </summary>
        public Authentication()
        {
        }

        /// <summary>
        /// Log into gPodder.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="password">Password for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous login operation.</returns>
        /// <exception cref="InvalidCredentialsException">Thrown when the provided credentials were invalid.</exception>
        /// <exception cref="InvalidSessionIdException">Thrown when the provided session ID does not match the provided user.</exception>
        /// <exception cref="HttpRequestException">Thrown if any other error occurs when making the login request.</exception>
        public async Task Login(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{GPodderConfig.BaseApiUrl}/api/2/auth/{username}/login.json");
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var authResponse = await Utilities.HttpClient.SendAsync(httpRequest);
            try
            {
                authResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                switch (authResponse.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        // If the URL is accessed without login credentials provided
                        throw new InvalidCredentialsException("Login credentials were not provided or were invalid.");
                    case HttpStatusCode.BadRequest:
                        // If the client provides a cookie, but for a different username than the one given
                        throw new InvalidSessionIdException("The provided session ID did not belong to the provided user.");
                    default:
                        throw;
                }
            }

            if (authResponse.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
            {
                Utilities.HttpClient.DefaultRequestHeaders.Add("Set-Cookie", setCookieValues);
                return;
            }

            // this shouldn't happen but just in case
            throw new Exception("Unable to retrieve 'Set-Cookie' header from response.");
        }

        /// <summary>
        /// Logs the user out of gPodder.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous logout operation.</returns>
        /// <exception cref="InvalidSessionIdException">Thrown when the provided session ID does not match the provided user.</exception>
        /// <exception cref="HttpRequestException">Thrown if any other error occurs when making the login request.</exception>
        public async Task Logout(string username)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{GPodderConfig.BaseApiUrl}/api/2/auth/{username}/logout.json");
            var logoutResponse = await Utilities.HttpClient.SendAsync(httpRequest);

            try
            {
                logoutResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                switch (logoutResponse.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new InvalidSessionIdException("The provided session ID did not belong to the provided user.");
                    default:
                        throw;
                }
            }

            Utilities.HttpClient.DefaultRequestHeaders.Remove("Set-Cookie");
        }
    }
}
