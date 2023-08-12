﻿namespace Singleton.Start
{
    internal class Program
    {
        static MemoryLogger logger;
        static void Main(string[] args)
        {
            AssignVoucher("issam@metigator.com", "ABC123");

            UseVoucher("ABC123");

            logger.ShowLog();

            Console.ReadKey();
        }

        static void AssignVoucher(string email, string voucher)
        {
            logger = new MemoryLogger();

            //logic here
            
            logger.LogInfo($"Voucher '{voucher}' assigned");
            
            //another logic
            logger.LogError($"unable to send email '{email}'");
        }
        static void UseVoucher(string voucher)
        {
            logger = new MemoryLogger();

            // Logic here
            logger.LogWarning($"3 attempts made to validate the voucher");

            // Logic here
            logger.LogInfo($"'{voucher}' is used");
        }
    }
}