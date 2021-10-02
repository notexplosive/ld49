namespace LD49.Data
{
    public class NamedVariable : MathExpression
    {
        private static int totalIndex;
        private readonly int index;
        private readonly string name;

        public static readonly NamedVariable X = new NamedVariable("X");
        public static readonly NamedVariable Y = new NamedVariable("Y");
        public static readonly NamedVariable Z = new NamedVariable("Z");
        
        private NamedVariable(string name)
        {
            this.index = NamedVariable.totalIndex++;
            this.name = name;
        }

        public override int UnderlyingValue => int.MinValue + this.index;

        public override string ToString()
        {
            return this.name;
        }
    }
}
