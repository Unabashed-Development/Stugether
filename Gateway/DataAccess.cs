﻿using Dapper;
using System.Data;
using Model;
using System;

namespace Gateway
{
    public static class DataAccess
    {
        #region Methods
        // These methods might be able to turn into generic methods, but that might be a bit too difficult or not useful enough.

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
        /// <param name="verificationCode">The verification code generated by the program.</param>
        public static void CreateAccount(Account account, string verificationCode)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute($"INSERT INTO Account(Email, Password, VerificationCode) VALUES (@Email, @Password, {verificationCode})", account); // Better is to use a StoredProcedure later on
            }
        }

        /// <summary>
        /// Checks if the given verification code matches the verification code in the database for the account. Also sets the verified column to true (1).
        /// </summary>
        /// <param name="verificationCode">The verification code that needs to be checked with the database.</param>
        /// <param name="account">The account the verification code needs to be checked for.</param>
        /// <returns>True if the verification code matches, false if not.</returns>
        public static bool CheckIfVerificationCodeMatches(string verificationCode, Account account)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                string verificationCodeInDatabase = connection.QuerySingle<string>("SELECT VerificationCode FROM Account WHERE Email = @Email", account);
                if (verificationCodeInDatabase == verificationCode)
                {
                    connection.Execute($"UPDATE Account SET AccountVerified = 1 WHERE Email = @Email", account);
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}