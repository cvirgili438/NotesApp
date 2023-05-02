using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp
{
    internal class CreateNote
    {
        public CreateNote(string name, string configString) 
        {
            try {
                Console.Write("Please insert a valid note");
                string note = Console.ReadLine();
                if (note == null)
                {
                    throw new ArgumentNullException();                    
                }
                Console.Write("Insert the day for the note or some day closer");
                int day = int.Parse(Console.ReadLine());
                if (day > 31 || day < 1) 
                { 
                    throw new FormatException(); 
                }
                Console.Write("Inserte the Month");
                int month = int.Parse(Console.ReadLine());
                if (month >12 || month < 1 ) 
                {
                    throw new Exception("Valid month please"); 
                }
                Console.Write("Inserte the Year");
                int year = int.Parse(Console.ReadLine());
                if (year < 2023) { throw new Exception("Valid year please"); }
                DateTime date = new DateTime(year,month,day);
                
                using (SqlConnection sql = new SqlConnection(configString)) 
                {
                    using (SqlCommand cmd = new SqlCommand("sp_create_note",sql)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        sql.Open();
                        cmd.Parameters.Add("@texto",note);
                        cmd.Parameters.Add("@to_day",date);
                        cmd.Parameters.Add("@nombre", name);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (FormatException){
                Console.WriteLine("Please insert a valid Number");
            }
            catch(Exception) {
                Console.WriteLine("Invalid Note");
                CreateNote newNote = new CreateNote(name,configString);
            }
            
           

        } 
    }
}
