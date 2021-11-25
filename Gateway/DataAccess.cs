using Dapper;
using System.Data;
using Model;

namespace Gateway
{
    public static class DataAccess
    {
        //public DataAccess CreateAccount(string email, string password)
        //{
        //    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StudentMatcherDB")))
        //    {
        //        var output = connection.Query($"SELECT COUNT(1) FROM Account WHERE Email == {email} AND Password == {password}");
        //        return output;
        //    }
        //}

        public static void CreateAccount(Account account)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute("INSERT INTO Account(Email, Password) VALUES (@Email, @Password)", account); // Better is to use a StoredProcedure later on
            }
        }
    }
}
