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
using System.Configuration;

namespace EsDnevnik
{
    public partial class Odeljenje : Form
    {
        DataTable ucenici,ucenici2,ucenici3,ucenici4,izmena,izmena1,izmena2;
        int brVrste;
        SqlCommand komanda;
        public Odeljenje()
        {
            InitializeComponent();
        }
        private void TxtLoad()
        {
            if (ucenici.Rows.Count == -1)
            {
                label5.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                button1.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                textBox1.Text = ucenici.Rows[brVrste][0].ToString();
                textBox2.Text = ucenici.Rows[brVrste][1].ToString();
                textBox3.Text = ucenici.Rows[brVrste][2].ToString();
                comboBox1.Text = ucenici2.Rows[brVrste][0].ToString();
                comboBox2.Text = ucenici3.Rows[brVrste][0].ToString();
                comboBox3.Text = ucenici4.Rows[brVrste][0].ToString();
                if (brVrste == 0)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                }
                else {
                    button1.Enabled = true;
                    button2.Enabled = true;
                }
                if (brVrste == ucenici.Rows.Count - 1)
                {
                    button6.Enabled = false;
                    button7.Enabled = false;
                }
                else {
                    button6.Enabled = true;
                    button7.Enabled = true;
                } 
            }


        }

        private void Odeljenje_Load(object sender, EventArgs e)
        {
            ucenici = new DataTable();
            ucenici2 = new DataTable();
            ucenici3 = new DataTable();
            ucenici4 = new DataTable();

            ucenici = Konekcija.Unos("SELECT * FROM Odeljenje");
            ucenici2 = Konekcija.Unos("SELECT naziv FROM Smer JOIN Odeljenje ON smer.id = odeljenje.smer_id");
            ucenici3 = Konekcija.Unos("SELECT ime + ' ' + prezime from Osoba JOIN Odeljenje ON osoba.id = odeljenje.razredni_id");
            ucenici4 = Konekcija.Unos("SELECT naziv FROM Skolska_godina JOIN Odeljenje ON odeljenje.godina_id = skolska_godina.id");
            TxtLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(textBox1.Text);
                int smer, godina, razredni;
                string[] ime_prezime = comboBox2.Text.Split(' ');

                izmena = new DataTable();
                izmena = Konekcija.Unos("SELECT id FROM Smer WHERE naziv = '" + comboBox1.Text + "'");
                izmena1 = new DataTable();
                izmena1 = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = '" + ime_prezime[0] + "'" + " AND prezime = '" + ime_prezime[1] + "'");
                izmena2 = new DataTable();
                izmena2 = Konekcija.Unos("SELECT id FROM Skolska_godina WHERE naziv = '" + comboBox3.Text + "'");

                razredni = (int)izmena.Rows[0][0];
                smer = (int)izmena.Rows[0][0];
                godina = (int)izmena.Rows[0][0];

                komanda = new SqlCommand();
                komanda.CommandText = ("INSERT INTO Odeljenje VALUES (" + Convert.ToInt32(textBox2.Text) + ", " + textBox3.Text + ", " + smer + ", " + razredni + ", " + godina + ")");
                SqlConnection kon = new SqlConnection(Konekcija.Veza());

                kon.Open();
                komanda.Connection = kon;
                komanda.ExecuteNonQuery();
                kon.Close();

                label7.Text = "Podatak unesen!";
                label7.Visible = true;

                ucenici = new DataTable();
                ucenici2 = new DataTable();
                ucenici3 = new DataTable();
                ucenici4 = new DataTable();

                ucenici = Konekcija.Unos("SELECT * FROM Odeljenje");
                ucenici2 = Konekcija.Unos("SELECT naziv FROM Smer JOIN Odeljenje ON smer.id = odeljenje.smer_id");
                ucenici3 = Konekcija.Unos("SELECT ime + ' ' + prezime from Osoba JOIN Odeljenje ON osoba.id = odeljenje.razredni_id");
                ucenici4 = Konekcija.Unos("SELECT naziv FROM Skolska_godina JOIN Odeljenje ON odeljenje.godina_id = skolska_godina.id");
                TxtLoad();
            }
            catch
            {
                label7.Text = "Podaci lose uneseni";
                label7.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (brVrste < ucenici.Rows.Count - 1)
            {
                brVrste++;
                TxtLoad();
                label7.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Brisi
        {

            try
            {

                komanda = new SqlCommand();
                komanda.CommandText = ("DELETE FROM odeljenje WHERE id = " + Convert.ToInt32(textBox1.Text));
                SqlConnection kon = new SqlConnection(Konekcija.Veza());
                kon.Open();
                komanda.Connection = kon;
                komanda.ExecuteNonQuery();
                kon.Close();

                ucenici = new DataTable();
                ucenici2 = new DataTable();
                ucenici3 = new DataTable();
                ucenici4 = new DataTable();

                ucenici = Konekcija.Unos("SELECT * FROM Odeljenje");
                ucenici2 = Konekcija.Unos("SELECT naziv FROM Smer JOIN Odeljenje ON smer.id = odeljenje.smer_id");
                ucenici3 = Konekcija.Unos("SELECT ime + ' ' + prezime from Osoba JOIN Odeljenje ON osoba.id = odeljenje.razredni_id");
                ucenici4 = Konekcija.Unos("SELECT naziv FROM Skolska_godina JOIN Odeljenje ON odeljenje.godina_id = skolska_godina.id");
                brVrste = 0;
                label7.Text = "Podaci su uneti dobro";
                label7.Visible = true;
                TxtLoad();
            }
            catch
            {
                label7.Text = "Dati informacije nisu uneseni dobro";
                label7.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(textBox1.Text);
                int smer, godina, razredni;
                string[] ime_prezime = comboBox2.Text.Split(' ');


                izmena = new DataTable();
                izmena = Konekcija.Unos("SELECT id FROM Smer WHERE naziv = '" + comboBox1.Text + "'");
                izmena1 = new DataTable();
                izmena1 = Konekcija.Unos("SELECT id FROM Osoba WHERE ime = '" + ime_prezime[0] + "'" + " AND prezime = '" + ime_prezime[1] + "'");
                izmena2 = new DataTable();
                izmena2 = Konekcija.Unos("SELECT id FROM Skolska_godina WHERE naziv = '" + comboBox3.Text + "'");


                godina = (int)izmena.Rows[0][0];
                smer = (int)izmena.Rows[0][0];
                razredni = (int)izmena.Rows[0][0];
                
                komanda = new SqlCommand();
                komanda.CommandText = (" UPDATE Odeljenje SET indeks = " + Convert.ToInt32(textBox3.Text) + " WHERE id = " + ID +
                    " UPDATE Odeljenje SET razred = " + Convert.ToInt32(textBox2.Text) + " WHERE id = " + ID +
                    " UPDATE Odeljenje SET smer_id = " + smer + " WHERE id = " + ID +
                    " UPDATE Odeljenje SET razredni_id = " + razredni + " WHERE id = " + ID +
                    " UPDATE Odeljenje SET godina_id = " + godina + " WHERE id = " + ID);

                SqlConnection kon = new SqlConnection(Konekcija.Veza());
                kon.Open();
                komanda.Connection = kon;
                komanda.ExecuteNonQuery();
                kon.Close();

                ucenici = new DataTable();
                ucenici2 = new DataTable();
                ucenici3 = new DataTable();
                ucenici4 = new DataTable();

                ucenici = Konekcija.Unos("SELECT * FROM Odeljenje");
                ucenici2 = Konekcija.Unos("SELECT naziv FROM Smer JOIN Odeljenje ON smer.id = odeljenje.smer_id");
                ucenici3 = Konekcija.Unos("SELECT ime + ' ' + prezime from Osoba JOIN Odeljenje ON osoba.id = odeljenje.razredni_id");
                ucenici4 = Konekcija.Unos("SELECT naziv FROM Skolska_godina JOIN Odeljenje ON odeljenje.godina_id = skolska_godina.id");


                TxtLoad();
                label7.Text = "Podaci su uspesno izmenjeni!";
                label7.Visible = true;
            }
            catch
            {
                label7.Text = "Podaci nisu dobro uneti!";
                label7.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (brVrste > 0)
            {
                brVrste--;
                TxtLoad();
                label7.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brVrste = 0;
            TxtLoad();
            label7.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            brVrste = ucenici.Rows.Count - 1;         
            TxtLoad();
            label7.Visible = false;
        }
    }
}
