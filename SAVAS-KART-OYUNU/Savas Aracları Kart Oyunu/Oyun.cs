using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Savas_Aracları_Kart_Oyunu
{
    public class Oyun
    {
        private readonly string logDosyaYolu = "OyunLog.txt";
       
        // oyuncu ve bilgisayarın sahip olduğu kartlar
        public List<Kart> OyuncuKartlari { get; private set; }
        public List<Kart> BilgisayarKartlari { get; private set; }
        public int OyuncuSkoru { get; private set; }
        public int BilgisayarSkoru { get; private set; }
        public int ToplamHamle { get; private set; }
        public int MaxHamle { get; private set; }
        public bool oyunBitti = false;
        // oyuncu ve ve ilgisayarın seçtiği kartlar
        private List<Kart> oyuncuSecilenKartlar;
        private List<Kart> bilgisayarSecilenKartlar;

        public Oyun(int maxHamle = 5)
        {
            MaxHamle = maxHamle;
            OyuncuSkoru = 0;
            BilgisayarSkoru = 0;
            ToplamHamle = 0;
            OyuncuKartlari = KartDagit(); // Oyuncu ve bilgisayar için 6'şar adet kart dağıtılıyor
            BilgisayarKartlari = KartDagit();
            oyuncuSecilenKartlar = new List<Kart>();
            bilgisayarSecilenKartlar = new List<Kart>();
            File.WriteAllText(logDosyaYolu, ""); // Log dosyasını sıfırla
        }

        private List<Kart> KartDagit()
        {
            List<Kart> kartlar = new List<Kart>();
            for (int i = 0; i < 6; i++) // 6 kart ekleniyor
            {
                kartlar.Add(new Siha());
                kartlar.Add(new KFS());
                kartlar.Add(new Sida());
                kartlar.Add(new Ucak());
                kartlar.Add(new Firkateyn());
                kartlar.Add(new Obus());
            }
            return kartlar;
        }


        public void OyunHamlesi()
        {
            if (oyunBitti) return; // Oyun bittiyse işlem yapılmaz

            ToplamHamle++;
            Console.WriteLine($"\n--- {ToplamHamle}. Hamle ---");
            // Oyuncu ve bilgisayar için rastgele kart seçimi yapılır 

            var oyuncuKartlar = KartSec(OyuncuKartlari, oyuncuSecilenKartlar);
            var bilgisayarKartlar = KartSec(BilgisayarKartlari, bilgisayarSecilenKartlar, true);
            
            // Kart karşılaştırmaları yapılır 

            if (bilgisayarKartlar.Count >= 3 && oyuncuKartlar.Count >= 3)
            {
                Karsilastir(oyuncuKartlar, bilgisayarKartlar);
            }
       
            else
            {
                oyunBitti = true;  // Kartlar tükenmişse oyun biter
            }



            HamleSonu();

            // Oyuncu ve bilgisayar kartlarını 3’e tamamlama
            if (!oyunBitti)
            {
                if (OyuncuKartlari.Count < 3) OyuncuKartlari.Add(RastgeleKartVer(OyuncuSkoru >= 20));
                if (BilgisayarKartlari.Count < 3) BilgisayarKartlari.Add(RastgeleKartVer(BilgisayarSkoru >= 20));
            }

            LogYaz();
        }
        // Rastgele kart verir, seviye 20 üzerindeyse ekstra kart ekler
        private Kart RastgeleKartVer(bool seviye20Ulasti)
        {
            var kartlar = new List<Kart> { new Ucak(), new Firkateyn(), new Obus() };
            if (seviye20Ulasti) kartlar.AddRange(new List<Kart> { new Siha(), new Sida(), new KFS() });
            Random rnd = new Random();
            return kartlar[rnd.Next(0, kartlar.Count)];
        }
        // Oyuncu veya bilgisayar için kart seçimi yapar
        private List<Kart> KartSec(List<Kart> tumKartlar, List<Kart> secilenKartlar, bool rastgele = false)
        {
            List<Kart> secim = new List<Kart>();

            if (secilenKartlar.Count == tumKartlar.Count)
            {
                secilenKartlar.Clear();
            }

            var mevcutKartlar = tumKartlar.Except(secilenKartlar).ToList();
            Random rnd = new Random();

            // rastgele 3 kart seç

            while (secim.Count < 3 && mevcutKartlar.Count > 0)
            {
                Kart kart = mevcutKartlar[rnd.Next(0, mevcutKartlar.Count)];
                if (!secilenKartlar.Contains(kart))
                {
                    secim.Add(kart);
                    secilenKartlar.Add(kart);
                }
            }

            return secilenKartlar;
        }
        // Kartları karşılaştırma işlemi
        private void Karsilastir(List<Kart> oyuncuKartlar, List<Kart> bilgisayarKartlar)
        {
            List<Kart> elenenOyuncuKartlar = new List<Kart>();
            List<Kart> elenenBilgisayarKartlar = new List<Kart>();

            // Her iki tarafın 3 kartını karşılaştır
            for (int i = 0; i < 3; i++)
            {
                var oyuncuKart = oyuncuKartlar[i];
                var bilgisayarKart = bilgisayarKartlar[i];
                Console.WriteLine($"Oyuncu kartı: {oyuncuKart.Ad} - Saldırı Gücü: {oyuncuKart.SaldiriGucu}");
                Console.WriteLine($"Bilgisayar kartı: {bilgisayarKart.Ad} - Saldırı Gücü: {bilgisayarKart.SaldiriGucu}");

                // Kartların dayanıklılığı kontrol edilir ve karşılaştırılır

                if (oyuncuKart.SaldiriGucu == bilgisayarKart.SaldiriGucu)
                {
                    oyuncuKart.Dayaniklilik -= 1;
                    bilgisayarKart.Dayaniklilik -= 1;
                    Console.WriteLine("Berabere! Kartların dayanıklılığı 1 azaltıldı.");

                    // Dayanıklılığı sıfır veya daha az olan kartları "elenen" listelerine ekle
                    if (oyuncuKart.Dayaniklilik <= 0) elenenOyuncuKartlar.Add(oyuncuKart);
                    if (bilgisayarKart.Dayaniklilik <= 0) elenenBilgisayarKartlar.Add(bilgisayarKart);
                }


                oyuncuKart.Saldir(bilgisayarKart);

                if (bilgisayarKart.Dayaniklilik <= 0)
                {

                    Console.WriteLine($"Bilgisayar Kart Elenen Kart: {bilgisayarKart.Ad} elendi !");
                    ElenmeIslemi(oyuncuKart, bilgisayarKart);
                    elenenBilgisayarKartlar.Add(bilgisayarKart);
                }

                bilgisayarKart.Saldir(oyuncuKart);

                if (oyuncuKart.Dayaniklilik <= 0)
                {

                    Console.WriteLine($"Oyuncu Kart Elenen Kart: {oyuncuKart.Ad} elendi !");
                    ElenmeIslemi(bilgisayarKart, oyuncuKart);
                    elenenOyuncuKartlar.Add(oyuncuKart);
                }

            }
            // Elenen oyuncu ve bilgisayar kartlarını oyuncu listesinden kaldır
            foreach (var kart in elenenOyuncuKartlar) OyuncuKartlari.Remove(kart);
            foreach (var kart in elenenBilgisayarKartlar) BilgisayarKartlari.Remove(kart);
        }

        private void ElenmeIslemi(Kart kazananKart, Kart elenenKart)
        {
            // Elenen kartın puanını belirliyoruz (minimum 10 puan)
            int kazancPuan = Math.Max(elenenKart.SeviyePuani, 10);

            // Kazanan karta bu puanı ekler

            kazananKart.SeviyePuani += kazancPuan;

            // Kazanan kart oyuncuya aitse, oyuncu skoruna puan eklenir
            //değilse bilgisayar skoruna puan eklenir

            if (OyuncuKartlari.Contains(kazananKart)) OyuncuSkoru += kazancPuan;
            else BilgisayarSkoru += kazancPuan;

            // Elenen kart oyuncuya aitse, oyuncunun kartlarından çıkarılır
            // değilse bilgisayarın kartlarından çıkarılır
            if (OyuncuKartlari.Contains(elenenKart)) OyuncuKartlari.Remove(elenenKart);
            else if (BilgisayarKartlari.Contains(elenenKart)) BilgisayarKartlari.Remove(elenenKart);
        }

        private void HamleSonu()
        {
            Console.WriteLine($"--- {ToplamHamle}. Hamle Sonu ---");
            Console.WriteLine($"Oyuncunun Skoru: {OyuncuSkoru}, Bilgisayarın Skoru: {BilgisayarSkoru}");
            Console.WriteLine($"Oyuncu Kartları: {string.Join(", ", OyuncuKartlari.Select(k => k.Ad))}");
            Console.WriteLine($"Bilgisayar Kartları: {string.Join(", ", BilgisayarKartlari.Select(k => k.Ad))}");
            // Oyunun bitiş şartlarını kontrol edilir
            // 1. Oyuncunun veya bilgisayarın kart sayısı 3'ten azsa
            // 2. Toplam hamle sayısı maksimum hamle sayısına ulaştıysa
            if (OyuncuKartlari.Count < 3 || BilgisayarKartlari.Count < 3 || ToplamHamle >= MaxHamle)
            {
                OyunSonu(); // Oyun bitiş kontrolü
                oyunBitti = true;
            }
        }

        public void OyunSonu()
        {
            if (oyunBitti) return;
            oyunBitti = true; // oyun bittiğini işaretle
            Console.WriteLine($"\nOyun sona erdi! Nihai Skor - Oyuncu: {OyuncuSkoru}, Bilgisayar: {BilgisayarSkoru}");

            string kazanan = ""; // Kazananı tutacak değişken

            if (OyuncuKartlari.Count == 0)
            {
                Console.WriteLine("Bilgisayar kazandı. Oyuncunun kartları tükendi!");
                kazanan = "Bilgisayar";
            }
            else if (BilgisayarKartlari.Count == 0)
            {
                Console.WriteLine("Oyuncu kazandı. Bilgisayarın kartları tükendi!");
                kazanan = "Oyuncu";
            }
            else
            {
                if (OyuncuSkoru > BilgisayarSkoru)
                {
                    Console.WriteLine("Oyuncu kazandı! Skoru daha yüksek.");
                    kazanan = "Oyuncu";
                }
                else if (BilgisayarSkoru > OyuncuSkoru)
                {
                    Console.WriteLine("Bilgisayar kazandı! Skoru daha yüksek.");
                    kazanan = "Bilgisayar";
                }
                else
                {
                    Console.WriteLine("Oyun berabere! Skorlar ve dayanıklılıklar eşit.");
                    kazanan = "Berabere";
                }
            }

            Console.WriteLine("Kalan Oyuncu Kartları: " + string.Join(", ", OyuncuKartlari.Select(k => k.Ad)));
            Console.WriteLine("Kalan Bilgisayar Kartları: " + string.Join(", ", BilgisayarKartlari.Select(k => k.Ad)));
      

            LogYaz(true);

            // Form üzerindeki kazananı göster
            SavasOyunlari form = Application.OpenForms["Form1"] as SavasOyunlari; // Açık olan Form1'i bul
            if (form != null)
            {
                form.Invoke((MethodInvoker)(() =>
                {
                    form.GosterKazanan(kazanan); // GosterKazanan metodunu çağır
                }));
            }
        }
    private void LogYaz(bool oyunSonu = false)
    {
           // Log dosyasına yazma işlemi için StreamWriter 
        using (StreamWriter writer = new StreamWriter(logDosyaYolu, true))
        {
            if (!oyunSonu)
            {
                writer.WriteLine($"--- {ToplamHamle}. Hamle Sonu ---");
                writer.WriteLine($"Oyuncunun Skoru: {OyuncuSkoru}, Bilgisayarın Skoru: {BilgisayarSkoru}");
                writer.WriteLine("Oyuncu Kartları: " + string.Join(", ", OyuncuKartlari.Select(k => k.Ad)));
                writer.WriteLine("Bilgisayar Kartları: " + string.Join(", ", BilgisayarKartlari.Select(k => k.Ad)));
            }
            else
            {
                writer.WriteLine("\nOyun sona erdi!");
                writer.WriteLine($"Nihai Skor - Oyuncu: {OyuncuSkoru}, Bilgisayar: {BilgisayarSkoru}");
                if (OyuncuKartlari.Count == 0)
                    writer.WriteLine("Bilgisayar kazandı. Oyuncunun kartları tükendi!");
                else if (BilgisayarKartlari.Count == 0)
                    writer.WriteLine("Oyuncu kazandı. Bilgisayarın kartları tükendi!");
                else
                {
                    if (OyuncuSkoru > BilgisayarSkoru)
                        writer.WriteLine("Oyuncu kazandı! Skoru daha yüksek.");
                    else if (BilgisayarSkoru > OyuncuSkoru)
                        writer.WriteLine("Bilgisayar kazandı! Skoru daha yüksek.");
                    else
                        writer.WriteLine("Oyun berabere! Skorlar ve dayanıklılıklar eşit.");
                }
                    // Oyuncu ve bilgisayarın kalan kartlarını log dosyasına yaz
                    writer.WriteLine("Kalan Oyuncu Kartları: " + string.Join(", ", OyuncuKartlari.Select(k => k.Ad)));
                writer.WriteLine("Kalan Bilgisayar Kartları: " + string.Join(", ", BilgisayarKartlari.Select(k => k.Ad)));
            }
        }
    }

        [STAThread]
        public static void Main(string[] args)
        {
          
                Application.EnableVisualStyles(); // Windows'un modern görsel stilleri
            Application.SetCompatibleTextRenderingDefault(false); //metin işleme motorunu kullanmayı ayarlar.
            Application.Run(new SavasOyunlari()); // Form1 yerine formun adı
            }
        }
    }

