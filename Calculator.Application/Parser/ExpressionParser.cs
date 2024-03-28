using System;
using Calculator.Core.Nodes;
using Calculator.Core.Consts;

namespace Calculator.Core.Extensions
{
    public class ExpressionParser
    {
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

        private double EvaluateExpression(ExpressionNode expression)
        {
            if (expression is NumberNode numberNode)
            {
                return numberNode.Value;
            }
            else if (expression is VariableNode variableNode)
            {
                // implementation for variables

                return 0;
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
                // implementation for functions

                return 0;
            }
            else
            {
                throw new Exception("Invalid expression");
            }
        }

        private ExpressionNode ParseExpression(string expression)
        {
            var lexer = new ExpressionLexicalAnalyzer(expression);
            var tokens = lexer.Tokenize();
            var parser = new ExpressionParserExtension(tokens);

            return parser.Parse();
        }
    }
}
