using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savas_Aracları_Kart_Oyunu
{
    public class Firkateyn : DenizAraci
    {
        public Firkateyn() : base("Firkateyn", 10,25, 0) { }

        public override string AltSinif => "Firkateyn";
        // Hava araçlarına karşı ek vuruş avantajı
        public override int HavaVurusAvantaji => 5; // Firkateyn'in hava araçlarına karşı avantajı

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
        public override void Saldir(Kart hedefKart)  // SavasAraci ya da Kart
        {
            // Öncelikle üst sınıfın saldırı mantığını uygulayalım
            base.Saldir(hedefKart);

            // Ekstra olarak durumu loglayalım
            Console.WriteLine($"{Ad}, {hedefKart.Ad} hedefine saldırıyor.");

            // Eğer hedef kart hava aracıysa, vuruş avantajı uygulandı
            if (hedefKart.Sinif == "Hava")
            {
                hedefKart.Dayaniklilik -= HavaVurusAvantaji; // Hava aracına ekstra hasar veriyoruz
                Console.WriteLine($"{Ad}, hava aracına karşı avantajlı vuruş yaptı! Dayanıklılık: {hedefKart.Dayaniklilik}");
            }
            // Dayanıklılık 0'ın altına düşmemeli, bu kontrolü sağlayalım
            if (hedefKart.Dayaniklilik < 0)
            {
                hedefKart.Dayaniklilik = 0;
            }
            // Son durumda hedefin dayanıklılığı bilgisi
            Console.WriteLine($"{hedefKart.Ad} hedefinin kalan dayanıklılığı: {hedefKart.Dayaniklilik}");
        }
    }
}
