namespace Calculator.Core.Nodes
{
    public class FunctionNode : ExpressionNode
    {
        public string Name { get; }
        public ExpressionNode[] Arguments { get; }

        public FunctionNode(string name, ExpressionNode[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}
