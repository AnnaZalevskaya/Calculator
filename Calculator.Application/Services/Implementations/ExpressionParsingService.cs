using Calculator.Application.Services.Interfaces;
using Calculator.Core.Extensions;
using Calculator.Infrastructure.Data;

namespace Calculator.Application.Services.Implementations
{
    public class ExpressionParsingService : IExpressionParsingService
    {
        private readonly ExpressionParser _parser;
        private readonly ExpressionContext _context;

        public ExpressionParsingService()
        {
            _context = new ExpressionContext();
            _parser = new ExpressionParser(_context);
        }

        public double EvaluateExpression(string expression)
        {
            return _parser.ParseAndEvaluate(expression);
        }
    }
}