using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butorbolt.Models
{
    public class AlapanyagModel
    {
        public int? id { get; set; }
        public string megnevezes { get; set; }

        public AlapanyagModel(MySqlDataReader reader)
        {
            this.id = Convert.ToInt32(reader["id"]);
            this.megnevezes = reader["megnevezes"].ToString();
        }

        public static List<AlapanyagModel> select()
        {
            List<AlapanyagModel> lista = new List<AlapanyagModel>();
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                string sql = "SELECT * FROM alapanyag";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new AlapanyagModel(reader));
                        }
                    }
                }
                con.Close();
            }
            return lista;
        }

        public AlapanyagModel(int? id, string megnevezes)
        {
            this.id = id;
            this.megnevezes = megnevezes;
        }

    }
}
