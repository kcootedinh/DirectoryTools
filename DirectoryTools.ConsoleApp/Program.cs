using System;
using Microsoft.Extensions.CommandLineUtils;

namespace DirectoryTools.ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication { Name = "Directory Tools" };
            app.HelpOption("-?|-h|--help");

            app.Command("invert", command =>
            {
                command.Description = "Inverts a folder and it's immediate sub-folders.";

                var targetArgument = command.Argument("[target]", "Target base folder to invert.");
                
                var dryRunOption = command.Option(
                    "-d|--dryrun",
                    "true if changes should only be reported, and not actioned.",
                    CommandOptionType.NoValue
                );

                command.OnExecute(() =>
                {
                    Console.WriteLine($"{command.Name} OnExecute");
                    Console.WriteLine($"{targetArgument.Value}");
                    Console.WriteLine(dryRunOption.HasValue() ? "Yes" : "No");
                    return 0;
                });
            });
            
            app.OnExecute(() =>
            {
                Console.WriteLine("App OnExecute");
                return 0;
            });

            return app.Execute(args);
        }
    }
}