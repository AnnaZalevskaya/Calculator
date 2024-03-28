namespace Calculator.Core.Nodes
{
    public class BinaryOperatorNode : ExpressionNode
    {
        public char MathOperator { get; }
        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }

        public BinaryOperatorNode(char mathOperator, ExpressionNode left, ExpressionNode right)
        {
            MathOperator = mathOperator;
            Left = left;
            Right = right;
        }
    }
}
