using System;
using Microsoft.Extensions.CommandLineUtils;
using Serilog;

namespace DirectoryTools.ConsoleApp
{
    static class Program
    {
        static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            
            var app = new CommandLineApplication { Name = "Directory Tools" };
            app.HelpOption("-?|-h|--help");

            app.Command("invert", InvertDirectoryChildrenCommand.Configure);
            
            app.OnExecute(() =>
            {
                Console.WriteLine("App OnExecute");
                return 0;
            });

            return app.Execute(args);
        }
    }
}