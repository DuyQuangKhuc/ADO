using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
            "Data Source=DESKTOP-O6LJBT8\\LONG;database=MyStoreDB;uid=sa;pwd=long;" +
            "Integrated Security=True;TrustServerCertificate=True;Encrypt = False";//không kiểm tra tính hợp lệ của SSL/TLS trong sql server.

        // Provide the query string with a parameter placeholder.
        //string queryString =
        //    "SELECT ProductID, UnitPrice, ProductName from dbo.products "
        //        + "WHERE UnitPrice > @pricePoint "
        //        + "ORDER BY UnitPrice DESC;";


        //  // Specify the parameter value.
        int choice;
        while (true)
        {
            Console.WriteLine("Moi nhap lua chon");
            switch (choice = int.Parse(Console.ReadLine()))
            {
                case 0:
                    string queryString =
               "SELECT * from Products;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(queryString, connection);
                        //   command.Parameters.AddWithValue("@pricePoint", paramValue);

                        // Open the connection in a try/catch block.
                        // Create and execute the DataReader, writing the result
                        // set to the console window.
                        try
                        {
                            Console.WriteLine("\tID\tTEN\tLOAI\tPRICE\tUnitPrice");
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                                    reader[0], reader[1], reader[2], reader[3], reader[4]);
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();
                    }
                    break;
                case 1:
                    Console.WriteLine("Ten la: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("ID category: ");
                    int categoryid = int.Parse(Console.ReadLine());
                    Console.WriteLine("Price la: ");
                    int price = int.Parse(Console.ReadLine());
                    Console.WriteLine("UnitPrice la: ");
                    int unitPrice = int.Parse(Console.ReadLine());

                    string InserString = "Insert into Products values(@name,@categoryid,@price,@unitprice);";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(InserString, connection);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@categoryid", categoryid);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@unitprice", price);
                        //   command.Parameters.AddWithValue("@pricePoint", paramValue);
                        // Open the connection in a try/catch block.
                        // Create and execute the DataReader, writing the result
                        // set to the console window.
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Them moi thanh cong");
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();
                    }
                    break;
                case 2:


                    string store = "CREATE PROCEDURE GetProductByID\r\n(\r\n   @Id INT\r\n)\r\nAS\r\nBEGIN\r\n     SELECT ProductId, ProductName, CategoryId, UnitInStock, UnitPrice\r\n     FROM Products\r\n     WHERE ProductId = @Id\r\nEND;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(store, connection);
                        //   command.Parameters.AddWithValue("@pricePoint", paramValue);
                        // Open the connection in a try/catch block.
                        // Create and execute the DataReader, writing the result
                        // set to the console window.
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Store procedure thanh cong");
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();
                    }
                    break;
                case 3:
                    try
                    {
                        //Store the connection string in the ConnectionString variable


                        //Create the SqlConnection object
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            //Create the SqlCommand object
                            SqlCommand cmd = new SqlCommand()
                            {
                                CommandText = "GetProductByID",
                                Connection = connection,
                                CommandType = CommandType.StoredProcedure
                            };

                            //Create an instance of SqlParameter
                            SqlParameter param1 = new SqlParameter
                            {
                                ParameterName = "@Id",
                                SqlDbType = SqlDbType.Int,
                                Value = 2,
                                Direction = ParameterDirection.Input
                            };

                            cmd.Parameters.Add(param1);

                            connection.Open();

                            SqlDataReader sdr = cmd.ExecuteReader();
                            while (sdr.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                                     sdr[0], sdr[1], sdr[2], sdr[3], sdr[4]);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception Occurred: {ex.Message}");
                    }
                    break;
                case 4:
                    int getid = int.Parse(Console.ReadLine());
                    Console.WriteLine("Nhaapj id: ");
                    string queryGET ="SELECT * from Products where productid = @id;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(queryGET, connection);
                        command.Parameters.AddWithValue("@id", getid);

                        // Open the connection in a try/catch block.
                        // Create and execute the DataReader, writing the result
                        // set to the console window.
                        try
                        {
                            Console.WriteLine("\tID\tTEN\tLOAI\tPRICE\tUnitPrice");
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                                    reader[0], reader[1], reader[2], reader[3], reader[4]);
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();
                    }
                    break;
                case 5:
                    int deleteid = int.Parse(Console.ReadLine());
                    Console.WriteLine("Nhaapj id: ");
                    string deleteProduct = "Delete * from Products where productid =@id";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(deleteProduct, connection);
                        command.Parameters.AddWithValue("@id", deleteid);
                        //   command.Parameters.AddWithValue("@pricePoint", paramValue);
                        // Open the connection in a try/catch block.
                        // Create and execute the DataReader, writing the result
                        // set to the console window.
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Xoa thanh cong");
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();
                    }
                    break;
                default:
                    Console.WriteLine("Moi nhap dung");
                    break;
            }
        }
    }
    // Create and open the connection in a using block. This
    // ensures that all resources will be closed and disposed
    // when the code exits.

}
