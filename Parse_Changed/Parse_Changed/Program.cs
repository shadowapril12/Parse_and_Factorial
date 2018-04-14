using System;

namespace Parse_Changed
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                //Переменная s - хранит строку передаваемую из консоли
                string s = Console.ReadLine();
                var begin = DateTime.Now;
                //Передача строки функции CalcExpression и вывод результата
                Console.WriteLine("Результат равен {0}", CalcExpression(s));
                var end = DateTime.Now;
                var res = end - begin;
                Console.WriteLine("Время выполнения программы {0}\n", res.TotalSeconds);
            }
        }
        /// <summary>
        /// CalcExpression - основной метод, обнаруживает позиции скобок, для сохранения
        /// приоритетности выполнения математических операций.
        /// </summary>
        /// <param name="s">Выражение передаваемое в виде строки</param>
        /// <returns>Возвращает результат вычисления</returns>
        public static string CalcExpression(string s)
        {
            //Цикл, выполняющий до тех пор, пока в строке присутствую открывающиеся скобки
            while (s.Contains("("))
            {
                ///lvl - счетчик, увеличивающийся при обнаружении открывающейся скобки,
                ///и уменьшающийся при обнаружении закрывающейся
                int lvl = 1;

                //idx - индекс открывающейся скобки
                int idx = s.IndexOf("(");
                int i;
                for(i = idx + 1; lvl > 0 && i < s.Length; i++)
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

            return CalcPlusMinus(s) + "";

        }
        /// <summary>
        /// Метод CalcFactorial - служит для нахождения факториала, передаваемого в функцию числа
        /// </summary>
        /// <param name="n">Передаваемое число</param>
        /// <returns>Возаращает значение факториала</returns>
        public static int CalcFactorial(int n)
        {
            int factorial;

            if(n == 1)
            {
                return n;
            }
            else
            {
                factorial = CalcFactorial(n - 1) * n;
            }

            return factorial;
        }

        /// <summary>
        /// Метод MulDivFac - служит для выполнения операция умножения с делением, а также для
        /// нахождения факториала
        /// </summary>
        /// <param name="s">Строка передаваемая в функцию</param>
        /// <param name="i">Индекс символа</param>
        /// <returns>Возвращает результат умножения/деления либо фактрориала</returns>
        public static int MulDivFac(string s, ref int i)
        {
            //Парсированная строка, до первого знака математических операций
            int num = Num(s, ref i);  

            while(i < s.Length)
            {
                ///Если индекс содержит знак восклицания, то выполняется функция,
                ///возвращающая значение факториала
                if (s[i] == '!')
                {
                    int facIdx = i - 1; 
                    num = CalcFactorial(int.Parse(s[facIdx] + ""));
                    i++;
                }
                else if (s[i] == '*')
                {
                    i++;

                    //Num - метод для считывания числа из строки
                    int b = Num(s, ref i);
                    num *= b;
                }
                else if(s[i] == '/')
                {
                    i++;
                    int b = Num(s, ref i);
                    num /= b;
                }
                else
                {
                    return num;
                }
            }

            return num;
        }

        /// <summary>
        /// Метод CalcPlusMinus - служит для выполнения
        /// математических операция сложения и вычитания
        /// </summary>
        /// <param name="s">Передаваемая в функцию строка</param>
        /// <returns>Возвращает сумму или разность, в зависимости от знака</returns>
        public static int CalcPlusMinus(string s)
        {
            int index = 0;

            int num = MulDivFac(s, ref index); 

            while(index < s.Length)
            {
                if(s[index] == '+')
                {
                    index++;
                    int b = MulDivFac(s, ref index);
                    num += b;
                }
                else if(s[index] == '-')
                {
                    index++;
                    int b = MulDivFac(s, ref index);
                    num -= b;
                }
                else
                {
                    Console.WriteLine("Error");
                    return 0;
                }
            }

            return num;
        }

        /// <summary>
        /// Метод Num - служит для считывания числа из строки, останавливается
        /// на первом не числовом символе
        /// </summary>
        /// <param name="s">Передаваемая в функцию строка</param>
        /// <param name="i">Индекс символа строки</param>
        /// <returns>Возвращает парсированную строку</returns>
        public static int Num(string s, ref int i)
        {
            string buff = "0";
            
            for(; i < s.Length && char.IsDigit(s[i]); i++)
            {
                buff += s[i];
            }

            return Convert.ToInt32(buff);
        }



        
    }
}
