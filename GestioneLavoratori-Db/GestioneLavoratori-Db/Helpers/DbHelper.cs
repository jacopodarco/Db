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

                CommandText = "INSERT INTO Lavoratori(ID,Nome,Cognome,Eta,Retribuzione,Tipo,RAL,Tasse)" +
                " VALUES" +
                "(@ID, @Nome,@Cognome,@Eta,@Retribuzione,@Tipo,@RAL,@Tasse)"
            };

            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = l.ID;
            cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 255).Value = l.Nome;
            cmd.Parameters.Add("@Cognome", SqlDbType.NVarChar, 255).Value = l.Cognome;
            cmd.Parameters.Add("@Eta", SqlDbType.Int).Value = l.Eta;
            cmd.Parameters.Add("@Retribuzione", SqlDbType.Float).Value = l.Retribuzione;
            cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = l.Tipo;
            cmd.Parameters.Add("@RAL", SqlDbType.Float).Value = l.RAL;
            cmd.Parameters.Add("@Tasse", SqlDbType.Float).Value = l.Tasse();

            connection.Open();

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("SUCCESSO!", result);
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

            Console.WriteLine("SUCCESSO!");
        }
        public static int Update(Lavoratore l)
        {
            int result = 0;

            string updateQuery = "UPDATE Lavoratori SET Nome=@Nome,Cognome=@Cognome," +
                "Retribuzione=@retribuzione, RAL=@RAL, Tasse=@Tasse," +
                "Tipo=@Tipo " +
                "WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = updateQuery
            };
            cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 255).Value = l.Nome;
            cmd.Parameters.Add("@Cognome", SqlDbType.NVarChar, 255).Value = l.Cognome;
            cmd.Parameters.Add("@Retribuzione", SqlDbType.Float).Value = l.Retribuzione;
            cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = l.Tipo;
            cmd.Parameters.Add("@RAL", SqlDbType.Float).Value = l.RAL;
            cmd.Parameters.Add("@Tasse", SqlDbType.Float).Value = l.Tasse();

            cmd.Parameters.AddWithValue("@ID", l.ID);
            cmd.Connection.Open();
            result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            Console.WriteLine("SUCCESSO!");

            return result;
        }

            public static DataSet GetLav()
        {
            DataSet result = new DataSet();
            string selectQuery = "SELECT ID, Nome, Cognome, Eta, Tipo, Retribuzione,RAL,Tasse " +
                "FROM Lavoratori";

            SqlCommand cmd = new SqlCommand(selectQuery, GetConnection());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(result);
            return result;
        }
    }
}
