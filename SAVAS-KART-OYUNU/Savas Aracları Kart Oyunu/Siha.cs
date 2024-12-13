using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Savas_Aracları_Kart_Oyunu;


namespace Savas_Aracları_Kart_Oyunu
{
    public class Siha : HavaAraci
    {
        public Siha() : base("SİHA", 10, 15, 0)
        { }
      
        public override int KaraVurusAvantaji => 10;
        public override int DenizVurusAvantaji => 15;

       
        public override string AltSinif => "Siha";

        public override void KartPuaniGoster()
        {
            Console.WriteLine($"Kart: {Ad}, Sınıf: {Sinif}, Alt Sınıf: SİHA, Dayanıklılık: {Dayaniklilik}, Seviye Puanı: {SeviyePuani}");
        }
        public override void DurumGuncelle(int hasar)
        {
            Dayaniklilik -= hasar;
            if (Dayaniklilik < 0)
            {
                Dayaniklilik = 0;
            }
        }
        // Saldırma metodu
        public override void Saldir(Kart hedefArac)
        {

            int saldiriGucu = VurusGucu;
       
            if (hedefArac.Sinif == "Kara")
            {
                saldiriGucu += KaraVurusAvantaji;
            }
            else if (hedefArac.Sinif == "Deniz")
            {
                saldiriGucu += DenizVurusAvantaji;
            }
            hedefArac.DurumGuncelle(saldiriGucu);

            Console.WriteLine($"{this.Ad} saldırdı. {hedefArac.Ad} kalan dayanıklılığı: {hedefArac.Dayaniklilik}");

            if (hedefArac.Dayaniklilik <= 0)
            {
                hedefArac.Dayaniklilik = 0; // Dayanıklılık sıfırın altına inmemeli
                Console.WriteLine($"{hedefArac.Ad} elendi!");
            }
        }

    }
    }
