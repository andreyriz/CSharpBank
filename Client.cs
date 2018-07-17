using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDRUSCO
{
    [Serializable]
    public class Client
    {
        public List<UInt64> Number { set; get; }
        public List<String> Pass { set; get; }
        public List<Double> Sum { set; get; }
        public Client(String bpuss, Double csum, UInt64 xnumcard)
        {
            Sum = new List<Double>();
            Number = new List<UInt64>();
            Pass = new List<String>();
            Number.Add(xnumcard);
            Pass.Add(bpuss);
            Sum.Add(csum);
        }
        public Client()
        {
        }
        public void AddNewCard(UInt64 a,String b)
        {
            Console.WriteLine("Введите количество денег на счету");
            Double sum = Double.Parse(Console.ReadLine());
            if (sum > 0)
            {
                Pass.Add(b);
                Number.Add(a);
                Sum.Add(sum);
                Console.WriteLine("Данные вашей новой карты: pass-{0}, num-{1}, sum-{2}",Pass[Pass.Count-1],Number[Number.Count-1],Sum[Sum.Count-1]);
                Console.WriteLine("Операция проведена успешно");
            }
            else
            {
                Console.WriteLine("Убедитесь в правильности введенных данных и попробуйте еще раз");
            }
        }
    }
}
