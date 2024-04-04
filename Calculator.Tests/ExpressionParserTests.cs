using Calculator.Core.Extensions;
using Calculator.Infrastructure.Data;

namespace Calculator.Tests
{
	public class ExpressionParserTests
	{
		private readonly ExpressionParser parser = CreateExpressionParser();

		[Theory]
		[InlineData("2 + 1 - 3", 0d)]
		[InlineData("-2 + 1 - 3", -4d)]
		[InlineData("1 + f", 4d)]
		[InlineData("1 + x", 2d)]
		[InlineData("f + x + y", 6d)]
		public void ParseAndEvaluate_ReturnsRightValuesWhenDifferentTypesOfExpressionIsPassed(string expression, double expected)
		{
			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("1 +- 2 -- 3")]
		[InlineData("1 + z")] 
		public void ParseAndEvaluate_ReturnsNaNWhenDifferentTypesOfExpressionIsPassed(string expression)
		{
			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(double.NaN, actual);
		}

		private static ExpressionParser CreateExpressionParser()
		{
			var context = new ExpressionContext();

			context.AddVariable("x", 1d);
			context.AddVariable("y", 2d);
			context.AddFunction("f", (x, y) => x + y);

			return new ExpressionParser(context);
		}
	}
}
