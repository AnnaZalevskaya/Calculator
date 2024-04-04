using System;
using System.Linq;

namespace Calculator.Wpf.Validation
{
    public class UserValidator
    {
        private char[] _statementOperators = { '+', '-', '*', '/' };
        private char[] _decimalSeparators = { '.', ',' };
        private char[] _brackets = { '(', ')' };
        public string Validate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return "Expression is empty";
            }

            string[] errorMessages =
            {
                CheckFirstCharacter(expression),
                CheckCorrectSequenceОfStatementsOperators(expression),
                CheckConsecutiveOperators(expression),
                CheckUnmatchedParentheses(expression),
                CheckClosingParenthesisBeforeOperator(expression),
                CheckDivisionByZero(expression),
                CheckInvalidVariableExpression(expression),
                CheckInvalidVariableSequence(expression),
                CheckOpeningBracketAfterNumber(expression)
            };

            foreach (string errorMessage in errorMessages)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return errorMessage;
                }
            }

            return "Expression is valid";
        }

        private string CheckDivisionByZero(string expression)
        {
            if (expression.Contains("/0"))
            {
                return "Division by zero detected";
            }
            return null;
        }

        private string CheckInvalidVariableExpression(string expression)
        {

            return ProcessExpression(expression, (currentChar, nextChar) =>
            {
                if ((char.IsDigit(currentChar) && char.IsLetter(nextChar))||(char.IsLetter(currentChar) && char.IsDigit(nextChar)))
                {
                    return $"Invalid variable expression '{currentChar}{nextChar}' found";
                }

                return null;
            });
        }

        private string CheckInvalidVariableSequence(string expression)
        {
            return ProcessExpression(expression, (currentChar, nextChar) =>
            {
                if ((char.IsLetter(currentChar) && (_decimalSeparators.Contains(nextChar) || _brackets.Contains(nextChar))) || ((_decimalSeparators.Contains(currentChar)) && char.IsLetter(nextChar)))
                {
                    return $"Invalid variable expression '{currentChar}{nextChar}' found";
                }

                return null;
            });
        }

        private string CheckUnmatchedParentheses(string expression)
        {
            int openParenthesesCount = expression.Count(c => c == '(');
            int closingParenthesesCount = expression.Count(c => c == ')');

            if (openParenthesesCount != closingParenthesesCount)
            {
                if (openParenthesesCount > closingParenthesesCount)
                {
                    return "Unmatched opening parenthesis";
                }
                else
                {
                    return "Unmatched closing parenthesis";
                }
            }

            return null;
        }

        private string CheckCorrectSequenceОfStatementsOperators(string expression)
        {
            return ProcessExpression(expression, (currentChar, nextChar) =>
            {
                if ((_statementOperators.Contains(currentChar) || _brackets.Contains(currentChar))
                   && (_decimalSeparators.Contains(nextChar)))
                {
                    return $"Correct Sequence operators '{currentChar}' and '{nextChar}' found";
                }

                return null;
            });
        }

        private string CheckConsecutiveOperators(string expression)
        {
            return ProcessExpression(expression, (currentChar, nextChar) =>
            {
                if ((_statementOperators.Contains(currentChar) || _decimalSeparators.Contains(currentChar))
                    && (_statementOperators.Contains(nextChar) || _decimalSeparators.Contains(nextChar)))
                {
                    return $"Consecutive operators '{currentChar}' and '{nextChar}' found";
                }

                return null;
            });
        }

        private string CheckFirstCharacter(string expression)
        {
            char firstChar = expression[0];
            if (firstChar == '*' || firstChar == '/' || _decimalSeparators.Contains(firstChar))
            {
                return "Invalid first character in expression";
            }

            return null;
        }

        private string CheckClosingParenthesisBeforeOperator(string expression)
        {
            return ProcessExpression(expression, (currentChar, nextChar) =>
            {
                if ((_statementOperators.Contains(currentChar))
                       && (nextChar == ')'))
                {
                    return $"Closing parenthesis ')' preceded by the operator '{currentChar}";
                }

                return null;
            });
        }
        
        public string CheckOpeningBracketAfterNumber(string expression)
        {
            return ProcessExpression(expression, (currentChar, nextChar) =>
            {
                if (char.IsDigit(currentChar) && nextChar == '(')
                {
                    return $"Opening bracket cannot follow a number";
                }

                return null;
            });
        }

        private string ProcessExpression(string expression, Func<char, char, string> action)
        {
            for (int i = 0; i < expression.Length - 1; i++)
            {
                char currentChar = expression[i];
                char nextChar = expression[i + 1];
                string result = action(currentChar, nextChar);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}

