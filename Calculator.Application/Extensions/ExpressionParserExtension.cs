using Calculator.Core.Nodes;
using System.Collections.Generic;
using System;
using Calculator.Core.Consts;

namespace Calculator.Core.Extensions
{
    public class ExpressionParserExtension
    {
        private readonly List<Token> _tokens;
        private int _position;

        public ExpressionParserExtension(List<Token> tokens)
        {
            _tokens = tokens;
            _position = 0;
        }

        public ExpressionNode Parse()
        {
            return ParseExpression();
        }

        private ExpressionNode ParseExpression()
        {
            var left = ParseTerm();

            while (_position < _tokens.Count)
            {
                var token = _tokens[_position];

                if (token.Type != TokenType.Operator || (token.Value != OperatorConsts.Plus.ToString()
                    && token.Value != OperatorConsts.Minus.ToString()))
                {
                    break;
                }

                _position++;

                var right = ParseTerm();

                left = new BinaryOperatorNode(token.Value.ToCharArray()[0], left, right);
            }

            return left;
        }

        private ExpressionNode ParseTerm()
        {
            var left = ParseFactor();

            while (_position < _tokens.Count)
            {
                var character = _tokens[_position];

                if (character.Type != TokenType.Operator || 
                    (character.Value != OperatorConsts.Multiplication.ToString() 
                    && character.Value != OperatorConsts.Division.ToString()))
                {
                    break;
                }

                _position++;

                var right = ParseFactor();

                left = new BinaryOperatorNode(character.Value.ToCharArray()[0], left, right);
            }

            return left;
        }

        private ExpressionNode ParseFactor()
        {
            var token = _tokens[_position];
            _position++;

            switch(token.Type)
            {
                case TokenType.Number:
                    if (double.TryParse(token.Value.ToString(), out double value))
                    {
                        return new NumberNode(value);
                    }
                    else
                    {
                        throw new Exception($"Invalid number: {token.Value}");
                    }
                case TokenType.Variable:
                    return new VariableNode(token.Value.ToString());
                case TokenType.Function:
                    var argument1 = ParseFactor();
                    var argument2 = ParseFactor();

                    return new FunctionNode(token.Value.ToString(), argument1, argument2);
                case TokenType.LeftParenthesis:
                    var expression = ParseExpression();

                    if (_position < _tokens.Count && _tokens[_position].Type == TokenType.RightParenthesis)
                    {
                        _position++;

                        return expression;
                    }
                    break;
            }

            return null;
        }
    }
}
