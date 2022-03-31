using System.Collections.Generic;
using MathExpressionParsing.Core;
using Xunit;

namespace Tests;

public class UnitTest1
{
    [Theory]
    [ClassData(typeof(ScannerTestData))]
    public void TestScanning(string inputData, List<Token> expectedTokens)
    {
        Scanner scan = new(inputData);

        var tokens = scan.ScanTokens();
        
        Assert.Equal(expectedTokens, tokens);
    }
}