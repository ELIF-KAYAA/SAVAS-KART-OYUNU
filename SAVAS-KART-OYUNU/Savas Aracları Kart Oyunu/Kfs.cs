using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savas_Aracları_Kart_Oyunu;  


namespace Savas_Aracları_Kart_Oyunu
{
    public class KFS : KaraAraci
    {
        public override string AltSinif => "KFS";
        public override int HavaVurusAvantaji => 20;
        public override int DenizVurusAvantaji => 10;
        public KFS() : base("Kara Füze Sistemi", 10, 10, 0) // Saldırı gücü 20, dayanıklılık 10, seviye puanı 0
        { }

        public override void KartPuaniGoster()
        {
            Console.WriteLine($"Kart: {Ad}, Sınıf: {Sinif}, Alt Sınıf: KFS, Dayanıklılık: {Dayaniklilik}, Seviye Puanı: {SeviyePuani}");
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
            int hasar = hedefArac.Sinif == "Hava" ? VurusGucu + HavaVurusAvantaji : VurusGucu;
            hedefArac.DurumGuncelle(hasar);

            Console.WriteLine($"{this.Ad}, {hedefArac.Ad} hedefine saldırdı. {hedefArac.Ad} dayanıklılığı {hedefArac.Dayaniklilik} kaldı.");
        }
    }

}
