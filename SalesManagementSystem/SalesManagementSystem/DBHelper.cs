using DevExpress.Xpo.DB.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public class DBHelper
    {
        public NpgsqlConnection connection;
        public NpgsqlCommand command;
        public NpgsqlDataAdapter dataAdapter;
        public NpgsqlDataReader ndr;

        public DBHelper(string query)
        {
            connection = new NpgsqlConnection("server=localhost; port=5432; Database=SalesTrackingSystem; user ID=postgres; password=admin");
            connection.Open();
            command = new NpgsqlCommand(query, connection);
            dataAdapter = new NpgsqlDataAdapter(command);
            
        }

        ~DBHelper()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

}
