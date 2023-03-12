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
    public partial class Osoba : Form
    {
        DataTable osoba, osoba2, osoba3, osoba4, izmena, izmena1, izmena2;

        private void button6_Click(object sender, EventArgs e)
        {
            if(brVrste < osoba.Rows.Count - 1)
            {
                brVrste++;
                TxtLoad();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            brVrste = osoba.Rows.Count - 1;
            TxtLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text.Length == 13)
                {
                    if (textBox8.Text == "1" || textBox8.Text == "2")
                    {
                        komanda = new SqlCommand();
                        komanda.CommandText = ("INSERT INTO Osoba VALUES (" + "'" + textBox2.Text + "'" + ", " + "'" + textBox3.Text + "'" + ", " + "'"
                        + textBox4.Text + "'" + ", " + "'" + textBox5.Text + "'" + ", " + "'" + textBox6.Text + "'" + ", " + "'" + textBox7.Text + "'" + ", " + int.Parse(textBox8.Text) + ")");

                        SqlConnection kon = new SqlConnection(Konekcija.Veza());
                        kon.Open();
                        komanda.Connection = kon;
                        komanda.ExecuteNonQuery();
                        kon.Close();

                        osoba = new DataTable();
                        osoba = Konekcija.Unos("SELECT * FROM Osoba");
                        label9.Text = "Podatak je uspesno unesen!";
                        label9.Visible = true;
                        TxtLoad();
                    }
                    else {
                        label9.Text = "Mora biti ili 1(ucenik) ili 2(profesor)!";
                        label9.Visible = true;
                    } 
                }
                else {
                    label9.Text = "Unesite JMBG sa 13 cifara!";
                    label9.Visible = true;
                } 

            }
            catch
            {
                label9.Text = "Niste uneli dobre podatke!";
                label9.Visible = true;
            }
        }

        private void Osoba_Load(object sender, EventArgs e)
        {
            osoba = new DataTable();
            osoba = Konekcija.Unos("SELECT * FROM Osoba");
            TxtLoad();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text.Length == 13)
                {
                    if (textBox8.Text == "1" || textBox8.Text == "2")
                    {
                        int ID = Convert.ToInt32(textBox1.Text);
                        komanda = new SqlCommand();
                        komanda.CommandText = ("UPDATE Osoba SET ime = " + "'" + textBox2.Text + "'" + " WHERE id = " + ID);
                        komanda.CommandText = (" UPDATE Osoba SET prezime = " + "'" + textBox3.Text + "'" + " WHERE id = " + ID);
                        komanda.CommandText = (" UPDATE Osoba SET adresa = " + "'" + textBox4.Text + "'" + " WHERE id = " + ID);
                        komanda.CommandText = (" UPDATE Osoba SET jmbg = " + "'" + textBox5.Text + "'" + " WHERE id = " + ID);
                        komanda.CommandText = (" UPDATE Osoba SET email = " + "'" + textBox6.Text + "'" + " WHERE id = " + ID);
                        komanda.CommandText = (" UPDATE Osoba SET pass = " + "'" + textBox7.Text + "'" + " WHERE id = " + ID);
                        komanda.CommandText = (" UPDATE Osoba SET uloga = " + Convert.ToInt32(textBox8.Text) + " WHERE id = " + ID);

                        SqlConnection con = new SqlConnection(Konekcija.Veza());
                        con.Open();
                        komanda.Connection = con;
                        komanda.ExecuteNonQuery();
                        con.Close();

                        osoba = new DataTable();
                        osoba = Konekcija.Unos("SELECT * FROM Osoba");
                        TxtLoad();

                        label9.Text = "Podatak je uspesno izmenjen!";
                        label9.Visible = true;
                    }
                    else
                    {
                        label9.Text = "Uloga je ili 1(ucenik) ili 2(profesor)!";
                        label9.Visible = true;
                    }
                }
                else {
                    label9.Text = "Unesite JMBG sa 13 cifara!";
                    label9.Visible = true;
                }
                
            }
            catch
            {
                label7.Text = "Podaci nisu dobro uneti!";
                label7.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int max_id;
                komanda = new SqlCommand();
                komanda.CommandText = ("DELETE FROM Osoba WHERE id = " + Convert.ToInt32(textBox1.Text));

                izmena = new DataTable();
                izmena = Konekcija.Unos("SELECT * FROM Osoba");

                max_id = osoba.Rows.Count - 1;
                SqlConnection kon = new SqlConnection(Konekcija.Veza());
                kon.Open();
                komanda.Connection = kon;
                komanda.ExecuteNonQuery();
                kon.Close();

                label9.Text = "Podatak uspesno obrisan!";
                label9.Visible = true;

                osoba = new DataTable();
                osoba = Konekcija.Unos("SELECT * FROM Osoba");
                brVrste = 0;
                TxtLoad();
            }
            catch
            {
                label9.Text = "Podatak ne moze biti obrisan!";
                label9.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (brVrste > 0)
            {
                brVrste--;
                TxtLoad();
            }
        }

        int brVrste;
        SqlCommand komanda;

        public Osoba()
        {
            InitializeComponent();
        }

        private void TxtLoad()
        {
            if (osoba.Rows.Count == 1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
            }
            else
            {
                textBox1.Text = osoba.Rows[brVrste][0].ToString();
                textBox2.Text = osoba.Rows[brVrste][1].ToString();
                textBox3.Text = osoba.Rows[brVrste][2].ToString();
                textBox4.Text = osoba.Rows[brVrste][3].ToString();
                textBox5.Text = osoba.Rows[brVrste][4].ToString();
                textBox6.Text = osoba.Rows[brVrste][5].ToString();
                textBox7.Text = osoba.Rows[brVrste][6].ToString();
                textBox8.Text = osoba.Rows[brVrste][7].ToString();
                if (brVrste == 0)
                {
                    button2.Enabled = false;
                    button1.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                    button1.Enabled = true;
                }
                if (brVrste == osoba.Rows.Count - 1)
                {
                    button6.Enabled = false;
                    button7.Enabled = false;
                }
                else
                {
                    button6.Enabled = true;
                    button7.Enabled = true;
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            brVrste = 0;
            TxtLoad();
        }
    }
}
