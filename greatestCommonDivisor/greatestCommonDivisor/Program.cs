using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greatestCommonDivisor
{
    class Program
    {
        public static int Gcd(int p, int q)
        {
            if (q == 0) return p;
            int r = p % q;
            return Gcd(q, r);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Gcd(100, 2));
            Console.ReadLine();
        }
    }
}
