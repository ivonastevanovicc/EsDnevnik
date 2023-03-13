using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EsDnevnik
{
    public partial class Upisnica : Form
    {
        DataTable informacije, pom;
        SqlCommand promene;
        string[] pom2;
        public Upisnica()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            informacije = new DataTable();
            informacije = Konekcija.Unos("SELECT Upisnica.id AS id, Osoba.ime + ' ' + Osoba.prezime AS 'ime i prezime', STR(Odeljenje.razred,1,0) + '/' + Odeljenje.indeks AS Odeljenje, Smer.naziv AS Smer FROM Upisnica\r\nJOIN Osoba ON Upisnica.osoba_id = Osoba.id\r\nJOIN Odeljenje ON Upisnica.odeljenje_id = Odeljenje.id\r\nJOIN Smer ON Odeljenje.smer_id = Smer.id");

            if (dataGridView1.Rows.Count != 1)
            {
                while (dataGridView1.Rows.Count != 1)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }
            }

            for (int i = 0; i < informacije.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["Id"].Value = Convert.ToString(informacije.Rows[i]["id"]);
                dataGridView1.Rows[i].Cells["Ime_prezime"].Value = Convert.ToString(informacije.Rows[i]["ime i prezime"]);
                dataGridView1.Rows[i].Cells["Odeljenje"].Value = Convert.ToString(informacije.Rows[i]["Odeljenje"]);
                dataGridView1.Rows[i].Cells["Smer"].Value = Convert.ToString(informacije.Rows[i]["Smer"]);
            }
        }

        private void Upisnica_Load(object sender, EventArgs e)
        {
            pom = new DataTable();//Dodavanje ucenika
            pom = Konekcija.Unos("SELECT ime + ' ' + prezime AS 'ime i prezime' FROM Osoba WHERE uloga = 1");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["ime i prezime"]);
                Ime_prezime.Items.Add(pom2[i]);
            }

            pom = new DataTable();//Dodavanje odeljenja
            pom = Konekcija.Unos("SELECT STR(Odeljenje.razred,1,0) + '/' + Odeljenje.indeks AS Odeljenje FROM Odeljenje");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["Odeljenje"]);
                Odeljenje.Items.Add(pom2[i]);
            }

            pom = new DataTable();//Dodavanje smerova
            pom = Konekcija.Unos("SELECT naziv FROM Smer");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["naziv"]);
                Smer.Items.Add(pom2[i]);
            }
            Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Brisi")
            {
                try
                
                    {
                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        promene = new SqlCommand();
                        promene.CommandText = ("DELETE FROM Upisnica WHERE id = " + indeks);

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        promene.Connection = con;
                        promene.ExecuteNonQuery();
                        con.Close();
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        Refresh();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne mozete da obrisete ove podatake, druge tabele zahtevaju ove podatake! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Menjaj")
            {
                try
                
                    {
                        promene = new SqlCommand();

                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                        string[] ime_prezime = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Ime_prezime"].Value).Split(' ');
                        string[] odeljenje = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Odeljenje"].Value).Split('/');

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = " + "'" + ime_prezime[0] + "' AND prezime = " + "'" + ime_prezime[1] + "'");
                        int osoba_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Odeljenje WHERE razred = " + "'" + odeljenje[0] + "' AND indeks = " + "'" + odeljenje[1] + "'");
                        int odeljenje_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT * FROM Upisnica WHERE osoba_id = " + osoba_id + " AND odeljenje_id = " + odeljenje_id);
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("UPDATE Upisnica SET osoba_id = " + osoba_id + " WHERE id = " + indeks +
                            " UPDATE Upisnica SET odeljenje_id = " + odeljenje_id + " WHERE id = " + indeks);

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        promene.Connection = con;
                        promene.ExecuteNonQuery();
                        con.Close();

                        Refresh();

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Podatak vec postoji u tabeli - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Refresh();
                }
            }

            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Dodaj")
            {
                try
                
                    {
                        promene = new SqlCommand();

                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                        string[] ime_prezime = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Nastavnik"].Value).Split(' ');
                        string[] odeljenje = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Odeljenje"].Value).Split('/');

                        if (ime_prezime[0] == "" || ime_prezime[1] == "" || odeljenje[0] == "" || odeljenje[1] == "")
                            throw new Exception();

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = " + "'" + ime_prezime[0] + "' AND prezime = " + "'" + ime_prezime[1] + "'");
                        int osoba_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Odeljenje WHERE razred = " + "'" + odeljenje[0] + "' AND indeks = " + "'" + odeljenje[1] + "'");
                        int odeljenje_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT * FROM Raspodela WHERE nastavnik_id = " + osoba_id + " AND odeljenje_id = " + odeljenje_id);
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("INSERT INTO Raspodela VALUES (" + osoba_id + ", " + odeljenje_id + ")");

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        promene.Connection = con;
                        promene.ExecuteNonQuery();
                        con.Close();

                        Refresh();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne mozete da dodate vec postojece podatke! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Refresh();
                }
            }
        }
    }
}
