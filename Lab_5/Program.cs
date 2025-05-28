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
            Console.WriteLine($"Метод ToStringWithIntPart формує рядкове подання дробу з кортежу f з виділеною цілею частиною: {(ToStringWithIntPart(frac1))}");
            string doubleNum = frac1.denom == 0 ? "Неможливо вивести дійсне значення дробу, коли знаменник дорівнює 0" : $"{DoubleValue(frac1)}";
            Console.WriteLine($"Метод DoubleValue формує дійсне значення дробу f: {doubleNum}");
            Console.WriteLine("Для виконання подальших завдань потрібно ввести ще один дріб");
            MyFrac frac2 = InputTuple();
            MyFrac plus = Plus(frac1, frac2);
            string plusRes = plus.denom == 0 ? "Неможливо вивести суму дробів, коли знаменник дорівнює 0" : $"{MyFracToString(Normalize(plus))}";
            Console.WriteLine($"Метод Plus рахує суму двох дробів f1 + f2: {plusRes}");
            MyFrac minus = Minus(frac1, frac2);
            string minusRes = minus.denom == 0 ? "Неможливо вивести суму дробів, коли знаменник дорівнює 0" : $"{MyFracToString(Normalize(minus))}";
            Console.WriteLine($"Метод Minus рахує рівзницю двох дробів f1 - f2: {minusRes}");
            MyFrac multiply = Multiply(frac1, frac2);
            string multiplyRes = multiply.denom == 0 ? "Неможливо вивести суму дробів, коли знаменник дорівнює 0" : $"{MyFracToString(Normalize(multiply))}";
            Console.WriteLine($"Метод Multiply рахує добуток двох дробів f1 * f2: {multiplyRes}");
            MyFrac divide = Divide(frac1, frac2);
            string divideRes = divide.denom == 0 ? "Неможливо вивести суму дробів, коли знаменник дорівнює 0" : $"{MyFracToString(Normalize(divide))}";
            Console.WriteLine($"Метод Divide рахує чатку двох дробів f1/f2: {divideRes}");
            Console.Write("Введіть число для роботи подальших методів: ");
            int n = int.Parse(Console.ReadLine());
            MyFrac calc1 = CalcExpr1(n);
            Console.WriteLine($"Метод CalcExpr1 рахує, користуючись вищезгаданими методами, суму 1/(1*2)+1/(2*3)+1/(3*4)+...+1/(n*(n+1)) як дріб: {MyFracToString(calc1)}");
            MyFrac calc2 = CalcExpr2(n);
            Console.WriteLine($"рахує, користуючись вищезгаданими методами, добуток (1–1/4)*(1–1/9)*(1–1/16)*...*(1–1/n^2) як дріб: {MyFracToString(calc2)}");
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
                return new MyFrac(0, 0);
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
            long intPart = f.denom == 0 ? 0 : f.nom / f.denom;
            string res = "";
            
            if (f.denom == 0)
            {
                return res = "Неможливо вивести цілу частину, коли знаменник дорівнює 0";
            }
            else
            {
                MyFrac normIt = Normalize(Minus((f.nom, f.denom), (f.denom * intPart, f.denom)));

                if (intPart < 0)
                {
                    normIt.nom = Math.Abs(normIt.nom);
                    normIt.denom = Math.Abs(normIt.denom);
                    return res += $"-({-intPart} + {MyFracToString(normIt)})";
                }
                else if (intPart == 0)
                {
                    return res = $"{MyFracToString(normIt)}";
                }
                else
                {
                    return res += $"({intPart} + {MyFracToString(normIt)})";
                }
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
        static MyFrac CalcExpr1(int n)
        {
            MyFrac res = new MyFrac(1, 1 * 2);

            for (int i = 2; i <= n; i++)
            {
                res = Plus(res, (1, i * (i + 1)));
                res = Normalize(res);
            }

            if (n <= 0)
                return new MyFrac(0, 0);
            else return res;
        }
        static MyFrac CalcExpr2(int n)
        {
            MyFrac res = new MyFrac(1, 1);

            for (int i = 2; i <= n; i++)
            {
                MyFrac one = new MyFrac(1, 1);
                MyFrac oneMinus = Minus(one, new MyFrac(1, i * i));

                res = Multiply(res, oneMinus);
                res = Normalize(res);
            }

            if (n <= 0)
                return new MyFrac(0, 0);
            else return res;
        }
    }
}