using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas_Aracları_Kart_Oyunu
{
    public class Obus : KaraAraci
    {
        public Obus() : base("Obus", 10, 20, 0) { }

        public override string AltSinif => "Obus";

        public override int DenizVurusAvantaji => 5;  // Deniz araçlarına karşı +5 avantaj
        public override int HavaVurusAvantaji => 0;
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
            int hasar = hedefArac.Sinif == "Deniz" ? VurusGucu + DenizVurusAvantaji : VurusGucu;
            hedefArac.DurumGuncelle(hasar);
            Console.WriteLine($"{Ad}, {hedefArac.Ad} hedefine saldırdı. {hedefArac.Ad} dayanıklılığı {hedefArac.Dayaniklilik} kaldı.");
        }
    }
}


