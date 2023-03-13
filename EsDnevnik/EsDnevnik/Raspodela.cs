﻿using System;
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
    public partial class Raspodela : Form
    {
        DataTable informacije, pom;
        SqlCommand promene;
        string[] pom2;
        public Raspodela()
        {
            InitializeComponent();
        }
        private void Refresh()
        {
            informacije = new DataTable();
            informacije = Konekcija.Unos("SELECT raspodela.id, Osoba.ime + ' ' + Osoba.prezime AS Nastavnik, Skolska_godina.naziv AS Godina, Predmet.naziv AS Predmet, STR(Odeljenje.razred,1,0) + '/' + Odeljenje.indeks AS Odeljenje FROM Raspodela left join Osoba ON Raspodela.nastavnik_id = Osoba.id left join Skolska_godina ON Raspodela.godina_id = Skolska_godina.id left join Predmet ON Raspodela.predmet_id = Predmet.id left join Odeljenje ON Raspodela.odeljenje_id = Odeljenje.id");

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
                dataGridView1.Rows[i].Cells["id"].Value = Convert.ToString(informacije.Rows[i]["id"]);
                dataGridView1.Rows[i].Cells["Nastavnik"].Value = Convert.ToString(informacije.Rows[i]["Nastavnik"]);
                dataGridView1.Rows[i].Cells["Godina"].Value = Convert.ToString(informacije.Rows[i]["Godina"]);
                dataGridView1.Rows[i].Cells["Predmet"].Value = Convert.ToString(informacije.Rows[i]["Predmet"]);
                dataGridView1.Rows[i].Cells["Odeljenje"].Value = Convert.ToString(informacije.Rows[i]["Odeljenje"]);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Brisi")
            {
                try
                {
                    if (MessageBox.Show("Da li ste sigurni da zelite da obrisete ove podatake?", "EsDnevnik", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                        promene = new SqlCommand();
                        promene.CommandText = ("DELETE FROM Raspodela WHERE id = " + indeks);

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
                    if (MessageBox.Show("Da li ste sigurni da zelite da izmenite ove podatke?", "EsDnevnik", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        promene = new SqlCommand();

                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                        string[] ime_prezime = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Nastavnik"].Value).Split(' ');
                        string godina = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Godina"].Value);
                        string predmet = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);
                        string[] odeljenje = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Odeljenje"].Value).Split('/');

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = " + "'" + ime_prezime[0] + "' AND prezime = " + "'" + ime_prezime[1] + "'");
                        int osoba_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Skolska_godina WHERE naziv = " + "'" + godina + "'");
                        int godina_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Predmet WHERE naziv = " + "'" + predmet + "'");
                        int predmet_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Odeljenje WHERE razred = " + "'" + odeljenje[0] + "' AND indeks = " + "'" + odeljenje[1] + "'");
                        int odeljenje_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT * FROM Raspodela WHERE nastavnik_id = " + osoba_id + " AND godina_id = " + godina_id + " AND predmet_id = " + predmet_id + " AND odeljenje_id = " + odeljenje_id);
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("UPDATE Raspodela SET nastavnik_id = " + osoba_id + " WHERE id = " + indeks +
                            " UPDATE Raspodela SET godina_id = " + godina_id + " WHERE id = " + indeks +
                            " UPDATE Raspodela SET predmet_id = " + predmet_id + " WHERE id = " + indeks +
                            " UPDATE Raspodela SET odeljenje_id = " + odeljenje_id + " WHERE id = " + indeks);

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
                    if (MessageBox.Show("Da li ste sigurni da zelite da dodate ove podatke?", "EsDnevnik", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        promene = new SqlCommand();

                        int indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                        string[] ime_prezime = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Nastavnik"].Value).Split(' ');
                        string godina = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Godina"].Value);
                        string predmet = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);
                        string[] odeljenje = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Odeljenje"].Value).Split('/');

                        if (ime_prezime[0] == "" || ime_prezime[1] == "" || godina == "" || predmet == "" || odeljenje[0] == "" || odeljenje[1] == "")
                            throw new Exception();

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = " + "'" + ime_prezime[0] + "' AND prezime = " + "'" + ime_prezime[1] + "'");
                        int osoba_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Skolska_godina WHERE naziv = " + "'" + godina + "'");
                        int godina_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Predmet WHERE naziv = " + "'" + predmet + "'");
                        int predmet_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT id FROM Odeljenje WHERE razred = " + "'" + odeljenje[0] + "' AND indeks = " + "'" + odeljenje[1] + "'");
                        int odeljenje_id = (int)informacije.Rows[0][0];

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT * FROM Raspodela WHERE nastavnik_id = " + osoba_id + " AND godina_id = " + godina_id + " AND predmet_id = " + predmet_id + " AND odeljenje_id = " + odeljenje_id);
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("INSERT INTO Raspodela VALUES (" + osoba_id + ", " + godina_id + ", " + predmet_id + ", " + odeljenje_id + ")");

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

        private void Raspodela_Load(object sender, EventArgs e)
        {
            pom = new DataTable();
            pom = Konekcija.Unos("SELECT naziv FROM Skolska_godina");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["naziv"]);
                Godina.Items.Add(pom2[i]);
            }

            pom = new DataTable();//Dodavanje nastavnika
            pom = Konekcija.Unos("SELECT ime + ' ' + prezime AS Nastavnik FROM Osoba WHERE uloga = 2");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["Nastavnik"]);
                Nastavnik.Items.Add(pom2[i]);
            }

            pom = new DataTable();//Dodavanje predmeta
            pom = Konekcija.Unos("SELECT DISTINCT naziv FROM Predmet");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["naziv"]);
                Predmet.Items.Add(pom2[i]);
            }

            pom = new DataTable();//Dodavanje odeljenja
            pom = Konekcija.Unos("SELECT STR(razred,1,0) + '/' + indeks AS Odlj FROM Odeljenje");
            pom2 = new string[pom.Rows.Count];

            for (int i = 0; i < pom.Rows.Count; i++)
            {
                pom2[i] = Convert.ToString(pom.Rows[i]["Odlj"]);
                Odeljenje.Items.Add(pom2[i]);
            }
            Refresh();
        }
    }
}
