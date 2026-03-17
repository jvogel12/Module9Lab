using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class CustomersModel : PageModel
    {
        // Aatributes for the CustomersModel class
        //Create a list to hold the list of customers that we retrieve from the database
        public List<Customer> Customers {get; set;} 
        // The onGet method is called when the page is accessed via a get request
        public void OnGet()
        {
            //Initialize the list of customers
            Customers = new List<Customer>();

            //Set up the string with the database connection information
            string connectionString = "Server=localhost;Database=Northwind;UserID=sa;Password=P@ssw0rd;TrustServerCertificate=True;";

            //Open our database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Open the db connection
                connection.Open();

                //Create a string to hold the sql statement that we want to execute against the database
                string sql = "SELECT CustomerID, CompanName, ContactName, Country FROM Customers";

                //Run the SQL statement against the db
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //Create a new data reader to read the records
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Loop through the result set - the records we got from the db
                        while (reader.Read())
                        {
                            //Create a new customer object & add it to our list of Customers
                            Customers.Add(new Customer {
                                CustomerID = reader.GetString(0),
                                CompanyName = reader.GetString(1),
                                ContactName = reader.GetString(2),
                                Country = reader.GetString(3)
                            });
                        }//end loop
                    }
                }
            }
        }
    }
}

//Class for representing a customer from Northwind database
public class Customer
{
    public string CustomerID { get; set; }
    public string CompanyName {get; set; }
    public string ContactName {get; set;}
    public string Country {get; set;}
} //end customer class