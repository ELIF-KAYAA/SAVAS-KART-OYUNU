using System;


namespace Savas_Aracları_Kart_Oyunu
{
    public abstract class SavasAraci : Kart
    {
        //public string Ad { get; set; }
        //public int Dayaniklilik { get; set; }
        public int VurusGucu { get; set; }
        //public int SeviyePuani { get; protected set; } // protected set: SeviyePuani değerini yalnızca SavasAraci sınıfı ve ondan türeyen sınıfların değiştirebilir.
        //public string Sinif { get; set; }

        public SavasAraci(string ad, int vurusGucu, int dayaniklilik, string sinif)
             : base(ad, vurusGucu, dayaniklilik, 0)
        {
            this.Ad = ad;
            this.VurusGucu = vurusGucu;
            this.Dayaniklilik = dayaniklilik;
            this.Sinif = sinif;
            this.SeviyePuani = 0;  // Başlangıç seviyesi puanı

        }
        public virtual int KaraVurusAvantaji => 0;
        public virtual int DenizVurusAvantaji => 0;
        public virtual int HavaVurusAvantaji => 0;
        public override void KartPuaniGoster()
        {
            Console.WriteLine($"Kart: {Ad}, Sınıf: {Sinif}, Dayanıklılık: {Dayaniklilik}, Seviye Puanı: {SeviyePuani}");
        }

        public override void Saldir(Kart hedefArac)
        {
            int saldiriGucu = VurusGucu;

            // Kara araçlarına saldırıda avantaj ekle
            if (this is HavaAraci && hedefArac.Sinif == "Kara")
            {
                saldiriGucu += KaraVurusAvantaji;
            }
            // Hava araçlarına saldırıda avantaj ekle
            else if (this is DenizAraci && hedefArac.Sinif == "Hava")
            {
                saldiriGucu += HavaVurusAvantaji;
            }
            // Deniz araçlarına saldırıda avantaj ekle
            else if (this is KaraAraci && hedefArac.Sinif == "Deniz")
            {
                saldiriGucu += DenizVurusAvantaji;
            }

            hedefArac.DurumGuncelle(saldiriGucu);
            Console.WriteLine($"{Ad} saldırdı. {hedefArac.Ad} kalan dayanıklılığı: {hedefArac.Dayaniklilik}");

            if (hedefArac.Dayaniklilik <= 0)
            {
                Console.WriteLine($"{hedefArac.Ad} elendi!");
            }
        }
    }
}

