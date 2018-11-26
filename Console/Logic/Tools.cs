using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Tools
    {
        public static void Swap<T>(ref T A, ref T B)
        {
            T temp = A;
            A = B;
            B = A;
        }
    }
}
