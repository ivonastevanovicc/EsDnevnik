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
    public partial class Forme : Form
    {
        DataTable informacije;
        SqlCommand promene;
        string tabele;
        public Forme(string ime_tabele)
        {
            tabele = ime_tabele;
            InitializeComponent();
        }

        private void Refresh(string tabele)
        {
            try
            {
                informacije = new DataTable();
                informacije = Konekcija.Unos("SELECT * FROM " + tabele);
                if (dataGridView1.Rows.Count != 1)
                {
                    while (dataGridView1.Rows.Count != 1)
                    {
                        dataGridView1.Rows.RemoveAt(0);
                    }
                }

                if (tabele == "Smer")
                {
                    dataGridView1.Columns["Predmet"].Visible = false;
                    dataGridView1.Columns["Razred"].Visible = false;

                    for (int i = 0; i < informacije.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells["idd"].Value = Convert.ToString(informacije.Rows[i]["id"]);
                        dataGridView1.Rows[i].Cells["Smer"].Value = Convert.ToString(informacije.Rows[i]["naziv"]);
                    }
                }
                else if (tabele == "Predmet")
                {
                    dataGridView1.Columns["Smer"].Visible = false;
                    for (int i = 0; i < informacije.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells["idd"].Value = Convert.ToString(informacije.Rows[i]["id"]);
                        dataGridView1.Rows[i].Cells["Predmet"].Value = Convert.ToString(informacije.Rows[i]["naziv"]);
                        dataGridView1.Rows[i].Cells["Razred"].Value = Convert.ToString(informacije.Rows[i]["razred"]);
                    }

                }
                else
                {
                    dataGridView1.Columns["Predmet"].Visible = false;
                    dataGridView1.Columns["Razred"].Visible = false;
                    dataGridView1.Columns["Smer"].Visible = false;
                    dataGridView1.Columns["idd"].Visible = false;
                    dataGridView1.DataSource = informacije;
                    dataGridView1.Columns["id"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Source);
            }
        }

        private void Forme_Load(object sender, EventArgs e)
        {
            Refresh(tabele);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Brisi")
            {
                try
                {

                    int indeks;
                    if (tabele == "Skolska_godina")
                    {
                        indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    }
                    else
                    {
                        indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idd"].Value);
                    }

                    promene = new SqlCommand();
                    promene.CommandText = ("DELETE FROM " + tabele + " WHERE id = " + indeks);

                    SqlConnection con = new SqlConnection(Konekcija.Veza());
                    con.Open();
                    promene.Connection = con;
                    promene.ExecuteNonQuery();
                    con.Close();
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    Refresh(tabele);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne mozete obrisati ove podatake! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Refresh(tabele);
                }
            }

            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Menjaj")
            {
                try
                {

                    promene = new SqlCommand();

                    if (tabele == "Predmet")
                    {
                        int indeks;
                        indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idd"].Value);
                        string razred;
                        razred = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Razred"].Value);
                        string naziv;
                        naziv = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT naziv, razred FROM " + tabele + " WHERE naziv = " + "'" + naziv + "' AND razred = " + "'" + razred + "'");
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("UPDATE " + tabele + " SET naziv = " + "'" + naziv + "'" + " WHERE id = " + indeks +
                            " UPDATE " + tabele + " SET razred = " + "'" + razred + "'" + " WHERE id = " + indeks);
                    }
                    else if (tabele == "Smer")
                    {
                        int indeks;
                        indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idd"].Value);
                        string naziv;
                        naziv = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Smer"].Value);

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT naziv FROM " + tabele + " WHERE naziv = " + "'" + naziv + "'");
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("UPDATE " + tabele + " SET naziv = " + "'" + naziv + "'" + " WHERE id = " + indeks);
                    }
                    else
                    {
                        int indeks;
                        indeks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                        string naziv;
                        naziv = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["naziv"].Value);

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT naziv FROM " + tabele + " WHERE naziv = " + "'" + naziv + "'");
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("UPDATE " + tabele + " SET naziv = " + "'" + naziv + "'" + " WHERE id = " + indeks);
                    }

                    SqlConnection con = new SqlConnection(Konekcija.Veza());
                    con.Open();
                    promene.Connection = con;
                    promene.ExecuteNonQuery();
                    con.Close();

                    Refresh(tabele);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vec imate ovaj podatak u tabeli! " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Refresh(tabele);
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Dodaj")
            {
                try
                {

                    promene = new SqlCommand();

                    if (tabele == "Predmet")
                    {
                        string naziv;
                        naziv = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Predmet"].Value);
                        string razred;
                        razred = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Razred"].Value);

                        if (naziv == "" || razred == "")
                        {
                            throw new Exception();
                        }

                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT naziv, razred FROM " + tabele + " WHERE naziv = " + "'" + naziv + "' AND razred = " + "'" + razred + "'");
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("INSERT INTO " + tabele + " VALUES (" + "'" + naziv + "', " + "'" + razred + "')");
                    }
                    else if (tabele == "Smer")
                    {
                        string naziv;
                        naziv = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Smer"].Value);
                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT naziv FROM " + tabele + " WHERE naziv = " + "'" + naziv + "'");
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("INSERT INTO " + tabele + " VALUES (" + "'" + naziv + "')");
                    }
                    else
                    {
                        string naziv;
                        naziv = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["naziv"].Value);
                        informacije = new DataTable();
                        informacije = Konekcija.Unos("SELECT naziv FROM " + tabele + " WHERE naziv = " + "'" + naziv + "'");
                        if (informacije.Rows.Count >= 1) throw new Exception();

                        promene.CommandText = ("INSERT INTO " + tabele + " VALUES (" + "'" + naziv + "')");
                    }

                    SqlConnection con = new SqlConnection(Konekcija.Veza());
                    con.Open();
                    promene.Connection = con;
                    promene.ExecuteNonQuery();
                    con.Close();

                    Refresh(tabele);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne mozete dodati podatke koje vec imate u tabeli! - " + ex.Source, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Refresh(tabele);
                }
            }

        }
    }
}
