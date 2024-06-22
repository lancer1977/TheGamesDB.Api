using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using PolyhydraGames.Core.Global.Helpers;

namespace PolyhydraGames.VGCollect.Test
{
    public class Tests
    {

        private string username;
        private string password;
        [SetUp]
        public void Setup()
        {
            username = "lancer1977";
            password = "3p7n1a2b";
        }
        [Test]
        public async Task Platforms()
        {
            var result = await VGCollectApi.Platforms();
            Assert.IsTrue(result.Count > 1);
        }

        [Test]
        public async Task GetData()
        {
            //var file = PolyhydraGames
            var result =await WebHelpers.GetString("https://rpggeek.com/rpgitemversion/1000/");
             result = result.Replace("\t","").Replace("\n","");
            Assert.IsTrue(result.Contains("2002"));
        }
        [Test]
        public async Task Categories()
        {
            var result = await VGCollectApi.Categories();
            Assert.IsTrue(result.Count > 1);
        }

        [Test]
        public async Task Category()
        {
            var result = await VGCollectApi.Category("57");
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Verify()
        {
            var result = await VGCollectApi.Verify(username, password);
            Assert.IsTrue(result.Result != null);
        }

        [Test]
        public async Task Collection()
        {
            var result = await VGCollectApi.Collection(username);
            Assert.IsTrue(result.Count > 10);
        }

        [Test]
        public async Task CurrentlyPlaying()
        {
            var result = await VGCollectApi.CurrentlyPlaying(username);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public async Task Search()
        {
            var result = await VGCollectApi.Search("dragon");
            Assert.IsTrue(result.Count > 0);
        }
        [Test]
        public async Task Barcode()
        {
            var result = await VGCollectApi.Search("5097100890");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public async Task Item()
        {
            var result = await VGCollectApi.Item(82687);
            Debug.WriteLine(result.Result);
            Assert.IsTrue(result.Result != null);
        }

        [Test]
        public async Task AddBacklog()
        {
            var result = await VGCollectApi.AddSellList(username,password,82687);
            Assert.IsTrue(result != null);
        }

 
    }
}