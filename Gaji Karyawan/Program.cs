using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Net.Security;

namespace Gaji_Karyawan
{
    internal class Program
    {
        private const string Username = "admin";
        private const string Password = "admin";
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                Console.WriteLine("Masukkan Username: ");
                string usernameInput = Console.ReadLine();
                Console.WriteLine("Masukkan Password");
                string passwordInput = Console.ReadLine();
                if (Authenticate(usernameInput, passwordInput))
                {
                    Console.WriteLine("Login Berhasil!\n");
                }
                else
                {
                    Console.WriteLine("Username atau Password Salah. Silahkan Coba Lagi.\n");
                }
                try
                {
                    Console.WriteLine("Masukkan Database Tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data Source = LAPTOP-OVM6JR48\\IRFANHIDAYATULAH;" +
                                        "Initial Catalog = {0};User ID = sa; Password = irfanhp30";
                                conn = new SqlConnection(string.Format(strKoneksi, db));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Data Karyawan");
                                        Console.WriteLine("2. Jadwal Kerja");
                                        Console.WriteLine("3. Dokument Izin");
                                        Console.WriteLine("4. Jabatan");
                                        Console.WriteLine("5. Keluar");
                                        Console.Write("\nEnter Your Choice (1-5): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Karyawan");
                                                    Console.WriteLine();
                                                    pr.DataKaryawan(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Jadwal Kerja");
                                                    Console.WriteLine();
                                                    pr.JadwalKerja(conn);
                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Dokument Izin");
                                                    Console.WriteLine();
                                                    pr.DokumentIzin(conn);
                                                }
                                                break;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Jabatan");
                                                    Console.WriteLine();
                                                    pr.Jabatan(conn);
                                                }
                                                break;
                                            case '5':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Keluar");
                                                    Console.WriteLine();
                                                    conn.Close();
                                                    return;
                                                }
                                                break;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Invalid choice.");
                                                    Console.WriteLine();
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nChech for thr value");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        static bool Authenticate(string username, string password)
        {
            return username == Username && password == Password;
        }
        public void DataKaryawan(SqlConnection conn)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Data Karyawan");
                Console.WriteLine("1. Tambah Data Karyawan");
                Console.WriteLine("2. Edit Data Karyawan");
                Console.WriteLine("3. Hapus Data Karyawan");
                Console.WriteLine("4. Tampilkan Data Karyawan");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("\nEnter Your Choice (1-5): ");
                char choice = Convert.ToChar(Console.ReadLine());

                switch (choice)
                {
                    case '1':
                        {
                            Console.Clear();
                            Console.WriteLine("Input Data Karyawan\n");
                            Console.WriteLine("Masukkan Id Karyawan: ");
                            string idkr = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Masukkan Nama Karyawan : ");
                            string Nama = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Masukkan Nama Jabatan : ");
                            string Jabatan = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Masukkan Alamat: ");
                            string Alamat = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Masukkan No Tlp Karyawan: ");
                            string Notlp = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Masukkan Gaji Karyawan: ");
                            string Gaji = Convert.ToString(Console.ReadLine());
                            try
                            {
                                TambahDataKaryawan(idkr, Nama, Jabatan, Alamat, Notlp, Gaji, conn);
                            }
                            catch
                            {
                                Console.WriteLine("\nAnda tidak mempunyai akses untuk menambah data");
                            }
                        }
                        break;
                    case '2':
                        try
                        {
                            EditDataKaryawan(conn);
                        }
                        catch
                        {
                            Console.WriteLine("\nAnda tidak mempunyai akses untuk mengedit");
                        }
                        break;
                    case '3':
                        {
                            Console.Clear();
                            Console.WriteLine("Masukkan Nama Karyawan yang ingin dihapus: ");
                            string Nama = Convert.ToString(Console.ReadLine());
                            try
                            {
                                HapusDataKaryawan(Nama, conn);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nAnda tidak mempunyai akses untuk mengedit");
                                Console.WriteLine(e.ToString());
                            }
                        }
                        break;
                    case '4':
                        {
                            Console.Clear();
                            Console.WriteLine("Data Karyawan");
                            TampilkanDataKaryawan(conn);
                        }
                        break;
                    case '5':
                        {
                            Console.Clear();
                            Console.WriteLine();
                            conn.Close();
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid choice.");
                        }
                        break;
                }
            }
        }

        public void TambahDataKaryawan(string idkr, string Nama, string Jabatan, string Alamat, string Notlp, string Gaji, SqlConnection conn)
        {
            string str = "";
            str = "insert into Karyawan (Id_Karyawan, Nama_Karyawan, Nama_Jabatan, Alamat, No_tlp, Gaji)" +
                "values (@idk,@nma,@nj,@a,@nt,@g)";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("idk", idkr));
            cmd.Parameters.Add(new SqlParameter("nma", Nama));
            cmd.Parameters.Add(new SqlParameter("nj", Jabatan));
            cmd.Parameters.Add(new SqlParameter("a", Alamat));
            cmd.Parameters.Add(new SqlParameter("nt", Notlp));
            cmd.Parameters.Add(new SqlParameter("g", Gaji));

            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }

        public void EditDataKaryawan(SqlConnection conn)
        {
            Console.WriteLine("Edit Data Karyawan");
            Console.WriteLine("Masukkan Id_Karyawan: ");
            string Id_Karyawan = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Masukkan Nama_Karyawan: ");
            string Nama_Karyawan = Console.ReadLine();

            string str = "";

            str = "Update Karyawan set Nama_Karyawan = @nma WHERE Id_Karyawan = @idk";
            string Nama = Console.ReadLine();
            // Implementasi logika untuk mengedit data karyawan
        }

        public void HapusDataKaryawan(string Nama, SqlConnection conn)
        {
            string str = "";
            str = "Hapus Data Karyawan where Nama_Karyawan = @nma";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nma", Nama));
            cmd.ExecuteNonQuery ();
            Console.WriteLine("Data Berhasil Dihapus");
        }

        public void TampilkanDataKaryawan(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("Select Nama_Karyawan, Nama_Jabatan, Alamat, No_tlp, Gaji", conn);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }

        public void JadwalKerja(SqlConnection conn)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Data Jadwal Kerja");
                Console.WriteLine("1. Tambah Jadwal");
                Console.WriteLine("2. Edit Jadwal");
                Console.WriteLine("3. Hapus Jadwal");
                Console.WriteLine("4. Tampilkan Jadwal");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("\nEnter Your Choice (1-5): ");
                char choice = Convert.ToChar(Console.ReadLine());

                switch (choice)
                {
                    case '1':
                        {
                            TambahJadwal(conn);
                        }
                        break;
                    case '2':
                        {
                            EditJadwal(conn);
                        }
                        break;
                    case '3':
                        {
                            HapusJadwal(conn);
                        }
                        break;
                    case '4':
                        {
                            TampilkanJadwal(conn);
                        }
                        break;
                    case '5':
                        {
                            Console.Clear();
                            Console.WriteLine("Kembali ke Menu Utama");
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid choice.");
                            Console.WriteLine();
                        }
                        break;
                }
            }
        }

        public void TambahJadwal(SqlConnection conn)
        {
            Console.WriteLine("Tambah Data Jadwal");
            // Implementasi logika untuk menambah data karyawan
        }

        public void EditJadwal(SqlConnection conn)
        {
            Console.WriteLine("Edit Data Jadwal");
            // Implementasi logika untuk mengedit data karyawan
        }

        public void HapusJadwal(SqlConnection conn)
        {
            Console.WriteLine("Hapus Data Jadwal");
            // Implementasi logika untuk menghapus data karyawan
        }

        public void TampilkanJadwal(SqlConnection conn)
        {
            Console.WriteLine("Tampilkan Data Jadwal");
            // Implementasi logika untuk menampilkan data karyawan
        }

        public void DokumentIzin(SqlConnection conn)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Data Dokument Izin");
                Console.WriteLine("1. Tambah Dokument");
                Console.WriteLine("2. Edit Dokument");
                Console.WriteLine("3. Hapus Dokument");
                Console.WriteLine("4. Tampilkan Dokument");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("\nEnter Your Choice (1-5): ");
                char choice = Convert.ToChar(Console.ReadLine());

                switch (choice)
                {
                    case '1':
                        {
                            TambahDokument(conn);
                        }
                        break;
                    case '2':
                        {
                            EditDokument(conn);
                        }
                        break;
                    case '3':
                        {
                            HapusDokument(conn);
                        }
                        break;
                    case '4':
                        {
                            TampilkanDokument(conn);
                        }
                        break;
                    case '5':
                        {
                            Console.Clear();
                            Console.WriteLine("Kembali ke Menu Utama");
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid choice.");
                            Console.WriteLine();
                        }
                        break;
                }
            }
        }

        public void TambahDokument(SqlConnection conn)
        {
            Console.WriteLine("Tambah Dokument");
            // Implementasi logika untuk menambah data karyawan
        }

        public void EditDokument(SqlConnection conn)
        {
            Console.WriteLine("Edit Dokument");
            // Implementasi logika untuk mengedit data karyawan
        }

        public void HapusDokument(SqlConnection conn)
        {
            Console.WriteLine("Hapus Dokument");
            // Implementasi logika untuk menghapus data karyawan
        }

        public void TampilkanDokument(SqlConnection conn)
        {
            Console.WriteLine("Tampilkan Dokument");
            // Implementasi logika untuk menampilkan data karyawan
        }

        public void Jabatan(SqlConnection conn)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Data Jabatan");
                Console.WriteLine("1. Tambah Data Jabatan");
                Console.WriteLine("2. Edit Data Jabatan");
                Console.WriteLine("3. Hapus Data Jabatan");
                Console.WriteLine("4. Tampilkan Data Jabatan");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("\nEnter Your Choice (1-5): ");
                char choice = Convert.ToChar(Console.ReadLine());

                switch (choice)
                {
                    case '1':
                        {
                            TambahJabatan(conn);
                        }
                        break;
                    case '2':
                        {
                            EditJabatan(conn);
                        }
                        break;
                    case '3':
                        {
                            HapusJabatan(conn);
                        }
                        break;
                    case '4':
                        {
                            TampilkanJabatan(conn);
                        }
                        break;
                    case '5':
                        {
                            Console.Clear();
                            Console.WriteLine("Kembali ke Menu Utama");
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid choice.");
                            Console.WriteLine();
                        }
                        break;
                }
            }
        }

        public void TambahJabatan(SqlConnection conn)
        {
            Console.WriteLine("Tambah Data Jabatan");
            // Implementasi logika untuk menambah data karyawan
        }

        public void EditJabatan(SqlConnection conn)
        {
            Console.WriteLine("Edit Data Jabatan");
            // Implementasi logika untuk mengedit data karyawan
        }

        public void HapusJabatan(SqlConnection conn)
        {
            Console.WriteLine("Hapus Data Jabatan");
            // Implementasi logika untuk menghapus data karyawan
        }

        public void TampilkanJabatan(SqlConnection conn)
        {
            Console.WriteLine("Tampilkan Data Jabatan");
            // Implementasi logika untuk menampilkan data karyawan
        }
    }
}