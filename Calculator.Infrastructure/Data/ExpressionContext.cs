using System.Collections.Generic;
using System;
using static Calculator.Core.Models.Function;

namespace Calculator.Infrastructure.Data
{
    public class ExpressionContext
    {
        private readonly Dictionary<string, double> _variables;
        private readonly Dictionary<string, GenericFunction<double, double>> _functions;

        public ExpressionContext()
        {
            _variables = new Dictionary<string, double>();
            _functions = new Dictionary<string, GenericFunction<double, double>>();
        }

        public void AddVariable(string name, double value)
        {
            _variables[name] = value;
        }

        public double GetVariable(string name)
        {
            if (_variables.TryGetValue(name, out double value))
            {  
                return value; 
            }

            throw new Exception($"Variable '{name}' not found.");
        }

        public bool ContainsVariable(string name)
        {
            return _variables.ContainsKey(name); 
        }

        public void RemoveVariable(string name)
        {
            _variables.Remove(name);
        }

        public void AddFunction(string name, GenericFunction<double, double> function)
        {
            _functions[name] = function;
        }

        public GenericFunction<double, double> GetFunction(string name)
        {
            if (_functions.TryGetValue(name, out GenericFunction<double, double> function))
            {
                return function;
            }

            throw new Exception($"Function '{name}' not found.");
        }

        public bool ContainsFunction(string name)
        {
            return _functions.ContainsKey(name);
        }

        public void RemoveFunction(string name)
        {
            _functions.Remove(name);
        }
    }
}
