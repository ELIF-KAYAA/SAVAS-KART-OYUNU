using System;

namespace Savas_Aracları_Kart_Oyunu
{
    public enum Sahip
    {
        Oyuncu,
        Bilgisayar
    }
    public class Kart
    {
        public string Ad { get; set; }
        public int SaldiriGucu { get; set; }
        public int Dayaniklilik { get; set; }
        public int SeviyePuani { get; set; }
        public Sahip Sahibi { get; set; }
        public string Sinif { get; set; }


        public Kart(string ad, int saldiriGucu, int dayaniklilik, int seviyePuani)
        {
            Ad = ad;
            SaldiriGucu = saldiriGucu;
            Dayaniklilik = dayaniklilik;
            SeviyePuani = seviyePuani;
            Sahibi = Sahibi ;
        }

        public virtual void KartPuaniGoster()
        {
            Console.WriteLine($"Kart: {Ad}, Sınıf: {Sinif}, Dayanıklılık: {Dayaniklilik}, Seviye Puanı: {SeviyePuani}");
        }

        public virtual void DurumGuncelle(int hasar)
        {
            Dayaniklilik -= hasar;
            if (Dayaniklilik < 0) Dayaniklilik = 0;
        }

        // Kartların nasıl saldıracağını belirten soyut metod
        public virtual void Saldir(Kart hedefKart)
        {
            hedefKart.Dayaniklilik -= SaldiriGucu;
        }
    }
}


