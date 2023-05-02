using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp
{
    internal class Notes
    {
        private DataTable _dt = new DataTable();
        private string _name; 
        public Notes(string configString, string name)
        {
            using (SqlConnection sql = new SqlConnection(configString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_get_user_notes", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    sql.Open();
                    cmd.Parameters.Add(new SqlParameter("@Nombre", $"%{name}%"));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine($"Para el sr {reader.GetString(reader.GetOrdinal("Nombre"))}");
                            while (reader.Read())
                            {
                                
                                Console.WriteLine($"tiene la tarea de {reader.GetString(reader.GetOrdinal("texto"))} para el dia {reader.GetDateTime(reader.GetOrdinal("to_day")).ToString("dd/MM/yyyy")}");
                            }
                            var dt = new DataTable();
                            var da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            _dt= dt;
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron notas para el usuario especificado.");
                            Program
                        }
                    }
                }
            }

            Console.Write("You wanna create new Task ?");
            string response = Console.ReadLine();
            if (response == "y" || response == "Y") 
            { 
                CreateNote newNote = new CreateNote();
            }
            
        }
        public DataTable PrintDataTable { 
            get 
            {
                
                return _dt;
            } 
        }


    }
}
