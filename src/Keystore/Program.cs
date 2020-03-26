using System;

namespace C__SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = WalletUtility.FromPassword("123456789");
            Console.WriteLine("----------------"+result);
        }
    }
}
