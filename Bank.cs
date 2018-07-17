using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ANDRUSCO
{
    //содержит список клиентов
    //метод для записи данных в файл
    //метод для считывания данных из файла
    //метод для генерирования данных о карте
    //метод для генерирования пароля
    public class Bank
    {
        static private List<Client> MyList;
        public Bank()
        {
            OutputFile();
        }
        public void InputFile()
        {
            String way = Environment.CurrentDirectory;
            way += "/result.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(List<Client>));
            using (Stream stream = new FileStream(way, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, MyList);
            }
        }
        public void OutputFile()
        {
            String way = Environment.CurrentDirectory;
            way += "/result.xml";
            FileInfo inf = new FileInfo(way);
            if (inf.Exists)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Client>));
                using (Stream stream = new FileStream(way, FileMode.Open))
                {
                    MyList=((List<Client>)formatter.Deserialize(stream));
                }
            }
            else
            {
                Console.WriteLine("Файл не был открыт или он не был найден обратитесь к производителю");
            }
        }
        public UInt64 getNum()
        {
            Random x = new Random();
            UInt64 buf = (UInt64)x.Next(100000000, 999999999);
            for (int i = 0; i < MyList.Count; i++)
            {
                for (int j = 0; j < MyList[i].Number.Count; j++)
                {
                    if (buf == MyList[i].Number[j])
                    {
                        i = 0;
                        j = 0;
                        buf = (UInt64)x.Next(100000000, 999999999);
                    }
                }
            }
            Console.WriteLine("Номер вашей карты {0}", buf);
            return buf;
        }
        public String getPuss()
        {
            Random x = new Random();
            String buf = x.Next(1000, 9999).ToString();
            Console.WriteLine("Ваш пароль для этой карты {0}", buf);
            return buf;
        }
        public Client Test(String a, UInt64 b)
        {
            //Console.WriteLine("My pass-{0},My num-{1}", a,b);
            for (int i = 0; i < MyList.Count; i++)
            {
                for (int j = 0; j < MyList[i].Number.Count; j++)
                {
                    //Console.WriteLine("pass-{0}, num-{1}, sum-{2}", MyList[i].Pass, MyList[i].Number[j], MyList[i].Sum[j]);
                    if (MyList[i].Number[j] == b && MyList[i].Pass[j] == a)
                    {
                        return MyList[i];
                    }
                }
            }
            return null;
        }
        public void AddClient(Client a)
        {
            MyList.Add(a);
        }
        public Client GetMyList(int a)
        {
            if (a<MyList.Count)
            {
                return MyList[a];
            }
            else
            {
                return null;
            }
        }
        public int getCountMyList()
        {
            return MyList.Count;
        }
    }

}
