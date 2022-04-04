# Math Expression Parser

This parser was written entirely in C#, and was created for the sake of learning about [Abstract Syntax Trees](https://en.wikipedia.org/wiki/Abstract_syntax_tree), [Recursive Descent Parsing](https://en.wikipedia.org/wiki/Recursive_descent_parser), and the [Visitor Pattern](https://en.wikipedia.org/wiki/Visitor_pattern).

Contributions, suggestions, and critiques are greatly appreciated!

## Currently Supported Features


### Expressions
```
>>> 2 / 2
1
>>> 5! * 5
600
>>> -(8!) + 9 ^ 3
-39591
>>> pi ^ 2
9.869604401089358
```

### Constants
```
>>> pi
3.141592653589793
>>> e
2.718281828459045
```

### Function Calls
```
>>> sin(0)
0
>>> factorial(5)
120
>>> tan(0)
0
```
Side note: trigonometric functions take radians, not degrees.

## Modifying the internal math library
You are able to create your own constants and functions by modifying the `MathLibrary` class.

Functions are created by annotating them with the `FunctionName` annotation, and constants are created by annotating them with the `MathConstant` annotation. For example,
```cs
public class MathLibrary
{
	/*
	 * The first parameter is the name the user
	 * will need to use. Fairly self-explanatory.
	 */
	[MathConstant("myConstant")]
	private static readonly double MyConstant = 1234.5;

	/*
	 * The first parameter of the constructor
	 * is the name the user will call the function with.
	 * Note: The user can call the function in ANY casing
	 * and it will work.
	 * 
	 * The second parameter is optional,
	 * and indicates how many arguments the function takes.
	 * 
	 * If left out, the function will take as many arguments
	 * as the user enters.
	 */
	[FunctionName("myFunction", 1)]
	public static double DoSomething(double input)
	{
		return input + 1;
	}

	// When the argument number param is left out, the input is passed in as an array.
	[FunctionName("anotherFunction")]
	public static double AnotherFunction(double[] input)
	{
		double sum = 0;
		foreach(var number in input)
		{
			sum += number;
		}
		
		return sum;
	}
}
```
## Issues/To-do list
- ~~Support postfix unary operators.~~
- Support more accurate trig function calls. Floating point inaccuracies mess with some values, such as asymptotes.
- Use source generators instead of reflection for functions/constants.
- Perhaps support variable declarations?
- More tests.

##

This project has been largely inspired by [Crafting Interpreters](http://www.craftinginterpreters.com/), check out the book if you're interested in creating an interpreter or compiler of your own! 
