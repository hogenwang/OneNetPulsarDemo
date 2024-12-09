using OneNetPulsarDemo.PulsarSubscription;

namespace OneNetPulsarDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            await IoTPulsarConsume.DoStartConsume();
            
        }
    }
}
