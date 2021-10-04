using System;

namespace LD49.Data
{
    public class Level
    {
        public static Level[] All =
        {
            new Level(Allowances.EverythingEnabled,
                new Equation(Zero.Instance, MathOperator.Add(Prime.Thirteen, NamedVariable.X))),
            new Level(Allowances.EverythingEnabled, new Equation(Prime.Seven)),
            new Level(Allowances.EverythingEnabled,
                new Equation(Zero.Instance, MathOperator.Add(Prime.Thirteen, NamedVariable.X)))
        };

        public readonly Allowances allowances;
        public readonly MathExpression endingExpression = NamedVariable.X;
        public readonly Equation startingEquation = new Equation(One.Instance, One.Instance);

        public Level(Allowances allowances)
        {
            this.allowances = allowances;
        }

        public Level(Allowances allowances, Equation startingEquation) : this(allowances)
        {
            this.startingEquation = startingEquation;
        }

        public Level(Allowances allowances, Equation startingEquation, MathExpression endingExpression) : this(
            allowances, startingEquation)
        {
            this.endingExpression = endingExpression;
        }
    }
}
