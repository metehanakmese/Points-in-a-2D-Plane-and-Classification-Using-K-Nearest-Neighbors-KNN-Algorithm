using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classification_using_k_nearest_neighbors_knn_algorithm
{
    class Banknot
    {
        public double VaryansDegeri;            //Banknotun özelliklerini tutan sınıf
        public double CarpiklikDegeri;
        public double BasiklikDegeri;
        public double EntropiDegeri;
        public int Tur;
        public double Mesafe;
        public Banknot(double VaryansDegeri, double CarpiklikDegeri, double BasiklikDegeri, double EntropiDegeri, int Tur, double Mesafe)
        {
            this.VaryansDegeri = VaryansDegeri;
            this.CarpiklikDegeri = CarpiklikDegeri;
            this.BasiklikDegeri = BasiklikDegeri;       //Constructor
            this.EntropiDegeri = EntropiDegeri;
            this.Tur = Tur;
            this.Mesafe = Mesafe;
        }
        public Banknot(double VaryansDegeri, double CarpiklikDegeri, double BasiklikDegeri, double EntropiDegeri, int Tur)
        {
            this.VaryansDegeri = VaryansDegeri;
            this.CarpiklikDegeri = CarpiklikDegeri;
            this.BasiklikDegeri = BasiklikDegeri;       //Constructor
            this.EntropiDegeri = EntropiDegeri;
            this.Tur = Tur;
        }
        public Banknot(double VaryansDegeri, double CarpiklikDegeri, double BasiklikDegeri, double EntropiDegeri)
        {
            this.VaryansDegeri = VaryansDegeri;
            this.CarpiklikDegeri = CarpiklikDegeri;
            this.BasiklikDegeri = BasiklikDegeri;       //Constructor
            this.EntropiDegeri = EntropiDegeri;
        }
        public void SetMesafe(double m)
        {
            this.Mesafe = m;
        }
        public static double MesafeHesapla(Banknot b1, Banknot b2)          //İki banknot arasındaki benzerliği hesaplayan formül
        {
            return Math.Sqrt(Math.Pow((b2.VaryansDegeri - b1.VaryansDegeri), 2) + Math.Pow((b2.CarpiklikDegeri - b1.CarpiklikDegeri), 2) + Math.Pow((b2.BasiklikDegeri - b1.BasiklikDegeri), 2) + Math.Pow((b2.EntropiDegeri - b1.EntropiDegeri), 2));
        }
    }
    class Program
    {
        static string textFile = @"C:\Users\yoclo\source\repos\Proje2\Proje2\bin\Debug\data_banknote_authentication.txt";
        public static int ToplamSayiBul(string s)       //Örnek olarak verilen banknot textinin satır sayısı için (banknot adeti)
        {
            int deger = 0;
            if (File.Exists(s))
            {
                string[] lines = File.ReadAllLines(s);
                foreach (string line in lines)
                {
                    deger++;
                }
            }
            return deger;
        }
        public static Banknot[] DosyaOku(Banknot[] banknotDizisi)  //Text içindeki banknotları, Banknot nesnesi halinde tutan arrayi
        {                                                          //oluşturan metot
            int sayac = 0;
            if (File.Exists(textFile))
            {
                string[] lines = File.ReadAllLines(textFile);
                foreach (string line in lines)
                {
                    string[] nitelikler = line.Split(',');
                    Banknot banknot = new Banknot(DoubleCevir(nitelikler[0]), DoubleCevir(nitelikler[1]), DoubleCevir(nitelikler[2]), DoubleCevir(nitelikler[3]), Convert.ToInt32(nitelikler[4]));
                    banknotDizisi[sayac] = banknot;
                    sayac++;
                }
            }
            return banknotDizisi;
        }
        public static Banknot[] BelirliBirDegereGoreMesafeBulma(Banknot b1, Banknot[] banknotDizisi)
        {
            for (int i = 0; i < banknotDizisi.Length; i++)        //Özellikleri girilen banknotun, text içindeki banknotlarla olan benzerliğini
            {                                                     //kaydeden metot
                banknotDizisi[i].SetMesafe(Banknot.MesafeHesapla(b1, banknotDizisi[i]));
            }
            return banknotDizisi;
        }
        public static Banknot[] Sıralama(Banknot[] banknotDizisi)    //Arrayi benzerliğe göre sıraladığımız metot
        {
            int x = 0;
            while (x < banknotDizisi.Length)
            {
                int y = 0;
                while (y < banknotDizisi.Length)
                {
                    if (banknotDizisi[x].Mesafe < banknotDizisi[y].Mesafe)
                    {
                        Banknot temp = new Banknot(banknotDizisi[x].VaryansDegeri, banknotDizisi[x].CarpiklikDegeri, banknotDizisi[x].BasiklikDegeri, banknotDizisi[x].EntropiDegeri, banknotDizisi[x].Tur, banknotDizisi[x].Mesafe);
                        banknotDizisi[x] = banknotDizisi[y];
                        banknotDizisi[y] = temp;
                    }
                    y++;
                }
                x++;
            }
            return banknotDizisi;
        }
        public static int GercekMiSahteMi(Banknot[] b, int k, int gercekmiSahtemi)     //Bir banknotun, banknot arrayine kıyasla gerçek veya
        {                                                                             //sahte olduğunu tahminlediğimiz metot
            int gercekSayisi = 0;
            int sahteSayisi = 0;

            for (int i = 0; i < k; i++)
            {
                if (b[i].Tur == 1)
                {
                    gercekSayisi++;
                }
                else
                {
                    sahteSayisi++;
                }
            }
            if (gercekSayisi > sahteSayisi)
            {
                Console.WriteLine("Girdiğiniz para gerçek");
                gercekmiSahtemi = 1;
            }
            else if (sahteSayisi > gercekSayisi)
            {
                Console.WriteLine("Girdiğiniz para sahte");
                gercekmiSahtemi = 0;

            }
            else
            {
                if (b[0].Tur == 1)
                {
                    Console.WriteLine("Girdiğiniz para gerçek");
                    gercekmiSahtemi = 1;

                }
                else
                {
                    Console.WriteLine("Girdiğiniz para sahte");
                    gercekmiSahtemi = 0;

                }
            }
            return gercekmiSahtemi;
        }
        public static int IndexBulma(Banknot[] b)
        {
            int index = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i].Tur == 1)
                {
                    break;
                }
                index++;
            }
            return index;
        }
        public static Banknot[] TestVerisiOlusturma(Banknot[] b, int index, int k)      //Test verisini oluşturan metot
        {
            int sayac = index - k;
            Banknot[] testVerisi = new Banknot[k];
            for (int i = 0; i < k; i++)
            {
                testVerisi[i] = b[sayac];
                sayac++;
            }
            return testVerisi;
        }
        public static Banknot[] VeriSetiOlusturma(Banknot[] b, int k, int index)      //Verisetini oluşturan metot
        {
            int sayac = 0;
            Banknot[] VeriSeti = new Banknot[b.Length - (2 * k)];
            for (int i = 0; i < b.Length; i++)
            {
                if (i >= index - k && i < index)
                {
                    continue;
                }
                if (i >= b.Length - k && i < b.Length)
                {
                    continue;
                }
                VeriSeti[sayac] = b[i];
                sayac++;
            }
            return VeriSeti;
        }
        public static int BasariOlcumu(Banknot[] VeriSeti, Banknot[] OlcumVerisi, int k1, int dogrulukSayisi)
        {                              //Test verisindeki her banknotu tahminleyen ve başarı ölçümünü hesaplayan metot
            for (int i = 0; i < OlcumVerisi.Length; i++)
            {
                BelirliBirDegereGoreMesafeBulma(OlcumVerisi[i], VeriSeti);
                Sıralama(VeriSeti);
                Yazdir(VeriSeti, k1);
                int gercekmiSahtemi = 0;
                gercekmiSahtemi = GercekMiSahteMi(VeriSeti, k1, gercekmiSahtemi);
                if (gercekmiSahtemi == OlcumVerisi[i].Tur)
                {
                    dogrulukSayisi++;
                }
            }
            return dogrulukSayisi;
        }
        public static void Yazdir(Banknot[] b, int k)  //Konsola yazdırma metodu
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("****************************************************************************************************************");
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine("Varyans Değeri       Çarpıklık Degeri       Basıklık Değeri       Entropi Değeri       Tur       Mesafesi");
                Console.WriteLine(string.Format("{0,14}       {1,16}       {2,15}       {3,14}       {4,3}       {5,8:0.00}", b[i].VaryansDegeri, b[i].CarpiklikDegeri, b[i].BasiklikDegeri, b[i].EntropiDegeri, b[i].Tur, b[i].Mesafe));
                Console.WriteLine("****************************************************************************************************************");
            }
        }
        public static double DoubleCevir(string s)      //Convert.ToDouble Türkçe bilgisayarda çevirmediği için yazdığımız metot
        {

            for (int i = 0; i < s.Length; i++)
            {
                if (s.ElementAt(i).Equals('.'))
                {
                    s = s.Replace('.', ',');
                    break;
                }
            }

            return Convert.ToDouble(s);

        }
        static void Main(string[] args)
        {

            int ToplamElemanSayisi = ToplamSayiBul(textFile);
            Banknot[] OrnekBanknotlar = new Banknot[ToplamElemanSayisi];
            DosyaOku(OrnekBanknotlar);                  //Text içindeki Banknotlar
            int index = IndexBulma(OrnekBanknotlar);
            Banknot[] SifirTestiVerisi = TestVerisiOlusturma(OrnekBanknotlar, index, 100);                      //Test verisi arrayi
            Banknot[] BirTestiVerisi = TestVerisiOlusturma(OrnekBanknotlar, OrnekBanknotlar.Length, 100);       //Test verisi arrayi
            Banknot[] VeriSeti = VeriSetiOlusturma(OrnekBanknotlar, 100, index);                                //Veriseti arrayi
            Console.WriteLine("Gerçekliğini Kontrol Etmek İstediğiniz Paranın; ");
            Console.WriteLine("Varyans Değerini Giriniz : ");
            double varyansDegeri = DoubleCevir(Console.ReadLine());
            Console.WriteLine("Çarpıklık Değerini Giriniz : ");                 //Türü belli olmayan banknotun özelliklerinin girdisi
            double carpiklikDegeri = DoubleCevir(Console.ReadLine());
            Console.WriteLine("Basıklık Değerini Giriniz : ");
            double basiklikDegeri = DoubleCevir(Console.ReadLine());
            Console.WriteLine("Entropi Değerini Giriniz : ");
            double entropiDegeri = DoubleCevir(Console.ReadLine());
            Banknot bilinmeyenBanknot = new Banknot(varyansDegeri, carpiklikDegeri, basiklikDegeri, entropiDegeri);
            BelirliBirDegereGoreMesafeBulma(bilinmeyenBanknot, OrnekBanknotlar);
            Sıralama(OrnekBanknotlar);
            Console.WriteLine("Kıyaslamak istediğiniz miktarı giriniz : ");
            int k = Int32.Parse(Console.ReadLine());
            Yazdir(OrnekBanknotlar, k);
            int gercekmiSahtemi2 = 0;
            GercekMiSahteMi(OrnekBanknotlar, k, gercekmiSahtemi2);
            Console.WriteLine("Test Verisini denemek için k sınırını belirleyiniz :");
            int k1 = Int32.Parse(Console.ReadLine());
            int dogrulukSayisi = 0;
            dogrulukSayisi = BasariOlcumu(VeriSeti, SifirTestiVerisi, k1, dogrulukSayisi);
            dogrulukSayisi = BasariOlcumu(VeriSeti, BirTestiVerisi, k1, dogrulukSayisi);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(string.Format("Başarı Oranı: {0,0:0.00}", dogrulukSayisi * 100 / 200));
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("****************************************************************************************************************");
            for (int i = 0; i < VeriSeti.Length; i++)
            {
                Console.WriteLine("Varyans Değeri       Çarpıklık Degeri       Çarpıklık Değeri       Entropi Değeri       Tur       Mesafesi");
                Console.WriteLine(string.Format("{0,14}       {1,16}       {2,15}       {3,14}       {4,3}       {5,8:0.00}", VeriSeti[i].VaryansDegeri, VeriSeti[i].CarpiklikDegeri, VeriSeti[i].BasiklikDegeri, VeriSeti[i].EntropiDegeri, VeriSeti[i].Tur, VeriSeti[i].Mesafe));
                Console.WriteLine("****************************************************************************************************************");
            }
            Console.ReadKey();
        }
    }
}


