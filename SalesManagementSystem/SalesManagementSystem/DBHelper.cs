using DevExpress.Xpo.DB.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
public class DBHelper : IDisposable
{
    public NpgsqlConnection connection;
    public NpgsqlCommand command;
    public NpgsqlDataAdapter dataAdapter;

    public DBHelper(string query)
    {
        connection = new NpgsqlConnection("server=localhost; port=5432; Database=SalesTrackingSystem; user ID=postgres; password=ecem");
        connection.Open();
        command = new NpgsqlCommand(query, connection);
        dataAdapter = new NpgsqlDataAdapter(command);
    }

    public void Dispose()
    {
        if (connection != null && connection.State == ConnectionState.Open)
            connection.Close();
        dataAdapter?.Dispose();
        command?.Dispose();
        connection?.Dispose();
    }
}
