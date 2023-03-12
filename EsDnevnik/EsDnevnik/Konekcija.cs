using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace EsDnevnik
{
    internal class Konekcija
    {
        static public SqlConnection Connect()
        {
            string CS;
            CS = ConfigurationManager.ConnectionStrings["home"].ConnectionString;
            // MessageBox.Show(CS);
            SqlConnection conn = new SqlConnection(CS);
            return conn;
        }
        static public DataTable Unos(string Komanda)
        {
            DataTable Tabela = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(Komanda, Konekcija.Connect());
            adapter.Fill(Tabela);
            return Tabela;
        }

        static public string Veza() 
        { 
            return ConfigurationManager.ConnectionStrings["home"].ConnectionString; 
        }
    }
}
