using System.Collections.Generic;
using MathExpressionParsing.Core;
using Xunit;

namespace Tests;

public class Tests
{
    [Theory]
    [ClassData(typeof(ScannerTestData))]
    public void TestScanning(string inputData, List<Token> expectedTokens)
    {
        Scanner scan = new(inputData);

        var tokens = scan.ScanTokens();
        
        Assert.Equal(expectedTokens, tokens);
    }

    [Theory]
    [ClassData(typeof(EvaluationTestData))]
    public void TestEvaluation(string inputData, double expectedResult)
    {
        Scanner scan = new(inputData);
        Parser parser = new(scan.ScanTokens());

        Interpreter interpreter = new();
        var actualResult = interpreter.Evaluate(parser.Parse());
        
        Assert.Equal(actualResult, expectedResult);
    }
}