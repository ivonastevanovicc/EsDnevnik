using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EsDnevnik
{
    public partial class Ocene : Form
    {
        DataTable informacije, pom;
        SqlCommand promene;
        string[] pom2;

        public Ocene()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Brisi")
            {
                try
                {
                    {
                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        promene = new SqlCommand();
                        promene.CommandText = ("DELETE FROM Ocena WHERE id = " + indeks);

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        promene.Connection = con;
                        promene.ExecuteNonQuery();
                        con.Close();
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        Refresh();
                    }
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
                    {
                        promene = new SqlCommand();

                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                        string[] ime_prezime = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Ucenik"].Value).Split(' ');
                        string datum = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Datum"].Value);
                        string raspodela = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);
                        string ocena = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Ocena"].Value);
                        string predmet = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = " + "'" + ime_prezime[0] + "' AND prezime = " + "'" + ime_prezime[1] + "'");
                        int osoba_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Predmet WHERE naziv = " + "'" + predmet + "'");
                        int predmet_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Raspodela WHERE predmet_id = " + predmet_id);
                        int raspodela_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT * FROM Ocena WHERE datum = '" + datum + "' AND raspodela_id = " + raspodela_id + " AND ocena = '" + ocena + "' AND ucenik_id = " + osoba_id);
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("UPDATE Ocena SET datum = '" + datum + "' WHERE id = " + indeks +
                            " UPDATE Ocena SET raspodela_id = " + raspodela_id + " WHERE id = " + indeks +
                            " UPDATE Ocena SET ocena = '" + ocena + "' WHERE id = " + indeks +
                            " UPDATE Ocena SET ucenik_id = " + osoba_id + " WHERE id = " + indeks);

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        promene.Connection = con;
                        promene.ExecuteNonQuery();
                        con.Close();

                        Refresh();

                    }
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
                    {
                        promene = new SqlCommand();

                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                        string[] ime_prezime = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Ucenik"].Value).Split(' ');
                        string datum = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Datum"].Value);
                        string ocena = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Ocena"].Value);
                        string predmet = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);

                        if (ime_prezime[0] == "" || ime_prezime[1] == "" || predmet == "" || datum == "" || ocena == "") throw new Exception();

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = " + "'" + ime_prezime[0] + "' AND prezime = " + "'" + ime_prezime[1] + "'");
                        int osoba_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Predmet WHERE naziv = " + "'" + predmet + "'");
                        int predmet_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Raspodela WHERE predmet_id = " + predmet_id);
                        int raspodela_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT * FROM Ocena WHERE datum = '" + datum + "' AND raspodela_id = " + raspodela_id + " AND ocena = '" + ocena + "' AND ucenik_id = " + osoba_id);
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("INSERT INTO Ocena VALUES ('" + datum + "', " + raspodela_id + ", " + ocena + ", " + osoba_id + ")");

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        promene.Connection = con;
                        promene.ExecuteNonQuery();
                        con.Close();

                        Refresh();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne mozete da dodate vec postojece podatke! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Refresh();
                }
            }
        }

        private void Refresh()
        {
            informacije = new DataTable();
            informacije = Konekcija.Unos("SELECT Ocena.id AS Id, datum AS Datum, Osoba.ime + ' ' + Osoba.prezime AS Ucenik, ocena AS Ocena, Predmet.naziv AS Predmet FROM Ocena\r\n JOIN Osoba ON Ocena.ucenik_id = Osoba.id\r\n JOIN Raspodela ON Raspodela.id = Ocena.raspodela_id\r\n JOIN Predmet ON Predmet.id = Raspodela.predmet_id");

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
                dataGridView1.Rows[i].Cells["Id"].Value = Convert.ToString(informacije.Rows[i]["Id"]);
                dataGridView1.Rows[i].Cells["Datum"].Value = Convert.ToString(informacije.Rows[i]["Datum"]);
                dataGridView1.Rows[i].Cells["Ucenik"].Value = Convert.ToString(informacije.Rows[i]["Ucenik"]);
                dataGridView1.Rows[i].Cells["Ocena"].Value = Convert.ToString(informacije.Rows[i]["Ocena"]);
                dataGridView1.Rows[i].Cells["Predmet"].Value = Convert.ToString(informacije.Rows[i]["Predmet"]);
            }
        }

        private void Ocene_Load(object sender, EventArgs e)
        {
            pom = new DataTable();
            pom = Konekcija.Unos("SELECT ime + ' ' + prezime AS Ucenik FROM Osoba WHERE ULOGA = 1");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["Ucenik"]);
                Ucenik.Items.Add(pom2[i]);
            }

            pom = new DataTable();
            pom = Konekcija.Unos("SELECT DISTINCT naziv FROM Predmet");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["naziv"]);
                Predmet.Items.Add(pom2[i]);
            }

            Refresh();
        }
    }
}
