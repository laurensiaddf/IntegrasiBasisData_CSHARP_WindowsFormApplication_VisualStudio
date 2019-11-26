using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modul4
{
    public partial class Form1 : Form
    {
        SqlConnection koneksi = new SqlConnection(Properties.Settings.Default.ConnectionString);
        public String No_ktp;
        public String No_Plat;

        public void tampil_data()
        {
            DataSet mobil = new DataSet();
            DataSet penyewa = new DataSet();

            var tabel = new SqlDataAdapter();
            //menampilkan data pada tab 1 Katalog Mobil
            tabel.SelectCommand = new SqlCommand("Select no_plat as 'No. Plat',jenis_mobil as 'Jenis', merk_mobil as 'Merk',harga_sewa, tahun_buat, lain_lain from mobil where stat='masih'");
            tabel.SelectCommand.Connection = koneksi;
            tabel.SelectCommand.CommandType = CommandType.Text;
            tabel.Fill(mobil);
            dataGridView1.DataSource = mobil.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();

            tabel.SelectCommand = new SqlCommand("Select*from penyewa_mobil");
            tabel.SelectCommand.Connection = koneksi;
            tabel.SelectCommand.CommandType = CommandType.Text;
            tabel.Fill(penyewa);
            DataPenyewa.DataSource = penyewa.Tables[0];
            DataPenyewa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns();
        }

        public Form1()
        {
            InitializeComponent();
            nama_input.Enabled = false;
            alamat_input.Enabled = false;
            noktp_input.Enabled = false;
            plat_input.Enabled = false;
            merk_input.Enabled = false;
            btn_pinjam.Visible = false;
            btn_hapus.Visible = false;
            nama_txt.Text = null;
            btn_edit.Visible = false;
            nama_edit.Enabled = false;
            alamat_edit.Enabled = false;
            tampil_data();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            No_Plat = row.Cells[0].Value.ToString();
            String jenis = row.Cells[1].Value.ToString();
            String merk = row.Cells[2].Value.ToString();
            String harga = row.Cells[3].Value.ToString();
            String tahun_buat = row.Cells[4].Value.ToString();
            String lain_lain = row.Cells[5].Value.ToString();
            detail tampil = new detail(No_Plat, jenis, merk, harga, tahun_buat, lain_lain);
            tampil.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            No_Plat = row.Cells[0].Value.ToString();
            plat_input.Text = No_Plat;
            merk_input.Text = row.Cells[2].Value.ToString();
            nama_input.Enabled = true;
            alamat_input.Enabled = true;
            noktp_input.Enabled = true;
            btn_pinjam.Visible = true;
        }

        private void btn_pinjam_Click(object sender, EventArgs e)
        {
            String kueri_penyewa = "insert into penyewa values(@no_ktp,@nama,@alamat,@no_plat)";
            using (SqlCommand tambah = new SqlCommand("", koneksi))
            {
                tambah.Parameters.AddWithValue("@no_ktp", SqlDbType.VarChar).Value = noktp_input.Text;
                tambah.Parameters.AddWithValue("@nama", SqlDbType.VarChar).Value = nama_input.Text;
                tambah.Parameters.AddWithValue("@alamat", SqlDbType.VarChar).Value = alamat_input.Text;
                tambah.Parameters.AddWithValue("@no_plat", SqlDbType.VarChar).Value = plat_input.Text;
                koneksi.Open();
                tambah.CommandType = CommandType.Text;
                tambah.CommandText = kueri_penyewa;
                tambah.ExecuteNonQuery();
                tambah.CommandText = "update mobil set stat='dipinjam' where no_plat = '" + plat_input.Text + "'";
                tambah.ExecuteNonQuery();
                koneksi.Close();
                tampil_data();
                btn_pinjam.Visible = false;
                nama_input.Text = null;
                nama_input.Enabled = false;
                alamat_input.Enabled = false;
                noktp_input.Enabled = false;
                alamat_input.Text = null;
                noktp_input.Text = null;
                plat_input.Text = null;
                merk_input.Text = null;
            }
        }

        private void DataPenyewa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.DataPenyewa.Rows[e.RowIndex];
            No_ktp = row.Cells[0].Value.ToString();
            No_Plat = row.Cells[3].Value.ToString();
            nama_txt.Text = row.Cells[1].Value.ToString();
            btn_hapus.Visible = true;
            nama_edit.Enabled = true;
            alamat_edit.Enabled = true;
            btn_edit.Visible = true;
            nama_edit.Text = row.Cells[1].Value.ToString();
            alamat_edit.Text = row.Cells[2].Value.ToString();

        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            SqlCommand hapus = new SqlCommand();
            hapus.Connection = koneksi;
            koneksi.Open();
            hapus.CommandType = CommandType.Text;
            hapus.CommandText = "Delete from penyewa where no_ktp = '" + No_ktp + "'";
            hapus.ExecuteNonQuery();
            hapus.CommandText = "Update mobil set stat='masih' where no_plat = '" + No_Plat + "'";
            hapus.ExecuteNonQuery();
            koneksi.Close();
            tampil_data();
            btn_hapus.Visible = false;
            nama_edit.Enabled = false;
            nama_edit.Text = null;
            alamat_edit.Enabled = false;
            alamat_edit.Text = null;
            btn_edit.Visible = false;
            nama_txt.Text = null;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            SqlCommand edit = new SqlCommand();
            edit.Connection = koneksi;
            koneksi.Open();
            edit.CommandType = CommandType.Text;
            edit.CommandText = "Update penyewa set nama=@nama,alamat=@alamat where no_ktp='" + No_ktp + "'";
            edit.Parameters.AddWithValue("@nama", SqlDbType.VarChar).Value = nama_edit.Text;
            edit.Parameters.AddWithValue("@alamat", SqlDbType.VarChar).Value = alamat_edit.Text;
            edit.ExecuteNonQuery();
            MessageBox.Show("Data berhasil diubah");
            koneksi.Close();
            btn_hapus.Visible = false;
            nama_edit.Enabled = false;
            nama_edit.Text = null;
            alamat_edit.Enabled = false;
            alamat_edit.Text = null;
            btn_edit.Visible = false;
            nama_txt.Text = null;
            tampil_data();
        }

        private void tambah_Click(object sender, EventArgs e)
        {
            tambahmobil tm = new tambahmobil();
            tm.Visible = true;
            this.Visible = true;
        }

        private void hapusmobil_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Data yang dihapus akan hilang permanen. Lanjutkan Menghapus?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand hapus = new SqlCommand();
                hapus.Connection = koneksi;
                koneksi.Open();
                hapus.CommandType = CommandType.Text;
                hapus.CommandText = "Delete from mobil where no_plat = '" + No_Plat + "'";
                hapus.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dhapus");
                koneksi.Close();
                tampil_data();
            }
            else
            {

            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            tampil_data();
        }
    }
}
