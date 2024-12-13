using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Savas_Aracları_Kart_Oyunu.Kart;

namespace Savas_Aracları_Kart_Oyunu
{
    public class Ucak : HavaAraci
    {
        public override string AltSinif => "Ucak";
        public override int KaraVurusAvantaji => 10; // Kara araçlarına karşı avantaj
        public override int DenizVurusAvantaji => 0;

        public Ucak() : base("Uçak", 20, 20, 0) { }

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

            // Eğer hedef kara aracıysa, kara vuruş avantajını ekle
            if (hedefArac.Sinif == "Kara")
            {
                saldiriGucu += KaraVurusAvantaji;
            }

            hedefArac.DurumGuncelle(saldiriGucu);

            Console.WriteLine($"{Ad} saldırdı. {hedefArac.Ad} kalan dayanıklılığı: {hedefArac.Dayaniklilik}");

            // Hedef aracın dayanıklılığı sıfırın altına düşerse elendiğini belirt
            if (hedefArac.Dayaniklilik <= 0)
            {
                hedefArac.Dayaniklilik = 0;
                Console.WriteLine($"{hedefArac.Ad} elendi!");
            }
        }
    }
}
