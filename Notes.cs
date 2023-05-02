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
                            int whil = 0;
                            
                            while (reader.Read())
                            {
                                if (whil <1) {Console.WriteLine($"Para el sr {reader.GetString(reader.GetOrdinal("Nombre"))}"); }
                                whil++;

                                Console.WriteLine($"tiene la tarea de \t {reader.GetString(reader.GetOrdinal("texto"))} \n para el dia {reader.GetDateTime(reader.GetOrdinal("to_day")).ToString("dd/MM/yyyy")}");
                            }
                           
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron notas para el usuario especificado.");
                            Program.RestartApplication();
                        }
                    }
                }
            }

            Console.Write("You wanna create new Task?  Y / N ");
            string response = Console.ReadLine();
            if (response == "y" || response == "Y")
            {
                CreateNote newNote = new CreateNote($"%{name}%",configString);
            }
            else { }
            
        }
        public DataTable PrintDataTable { 
            get 
            {
                
                return _dt;
            } 
        }


    }
}
