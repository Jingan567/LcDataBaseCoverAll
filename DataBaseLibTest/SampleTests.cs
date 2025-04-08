namespace DataBaseLibTest
{
    /// <summary>
    /// •	[TestFixture] 标记一个测试类。
    /// •	[SetUp] 标记一个在每个测试方法之前运行的方法。
    /// •	[Test] 标记一个测试方法。
    /// •	Assert 类用于验证测试结果
    /// </summary>
    [TestFixture]
    public class SampleTests
    {
        [SetUp]
        public void Setup()
        {
            // 在每个测试之前运行的代码
        }

        [Test]
        public void Test1()
        { 
            // 测试代码
            Assert.Pass();
        }

        [Test]
        public void TestAddition()
        {
            int a = 5;
            int b = 10;
            int result = a + b;
            //Assert.AreEqual(15, result, "Addition test failed");//classical model, 使用会警告
            Assert.That(result, Is.EqualTo(15), "Addition test failed");//constraint model,约束模型，更自由，更可读
        }
    }
}