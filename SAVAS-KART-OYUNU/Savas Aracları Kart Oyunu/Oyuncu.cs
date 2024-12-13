using System;
using System.Collections.Generic;

    namespace Savas_Aracları_Kart_Oyunu
    {
        public class Oyuncu
        {
            public int OyuncuID { get; set; }
            public string OyuncuAdi { get; set; }
            public int Skor { get; set; }
            public List<Kart> KartListesi { get; set; }

            // Parametresiz yapıcı metot
            public Oyuncu()
            {
                KartListesi = new List<Kart>();
                Skor = 0; // Başlangıç skoru
            }

            // Parametreli yapıcı metot
            public Oyuncu(int oyuncuID, string oyuncuAdi, int skor = 0)
            {
                OyuncuID = oyuncuID;
                OyuncuAdi = oyuncuAdi;
                Skor = skor;
                KartListesi = new List<Kart>();
            }

            // Skoru gösteren fonksiyon
            public void SkorGoster()
            {
                Console.WriteLine($"{OyuncuAdi} Skor: {Skor}");
            }

            // Kart seçme fonksiyonu 
            public virtual Kart KartSec()
            {
                // Temel sınıfta boş bırakılıyor, alt sınıflarda özelleştirilecek
                return null;
            }
        }

        // Bilgisayar oyuncusu için türetilmiş sınıf
        public class BilgisayarOyuncu : Oyuncu
        {
            private Random random = new Random();

            public BilgisayarOyuncu(int oyuncuID, int skor = 0)
                : base(oyuncuID, "Bilgisayar", skor) { }

            public override Kart KartSec()
            {
                if (KartListesi.Count > 0)
                {
                // Rastgele bir kart seçmek için bir indeks oluştur
                int index = random.Next(KartListesi.Count);
                    return KartListesi[index]; // Rastgele seçilen kartı döndür
            }
                return null; // Eğer kart listesi boşsa, null döndür
        }
        }

        // Kullanıcı oyuncusu
        public class KullaniciOyuncu : Oyuncu
        {
       // oyuncu bilgilerini alır
            public KullaniciOyuncu(int oyuncuID, string oyuncuAdi, int skor = 0)
                : base(oyuncuID, oyuncuAdi, skor) { }

        // Kullanıcının kart seçme işlemi için
        public override Kart KartSec()
            {
                Console.WriteLine("Kartlar:");
                for (int i = 0; i < KartListesi.Count; i++) // KartListesi'ndeki her kart sırasıyla listelenir
            {
                    Console.WriteLine($"{i + 1}. {KartListesi[i].Ad}");  // Kartlar 1'den başlayarak numaralandırılıyor
            }
            // Kullanıcıdan bir seçim yapması isteniyor
            Console.Write("Bir kart seçin (1, 2, ...): ");
                int secim = int.Parse(Console.ReadLine());
                return KartListesi[secim - 1]; // Kullanıcının seçtiği kart döndürülüyor
        }
        }
    }
