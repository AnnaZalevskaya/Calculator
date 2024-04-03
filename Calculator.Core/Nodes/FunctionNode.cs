namespace Calculator.Core.Nodes
{
    public class FunctionNode : ExpressionNode
    {
        public string Name { get; }
        public ExpressionNode Argument1 { get; }
        public ExpressionNode Argument2 { get; }

        public FunctionNode(string name, ExpressionNode argument1, ExpressionNode argument2)
        {
            Name = name;
            Argument1 = argument1;
            Argument2 = argument2;
        }
    }
}
