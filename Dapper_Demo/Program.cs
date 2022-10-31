using Dapper;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;


            //read data
            IDbConnection db = new SqlConnection(connectionString);
            db.Open();
            string sql = "Select * from Wallets";
            var results = db.Query(sql);
            foreach (var row in results)
            {
                Console.WriteLine(row);
            }


            //insert data
            var wallet = new Wallet { Holder = "test", Balance = 16000m };
            var sqlScript = "INSERT INTO WALLETS (Holder, Balance) " +
                "VALUES (@Holder, @Balance)"
                + "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var parameters = new { Holder = wallet.Holder, Balance = wallet.Balance };

            wallet.Id = db.Query<int>(sqlScript, parameters).SingleOrDefault();

            Console.WriteLine(wallet);


            //update data
            var walletTodUpdated = new Wallet { Id = 3, Holder = "updated", Balance = 16000m };
            var sqlSc = "UPDATE WALLETS SET Holder = @Holder, Balance = @Balance where ID = @ID";
            var parameterss = new
            {
                Id = walletTodUpdated.Id,
                Holder = walletTodUpdated.Holder,
                Balance = walletTodUpdated.Balance
            };

            //db.Execute(sqlSc, parameterss);


            //delete data
            var walletToDelete = new Wallet { Id = 4 };
            var sqlDelete = "DELETE FROM WALLETS WHERE ID = @Id;";
            var parammms = new { Id = walletToDelete.Id };

            db.Execute(sqlDelete, parammms);


            //Execute multiple queries in one batch
            var sqlBatch = "SELECT MIN(Balance) FROM WALLETS;" +
                            "SELECT MAX(Balance) FROM WALLETS";

            var multi = db.QueryMultiple(sqlBatch);

            Console.WriteLine($"Min=> {multi.ReadSingle<decimal>()}" +
                $"  MAx=> {multi.ReadSingle<decimal>()}");

        }
    }

    public class Wallet
    {
        public int Id { get; set; }
        public string Holder { get; set; }
        public decimal? Balance { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Holder} ({Balance:C})";
        }
    }
}
