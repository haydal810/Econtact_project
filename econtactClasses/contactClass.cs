using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact_project.econtactClasses
{
    class contactClass
    {
        // Getter Setter properties
        // acts as Data Carrier on the application

        // type 'prop' and tap tab twice to produce below lines and fill in the field names:
        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //selecting data from database
        public DataTable Select() 
        {
            // Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // Step 2: Write an SQL query
                string sql = "Select * from tbl_contact";

                // Create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Create SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
                     

        }

        // Inserting Data into Database
        public bool Insert(contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            // Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            
            try
            {
                // Step 1: Create SQL Query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNumber, Address, Gender) VALUES (@FirstName, @LastName, @ContactNumber, @Address, @Gender)";
                // Create sql Command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create parametrs to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //Connection Open here:

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                // If the query runs successfully then the vakue of rows will be greater than zero else it's value will be zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
    }

        // Update Data in Database
        public bool Update(contactClass c)
        {
            // Create a default return value and set its default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {
                //SQL to Update data in our database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNumber=@ContactNumber, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                // Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create Parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", c.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);

                //Open database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                // If the query runs successfully, then the value of rows will be greater than zero else it's value will be zero:

                if (rows > 0) 
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        // Delete Data in Database
        public bool Delete(contactClass c)
        {
            //Create a default return value abd set it's value to false
            bool isSuccess = false;
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs successfully, then the value of rows is greater than zero, else it's value is 0.
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }
    }
}
