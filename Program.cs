using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your notes applications");
            Console.WriteLine("Please, Type your name ");    
            string name = Console.ReadLine(); 
            var configurationString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            Notes getNote = new Notes(configurationString,name);
            
            
               
        }
        public static void RestartApplication()
        {

            Console.Write("Desea reiniciar el programa ? Y/N ");
            string response = Console.ReadLine();
            if (response.ToUpper() == "N")
            {
                Environment.Exit(0);
            }
            else if (response.ToUpper() == "Y")
            {
                Main(new string[] { });
            }
            else { RestartApplication(); }
        }
    }
}
