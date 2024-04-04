using Calculator.Core.Consts;
using Calculator.Core.Extensions;
using Calculator.Infrastructure.Data;
using System;
using System.Linq;
using static Calculator.Core.Models.Function;

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
            string[] parts = expression.Split(OperatorConsts.EqualsSign);

            if (parts.Length != 2)
            {
                throw new Exception("Invalid function definition.");
            }

            string functionSignature = parts[0].Trim();
            string functionBody = parts[1].Trim();

            if (!functionSignature.StartsWith(OperatorConsts.Func.ToString()))
            {
                throw new Exception("Invalid function signature. Function name must start with 'f'.");
            }

            string parametersStr = functionSignature
                .Substring(functionSignature.IndexOf(OperatorConsts.OpeningParenthesis) + 1);
            parametersStr = parametersStr.TrimEnd(OperatorConsts.ClosingParenthesis);

            string[] parameterNames = parametersStr.Split(OperatorConsts.Comma);

            int argumentCount = parameterNames.Length;
            string functionName = OperatorConsts.Func.ToString() 
                + OperatorConsts.OpeningParenthesis.ToString() 
                + argumentCount.ToString() 
                + OperatorConsts.ClosingParenthesis.ToString(); 

            GenericFunction<double, double> function = args =>
            {
                for (int i = 0; i < args.Length; i++)
                {
                    _context.AddVariable(parameterNames[i].Trim(), args[i]);
                }

                ExpressionParser parser = new ExpressionParser(_context);
                double result = parser.ParseAndEvaluate(functionBody);

                return result;
            };

            _context.AddFunction(functionName, function);
        }
    }
}
