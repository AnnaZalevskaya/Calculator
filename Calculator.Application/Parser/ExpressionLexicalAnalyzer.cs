using Calculator.Core.Consts;
using Calculator.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace Calculator.Core
{
    public class ExpressionLexicalAnalyzer
    {
        private readonly ExpressionContext _context;
        private readonly string _expression;
        private int _position;

        public ExpressionLexicalAnalyzer(ExpressionContext context, string expression)
        {
            _context = context;
            _expression = expression;
            _position = 0;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_position < _expression.Length)
            {
                var currentChar = _expression[_position];

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
                        if (IsVariable(id))
                        {
                            tokens.Add(new Token(TokenType.Variable, id));
                        }
                    }
                    _position++;
                }
                else if (IsOperator(currentChar))
                {
                    tokens.Add(new Token(TokenType.Operator, currentChar.ToString()));
                    _position++;
                }
                else if (currentChar == OperatorConsts.OpeningParenthesis)
                {
                    tokens.Add(new Token(TokenType.LeftParenthesis, OperatorConsts.OpeningParenthesis.ToString()));
                    _position++;
                }
                else if (currentChar == OperatorConsts.ClosingParenthesis)
                {
                    tokens.Add(new Token(TokenType.RightParenthesis, OperatorConsts.ClosingParenthesis.ToString()));
                    _position++;
                }
                else
                {
                    _position++; 
                }
            }

            return tokens;
        }

        private string ReadNumber()
        {
            var startPosition = _position;
            var hasDecimalPoint = false;

            while (_position < _expression.Length && (char.IsDigit(_expression[_position]) 
                || (!hasDecimalPoint && _expression[_position] == OperatorConsts.Point)))
            {
                if (_expression[_position] == OperatorConsts.Point)
                {
                    if (hasDecimalPoint)
                    {
                        throw new ArgumentException("Invalid number format.");
                    }
                    hasDecimalPoint = true;
                }
                _position++;
            }

            return _expression.Substring(startPosition, _position - startPosition);
        }

        private bool IsVariable(string id)
        {
            return _context.ContainsVariable(id);
        }

        private string ReadId()
        {
            var startPosition = _position;

            if (_expression[startPosition] == OperatorConsts.Func)
            {
                int openingParenthesisIndex = _expression.IndexOf(OperatorConsts.OpeningParenthesis, startPosition);
                int closingParenthesisIndex = _expression.IndexOf(OperatorConsts.ClosingParenthesis, startPosition);

                if (openingParenthesisIndex != -1 && closingParenthesisIndex != -1 
                    && closingParenthesisIndex > openingParenthesisIndex + 1)
                {
                    string arguments = _expression.Substring(openingParenthesisIndex + 1, 
                        closingParenthesisIndex - openingParenthesisIndex - 1);
                    string[] argumentTokens = arguments.Split(OperatorConsts.Comma);
                    int argumentCount = argumentTokens.Length;
                    string functionName = OperatorConsts.Func.ToString()
                        + OperatorConsts.OpeningParenthesis.ToString()
                        + argumentCount.ToString()
                        + OperatorConsts.ClosingParenthesis.ToString();
                    _position = closingParenthesisIndex + 1; 

                    return functionName;
                }
            }

            string id = _expression.Substring(startPosition, 1);
            _position++;

            return id;
        }

        private bool IsFunction(string id)
        {
            return _context.ContainsFunction(id);
        }

        private bool IsOperator(char c)
        {
            return c == OperatorConsts.Plus || c == OperatorConsts.Minus || c == OperatorConsts.Multiplication 
                || c == OperatorConsts.Division;
        }
    }
}
