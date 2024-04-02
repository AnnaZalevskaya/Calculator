using Calculator.Core;
using Calculator.Infrastructure.Data;
using FluentAssertions;

namespace Calculator.Tests
{
	public class ExpressionLexicalAnalyzerTests
	{
		private ExpressionContext context = CreateContext();

		[Fact]
		public void Tokenize_ReturnsSameListWhenArithmeticExpressionIsPassed()
		{
			var expression = "(2 * (2 + 4 / 5) / 3) + 5 * 4 / (2 + 7)";

			var expected = new List<Token>()
			{
				new Token(TokenType.LeftParenthesis, "("),
				new Token(TokenType.Number, "2"),
				new Token(TokenType.Operator, "*"),
				new Token(TokenType.LeftParenthesis, "("),
				new Token(TokenType.Number, "2"),
				new Token(TokenType.Operator, "+"),
				new Token(TokenType.Number, "4"),
				new Token(TokenType.Operator, "/"),
				new Token(TokenType.Number, "5"),
				new Token(TokenType.RightParenthesis, ")"),
				new Token(TokenType.Operator, "/"),
				new Token(TokenType.Number, "3"),
				new Token(TokenType.RightParenthesis, ")"),
				new Token(TokenType.Operator, "+"),
				new Token(TokenType.Number, "5"),
				new Token(TokenType.Operator, "*"),
				new Token(TokenType.Number, "4"),
				new Token(TokenType.Operator, "/"),
				new Token(TokenType.LeftParenthesis, "("),
				new Token(TokenType.Number, "2"),
				new Token(TokenType.Operator, "+"),
				new Token(TokenType.Number, "7"),
				new Token(TokenType.RightParenthesis, ")")
			};

			var lexer = new ExpressionLexicalAnalyzer(context, expression);

			var actual = lexer.Tokenize();

			actual.Should()
					.HaveSameCount(expected)
					.And.BeEquivalentTo(expected);
		}

		[Fact]
		public void Tokenize_ReturnsSameListWhenVariableIsPassed()
		{
			var expression = "x = 5";

			var expected = new List<Token>()
			{
				new Token(TokenType.Variable, "x"),
				new Token(TokenType.Number, "5"),
			};

			var lexer = new ExpressionLexicalAnalyzer(context, expression);

			var actual = lexer.Tokenize();

			actual.Should()
					.HaveSameCount(expected)
					.And.BeEquivalentTo(expected);
		}

		[Fact]
		public void Tokenize_ReturnsSameListWhenFunctionIsPassed()
		{
			var expression = "f(x) = x + 1";

			var expected = new List<Token>()
			{
				new Token(TokenType.Function, "f"),
				new Token(TokenType.Variable, "x"),
				new Token(TokenType.Operator, "+"),
				new Token(TokenType.Variable, "1"),
			};

			var context = new ExpressionContext();
			var lexer = new ExpressionLexicalAnalyzer(context, expression);

			var actual = lexer.Tokenize();

			actual.Should()
					.HaveSameCount(expected)
					.And.BeEquivalentTo(expected);

		}

		private static ExpressionContext CreateContext() 
		{ 
			return new ExpressionContext(); 
		}	

	}
}
