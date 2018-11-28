using System.Collections.Generic;

namespace Logic
{
    public class FifteenComparer : IEqualityComparer<Fifteen>
    {
        public bool Equals(Fifteen x, Fifteen y)
        {
            return x.CheckSimilarity(y);
        }

        public int GetHashCode(Fifteen obj)
        {
            int hashCode = 1;
            int multiplier = 79;

            unchecked
            {
                foreach(int item in obj.Arr)
                {
                    hashCode = multiplier * hashCode + item.GetHashCode();
                }
            }

            return hashCode;
        }
    }
}
