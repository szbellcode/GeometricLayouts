namespace Console.Extensions
{
    public static class IntegerExtensions
    {
        public static bool BetweenInclusive(this int num, int lower, int upper)
        {
            return Between(num, lower, upper, true);
        }

        public static bool BetweenNonInclusive(this int num, int lower, int upper)
        {
            return Between(num, lower, upper, false);
        }

        private static bool Between(this int num, int lower, int upper, bool inclusive)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
    }
}
