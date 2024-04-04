using Calculator.Application.Services.Interfaces;

namespace Calculator.Application.Evaluator
{
    public class ExpressionEvaluator
    {
        private readonly IVariableService _variableService;
        private readonly IExpressionParsingService _expressionParsingService;

        public ExpressionEvaluator(IVariableService variableService, IExpressionParsingService expressionParsingService)
        {
            _variableService = variableService;
            _expressionParsingService = expressionParsingService;
        }

        public double EvaluateExpression(string expression)
        {
            return _expressionParsingService.EvaluateExpression(expression);
        }

        public void SaveVariable(string expression)
        {
            _variableService.SetVariable(expression);
        }

        public void SaveFunction(string expession)
        {
            _variableService.SetFunction(expession);
        }
    }
}
