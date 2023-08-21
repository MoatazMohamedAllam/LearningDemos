namespace IocContainer_Implementation
{
    internal class Test
    {
        private RandomGuidGenerator randomGuidGenerator;

        public Test(RandomGuidGenerator randomGuidGenerator)
        {
            this.randomGuidGenerator = randomGuidGenerator;
        }

        public void Show()
        {
            Console.WriteLine(this.randomGuidGenerator.RandomGuid + " from test");
        }
    }
}