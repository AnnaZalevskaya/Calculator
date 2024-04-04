namespace Calculator.Core.Nodes
{
    public class NumberNode : ExpressionNode
    {
        public double Value { get; }

        public NumberNode(double value)
        {
            Value = value;
        }
    }
}
