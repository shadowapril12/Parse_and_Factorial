using System;

namespace Parse2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while (true)
            {
                ///s - строка считываемая с консоли(Считывает математическое выражение в виде строки)
                string s = Console.ReadLine();
                Console.WriteLine("Результат {0}", CalcExpression(s));
            } 
        }
        
        /// <summary>
        /// CalcPlusMinus - производит сложение и вычитание считываемых из строки чисел
        /// </summary>
        /// <param name="s">Вводимая пользователем строка</param>
        /// <returns>Возвращает результат сложения или вычитания</returns>
        static int CalcPlusMinus(string s)
        {
            //index - индекс считываемого символа
            int index = 0;

            //num - возвращаемое функцией Rnum считанное число
            int num = Rnum(s, ref index);

            //Цикл продолжается до тех пор, пока индекс меньше длины строки
            while(index < s.Length)
            {
                if(s[index] == '+')
                {
                    index++;

                    //b - переменная, хранящая число справа от математического оператора
                    int b = Rnum(s, ref index);
                    num += b;
                }
                else if(s[index] == '-')
                {
                    index++;
                    int b = Rnum(s, ref index);
                    num -= b;
                }
            }
            return num;
        }

        /// <summary>
        /// Rnum - метод, который считывает из строки число, посимвольно, справа налево.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        static int Rnum(string s, ref int i)
        {
            //buff - переменная, в которую будут конкатенироваться числовые символы из строки
            string buff = "0";

            //В цикле происходит проверка на число символа и сама конкатенация
            for (; i < s.Length && char.IsDigit(s[i]); i++)
            {
                buff += s[i];
            }

            return int.Parse(buff);
        }


        /// <summary>
        /// Lnum - метод, который считывает число из строки, слева от знака математического оператора
        /// </summary>
        /// <param name="s">Строка, передаваемая в функцию</param>
        /// <param name="i">Индекс символа считываемый из строки</param>
        /// <returns></returns>
        static int Lnum(string s, ref int i)
        {
            //buff - переменная, в которую будут конкатенироваться символы, справа налево
            string buff = "";

            //Цикл проверки символа на число и сама конкатенация
            for(; i >= 0 && char.IsDigit(s[i]); i--)
            {
                buff += s[i];
            }

            ///Приведение строки к массиву символов
            ///Реверс массива
            ///Обратное приведение массива к строке
            ///Возврат полученного числа
            char[] buf = buff.ToCharArray();
            Array.Reverse(buf);
            buff = new string(buf);

            return int.Parse(buff);
        }

        /// <summary>
        /// CalcMulDiv - метод обнаружения математических операторов деления и умножения,
        /// и выполнения этих операций
        /// </summary>
        /// <param name="s">Строка передаваемая в метод</param>
        /// <returns>Возвращает строку, состоящую из предыдущей строки,
        /// с заменой операций умножения и деления на результаты этих операций</returns>
        static string CalcMulDiv(string s)
        {
            string result = s;
            
            //Цикл выполняется до тех пор, пока в строке присутствуют символы умножения и деления
            while (result.Contains("*") || result.Contains("/"))
            {
                //Индекс символа справа от математического оператора
                int lIndex = 0;

                //Индекс символа слева от математического оператора
                int rIndex = 0;

                //Результат выполнения математической опреации
                int localRes = 0;

                if (result.Contains("*"))
                {
                    //i - индекс знака умножения
                    int i = result.IndexOf("*");
                    lIndex = i - 1;
                    rIndex = i + 1;

                    //Вычисление результата, путем вызовы двух ранее описанных методов
                    localRes = Lnum(result, ref lIndex) * Rnum(result, ref rIndex);
                }
                else if(result.Contains("/"))
                {
                    //i - индекс знака деления
                    int i = result.IndexOf("/");
                    lIndex = i - 1;
                    rIndex = i + 1;

                    //Вычисление результата, путем вызовы двух ранее описанных методов
                    localRes = Lnum(result, ref lIndex) / Rnum(result, ref rIndex);
                }
                result = result.Substring(0, lIndex + 1) + localRes +
                    (rIndex < result.Length ? result.Substring(rIndex, result.Length - rIndex) : "");
                
            }

            return result;
        }

        /// <summary>
        /// FindFactorial - метод нахождения чисел, которым необходимо вычислеить факториал
        /// </summary>
        /// <param name="s">Строка передаваемая в метод</param>
        /// <returns>Строка с результатми вычисления факториалов</returns>
        static string FindFactorial(string s)
        {
            string res = s;
            while (res.Contains("!"))
            {
                //facIdx - индекс знака восклицания
                int facIdx = res.IndexOf("!");
                int i;

                //Переменная к которой будут конкатенироваться символы, слева от знака восклицания
                string buff = "";

                for (i = facIdx - 1; i >= 0 && char.IsDigit(res[i]); i--)
                {
                    buff += res[i];
                }

                ///Приведение строки к массиву символов
                ///Реверс массива
                ///Обратное приведение массива к строке
                ///Возврат полученного числа
                char[] buf = buff.ToCharArray();
                Array.Reverse(buf);
                buff = new string(buf);

                ///factorial - переменнная, в которой будет хранится результат вычисления факториала,
                ///т.е. метода CalcFactorial
                int factorial = CalcFactorial(int.Parse(buff));


                res = res.Substring(0, i + 1) + Convert.ToString(factorial) +
                    res.Substring(facIdx + 1, res.Length - 1 - facIdx);
            }

            return res;
        }

        /// <summary>
        /// CalcFactorial - функция рекурсивно вычисляющая факториал
        /// </summary>
        /// <param name="n">Число, для которого вычисляется факториал</param>
        /// <returns>Результат вычисления факториала</returns>
        static int CalcFactorial(int n)
        {
            int facRes = 0;

            if(n == 1)
            {
                return n;
            }
            else
            {
                facRes = CalcFactorial(n - 1) * n;
            }
            return facRes;
        }

        /// <summary>
        /// CalcExpression - основной метод функции, обнаруживает позиции скобок, для сохранения
        /// приоритетности выполнения математических операций. В который передается строка.
        /// Включает в себя выполнение всех ранее описанных методов
        /// </summary>
        /// <param name="s">Выражение передаваемое в виде строки</param>
        /// <returns>Возвращает результат вычисления</returns>
        static string CalcExpression(string s)
        {
            //Цикл, выполняющий до тех пор, пока в строке присутствую открывающиеся скобки
            while(s.Contains("("))
            {
                //idx - индекс открывающейся скобки
                int idx = s.IndexOf("(");

                ///lvl - счетчик, увеличивающийся при обнаружении открывающейся скобки,
                ///и уменьшающийся при обнаружении закрывающейся
                int lvl = 1;
                int i;
                for(i = idx; lvl > 0 && i < s.Length; i++)
                {
                    if(s[i] == ')')
                    {
                        lvl--;
                    }
                    if(s[i] == '(')
                    {
                        lvl++;
                    }
                }

                ///localRes - строка, содержащая внутри внешних скобок, передаваемая в функцию повторно,
                ///для обнаружения внутренних скобок
                string localRes = CalcExpression(s.Substring(idx + 1, i - idx - 2));
                
                s = s.Substring(0, idx) + localRes + 
                    (i < s.Length ? s.Substring(i, s.Length - i) : "");
            }

            //Нахождение факториалов
            s = FindFactorial(s);

            //Выполнение операций умножения и деления
            s = CalcMulDiv(s);

            //Выполнение операция сложения и вычитаня, возврат результата
            return Convert.ToString(CalcPlusMinus(s));
        }
    }

    
}
