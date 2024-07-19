using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaundryModel;
namespace LaundryData
{
    public class SqlDbData
    {
        static string connectionString
            
        = "Data Source =ENZO\\SQLEXPRESS; Initial Catalog = MattsLaundry; Integrated Security = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        public static void Connect()
        {
            sqlConnection.Open();
        }

        public static List<User> GetUsers()
        {
            string selectStatement = "SELECT * FROM users";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            List<User> users = new List<User>();

            while (reader.Read())
            {
                string name = reader["name"].ToString();
                string clWeight = reader["clWeight"].ToString();

                User readUser = new User();
                readUser.name = name;
                readUser.clWeight = clWeight;

                users.Add(readUser);
            }

            sqlConnection.Close();

            return users;
        }

        public static int AddUser(string name, string clWeight)
        {
            int success;

            string insertStatement = "INSERT INTO users VALUES (@name, @clWeight)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@name", name);
            insertCommand.Parameters.AddWithValue("@clWeight", clWeight);
            sqlConnection.Open();

            success = insertCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;

        }

        public static void UpdateUser(string name, string clWeight)
        {
            var updateStatment = $"UPDATE users SET clWeight = @clWeight WHERE name = @name";
            SqlCommand updateCommand = new SqlCommand(updateStatment, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@name", name);
            updateCommand.Parameters.AddWithValue("@clWeight", clWeight);

            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static void DeleteUser(string name)
        {
            string deleteStatement = $"DELETE FROM users WHERE name = @name";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@name", name);

            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();

        }

    }
}

