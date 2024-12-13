using System;

namespace Savas_Aracları_Kart_Oyunu
{
    public abstract class KaraAraci : SavasAraci
    {
        public abstract string AltSinif { get; }
        public override int DenizVurusAvantaji { get; } = 15;
        public override int HavaVurusAvantaji { get; }

        public KaraAraci(string ad, int vurusGucu, int dayaniklilik, int seviyePuani)
            : base(ad, vurusGucu, dayaniklilik, "Kara") { }

        public override void KartPuaniGoster()
        {
            Console.WriteLine($"Kart: {Ad}, Sınıf: {Sinif}, Alt Sınıf: {AltSinif}, Dayanıklılık: {Dayaniklilik}, Seviye Puanı: {SeviyePuani}");
        }

        public override void DurumGuncelle(int hasar)
        {
            Dayaniklilik -= hasar;
            if (Dayaniklilik < 0)
            {
                Dayaniklilik = 0;
            }
        }

        public override void Saldir(Kart hedefArac)
        {
            int saldiriGucu = SaldiriGucu;

            // Eğer hedef deniz aracıysa, deniz vuruş avantajını ekle
            if (hedefArac.Sinif == "Deniz")
            {
                saldiriGucu += DenizVurusAvantaji;
            }

            hedefArac.DurumGuncelle(saldiriGucu);
            Console.WriteLine($"{Ad} {hedefArac.Ad} hedefine saldırdı. Kalan dayanıklılık: {hedefArac.Dayaniklilik}");

            if (hedefArac.Dayaniklilik <= 0)
            {
                Console.WriteLine($"{hedefArac.Ad} elendi!");
            }
        }
    }
}

        