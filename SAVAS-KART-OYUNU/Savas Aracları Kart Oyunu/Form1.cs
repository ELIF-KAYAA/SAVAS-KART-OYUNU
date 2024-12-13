using System;
using System.Drawing;
using System.Windows.Forms;

namespace Savas_Aracları_Kart_Oyunu
{
    public partial class SavasOyunlari : Form
    {
        // Kullanıcı ve bilgisayar skorlarını ve tıklama sayaçlarını tutan değişkenler
        private int yeniOyunClickCount = 0;
        private int kartSecClickCount = 0;
        private int hamleYapClickCount = 0;
        private int oyuncuSkor = 0;
        private int bilgisayarSkor = 0;
        private Oyun oyun; // Oyun sınıfı nesnesi

        public SavasOyunlari()
        {
            InitializeComponent();
            oyun = new Oyun(); // Yeni bir oyun başlatılıyor

            this.DoubleBuffered = true; // Form için şeffaflık desteği
            GizleEfektler(); // Efektleri başlangıçta görünmez yapar
        }

        #region Efekt Fonksiyonları
        private void GizleEfektler()
        {
            // Efektlerin görünürlüğünü kapatır
            efekt.Visible = false;
            efekt1.Visible = false;
            efekt2.Visible = false;
            efekt3.Visible = false;
            efekt4.Visible = false;
        }

        private void GosterEfektler()
        {
            // Efektlerin görünürlüğünü açar
            efekt.Visible = true;
            efekt1.Visible = true;
            efekt2.Visible = true;
            efekt3.Visible = true;
            efekt4.Visible = true;
        }
        #endregion

        #region Kart Seçme İşlemleri
        // Bilgisayar ve oyuncu kartlarına tıklama işlevleri
        private void pbObusBilgisayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgisayarın Obüs kartı seçildi!");
        }

        private void pbSihaBilgisayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgisayarın Siha kartı seçildi!");
        }

        private void pbSidaBilgisayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgisayarın Sida kartı seçildi!");
        }

        private void pbKfsBilgisayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgisayarın Kfs kartı seçildi!");
        }

        private void pbUcakBilgisayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgisayarın Uçak kartı seçildi!");
        }

        private void pbFirkateynBilgisayar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgisayarın Firkateyn kartı seçildi!");
        }

        private void pbObusOyuncu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyuncunun Obüs kartı seçildi!");
        }

        private void pbSihaOyuncu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyuncunun Siha kartı seçildi!");
        }

        private void pbSidaOyuncu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyuncunun Sida kartı seçildi!");
        }

        private void pbKfsOyuncu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyuncunun Kfs kartı seçildi!");
        }

        private void pbUcakOyuncu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyuncunun Uçak kartı seçildi!");
        }

        private void pbFirkateynOyuncu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oyuncunun Firkateyn kartı seçildi!");
        }



        #endregion

        #region Oyun İşlemleri
        // Yeni oyun başlatma butonu
        private void btnYeniOyun_Click(object sender, EventArgs e)
        {
            yeniOyunClickCount++;
            if (yeniOyunClickCount == 2)
            {
                yeniOyunClickCount = 0;
                MessageBox.Show("Yeni oyun başlatılıyor!");
                oyun = new Oyun(); // Yeni oyun nesnesi oluşturuluyor
                SkoruGuncelle(pbKullanıcıSkor, 0);
                SkoruGuncelle(pbBilgisayarSkor, 0);
                pbBilgisayarKazandi.Visible = false;
                pbOyuncuKazandi.Visible = false;
                pbBerabere.Visible = false;

                // Butonları tekrar etkinleştir
                btnKartSec.Enabled = true;
                btnHamleYap.Enabled = true;

                // Efektleri gizle
                GizleEfektler();
            }
        }

        // Kart seçme butonu
        private void btnKartSec_Click(object sender, EventArgs e)
        {
            kartSecClickCount++;
            if (kartSecClickCount == 2)
            {
                kartSecClickCount = 0;
                MessageBox.Show("Kartlar seçiliyor...");
                oyun.OyunHamlesi();
                GuncelleArayuz();
                if (oyun.oyunBitti)
                {
                    DisableButtons();
                }
            }
        }


        // Hamle yapma butonu
        private void btnHamleYap_Click(object sender, EventArgs e)
        {
            // Oyun bittiyse hamle yapılmasına izin verme
            if (oyun.oyunBitti)
            {
                MessageBox.Show("Oyun sona erdi! Yeni bir oyun başlatmak için 'Yeni Oyun' butonuna tıklayın.");
                return;
            }

            hamleYapClickCount++;
            if (hamleYapClickCount == 2)
            {
                hamleYapClickCount = 0;
                MessageBox.Show("Hamle yapılıyor!");
                oyun.OyunHamlesi();
                GosterEfektler();
                System.Threading.Tasks.Task.Delay(1500).ContinueWith(t => GizleEfektler(),
                    System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
                GuncelleArayuz();
            
        
                // Eğer oyun bitti ise butonları devre dışı bırak
                if (oyun.oyunBitti)
                {
                    DisableButtons();
                }
            }
        }
        private void DisableButtons()
        {
            btnKartSec.Enabled = false;
            btnHamleYap.Enabled = false;
        }
        #endregion

        #region Arayüz Güncelleme

        // Kazananı gösteren metot
        public void GosterKazanan(string sonuc)
        {
            pbBilgisayarKazandi.Visible = false;
            pbOyuncuKazandi.Visible = false;
            pbBerabere.Visible = false;

            if (sonuc == "Bilgisayar")
                pbBilgisayarKazandi.Visible = true;
            else if (sonuc == "Oyuncu")
                pbOyuncuKazandi.Visible = true;
            else if (sonuc == "Berabere")
                pbBerabere.Visible = true;
        }

        // Skor güncelleme
        private void SkoruGuncelle(PictureBox skorPictureBox, int skor)
        {
            Bitmap bitmap = new Bitmap(skorPictureBox.Width, skorPictureBox.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                g.DrawString(skor.ToString(), new Font("Arial", 24), Brushes.Black, new PointF(10, 10));
            }
            skorPictureBox.Image = bitmap;
        }

        // Arayüzü güncelleme
        private void GuncelleArayuz()
        {
            SkoruGuncelle(pbKullanıcıSkor, oyun.OyuncuSkoru);
            SkoruGuncelle(pbBilgisayarSkor, oyun.BilgisayarSkoru);

            if (oyun.oyunBitti)
            {
                string kazanan = oyun.OyuncuSkoru > oyun.BilgisayarSkoru
                    ? "Oyuncu"
                    : oyun.BilgisayarSkoru > oyun.OyuncuSkoru
                        ? "Bilgisayar"
                        : "Berabere";
                GosterKazanan(kazanan);
            }
        }
        #endregion

        #region Form Yükleme
        private void Form1_Load(object sender, EventArgs e)
        {
            pbBilgisayar.BackColor = this.BackColor;
            pbOyuncu.BackColor = this.BackColor;
            pbKfsBilgisayar.BackColor = this.BackColor;
            pbUcakBilgisayar.BackColor = this.BackColor;
            pbFirkateynBilgisayar.BackColor = this.BackColor;
            pbSihaBilgisayar.BackColor = this.BackColor;
            pbSidaBilgisayar.BackColor = this.BackColor;

            pbKfsOyuncu.BackColor = this.BackColor;
            pbUcakOyuncu.BackColor = this.BackColor;
            pbFirkateynOyuncu.BackColor = this.BackColor;
            pbSihaOyuncu.BackColor = this.BackColor;
            pbSidaOyuncu.BackColor = this.BackColor;

            oyun = new Oyun(); // Yeni oyun nesnesi
            SkoruGuncelle(pbKullanıcıSkor, oyuncuSkor);
            SkoruGuncelle(pbBilgisayarSkor, bilgisayarSkor);
        }
        #endregion
    }
}
