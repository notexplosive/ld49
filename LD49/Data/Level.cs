namespace LD49.Data
{
    public class Level
    {
        public static Level[] All =
        {
            new Level(Allowances.EverythingEnabled, Prime.Seven),
            new Level(Allowances.EverythingEnabled, Prime.Thirteen)
        };

        public readonly Allowances allowances;
        public readonly MathExpression endingExpression = NamedVariable.X;
        public readonly MathExpression startingExpression = One.Instance;

        public Level(Allowances allowances)
        {
            this.allowances = allowances;
        }

        public Level(Allowances allowances, MathExpression startingExpression) : this(allowances)
        {
            this.startingExpression = startingExpression;
        }
        
        public Level(Allowances allowances, MathExpression startingExpression, MathExpression endingExpression) : this(allowances,startingExpression)
        {
            this.endingExpression = endingExpression;
        }
    }
}