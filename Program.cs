using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
namespace ANDRUSCO
{
    //написать приложение, имитирующее работу банкомата
    //Реализовать классы Banc, Client, Account.

    //Client:
    //номер карты
    //пароль
    //сумма на счету

    //Account:
    //методы для зачисления/растраты денег
    //метод проверки пароля при авторизации
    //метод для установки значения в поле "опасных" попыток авторизации.
    //После "правильной" авторизации значение обнуляется.
    //Вывод данных по аккаунту
    //метод для изменения пароля

    //Banc:
    //содержит список клиентов
    //метод для записи данных в файл
    //метод для считывания данных из файла
    //метод для генерирования данных о карте
    //метод для генерирования пароля

    //Изначально клиенту нужно открыть счёт в банке, получить номер счёта,
    //получить свой пароль, положить сумму на счёт.
    //1. Приложение предлагает ввести пароль предполагаемой кредитной карточки, даётся 3 попытки на правильный ввод пароля.Если попытки исчерпаны,
    //приложение выдаёт соответствующее сообщение и завершается.
    //2. При успешном вводе пароля выводится меню.
    //Пользователь может выбрать одно из нескольких действий:
    // -  вывод баланса на экран;
    // -  пополнение счёта;
    // -  снять деньги со счёта;
    // -  выход.
    //3. Если пользователь выбирает вывод баланса на экран, приложение отображает состояние предполагаемого счёта, после чего предлагает либо
    //вернуться в меню, либо совершить выход.
    //4. Если пользователь выбирает пополнение счёта,программа запрашивает сумму для пополнения и выполняет операцию, сопровождая её выводом
    //соответствующего комментария.Затем следует предложение вернуться в меню или выполнить выход.
    //5. Если пользователь выбирает снять деньг со счёта, программа запрашивает сумму.Если сумма превышает сумму счёта пользователя, программа выдаёт
    //сообщение и переводит пользователя в меню. Иначе - отображает сообщение о том, что сумма снята со счёта и уменьшает сумму счёта на указанную величину.

    //Все данные о клиенте хранить в файле.
    //Предусмотреть возможность авторизации нескольких клиентов.
    //Если были произведены попытки авторизации с тремя вводами неверного пароля - 
    //в файл записывать данные о попытке и при последующем обращении - выводить пользователю данной карты
    //информацию о попытках неавторизованного входа.
    [Serializable]
    class Program
    {
        delegate void mydel(int a);
        static void WhatAreYouNeed(int a)
        {
            ConsoleColor foreg = Console.ForegroundColor;
            Console.SetCursorPosition(1, 10);
            if (a == 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Показать активы");
            Console.SetCursorPosition(1, 11);
            if (a == 11)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Снять деньги");
            Console.SetCursorPosition(1, 12);
            if (a == 12)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Добавить деньги");
            Console.SetCursorPosition(1, 13);
            if (a == 13)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Сменить пароль");
            Console.SetCursorPosition(1, 14);
            if (a == 14)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.WriteLine("Добавиьт карту и деньги к карте");
            Console.SetCursorPosition(1, 15);
            if (a == 15)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Выйти");
            Console.ForegroundColor = foreg;
            Console.SetCursorPosition(1, a);
        }
        static void MenuText(int a)
        {
            ConsoleColor foreg = Console.ForegroundColor;
            Console.SetCursorPosition(1, 10);
            if (a == 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Войти");
            Console.SetCursorPosition(1, 11);
            if (a == 11)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Создать пользователя");
            Console.SetCursorPosition(1, 12);
            if (a == 12)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = foreg;
            }
            Console.Write("Выйти");
            Console.ForegroundColor = foreg;
            Console.SetCursorPosition(1, a);
        }
        static void Menu(mydel x, int first, int second)
        {
            ConsoleKeyInfo ItKey = new ConsoleKeyInfo();
            do
            {
                x(Console.CursorTop);
                ItKey = Console.ReadKey();
                switch (ItKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (Console.CursorTop >= first)
                            {
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, Console.CursorTop + 1);
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            if (Console.CursorTop <= second)
                            {
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        {

                        }
                        break;
                    default:
                        break;
                }
            } while (ItKey.Key != ConsoleKey.Enter);
        }
        static void Main(string[] args)
        {
            bool ext = false;
            Lazy<Bank> MyBank = new Lazy<Bank>();
            String Pass = null;
            UInt64 Num = 0;
            Double sum = 0;
            int counter = 0;
            List<mydel> del = new List<mydel>(2) { MenuText, WhatAreYouNeed };
            do
            {
                Console.CursorTop = 10;
                Menu(del[0], 12, 10);
                if (Console.CursorTop == 10)
                {
                    Console.Clear();
                    Console.WriteLine("Введите номер карты");
                    Num = UInt64.Parse(Console.ReadLine());
                    Console.WriteLine("Введите пароль");
                    Pass = Console.ReadLine();
                    Console.Clear();
                    if (Pass != null)
                    {
                        if (MyBank.Value.Test(Pass, Num) != null)
                        {
                            do
                            {
                                //Client OldClient = MyBank.Value.Test(Pass, Num);
                                Console.CursorTop = 10;
                                Menu(del[1], 15, 10);
                                if (Console.CursorTop == 10)//показать деньги
                                {
                                    Console.Clear();
                                    Console.SetCursorPosition(10, 10);
                                    Account.Show(MyBank.Value.Test(Pass, Num));
                                    Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                if (Console.CursorTop == 11)//снять 
                                {
                                    Console.Clear();
                                    sum = 0;
                                    Console.WriteLine("Введите сколько снять");
                                    sum = Double.Parse(Console.ReadLine());
                                    if (MyBank.Value.Test(Pass, Num).Sum.Count > 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("С какой карочки");
                                        counter = int.Parse(Console.ReadLine());
                                    }
                                    Account.MinusMoney(MyBank.Value.Test(Pass, Num), sum, counter);
                                    Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                if (Console.CursorTop == 12)//добавить
                                {
                                    Console.Clear();
                                    sum = 0;
                                    Console.WriteLine("Введите сколько добавить");
                                    sum = Double.Parse(Console.ReadLine());
                                    if (MyBank.Value.Test(Pass, Num).Sum.Count > 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("С какой карточки");
                                        counter = int.Parse(Console.ReadLine());
                                    }
                                    Account.PlusMoney(MyBank.Value.Test(Pass, Num), sum, counter);
                                    Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                if (Console.CursorTop == 14)//добавить карты и деньги на карты
                                {
                                    Console.SetCursorPosition(0, 10);
                                    Console.Clear();
                                    Console.WriteLine("введите сколько денег кладете на карту");
                                    MyBank.Value.Test(Pass, Num).AddNewCard(MyBank.Value.getNum(), MyBank.Value.getPuss());
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                if (Console.CursorTop == 13)//Изменить пароль
                                {
                                    Console.Clear();
                                    Account.ChangePass(MyBank.Value.Test(Pass, Num),counter);
                                    Console.Clear();
                                }
                                else
                                if (Console.CursorTop == 15)//выйти
                                {
                                    Console.SetCursorPosition(0, 10);
                                    Console.Clear();
                                    Console.CursorTop = 10;
                                    ext = true;
                                }
                            } while (!ext);
                            ext = false;
                        }
                        else
                        {
                            Console.WriteLine("Увы но такой карты мы не можем найти ;(");
                            Console.ReadKey();
                            Console.Clear();
                            Console.CursorTop = 10;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы не ввели данные");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                if (Console.CursorTop == 11)//создать пользователя
                {
                    Console.Clear();
                    MyBank.Value.AddClient(new Client(MyBank.Value.getPuss(), 0, MyBank.Value.getNum()));
                    Console.ReadKey();
                    Account.Show(MyBank.Value.GetMyList(MyBank.Value.getCountMyList() - 1));//лечится выстрелом из ружья
                    Console.Clear();
                }
                else
                if (Console.CursorTop == 12)
                {
                    Console.Clear();
                    ext = true;
                }
            } while (!ext);
            MyBank.Value.InputFile();
        }
    }
}
