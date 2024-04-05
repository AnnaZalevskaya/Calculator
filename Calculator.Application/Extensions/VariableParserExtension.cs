using Calculator.Core.Consts;
using Calculator.Infrastructure.Data;
using System;

namespace Calculator.Application.Extensions
{
    public class VariableParserExtension
    {
        private readonly ExpressionContext _context;

        public VariableParserExtension(ExpressionContext context)
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
            string[] parts = expression.Split(OperatorConsts.EqualsSign);

            if (parts.Length != 2)
            {
                throw new Exception("Invalid variable assignment.");
            }

            string variableName = parts[0].Trim();
            string variableValueStr = parts[1].Trim();

            if (!double.TryParse(variableValueStr, out double variableValue))
            {
                throw new Exception("Invalid variable value.");
            }

            _context.AddVariable(variableName, variableValue);
        }
    }
}
