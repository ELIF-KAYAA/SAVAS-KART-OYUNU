using System;

namespace Savas_Aracları_Kart_Oyunu
{
    public class Sida : DenizAraci
    {
        public override int HavaVurusAvantaji => 10;
        public override int KaraVurusAvantaji => 10;

        public override string AltSinif => "Sida";
        public Sida() : base("SİDA", 10, 25, 0) { }
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
            int saldiriGucu = VurusGucu;

            if (hedefArac.Sinif == "Hava")
            {
                saldiriGucu += HavaVurusAvantaji;
            }
            else if (hedefArac.Sinif == "Kara")
            {
                saldiriGucu += KaraVurusAvantaji;
            }

            hedefArac.DurumGuncelle(saldiriGucu);
            Console.WriteLine($"{Ad} {hedefArac.Ad} hedefine saldırdı. {hedefArac.Ad} kalan dayanıklılığı: {hedefArac.Dayaniklilik}");

            if (hedefArac.Dayaniklilik <= 0)
            {
                hedefArac.Dayaniklilik = 0;
                Console.WriteLine($"{hedefArac.Ad} elendi!");
            }
        }
    }
}

