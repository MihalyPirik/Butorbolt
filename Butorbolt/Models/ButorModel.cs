using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Butorbolt.Models
{
    public class ButorModel
    {
        public int id { get; set; }
        public string megnevezes { get; set; }
        public int? ar { get; set; }
        public string szin { get; set; }
        public DateTime? szallitas { get; set; }
        public int alapanyag_id { get; set; }
        public string alapanyagNev { get; set; }

        public ButorModel()
        {

        }

        public ButorModel(MySqlDataReader reader)
        {
            this.id = Convert.ToInt32(reader["id"]);
            this.megnevezes = reader["megnevezes"].ToString();
            this.ar = reader["ar"] == DBNull.Value ? null : Convert.ToInt32(reader["ar"]);
            this.szin = reader["szin"].ToString();
            this.szallitas = reader["szallitas"] == DBNull.Value ? null : Convert.ToDateTime(reader["szallitas"]);
            this.alapanyag_id = Convert.ToInt32(reader["alapanyag_id"]);
            this.alapanyagNev = reader["alapanyagnev"].ToString();
        }

        public static List<ButorModel> select(string megnevezes, int? alapanyagID)
        {
            List<ButorModel> lista = new List<ButorModel>();
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                string sql = "SELECT butor.id," + "butor.megnevezes, " +
                             "butor.ar, " +
                             "butor.szin, " +
                             "butor.szallitas, " +
                             "butor.alapanyag_id, " +
                             "alapanyag.megnevezes AS alapanyagnev " +
                             "FROM butor " +
                             "INNER JOIN alapanyag ON butor.alapanyag_id = alapanyag.id " +
                             "WHERE 1=1 ";
                if (!String.IsNullOrEmpty(megnevezes))
                {
                    sql += "AND butor.megnevezes LIKE @megnevezes ";
                }
                if (alapanyagID != null)
                {
                    sql += "AND butor.alapanyag_id = @alapanyagId ";
                }
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@megnevezes", "%" + megnevezes + "%");
                    cmd.Parameters.AddWithValue("@alapanyagId", alapanyagID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ButorModel(reader));
                        }
                    }
                }
                con.Close();
            }
            return lista;
        }

        public static int insert(ButorModel model)
        {
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "INSERT INTO butor (megnevezes, ar, szin, szallitas, alapanyag_id) " +
                          "VALUES (@megnevezes, @ar, @szin, @szallitas, @alapanyag_id)";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@megnevezes", model.megnevezes);
                    cmd.Parameters.AddWithValue("@ar", model.ar);
                    cmd.Parameters.AddWithValue("@szin", model.szin);
                    cmd.Parameters.AddWithValue("@szallitas", model.szallitas);
                    cmd.Parameters.AddWithValue("@alapanyag_id", model.alapanyag_id);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        public static void update(ButorModel model)
        {
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "UPDATE butor SET megnevezes = @megnevezes, ar = @ar, szin = @szin, szallitas = @szallitas, alapanyag_id = @alapanyag_id WHERE id = @id";
                using var cmd = new MySqlCommand(sql, con);
                {
                    cmd.Parameters.AddWithValue("@id", model.id);
                    cmd.Parameters.AddWithValue("@megnevezes", model.megnevezes);
                    cmd.Parameters.AddWithValue("@ar", model.ar);
                    cmd.Parameters.AddWithValue("@szin", model.szin);
                    cmd.Parameters.AddWithValue("@szallitas", model.szallitas);
                    cmd.Parameters.AddWithValue("@alapanyag_id", model.alapanyag_id);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public static void delete(ButorModel model)
        {
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                var sql = "DELETE FROM butor WHERE id = @id";
                using var cmd = new MySqlCommand(sql, con);
                {
                    cmd.Parameters.AddWithValue("@id", model.id);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
