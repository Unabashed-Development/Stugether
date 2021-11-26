﻿using Dapper;
using System.Data;
using Model;
using System;

namespace Gateway
{
    public static class DataAccess
    {
        /// <summary>
        /// Searches the database for an account.
        /// </summary>
        /// <param name="account">The account to be searched for.</param>
        /// <returns>True if the account exists in the database and false if it does not.</returns>
        public static bool CheckIfAccountExists(Account account)
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
                {
                    connection.QuerySingle("SELECT * FROM Account WHERE Email = @Email", account);
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Searches for the hashed and salted password for an existing account in a database.
        /// </summary>
        /// <param name="account">The account to be searched for.</param>
        /// <returns>The hashed and salted password.</returns>
        public static string GetHashedPassswordFromAccount(Account account)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                return connection.QuerySingle<string>("SELECT Password FROM Account WHERE Email = @Email", account);
            }
        }

        /// <summary>
        /// Creates an account in the database.
        /// </summary>
        /// <param name="account">The account to be added to the database.</param>
        public static void CreateAccount(Account account)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute("INSERT INTO Account(Email, Password) VALUES (@Email, @Password)", account); // Better is to use a StoredProcedure later on
            }
        }
    }
}
