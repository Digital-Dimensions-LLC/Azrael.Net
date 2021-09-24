using Microsoft.VisualStudio.TestTools.UnitTesting;
using Azrael.Net.Api;
using Azrael.Net.Data;
using Xunit;

using Assert = Xunit.Assert;


namespace Azrael.Net.Test
{

    [TestClass]
    public class AzraelTest
    {
        private readonly string apiKey = Utility.GetToken("token.txt");

        [TestMethod]
        [Theory]
        [InlineData("548009285892833280", true)]  // Dev ban
        [InlineData("641795527444529152", false)] // Api Owner
        [InlineData("341275030941859850", false)] // Self
        public async void TestCheckBan(string id, bool banned)
        {
            bool _testValue = await AzraelAPI.CheckBan(id, apiKey);
            Assert.Equal(_testValue, banned);
        }
        [TestMethod]
        [Theory]
        [InlineData("548009285892833280", true)]  // Dev ban
        [InlineData("641795527444529152", false)] // Api Owner
        [InlineData("341275030941859850", false)] // Self
        public async void TestGetBan(string id, bool banned)
        {
            BanRecord _testValue = await AzraelAPI.GetBan(id, apiKey);
            Assert.Equal(_testValue.Banned, banned);
            if (_testValue.Banned)
                Assert.NotNull(_testValue.BanData);
        }
    }
}
