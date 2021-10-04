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
#if DEBUG
            new Level(Allowances.EverythingEnabled,
                new Equation(Zero.Instance, MathOperator.Add(Prime.Thirteen, NamedVariable.X))),
#endif

            Poem.Introduction,

            new Level(Allowances.OnlyAddSubtract_OneZero_Tutorial,
                new Equation(MathOperator.Add(One.Instance, MathOperator.Add(One.Instance, One.Instance))),
                Zero.Instance),

            Poem.IntroduceX,

            new Level(
                new Allowances().EnableAddingTo_Expression().EnableSubtractingTo_Expression().EnableXNamedValue(),
                new Equation(AddMathExpression.CreateMany(NamedVariable.X, One.Instance,One.Instance,One.Instance,One.Instance,One.Instance), One.Instance)
            ),

            Poem.IntroducePrimes,
            // 3 levels with just primes
            //      - just add and subtract
            new Level(
                new Allowances()
                    .EnableAddingTo_Expression().EnableSubtractingTo_Expression()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(MathOperator.Add(Prime.FiftyNine, Prime.Seven),
                    AddMathExpression.CreateMany(Prime.Thirteen, Prime.Seventeen, Prime.EightyNine, NamedVariable.X))),
            //      - just multiply and divide
            //      todo: this is basically identical to the previous one... maybe that's OK?
            new Level(
                new Allowances()
                    .EnableDividingTo_Expression().EnableMultiplyingTo_Expression()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(MathOperator.Multiply(Prime.FiftyNine, Prime.Seven),
                    MultiplyMathExpression.CreateMany(Prime.Thirteen, Prime.Seventeen, Prime.EightyNine,
                        NamedVariable.X))),
            //      - both mul/div and add/sub - "easy mode"
            new Level(
                new Allowances()
                    .EnableAddingTo_Expression().EnableSubtractingTo_Expression()
                    .EnableDividingTo_Expression().EnableMultiplyingTo_Expression()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    MathOperator.Add(
                        Prime.ThirtyOne,
                        Prime.SeventyThree),
                    MultiplyMathExpression.CreateMany(
                        Prime.SixtyOne,
                        Prime.SeventyOne,
                        Prime.SixtySeven,
                        AddMathExpression.CreateMany(
                            MathOperator.Multiply(NamedVariable.X, Prime.TwentyThree), 
                            Prime.Five, 
                            Prime.FiftyThree)))),

            Poem.IntroduceNegative,
            
            // -X = 0, multiply by negative one
            new Level(
                new Allowances()
                    .EnableMultiplyingTo_Expression()
                    .EnableAddingTo_Storage()
                    .EnableNegating_Storage()
                    .EnableXNamedValue(),
                new Equation(
                    Zero.Instance, MathOperator.Negate(NamedVariable.X))
            ),
            
            // : storage can only add and negate, main expression can only add
            new Level(
                new Allowances()
                    .EnableAddingTo_Expression()
                    .EnableNegating_Storage()
                    .EnableAddingTo_Storage()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    Zero.Instance, AddMathExpression.CreateMany(NamedVariable.X, Prime.Eleven, Prime.Three, Prime.Seven))
                ),
            
            // : storage can only add,mul and negate, main expression can only add
            new Level(
                new Allowances()
                    .EnableAddingTo_Expression()
                    .EnableNegating_Storage()
                    .EnableAddingTo_Storage()
                    .EnableMultiplyingTo_Storage()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    Zero.Instance, AddMathExpression.CreateMany(NamedVariable.X, MultiplyMathExpression.CreateMany(Prime.Three, Prime.Seven, Prime.Five)))
            ),
            
            new Level(
                new Allowances()
                    .EnableAddingTo_Expression()
                    .EnableNegating_Storage()
                    .EnableAddingTo_Storage()
                    .EnableMultiplyingTo_Storage()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    Zero.Instance, AddMathExpression.CreateMany(NamedVariable.X, Prime.Eleven, MultiplyMathExpression.CreateMany(Prime.Three, MathOperator.Negate(Prime.Seven), Prime.Five), Prime.Seven))
            ),
            
            Poem.IntroduceReciprocal,
            
            
            
            // : 0 = X * 1 * 1 * 1 -> X (can only multiply on main and Invert in storage)
            // : storage can only multiply and invert, main expression can only multiply
            Poem.IntroduceInfinity,
            // Everything is enabled, except you can only add and multiply on main expression
            Poem.Credits
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
