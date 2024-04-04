using Calculator.Core.Extensions;
using Calculator.Infrastructure.Data;
using System;

namespace Calculator.Application.Extensions
{
    public class FunctionParserExtension
    {
        private readonly ExpressionContext _context;

        public FunctionParserExtension(ExpressionContext context)
        {
            _context = context;
        }

        public void ParseAndEvaluate(string expression)
        {
            try
            {
                ParseExpression(expression);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ParseExpression(string expression)
        {
            string[] parts = expression.Split('=');

            if (parts.Length != 2)
            {
                throw new Exception("Invalid function definition.");
            }

            string functionSignature = parts[0].Trim();
            string functionBody = parts[1].Trim();

            string[] functionParts = functionSignature.Split('(');

            if (functionParts.Length != 2)
            {
                throw new Exception("Invalid function signature.");
            }

            string functionName = functionParts[0].Trim();
            string parametersStr = functionParts[1].TrimEnd(')');

            string[] parameterNames = parametersStr.Split(',');

            foreach (var parameterName in parameterNames)
            {
                _context.AddVariable(parameterName.Trim(), 0);
            }

            Func<double, double, double> function = (x, y) =>
            {
                _context.AddVariable(parameterNames[0].Trim(), x);
                _context.AddVariable(parameterNames[1].Trim(), y);

                ExpressionParser parser = new ExpressionParser(_context);
                double result = parser.ParseAndEvaluate(functionBody);

                return result;
            };

            _context.AddFunction(functionName, function);
        }
    }
}
