using MathExpressionParsing.Core;

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
                // foreach (var token in tokens)
                // {
                //     Console.WriteLine(token);
                // }
                var parser = new Parser(tokens);
                var expressions = parser.Parse();

                foreach (var expr in expressions)
                {
                    Console.WriteLine(interpreter.Evaluate(expr));
                }
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case Scanner.ScannerError:
                        Console.WriteLine($"Error when scanning tokens: {ex.Message}");
                        break;
                    case Parser.ParserError:
                        Console.WriteLine($"Error when parsing tokens: {ex.Message}");
                        break;
                    case FunctionNotFoundException:
                        Console.WriteLine($"Error when running function call: {ex.Message}");
                        break;
                    default:
                        Console.WriteLine($"An unknown error occurred: {ex.Message}");
                        break;
                }
            }
        }
    }
}