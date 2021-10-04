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
                new Equation(
                    AddMathExpression.CreateMany(NamedVariable.X, One.Instance, One.Instance, One.Instance,
                        One.Instance, One.Instance), One.Instance)
            ),

            Poem.IntroducePrimes,
            // 3 levels with just primes
            //      - just add and subtract
            new Level(
                new Allowances()
                    .EnableAddingTo_Expression().EnableSubtractingTo_Expression()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(MathOperator.Add(Prime.Eleven, Prime.Seven),
                    AddMathExpression.CreateMany(Prime.Thirteen, Prime.Seventeen, Prime.Two, NamedVariable.X))),
            //      - just multiply and divide
            //      todo: this is basically identical to the previous one... maybe that's OK?
            new Level(
                new Allowances()
                    .EnableDividingTo_Expression().EnableMultiplyingTo_Expression()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(MathOperator.Multiply(Prime.Two, Prime.Seven),
                    MultiplyMathExpression.CreateMany(Prime.Thirteen, Prime.Seventeen, Prime.Three,
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
                        Prime.Seven,
                        Prime.Eleven),
                    MultiplyMathExpression.CreateMany(
                        Prime.Five,
                        Prime.Three,
                        Prime.TwentyNine,
                        AddMathExpression.CreateMany(
                            MathOperator.Multiply(NamedVariable.X, Prime.TwentyThree),
                            Prime.Nineteen,
                            Prime.Seventeen)))),

            Poem.IntroduceNegative,

            // -X = -1, multiply by negative one
            new Level(
                new Allowances()
                    .EnableMultiplyingTo_Expression()
                    .EnableAddingTo_Storage()
                    .EnableNegating_Storage(),
                new Equation(
                    MathOperator.Negate(One.Instance), MathOperator.Negate(NamedVariable.X))
            ),
            
            // -X = 0, subtract -X
            new Level(
                new Allowances()
                    .EnableSubtractingTo_Expression()
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
                    Zero.Instance,
                    AddMathExpression.CreateMany(NamedVariable.X, Prime.Eleven, Prime.Three, Prime.Seven))
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
                    Zero.Instance,
                    AddMathExpression.CreateMany(NamedVariable.X,
                        MultiplyMathExpression.CreateMany(Prime.Three, Prime.Seven, Prime.Five)))
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
                    Zero.Instance,
                    AddMathExpression.CreateMany(NamedVariable.X, Prime.Eleven,
                        MultiplyMathExpression.CreateMany(Prime.Three, MathOperator.Negate(Prime.Seven), Prime.Five),
                        Prime.Seven))
            ),

            Poem.IntroduceReciprocal,

            // 1/X = 1, multiply by X
            new Level(
                new Allowances()
                    .EnableDividingTo_Expression()
                    .EnableInverting_Storage()
                    .EnableMultiplyingTo_Storage()
                    .SetStorageStartValue_One()
                    .EnableXNamedValue(),
                new Equation(
                    One.Instance, MathOperator.Inverse(NamedVariable.X))
            ),

            // : storage can only mul and invert, main expression can only mul
            new Level(
                new Allowances()
                    .EnableMultiplyingTo_Expression()
                    .EnableInverting_Storage()
                    .EnableMultiplyingTo_Storage()
                    .EnableXNamedValue()
                    .SetStorageStartValue_One()
                    .EnableAllPrimes(),
                new Equation(
                    One.Instance,
                    MultiplyMathExpression.CreateMany(NamedVariable.X, Prime.Eleven, Prime.Three, Prime.Seven))
            ),

            // : storage can only add,mul and negate, main expression can only add
            new Level(
                new Allowances()
                    .EnableMultiplyingTo_Expression()
                    .EnableInverting_Storage()
                    .EnableAddingTo_Storage()
                    .EnableMultiplyingTo_Storage()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    One.Instance,
                    MultiplyMathExpression.CreateMany(NamedVariable.X,
                        AddMathExpression.CreateMany(Prime.Three, Prime.Seven, Prime.Five)))
            ),

            new Level(
                new Allowances()
                    .EnableMultiplyingTo_Expression()
                    .EnableInverting_Storage()
                    .EnableAddingTo_Storage()
                    .EnableMultiplyingTo_Storage()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    One.Instance,
                    MultiplyMathExpression.CreateMany(NamedVariable.X, Prime.Eleven,
                        AddMathExpression.CreateMany(Prime.Three, MathOperator.Inverse(Prime.Seven), Prime.Five),
                        Prime.Seven))
            ),

            Poem.IntroduceInfinity,

            // neg(X) * neg(inv(7)) * inv(5)
            new Level(
                new Allowances()
                    .EnableMultiplyingTo_Expression()
                    .EnableAddingTo_Expression()
                    .EnableInverting_Storage()
                    .EnableNegating_Storage()
                    .EnableAddingTo_Storage()
                    .EnableMultiplyingTo_Storage()
                    .EnableXNamedValue()
                    .EnableAllPrimes(),
                new Equation(
                    One.Instance,
                    MathOperator.Add(
                        One.Instance,
                    MultiplyMathExpression.CreateMany(
                        Prime.Three,
                        MathOperator.Negate(NamedVariable.X),
                        MathOperator.Negate(MathOperator.Inverse(Prime.Seven)),
                        MathOperator.Inverse(Prime.Five)
                    )))
            ),

            // x in complex expressions on both sides, need to extract them out

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
