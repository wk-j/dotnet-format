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
        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("≈≈ Invalid argument");
                return;
            }

            var source = args[0];

            if (!File.Exists(source)) {
                Console.WriteLine("≈≈ File not exist - {0}", source);
                return;
            }

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
    }
}
