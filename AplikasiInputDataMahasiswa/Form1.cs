using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInputDataMahasiswa
{
    public partial class Form1 : Form
    {
        private List<Mahasiswa> list = new List<Mahasiswa>();  
        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Kode", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Mata Kuliah", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Bobot", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Huruf", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("JML SKS", 70, HorizontalAlignment.Center);
        }

        private void ResetForm()
        {
            txtKode.Clear();
            txtMatKul.Clear();
            txtKelas.Clear();
            txtBobot.Text = "0";
            txtKode.Focus();
        }


        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";
            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // inputan selain angka
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
             
            Mahasiswa mhs = new Mahasiswa();
            mhs.Kode= txtKode.Text;
            mhs.MatKul = txtMatKul.Text;
            mhs.Kelas = txtKelas.Text;
            mhs.Bobot = int.Parse(txtBobot.Text);
            mhs.jml_sks = int.Parse(txtjmlsks.Text);
            

            int Bobot = int.Parse(txtBobot.Text);

            if(Bobot >= 4)
            {
                mhs.Nilai_Huruf= 'A';

            }else if(Bobot >= 3 )
            {
                mhs.Nilai_Huruf = 'B';
            }
            else if (Bobot >= 2)
            {
                mhs.Nilai_Huruf = 'C';
            }
            else if (Bobot >= 1 )
            {
                mhs.Nilai_Huruf = 'D';
            }
            list.Add(mhs);
            var msg = "Data mahasiswa berhasil disimpan.";
            MessageBox.Show(msg, "Informasi", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
            
            ResetForm();

        }

        private void TampilkanData()
        {
            lvwMahasiswa.Items.Clear();
            foreach (var mhs in list)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Kode);
                item.SubItems.Add(mhs.MatKul);
                item.SubItems.Add(mhs.Kelas);
                item.SubItems.Add(mhs.Bobot.ToString());
                item.SubItems.Add(mhs.Nilai_Huruf.ToString());
                item.SubItems.Add(mhs.jml_sks.ToString());
                lvwMahasiswa.Items.Add(item);
            }
        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus ? ", "Konfirmasi",
               
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    
                    var index = lvwMahasiswa.SelectedIndices[0];
                    list.RemoveAt(index);
                    TampilkanData();
                }
            }else 
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                int totalBobotSks = 0;
                int totalSks = 0;

                foreach (var mhs in list)
                {
                    totalBobotSks += mhs.Bobot * mhs.jml_sks;
                    totalSks += mhs.jml_sks;
                }

                if (totalSks > 0)
                {
                    double IPK = (double)totalBobotSks / totalSks;
                    MessageBox.Show("IPK: " + IPK.ToString("F2"), "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak ada data mata kuliah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            

        }
    }
}
