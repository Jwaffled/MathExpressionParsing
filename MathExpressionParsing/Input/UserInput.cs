using MathExpressionParsing.Core;
using MathExpressionParsing.Exceptions;

namespace MathExpressionParsing.Input;

public static class UserInput
{
    public static void Start()
    {
        var input = "";
        while (input.ToLower() != "exit")
        {
            Console.Write(">>> ");
            input = Console.ReadLine() ?? "";
            var scanner = new Scanner(input);
            var interpreter = new Interpreter();
            try
            {
                var tokens = scanner.ScanTokens();
                if (Settings.DebugMode)
                {
                    foreach (var token in tokens)
                    {
                        Console.WriteLine(token);
                    }
                }
                var parser = new Parser(tokens);
                var expression = parser.Parse();
                
                Console.WriteLine(interpreter.Evaluate(expression));
                // foreach (var expr in expressions)
                // {
                //     Console.WriteLine(interpreter.Evaluate(expr));
                // }
            }
            catch (Exception ex)
            {
                if (Settings.DebugMode)
                {
                    Console.Error.WriteLine($"[DEBUG]: An error occurred: {ex}");
                    return;
                }
                switch (ex)
                {
                    case ScannerException:
                        Console.WriteLine($"Error when scanning tokens: {ex.Message}");
                        break;
                    case ParserException:
                        Console.WriteLine($"Error when parsing tokens: {ex.Message}");
                        break;
                    case FunctionNotFoundException:
                        Console.WriteLine($"Error when running function call: {ex.Message}");
                        break;
                    case ConstantNotFoundException:
                        Console.WriteLine($"Error when resolving constant: {ex.Message}");
                        break;
                    default:
                        Console.WriteLine($"An unknown error occurred: {ex.Message}");
                        break;
                }
            }
        }
    }
}