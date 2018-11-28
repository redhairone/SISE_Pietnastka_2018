namespace Logic
{
    class Tools
    {
        public static void Swap<T>(ref T A, ref T B)
        {
            T temp = A;
            A = B;
            B = temp;
        }
    }
}
