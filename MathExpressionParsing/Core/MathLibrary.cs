using System.Reflection;
using MathExpressionParsing.Exceptions;

namespace MathExpressionParsing.Core;

public static class MathLibrary
{
    private static readonly MethodInfo[] Methods = typeof(MathLibrary).GetMethods();
    private static readonly FieldInfo[] Properties = typeof(MathLibrary).GetFields(BindingFlags.NonPublic | BindingFlags.Static);

    [MathConstant("pi")] 
    private const double Pi = Math.PI;

    [MathConstant("e")] 
    private const double E = Math.E;
    
    [FunctionName("sin", 1)]
    public static double Sine(double radians)
    {
        return Math.Sin(radians);
    }
    
    [FunctionName("cos", 1)]
    public static double Cosine(double radians)
    {
        return Math.Cos(radians);
    }

    [FunctionName("tan", 1)]
    public static double Tangent(double radians)
    {
        return Math.Tan(radians);
    }

    [FunctionName("add")]
    public static double Add(double[] operands)
    {
        double sum = 0;
        foreach (var num in operands)
        {
            sum += num;
        }

        return sum;
    }

    [FunctionName("factorial", 1)]
    public static double Factorial(int n)
    {
        if (n <= 0)
        {
            return Double.NaN;
        }
        if (n == 1)
        {
            return 1;
        }
        
        return n * Factorial(n - 1);
    }

    // I mean, it works I guess
    public static double FindAndInvoke(string funcName, params double[] operands)
    {
        var (method, argLength) = FindFunction(funcName);

        if (method is null)
        {
            throw new FunctionNotFoundException($"Function '{funcName}' does not exist.");
        }
        
        if (operands.Length != argLength && argLength is not null)
        {
            throw new ArgumentException(
                $"Method '{funcName}' takes {argLength} arguments, not {operands.Length}.");
        }

        if (argLength is null)
        {
            return (double) method.Invoke(null, new object[] { operands });
        }
        
        return (double) method.Invoke(null, operands.Select(x => (object) x).ToArray());
    }

    public static double? FindConstant(string constName)
    {
        foreach (var prop in Properties)
        {
            var attrs = Attribute.GetCustomAttributes(prop);
            if (attrs.Length > 0 && attrs[0] is MathConstant && ((MathConstant)attrs[0]).Name.Equals(constName.ToLower()))
            {
                return (double?) prop.GetValue(null);
            }
        }
        
        return null;
    }

    private static (MethodInfo?, int?) FindFunction(string funcName)
    {
        foreach (var method in Methods)
        {
            var attrs = Attribute.GetCustomAttributes(method);
            if (attrs.Length > 0 && attrs[0] is FunctionName && ((FunctionName)attrs[0]).Name.Equals(funcName.ToLower()))
            {
                return (method, ((FunctionName)attrs[0]).ArgumentLength);
            }
        }
        return (null, 0);
    }
}