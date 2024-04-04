namespace Calculator.Application.Services.Interfaces
{
    public interface IExpressionParsingService
    {
        double EvaluateExpression(string expression);
        void SaveVariable(string expression);
        void SaveFunction(string expression);
    }
}
