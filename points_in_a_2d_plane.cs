using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace points_in_a_2d_plane
{
    class nokta
    {
        public double x;                                        //Koordinatları tutan nokta sınıfı
        public double y;
        public nokta(double x, double y)                        //Constructor
        {
            this.x = x;
            this.y = y;
        }
        public double UzaklikHesapla(nokta n1, nokta n2)        //Verilen iki noktanın uzaklığını hesaplayıp döndürür
        {
            return Math.Sqrt(Math.Pow((n2.x - n1.x), 2) + Math.Pow((n2.y - n1.y), 2));
        }

    }
    class Program
    {

        public static double GetRandomNumber(int minimum, int maximum)  //Rastgele double sayı döndüren metod
        {
            return random.NextDouble() + random.Next(minimum, maximum);
        }
        public static Random random = new Random();
        public static void Main(string[] args)
        {

            Console.WriteLine("Yüksekliği giriniz : ");          //İstenen nokta sayısı ve matriksin genişlik ve yüksekliğinin girdisi
            int heigth = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Genişliği giriniz : ");
            int width = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Oluşturmak istediğiniz nokta sayısı : ");
            int n = Int32.Parse(Console.ReadLine());
            nokta[] noktalar = new nokta[n];                     //Oluşturulan rastgele noktaların nokta nesnesi şeklinde tutulan array
            double[,] noktalarmatrisi = new double[n, 2];        //Noktaların x ve y koordinatlarını tutan matris
            for (int i = 0; i < n; i++)                          //Rastgele noktaların oluşturulduğu ve matrisin içine atanan döngü
            {
                double x = GetRandomNumber(0, width);
                double y = GetRandomNumber(0, heigth);
                nokta nokta = new nokta(x, y);
                noktalar[i] = nokta;
                noktalarmatrisi[i, 0] = nokta.x;
                noktalarmatrisi[i, 1] = nokta.y;

            }
            foreach (nokta a in noktalar)       //Her noktanın x v y koordinatlarının konsola yazıldığı döngü
            {
                Console.WriteLine(string.Format("X kordinatı :{0,6:0.00}    Y kordinatı :{0,6:0.00} ", a.x, a.y));
            }
            double[,] uzaklikmatris = new double[n, n];
            for (int i = 0; i < n; i++)         //Uzaklık matrisinin oluşturulduğu döngü
            {
                for (int j = 0; j < n; j++)
                {
                    uzaklikmatris[i, j] = noktalar[i].UzaklikHesapla(noktalar[i], noktalar[j]);
                }
            }
            Console.WriteLine("            Distance Matrix");        //Uzaklık matrisini konsola yazdırdığımız döngü
            Console.Write("       ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(string.Format("{0,7}", i));
            }
            Console.WriteLine("");
            for (int i = 0; i < n; i++)
            {
                Console.Write(string.Format("{0,7}", i));
                for (int j = 0; j < n; j++)
                {
                    Console.Write(string.Format("{0,7:0.00}", uzaklikmatris[i, j]));

                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }

    }
}

