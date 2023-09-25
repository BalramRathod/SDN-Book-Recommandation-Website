using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.EHRContext
{
    public class DapperContext
    {

        private readonly IConfiguration _configuration;
        private readonly string _ConnectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _ConnectionString = configuration.GetConnectionString("con");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_ConnectionString);
    }
}
