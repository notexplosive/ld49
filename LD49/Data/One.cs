namespace LD49.Data
{
    public class One : SpecialNumber
    {
        public static readonly SpecialNumber Instance = new One();

        private One() : base(1)
        {
        }
    }
}
