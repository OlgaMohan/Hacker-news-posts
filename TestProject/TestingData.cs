using myproject;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Helper.InitializeClient();
        }

        [Test]
        public void Test1()
        {
           var response = LoadingData.LoadD(3);
            Assert.NotNull(response.Result);
            Assert.IsNotEmpty(response.Result);
            Assert.AreEqual(3, response.Result.Count);
        }
    }
}