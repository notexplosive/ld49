namespace LD49.Data
{
    public struct Allowances
    {
        // MAIN EXPRESSION
        public bool allowAddingToExpression;
        public bool allowSubtractingToExpression;
        public bool allowDividingToExpression;
        public bool allowMultiplyingToExpression;

        // STORAGE
        public bool allowAddingToStorage;
        public bool allowMultiplyingToStorage;
        public bool allowInvertingStorage;
        public bool allowNegatingStorage;
        public bool allowStorage => this.allowAddingToStorage && this.allowMultiplyingToStorage && this.allowInvertingStorage &&
                                    this.allowNegatingStorage;
        
        // VALUES
        public bool allowXNamedValue;
        public bool allowAllPrimes;
    }
}
