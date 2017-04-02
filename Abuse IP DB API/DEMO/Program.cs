using System;
using AIPDBAPI;

namespace DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" [?] What Is Your API Key?");
            string API_KEY = Console.ReadLine();
            Console.WriteLine(" [?] IP That You Want TO Report?");
            string IP = Console.ReadLine();
            Console.WriteLine(" [?] Comments?");
            string COMMENTS = Console.ReadLine();
            AIPDB.ReportIP(API_KEY, IP, COMMENTS, AIPDB.Categories.DDoS_Attack);
            Console.WriteLine(" [*] Done! Hit Enter To Exit!");
            Console.ReadLine();
        }
    }
}
