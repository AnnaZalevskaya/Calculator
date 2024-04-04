using Calculator.Application.Services.Interfaces;
using Calculator.Core.Extensions;

namespace Calculator.Application.Services.Implementations
{
    public class ExpressionParsingService : IExpressionParsingService
    {
        private readonly ExpressionParser _parser;

        public ExpressionParsingService(ExpressionParser parser)
        {
            _parser = parser;
        }

        public double EvaluateExpression(string expression)
        {
            return _parser.ParseAndEvaluate(expression);
        }

        public void SaveVariable(string expression)
        {
            _parser.ParseAndSaveVariable(expression);
        }

        public void SaveFunction(string expression)
        {
            _parser.ParseAndSaveFunction(expression);
        }
    }
}