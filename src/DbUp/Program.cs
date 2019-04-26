using System;
using System.Linq;
using System.Reflection;

namespace DbUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dBs = new[]
            {
                "Server=localhost\\SQLEXPRESS;Database=StarWars;Trusted_Connection=True;",
                "Server=localhost\\SQLEXPRESS;Database=StarWarsTest;Trusted_Connection=True;"
            };

            foreach (var dB in dBs)
            {
                var connectionString =
                    args.FirstOrDefault()
                    ?? dB;

                var upgrader =
                    DeployChanges.To
                        .SqlDatabase(connectionString)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                        .LogToConsole()
                        .Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                    Console.ResetColor();
#if DEBUG
                    Console.ReadLine();
#endif
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
