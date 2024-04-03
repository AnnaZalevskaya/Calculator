namespace Calculator.Core.Nodes
{
    public class VariableNode : ExpressionNode
    {
        public string Name { get; }

        public VariableNode(string name)
        {
            Name = name;
        }
    }
}
