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
    public partial class detail : Form
    {
        SqlConnection koneksi = new SqlConnection(Properties.Settings.Default.ConnectionString);
        private string no_Plat;
        private string jenis;
        private string merk;
        private string harga;
        private string tahun_buat;
        private string lain_lain;

        public detail()
        {
            InitializeComponent();
        }

        public detail(string no_Plat, string jenis, string merk, string harga, string tahun_buat, string lain_lain)
        {
            InitializeComponent();
            this.no_Plat = no_Plat;
            this.jenis = jenis;
            this.merk = merk;
            this.harga = harga;
            this.tahun_buat = tahun_buat;
            this.lain_lain = lain_lain;
        }

        private void detail_Load(object sender, EventArgs e)
        {
            this.plat_txt.Text = no_Plat;
            this.jenis_txt.Text = jenis;
            this.merk_txt.Text = merk;
            this.harga_txt.Text = harga;
            this.thnbuat_txt.Text = tahun_buat;
            this.lain_txt.Text = lain_lain;
        }

        private void tutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpanedit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(edit_noplat.Text) )
            {
                MessageBox.Show("Data masih kosong");
                return;
            }
            if (string.IsNullOrWhiteSpace(edit_jenis.Text))
            {
                MessageBox.Show("Data masih kosong");
                return;
            }
            if (string.IsNullOrWhiteSpace(edit_merk.Text))
            {
                MessageBox.Show("Data masih kosong");
                return;
            }
            if (string.IsNullOrWhiteSpace(edit_tahun.Text))
            {
                MessageBox.Show("Data masih kosong");
                return;
            }
            if (string.IsNullOrWhiteSpace(edit_harga.Text))
            {
                MessageBox.Show("Data masih kosong");
                return;
            }
            if (string.IsNullOrWhiteSpace(edit_lain.Text))
            {
                MessageBox.Show("Data masih kosong");
                return;
            }
            SqlCommand edit = new SqlCommand();
            edit.Connection = koneksi;
            koneksi.Open();
            edit.CommandType = CommandType.Text;
            edit.CommandText = "Update mobil set no_plat=@no_plat, jenis_mobil=@jenis_mobil, merk_mobil=@merk_mobil, tahun_buat=@tahun_buat, harga_sewa=@harga_sewa, lain_lain=@lain_lain where no_plat='" + no_Plat + "'";
            edit.Parameters.AddWithValue("@no_plat", SqlDbType.VarChar).Value = edit_noplat.Text;
            edit.Parameters.AddWithValue("@jenis_mobil", SqlDbType.VarChar).Value = edit_jenis.Text;
            edit.Parameters.AddWithValue("@merk_mobil", SqlDbType.VarChar).Value = edit_merk.Text;
            edit.Parameters.AddWithValue("@tahun_buat", SqlDbType.VarChar).Value = edit_tahun.Text;
            edit.Parameters.AddWithValue("@harga_sewa", SqlDbType.VarChar).Value = edit_harga.Text;
            edit.Parameters.AddWithValue("@lain_lain", SqlDbType.VarChar).Value = edit_lain.Text;
            edit.ExecuteNonQuery();
            MessageBox.Show("Data berhasil diupdate");
            koneksi.Close();
        }



    }
}
