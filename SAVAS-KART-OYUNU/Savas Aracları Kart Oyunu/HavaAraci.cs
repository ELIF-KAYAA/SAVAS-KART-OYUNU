using System;

namespace Savas_Aracları_Kart_Oyunu
{

    public abstract class HavaAraci : SavasAraci
    {
        public abstract string AltSinif { get; }
        // public virtual int KaraVurusAvantaji { get; } = 10;

        protected HavaAraci(string ad, int vurusGucu, int dayaniklilik, int seviyePuani)
            : base(ad, vurusGucu, dayaniklilik, "Hava") { }

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

            // Eğer hedef kara aracıysa, kara vuruş avantajını ekle
            if (hedefArac.Sinif == "Kara")
            {
                saldiriGucu += KaraVurusAvantaji;
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
      