using AutoFixture.Xunit2;
using DownloadManager.Models;
using Xunit;

namespace DownloadManager.Tests
{
    public class UnitTest1
    {
        [Theory]
        [AutoData]
        public void Test1(LoginRequest request)
        {
            request.Password = "Evan";

            Assert.Equal("Evan", request.Password);
        }
    }
}