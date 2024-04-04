using System;
using Calculator.Core.Nodes;
using Calculator.Core.Consts;
using Calculator.Infrastructure.Data;
using Calculator.Application.Extensions;

namespace Calculator.Core.Extensions
{
    public class ExpressionParser
    {
        private readonly ExpressionContext _context;
        private readonly VariableParserExtension _variableParser;
        private readonly FunctionParserExtension _functionParser;

        public ExpressionParser(ExpressionContext context)
        {
            _context = context;
            _variableParser = new VariableParserExtension(_context);
            _functionParser = new FunctionParserExtension(_context);
        }

        public double ParseAndEvaluate(string expression)
        {
            double result;

            try
            {
                result = EvaluateExpression(ParseExpression(expression));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                result = double.NaN;
            }

            return result;
        }

        public void ParseAndSaveVariable(string expression)
        {
            _variableParser.ParseAndEvaluate(expression);
        }

        public void ParseAndSaveFunction(string expression)
        {
            _functionParser.ParseAndEvaluate(expression);
        }

        private double EvaluateExpression(ExpressionNode expression)
        {
            if (expression is NumberNode numberNode)
            {
                return numberNode.Value;
            }
            else if (expression is VariableNode variableNode)
            {
                return _context.GetVariable(variableNode.Name);
            }
            else if (expression is BinaryOperatorNode binaryOperatorNode)
            {
                var leftValue = EvaluateExpression(binaryOperatorNode.Left);
                var rightValue = EvaluateExpression(binaryOperatorNode.Right);
                double res;

                switch (binaryOperatorNode.MathOperator)
                {
                    case OperatorConsts.Plus:
                        res = leftValue + rightValue;
                        break;
                    case OperatorConsts.Minus:
                        res = leftValue - rightValue;
                        break;
                    case OperatorConsts.Multiplication:
                        res = leftValue * rightValue;
                        break;
                    case OperatorConsts.Division:
                        res = leftValue / rightValue;
                        break;
                    default:
                        throw new Exception($"Unsupported operator: {binaryOperatorNode.MathOperator}");
                }

                return res;
            }
            else if (expression is FunctionNode functionNode)
            {
                var function = _context.GetFunction(functionNode.Name);
                var argumentValue1 = EvaluateExpression(functionNode.Argument1);
                var argumentValue2 = EvaluateExpression(functionNode.Argument2);

                return function(argumentValue1, argumentValue2);
            }
            else
            {
                throw new Exception("Invalid expression");
            }
        }

        private ExpressionNode ParseExpression(string expression)
        {    
            var lexer = new ExpressionLexicalAnalyzer(_context, expression);
            var tokens = lexer.Tokenize();
            var parser = new ExpressionParserExtension(tokens);

            return parser.Parse();
        }
    }
}
