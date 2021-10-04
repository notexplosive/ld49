using LD49.Components;

namespace LD49.Data
{
    public interface ILevel
    {
    }

    public class Level : ILevel
    {
        public static ILevel[] All =
        {
            // DEBUG
            new Level(Allowances.EverythingEnabled,
                new Equation(Zero.Instance, MathOperator.Add(Prime.Thirteen, NamedVariable.X))),
            // DEBUG
            
            Poem.Introduction,
            new Level(Allowances.OnlyAddSubtract_OneZero_Tutorial,
                new Equation(MathOperator.Add(One.Instance, MathOperator.Add(One.Instance, One.Instance))),
                Zero.Instance),
            
            Poem.IntroduceX,
            // one level X + 1 = 1 
            Poem.IntroducePrimes,
            // 3 levels with just primes
            //      - just add and subtract
            new Level(Allowances.EverythingEnabled,
                new Equation(Zero.Instance, MathOperator.Add(Prime.Thirteen, NamedVariable.X))),
            //      - just multiply and divide
            //      - both mul/div and add/sub - "easy mode"

            Poem.IntroduceNegative,
            // : 0 = X + 1 + 1 + 1 -> X (can only add on main and Negate in storage)
            // : storage can only add and negate, main expression can only add
            Poem.IntroduceReciprocal,
            // : 0 = X * 1 * 1 * 1 -> X (can only multiply on main and Invert in storage)
            // : storage can only multiply and invert, main expression can only multiply
            Poem.IntroduceInfinity,
            // Everything is enabled, except you can only add and multiply on main expression
            Poem.Credits,
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
