using Calculator.Core.Consts;
using Calculator.Infrastructure.Data;
using System.Collections.Generic;

namespace Calculator.Core
{
    public class ExpressionLexicalAnalyzer
    {
        private readonly ExpressionContext _context;
        private readonly string _expression;
        private int position;

        public ExpressionLexicalAnalyzer(ExpressionContext context, string expression)
        {
            _context = context;
            _expression = expression;
            position = 0;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (position < _expression.Length)
            {
                var currentChar = _expression[position];

                if (char.IsDigit(currentChar) || currentChar == OperatorConsts.Point)
                {
                    var number = ReadNumber();
                    tokens.Add(new Token(TokenType.Number, number));
                }
                else if (char.IsLetter(currentChar))
                {
                    var id = ReadId();
                    if (IsFunction(id))
                    {
                        tokens.Add(new Token(TokenType.Function, id));
                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.Variable, id));
                    }
                }
                else if (IsOperator(currentChar))
                {
                    tokens.Add(new Token(TokenType.Operator, currentChar.ToString()));
                    position++;
                }
                else if (currentChar == OperatorConsts.OpeningParenthesis)
                {
                    tokens.Add(new Token(TokenType.LeftParenthesis, OperatorConsts.OpeningParenthesis.ToString()));
                    position++;
                }
                else if (currentChar == OperatorConsts.ClosingParenthesis)
                {
                    tokens.Add(new Token(TokenType.RightParenthesis, OperatorConsts.ClosingParenthesis.ToString()));
                    position++;
                }
                else
                {
                    position++; 
                }
            }

            return tokens;
        }

        private string ReadNumber()
        {
            var startPosition = position;

            while (position < _expression.Length && (char.IsDigit(_expression[position]) 
                || _expression[position] == OperatorConsts.Point))
            {
                position++;
            }

            return _expression.Substring(startPosition, position - startPosition);
        }

        private string ReadId()
        {
            var startPosition = position;

            while (position < _expression.Length && char.IsLetterOrDigit(_expression[position]))
            {
                position++;
            }

            return _expression.Substring(startPosition, position - startPosition);
        }

        private bool IsFunction(string id)
        {
            var function = _context.GetFunction(id);

            if (function != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsOperator(char c)
        {
            return c == OperatorConsts.Plus || c == OperatorConsts.Minus || c == OperatorConsts.Multiplication 
                || c == OperatorConsts.Division;
        }
    }
}
