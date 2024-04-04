using System.Diagnostics;
using Calculator.Application.Services.Interfaces;

namespace Calculator.Application.Services.Implementations
{
    public class ExpressionEvaluator
    {
        private readonly IVariableService _variableService;
        public ExpressionEvaluator(IVariableService variableService)
        {
            _variableService = variableService;
        }

        public double EvaluateExpression(string expression)
        {
            var parser = new NCalc.Expression(expression);
           // parser.EvaluateParameter += EvaluateParameter;
            return (double)parser.Evaluate();
        }

        private double EvaluateParameter(string name)
        {
            return _variableService.GetvariableValue(name);
        }
    }
}
