using System.Data.SqlClient;


namespace Api.Services.Core.SmartCharging.Infrastructure
{
    public static class SetupDataBase
    {
        public static void Initialize()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=SmartCharging;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Identifier, Name, Capacity FROM MyGroup;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Access data from the reader
                                int column1Value = reader.GetInt32(0);
                                string column2Value = reader.GetString(1);
                                int column3Value = reader.GetInt32(2);

                                // Process the data as needed
                                Console.WriteLine($"Column1: {column1Value}, Column2: {column2Value}, Column3: {column3Value}");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle any exceptions that might occur during the SQL query execution
                Console.WriteLine("Error executing SQL query: " + ex.Message);
            }


        }
    }
}