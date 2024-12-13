using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas_Aracları_Kart_Oyunu
{

    public abstract class DenizAraci : SavasAraci
    {
        public override int HavaVurusAvantaji { get; } = 20;
        public override int KaraVurusAvantaji { get; } = 0;
        public abstract string AltSinif { get; }

        public DenizAraci(string ad, int vurusGucu, int dayaniklilik, int seviyePuani)
            : base(ad, vurusGucu, dayaniklilik, "Deniz") { }

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

            // Eğer hedef hava aracıysa, hava vuruş avantajını ekle
            if (hedefArac.Sinif == "Hava")
            {
                saldiriGucu += HavaVurusAvantaji;
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





