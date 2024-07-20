using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace schooldb.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString 
        {
            get 
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;


            } 
        }

        /// <summary>
        /// Returns connection to the school database.
        /// </summary>
        /// <example>
        /// private schooldbcontext school = new schooldbcontext();
        /// MySqlConnection Conn = school. AccessDatabase();
        /// </example>
        /// <returns> MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            // We are initiating the MySqlConnection Class to create an object
            // the object is a specific connection to our blog database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }







    }
}