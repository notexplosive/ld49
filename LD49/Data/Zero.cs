namespace LD49.Data
{
    public class Zero : SpecialNumber
    {
        public static readonly SpecialNumber Instance = new Zero();

        private Zero() : base(0)
        {
        }
    }
}
