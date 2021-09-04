using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyListOluşturucu
{
    public partial class FormKeyListOlustur : Form
    {
        public FormKeyListOlustur()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }


        private void KeyPressListOlusturMetodu()
        {
            try
            {
                int IlkSayi = Convert.ToInt32(txtSayiBir.Text);
                int IkinciSayi = Convert.ToInt32(txtSayiIki.Text);
                progressBar1.Maximum = IkinciSayi;

                if (IlkSayi > IkinciSayi)
                {
                    MessageBox.Show("Lütfen İlk Sayıyı Küçük Giriniz.", "KeyList | Kaan Yeşillik");
                }
                if (txtSayiBir.TextLength < txtSayiIki.TextLength)
                {
                    for (int key = IlkSayi; key <= IkinciSayi; key++)
                    {
                        string BasaSifirEkle = key.ToString().PadLeft(txtSayiIki.TextLength, '0');
                        lstKeyList.Items.Add(BasaSifirEkle);
                        BgWorker.ReportProgress(key);

                    }
                }
            }
            catch (Exception)
            {


                if (txtSayiBir.Text == "")
                {
                    MessageBox.Show("Lütfen İlk Sayıyı Giriniz.", "KeyList | Kaan Yeşillik");
                }
                if (txtSayiIki.Text == "")
                {
                    MessageBox.Show("Lütfen İkinci Sayıyı Giriniz.", "KeyList | Kaan Yeşillik");
                }

            }


        }

        private void txtSayiBir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSayiIki_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void btnOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                lstKeyList.Items.Clear();
                BgWorker.RunWorkerAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Birşeyler Ters Gitti. Lütfen Programı Kapatıp Tekrar Açınız.", "KeyList | Kaan Yeşillik");
                
            }
           

        }
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            KeyPressListOlusturMetodu();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "KeyList|*.txt";
            saveFileDialog1.FilterIndex = 1;
            DialogResult dk = saveFileDialog1.ShowDialog();
            if (dk == DialogResult.OK)
            {


                try
                {

                    StreamWriter s = new StreamWriter(saveFileDialog1.FileName);
                    for (int i = 0; i < lstKeyList.Items.Count; i++)
                    {

                        s.WriteLine(lstKeyList.Items[i].ToString());

                    }
                    s.Close();
                    MessageBox.Show("Key Listesi " + saveFileDialog1.FileName + " Konumuna Kaydedildi.","KeyList | Kaan Yeşillik");
                }
                catch
                {

                    MessageBox.Show("Bir Sorun Oluştu Lütfen Tekrar Deneyin.", "KeyList | Kaan Yeşillik");

                }
            }
            else
            {
                MessageBox.Show("Lütfen Konum Seçerek ve İsim Yazarak Tekrar Deneyiniz", "KeyList | Kaan Yeşillik");
            }
        }
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Key Listeniz Hazırdır. Kaydetme İşlemi Yapabilirsiniz.", "KeyList | Kaan Yeşillik");
        }


    }
}

