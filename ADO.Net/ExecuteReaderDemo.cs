using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.Net
{
    internal class ExecuteReaderDemo
    {
        public void Doo()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

            var connection = new SqlConnection(config.GetSection("conString").Value);

            var sql = "SELECT * FROM WALLETS";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.CommandType = System.Data.CommandType.Text;
            connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            List<Wallet> wallets = new List<Wallet>();
            while (dataReader.Read())
            {
                wallets.Add(new Wallet
                {
                    ID = Convert.ToInt32(dataReader["ID"]),
                    Holder = Convert.ToString(dataReader["Holder"]),
                    Balance = Convert.ToDecimal(dataReader["Balance"])
                });
            }
        }
    }
}
