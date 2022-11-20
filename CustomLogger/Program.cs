using System;

namespace CustomLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CustomLogger customLogger = CustomLogger.CreateInstance("Initial");
            customLogger.WriteToFile(LogLevel.Info ,"this is my log text tha we want to write");
        }
    }
}
