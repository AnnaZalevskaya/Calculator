namespace Calculator.Core.Models
{
    public class Function
    {
        public delegate TResult GenericFunction<T, TResult>(params T[] args);
    }
}
