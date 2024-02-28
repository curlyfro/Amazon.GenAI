using Amazon.GenAI;
using LangChain.Providers;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public Task Test1()
        {
            var llm = Constants.GetChatModelTypeById("ai21.j2-mid-v1");
            var result = llm.GenerateAsync("Hello World", new ChatSettings());
            Console.WriteLine(result.Result);
            return Task.CompletedTask;
        }
    }
}