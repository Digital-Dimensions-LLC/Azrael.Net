using Microsoft.VisualStudio.TestTools.UnitTesting;
using Azrael.Net.Api;
using Azrael.Net.Data;
using Azrael.Net;
using Xunit;
using System;

using Assert = Xunit.Assert;


namespace Azrael.Net.Test
{

    [TestClass]
    public class AzraelTest
    {
        private readonly string apiKey = Utility.GetToken("token.txt");
        private readonly string testProofLink = @"https://cdn.azrael.gg/uploads/null.png";

        [TestMethod]
        [Theory]
        [InlineData("548009285892833280", true)]  // Dev ban
        [InlineData("641795527444529152", false)] // Api Owner
        [InlineData("341275030941859850", false)] // Self
        [InlineData("889846992459694080", true)] // 3rd party raid
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
        [InlineData("889846992459694080", true)] // 3rd party raid
        public async void TestGetBan(string id, bool banned)
        {
            BanRecord _testValue = await AzraelAPI.GetBan(id, apiKey);
            Assert.Equal(_testValue.Banned, banned);
            if (_testValue.Banned)
                Assert.NotNull(_testValue.BanData);
        }

        [TestMethod]
        [Theory]
        [InlineData("882121392030625883")] // Equilateral
        public async void TestBanAdd(string id)
        {
            try
            {
                var record = await AzraelAPI.AddBan(id, apiKey, 5, testProofLink);
                Assert.Equal(200, record.Status);
            }
            catch
            {

            }
            finally
            {
                var unban = await AzraelAPI.DeleteBan(id, apiKey, "i'm a good noodle boy");
            }
                      
        }


    }
}
