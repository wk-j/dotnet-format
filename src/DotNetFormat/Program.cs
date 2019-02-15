using System;
using Microsoft.CodeAnalysis.Formatting;
using System.Linq;
using Microsoft.CodeAnalysis;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions;

namespace DotNetFormat {

    class Program {

        static SyntaxTree ReadFile(string file) {
            using (var reader = new StreamReader(file)) {
                var code = reader.ReadToEnd();
                return CSharpSyntaxTree.ParseText(code);
            }
        }

        static void WriteFile(string file, SyntaxNode node) {
            using (var writer = new StreamWriter(file)) {
                node.WriteTo(writer);
            }
        }

        static void ProcessSourceFile(string source) {
            var space = new AdhocWorkspace();
            var syntax = ReadFile(source);
            var newOption = space.Options
                 .WithChangedOption(NewLinesForBracesInLambdaExpressionBody, false)
                 .WithChangedOption(NewLinesForBracesInAnonymousMethods, false)
                 .WithChangedOption(NewLinesForBracesInAnonymousTypes, false)
                 .WithChangedOption(NewLinesForBracesInControlBlocks, false)
                 .WithChangedOption(NewLinesForBracesInTypes, false)
                 .WithChangedOption(NewLinesForBracesInMethods, false)
                 .WithChangedOption(NewLinesForBracesInProperties, false)
                 .WithChangedOption(NewLinesForBracesInObjectCollectionArrayInitializers, false)
                 .WithChangedOption(NewLinesForBracesInAccessors, false)
                 .WithChangedOption(NewLineForElse, false)
                 .WithChangedOption(NewLineForCatch, false)
                 .WithChangedOption(NewLineForFinally, false)
                 .WithChangedOption(NewLineForClausesInQuery, false)
                 .WithChangedOption(NewLineForMembersInAnonymousTypes, false)
                 .WithChangedOption(NewLineForMembersInObjectInit, false);

            var result = Formatter.Format(syntax.GetRoot(), space, newOption);
            WriteFile(source, result);
        }

        static bool Validate(string source) {
            if (!File.Exists(source)) {
                return false;
            }
            return true;
        }

        static void StartSpinner(string source) {
            var ok = Validate(source);
            Kurukuru.Spinner.Start($"Processing ({source}) ", spinner => {
                if (ok) {
                    ProcessSourceFile(source);
                    spinner.Succeed();
                } else {
                    spinner.Text = $"File not exist ({source})";
                    spinner.Fail();
                }
            });
        }

        static void StartWithoutSpinner(string source) {
            var ok = Validate(source);
            if (ok) {
                ProcessSourceFile(source);
                Console.WriteLine($" - Processing ({source})");
            } else {
                Console.WriteLine($" - File not exist ({source})");
            }
        }

        static void Main(string[] args) {
            if (args.Length == 1) {
                var source = args[0];
                StartSpinner(source);
            } else {
                string line;
                while ((line = Console.ReadLine()) != null) {
                    StartWithoutSpinner(line);
                }
            }
        }
    }
}
