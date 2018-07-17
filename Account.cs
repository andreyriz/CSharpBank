using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace ANDRUSCO
{
    //методы для зачисления/растраты денег
    //метод проверки пароля при авторизации
    //метод для установки значения в поле "опасных" попыток авторизации.
    //После "правильной" авторизации значение обнуляется.
    //Вывод данных по аккаунту
    //метод для изменения пароля
    public class Account
    {
        public bool Danger { get; set; }
        public static int Attempt { set; get; }
        public Account(Client a)
        {
            Attempt = 0;
            Danger = false;
        }
        static public void PlusMoney(Client first, Double a, int z)
        {
            if (a > 0 && z >= 0 && z < first.Sum.Count)
            {
                first.Sum[z] += a;
                Console.WriteLine("Операция проведена успешно");
            }
            else
            {
                Console.WriteLine("Условия ввода не соблюдены");
            }
        }
        static public void MinusMoney(Client first, Double a, int z)
        {
            if (a > 0 && a < 10000 && a < first.Sum[z] && z < first.Sum.Count && z >= 0)
            {
                first.Sum[z] -= a;
                Console.WriteLine("Операция проведена успешно");
            }
            else
            {
                Console.WriteLine("Условия ввода не соблюдены");
            }
        }
        public void InputPass(Client first)
        {
            String MyPass = " ";
            while (Attempt != 3 || !MyPass.Equals(first.Pass))
            {
                Console.WriteLine("Введи пароль количество попыток {0} -", Attempt);
                MyPass = Console.ReadLine();
                Attempt++;
            }
            if (Attempt == 3)
            {
                Danger = true;
            }
            Attempt = 0;
        }
        static public void Show(Client first)
        {
            for (int i = 0; i < first.Number.Count; i++)
            {
                Console.WriteLine("Номер карты-{0}, Ваш баланс-{1}, Пароль-{2}", first.Number[i], first.Sum[i], first.Pass[i]);
            }
        }
        static public void ChangePass(Client first, int i)
        {
            String pattern = @"[0-9](\d{3})";
            String MyPass = null, SecondMyPass = null;
            do
            {
                Console.WriteLine("Введите пароль");
                MyPass = Console.ReadLine();
                if (!Regex.IsMatch(MyPass, pattern) || MyPass.Count() > 4)
                {
                    Console.WriteLine("В строке есть буквы");
                }
                else
                {
                    Console.WriteLine("Повторите пароль");
                    SecondMyPass = Console.ReadLine();
                    if (SecondMyPass == MyPass)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Пароли не соответствуют");
                    }
                }
            } while (true);
            first.Pass[i] = MyPass;
        }
    }
}
