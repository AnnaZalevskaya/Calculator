using Calculator.Core.Extensions;
using Calculator.Infrastructure.Data;

namespace Calculator.Tests
{
	public class ExpressionParserTests
	{
		private readonly ExpressionParser parser = CreateExpressionParser();

		[Fact]
		public void ParseAndEvaluate_ReturnsSameValuesIfArithmeticExpressionWithPositiveValuesIsPassed()
		{
			var expression = "2 + 1 - 3";
			var expected = 0d;

			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ParseAndEvaluate_ReturnsSameValuesIfArithmeticExpressionWithNegativeValuesIsPassed()
		{
			var expression = "-2 + 1 - 3";
			var expected = -4d;

			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ParseAndEvaluate_ReturnsNaNIfArithmeticExpessionIsNotValid()
		{
			string expression = "1 +- 2 -- 3";
			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(double.NaN, actual);
		}

		[Fact]
		public void ParseAndEvaluate_ReturnsRightValueIfFunctionIsPassed()
		{
			string expression = "1 + f";
			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(4d, actual);
		}

		[Fact]
		public void ParseAndEvaluate_ReturnsRightValueIfVariableIsPassed()
		{
			string expression = "1 + x";
			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(2d, actual);
		}

		[Fact]
		public void ParseAndEvaluate_ReturnsRightValueIfVariableAndFunctionIsPassed()
		{
			string expression = "f + x + y";
			var actual = parser.ParseAndEvaluate(expression);
			Assert.Equal(6d, actual);
		}

		[Fact]
		public void ParseAndEvaluate_ReturnsNaNIfVariableOrFunctionIsNotExist()
		{
			string expression = "1 + z";
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
