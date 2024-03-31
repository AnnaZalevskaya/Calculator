using Calculator.Core.Nodes;
using System.Collections.Generic;
using System;
using Calculator.Core.Consts;

namespace Calculator.Core.Extensions
{
    public class ExpressionParserExtension
    {
        private readonly List<Token> _tokens;
        private int position;

        public ExpressionParserExtension(List<Token> tokens)
        {
            _tokens = tokens;
            position = 0;
        }

        public ExpressionNode Parse()
        {
            return ParseExpression();
        }

        private ExpressionNode ParseExpression()
        {
            var left = ParseTerm();

            while (position < _tokens.Count)
            {
                var token = _tokens[position];

                if (token.Type != TokenType.Operator || (token.Value != OperatorConsts.Plus.ToString()
                    && token.Value != OperatorConsts.Minus.ToString()))
                {
                    break;
                }

                position++;

                var right = ParseTerm();

                left = new BinaryOperatorNode(token.Value.ToCharArray()[0], left, right);
            }

            return left;
        }

        private ExpressionNode ParseTerm()
        {
            var left = ParseFactor();

            while (position < _tokens.Count)
            {
                var character = _tokens[position];
                if (character.Type != TokenType.Operator || 
                    (character.Value != OperatorConsts.Multiplication.ToString() 
                    && character.Value != OperatorConsts.Division.ToString()))
                {
                    break;
                }

                position++;

                var right = ParseFactor();

                left = new BinaryOperatorNode(character.Value.ToCharArray()[0], left, right);
            }

            return left;
        }

        private ExpressionNode ParseFactor()
        {
            var token = _tokens[position];
            position++;

            if (token.Type == TokenType.Number)
            {
                if (double.TryParse(token.Value.ToString(), out double value))
                {
                    return new NumberNode(value);
                }              
                else
                {
                    throw new Exception($"Invalid number: {token.Value}");
                }
            }
            else if (token.Type == TokenType.Variable)
            {
                return new VariableNode(token.Value.ToString());
            }
            else if (token.Type == TokenType.Function)
            {
                var argument1 = ParseFactor();
                var argument2 = ParseFactor();

                return new FunctionNode(token.Value.ToString(), argument1, argument2);
            }
            else if (token.Type == TokenType.LeftParenthesis)
            {
                var expression = ParseExpression();

                if (position < _tokens.Count && _tokens[position].Type == TokenType.RightParenthesis)
                {
                    position++;

                    return expression;
                }
            }

            return null;
        }
    }
}
