namespace Calculator.Application.Services.Interfaces
{
    public interface IExpressionParsingService
    {
        double EvaluateExpression(string expression);
    }
}
