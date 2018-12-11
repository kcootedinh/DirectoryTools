using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Serilog;
using Serilog.Core;

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
            // get target dir
            var targetDirectoryInfo = new DirectoryInfo(ParentPath);

            // verify exists
            if (!targetDirectoryInfo.Exists) return;

            // get direct children
            var directChildren = targetDirectoryInfo.GetDirectories();

            // get all 2nd level children
            var secondLevelChildren = directChildren.SelectMany(childDir => childDir.GetDirectories());
            
            // determine new direct children
            var newDirectChildrenNames = 
                secondLevelChildren.Select(secondLevelChild => secondLevelChild.Name)
                    .Distinct(StringComparer.InvariantCultureIgnoreCase);
            
            // determine new 2nd level children?
            
            // Create new child path and then move to current after all move operations
            // get all 3rd levels and rewrite paths?
            foreach (var secondLevelChild in secondLevelChildren)
            {
                foreach (var thirdLevelChild in secondLevelChild.GetDirectories())
                {
                    // move third level child to new home
                    // need to consider dry run here
                    var oldPath = thirdLevelChild.FullName;

                    var oldDirectName = secondLevelChild.Parent.Name;
                    var oldSecondLevelName = secondLevelChild.Name;
                    var newPath = Path.Combine(targetDirectoryInfo.FullName, oldSecondLevelName, oldDirectName, thirdLevelChild.Name);

                    Log.Information($"Move: {oldPath} => {newPath}");
                    
                    if (!DryRun)
                    {
                        // action
                    }
                }
                foreach (var thirdLevelChild in secondLevelChild.GetFiles())
                {
                    // move third level child to new home
                    // need to consider dry run here
                    var oldPath = thirdLevelChild.FullName;

                    var oldDirectName = secondLevelChild.Parent.Name;
                    var oldSecondLevelName = secondLevelChild.Name;
                    var newPath = Path.Combine(targetDirectoryInfo.FullName, oldSecondLevelName, oldDirectName, thirdLevelChild.Name);

                    Log.Information($"Move: {oldPath} => {newPath}");
                    
                    if (!DryRun)
                    {
                        // action
                    }
                }
            }
            // go?
            Console.WriteLine($"{nameof(InvertDirectoryChildrenCommand)} OnExecute");
            Console.WriteLine($"{ParentPath}");
            Console.WriteLine(DryRun ? "Yes" : "No");
        }
    }
}