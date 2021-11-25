using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Gateway
{
    public class AccountRepository
    {
        private SetupSQLConnection sqlConnection;

        public AccountRepository()
        {
            sqlConnection = new SetupSQLConnection();
        }

        //public void Login(string email, string password)
        //{
        //    SqlCommand cmd = sqlConnection.Connection.CreateCommand();
        //    cmd.CommandText = "SELECT COUNT(1) FROM Account WHERE Email=@email AND Password=@password";
        //    cmd.Parameters.AddWithValue("@email", email);
        //    cmd.Parameters.AddWithValue("@password", password);

        //    int count = Convert.ToInt32(cmd.ExecuteScalar());

        //    if (count == 1)
        //    {
        //        this.Close();
        //    }
        //    else
        //    {

        //    }

        //    try
        //    {
        //        sqlConnection.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sqlConnection.Connection.Close();
        //    }
        //}

        //public void Register(string email, string password)
        //{
        //    SqlCommand cmd = sqlConnection.Connection.CreateCommand();
        //    cmd.CommandText = "INSERT INTO Account(email,password,password_changed)VALUES(@email,@password,@password_changed)";
        //    cmd.Parameters.AddWithValue("@ID", ID);
        //    cmd.Parameters.AddWithValue("@FirstName", FirstName);
        //    cmd.Parameters.AddWithValue("@Address", Address);
        //    try
        //    {
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
    }
}
