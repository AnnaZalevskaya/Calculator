using Calculator.Wpf.Validation;

namespace Calculator.Tests
{
	public class UserValidatorTests
	{
		public static IEnumerable<object[]> testData =>
			new List<object[]>
			{
				new object[] {"", "Expression is empty"},
				new object[] {"(2 * (2 + 4 / 5) / 3) + 5 * 4 / (2 + 7)", "Expression is valid"},
				new object[] {"2/0", "Division by zero detected"},
				new object[] {"2f2=0", "Invalid variable expression '2f' found"},
				new object[] {".1+1", "Invalid first character in expression"},
				new object[] {"1++1", "Consecutive operators '+' and '+' found"},
				new object[] {"1-(0+1", "Unmatched opening parenthesis"},
				new object[] {"x.(1+1)", "Invalid variable expression 'x.' found"},
				new object[] {"1*(,)", "Correct Sequence operators '(' and ',' found"},
				new object[] {"1(1+1)", "Opening bracket cannot follow a number" },
				new object[] { "(1+1*)1", "Closing parenthesis ')' preceded by the operator '*" }
			};

		[Theory]
		[MemberData(nameof(testData))]
		public void Validate_ReturnsCorrectMessage(string expression, string message)
		{
			var validator = new UserValidator();
			var actual = validator.Validate(expression);

			Assert.Equal(message, actual);
		}

	}
}
