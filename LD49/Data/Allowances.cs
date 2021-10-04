namespace LD49.Data
{
    public struct Allowances
    {
        // MAIN EXPRESSION
        public bool allowAddingTo_Expression;
        public bool allowSubtractingTo_Expression;
        public bool allowDividingTo_Expression;
        public bool allowMultiplyingTo_Expression;

        // STORAGE
        public bool allowAddingTo_Storage;
        public bool allowMultiplyingTo_Storage;
        public bool allowInverting_Storage;
        public bool allowNegating_Storage;

        public bool AllowStorage => this.allowAddingTo_Storage
                                    || this.allowMultiplyingTo_Storage
                                    || this.allowInverting_Storage
                                    || this.allowNegating_Storage;

        // VALUES
        public bool allowXNamedValue;
        public bool allowAllPrimes;

        // WHATEVER THIS IS
        public bool allowDistribute;
        
        // AD-HOC
        public bool firstLevelTutorial;

        // static instances for convenience
        public static readonly Allowances EverythingEnabled = new Allowances
        {
            // MAIN EXPRESSION
            allowAddingTo_Expression = true,
            allowSubtractingTo_Expression = true,
            allowDividingTo_Expression = true,
            allowMultiplyingTo_Expression = true,

            // STORAGE
            allowAddingTo_Storage = true,
            allowMultiplyingTo_Storage = true,
            allowInverting_Storage = true,
            allowNegating_Storage = true,

            // VALUES
            allowXNamedValue = true,
            allowAllPrimes = true,
            
            // leaving this off for now, I think it's too confusing
            allowDistribute = false
        };
        
        public static readonly Allowances OnlyAddSubtract_OneZero_Tutorial = new Allowances
        {
            // MAIN EXPRESSION
            allowAddingTo_Expression = true,
            allowSubtractingTo_Expression = true,
            allowDividingTo_Expression = false,
            allowMultiplyingTo_Expression = false,

            // STORAGE
            allowAddingTo_Storage = false,
            allowMultiplyingTo_Storage = false,
            allowInverting_Storage = false,
            allowNegating_Storage = false,

            // VALUES
            allowXNamedValue = false,
            allowAllPrimes = false,
            
            // AD-HOC
            firstLevelTutorial = true,
            
            // leaving this off for now, I think it's too confusing
            allowDistribute = false
        };
    }
}
