namespace IocContainer_Implementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new DiServiceCollection();

            //services.RegisterSingleton(new RandomGuidGenerator());
            //services.RegisterSingleton<RandomGuidGenerator>();

            //services.RegisterTransient(new RandomGuidGenerator());
            //services.RegisterTransient<RandomGuidGenerator>();

            //services.RegisterSingleton<ISomeService, SomeServiceOne>();
            //services.RegisterTransient<ISomeService, SomeServiceOne>();

            services.RegisterSingleton<IRandomGuidProvider, RandomGuidProvider>();
            services.RegisterTransient<ISomeService, SomeServiceOne>();


            var container = services.GenerateContainer();

            //var service1 = container.GetService<RandomGuidGenerator>();
            //var service2 = container.GetService<RandomGuidGenerator>();

            var service3 = container.GetService<ISomeService>();
            var service4 = container.GetService<ISomeService>();

            //var test = new Test(service1);
            //test.Show();

            //Console.WriteLine(service1.RandomGuid);
            //Console.WriteLine(service2.RandomGuid);

            service3.PrintSomething();
            service4.PrintSomething();
        }
    }
}