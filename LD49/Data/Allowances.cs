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
        
        // AD-HOC
        public bool firstLevelTutorial;
    }
}
