using System;
using System.Collections.Generic;
using Calculator.Application.Services.Interfaces;

namespace Calculator.Application.Services.Implementations
{
    public class VariableService : IVariableService
    {
        private readonly Dictionary<string, double> _variables = new Dictionary<string, double>();
        public void Setvariable(string name, double value)
        {
            if (_variables.ContainsKey(name))
            {
                _variables[name] = value;
            }
            else
            {
                _variables.Add(name, value);
            }
        }

        public double GetvariableValue(string name)
        {
            if (_variables.ContainsKey(name))
            {
                return _variables[name];
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
