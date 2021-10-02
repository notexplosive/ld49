namespace LD49.Data
{
    public class Infinity : SpecialNumber
    {
        public static readonly SpecialNumber Instance = new Infinity();

        private Infinity() : base(int.MaxValue)
        {
        }
    }
}
