using System;
using Calculator.Application.Services.Interfaces;
using Calculator.Infrastructure.Data;

namespace Calculator.Application.Services.Implementations
{
    public class VariableService : IVariableService
    {
        private readonly ExpressionContext _context;
        private readonly IExpressionParsingService _expressionParsingService;

        public VariableService(ExpressionContext context, IExpressionParsingService expressionParsingService)
        {
            _context = context;
            _expressionParsingService = expressionParsingService;
        }

        public void SetVariable(string expression)
        {
            _expressionParsingService.SaveVariable(expression);
        }

        public void SetFunction(string expression)
        {
            _expressionParsingService.SaveFunction(expression);
        }

        public double GetVariableValue(string name)
        {
            if (_context.ContainsVariable(name))
            {
                return _context.GetVariable(name);
            }
            else
            {
                throw new ArgumentException($"Переменная '{name}' не найдена");
            }
        }

        public event EventHandler<string> EvaluateParameter;

        internal void OnEvaluateParameter(string name)
        {
            EvaluateParameter?.Invoke(this, name);
        }
    }
}
