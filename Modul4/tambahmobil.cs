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
    public partial class tambahmobil : Form
    {
        SqlConnection koneksi = new SqlConnection(Properties.Settings.Default.ConnectionString);
        public tambahmobil()
        {
            InitializeComponent();
        }

        private void simpan_Click(object sender, EventArgs e)
        {
            int harga = int.Parse(input_harga.Text);
            SqlCommand tambah = new SqlCommand();
            tambah.Connection = koneksi;
            koneksi.Open();
            tambah.CommandType = CommandType.Text;
            tambah.CommandText = "insert into mobil values (@no_plat, @jenis_mobil, @merk_mobil, @tahun_mobil, '" + harga + "', @lain_lain, 'masih')";
            tambah.Parameters.AddWithValue("@no_plat", SqlDbType.VarChar).Value = input_noplat.Text;
            tambah.Parameters.AddWithValue("@jenis_mobil", SqlDbType.VarChar).Value = input_jenis.Text;
            tambah.Parameters.AddWithValue("@merk_mobil", SqlDbType.VarChar).Value = input_merk.Text;
            tambah.Parameters.AddWithValue("@tahun_mobil", SqlDbType.VarChar).Value = input_tahun.Text;
            tambah.Parameters.AddWithValue("@lain_lain", SqlDbType.VarChar).Value = input_lainlain.Text;
            tambah.ExecuteNonQuery();
            MessageBox.Show("Data berhasil ditambahkan");
            koneksi.Close();
            input_noplat.Text = null;
            input_jenis.Text = null;
            input_merk.Text = null;
            input_tahun.Text = null;
            input_harga.Text = null;
            input_lainlain.Text = null;
        }

        private void kembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
