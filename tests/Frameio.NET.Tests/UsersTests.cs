﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Frameio.NET.Models;
using Newtonsoft.Json;
using Xunit;

namespace Frameio.NET.Tests
{
    public class UsersTests
    {
        [Fact]
        public async Task GetCurrentUser_Should_Return_ExpectedUser()
        {
            string userAccountId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            string userEmail = "name@domain.com";
            string userName = "Current User";

            User user = new User();
            user.Id = userId;
            user.AccountId = userAccountId;
            user.Email = userEmail;
            user.Name = userName;

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };

            FakeHttpMessageHandler fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);

            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            fakeHttpClient.BaseAddress = new Uri("http://Fake.domain.com");
            ApiClient client = new ApiClient(fakeHttpClient);

            Users usersClient = new Users(client);
            User userResponse = await usersClient.GetCurrentUser();

            Assert.NotNull(userResponse);
            Assert.Equal(userId, userResponse.Id);
            Assert.Equal(userAccountId, userResponse.AccountId);
            Assert.Equal(userEmail, userResponse.Email);
            Assert.Equal(userName, userResponse.Name);
        }
    }
}
