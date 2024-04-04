using System.Collections.Generic;
using System;

namespace Calculator.Infrastructure.Data
{
    public class ExpressionContext
    {
        private readonly Dictionary<string, double> variables;
        private readonly Dictionary<string, Func<double, double, double>> functions;

        public ExpressionContext()
        {
            variables = new Dictionary<string, double>();
            functions = new Dictionary<string, Func<double, double, double>>();
        }

        public void AddVariable(string name, double value)
        {
            variables[name] = value;
        }

        public double GetVariable(string name)
        {
            if (variables.TryGetValue(name, out double value))
            {  
                return value; 
            }

            throw new Exception($"Variable '{name}' not found.");
        }

        public void RemoveVariable(string name)
        {
            variables.Remove(name);
        }

        public void AddFunction(string name, Func<double, double, double> function)
        {
            functions[name] = function;
        }

        public Func<double, double, double> GetFunction(string name)
        {
            if (functions.TryGetValue(name, out Func<double, double, double> function))
            {
                return function;
            }

            throw new Exception($"Function '{name}' not found.");
        }

        public void RemoveFunction(string name)
        {
            functions.Remove(name);
        }
    }
}
