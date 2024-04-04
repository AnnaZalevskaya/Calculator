namespace Calculator.Application.Services.Interfaces
{
    public interface IVariableService
    {
        void Setvariable(string name, double value);
        double GetvariableValue(string name);
    }
}
