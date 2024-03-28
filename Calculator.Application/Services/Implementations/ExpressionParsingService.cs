using Calculator.Application.Services.Interfaces;
using Calculator.Core.Extensions;

namespace Calculator.Application.Services.Implementations
{
    public class ExpressionParsingService : IExpressionParsingService
    {
        private readonly ExpressionParser parser;

        public ExpressionParsingService()
        {
            parser = new ExpressionParser();
        }

        public double EvaluateExpression(string expression)
        {
            return parser.ParseAndEvaluate(expression);
        }
    }
}
