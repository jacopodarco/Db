using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GestioneLavoratori_Db.Helpers
{
    public static class DbHelper
    {
        private static SqlConnection connection;
        private static SqlConnection GetConnection()
        {
            if (connection == null)
            {
                string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
                connection = new SqlConnection(connectionString);
            }

            return connection;

        }

        public static void Insert(Lavoratore l)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = GetConnection(),

                CommandType = CommandType.Text,

                CommandText = "INSERT INTO Lavoratori(ID,Nome,Cognome,Eta,Retribuzione,Tipo)" +
                " VALUES" +
                "(@ID, @Nome,@Cognome,@Eta,@Retribuzione,@Tipo)"
            };

            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = l.ID;
            cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 255).Value = l.Nome;
            cmd.Parameters.Add("@Cognome", SqlDbType.NVarChar, 255).Value = l.Cognome;
            cmd.Parameters.Add("@Eta", SqlDbType.Int).Value = l.Eta;
            cmd.Parameters.Add("@Retribuzione", SqlDbType.Float).Value = l.Retribuzione;
            cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = l.Tipo;

            connection.Open();

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("{0} istanza inserita", result);
        }

        public static void Svuota(string tabella)
        {
            string deleteQuery = string.Format("DELETE from {0}", tabella);

            SqlCommand cmd = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = deleteQuery
            };

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Hai eliminato tutte le istanze congratulazioni");
        }
        public static DataSet GetLav()
        {
            DataSet result = new DataSet();
            string selectQuery = "SELECT ID, Nome, Cognome, Eta, Tipo, Retribuzione " +
                "FROM Lavoratori";

            SqlCommand cmd = new SqlCommand(selectQuery, GetConnection());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(result);
            return result;
        }
    }
}
