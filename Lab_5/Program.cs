using System;
using MyFrac = (long nom, long denom);

namespace Lab_5
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine(
                    """
                    ------------------------------------------------------------------------------------------------------------------------
                                                          Демонстрація роботи методів класу FracOperations
                    ------------------------------------------------------------------------------------------------------------------------
                    """
                    );


            FracOperations.Results();
        }
        public static void Error() { Console.WriteLine("Спробуйте ще раз"); }
    }
    public class FracOperations
    {
        public static void Results()
        {
            MyFrac frac1 = InputTuple();

            Console.WriteLine($"Метод MyFracToString формує рядкове подання дробу з кортежа у форматі \"чисельник / знаменник\": {MyFracToString(frac1)}");
            MyFrac normalizedFrac = Normalize(frac1);
            Console.WriteLine($"Метод Normalize \"нормалізує\" дріб кортежа f, тобто: 1) скорочує його; 2) робить знаменник невід'ємним: {MyFracToString(normalizedFrac)}");
            Console.WriteLine($"Метод ToStringWithIntPart формує рядкове подання дробу з кортежу f з виділеною цілею частиною: {ToStringWithIntPart(frac1)}");
            Console.WriteLine($"Метод DoubleValue формує дійсне значення дробу f: {DoubleValue(frac1)}");
            Console.WriteLine("Для виконання подальших завдань потрібно ввести ще один дріб");
            MyFrac frac2 = InputTuple();
            MyFrac plus = Plus(frac1, frac2);
            Console.WriteLine($"Метод Plus рахує суму двох дробів f1 + f2: {MyFracToString(Normalize(plus))}");
            MyFrac minus = Minus(frac1, frac2);
            Console.WriteLine($"Метод Minus рахує рівзницю двох дробів f1 - f2: {MyFracToString(Normalize(minus))}");
            MyFrac multiply = Multiply(frac1, frac2);
            Console.WriteLine($"Метод Multiply рахує добуток двох дробів f1 * f2: {MyFracToString(Normalize(multiply))}");
            MyFrac divide = Divide(frac1, frac2);
            Console.WriteLine($"Метод Divide рахує чатку двох дробів f1/f2: {MyFracToString(Normalize(divide))}");

        }
        private static MyFrac InputTuple()
        {
            int n = Input("Введіть чисельник: ");
            int d = Input("Введіть знаменник: ");
            return (n, d);
        }
        static int Input(string s)
        {
            do
            {
                Console.Write(s);
                if (int.TryParse(Console.ReadLine(), out int a)) return a;
                else Program.Error();
            }
            while (true);
        }
        static string MyFracToString(MyFrac f)
        {
            return $"{f.nom}/{f.denom}";
        }
        static MyFrac Normalize(MyFrac f)
        {
            if (f.denom == 0)
            {
                return f;
            }

            if (f.nom == 0)
            {
                return new MyFrac(0, 1);
            }

            if (f.denom < 0)
            {
                f.nom = -f.nom;
                f.denom = -f.denom;
            }

            long divisor = NSD(f.nom, f.denom);

            if (divisor > 1)
            {
                f.nom /= divisor;
                f.denom /= divisor;
            }

            return new MyFrac(f.nom, f.denom);
        }
        static long NSD(long n, long d)
        {
            n = Math.Abs(n);
            d = Math.Abs(d);

            while (d != 0)
            {
                long temp = d;
                d = n % d;
                n = temp;
            }

            return n;
        }
        static string ToStringWithIntPart(MyFrac f)
        {
            long intPart = f.nom / f.denom;
            string res = "";

            if (intPart < 0)
            {
                return res += $"-({intPart} + {Normalize(f)})";
            }
            else
            {
                return res += $"({intPart} + {Normalize(f)})";
            }
        }
        static double DoubleValue(MyFrac f)
        {
            return (double)f.nom / (double)f.denom;
        }
        static MyFrac Plus(MyFrac f1, MyFrac f2)
        {
            return (f1.nom * f2.denom + f1.denom * f2.nom, f1.denom * f2.denom);
        }
        static MyFrac Minus(MyFrac f1, MyFrac f2)
        {
            return (f1.nom * f2.denom - f1.denom * f2.nom, f1.denom * f2.denom);
        }
        static MyFrac Multiply(MyFrac f1, MyFrac f2)
        {
            return (f1.nom * f2.nom, f1.denom * f2.denom);
        }
        static MyFrac Divide(MyFrac f1, MyFrac f2)
        {
            return (f1.nom * f2.denom, f1.denom * f2.nom);
        }
    }
}