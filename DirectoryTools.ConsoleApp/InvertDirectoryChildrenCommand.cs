using System;
using Microsoft.Extensions.CommandLineUtils;

namespace DirectoryTools.ConsoleApp
{
    public class InvertDirectoryChildrenCommand : ICommand
    {
        public static void Configure(CommandLineApplication command)
        {
            command.Description = "Inverts a folder's children and their immediate sub-folders.";

            var targetArgument = command.Argument("[target]", "Target base folder containing 2 levels of children to invert.");

            var dryRunOption = command.Option(
                "-d|--dryrun",
                "true if changes should only be reported, and not actioned.",
                CommandOptionType.NoValue
            );

            command.OnExecute(() =>
            {
                new InvertDirectoryChildrenCommand(targetArgument.Value, dryRunOption.HasValue()).Run();
                return 0;
            });
        }

        private bool DryRun { get; }

        private string ParentPath { get; }

        private InvertDirectoryChildrenCommand(string parentPath, bool dryRun)
        {
            DryRun = dryRun;
            ParentPath = parentPath;
        }

        public void Run()
        {
            Console.WriteLine($"{nameof(InvertDirectoryChildrenCommand)} OnExecute");
            Console.WriteLine($"{ParentPath}");
            Console.WriteLine(DryRun ? "Yes" : "No");
        }
    }
}