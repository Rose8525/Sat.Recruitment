using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Api;
using Sat.Recruitment.Test.TestUtils;
using System;
using System.Net.Http;

namespace Sat.Recruitment.Test.IntegrationTests
{
    public class TestOneTimeSetup : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        public HttpClient HttpClient { get; }

        public TestOneTimeSetup()
        {
            UtilsTest.Init("Users.txt");
            _webApplicationFactory = new WebApplicationFactory<Startup>();
            HttpClient = _webApplicationFactory.CreateClient();
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _webApplicationFactory.Dispose();
        }
    }
}
