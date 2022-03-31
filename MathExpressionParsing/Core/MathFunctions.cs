using System.Reflection;

namespace MathExpressionParsing.Core;

public static class MathFunctions
{
    private static MethodInfo[] methods = typeof(MathFunctions).GetMethods();
    
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
        switch (radians)
        {
            case Math.PI / 6:
            case Math.PI * 7 / 6: return 1 / Math.Sqrt(3);
            
            case Math.PI / 4:
            case Math.PI * 5 / 4: return 1;
            
            case Math.PI / 3:
            case Math.PI * 4 / 3: return Math.Sqrt(3);
            
            case Math.PI / 2:
            case Math.PI * 3 / 2: return double.NaN;
            
            // more
        }
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

    private static (MethodInfo?, int?) FindFunction(string funcName)
    {
        foreach (var method in methods)
        {
            var attrs = Attribute.GetCustomAttributes(method);
            if (attrs.Length > 0 && attrs[0] is FunctionName && ((FunctionName)attrs[0]).Name.Equals(funcName))
            {
                return (method, ((FunctionName)attrs[0]).ArgumentLength);
            }
        }
        return (null, 0);
    }
}