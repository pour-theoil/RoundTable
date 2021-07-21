using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RoundTable.Models;
using RoundTable.Utils;

namespace RoundTable.Repostiories
{

    public class BaseRepository
    {
        private string _connectionString;
        public BaseRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}