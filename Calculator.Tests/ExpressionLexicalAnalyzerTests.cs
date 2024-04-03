using Calculator.Core;
using Calculator.Infrastructure.Data;
using FluentAssertions;

namespace Calculator.Tests
{
	public class ExpressionLexicalAnalyzerTests
	{
		private readonly ExpressionContext _context = CreateContext();

		public static IEnumerable<object[]> expectedLists =>
			new List<object[]>
			{
				new object[]
				{
					"(2 * (2 + 4 / 5) / 3) + 5 * 4 / (2 + 7)",
					new List<Token>()
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
					}
				},
				new object[]
				{
					"x = 5",
					new List<Token>()
					{
						new Token(TokenType.Variable, "x"),
						new Token(TokenType.Number, "5")
					}
				},
				new object[]
				{
					"f(x) = x + 1",
					new List<Token>()
					{
						new Token(TokenType.Function, "f"),
						new Token(TokenType.Variable, "x"),
						new Token(TokenType.Operator, "+"),
						new Token(TokenType.Variable, "1")
					}
				}
			};

		[Theory]
		[MemberData(nameof(expectedLists))]
		public void Tokenize_ReturnsSameListWhenDifferentTypesOfExpressionIsPassed(string expression, List<Token> expected)
		{
			var lexer = new ExpressionLexicalAnalyzer(_context, expression);

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
