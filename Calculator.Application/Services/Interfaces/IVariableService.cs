namespace Calculator.Application.Services.Interfaces
{
    public interface IVariableService
    {
        void SetVariable(string expression);
        double GetVariableValue(string name);
        void SetFunction(string expression);
    }
}
