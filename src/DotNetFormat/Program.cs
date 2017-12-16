using System;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.CSharp.Formatting;

namespace DotNetFormat {

    class Program {
        static void Process(string source) {
            if (!File.Exists(source)) {
                Console.WriteLine("≈≈ file not exist - {0}", source);
                return;
            }

            Console.WriteLine("√ process - {0}", source);

            SyntaxTree readFile(string file) {
                using (var reader = new StreamReader(file)) {
                    var code = reader.ReadToEnd();
                    return CSharpSyntaxTree.ParseText(code);
                }
            }

            void writeFile(string file, SyntaxNode node) {
                using (var writer = new StreamWriter(file)) {
                    node.WriteTo(writer);
                }
            }

            var space = MSBuildWorkspace.Create();
            var syntax = readFile(source);
            var newOption = space.Options
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes, false)
                .WithChangedOption(CSharpFormattingOptions.NewLineForMembersInObjectInit, false)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInTypes, false)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInMethods, false)
                .WithChangedOption(CSharpFormattingOptions.NewLinesForBracesInControlBlocks, false)
                .WithChangedOption(CSharpFormattingOptions.NewLineForElse, false);

            var result = Formatter.Format(syntax.GetRoot(), space, newOption);
            writeFile(source, result);
        }

        static void Main(string[] args) {
            if (args.Length == 1) {
                var source = args[0];
                Process(source);
            } else {
                string line;
                while ((line = Console.ReadLine()) != null) {
                    Process(line);
                }
            }
        }
    }
}
