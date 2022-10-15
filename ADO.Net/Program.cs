using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;

namespace ADO.Net
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

            var connection = new SqlConnection(config.GetSection("conString").Value);

            var wallet = new Wallet
            {
                Holder = "mehrez",
                Balance = 5000
            };

            var sql = "INSERT INTO WALLETS (HOLDER, BALANCE) VALUES" +
                "(@HOLDER, @BALANCE)";

            SqlParameter holderParam = new SqlParameter()
            {
                ParameterName = "@HOLDER",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Direction = System.Data.ParameterDirection.Input,
                Value = wallet.Holder
            };

            SqlParameter balanceParam = new SqlParameter()
            {
                ParameterName = "@BALANCE",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Input,
                Value = wallet.Balance
            };

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add(holderParam);
            cmd.Parameters.Add(balanceParam);
            cmd.CommandType = CommandType.Text;

            connection.Open();

            if (cmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet for {wallet.Holder} added successfully!");
            }
            else
            {
                Console.WriteLine("Error");
            }

            connection.Close();
        }
    }
}
